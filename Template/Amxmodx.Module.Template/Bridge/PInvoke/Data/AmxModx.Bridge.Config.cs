using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.Config
{
    /// <summary>
    /// 配置文件桥接接口
    /// 提供游戏配置文件处理功能
    /// </summary>
    public static class ConfigBridge
    {
        /// <summary>
        /// 加载游戏配置文件
        /// </summary>
        /// <param name="configName">配置文件名称</param>
        /// <returns>配置句柄</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_LoadGameConfig([MarshalAs(UnmanagedType.LPStr)] string configName);

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="configHandle">配置句柄</param>
        /// <param name="key">配置键</param>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>实际长度</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetConfigValue(IntPtr configHandle, [MarshalAs(UnmanagedType.LPStr)] string key, [Out] byte[] buffer, int bufferSize);

        /// <summary>
        /// 安全的获取配置值方法
        /// </summary>
        /// <param name="configHandle">配置句柄</param>
        /// <param name="key">配置键</param>
        /// <returns>配置值</returns>
        public static string GetConfigValueSafe(IntPtr configHandle, string key)
        {
            if (configHandle == IntPtr.Zero || string.IsNullOrEmpty(key))
                return string.Empty;

            byte[] buffer = new byte[256];
            int length = AmxModx_Bridge_GetConfigValue(configHandle, key, buffer, buffer.Length);
            if (length <= 0)
                return string.Empty;

            return System.Text.Encoding.UTF8.GetString(buffer, 0, Math.Min(length, buffer.Length));
        }
    }
}