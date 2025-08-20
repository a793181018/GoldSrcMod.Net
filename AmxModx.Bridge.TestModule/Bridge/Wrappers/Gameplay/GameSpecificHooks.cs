using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// 游戏特定钩子的委托定义
    /// </summary>
    public static class GameSpecificHooks
    {
        // Counter-Strike特定钩子
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CsPlayerOnTouchingWeaponCallback(int player, int weapon);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CsItemGetMaxSpeedCallback(int item, ref float maxSpeed);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int CsItemCanDropCallback(int item);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CsRestartCallback();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CsRoundRespawnCallback();

        // Team Fortress Classic特定钩子
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int TfcEngineerUseCallback(int engineer, int building);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TfcEmpExplodeCallback(int entity, int inflictor, float damage, float distance);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TfcTakeEmpBlastCallback(int entity, int inflictor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TfcRadiusDamage2Callback(int entity, int inflictor, float damage, float radius);

        // Day of Defeat特定钩子
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DodWeaponSpecialCallback(int weapon, int type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DodRoundRespawnCallback();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DodItemCanDropCallback(int item);

        // The Specialists特定钩子
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int TsBreakableRespawnCallback(int entity, int type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int TsShouldCollideCallback(int entity1, int entity2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int TsCanUsedThroughWallsCallback(int entity);

        // Natural Selection特定钩子
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NsUpdateOnRemoveCallback(int entity);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int NsGetPointValueCallback(int entity);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NsAwardKillCallback(int killer, int victim);

        // Earth's Special Forces特定钩子
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int EsfWeaponHolsterWhenMeleedCallback(int weapon);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int EsfTakeDamage2Callback(int entity, int inflictor, float damage, float armorDamage, int damageType);
    }

    /// <summary>
    /// 游戏特定钩子的P/Invoke接口
    /// </summary>
    public static class GameSpecificNativeMethods
    {
        private const string DllName = "hamsandwich_amxx";

        #region Counter-Strike Hooks

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsPlayerOnTouchingWeaponHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.CsPlayerOnTouchingWeaponCallback preCallback,
            GameSpecificHooks.CsPlayerOnTouchingWeaponCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsItemGetMaxSpeedHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.CsItemGetMaxSpeedCallback preCallback,
            GameSpecificHooks.CsItemGetMaxSpeedCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsItemCanDropHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.CsItemCanDropCallback preCallback,
            GameSpecificHooks.CsItemCanDropCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsRestartHook(
            GameSpecificHooks.CsRestartCallback preCallback,
            GameSpecificHooks.CsRestartCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsRoundRespawnHook(
            GameSpecificHooks.CsRoundRespawnCallback preCallback,
            GameSpecificHooks.CsRoundRespawnCallback postCallback);

        #endregion

        #region Team Fortress Classic Hooks

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterTfcEngineerUseHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.TfcEngineerUseCallback preCallback,
            GameSpecificHooks.TfcEngineerUseCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterTfcEmpExplodeHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.TfcEmpExplodeCallback preCallback,
            GameSpecificHooks.TfcEmpExplodeCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterTfcTakeEmpBlastHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.TfcTakeEmpBlastCallback preCallback,
            GameSpecificHooks.TfcTakeEmpBlastCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterTfcRadiusDamage2Hook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.TfcRadiusDamage2Callback preCallback,
            GameSpecificHooks.TfcRadiusDamage2Callback postCallback);

        #endregion

        #region Day of Defeat Hooks

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterDodWeaponSpecialHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.DodWeaponSpecialCallback preCallback,
            GameSpecificHooks.DodWeaponSpecialCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterDodRoundRespawnHook(
            GameSpecificHooks.DodRoundRespawnCallback preCallback,
            GameSpecificHooks.DodRoundRespawnCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterDodItemCanDropHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.DodItemCanDropCallback preCallback,
            GameSpecificHooks.DodItemCanDropCallback postCallback);

        #endregion

        #region The Specialists Hooks

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterTsBreakableRespawnHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.TsBreakableRespawnCallback preCallback,
            GameSpecificHooks.TsBreakableRespawnCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterTsShouldCollideHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.TsShouldCollideCallback preCallback,
            GameSpecificHooks.TsShouldCollideCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterTsCanUsedThroughWallsHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.TsCanUsedThroughWallsCallback preCallback,
            GameSpecificHooks.TsCanUsedThroughWallsCallback postCallback);

        #endregion

        #region Natural Selection Hooks

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterNsUpdateOnRemoveHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.NsUpdateOnRemoveCallback preCallback,
            GameSpecificHooks.NsUpdateOnRemoveCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterNsGetPointValueHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.NsGetPointValueCallback preCallback,
            GameSpecificHooks.NsGetPointValueCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterNsAwardKillHook(
            GameSpecificHooks.NsAwardKillCallback preCallback,
            GameSpecificHooks.NsAwardKillCallback postCallback);

        #endregion

        #region Earth's Special Forces Hooks

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterEsfWeaponHolsterWhenMeleedHook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.EsfWeaponHolsterWhenMeleedCallback preCallback,
            GameSpecificHooks.EsfWeaponHolsterWhenMeleedCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterEsfTakeDamage2Hook(
            [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            GameSpecificHooks.EsfTakeDamage2Callback preCallback,
            GameSpecificHooks.EsfTakeDamage2Callback postCallback);

        #endregion
    }
}