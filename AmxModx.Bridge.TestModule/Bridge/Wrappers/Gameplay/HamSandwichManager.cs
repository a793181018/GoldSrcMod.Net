using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AmxModx.Bridge.Wrappers.Gameplay
{
    /// <summary>
    /// Ham Sandwich桥接管理器，提供简化的API访问
    /// </summary>
    public static class HamSandwichManager
    {
        /// <summary>
        /// 初始化Ham Sandwich桥接
        /// </summary>
        public static bool Initialize()
        {
            return HamSandwichBridge.AmxModx_Bridge_HamSandwichInit() == 0;
        }

        /// <summary>
        /// 清理Ham Sandwich桥接资源
        /// </summary>
        public static void Cleanup()
        {
            HamSandwichBridge.AmxModx_Bridge_HamSandwichCleanup();
        }

        /// <summary>
        /// 注册Ham钩子
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="functionName">函数名称</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID</returns>
        public static int RegisterHamHook(int entityIndex, string functionName, bool post = false)
        {
            return HamSandwichBridge.RegisterHamHook(entityIndex, functionName, post);
        }

        /// <summary>
        /// 注册实体Ham钩子
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="functionName">函数名称</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID</returns>
        public static int RegisterEntityHamHook(int entityIndex, string functionName, bool post = false)
        {
            return HamSandwichBridge.RegisterEntityHamHook(entityIndex, functionName, post);
        }

        /// <summary>
        /// 注册游戏DLL函数钩子
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID</returns>
        public static int RegisterGameDllHook(string functionName, bool post = false)
        {
            return HamSandwichBridge.RegisterGameDllHook(functionName, post);
        }

        /// <summary>
        /// 注销Ham钩子
        /// </summary>
        /// <param name="hookId">钩子ID</param>
        /// <returns>是否成功</returns>
        public static bool UnregisterHook(int hookId)
        {
            return HamSandwichBridge.UnregisterHamHook(hookId);
        }

        /// <summary>
        /// 获取私有数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>是否成功</returns>
        public static bool GetPrivateDataInt(int entityIndex, string key, out int value)
        {
            return HamSandwichBridge.GetPrivateDataInt(entityIndex, key, out value);
        }

        /// <summary>
        /// 设置私有数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        public static bool SetPrivateDataInt(int entityIndex, string key, int value)
        {
            return HamSandwichBridge.SetPrivateDataInt(entityIndex, key, value);
        }

        /// <summary>
        /// 获取私有数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>是否成功</returns>
        public static bool GetPrivateDataFloat(int entityIndex, string key, out float value)
        {
            return HamSandwichBridge.GetPrivateDataFloat(entityIndex, key, out value);
        }

        /// <summary>
        /// 设置私有数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        public static bool SetPrivateDataFloat(int entityIndex, string key, float value)
        {
            return HamSandwichBridge.SetPrivateDataFloat(entityIndex, key, value);
        }

        /// <summary>
        /// 获取私有数据字符串值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出字符串</param>
        /// <returns>是否成功</returns>
        public static bool GetPrivateDataString(int entityIndex, string key, out string value)
        {
            value = string.Empty;
            var buffer = new byte[256];
            var result = HamSandwichBridge.GetPrivateDataString(entityIndex, key, out var actualValue);
            if (result)
            {
                value = actualValue;
            }
            return result;
        }

        /// <summary>
        /// 设置私有数据字符串值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">字符串值</param>
        /// <returns>是否成功</returns>
        public static bool SetPrivateDataString(int entityIndex, string key, string value)
        {
            return HamSandwichBridge.SetPrivateDataString(entityIndex, key, value);
        }

        /// <summary>
        /// 获取原始数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <returns>整数值</returns>
        public static int GetOriginalDataInt(int entityIndex, int offset)
        {
            return HamSandwichBridge.GetOriginalDataInt(entityIndex, offset);
        }

        /// <summary>
        /// 设置原始数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">值</param>
        public static void SetOriginalDataInt(int entityIndex, int offset, int value)
        {
            HamSandwichBridge.SetOriginalDataInt(entityIndex, offset, value);
        }

        /// <summary>
        /// 获取原始数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <returns>浮点值</returns>
        public static float GetOriginalDataFloat(int entityIndex, int offset)
        {
            return HamSandwichBridge.GetOriginalDataFloat(entityIndex, offset);
        }

        /// <summary>
        /// 设置原始数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">值</param>
        public static void SetOriginalDataFloat(int entityIndex, int offset, float value)
        {
            HamSandwichBridge.SetOriginalDataFloat(entityIndex, offset, value);
        }

        /// <summary>
        /// 获取返回值的整数值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">输出值</param>
        /// <returns>是否成功</returns>
        public static bool GetReturnValueInt(string functionName, out int value)
        {
            return HamSandwichBridge.GetReturnValueInt(functionName, out value);
        }

        /// <summary>
        /// 设置返回值的整数值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        public static bool SetReturnValueInt(string functionName, int value)
        {
            return HamSandwichBridge.SetReturnValueInt(functionName, value);
        }

        /// <summary>
        /// 获取返回值的浮点值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">输出值</param>
        /// <returns>是否成功</returns>
        public static bool GetReturnValueFloat(string functionName, out float value)
        {
            return HamSandwichBridge.GetReturnValueFloat(functionName, out value);
        }

        /// <summary>
        /// 设置返回值的浮点值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        public static bool SetReturnValueFloat(string functionName, float value)
        {
            return HamSandwichBridge.SetReturnValueFloat(functionName, value);
        }

        /// <summary>
        /// 获取超级数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>是否成功</returns>
        public static bool GetSuperDataInt(int entityIndex, string className, string key, out int value)
        {
            return HamSandwichBridge.GetSuperDataInt(entityIndex, className, key, out value);
        }

        /// <summary>
        /// 设置超级数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        public static bool SetSuperDataInt(int entityIndex, string className, string key, int value)
        {
            return HamSandwichBridge.SetSuperDataInt(entityIndex, className, key, value);
        }

        /// <summary>
        /// 获取超级数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>是否成功</returns>
        public static bool GetSuperDataFloat(int entityIndex, string className, string key, out float value)
        {
            return HamSandwichBridge.GetSuperDataFloat(entityIndex, className, key, out value);
        }

        /// <summary>
        /// 设置超级数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        public static bool SetSuperDataFloat(int entityIndex, string className, string key, float value)
        {
            return HamSandwichBridge.SetSuperDataFloat(entityIndex, className, key, value);
        }
    }
}