using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.File
{
    /// <summary>
    /// 文件系统高级封装类
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件内容</returns>
        public static string ReadFile(string filePath)
        {
            IntPtr buffer = NativeFile.AmxModx_Bridge_ReadFile(filePath, out int size);
            if (buffer == IntPtr.Zero)
                return null;

            try
            {
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return Encoding.UTF8.GetString(bytes);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// 写入文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">内容</param>
        /// <returns>是否成功</returns>
        public static bool WriteFile(string filePath, string content)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return NativeFile.AmxModx_Bridge_WriteFile(filePath, content, bytes.Length) != 0;
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filePath)
        {
            return NativeFile.AmxModx_Bridge_FileExists(filePath) != 0;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否成功</returns>
        public static bool DeleteFile(string filePath)
        {
            return NativeFile.AmxModx_Bridge_DeleteFile(filePath) != 0;
        }
    }

    /// <summary>
    /// 数据包高级封装类
    /// </summary>
    public class DataPack : IDisposable
    {
        private IntPtr _handle;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataPack()
        {
            _handle = NativeDataPack.AmxModx_Bridge_CreateDataPack();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~DataPack()
        {
            Dispose(false);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否手动释放</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_handle != IntPtr.Zero)
            {
                NativeDataPack.AmxModx_Bridge_DestroyDataPack(_handle);
                _handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// 读取整数
        /// </summary>
        /// <returns>整数值</returns>
        public int ReadCell()
        {
            return NativeDataPack.AmxModx_Bridge_ReadCell(_handle);
        }

        /// <summary>
        /// 读取浮点数
        /// </summary>
        /// <returns>浮点数值</returns>
        public float ReadFloat()
        {
            return NativeDataPack.AmxModx_Bridge_ReadFloat(_handle);
        }

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <returns>字符串值</returns>
        public string ReadString()
        {
            IntPtr str = NativeDataPack.AmxModx_Bridge_ReadString(_handle);
            return str != IntPtr.Zero ? Marshal.PtrToStringAnsi(str) : null;
        }

        /// <summary>
        /// 写入整数
        /// </summary>
        /// <param name="value">整数值</param>
        public void WriteCell(int value)
        {
            NativeDataPack.AmxModx_Bridge_WriteCell(_handle, value);
        }

        /// <summary>
        /// 写入浮点数
        /// </summary>
        /// <param name="value">浮点数值</param>
        public void WriteFloat(float value)
        {
            NativeDataPack.AmxModx_Bridge_WriteFloat(_handle, value);
        }

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value">字符串值</param>
        public void WriteString(string value)
        {
            NativeDataPack.AmxModx_Bridge_WriteString(_handle, value);
        }
    }

    /// <summary>
    /// 控制台变量高级封装类
    /// </summary>
    public class ConsoleVariable : IDisposable
    {
        private IntPtr _handle;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="flags">标志位</param>
        public ConsoleVariable(string name, string defaultValue, int flags = 0)
        {
            _handle = NativeCVar.AmxModx_Bridge_CreateCVar(name, defaultValue, flags);
        }

        /// <summary>
        /// 构造函数（查找现有变量）
        /// </summary>
        /// <param name="name">变量名</param>
        public ConsoleVariable(string name)
        {
            _handle = NativeCVar.AmxModx_Bridge_FindCVar(name);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~ConsoleVariable()
        {
            Dispose(false);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否手动释放</param>
        protected virtual void Dispose(bool disposing)
        {
            _handle = IntPtr.Zero;
        }

        /// <summary>
        /// 获取字符串值
        /// </summary>
        public string StringValue
        {
            get
            {
                IntPtr str = NativeCVar.AmxModx_Bridge_GetCVarString(_handle);
                return str != IntPtr.Zero ? Marshal.PtrToStringAnsi(str) : null;
            }
            set => NativeCVar.AmxModx_Bridge_SetCVarString(_handle, value);
        }

        /// <summary>
        /// 获取浮点数值
        /// </summary>
        public float FloatValue
        {
            get => NativeCVar.AmxModx_Bridge_GetCVarFloat(_handle);
            set => NativeCVar.AmxModx_Bridge_SetCVarFloat(_handle, value);
        }

        /// <summary>
        /// 获取整数值
        /// </summary>
        public int IntValue
        {
            get => NativeCVar.AmxModx_Bridge_GetCVarInt(_handle);
            set => NativeCVar.AmxModx_Bridge_SetCVarInt(_handle, value);
        }
    }

    /// <summary>
    /// 游戏配置高级封装类
    /// </summary>
    public class GameConfig : IDisposable
    {
        private IntPtr _handle;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath">配置文件路径</param>
        public GameConfig(string filePath)
        {
            _handle = NativeGameConfig.AmxModx_Bridge_LoadGameConfig(filePath);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~GameConfig()
        {
            Dispose(false);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否手动释放</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_handle != IntPtr.Zero)
            {
                NativeGameConfig.AmxModx_Bridge_UnloadGameConfig(_handle);
                _handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>配置值</returns>
        public string GetValue(string key)
        {
            IntPtr str = NativeGameConfig.AmxModx_Bridge_GetConfigValue(_handle, key);
            return str != IntPtr.Zero ? Marshal.PtrToStringAnsi(str) : null;
        }
    }

    /// <summary>
    /// 持久化存储高级封装类
    /// </summary>
    public class Vault : IDisposable
    {
        private IntPtr _handle;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">保险库名称</param>
        public Vault(string name)
        {
            _handle = NativeVault.AmxModx_Bridge_OpenVault(name);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~Vault()
        {
            Dispose(false);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否手动释放</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_handle != IntPtr.Zero)
            {
                NativeVault.AmxModx_Bridge_CloseVault(_handle);
                _handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取整数值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>整数值</returns>
        public int GetInt(string key, int defaultValue = 0)
        {
            return NativeVault.AmxModx_Bridge_GetVaultInt(_handle, key, defaultValue);
        }

        /// <summary>
        /// 设置整数值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">整数值</param>
        public void SetInt(string key, int value)
        {
            NativeVault.AmxModx_Bridge_SetVaultInt(_handle, key, value);
        }

        /// <summary>
        /// 获取字符串值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字符串值</returns>
        public string GetString(string key, string defaultValue = null)
        {
            IntPtr str = NativeVault.AmxModx_Bridge_GetVaultString(_handle, key, defaultValue);
            return str != IntPtr.Zero ? Marshal.PtrToStringAnsi(str) : defaultValue;
        }

        /// <summary>
        /// 设置字符串值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">字符串值</param>
        public void SetString(string key, string value)
        {
            NativeVault.AmxModx_Bridge_SetVaultString(_handle, key, value);
        }
    }
}