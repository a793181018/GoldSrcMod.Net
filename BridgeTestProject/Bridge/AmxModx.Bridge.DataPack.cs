using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.DataPack
{
    /// <summary>
    /// 数据包桥接接口，提供CDataPack操作功能
    /// </summary>
    public static class DataPackBridge
    {
        private const string NativeLibrary = "amxmodx_mm";

        #region DataPack操作

        /// <summary>
        /// 创建新的数据包
        /// </summary>
        /// <returns>新创建的数据包指针</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_CreateDataPack();

        /// <summary>
        /// 销毁数据包
        /// </summary>
        /// <param name="pack">要销毁的数据包指针</param>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_DestroyDataPack(IntPtr pack);

        /// <summary>
        /// 获取数据包当前位置
        /// </summary>
        /// <param name="pack">数据包指针</param>
        /// <returns>当前位置</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetDataPackPosition(IntPtr pack);

        /// <summary>
        /// 设置数据包位置
        /// </summary>
        /// <param name="pack">数据包指针</param>
        /// <param name="position">要设置的位置</param>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_SetDataPackPosition(IntPtr pack, int position);

        /// <summary>
        /// 向数据包写入整数
        /// </summary>
        /// <param name="pack">数据包指针</param>
        /// <param name="value">要写入的值</param>
        /// <returns>写入是否成功</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_WriteDataPackCell(IntPtr pack, int value);

        /// <summary>
        /// 向数据包写入浮点数
        /// </summary>
        /// <param name="pack">数据包指针</param>
        /// <param name="value">要写入的值</param>
        /// <returns>写入是否成功</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_WriteDataPackFloat(IntPtr pack, float value);

        /// <summary>
        /// 向数据包写入字符串
        /// </summary>
        /// <param name="pack">数据包指针</param>
        /// <param name="value">要写入的字符串</param>
        /// <returns>写入是否成功</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_WriteDataPackString(IntPtr pack, [MarshalAs(UnmanagedType.LPStr)] string value);

        /// <summary>
        /// 从数据包读取整数
        /// </summary>
        /// <param name="pack">数据包指针</param>
        /// <returns>读取的整数值</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ReadDataPackCell(IntPtr pack);

        /// <summary>
        /// 从数据包读取浮点数
        /// </summary>
        /// <param name="pack">数据包指针</param>
        /// <returns>读取的浮点数值</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float AmxModx_Bridge_ReadDataPackFloat(IntPtr pack);

        /// <summary>
        /// 从数据包读取字符串
        /// </summary>
        /// <param name="pack">数据包指针</param>
        /// <returns>读取的字符串</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_ReadDataPackString(IntPtr pack);

        #endregion

        #region 便捷方法

        /// <summary>
        /// 从数据包读取字符串的安全封装
        /// </summary>
        /// <param name="pack">数据包指针</param>
        /// <returns>读取的字符串</returns>
        public static string ReadDataPackStringSafe(IntPtr pack)
        {
            if (pack == IntPtr.Zero)
                return string.Empty;

            IntPtr ptr = AmxModx_Bridge_ReadDataPackString(pack);
            return ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : string.Empty;
        }

        #endregion
    }
}