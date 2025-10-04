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
                ? _(U8, U8, U16, U8, U8, U8, S16, S16, S16, U8)
                : _(U8, U8, U32, U8, U8, U8, S16, S16, S16, U8, U8),
            Opcode.UnitAnim => Unknown, // () => _(U8, U8, U8, U8), // FFHacktics wiki claims 4 bytes, but size is 5? 
            Opcode.Unknown12 => Unknown,
            Opcode.ChangeMapBeta => () => _(U8, U8),
            Opcode.Unknown14 => Invalid,
            Opcode.Unknown15 => Invalid,
            Opcode.Pause => None,
            Opcode.Unknown17 => Strange,
            Opcode.Effect => () => _(U16, U8, U8, U8, U8),
            Opcode.Camera => () => _(S16, S16, S16, S16, S16, S16, S16, S16),
            Opcode.MapDarkness => () => _(U8, U8, U8, U8, U8),
            Opcode.MapLight => () => _(S16, S16, S16, S16, S16, S16, S16),
            Opcode.EventSpeed => () => _(U8),
            Opcode.CameraFusionStart => None,
            Opcode.CameraFusionEnd => None, // handler not in main code?
            Opcode.Focus => () => _(U8, U8, U8, U8, U8),
            Opcode.SoundEffect => Invalid, // handler removed? FFhacktics wiki claims 2 bytes, but size is 6?
            Opcode.SwitchTrack => Unknown, // FFhacktics wiki claims 3 bytes, but size is 2?
            Opcode.Unknown22 => Unknown,
            Opcode.Unknown23 => Strange,
            Opcode.Unknown24 => Strange,
            Opcode.Unknown25 => Unknown,
            Opcode.Unknown26 => Unknown,
            Opcode.ReloadMapState => None,
            Opcode.WalkTo => () => _(U8, U8, U8, U8, U8, U8, U8, U8),
            Opcode.WaitWalk => () => _(U8, U8),
            Opcode.BlockStart => None,
            Opcode.BlockEnd => None, // handler not in main code?
            Opcode.FaceUnit2 => () => _(U8, U8, U8, U8, U8, U8, U8),
            Opcode.RotateUnit => () => _(U8, U8, U8, U8, U8, U8),
            Opcode.Background => () => _(U8, U8, U8, U8, U8, U8, U8, U8),
            Opcode.Unknown2F => Invalid,
            Opcode.Unknown30 => Invalid,
            Opcode.ColorBGBeta => () => _(U8, U8, U8, U8, U8),
            Opcode.ColorUnit => () => _(U8, U8, U8, U8, U8, U8, U8),
            Opcode.ColorField => () => _(U8, U8, U8, U8, U8),
            Opcode.Unknown34 => Invalid,
            Opcode.Unknown35 => Invalid,
            Opcode.Unknown36 => Invalid,
            Opcode.Unknown37 => () => _(U16),
            Opcode.FocusSpeed => () => _(U16),
            Opcode.Unknown39 => Unknown,
            Opcode.Unknown3A => Unknown,
            Opcode.SpriteMove => () => _(U8, U8, S16, S16, S16, U8, U8, S16),
            Opcode.Weather => () => _(U8, U8),
            Opcode.RemoveUnit => () => _(U8, U8),
            Opcode.ColorScreen => () => _(U8, U8, U8, U8, U8, U8, U8, S16),
            Opcode.Unknown3F => () => _(U8, U8, U8, U8),
            Opcode.Unknown40 => () => _(U8, U8, U8, U8, U8),
            Opcode.EarthquakeStart => None,
            Opcode.EarthquakeEnd => None,
            Opcode.CallFunction => () => _(U8),
            Opcode.Draw => () => _(U8, U8),
            Opcode.AddUnit => () => _(U8, U8, U8),
            Opcode.Erase => () => _(U8, U8),
            Opcode.AddGhostUnit => () => _(U8, U8, U8, U8, U8, U8, U8, U8),
            Opcode.WaitAddUnit => None,
            Opcode.AddUnitStart => None,
            Opcode.AddUnitEnd => None, // handler not in main code?
            Opcode.WaitAddUnitEnd => None,
            Opcode.ChangeMap => () => _(U8, U8),
            Opcode.Reveal => Unknown,
            Opcode.UnitShadow => Unknown,
            Opcode.SetDaytime => Unknown,
            Opcode.PortraitCol => Unknown,
            Opcode.ChangeDialog => isEnhanced == false
                ? () => _(U8, U16, U16)
                : () => _(U8, S32, U16),
            Opcode.Unknown52 => Invalid,
            Opcode.FaceUnit => Unknown,
            Opcode.Use3DObject => Unknown,
            Opcode.UseFieldObject => Unknown,
            Opcode.Wait3DObject => None,
            Opcode.WaitFieldObject => None,
            Opcode.LoadEVTCHR => Unknown,
            Opcode.SaveEVTCHR => Unknown,
            Opcode.SaveEVTCHRClear => Unknown,
            Opcode.LoadEVTCHRClear => Unknown,
            Opcode.Unknown5C => Unknown,
            Opcode.Unknown5D => Unknown,
            Opcode.EndTrack => Unknown,
            Opcode.WarpUnit => Unknown,
            Opcode.FadeSound => Unknown,
            Opcode.Unknown61 => Strange,
            Opcode.Unknown62 => () => _(U16, U8, U8, U8, U8), // not totally confident
            Opcode.CameraSpeedCurve => Unknown,
            Opcode.WaitRotateUnit => Unknown,
            Opcode.WaitRotateAll => None,
            Opcode.Unknown66 => None,
            Opcode.Unknown67 => Invalid,
            Opcode.MirrorSprite => () => _(U16, U8),
            Opcode.FaceTile => Unknown,
            Opcode.EditBGSound => Unknown,
            Opcode.BGSound => Unknown,
            Opcode.Unknown6C => Unknown,
            Opcode.Unknown6D => Unknown,
            Opcode.SpriteMoveBeta => Unknown,
            Opcode.WaitSpriteMove => Unknown,
            Opcode.Jump => Unknown,
            Opcode.Unknown71 => Unknown,
            Opcode.Unknown72 => None,
            Opcode.Unknown73 => Unknown,
            Opcode.Unknown74 => Strange,
            Opcode.Unknown75 => Strange,
            Opcode.DarkScreen => Unknown,
            Opcode.RemoveDarkScreen => None,
            Opcode.DisplayConditions => Unknown,
            Opcode.WalkToAnim => Unknown,
            Opcode.DismissUnit => Unknown,
            Opcode.Unknown7B => Unknown,
            Opcode.Unknown7C => None,
            Opcode.ShowGraphic => Unknown,
            Opcode.WaitValue => Unknown,
            Opcode.EVTCHRPalette => Unknown,
            Opcode.March => Unknown,
            Opcode.Unknown81 => Unknown,
            Opcode.Unknown82 => None,
            Opcode.ChangeStats => Unknown,
            Opcode.PlayTune => Unknown,
            Opcode.UnlockDate => Unknown,
            Opcode.TempWeapon => Unknown,
            Opcode.Arrow => () => _(U16, U16),
            Opcode.MapUnfreeze => None,
            Opcode.MapFreeze => None,
            Opcode.EffectStart => None,
            Opcode.EffectEnd => None,
            Opcode.UnitAnimRotate => Unknown,
            Opcode.Unknown8D => Invalid,
            Opcode.WaitGraphicPrint => None,
            Opcode.Unknown8F => Unknown,
            Opcode.Unknown90 => Unknown,
            Opcode.ShowMapTitle => Unknown,
            Opcode.InflictStatus => Unknown,
            Opcode.Unknown93 => Unknown,
            Opcode.TeleportOut => Unknown,
            Opcode.Unknown95 => Strange,
            Opcode.AppendMapState => None,
            Opcode.ResetPalette => () => _(U16),
            Opcode.TeleportIn => Unknown,
            Opcode.BlueRemoveUnit => Unknown,
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
            Opcode.UnknownAD => () => _(U32, S32),
            Opcode.UnknownAE => () => _(U32, S32),
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
            Opcode.UnknownC5 => () => _(U32, S32),
            Opcode.UnknownC6 => () => _(S32, S32),
            Opcode.UnknownC7 => () => _(S32, S32, S32, S32),
            Opcode.UnknownC8 => () => _(S32, S32),
            Opcode.UnknownC9 => () => _(U8),
            Opcode.UnknownCA => Unknown,
            Opcode.UnknownCB => () => _(U32, S32),
            Opcode.UnknownCC => () => _(S32, S32, S32),
            Opcode.UnknownCD => () => _(S32, U32),
            Opcode.UnknownCE => () => _(B8),
            Opcode.UnknownCF => None,
            Opcode.JumpForwardIfZero => () => _(U8),
            Opcode.JumpForward => () => _(U8),
            Opcode.ForwardTarget => Strange,
            Opcode.JumpBack => Unknown,
            Opcode.UnknownD4 => Unknown,
            Opcode.BackTarget => Strange,
            Opcode.UnknownD6 => Invalid,
            Opcode.UnknownD7 => Invalid,
            Opcode.UnknownD8 => Strange,
            Opcode.UnknownD9 => Strange,
            Opcode.UnknownDA => Strange,
            Opcode.EventEnd => None,
            Opcode.UnknownDC => None,
            Opcode.UnknownDD => Invalid,
            Opcode.UnknownDE => Invalid,
            Opcode.UnknownDF => Invalid,
            Opcode.UnknownE0 => Strange,
            Opcode.UnknownE1 => Invalid,
            Opcode.UnknownE2 => Invalid,
            Opcode.EventEnd2 => None,
            Opcode.UnknownE4 => Invalid,
            Opcode.WaitForInstruction => Unknown,
            Opcode.UnknownE6 => Invalid,
            Opcode.UnknownE7 => () => _(U32),
            Opcode.UnknownE8 => Unknown,
            Opcode.UnknownE9 => () => _(U32, U32),
            Opcode.UnknownEA => () => _(U32, B8),
            Opcode.UnknownEB => Unknown,
            Opcode.UnknownEC => None,
            Opcode.UnknownED => () => _(U32, U16),
            Opcode.UnknownEE => () => _(U32),
            Opcode.UnknownEF => None,
            Opcode.UnknownF0 => None,
            Opcode.Wait => Unknown,
            Opcode.Pad => Strange,
            _ => throw new NotSupportedException(),
        };

        private const GetDelegate Invalid = null;
        private const GetDelegate Unknown = null;
        private const GetDelegate Strange = null; // instructions that should be valid but are seemingly missing handlers in the main code
        private const GetDelegate None = null;

        private const GetDelegate Compare = null;

        private static readonly GetDelegate Math1 = () => _(U16);
        private static readonly GetDelegate Math2 = () => _(U16, U16);

        private const OperandType B8 = OperandType.GenericBool8;
        private const OperandType S8 = OperandType.GenericInt8;
        private const OperandType U8 = OperandType.GenericUInt8;
        private const OperandType S16 = OperandType.GenericUInt16;
        private const OperandType U16 = OperandType.GenericUInt16;
        private const OperandType S32 = OperandType.GenericInt32;
        private const OperandType U32 = OperandType.GenericUInt32;

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
                var getOperands = Get(opcode, isEnhanced);
                if (getOperands == null)
                {
                    continue;
                }

                var definedSize = opcode.GetSize(isEnhanced);
                int operandSize = 0;
                foreach (var operandType in getOperands())
                {
                    operandSize += operandType.GetSize();
                }
                if (operandSize != definedSize)
                {
                    throw new InvalidOperationException($"operand size mismatch in {(isEnhanced ? "enhanced" : "classic")}: {definedSize} vs {operandSize}");
                }
            }
        }
    }
}
