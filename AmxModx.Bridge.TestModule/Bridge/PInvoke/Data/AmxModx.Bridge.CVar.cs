using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.CVar
{
    /// <summary>
    /// 控制台变量桥接接口，提供CVar操作功能
    /// </summary>
    public static class CVarBridge
    {
        private const string NativeLibrary = "amxmodx_mm";

        #region CVar操作

        /// <summary>
        /// 创建控制台变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="value">初始值</param>
        /// <param name="flags">变量标志</param>
        /// <returns>创建的变量指针</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_CreateCVar([MarshalAs(UnmanagedType.LPStr)] string name, [MarshalAs(UnmanagedType.LPStr)] string value, int flags);

        /// <summary>
        /// 查找控制台变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>找到的变量指针</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_FindCVar([MarshalAs(UnmanagedType.LPStr)] string name);

        /// <summary>
        /// 获取控制台变量字符串值
        /// </summary>
        /// <param name="cvar">变量指针</param>
        /// <returns>字符串值</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_GetCVarString(IntPtr cvar);

        /// <summary>
        /// 获取控制台变量浮点数值
        /// </summary>
        /// <param name="cvar">变量指针</param>
        /// <returns>浮点数值</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float AmxModx_Bridge_GetCVarFloat(IntPtr cvar);

        /// <summary>
        /// 获取控制台变量整数值
        /// </summary>
        /// <param name="cvar">变量指针</param>
        /// <returns>整数值</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetCVarInt(IntPtr cvar);

        /// <summary>
        /// 设置控制台变量字符串值
        /// </summary>
        /// <param name="cvar">变量指针</param>
        /// <param name="value">要设置的字符串值</param>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetCVarString(IntPtr cvar, [MarshalAs(UnmanagedType.LPStr)] string value);

        /// <summary>
        /// 设置控制台变量浮点数值
        /// </summary>
        /// <param name="cvar">变量指针</param>
        /// <param name="value">要设置的浮点数值</param>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetCVarFloat(IntPtr cvar, float value);

        /// <summary>
        /// 设置控制台变量整数值
        /// </summary>
        /// <param name="cvar">变量指针</param>
        /// <param name="value">要设置的整数值</param>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetCVarInt(IntPtr cvar, int value);

        #endregion

        #region 便捷方法

        /// <summary>
        /// 获取CVar字符串值的安全封装
        /// </summary>
        /// <param name="cvar">变量指针</param>
        /// <returns>字符串值</returns>
        public static string GetCVarStringSafe(IntPtr cvar)
        {
            if (cvar == IntPtr.Zero)
                return string.Empty;

            IntPtr ptr = AmxModx_Bridge_GetCVarString(cvar);
            return ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : string.Empty;
        }

        /// <summary>
        /// 通过变量名获取字符串值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>字符串值</returns>
        public static string GetCVarStringByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            IntPtr cvar = AmxModx_Bridge_FindCVar(name);
            return GetCVarStringSafe(cvar);
        }

        /// <summary>
        /// 通过变量名获取浮点数值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>浮点数值</returns>
        public static float GetCVarFloatByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return 0.0f;

            IntPtr cvar = AmxModx_Bridge_FindCVar(name);
            return cvar != IntPtr.Zero ? AmxModx_Bridge_GetCVarFloat(cvar) : 0.0f;
        }

        /// <summary>
        /// 通过变量名获取整数值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>整数值</returns>
        public static int GetCVarIntByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return 0;

            IntPtr cvar = AmxModx_Bridge_FindCVar(name);
            return cvar != IntPtr.Zero ? AmxModx_Bridge_GetCVarInt(cvar) : 0;
        }

        /// <summary>
        /// 通过变量名设置字符串值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="value">要设置的值</param>
        public static void SetCVarStringByName(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
                return;

            IntPtr cvar = AmxModx_Bridge_FindCVar(name);
            if (cvar != IntPtr.Zero)
                AmxModx_Bridge_SetCVarString(cvar, value);
        }

        /// <summary>
        /// 通过变量名设置浮点数值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="value">要设置的值</param>
        public static void SetCVarFloatByName(string name, float value)
        {
            if (string.IsNullOrEmpty(name))
                return;

            IntPtr cvar = AmxModx_Bridge_FindCVar(name);
            if (cvar != IntPtr.Zero)
                AmxModx_Bridge_SetCVarFloat(cvar, value);
        }

        /// <summary>
        /// 通过变量名设置整数值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="value">要设置的值</param>
        public static void SetCVarIntByName(string name, int value)
        {
            if (string.IsNullOrEmpty(name))
                return;

            IntPtr cvar = AmxModx_Bridge_FindCVar(name);
            if (cvar != IntPtr.Zero)
                AmxModx_Bridge_SetCVarInt(cvar, value);
        }

        #endregion
    }
}