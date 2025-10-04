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

namespace Gibbed.IvaliceChronicles.ScriptFormats
{
    public enum Opcode : byte
    {
        Unknown00,
        Unknown01,
        Unknown02,
        Unknown03,
        Unknown04,
        Unknown05,
        Unknown06,
        Unknown07,
        Unknown08,
        Unknown09,
        Unknown0A,
        Unknown0B,
        Unknown0C,
        Unknown0D,
        Unknown0E,
        Unknown0F,
        DisplayMessage, // 10
        UnitAnim, // 11
        Unknown12,
        ChangeMapBeta, // 13 - same handler as 4C
        Unknown14,
        Unknown15,
        Pause, // 16
        Unknown17,
        Effect, // 18
        Camera, // 19
        MapDarkness, // 1A
        MapLight, // 1B
        EventSpeed, // 1C
        CameraFusionStart, // 1D
        CameraFusionEnd, // 1E
        Focus, // 1F
        SoundEffect, // 20
        SwitchTrack, // 21
        Unknown22, // 22 - same handler as F0
        Unknown23,
        Unknown24,
        Unknown25,
        Unknown26,
        ReloadMapState, // 27
        WalkTo, // 28
        WaitWalk, // 29
        BlockStart, // 2A
        BlockEnd, // 2B
        FaceUnit2, // 2C
        RotateUnit, // 2D
        Background, // 2E
        Unknown2F,
        Unknown30,
        ColorBGBeta, // 31
        ColorUnit, // 32
        ColorField, // 33
        Unknown34,
        Unknown35,
        Unknown36,
        Unknown37,
        FocusSpeed, // 38
        Unknown39,
        Unknown3A,
        SpriteMove, // 3B
        Weather, // 3C
        RemoveUnit, // 3D
        ColorScreen, // 3E
        Unknown3F,
        Unknown40,
        EarthquakeStart, // 41
        EarthquakeEnd, // 42
        CallFunction, // 43
        Draw, // 44
        AddUnit, // 45
        Erase, // 46
        AddGhostUnit, // 47
        WaitAddUnit, // 48
        AddUnitStart, // 49
        AddUnitEnd, // 4A
        WaitAddUnitEnd, // 4B
        ChangeMap, // 4C - same handler as 13
        Reveal, // 4D
        UnitShadow, // 4E
        SetDaytime, // 4F
        PortraitCol, // 50
        ChangeDialog, // 51
        Unknown52,
        FaceUnit, // 53
        Use3DObject, // 54
        UseFieldObject, // 55
        Wait3DObject, // 56
        WaitFieldObject, // 57
        LoadEVTCHR, // 58
        SaveEVTCHR, // 59
        SaveEVTCHRClear, // 5A
        LoadEVTCHRClear, // 5B
        Unknown5C,
        Unknown5D,
        EndTrack, // 5E
        WarpUnit, // 5F
        FadeSound, // 60
        Unknown61,
        Unknown62,
        CameraSpeedCurve, // 63
        WaitRotateUnit, // 64
        WaitRotateAll, // 65
        Unknown66,
        Unknown67,
        MirrorSprite, // 68
        FaceTile, // 69
        EditBGSound, // 6A
        BGSound, // 6B
        Unknown6C,
        Unknown6D,
        SpriteMoveBeta, // 6E
        WaitSpriteMove, // 6F
        Jump, // 70
        Unknown71,
        Unknown72,
        Unknown73,
        Unknown74,
        Unknown75,
        DarkScreen, // 76
        RemoveDarkScreen, // 77
        DisplayConditions, // 78
        WalkToAnim, // 79
        DismissUnit, // 7A
        Unknown7B,
        Unknown7C,
        ShowGraphic, // 7D
        WaitValue, // 7E
        EVTCHRPalette, // 7F
        March, // 80
        Unknown81,
        Unknown82,
        ChangeStats, // 83
        PlayTune, // 84
        UnlockDate, // 85
        TempWeapon, // 86
        Arrow, // 87
        MapUnfreeze, // 88
        MapFreeze, // 89
        EffectStart, // 8A
        EffectEnd, // 8B
        UnitAnimRotate, // 8C
        Unknown8D,
        WaitGraphicPrint, // 8E
        Unknown8F,
        Unknown90,
        ShowMapTitle, // 91
        InflictStatus, // 92
        Unknown93,
        TeleportOut, // 93
        Unknown95,
        AppendMapState, // 96
        ResetPalette, // 97
        TeleportIn, // 98
        BlueRemoveUnit, // 99
        Unknown9A,
        Unknown9B,
        Unknown9C,
        Unknown9D,
        Unknown9E,
        Unknown9F,
        LessThanEquals, // A0
        GreaterThanEquals, // A1
        Equals, // A2
        NotEquals, // A3
        LessThan, // A4
        GreaterThan, // A5
        UnknownA6,
        UnknownA7,
        UnknownA8,
        UnknownA9,
        UnknownAA,
        UnknownAB,
        UnknownAC,
        UnknownAD,
        UnknownAE,
        UnknownAF,
        Add, // B0
        AddVar, // B1
        Sub, // B2
        SubVar, // B3
        Mul, // B4
        MulVar, // B5
        Div, // B6
        DivVar, // B7
        Mod, // B8
        ModVar, // B9
        And, // BA
        AndVar, // BB
        Or, // BC
        OrVar, // BD
        Zero, // BE
        UnknownBF,
        UnknownC0,
        UnknownC1,
        UnknownC2,
        UnknownC3,
        UnknownC4,
        UnknownC5,
        UnknownC6,
        UnknownC7,
        UnknownC8,
        UnknownC9,
        UnknownCA,
        UnknownCB,
        UnknownCC,
        UnknownCD,
        UnknownCE,
        UnknownCF,
        JumpForwardIfZero, // D0
        JumpForward, // D1
        ForwardTarget, // D2
        JumpBack, // D3
        UnknownD4,
        BackTarget, // D5
        UnknownD6,
        UnknownD7,
        UnknownD8,
        UnknownD9,
        UnknownDA,
        EventEnd, // DB - same handler as E3
        UnknownDC,
        UnknownDD,
        UnknownDE,
        UnknownDF,
        UnknownE0,
        UnknownE1,
        UnknownE2,
        EventEnd2, // E3 - same handler as DB
        UnknownE4,
        WaitForInstruction, // E5
        UnknownE6,
        UnknownE7,
        UnknownE8,
        UnknownE9,
        UnknownEA,
        UnknownEB,
        UnknownEC,
        UnknownED,
        UnknownEE,
        UnknownEF,
        UnknownF0, // F0 - same handler as 22
        Wait, // F1
        Pad, // F2
    }
}
