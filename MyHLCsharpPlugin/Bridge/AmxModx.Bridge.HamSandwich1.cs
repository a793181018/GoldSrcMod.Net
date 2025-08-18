using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// Ham Sandwich模块的P/Invoke接口定义
    /// </summary>
    public static class NativeMethods
    {
        private const string DllName = "hamsandwich_amxx";

        // 回调委托定义
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void HamHookCallback(int entity, int paramCount, IntPtr parameters);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void HamHookCallbackPost(int entity, int paramCount, IntPtr parameters);

        // 钩子管理接口
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterHamHook(int function, [MarshalAs(UnmanagedType.LPStr)] string entityClass,
            HamHookCallback preCallback, HamHookCallbackPost postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterHamHookFromEntity(int function, int entityId,
            HamHookCallback preCallback, HamHookCallbackPost postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterHamHookPlayer(int function,
            HamHookCallback preCallback, HamHookCallbackPost postCallback);

        // 钩子控制接口
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableHamHook(int hookId);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DisableHamHook(int hookId);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsHamHookValid(int function);

        // 数据访问接口
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetPrivateDataInt(int entity, int offset);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetPrivateDataFloat(int entity, int offset);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetPrivateDataEntity(int entity, int offset);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetPrivateDataInt(int entity, int offset, int value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetPrivateDataFloat(int entity, int offset, float value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetPrivateDataEntity(int entity, int offset, int entityValue);

        // 返回值处理接口
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetReturnStatus();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetReturnValueInt();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetReturnValueFloat();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetReturnValueVector([Out] float[] vec);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetReturnValueEntity();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetReturnValueInt(int value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetReturnValueFloat(float value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetReturnValueVector(float[] vec);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetReturnValueEntity(int entity);

        // 参数处理接口
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetParameterInt(int paramIndex, int value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetParameterFloat(int paramIndex, float value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetParameterVector(int paramIndex, float[] vec);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetParameterEntity(int paramIndex, int entity);

        // 初始化/清理接口
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitializeHamSandwichBridge();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CleanupHamSandwichBridge();

        // 特殊数据类型接口
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityVarInt(int entity, int offset);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetEntityVarFloat(int entity, int offset);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetEntityVarVector(int entity, int offset, [Out] float[] vec);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityVarInt(int entity, int offset, int value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityVarFloat(int entity, int offset, float value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityVarVector(int entity, int offset, float[] vec);
        // 特殊数据类型接口
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetItemInfoFieldInt(IntPtr itemInfo, int field);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetItemInfoFieldInt(IntPtr itemInfo, int field, int value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsValidHamFunction(int function);

        // 游戏特定钩子接口
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsPlayerOnTouchingWeaponHook(string entityClass, HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsItemGetMaxSpeedHook(string entityClass, HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsItemCanDropHook(string entityClass, HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsRestartHook(HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsRoundRespawnHook(HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterTfcEngineerUseHook(string entityClass, HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterTfcEmpExplodeHook(string entityClass, HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterTfcTakeEmpBlastHook(string entityClass, HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterTfcRadiusDamage2Hook(string entityClass, HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterDodWeaponSpecialHook(string entityClass, HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterDodRoundRespawnHook(HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterDodItemCanDropHook(string entityClass, HamHookCallback preCallback, HamHookCallback postCallback);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsGameSupported(string gameName);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetCurrentGame();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsGameHookAvailable(int hookType);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetGameHookCount();

        // TraceResult结构体访问
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTraceResultAllSolid(IntPtr traceResult, out int allSolid);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTraceResultStartSolid(IntPtr traceResult, out int startSolid);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTraceResultFraction(IntPtr traceResult, out float fraction);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTraceResultEndPos(IntPtr traceResult, [Out] float[] endPos);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTraceResultPlaneNormal(IntPtr traceResult, [Out] float[] normal);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTraceResultHit(IntPtr traceResult, out int hit);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTraceResultHitGroup(IntPtr traceResult, out int hitGroup);

        // ItemInfo结构体访问
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetItemInfoName(IntPtr itemInfo, StringBuilder name, int maxLen);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetItemInfoMaxClip(IntPtr itemInfo, out int maxClip);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetItemInfoSlot(IntPtr itemInfo, out int slot);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetItemInfoPosition(IntPtr itemInfo, out int position);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetItemInfoId(IntPtr itemInfo, out int id);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetItemInfoFlags(IntPtr itemInfo, out int flags);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetItemInfoMaxClip(IntPtr itemInfo, int maxClip);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetItemInfoSlot(IntPtr itemInfo, int slot);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetItemInfoPosition(IntPtr itemInfo, int position);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetItemInfoFlags(IntPtr itemInfo, int flags);

        // 向量操作辅助函数
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void VectorNormalize([In, Out] float[] vec);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float VectorLength(float[] vec);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void VectorAdd(float[] a, float[] b, [Out] float[] result);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void VectorSubtract(float[] a, float[] b, [Out] float[] result);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void VectorScale(float[] vec, float scale, [Out] float[] result);

        // 实体变换矩阵访问
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetEntityOrigin(int entity, [Out] float[] origin);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetEntityAngles(int entity, [Out] float[] angles);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityOrigin(int entity, float[] origin);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityAngles(int entity, float[] angles);

        // 距离和可见性检查
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetDistanceBetweenEntities(int entity1, int entity2);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsEntityVisible(int entity, int target);

        #region 直接访问接口 (Direct Bridge)

        /// <summary>
        /// 验证实体ID是否有效
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool IsValidEntity(int entityId);

        /// <summary>
        /// 验证偏移量是否有效
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool IsValidOffset(int offset);

        /// <summary>
        /// 获取实体的基址指针
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetEntityBasePointer(int entityId);

        /// <summary>
        /// 获取实体内存大小
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityMemorySize(int entityId);

        /// <summary>
        /// 将实体指针转换为实体索引
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int EntityToIndex(IntPtr edict);

        /// <summary>
        /// 将实体索引转换为实体指针
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr IndexToEntity(int index);

        /// <summary>
        /// 获取实体的私有数据指针
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetPrivateDataPtr(int entityId);

        #endregion
    }
}