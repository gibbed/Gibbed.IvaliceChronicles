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

namespace Gibbed.IvaliceChronicles.ScriptFormats
{
    public static class Operands
    {
        static Operands()
        {
            SanityCheck();
        }

        public delegate IEnumerable<OperandType> GetDelegate();

        public static GetDelegate Get(this Opcode opcode, bool isEnhanced) => opcode switch
        {
            Opcode.Unknown00 => Invalid,
            Opcode.Unknown01 => Invalid,
            Opcode.Unknown02 => Invalid,
            Opcode.Unknown03 => Invalid,
            Opcode.Unknown04 => Invalid,
            Opcode.Unknown05 => Invalid,
            Opcode.Unknown06 => Invalid,
            Opcode.Unknown07 => Invalid,
            Opcode.Unknown08 => Invalid,
            Opcode.Unknown09 => Invalid,
            Opcode.Unknown0A => Invalid,
            Opcode.Unknown0B => Invalid,
            Opcode.Unknown0C => Invalid,
            Opcode.Unknown0D => Invalid,
            Opcode.Unknown0E => Invalid,
            Opcode.Unknown0F => Invalid,
            Opcode.DisplayMessage => () => isEnhanced == false
                ? _(U8, U8, MessageIndex, U8, U8, U8, S16, S16, S16, U8)
                : _(U8, U8, U32, U8, U8, U8, S16, S16, S16, U8, U8),
            Opcode.AnimationRequest => () => _(U16, U16, U8), // FFHacktics wiki claims 4 bytes, but size is 5?
            Opcode.WaitAnimationEnd => () => _(U16),
            Opcode.JumpMap => () => _(U8, U8),
            Opcode.Unknown14 => Invalid,
            Opcode.Unknown15 => Invalid,
            Opcode.WaitTask => None,
            Opcode.Unknown17 => Strange,
            Opcode.ChangeEffect => () => _(U16, U8, U8, U8, U8),
            Opcode.MoveCamera => () => _(S16, S16, S16, S16, S16, S16, S16, S16),
            Opcode.MoveAmbient => () => _(U8, U8, U8, U8, U8),
            Opcode.MoveLight => () => _(S16, S16, S16, S16, S16, S16, S16),
            Opcode.ChangeFrameRate => () => _(U8),
            Opcode._CameraFusionStart => None,
            Opcode._CameraFusionEnd => None, // handler not in main code?
            Opcode._Focus => () => _(U8, U8, U8, U8, U8),
            Opcode.Unknown20 => Invalid, // handler removed?
            Opcode.PlaySound => () => _(U16),
            Opcode.PlayMusic => () => _(U8, S8, U8),
            Opcode.Unknown23 => Strange,
            Opcode.Unknown24 => Strange,
            Opcode.Unknown25 => Unknown,
            Opcode.Unknown26 => Unknown,
            Opcode.RewriteMap => None,
            Opcode.MoveToPanel => () => _(U8, U8, U8, U8, U8, U8, U8, U8),
            Opcode.WaitCharacterMove => () => _(U8, U8),
            Opcode._BlockStart => None,
            Opcode._BlockEnd => None, // handler not in main code?
            Opcode.Direction2_0 => () => _(U8, U8, U8, U8, U8, U8, U8),
            Opcode.Direction => () => _(U8, U8, U8, U8, U8, U8),
            Opcode.FadeGradation => () => _(U8, U8, U8, U8, U8, U8, U8, U8),
            Opcode.Unknown2F => Invalid,
            Opcode.Unknown30 => Invalid,
            Opcode.ChangeGradation => () => _(U8, U8, U8, U8, U8),
            Opcode.SetCharacterColor => () => _(U8, U8, U8, U8, U8, U8, U8),
            Opcode.ChangeMapClut => () => _(U8, U8, U8, U8, U8),
            Opcode.Unknown34 => Invalid,
            Opcode.Unknown35 => Invalid,
            Opcode.Unknown36 => Invalid,
            Opcode.Unknown37 => () => _(U16),
            Opcode._FocusSpeed => () => _(U16),
            Opcode.WaitCharacterMoveWotL => None,
            Opcode.WaitFileRead => None,
            Opcode.MoveSprite => MoveSprite,
            Opcode.ChangeWeather => () => _(U8, U8),
            Opcode.DisappearUnit => () => _(U8, U8),
            Opcode.SetChangePaletteData => () => _(U8, U8, U8, U8, U8, U8, U8, S16),
            Opcode.ChangeMapD => () => _(U8, U8, U8, U8),
            Opcode.ChangeMapSTP => () => _(U8, U8, U8, U8, U8),
            Opcode.StartShake => () => _(U8, U8, U8, U8),
            Opcode.StopShake => None,
            Opcode._CallFunction => () => _(U8),
            Opcode._Draw => () => _(U8, U8),
            Opcode.LoadAnimation => () => _(U8, U8, U8),
            Opcode.DeactivateAnimation => () => _(U8, U8),
            Opcode.ActivateAnimation => () => _(U8, U8, U8, U8, U8, U8, U8, U8),
            Opcode.WaitLoadAnimation => None,
            Opcode.ActivateAnimationStart => None,
            Opcode.ActivateAnimationEnd => None, // handler not in main code?
            Opcode.WaitActivateAnimation => None,
            Opcode.JumpMap2 => () => _(U8, U8),
            Opcode._Reveal => () => _(U8),
            Opcode.SetAnimationShadow => () => _(U8, U8, U8),
            Opcode.SetDaytime => () => _(U8),
            Opcode.SetFace => () => _(U8),
            Opcode._ChangeDialog => isEnhanced == false
                ? () => _(U8, S16, U16)
                : () => _(U8, S32, U16),
            Opcode.Unknown52 => Invalid,
            Opcode.Direction2_1 => () => _(U8, U8, U8, U8, U8, U8, U8),
            Opcode.StartModelAnimation => () => _(U8, U8),
            Opcode.StartVRAMAnimation => () => _(U8, U8),
            Opcode.WaitModelAnimation => None,
            Opcode.WaitVRAMAnimation => None,
            Opcode.LoadEventCharacter => () => _(U8, U8, U8),
            Opcode.ActivateEventCharacter => () => _(U8),
            Opcode.DeactivateEventCharacter => () => _(U8),
            Opcode.DisposeEventCharacter => () => _(U8),
            Opcode.ActivateCompressedAnimation => () => _(U16, U8),
            Opcode.DeactivateCompressedAnimation => () => _(U8),
            Opcode.DisposeMusic => () => _(U8),
            Opcode.SetAnimationPosition => () => _(U8, U8, U8, U8, U8, U8),
            Opcode.FadeMusic => () => _(U8, U8),
            Opcode.Unknown61 => Strange,
            Opcode.Unknown62 => () => _(U16, U8, U8, U8, U8), // not totally confident
            Opcode.SetMoveCameraFlags => () => _(U8),
            Opcode.WaitDirection => () => _(U8, U8),
            Opcode.WaitDirectionAll => None,
            Opcode.SetPresentClutDataAsDefault => None,
            Opcode.Unknown67 => Invalid,
            Opcode.SetAnimationHorizontalFlip => () => _(U16, U8),
            Opcode.Direction4 => () => _(U8, U8, U8, U8, U8, U8, U8, U8),
            Opcode.FadeSoundEffect => () => _(U8, S8, S8, U8, U8),
            Opcode.PlaySoundEffect => () => _(U8, S8, S8, U8, U8),
            Opcode.SetAnimationColorChangeOff => () => _(U16),
            Opcode.SetAnimationColorChangeOn => () => _(U16),
            Opcode.MoveSprite2 => MoveSprite,
            Opcode.WaitMoveSprite => () => _(U8, U8),
            Opcode.JumpToPanel => () => _(U8, U8, U8, U8),
            Opcode.RaiseAnimationPriority => () => _(U16),
            Opcode.ForceStop => None,
            Opcode.Unknown73 => () => _(U16, U16, U16, U16, U16, U16, U16),
            Opcode.Unknown74 => Strange,
            Opcode.Unknown75 => Strange,
            Opcode.StartWipe => () => _(U8, U8, U8, U8, U8, U8),
            Opcode.StopWipe => None,
            Opcode._DisplayConditions => () => _(U8, U8),
            Opcode._WalkToAnim => () => _(U16, U16), // FFHacktics wiki claims 3 bytes, but size is 4?
            Opcode.EraseUnit => () => _(U8, U8),
            Opcode.Unknown7B => () => _(U16),
            Opcode.StopAllEffects => None,
            Opcode.DisplayChapter => () => _(U8),
            Opcode.WaitEventFlag => () => _(U16, U16),
            Opcode.SetEventCharacterClut => () => _(U8, U8, U8, U8),
            Opcode.RequestStandardAnimation => () => _(U8, U8, U8),
            Opcode.SetAnimationSound => () => _(U16, OffOn),
            Opcode.Unknown82 => None,
            Opcode._ChangeStats => () => _(U8, U8, U8, S16),
            Opcode.PlayJingle => () => _(U8),
            Opcode.ChangeTreasureFindDay => () => _(U8),
            Opcode.EquipWeapon => () => _(U8, U8, U8),
            Opcode.UseGun => () => _(U16, U16),
            Opcode.RestartMapPaletteAnimation => None,
            Opcode.StopMapPaletteAnimation => None,
            Opcode.WaitEffectLoad => None,
            Opcode.PlayEffect => None,
            Opcode.SetAnimationFlipDirection => () => _(U16, U8, U16, OffOn),
            Opcode.Unknown8D => Invalid,
            Opcode.WaitDisplayChapter => None,
            Opcode.Unknown8F => () => _(U8),
            Opcode.WaitActivePanel => () => _(U8, U8, U8),
            Opcode.DisplayMapTitle => () => _(U8, U8, U8),
            Opcode._InflictStatus => () => _(U16, U8, U16),
            Opcode.Unknown93 => () => _(U16), // if (!read_eventflag(508)) write_eventflag(84, arg)
            Opcode.TeleportOut => () => _(U16),
            Opcode.Unknown95 => Strange,
            Opcode._AppendMapState => None,
            Opcode.SetAnimationBrightColor => () => _(U16),
            Opcode.TeleportIn => () => _(U16),
            Opcode._BlueRemoveUnit => () => _(U16),
            Opcode.Unknown9A => Invalid,
            Opcode.Unknown9B => Invalid,
            Opcode.Unknown9C => Invalid,
            Opcode.Unknown9D => Invalid,
            Opcode.Unknown9E => Invalid,
            Opcode.Unknown9F => Invalid,
            Opcode.LessThanEquals => Compare,
            Opcode.GreaterThanEquals => Compare,
            Opcode.Equals => Compare,
            Opcode.NotEquals => Compare,
            Opcode.LessThan => Compare,
            Opcode.GreaterThan => Compare,
            Opcode.UnknownA6 => () => _(U32),
            Opcode.UnknownA7 => () => _(U32),
            Opcode.UnknownA8 => () => _(U16, U32), // U16 unused?
            Opcode.UnknownA9 => () => _(U16, U32), // U16 unused?
            Opcode.UnknownAA => () => _(B8),
            Opcode.UnknownAB => () => _(U32),
            Opcode.UnknownAC => () => _(U32),
            Opcode.ChangePostEffectDepthLUT => () => _(U32, S32),
            Opcode.ChangePostEffectLUT => () => _(U32, S32),
            Opcode.UnknownAF => () => _(U32),
            Opcode.Add => Math2,
            Opcode.AddVar => Math2,
            Opcode.Sub => Math2,
            Opcode.SubVar => Math2,
            Opcode.Mul => Math2,
            Opcode.MulVar => Math2,
            Opcode.Div => Math2,
            Opcode.DivVar => Math2,
            Opcode.Mod => Math2,
            Opcode.ModVar => Math2,
            Opcode.And => Math2,
            Opcode.AndVar => Math2,
            Opcode.Or => Math2,
            Opcode.OrVar => Math2,
            Opcode.Zero => Math1,
            Opcode.UnknownBF => Invalid,
            Opcode.UnknownC0 => Strange,
            Opcode.UnknownC1 => None,
            Opcode.UnknownC2 => () => _(S32),
            Opcode.UnknownC3 => () => _(U16),
            Opcode.UnknownC4 => () => _(U32, U32, U32),
            Opcode.ChangeDepthOfField => () => _(U32, S32),
            Opcode.UnknownC6 => () => _(S32, S32),
            Opcode.UnknownC7 => () => _(S32, S32, S32, S32),
            Opcode.UnknownC8 => () => _(S32, S32),
            Opcode.UnknownC9 => () => _(U8),
            Opcode.UnknownCA => () => _(U8),
            Opcode.ChangePostEffectGlare => () => _(U32, S32),
            Opcode.UnknownCC => () => _(S32, S32, S32),
            Opcode.UnknownCD => () => _(S32, U32),
            Opcode.UnknownCE => () => _(B8),
            Opcode.UnknownCF => None,
            Opcode.SeekCodeForwardIfZero => () => _(U8),
            Opcode.SeekCodeForward => () => _(U8),
            Opcode.SeekCodeForwardTarget => () => _(U8),
            Opcode.SeekCodeBackward => () => _(U8),
            Opcode.Terminate => () => _(U8), // unused arg?
            Opcode.SeekCodeBackwardTarget => () => _(U8),
            Opcode.UnknownD6 => Invalid,
            Opcode.UnknownD7 => Invalid,
            Opcode.UnknownD8 => () => _(U8),
            Opcode.UnknownD9 => () => _(U8),
            Opcode.UnknownDA => Invalid,
            Opcode._EventEnd => None,
            Opcode.UnknownDC => None,
            Opcode.UnknownDD => Invalid,
            Opcode.UnknownDE => Invalid,
            Opcode.UnknownDF => Invalid,
            Opcode.UnknownE0 => Invalid,
            Opcode.UnknownE1 => Invalid,
            Opcode.UnknownE2 => Invalid,
            Opcode._EventEnd2 => None,
            Opcode.UnknownE4 => Invalid,
            Opcode._WaitForInstruction => () => _(U16),
            Opcode.UnknownE6 => Invalid,
            Opcode.DisplayCaption => () => _(U32),
            Opcode.UnknownE8 => () => _(U8),
            Opcode.UnknownE9 => () => _(U32, U32),
            Opcode.UnknownEA => () => _(U32, B8),
            Opcode.UnknownEB => isEnhanced == false
                ? () => _(U16, U16, U16)
                : () => _(S32, S32, U16),
            Opcode.UnknownEC => None,
            Opcode.UnknownED => () => _(U32, U16),
            Opcode.UnknownEE => () => _(U32),
            Opcode.UnknownEF => None,
            Opcode.UnknownF0 => None,
            Opcode._Wait => () => _(U16),
            Opcode._Pad => None,
            _ => throw new NotSupportedException(),
        };

