using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge
{
    /// <summary>
    /// AMXModX文件管理器
    /// </summary>
    public static class FileManager
    {
        /// <summary>
        /// 读取文件内容为字节数组
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <returns>文件内容字节数组，失败返回null</returns>
        public static byte[] ReadFileBytes(string filename)
        {
            IntPtr buffer;
            int size;
            
            if (NativeFile.AmxModx_Bridge_ReadFile(filename, out buffer, out size) == 1)
            {
                try
                {
                    byte[] data = new byte[size];
                    Marshal.Copy(buffer, data, 0, size);
                    return data;
                }
                finally
                {
                    NativeFile.AmxModx_Bridge_FreeBuffer(buffer);
                }
            }
            
            return null;
        }

        /// <summary>
        /// 读取文件内容为字符串
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <returns>文件内容字符串，失败返回null</returns>
        public static string ReadFileText(string filename)
        {
            byte[] data = ReadFileBytes(filename);
            return data != null ? Encoding.UTF8.GetString(data) : null;
        }

        /// <summary>
        /// 写入字节数组到文件
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <param name="data">要写入的数据</param>
        /// <returns>是否写入成功</returns>
        public static bool WriteFileBytes(string filename, byte[] data)
        {
            return NativeFile.AmxModx_Bridge_WriteFile(filename, data, data.Length) == 1;
        }

        /// <summary>
        /// 写入字符串到文件
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <param name="text">要写入的文本</param>
        /// <returns>是否写入成功</returns>
        public static bool WriteFileText(string filename, string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            return WriteFileBytes(filename, data);
        }

        /// <summary>
        /// 获取目录中的文件列表
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>文件名数组</returns>
        public static string[] GetFiles(string path)
        {
            IntPtr filesPtr;
            int count;
            
            if (NativeFile.AmxModx_Bridge_ReadDir(path, out filesPtr, out count) == 1)
            {
                try
                {
                    string[] fileList = new string[count];
                    IntPtr[] ptrArray = new IntPtr[count];
                    
                    Marshal.Copy(filesPtr, ptrArray, 0, count);
                    
                    for (int i = 0; i < count; i++)
                    {
                        fileList[i] = Marshal.PtrToStringAnsi(ptrArray[i]);
                    }
                    
                    return fileList;
                }
                finally
                {
                    NativeFile.AmxModx_Bridge_FreeStringArray(filesPtr, count);
                }
            }
            
            return new string[0];
        }
    }

    /// <summary>
    /// AMXModX数据包管理器
    /// </summary>
    public class DataPack : IDisposable
    {
        private IntPtr _handle;
        private bool _disposed = false;

        /// <summary>
        /// 创建新的数据包
        /// </summary>
        public DataPack()
        {
            _handle = NativeDataPack.AmxModx_Bridge_CreateDataPack();
            if (_handle == IntPtr.Zero)
                throw new InvalidOperationException("Failed to create DataPack");
        }

        /// <summary>
        /// 获取数据包大小
        /// </summary>
        public int Size => NativeDataPack.AmxModx_Bridge_GetDataPackSize(_handle);

        /// <summary>
        /// 获取当前读取位置
        /// </summary>
        public int Position => NativeDataPack.AmxModx_Bridge_GetPosition(_handle);

        /// <summary>
        /// 重置数据包读取位置
        /// </summary>
        public void Reset()
        {
            NativeDataPack.AmxModx_Bridge_ResetDataPack(_handle);
        }

        /// <summary>
        /// 设置读取位置
        /// </summary>
        /// <param name="position">目标位置</param>
        public void SetPosition(int position)
        {
            NativeDataPack.AmxModx_Bridge_SetPosition(_handle, position);
        }

        /// <summary>
        /// 写入整数
        /// </summary>
        /// <param name="value">整数值</param>
        public void WriteInt(int value)
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

        /// <summary>
        /// 读取整数
        /// </summary>
        /// <returns>整数值</returns>
        public int ReadInt()
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
            IntPtr strPtr = NativeDataPack.AmxModx_Bridge_ReadString(_handle);
            return strPtr != IntPtr.Zero ? Marshal.PtrToStringAnsi(strPtr) : null;
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
                if (_handle != IntPtr.Zero)
                {
                    NativeDataPack.AmxModx_Bridge_DestroyDataPack(_handle);
                    _handle = IntPtr.Zero;
                }
                _disposed = true;
            }
        }

        ~DataPack()
        {
            Dispose(false);
        }
    }

    /// <summary>
    /// AMXModX控制台变量管理器
    /// </summary>
    public static class CVarManager
    {
        /// <summary>
        /// 创建控制台变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="flags">标志位</param>
        /// <returns>变量ID</returns>
        public static int CreateCVar(string name, string defaultValue, int flags = 0)
        {
            return NativeCVar.AmxModx_Bridge_CreateCVar(name, defaultValue, flags);
        }

        /// <summary>
        /// 查找控制台变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>变量ID，未找到返回-1</returns>
        public static int FindCVar(string name)
        {
            return NativeCVar.AmxModx_Bridge_FindCVar(name);
        }

        /// <summary>
        /// 获取控制台变量字符串值
        /// </summary>
        /// <param name="cvarId">变量ID</param>
        /// <returns>字符串值</returns>
        public static string GetCVarString(int cvarId)
        {
            StringBuilder buffer = new StringBuilder(256);
            return NativeCVar.AmxModx_Bridge_GetCVarString(cvarId, buffer, buffer.Capacity) == 1 
                ? buffer.ToString() 
                : null;
        }

        /// <summary>
        /// 设置控制台变量字符串值
        /// </summary>
        /// <param name="cvarId">变量ID</param>
        /// <param name="value">新值</param>
        /// <returns>是否设置成功</returns>
        public static bool SetCVarString(int cvarId, string value)
        {
            return NativeCVar.AmxModx_Bridge_SetCVarString(cvarId, value) == 1;
        }

        /// <summary>
        /// 获取控制台变量浮点值
        /// </summary>
        /// <param name="cvarId">变量ID</param>
        /// <returns>浮点值</returns>
        public static float GetCVarFloat(int cvarId)
        {
            return NativeCVar.AmxModx_Bridge_GetCVarFloat(cvarId);
        }

        /// <summary>
        /// 设置控制台变量浮点值
        /// </summary>
        /// <param name="cvarId">变量ID</param>
        /// <param name="value">新值</param>
        /// <returns>是否设置成功</returns>
        public static bool SetCVarFloat(int cvarId, float value)
        {
            return NativeCVar.AmxModx_Bridge_SetCVarFloat(cvarId, value) == 1;
        }

        /// <summary>
        /// 获取控制台变量整数值
        /// </summary>
        /// <param name="cvarId">变量ID</param>
        /// <returns>整数值</returns>
        public static int GetCVarInt(int cvarId)
        {
            return NativeCVar.AmxModx_Bridge_GetCVarInt(cvarId);
        }

        /// <summary>
        /// 设置控制台变量整数值
        /// </summary>
        /// <param name="cvarId">变量ID</param>
        /// <param name="value">新值</param>
        /// <returns>是否设置成功</returns>
        public static bool SetCVarInt(int cvarId, int value)
        {
            return NativeCVar.AmxModx_Bridge_SetCVarInt(cvarId, value) == 1;
        }
    }

    /// <summary>
    /// AMXModX游戏配置管理器
    /// </summary>
    public class GameConfig : IDisposable
    {
        private int _configId;
        private bool _disposed = false;

        /// <summary>
        /// 加载游戏配置文件
        /// </summary>
        /// <param name="filename">配置文件名</param>
        public GameConfig(string filename)
        {
            _configId = NativeGameConfig.AmxModx_Bridge_LoadGameConfig(filename);
            if (_configId <= 0)
                throw new InvalidOperationException($"Failed to load game config: {filename}");
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>配置值，未找到返回null</returns>
        public string GetValue(string key)
        {
            StringBuilder buffer = new StringBuilder(512);
            return NativeGameConfig.AmxModx_Bridge_GetGameConfigValue(_configId, key, buffer, buffer.Capacity) == 1 
                ? buffer.ToString() 
                : null;
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
                NativeGameConfig.AmxModx_Bridge_FreeGameConfig(_configId);
                _disposed = true;
            }
        }

        ~GameConfig()
        {
            Dispose(false);
        }
    }

    /// <summary>
    /// AMXModX持久化存储管理器
    /// </summary>
    public class Vault : IDisposable
    {
        private int _vaultId;
        private bool _disposed = false;

        /// <summary>
        /// 打开Vault数据库
        /// </summary>
        /// <param name="filename">数据库文件名</param>
        public Vault(string filename)
        {
            _vaultId = NativeVault.AmxModx_Bridge_OpenVault(filename);
            if (_vaultId <= 0)
                throw new InvalidOperationException($"Failed to open vault: {filename}");
        }

        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>是否设置成功</returns>
        public bool SetValue(string key, string value)
        {
            return NativeVault.AmxModx_Bridge_SetVaultValue(_vaultId, key, value) == 1;
        }

        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>键值，未找到返回null</returns>
        public string GetValue(string key)
        {
            StringBuilder buffer = new StringBuilder(512);
            return NativeVault.AmxModx_Bridge_GetVaultValue(_vaultId, key, buffer, buffer.Capacity) == 1 
                ? buffer.ToString() 
                : null;
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
                NativeVault.AmxModx_Bridge_CloseVault(_vaultId);
                _disposed = true;
            }
        }

        ~Vault()
        {
            Dispose(false);
        }
    }
}