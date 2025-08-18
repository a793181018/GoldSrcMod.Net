using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.GameConfigs
{
    /// <summary>
    /// 游戏配置系统桥接接口
    /// </summary>
    public static class GameConfigBridge
    {
        private const string DllName = "amxmodx_mm";

        /// <summary>
        /// 加载游戏配置文件
        /// </summary>
        /// <param name="fileName">配置文件名</param>
        /// <param name="errorBuffer">错误信息缓冲区</param>
        /// <param name="errorBufferSize">缓冲区大小</param>
        /// <returns>游戏配置句柄，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_LoadGameConfigFile(
            [MarshalAs(UnmanagedType.LPStr)] string fileName,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder errorBuffer,
            int errorBufferSize);

        /// <summary>
        /// 获取游戏配置偏移量
        /// </summary>
        /// <param name="handle">游戏配置句柄</param>
        /// <param name="key">配置键名</param>
        /// <returns>偏移量值，失败返回-1</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GameConfGetOffset(
            int handle,
            [MarshalAs(UnmanagedType.LPStr)] string key);

        /// <summary>
        /// 获取类配置偏移量
        /// </summary>
        /// <param name="handle">游戏配置句柄</param>
        /// <param name="className">类名</param>
        /// <param name="key">配置键名</param>
        /// <returns>偏移量值，失败返回-1</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GameConfGetClassOffset(
            int handle,
            [MarshalAs(UnmanagedType.LPStr)] string className,
            [MarshalAs(UnmanagedType.LPStr)] string key);

        /// <summary>
        /// 获取游戏配置键值
        /// </summary>
        /// <param name="handle">游戏配置句柄</param>
        /// <param name="key">配置键名</param>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>是否成功获取</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GameConfGetKeyValue(
            int handle,
            [MarshalAs(UnmanagedType.LPStr)] string key,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer,
            int bufferSize);

        /// <summary>
        /// 获取游戏配置地址
        /// </summary>
        /// <param name="handle">游戏配置句柄</param>
        /// <param name="name">地址名</param>
        /// <returns>地址值，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr AmxModx_Bridge_GameConfGetAddress(
            int handle,
            [MarshalAs(UnmanagedType.LPStr)] string name);

        /// <summary>
        /// 关闭游戏配置文件
        /// </summary>
        /// <param name="handle">游戏配置句柄</param>
        /// <returns>是否成功关闭</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_CloseGameConfigFile(ref int handle);

        /// <summary>
        /// 游戏配置管理器包装类
        /// </summary>
        public class GameConfigManager : IDisposable
        {
            private int _handle = -1;
            private bool _disposed = false;

            /// <summary>
            /// 加载游戏配置文件
            /// </summary>
            /// <param name="fileName">配置文件名</param>
            /// <returns>是否加载成功</returns>
            public bool LoadConfig(string fileName)
            {
                if (_handle != -1)
                    CloseConfig();

                StringBuilder errorBuffer = new StringBuilder(256);
                _handle = AmxModx_Bridge_LoadGameConfigFile(fileName, errorBuffer, errorBuffer.Capacity);

                if (_handle == 0)
                {
                    throw new InvalidOperationException($"Failed to load game config: {errorBuffer}");
                }

                return true;
            }

            /// <summary>
            /// 获取配置偏移量
            /// </summary>
            public int GetOffset(string key)
            {
                if (_handle == -1)
                    throw new InvalidOperationException("No config loaded");

                return AmxModx_Bridge_GameConfGetOffset(_handle, key);
            }

            /// <summary>
            /// 获取类配置偏移量
            /// </summary>
            public int GetClassOffset(string className, string key)
            {
                if (_handle == -1)
                    throw new InvalidOperationException("No config loaded");

                return AmxModx_Bridge_GameConfGetClassOffset(_handle, className, key);
            }

            /// <summary>
            /// 获取配置键值
            /// </summary>
            public string GetKeyValue(string key)
            {
                if (_handle == -1)
                    throw new InvalidOperationException("No config loaded");

                StringBuilder buffer = new StringBuilder(256);
                if (AmxModx_Bridge_GameConfGetKeyValue(_handle, key, buffer, buffer.Capacity) == 1)
                {
                    return buffer.ToString();
                }
                return null;
            }

            /// <summary>
            /// 获取配置地址
            /// </summary>
            public IntPtr GetAddress(string name)
            {
                if (_handle == -1)
                    throw new InvalidOperationException("No config loaded");

                UIntPtr address = AmxModx_Bridge_GameConfGetAddress(_handle, name);
                return address == UIntPtr.Zero ? IntPtr.Zero : (IntPtr)address;
            }

            /// <summary>
            /// 关闭配置文件
            /// </summary>
            public void CloseConfig()
            {
                if (_handle != -1)
                {
                    AmxModx_Bridge_CloseGameConfigFile(ref _handle);
                    _handle = -1;
                }
            }

            /// <summary>
            /// 释放资源
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        CloseConfig();
                    }
                    _disposed = true;
                }
            }

            ~GameConfigManager()
            {
                Dispose(false);
            }
        }
    }
}