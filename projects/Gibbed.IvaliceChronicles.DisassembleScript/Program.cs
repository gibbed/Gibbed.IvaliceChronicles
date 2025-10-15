/* Copyright (c) 2025 Rick (rick 'at' gibbed 'dot' us)
 *
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Gibbed.IvaliceChronicles.ScriptFormats;
using Gibbed.Memory;
using NDesk.Options;

namespace Gibbed.IvaliceChronicles.DisassembleScript
{
    internal class Program
    {
        private static string GetExecutableName()
        {
            return Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
        }

        public static void Main(string[] args)
        {
            GameMode mode = GameMode.Enhanced;
            int? fftpackIndex = null;
            bool showOffset = false;
            bool verbose = false;
            bool showHelp = false;

            var options = new OptionSet()
            {
                { "m=|mode", "set mode to fftpack (event_test_evt.bin), classic, or enhanced (default)", v => mode = ParseEnum<GameMode>(v) },
                { "i=|index", "when in fftpack mode, what script index to disassemble", v => fftpackIndex = int.Parse(v) },
                { "f|offset", "show offsets in output", v => showOffset = v != null },
                { "v|verbose", "be verbose", v => verbose = v != null },
                { "h|help", "show this message and exit", v => showHelp = v != null },
            };

            List<string> extras;
            try
            {
                extras = options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("{0}: ", GetExecutableName());
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `{0} --help' for more information.", GetExecutableName());
                return;
            }

            if (extras.Count < 1 || showHelp == true)
            {
                Console.WriteLine("Usage: {0} [OPTIONS]+ input_script", GetExecutableName());
                Console.WriteLine();
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            var inputPath = extras[0];
            var messagePath = extras.Count > 1 ? extras[1] : null;

            // temporary, until I do real file output
            Console.OutputEncoding = Encoding.UTF8;

            var opcodePadding = 1 + Enum.GetNames(typeof(Opcode)).Max(v => v.Length);

            var scriptBytes = File.ReadAllBytes(inputPath);
            ReadOnlySpan<byte> scriptSpan = new(scriptBytes);

            const Endian endian = Endian.Little;

            string[] messages = null;
            if (mode == GameMode.FFTPack)
            {
                if (mode == GameMode.FFTPack)
                {
                    if (fftpackIndex == null)
                    {
                        Console.WriteLine("warning: did not specify index for FFTPack mode, defaulting to first script");
                    }

                    scriptSpan = scriptSpan.Slice((fftpackIndex ?? 0) * 0x2800, 0x2800);
                }

                var padByte = (byte)Opcode._Pad;
                if (scriptSpan[0] != padByte ||
                    scriptSpan[1] != padByte ||
                    scriptSpan[2] != padByte ||
                    scriptSpan[3] != padByte)
                {
                    var index = 0;
                    var messageOffset = scriptSpan.ReadValueS32(ref index, endian);
                    messages = ReadMessages(scriptSpan.Slice(messageOffset));
                    scriptSpan = scriptSpan.Slice(index, messageOffset - index);
                }
            }
            else if (mode == GameMode.Classic)
            {
                if (string.IsNullOrEmpty(messagePath) == false)
                {
                    var messageBytes = File.ReadAllBytes(messagePath);
                    ReadOnlySpan<byte> messageSpan = new(messageBytes);
                    messages = ReadMessages(messageSpan);
                }
            }

            DumpBody(scriptSpan, ToScriptMode(mode), messages, opcodePadding, showOffset);
        }

        private static ScriptMode ToScriptMode(GameMode mode) => mode switch
        {
            GameMode.FFTPack or
            GameMode.Classic => ScriptMode.Classic,
            GameMode.Enhanced => ScriptMode.Enhanced,
            _ => throw new NotSupportedException(),
        };

        private static string[] ReadMessages(ReadOnlySpan<byte> span)
        {
            List<string> messageList = new();
            int messageStart = 0;
            for (int i = 0; i < span.Length;)
            {
                var b = span[i++];
                int value = b;
                if ((b & 0xF0) == 0xD0)
                {
                    value = (value << 8) | span[i++];
                }
                if (value == 0xFE || value == 0xFF)
                {
                    messageList.Add(DecodeMessage(span.Slice(messageStart, i - messageStart)));
                    messageStart = i;
                }
            }

            span = span.Slice(messageStart);
            int nonpadIndex = -1;
            for (int i = 0; i < span.Length; i++)
            {
                if (span[i] != 0)
                {
                    nonpadIndex = i;
                    break;
                }
            }

            if (nonpadIndex >= 0)
            {
                throw new InvalidOperationException("unexpected data after messages");
            }

            return messageList.ToArray();
        }

        private static void DumpBody(
            ReadOnlySpan<byte> span,
            ScriptMode mode,
            string[] messages,
            int opcodePadding,
            bool showOffset)
        {
            const Endian endian = Endian.Little;

            for (int offset = 0; offset < span.Length;)
            {
                var opcode = (Opcode)span[offset];

                if (showOffset == true)
                {
                    Console.Write("    ");
                    Console.Write($"@{offset:D4} ");
                }

                var size = opcode.GetSize(mode);

                if (size == 0)
                {
                    Console.Write(opcode.ToString());
                }
                else
                {
                    Console.Write(opcode.ToString().PadRight(opcodePadding));

                    var operandsSpan = span.Slice(offset + 1, size);

                    var getOperands = Operands.Get(opcode, mode);
                    if (getOperands == null || Operands.IsUnknown(getOperands) == true)
                    {
                        Console.Write(" unknown:");
                        for (int i = 0; i < size; i++)
                        {
                            Console.Write($" {operandsSpan[i]:X02}");
                        }
                    }
                    else
                    {
                        int operandOffset = 0;
                        foreach (var operandType in getOperands())
                        {
                            var operandSize = operandType.GetSize();
                            if (operandOffset + operandSize > operandsSpan.Length)
                            {
                                Console.Write($" // error, wanted {operandSize} bytes, but {operandsSpan.Length - operandOffset} remaining bytes");
                                break;
                            }
                            object operand;
                            if (operandType == OperandType.UInt16MessageIndex)
                            {
                                int messageIndex = operandsSpan.ReadValueU16(ref operandOffset, endian);
                                operand = messages[messageIndex];
                            }
                            else
                            {
                                operand = operandType switch
                                {
                                    OperandType.Bool8 => operandsSpan.ReadValueB8(ref operandOffset),
                                    OperandType.Bool8OnOff => operandsSpan.ReadValueB8(ref operandOffset) == true ? "on" : "off",
                                    OperandType.Bool8OffOn => operandsSpan.ReadValueB8(ref operandOffset) == true ? "off" : "on",
                                    OperandType.Int8 => operandsSpan.ReadValueS8(ref operandOffset),
                                    OperandType.UInt8 => operandsSpan.ReadValueU8(ref operandOffset),
                                    OperandType.Int16 => operandsSpan.ReadValueS16(ref operandOffset, endian),
                                    OperandType.UInt16 => operandsSpan.ReadValueU16(ref operandOffset, endian),
                                    OperandType.Int32 => operandsSpan.ReadValueS32(ref operandOffset, endian),
                                    OperandType.UInt32 => operandsSpan.ReadValueU32(ref operandOffset, endian),
                                    _ => throw new NotSupportedException(),
                                };
                            }
                            Console.Write($" {operand}");
                        }
                        if (operandOffset < operandsSpan.Length)
                        {
                            Console.Write($" // error, {operandsSpan.Length - operandOffset} remaining bytes");
                        }
                    }
                }
                
                Console.WriteLine();

                offset += 1 + size;
            }
        }

        // https://ffhacktics.com/wiki/Font
        // https://ffhacktics.com/wiki/Text_Format
        private static string DecodeMessage(ReadOnlySpan<byte> span)
        {
            StringBuilder sb = new();
            for (int i = 0; i < span.Length; )
            {
                var b = span[i++];

                int value;
                if ((b & 0xF0) == 0xD0)
                {
                    value = (b << 8) | span[i++];
                }
                else
                {
                    value = b;
                }

                if (value >= 0xE0 && value <= 0xFF)
                {
                    int arg = -1;

                    if (value == 0xE2 || value == 0xE3 || value == 0xE6 || value == 0xE8 ||
                        value == 0xEC || value == 0xF5 || value == 0xF6)
                    {
                        arg = span[i++];
                    }

                    sb.Append('{');
                    sb.Append(value switch
                    {
                        0xE0 => "Ramza",
                        0xE1 => "UnitName",
                        0xE2 => $"Delay {arg}",
                        0xE3 => $"Color {arg}",
                        0xF4 => "WaitPress",
                        0xF8 => "NewLine",
                        0xFB => "BeginList",
                        0xFC => "EndList",
                        0xFD => "StayOpen",
                        0xFE => "End",
                        0xFF => "Close",
                        (>= 0xE4 and <= 0xE5) or
                        0xE7 or
                        (>= 0xE9 and <= 0xEB) or
                        (>= 0xED and <= 0xF3) or
                        0xF7 or 0xF9 => $"0x{value:X2}",
                        0xE6 or 0xE8 or 0xEC or 0xF5 or 0xF6 => $"0x{value:X2}{arg:X2}",
                        _ => throw new NotSupportedException(),
                    });
                    sb.Append('}');

                    if (b == 0xFE || b == 0xFF)
                    {
                        break;
                    }

                    continue;
                }

                if (TryGetChar(value, out string ch) == false)
                {
                    sb.Append('{');
                    sb.Append($"unknown:{value:X}");
                    sb.Append('}');
                }
                else
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString();
        }

        private static bool TryGetChar(int value, out string ch)
        {
            // TODO(gibbed): make this a proper table lookup
            string result = value switch
            {
                0xDA73 => "{SP}",

                >= 0x00 and <= 0x09 => $"{(char)('0' + (value - 0x00))}",
                >= 0x0A and <= 0x23 => $"{(char)('A' + (value - 0x0A))}",
                >= 0x24 and <= 0x3D => $"{(char)('a' + (value - 0x24))}",
                0x3E => "!",
                0x40 => "?",
                0x42 => "+",
                0x44 => "/",
                0x46 => ":",
                0x5F => ".",
                0x8B => "·",
                >= 0x8D and <= 0x8E => $"{(char)('(' + (value - 0x8D))}",
                0x91 => "\\",
                0x93 => "'",
                0x95 => " ", // WotL
                0xFA => "\u3000", // WotL
                >= 0xD129 and <= 0xD132 => "*",
                0xDA60 => "á", // WotL
                0xDA61 => "à", // WotL
                0xDA62 => "é", // WotL
                0xDA63 => "è", // WotL
                0xDA64 => "í", // WotL
                0xDA65 => "ú", // WotL
                0xDA66 => "ù", // WotL
                0xDA67 => "\u2013", // WotL
                0xDA68 => "\u2014", // WotL
                0xDA74 => ",",
                _ => null,
            };
            ch = result;
            return result != null ? true : false;
        }

        private static T ParseEnum<T>(string name)
            where T : struct
        {
            return Enum.TryParse<T>(name, ignoreCase: true, out var value) == false
                ? throw new ArgumentOutOfRangeException(nameof(name))
                : value;
        }
    }
}
