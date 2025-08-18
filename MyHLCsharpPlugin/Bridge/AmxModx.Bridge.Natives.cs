// vim: set ts=4 sw=4 tw=99 noet:
using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Natives
{
    /// <summary>
    /// 原生函数样式枚举
    /// </summary>
    public enum NativeStyle
    {
        /// <summary>
        /// 标准样式
        /// </summary>
        Default = 0,
        /// <summary>
        /// 引用参数
        /// </summary>
        ByRef = 1,
        /// <summary>
        /// 数组参数
        /// </summary>
        Array = 2,
        /// <summary>
        /// 可变参数
        /// </summary>
        VarArgs = 3
    }

    /// <summary>
    /// 参数类型枚举
    /// </summary>
    public enum ParamType
    {
        /// <summary>
        /// 单元格类型
        /// </summary>
        Cell = 1,
        /// <summary>
        /// 数组类型
        /// </summary>
        Array = 2,
        /// <summary>
        /// 引用类型
        /// </summary>
        ByRef = 3,
        /// <summary>
        /// 可变参数类型
        /// </summary>
        VarArg = 4
    }

    /// <summary>
    /// 原生函数信息结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeInfo
    {
        /// <summary>
        /// 函数名称
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Name;

        /// <summary>
        /// 函数指针
        /// </summary>
        public IntPtr Function;
    }

    /// <summary>
    /// 原生函数委托
    /// </summary>
    /// <param name="amx">AMX实例指针</param>
    /// <param name="params">参数数组</param>
    /// <returns>返回值</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int NativeDelegate(IntPtr amx, IntPtr params);

    /// <summary>
    /// 原生函数桥接类
    /// </summary>
    public static class NativeBridge
    {
        /// <summary>
        /// 注册原生函数
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="name">函数名称</param>
        /// <param name="func">函数指针</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_RegisterNative(IntPtr amx, [MarshalAs(UnmanagedType.LPStr)] string name, NativeDelegate func);

        /// <summary>
        /// 注销原生函数
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="name">函数名称</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_UnregisterNative(IntPtr amx, [MarshalAs(UnmanagedType.LPStr)] string name);

        /// <summary>
        /// 注册库
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="libname">库名称</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_RegisterLibrary(IntPtr amx, [MarshalAs(UnmanagedType.LPStr)] string libname);

        /// <summary>
        /// 获取原生函数数量
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <returns>函数数量</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetNativeCount(IntPtr amx);

        /// <summary>
        /// 获取原生函数名称
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="index">函数索引</param>
        /// <returns>函数名称</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_GetNativeName(IntPtr amx, int index);

        /// <summary>
        /// 检查原生函数是否存在
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="name">函数名称</param>
        /// <returns>存在返回true，不存在返回false</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_NativeExists(IntPtr amx, [MarshalAs(UnmanagedType.LPStr)] string name);

        /// <summary>
        /// 清除库
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_ClearLibraries(IntPtr amx);

        /// <summary>
        /// 获取字符串参数
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="param">参数索引</param>
        /// <param name="length">字符串长度输出</param>
        /// <returns>字符串内容</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_GetString(IntPtr amx, int param, out int length);

        /// <summary>
        /// 设置字符串参数
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="param">参数索引</param>
        /// <param name="str">字符串内容</param>
        /// <param name="maxlen">最大长度</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_SetString(IntPtr amx, int param, [MarshalAs(UnmanagedType.LPStr)] string str, int maxlen);

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="index">参数索引</param>
        /// <returns>参数值</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetParam(IntPtr amx, int index);

        /// <summary>
        /// 获取参数地址
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="index">参数索引</param>
        /// <returns>参数地址</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_GetParamAddress(IntPtr amx, int index);

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="index">参数索引</param>
        /// <param name="value">参数值</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_SetParam(IntPtr amx, int index, int value);

        /// <summary>
        /// 获取浮点数参数
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="index">参数索引</param>
        /// <returns>浮点数值</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern float AmxModx_Bridge_GetFloat(IntPtr amx, int index);

        /// <summary>
        /// 设置浮点数参数
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="index">参数索引</param>
        /// <param name="value">浮点数值</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_SetFloat(IntPtr amx, int index, float value);

        /// <summary>
        /// 获取数组参数
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="param">参数索引</param>
        /// <param name="dest">目标数组</param>
        /// <param name="size">数组大小</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetArray(IntPtr amx, int param, [Out] int[] dest, int size);

        /// <summary>
        /// 设置数组参数
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="param">参数索引</param>
        /// <param name="source">源数组</param>
        /// <param name="size">数组大小</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_SetArray(IntPtr amx, int param, [In] int[] source, int size);

        /// <summary>
        /// 记录错误
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="err">错误代码</param>
        /// <param name="message">错误消息</param>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_LogError(IntPtr amx, int err, [MarshalAs(UnmanagedType.LPStr)] string message);

        /// <summary>
        /// 安全获取原生函数名称
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="index">函数索引</param>
        /// <returns>函数名称</returns>
        public static string GetNativeNameSafe(IntPtr amx, int index)
        {
            IntPtr namePtr = AmxModx_Bridge_GetNativeName(amx, index);
            return namePtr != IntPtr.Zero ? Marshal.PtrToStringAnsi(namePtr) : string.Empty;
        }

        /// <summary>
        /// 安全获取字符串
        /// </summary>
        /// <param name="amx">AMX实例指针</param>
        /// <param name="param">参数索引</param>
        /// <returns>字符串内容</returns>
        public static string GetStringSafe(IntPtr amx, int param)
        {
            int length;
            IntPtr strPtr = AmxModx_Bridge_GetString(amx, param, out length);
            return strPtr != IntPtr.Zero ? Marshal.PtrToStringAnsi(strPtr, length) : string.Empty;
        }
    }
}