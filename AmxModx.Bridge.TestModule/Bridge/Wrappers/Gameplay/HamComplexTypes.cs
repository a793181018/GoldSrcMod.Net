using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich
{






    /// <summary>
    /// 游戏特定钩子类型枚举
    /// </summary>
    public enum GameSpecificHookType
    {
        // Counter-Strike
        CsPlayerOnTouchingWeapon = 1000,
        CsItemGetMaxSpeed,
        CsItemCanDrop,
        CsRestart,
        CsRoundRespawn,

        // Team Fortress Classic
        TfcEngineerUse,
        TfcEmpExplode,
        TfcTakeEmpBlast,
        TfcRadiusDamage2,

        // Day of Defeat
        DodWeaponSpecial,
        DodRoundRespawn,
        DodItemCanDrop,

        // The Specialists
        TsBreakableRespawn,
        TsShouldCollide,
        TsCanUsedThroughWalls,

        // Natural Selection
        NsUpdateOnRemove,
        NsGetPointValue,
        NsAwardKill,

        // Earth's Special Forces
        EsfWeaponHolsterWhenMeleed,
        EsfTakeDamage2
    }
}