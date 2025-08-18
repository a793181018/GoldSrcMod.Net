using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.Core
{
    /// <summary>
    /// AMX Mod X核心桥接接口
    /// 提供amxcore.cpp中核心功能的C#访问
    /// </summary>
    public static class CoreBridge
    {
        /// <summary>
        /// 获取当前函数的参数数量
        /// </summary>
        /// <returns>参数数量</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetNumArgs();

        /// <summary>
        /// 获取指定索引的参数值
        /// </summary>
        /// <param name="index">参数索引（从0开始）</param>
        /// <param name="offset">数组偏移量（对于数组参数）</param>
        /// <returns>参数值</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetArg(int index, int offset);

        /// <summary>
        /// 设置指定索引的参数值
        /// </summary>
        /// <param name="index">参数索引（从0开始）</param>
        /// <param name="offset">数组偏移量（对于数组参数）</param>
        /// <param name="value">要设置的值</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_SetArg(int index, int offset, int value);

        /// <summary>
        /// 获取可用堆空间大小
        /// </summary>
        /// <returns>堆空间字节数</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetHeapSpace();

        /// <summary>
        /// 根据函数名获取函数索引
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <returns>函数索引，-1表示未找到</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetFunctionIndex([MarshalAs(UnmanagedType.LPStr)] string functionName);

        /// <summary>
        /// 交换32位整数的字节顺序
        /// </summary>
        /// <param name="value">要交换的值</param>
        /// <returns>交换后的值</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_SwapChars(int value);

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="maxLen">缓冲区最大长度</param>
        /// <returns>属性值</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetProperty(int id, [MarshalAs(UnmanagedType.LPStr)] string name, int value, [Out] StringBuilder buffer, int maxLen);

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <param name="newName">新属性名称（可选）</param>
        /// <returns>之前的属性值</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_SetProperty(int id, [MarshalAs(UnmanagedType.LPStr)] string name, int value, [MarshalAs(UnmanagedType.LPStr)] string newName);

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns>被删除的属性值</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_DeleteProperty(int id, [MarshalAs(UnmanagedType.LPStr)] string name, int value);

        /// <summary>
        /// 检查属性是否存在
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns>存在返回true，不存在返回false</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_PropertyExists(int id, [MarshalAs(UnmanagedType.LPStr)] string name, int value);

        /// <summary>
        /// 安全的获取属性值方法
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns>属性值和名称</returns>
        public static (int value, string name) GetPropertySafe(int id, string name, int value)
        {
            StringBuilder buffer = new StringBuilder(256);
            int result = AmxModx_Bridge_GetProperty(id, name, value, buffer, buffer.Capacity);
            return (result, buffer.ToString());
        }

        /// <summary>
        /// 安全的设置属性值方法
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns>之前的属性值</returns>
        public static int SetPropertySafe(int id, string name, int value)
        {
            return AmxModx_Bridge_SetProperty(id, name, value, null);
        }

        /// <summary>
        /// 安全的删除属性方法
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns>被删除的属性值</returns>
        public static int DeletePropertySafe(int id, string name, int value)
        {
            return AmxModx_Bridge_DeleteProperty(id, name, value);
        }
    }
}