        private const GetDelegate None = null;

        private const GetDelegate Compare = null;

        private static readonly GetDelegate Invalid = () => throw new NotSupportedException();
        private static readonly GetDelegate Unknown = () => throw new NotSupportedException();

        // instructions that should be valid but are seemingly missing handlers in the main code
        private static readonly GetDelegate Strange = () => throw new NotSupportedException();

        private static readonly GetDelegate Math1 = () => _(U16);
        private static readonly GetDelegate Math2 = () => _(U16, U16);
        private static readonly GetDelegate MoveSprite = () => _(U8, U8, S16, S16, S16, U8, U8, S16);

        public static bool IsUnknown(GetDelegate getter)
        {
            return getter == Invalid || getter == Unknown || getter == Strange;
        }

        private const OperandType B8 = OperandType.Bool8;
        private const OperandType OnOff = OperandType.Bool8OnOff;
        private const OperandType OffOn = OperandType.Bool8OffOn;
        private const OperandType S8 = OperandType.Int8;
        private const OperandType U8 = OperandType.UInt8;
        private const OperandType S16 = OperandType.Int16;
        private const OperandType U16 = OperandType.UInt16;
        private const OperandType MessageIndex = OperandType.UInt16MessageIndex;
        private const OperandType S32 = OperandType.Int32;
        private const OperandType U32 = OperandType.UInt32;

