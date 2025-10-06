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
            bool verbose = false;
            bool showHelp = false;

            var options = new OptionSet()
            {
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

            // temporary, until I do real file output
            Console.OutputEncoding = Encoding.UTF8;

            var opcodePadding = 1 + Enum.GetNames(typeof(Opcode)).Max(v => v.Length);

            var scriptBytes = File.ReadAllBytes(inputPath);

            ReadOnlySpan<byte> scriptSpan = new(scriptBytes);
            DumpBody(opcodePadding, scriptSpan, true, false);//.Slice(4, 0x8BE - 4));
        }

        private static void DumpBody(int opcodePadding, ReadOnlySpan<byte> span, bool isEnhanced, bool showOffset)
        {
            const Endian endian = Endian.Little;

            int offset = 0;
            while (span.Length > 0)
            {
                var opcode = (Opcode)span[0];

                if (showOffset == true)
                {
                    Console.Write("    ");
                    Console.Write($"@{offset:D4} ");
                }

                var size = opcode.GetSize(isEnhanced);

                if (size == 0)
                {
                    Console.Write(opcode.ToString());
                }
                else
                {
                    Console.Write(opcode.ToString().PadRight(opcodePadding));

                    var operandsSpan = span.Slice(1, size);

                    var getOperands = Operands.Get(opcode, isEnhanced);
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
                        int index = 0;
                        foreach (var operandType in getOperands())
                        {
                            var operandSize = operandType.GetSize();
                            if (index + operandSize > operandsSpan.Length)
                            {
                                Console.Write($" // error, wanted {operandSize} bytes, but {operandsSpan.Length - index} remaining bytes");
                                break;
                            }
                            object operand = operandType switch
                            {
                                OperandType.Bool8 => operandsSpan.ReadValueB8(ref index),
                                OperandType.Bool8OnOff => operandsSpan.ReadValueB8(ref index) == true ? "on" : "off",
                                OperandType.Bool8OffOn => operandsSpan.ReadValueB8(ref index) == true ? "off" : "on",
                                OperandType.Int8 => operandsSpan.ReadValueS8(ref index),
                                OperandType.UInt8 => operandsSpan.ReadValueU8(ref index),
                                OperandType.Int16 => operandsSpan.ReadValueS16(ref index, endian),
                                OperandType.UInt16 => operandsSpan.ReadValueU16(ref index, endian),
                                OperandType.Int32 => operandsSpan.ReadValueS32(ref index, endian),
                                OperandType.UInt32 => operandsSpan.ReadValueU32(ref index, endian),
                                _ => throw new NotSupportedException(),
                            };
                            Console.Write($" {operand}");
                        }
                        if (index < operandsSpan.Length)
                        {
                            Console.Write($" // error, {span.Length - index} remaining bytes");
                        }
                    }
                }
                
                Console.WriteLine();

                span = span.Slice(1 + size);
                offset += 1 + size;
            }
        }
    }
}
