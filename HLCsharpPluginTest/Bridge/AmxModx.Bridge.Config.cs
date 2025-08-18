using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Config
{
    /// <summary>
    /// 配置管理桥接接口，提供游戏配置操作功能
    /// </summary>
    public static class ConfigBridge
    {
        private const string NativeLibrary = "amxmodx_mm";

        #region 游戏配置操作

        /// <summary>
        /// 加载游戏配置文件
        /// </summary>
        /// <param name="filePath">配置文件路径</param>
        /// <returns>加载的配置对象指针</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_LoadGameConfig([MarshalAs(UnmanagedType.LPStr)] string filePath);

        /// <summary>
        /// 卸载游戏配置文件
        /// </summary>
        /// <param name="config">配置对象指针</param>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void AmxModx_Bridge_UnloadGameConfig(IntPtr config);

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="config">配置对象指针</param>
        /// <param name="key">配置键名</param>
        /// <returns>配置值字符串</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_GetConfigValue(IntPtr config, [MarshalAs(UnmanagedType.LPStr)] string key);

        #endregion

        #region 便捷方法

        /// <summary>
        /// 获取配置值的安全封装
        /// </summary>
        /// <param name="config">配置对象指针</param>
        /// <param name="key">配置键名</param>
        /// <returns>配置值字符串</returns>
        public static string GetConfigValueSafe(IntPtr config, string key)
        {
            if (config == IntPtr.Zero || string.IsNullOrEmpty(key))
                return string.Empty;

            IntPtr ptr = AmxModx_Bridge_GetConfigValue(config, key);
            return ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : string.Empty;
        }

        #endregion
    }
}