using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// Ham Sandwich模块的P/Invoke接口定义
    /// 提供与AMXX Ham Sandwich模块的桥接功能
    /// </summary>
    public static class NativeMethods
    {
        private const string DllName = "hamsandwich_amxx";

        #region 初始化和清理

        /// <summary>
        /// 初始化Ham Sandwich桥接
        /// </summary>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_HamSandwichInit();

        /// <summary>
        /// 清理Ham Sandwich桥接资源
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_HamSandwichCleanup();

        /// <summary>
        /// 初始化Ham Sandwich模块
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitializeHamSandwichBridge();

        #endregion

        #region 钩子注册

        /// <summary>
        /// 注册Ham钩子
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="functionName">函数名称</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterHamHook(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string functionName, bool post);

        /// <summary>
        /// 注销Ham钩子
        /// </summary>
        /// <param name="hookId">钩子ID</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UnregisterHamHook(int hookId);

        /// <summary>
        /// 注册实体Ham钩子
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="functionName">函数名称</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterEntityHamHook(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string functionName, bool post);

        /// <summary>
        /// 注册游戏DLL函数钩子
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterGameDllHook([MarshalAs(UnmanagedType.LPStr)] string functionName, bool post);

        #endregion

        #region 数据访问

        /// <summary>
        /// 获取私有数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetPrivateDataInt(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string key, out int value);

        /// <summary>
        /// 获取私有数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetPrivateDataFloat(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string key, out float value);

        /// <summary>
        /// 获取私有数据字符串值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出缓冲区</param>
        /// <param name="maxLength">缓冲区最大长度</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetPrivateDataString(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string key, [Out] byte[] value, int maxLength);

        /// <summary>
        /// 设置私有数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetPrivateDataInt(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string key, int value);

        /// <summary>
        /// 设置私有数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetPrivateDataFloat(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string key, float value);

        /// <summary>
        /// 设置私有数据字符串值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetPrivateDataString(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value);

        #endregion

        #region 原始数据访问

        /// <summary>
        /// 获取原始数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <returns>整数值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetOriginalDataInt(int entityIndex, int offset);

        /// <summary>
        /// 获取原始数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <returns>浮点值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetOriginalDataFloat(int entityIndex, int offset);

        /// <summary>
        /// 设置原始数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetOriginalDataInt(int entityIndex, int offset, int value);

        /// <summary>
        /// 设置原始数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetOriginalDataFloat(int entityIndex, int offset, float value);

        #endregion

        #region 返回值处理

        /// <summary>
        /// 获取返回值的整数值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetReturnValueInt([MarshalAs(UnmanagedType.LPStr)] string functionName, out int value);

        /// <summary>
        /// 获取返回值的浮点值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetReturnValueFloat([MarshalAs(UnmanagedType.LPStr)] string functionName, out float value);

        /// <summary>
        /// 设置返回值的整数值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetReturnValueInt([MarshalAs(UnmanagedType.LPStr)] string functionName, int value);

        /// <summary>
        /// 设置返回值的浮点值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetReturnValueFloat([MarshalAs(UnmanagedType.LPStr)] string functionName, float value);

        /// <summary>
        /// 获取原始返回值的整数值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <returns>整数值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetOriginalReturnInt([MarshalAs(UnmanagedType.LPStr)] string functionName);

        /// <summary>
        /// 获取原始返回值的浮点值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <returns>浮点值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetOriginalReturnFloat([MarshalAs(UnmanagedType.LPStr)] string functionName);

        /// <summary>
        /// 重置返回值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResetReturnValue([MarshalAs(UnmanagedType.LPStr)] string functionName);

        #endregion

        #region 实体验证

        /// <summary>
        /// 验证实体是否有效
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <returns>是否有效</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsValidEntity(int entityIndex);

        /// <summary>
        /// 验证偏移量是否有效
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <returns>是否有效</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsValidOffset(int offset);

        #endregion

        #region 实体变量访问

        /// <summary>
        /// 获取实体变量的整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <returns>整数值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityVarInt(int entityIndex, int offset);

        /// <summary>
        /// 获取实体变量的浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <returns>浮点值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetEntityVarFloat(int entityIndex, int offset);

        /// <summary>
        /// 获取实体变量的向量值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">输出缓冲区</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetEntityVarVector(int entityIndex, int offset, [Out] float[] value);

        /// <summary>
        /// 获取实体私有数据的整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <returns>整数值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetPrivateDataInt(int entityIndex, int offset);

        /// <summary>
        /// 获取实体私有数据的浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <returns>浮点值</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetPrivateDataFloat(int entityIndex, int offset);

        /// <summary>
        /// 获取实体私有数据的实体引用
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <returns>实体ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetPrivateDataEntity(int entityIndex, int offset);

        /// <summary>
        /// 获取实体私有数据的向量值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">输出缓冲区</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetPrivateDataVector(int entityIndex, int offset, [Out] float[] value);

        /// <summary>
        /// 设置实体变量的整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">要设置的值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityVarInt(int entityIndex, int offset, int value);

        /// <summary>
        /// 设置实体变量的浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">要设置的值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityVarFloat(int entityIndex, int offset, float value);

        /// <summary>
        /// 设置实体变量的向量值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">要设置的向量值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntityVarVector(int entityIndex, int offset, [In] float[] value);

        /// <summary>
        /// 设置实体私有数据的整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">要设置的值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetPrivateDataInt(int entityIndex, int offset, int value);

        /// <summary>
        /// 设置实体私有数据的浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">要设置的值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetPrivateDataFloat(int entityIndex, int offset, float value);

        /// <summary>
        /// 设置实体私有数据的实体引用
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">要设置的实体ID</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetPrivateDataEntity(int entityIndex, int offset, int value);

        /// <summary>
        /// 设置实体私有数据的向量值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">要设置的向量值</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetPrivateDataVector(int entityIndex, int offset, [In] float[] value);

        #endregion

        #region 超级数据访问

        /// <summary>
        /// 获取超级数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetSuperDataInt(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string className, [MarshalAs(UnmanagedType.LPStr)] string key, out int value);

        /// <summary>
        /// 获取超级数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetSuperDataFloat(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string className, [MarshalAs(UnmanagedType.LPStr)] string key, out float value);

        /// <summary>
        /// 设置超级数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetSuperDataInt(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string className, [MarshalAs(UnmanagedType.LPStr)] string key, int value);

        /// <summary>
        /// 设置超级数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetSuperDataFloat(int entityIndex, [MarshalAs(UnmanagedType.LPStr)] string className, [MarshalAs(UnmanagedType.LPStr)] string key, float value);

        #endregion

        #region 游戏特定钩子

        /// <summary>
        /// 注册CS玩家触摸武器钩子
        /// </summary>
        /// <param name="callback">回调函数指针</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsPlayerOnTouchingWeaponHook(IntPtr callback, bool post);

        /// <summary>
        /// 注册CS玩家购买武器钩子
        /// </summary>
        /// <param name="callback">回调函数指针</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsPlayerOnBuyWeaponHook(IntPtr callback, bool post);

        /// <summary>
        /// 注册CS玩家掉落武器钩子
        /// </summary>
        /// <param name="callback">回调函数指针</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsPlayerOnDropWeaponHook(IntPtr callback, bool post);

        /// <summary>
        /// 注册CS武器开火钩子
        /// </summary>
        /// <param name="callback">回调函数指针</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterCsWeaponOnFireHook(IntPtr callback, bool post);

        #endregion

        #region 钩子管理

        /// <summary>
        /// Ham钩子回调委托
        /// </summary>
        /// <param name="entity">实体索引</param>
        /// <param name="paramCount">参数数量</param>
        /// <param name="parameters">参数指针</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void HamHookCallback(int entity, int paramCount, IntPtr parameters);

        /// <summary>
        /// 注册Ham钩子
        /// </summary>
        /// <param name="function">函数类型</param>
        /// <param name="entityClass">实体类名</param>
        /// <param name="preCallback">前置回调</param>
        /// <param name="postCallback">后置回调</param>
        /// <returns>钩子ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterHamHook(int function, [MarshalAs(UnmanagedType.LPStr)] string entityClass, HamHookCallback? preCallback, HamHookCallback? postCallback);

        /// <summary>
        /// 注册实体Ham钩子
        /// </summary>
        /// <param name="function">函数类型</param>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="preCallback">前置回调</param>
        /// <param name="postCallback">后置回调</param>
        /// <returns>钩子ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterHamHookFromEntity(int function, int entityIndex, HamHookCallback? preCallback, HamHookCallback? postCallback);

        /// <summary>
        /// 注册玩家Ham钩子
        /// </summary>
        /// <param name="function">函数类型</param>
        /// <param name="preCallback">前置回调</param>
        /// <param name="postCallback">后置回调</param>
        /// <returns>钩子ID</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RegisterHamHookPlayer(int function, HamHookCallback? preCallback, HamHookCallback? postCallback);

        /// <summary>
        /// 启用Ham钩子
        /// </summary>
        /// <param name="hookId">钩子ID</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool EnableHamHook(int hookId);

        /// <summary>
        /// 禁用Ham钩子
        /// </summary>
        /// <param name="hookId">钩子ID</param>
        /// <returns>成功返回true</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool DisableHamHook(int hookId);

        /// <summary>
        /// 检查Ham钩子是否有效
        /// </summary>
        /// <param name="function">函数类型</param>
        /// <returns>有效返回1，无效返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsHamHookValid(int function);

        #endregion
    }
}