        private static IEnumerable<OperandType> _(params OperandType[] operands)
        {
            foreach (var operand in operands)
            {
                yield return operand;
            }
        }

        public static void SanityCheck()
        {
            SanityCheck(true);
            SanityCheck(false);
        }

        internal static void SanityCheck(bool isEnhanced)
        {
            foreach (Opcode opcode in Enum.GetValues(typeof(Opcode)))
            {
                if (opcode == Opcode.UnknownE6)
                {
                    // dynamic size, so skip
                    continue;
                }

                var getOperands = Get(opcode, isEnhanced);
                if (getOperands == Invalid || getOperands == Unknown || getOperands == Strange)
                {
                    continue;
                }

                var definedSize = opcode.GetSize(isEnhanced);
                if (getOperands == null)
                {
                    if (definedSize > 0)
                    {
                        throw new InvalidOperationException($"{opcode} operands size non-zero but none specified");
                    }
                    continue;
                }

                int operandSize = 0;
                foreach (var operandType in getOperands())
                {
                    operandSize += operandType.GetSize();
                }
                if (operandSize != definedSize)
                {
                    throw new InvalidOperationException($"{opcode} operand size mismatch in {(isEnhanced ? "enhanced" : "classic")}: {definedSize} vs {operandSize}");
                }
            }
        }
    }
}
