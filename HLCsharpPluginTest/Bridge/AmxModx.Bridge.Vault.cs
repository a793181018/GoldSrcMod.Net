using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Vault
{
    /// <summary>
    /// Vault持久化存储桥接接口，提供键值存储功能
    /// </summary>
    public static class VaultBridge
    {
        private const string NativeLibrary = "amxmodx_mm";

        #region Vault操作

        /// <summary>
        /// 获取全局Vault实例
        /// </summary>
        /// <returns>Vault实例指针</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_GetVault();

        /// <summary>
        /// 检查键是否存在
        /// </summary>
        /// <param name="vault">Vault实例指针</param>
        /// <param name="key">键名</param>
        /// <returns>键是否存在</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_VaultExists(IntPtr vault, [MarshalAs(UnmanagedType.LPStr)] string key);

        /// <summary>
        /// 存储键值对
        /// </summary>
        /// <param name="vault">Vault实例指针</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>存储是否成功</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_VaultPut(IntPtr vault, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value);

        /// <summary>
        /// 移除键值对
        /// </summary>
        /// <param name="vault">Vault实例指针</param>
        /// <param name="key">键名</param>
        /// <returns>移除是否成功</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AmxModx_Bridge_VaultRemove(IntPtr vault, [MarshalAs(UnmanagedType.LPStr)] string key);

        /// <summary>
        /// 获取键对应的值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>值字符串</returns>
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AmxModx_Bridge_VaultGet([MarshalAs(UnmanagedType.LPStr)] string key);

        #endregion

        #region 便捷方法

        /// <summary>
        /// 获取Vault值的安全封装
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>值字符串</returns>
        public static string GetVaultValueSafe(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            IntPtr ptr = AmxModx_Bridge_VaultGet(key);
            return ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : string.Empty;
        }

        /// <summary>
        /// 存储键值对的安全封装
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>存储是否成功</returns>
        public static bool SetVaultValueSafe(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                return false;

            IntPtr vault = AmxModx_Bridge_GetVault();
            return vault != IntPtr.Zero && AmxModx_Bridge_VaultPut(vault, key, value);
        }

        /// <summary>
        /// 检查键是否存在
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>键是否存在</returns>
        public static bool ExistsVaultKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            IntPtr vault = AmxModx_Bridge_GetVault();
            return vault != IntPtr.Zero && AmxModx_Bridge_VaultExists(vault, key);
        }

        #endregion
    }
}