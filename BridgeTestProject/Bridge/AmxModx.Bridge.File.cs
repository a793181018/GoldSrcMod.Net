using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.File
{
    /// <summary>
    /// 文件系统桥接接口，提供文件操作功能
    /// </summary>
    public static class FileBridge
    {
        private const string NativeLibrary = "amxmodx_mm";

        #region 文件系统操作

        /// <summary>
        /// 读取文件内容到内存缓冲区
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="size">返回文件大小</param>
        /// <returns>文件内容缓冲区指针，需要手动释放</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_ReadFile([MarshalAs(UnmanagedType.LPStr)] string filePath, out int size);

        /// <summary>
        /// 将数据写入文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="data">要写入的数据</param>
        /// <param name="size">数据大小</param>
        /// <returns>写入是否成功</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_WriteFile([MarshalAs(UnmanagedType.LPStr)] string filePath, [MarshalAs(UnmanagedType.LPStr)] string data, int size);

        /// <summary>
        /// 释放由ReadFile分配的缓冲区
        /// </summary>
        /// <param name="buffer">要释放的缓冲区指针</param>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_FreeBuffer(IntPtr buffer);

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>文件是否存在</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_FileExists([MarshalAs(UnmanagedType.LPStr)] string fileName);

        /// <summary>
        /// 从文件读取字符串内容
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>读取是否成功</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_ReadFileString([MarshalAs(UnmanagedType.LPStr)] string fileName, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, int bufferSize);

        /// <summary>
        /// 将字符串写入文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">要写入的内容</param>
        /// <returns>写入是否成功</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_WriteFileString([MarshalAs(UnmanagedType.LPStr)] string fileName, [MarshalAs(UnmanagedType.LPStr)] string content);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>删除是否成功</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_DeleteFile([MarshalAs(UnmanagedType.LPStr)] string fileName);

        #endregion

        #region 便捷方法

        /// <summary>
        /// 安全读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件内容字符串，失败返回null</returns>
        public static string ReadFileSafe(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            IntPtr buffer = IntPtr.Zero;
            try
            {
                buffer = AmxModx_Bridge_ReadFile(filePath, out int size);
                if (buffer == IntPtr.Zero || size <= 0)
                    return null;

                byte[] data = new byte[size];
                Marshal.Copy(buffer, data, 0, size);
                return Encoding.UTF8.GetString(data).TrimEnd('\0');
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                    AmxModx_Bridge_FreeBuffer(buffer);
            }
        }

        /// <summary>
        /// 安全写入文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">要写入的内容</param>
        /// <returns>写入是否成功</returns>
        public static bool WriteFileSafe(string filePath, string content)
        {
            if (string.IsNullOrEmpty(filePath) || content == null)
                return false;

            byte[] data = Encoding.UTF8.GetBytes(content);
            return AmxModx_Bridge_WriteFile(filePath, content, data.Length);
        }

        #endregion
    }
}