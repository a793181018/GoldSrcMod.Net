using System;

namespace AmxModx.Bridge.HamSandwich
{
    /// <summary>
    /// 实体私有数据访问器
    /// </summary>
    public static class HamEntityData
    {
        /// <summary>
        /// 获取实体私有数据（整数）
        /// </summary>
        public static int GetInt(int entity, string key)
        {
            if (NativeMethods.GetPrivateDataInt(entity, key, out int value))
                return value;
            return 0;
        }

        /// <summary>
        /// 获取实体私有数据（浮点数）
        /// </summary>
        public static float GetFloat(int entity, string key)
        {
            if (NativeMethods.GetPrivateDataFloat(entity, key, out float value))
                return value;
            return 0.0f;
        }

        /// <summary>
        /// 获取实体私有数据（字符串）
        /// </summary>
        public static string GetString(int entity, string key)
        {
            byte[] buffer = new byte[256];
            if (NativeMethods.GetPrivateDataString(entity, key, buffer, buffer.Length))
                return System.Text.Encoding.ASCII.GetString(buffer).TrimEnd('\0');
            return string.Empty;
        }

        /// <summary>
        /// 设置实体私有数据（整数）
        /// </summary>
        public static bool SetInt(int entity, string key, int value)
        {
            return NativeMethods.SetPrivateDataInt(entity, key, value);
        }

        /// <summary>
        /// 设置实体私有数据（浮点数）
        /// </summary>
        public static bool SetFloat(int entity, string key, float value)
        {
            return NativeMethods.SetPrivateDataFloat(entity, key, value);
        }

        /// <summary>
        /// 设置实体私有数据（字符串）
        /// </summary>
        public static bool SetString(int entity, string key, string value)
        {
            return NativeMethods.SetPrivateDataString(entity, key, value);
        }
    }

    /// <summary>
    /// Ham返回值管理器（当前版本简化实现）
    /// </summary>
    public static class HamReturnValue
    {
        /// <summary>
        /// 获取返回状态（当前不支持）
        /// </summary>
        public static HamReturnStatus GetStatus()
        {
            // 当前实现不支持返回状态查询
            return HamReturnStatus.Continue;
        }

        /// <summary>
        /// 获取返回值（整数）
        /// </summary>
        public static int GetInt(string functionName)
        {
            // 当前实现不支持返回值获取
            return 0;
        }

        /// <summary>
        /// 获取返回值（浮点数）
        /// </summary>
        public static float GetFloat(string functionName)
        {
            // 当前实现不支持返回值获取
            return 0.0f;
        }

        /// <summary>
        /// 设置返回值（整数）
        /// </summary>
        public static bool SetInt(string functionName, int value)
        {
            // 当前实现不支持返回值设置
            return false;
        }

        /// <summary>
        /// 设置返回值（浮点数）
        /// </summary>
        public static bool SetFloat(string functionName, float value)
        {
            // 当前实现不支持返回值设置
            return false;
        }

        /// <summary>
        /// 重置返回值
        /// </summary>
        public static void Reset(string functionName)
        {
            // 当前实现不支持返回值重置
        }
    }
}