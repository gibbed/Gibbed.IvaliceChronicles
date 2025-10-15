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
        AnimationRequest, // 11
        WaitAnimationEnd, // 12
        JumpMap, // 13 - same handler as 4C
        Unknown14,
        Unknown15,
        WaitTask, // 16
        Unknown17,
        ChangeEffect, // 18
        MoveCamera, // 19
        MoveAmbient, // 1A
        MoveLight, // 1B
        ChangeFrameRate, // 1C
        _CameraFusionStart, // 1D
        _CameraFusionEnd, // 1E
        _Focus, // 1F
        Unknown20, // 20
        PlaySound, // 21
        PlayMusic, // 22 - same handler as F0
        Unknown23,
        Unknown24,
        Unknown25,
        Unknown26,
        RewriteMap, // 27
        MoveToPanel, // 28
        WaitCharacterMove, // 29
        _BlockStart, // 2A
        _BlockEnd, // 2B
        Direction2_0, // 2C
        Direction, // 2D
        FadeGradation, // 2E
        Unknown2F,
        Unknown30,
        ChangeGradation, // 31
        SetCharacterColor, // 32
        ChangeMapClut, // 33
        Unknown34,
        Unknown35,
        Unknown36,
        Unknown37,
        _FocusSpeed, // 38
        WaitCharacterMoveWotL, // 39
        WaitFileRead, // 3A
        MoveSprite, // 3B - same handler as 6E?
        ChangeWeather, // 3C
        DisappearUnit, // 3D
        SetChangePaletteData, // 3E
        ChangeMapD, // 3F
        ChangeMapSTP, // 40
        StartShake, // 41
        StopShake, // 42
        _CallFunction, // 43
        _Draw, // 44
        LoadAnimation, // 45
        DeactivateAnimation, // 46
        ActivateAnimation, // 47
        WaitLoadAnimation, // 48
        ActivateAnimationStart, // 49
        ActivateAnimationEnd, // 4A
        WaitActivateAnimation, // 4B
        JumpMap2, // 4C - same handler as 13
        _Reveal, // 4D
        SetAnimationShadow, // 4E
        SetDaytime, // 4F
        SetFace, // 50
        _ChangeDialog, // 51
        Unknown52,
        Direction2_1, // 53
        StartModelAnimation, // 54
        StartVRAMAnimation, // 55
        WaitModelAnimation, // 56
        WaitVRAMAnimation, // 57
        LoadEventCharacter, // 58
        ActivateEventCharacter, // 59
        DeactivateEventCharacter, // 5A
        DisposeEventCharacter, // 5B
        ActivateCompressedAnimation, // 5C
        DeactivateCompressedAnimation, // 5D
        DisposeMusic, // 5E
        SetAnimationPosition, // 5F
        FadeMusic, // 60
        Unknown61, // 61
        Unknown62, // 62
        SetMoveCameraFlags, // 63
        WaitDirection, // 64
        WaitDirectionAll, // 65
        SetPresentClutDataAsDefault, // 66 - choose better name
        Unknown67,
        SetAnimationHorizontalFlip, // 68
        Direction4, // 69
        FadeSoundEffect, // 6A
        PlaySoundEffect, // 6B
        SetAnimationColorChangeOff, // 6C
        SetAnimationColorChangeOn, // 6D
        MoveSprite2, // 6E
        WaitMoveSprite, // 6F
        JumpToPanel, // 70
        RaiseAnimationPriority, // 71
        ForceStop, // 72 - unused?
        Unknown73,
        Unknown74,
        Unknown75,
        StartWipe, // 76
        StopWipe, // 77
        _DisplayConditions, // 78
        _WalkToAnim, // 79
        EraseUnit, // 7A
        Unknown7B, // 7B - valid in WotL
        StopAllEffects, // 7C
        DisplayChapter, // 7D
        WaitEventFlag, // 7E
        SetEventCharacterClut, // 7F
        RequestStandardAnimation, // 80
        SetAnimationSound, // 81
        Unknown82, // 82
        _ChangeStats, // 83
        PlayJingle, // 84
        ChangeTreasureFindDay, // 85
        EquipWeapon, // 86
        UseGun, // 87
        RestartMapPaletteAnimation, // 88
        StopMapPaletteAnimation, // 89
        WaitEffectLoad, // 8A
        PlayEffect, // 8B
        SetAnimationFlipDirection, // 8C
        Unknown8D, // 8D
        WaitDisplayChapter, // 8E
        Unknown8F, // 8F - allgrayf
        WaitActivePanel, // 90 - waits for active panel x/y to match args
        DisplayMapTitle, // 91
        _InflictStatus, // 92
        Unknown93, // 93
        TeleportOut, // 93
        Unknown95, // 95
        _AppendMapState, // 96
        SetAnimationBrightColor, // 97
        TeleportIn, // 98
        _BlueRemoveUnit, // 99
        Unknown9A, // 9A
        Unknown9B, // 9B
        Unknown9C, // 9C
        Unknown9D, // 9D
        Unknown9E, // 9E
        Unknown9F, // 9F
        LessThanEquals, // A0
        GreaterThanEquals, // A1
        Equals, // A2
        NotEquals, // A3
        LessThan, // A4
        GreaterThan, // A5
        UnknownA6, // A6
        UnknownA7, // A7
        UnknownA8, // A8
        UnknownA9, // A9
        UnknownAA, // AA
        UnknownAB, // AB
        UnknownAC, // AC
        ChangePostEffectDepthLUT, // AD
        ChangePostEffectLUT, // AE
        UnknownAF, // AF
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
        UnknownBF, // BF
        UnknownC0, // C0
        UnknownC1, // C1
        UnknownC2, // C2
        UnknownC3, // C3
        UnknownC4, // C4
        ChangeDepthOfField, // C5
        UnknownC6, // C6
        UnknownC7, // C7
        UnknownC8, // C8
        UnknownC9, // C9
        UnknownCA, // CA
        ChangePostEffectGlare, // CB
        UnknownCC, // CC
        UnknownCD, // CD
        UnknownCE, // CE
        UnknownCF, // CF
        SeekCodeForwardIfZero, // D0
        SeekCodeForward, // D1
        SeekCodeForwardTarget, // D2
        SeekCodeBackward, // D3
        Terminate, // D4
        SeekCodeBackwardTarget, // D5
        UnknownD6, // D6
        UnknownD7, // D7
        UnknownD8, // D8
        UnknownD9, // D9
        UnknownDA, // DA
        _EventEnd, // DB - same handler as E3
        UnknownDC, // DC - errorf = 13; unused?
        UnknownDD, // DD
        UnknownDE, // DE
        UnknownDF, // DF
        UnknownE0, // E0
        UnknownE1, // E1
        UnknownE2, // E2
        _EventEnd2, // E3 - same handler as DB
        UnknownE4, // E4
        _WaitForInstruction, // E5
        UnknownE6, // E6
        DisplayCaption, // E7
        UnknownE8, // E8
        UnknownE9, // E9
        UnknownEA, // EA
        UnknownEB, // EB
        UnknownEC, // EC
        UnknownED, // ED
        UnknownEE, // EE
        UnknownEF, // EF
        UnknownF0, // F0 - same handler as 22
        _Wait, // F1
        _Pad, // F2
    }
}
