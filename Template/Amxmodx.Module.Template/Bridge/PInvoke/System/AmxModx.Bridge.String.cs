using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.System
{
    /// <summary>
    /// 字符串编码枚举
    /// </summary>
    public enum StringEncoding
    {
        /// <summary>ASCII编码</summary>
        Ascii = 0,
        /// <summary>UTF-8编码</summary>
        Utf8 = 1,
        /// <summary>UTF-16编码</summary>
        Utf16 = 2,
        /// <summary>本地编码</summary>
        Local = 3
    }

    /// <summary>
    /// 字符串系统桥接接口
    /// 提供AMX Mod X字符串处理的C#访问
    /// </summary>
    public static class StringBridge
    {
        private const string DllName = "amxmodx_mm";

        /// <summary>
        /// 创建字符串
        /// </summary>
        /// <param name="text">字符串内容</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>字符串句柄</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_CreateString(
            [MarshalAs(UnmanagedType.LPStr)] string text,
            StringEncoding encoding);

        /// <summary>
        /// 销毁字符串
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <returns>是否成功销毁</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_DestroyString(IntPtr handle);

        /// <summary>
        /// 获取字符串长度
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <returns>字符串长度</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetStringLength(IntPtr handle);

        /// <summary>
        /// 获取字符串内容
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <param name="buffer">内容缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>实际内容长度</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetStringContent(
            IntPtr handle,
            [Out] StringBuilder buffer,
            int bufferSize,
            StringEncoding encoding);

        /// <summary>
        /// 设置字符串内容
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <param name="text">新内容</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>是否成功设置</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetStringContent(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPStr)] string text,
            StringEncoding encoding);

        /// <summary>
        /// 字符串比较
        /// </summary>
        /// <param name="handle1">第一个字符串句柄</param>
        /// <param name="handle2">第二个字符串句柄</param>
        /// <param name="caseSensitive">是否区分大小写</param>
        /// <returns>比较结果（0表示相等）</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_CompareStrings(
            IntPtr handle1,
            IntPtr handle2,
            [MarshalAs(UnmanagedType.Bool)] bool caseSensitive);

        /// <summary>
        /// 字符串查找
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <param name="subString">子字符串</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="caseSensitive">是否区分大小写</param>
        /// <returns>子字符串位置（-1表示未找到）</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_FindString(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPStr)] string subString,
            int startIndex,
            [MarshalAs(UnmanagedType.Bool)] bool caseSensitive);

        /// <summary>
        /// 字符串替换
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <param name="oldValue">要替换的内容</param>
        /// <param name="newValue">替换为的内容</param>
        /// <param name="caseSensitive">是否区分大小写</param>
        /// <returns>替换次数</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ReplaceString(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPStr)] string oldValue,
            [MarshalAs(UnmanagedType.LPStr)] string newValue,
            [MarshalAs(UnmanagedType.Bool)] bool caseSensitive);

        /// <summary>
        /// 字符串分割
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <param name="delimiter">分隔符</param>
        /// <param name="parts">分割结果数组</param>
        /// <param name="maxParts">最大分割数量</param>
        /// <returns>实际分割数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_SplitString(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPStr)] string delimiter,
            [Out] IntPtr[] parts,
            int maxParts);

        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="format">格式化字符串</param>
        /// <param name="args">参数数组</param>
        /// <param name="argCount">参数数量</param>
        /// <returns>格式化后的字符串句柄</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_FormatString(
            [MarshalAs(UnmanagedType.LPStr)] string format,
            [MarshalAs(UnmanagedType.LPArray)] object[] args,
            int argCount);

        /// <summary>
        /// 字符串转大写
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <returns>是否成功转换</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_ToUpperCase(IntPtr handle);

        /// <summary>
        /// 字符串转小写
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <returns>是否成功转换</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_ToLowerCase(IntPtr handle);

        /// <summary>
        /// 去除字符串空白字符
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <returns>是否成功去除</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_TrimString(IntPtr handle);

        /// <summary>
        /// 字符串子串提取
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="length">子串长度（-1表示到末尾）</param>
        /// <returns>子字符串句柄</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_Substring(
            IntPtr handle,
            int startIndex,
            int length);

        /// <summary>
        /// 字符串复制
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <returns>复制的字符串句柄</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_CopyString(IntPtr handle);

        /// <summary>
        /// 字符串拼接
        /// </summary>
        /// <param name="handles">字符串句柄数组</param>
        /// <param name="count">字符串数量</param>
        /// <returns>拼接后的字符串句柄</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_ConcatenateStrings(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] handles,
            int count);

        /// <summary>
        /// 安全的创建字符串
        /// </summary>
        /// <param name="text">字符串内容</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>字符串句柄</returns>
        public static IntPtr CreateStringSafe(string text, StringEncoding encoding = StringEncoding.Utf8)
        {
            if (string.IsNullOrEmpty(text))
                return IntPtr.Zero;

            return AmxModx_Bridge_CreateString(text, encoding);
        }

        /// <summary>
        /// 安全的销毁字符串
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <returns>是否成功销毁</returns>
        public static bool DestroyStringSafe(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                return false;

            return AmxModx_Bridge_DestroyString(handle);
        }

        /// <summary>
        /// 安全的获取字符串内容
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>字符串内容</returns>
        public static string GetStringContentSafe(IntPtr handle, StringEncoding encoding = StringEncoding.Utf8)
        {
            if (handle == IntPtr.Zero)
                return string.Empty;

            StringBuilder buffer = new StringBuilder(1024);
            int length = AmxModx_Bridge_GetStringContent(handle, buffer, buffer.Capacity, encoding);
            
            if (length > 0)
            {
                return buffer.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 安全的字符串格式化
        /// </summary>
        /// <param name="format">格式化字符串</param>
        /// <param name="args">参数数组</param>
        /// <returns>格式化后的字符串句柄</returns>
        public static IntPtr FormatStringSafe(string format, params object[] args)
        {
            if (string.IsNullOrEmpty(format))
                return IntPtr.Zero;

            return AmxModx_Bridge_FormatString(format, args, args?.Length ?? 0);
        }

        /// <summary>
        /// 安全的字符串复制
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <returns>复制的字符串句柄</returns>
        public static IntPtr CopyStringSafe(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                return IntPtr.Zero;

            return AmxModx_Bridge_CopyString(handle);
        }

        /// <summary>
        /// 安全的字符串拼接
        /// </summary>
        /// <param name="handles">字符串句柄数组</param>
        /// <returns>拼接后的字符串句柄</returns>
        public static IntPtr ConcatenateStringsSafe(params IntPtr[] handles)
        {
            if (handles == null || handles.Length == 0)
                return IntPtr.Zero;

            return AmxModx_Bridge_ConcatenateStrings(handles, handles.Length);
        }

        /// <summary>
        /// 安全的子串提取
        /// </summary>
        /// <param name="handle">字符串句柄</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="length">子串长度</param>
        /// <returns>子字符串句柄</returns>
        public static IntPtr SubstringSafe(IntPtr handle, int startIndex, int length = -1)
        {
            if (handle == IntPtr.Zero || startIndex < 0)
                return IntPtr.Zero;

            return AmxModx_Bridge_Substring(handle, startIndex, length);
        }
    }
}