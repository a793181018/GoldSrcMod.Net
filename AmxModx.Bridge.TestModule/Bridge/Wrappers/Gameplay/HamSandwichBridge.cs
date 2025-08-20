using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Wrappers.Gameplay
{
    /// <summary>
    /// HamSandwich桥接类，提供HamSandwich模块的完整桥接接口
    /// </summary>
    public static class HamSandwichBridge
    {
        #region 初始化和清理

        /// <summary>
        /// 初始化HamSandwich桥接
        /// </summary>
        /// <returns>成功返回0，失败返回-1</returns>
        public static int AmxModx_Bridge_HamSandwichInit()
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.AmxModx_Bridge_HamSandwichInit() ? 0 : -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HamSandwich初始化失败: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// 清理HamSandwich桥接资源
        /// </summary>
        public static void AmxModx_Bridge_HamSandwichCleanup()
        {
            try
            {
                AmxModx.Bridge.HamSandwich.NativeMethods.AmxModx_Bridge_HamSandwichCleanup();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HamSandwich清理失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 初始化HamSandwich模块
        /// </summary>
        public static void InitializeHamSandwichBridge()
        {
            try
            {
                AmxModx.Bridge.HamSandwich.NativeMethods.InitializeHamSandwichBridge();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HamSandwich模块初始化失败: {ex.Message}");
            }
        }

        #endregion

        #region 钩子管理

        /// <summary>
        /// 注册Ham钩子
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="functionName">函数名称</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID，失败返回-1</returns>
        public static int RegisterHamHook(int entityIndex, string functionName, bool post)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.RegisterHamHook(entityIndex, functionName, post);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"注册Ham钩子失败: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// 注册实体Ham钩子
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="functionName">函数名称</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID，失败返回-1</returns>
        public static int RegisterEntityHamHook(int entityIndex, string functionName, bool post)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.RegisterEntityHamHook(entityIndex, functionName, post);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"注册实体Ham钩子失败: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// 注册游戏DLL函数钩子
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="post">是否为后置钩子</param>
        /// <returns>钩子ID，失败返回-1</returns>
        public static int RegisterGameDllHook(string functionName, bool post)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.RegisterGameDllHook(functionName, post);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"注册游戏DLL钩子失败: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// 注销Ham钩子
        /// </summary>
        /// <param name="hookId">钩子ID</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool UnregisterHamHook(int hookId)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.UnregisterHamHook(hookId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"注销Ham钩子失败: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region 数据访问

        /// <summary>
        /// 获取私有数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool GetPrivateDataInt(int entityIndex, string key, out int value)
        {
            value = 0;
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.GetPrivateDataInt(entityIndex, key, out value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取私有数据整数值失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取私有数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool GetPrivateDataFloat(int entityIndex, string key, out float value)
        {
            value = 0.0f;
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.GetPrivateDataFloat(entityIndex, key, out value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取私有数据浮点值失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取私有数据字符串值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool GetPrivateDataString(int entityIndex, string key, out string value, int maxLength = 256)
        {
            value = string.Empty;
            try
            {
                var buffer = new byte[maxLength];
                var result = AmxModx.Bridge.HamSandwich.NativeMethods.GetPrivateDataString(entityIndex, key, buffer, maxLength);
                if (result)
                {
                    value = System.Text.Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取私有数据字符串失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置私有数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetPrivateDataInt(int entityIndex, string key, int value)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.SetPrivateDataInt(entityIndex, key, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设置私有数据整数值失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置私有数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetPrivateDataFloat(int entityIndex, string key, float value)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.SetPrivateDataFloat(entityIndex, key, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设置私有数据浮点值失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置私有数据字符串值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetPrivateDataString(int entityIndex, string key, string value)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.SetPrivateDataString(entityIndex, key, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设置私有数据字符串失败: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region 原始数据访问

        /// <summary>
        /// 获取原始数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <returns>整数值</returns>
        public static int GetOriginalDataInt(int entityIndex, int offset)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.GetOriginalDataInt(entityIndex, offset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取原始数据整数值失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 获取原始数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <returns>浮点值</returns>
        public static float GetOriginalDataFloat(int entityIndex, int offset)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.GetOriginalDataFloat(entityIndex, offset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取原始数据浮点值失败: {ex.Message}");
                return 0.0f;
            }
        }

        /// <summary>
        /// 设置原始数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">值</param>
        public static void SetOriginalDataInt(int entityIndex, int offset, int value)
        {
            try
            {
                AmxModx.Bridge.HamSandwich.NativeMethods.SetOriginalDataInt(entityIndex, offset, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设置原始数据整数值失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 设置原始数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">值</param>
        public static void SetOriginalDataFloat(int entityIndex, int offset, float value)
        {
            try
            {
                AmxModx.Bridge.HamSandwich.NativeMethods.SetOriginalDataFloat(entityIndex, offset, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设置原始数据浮点值失败: {ex.Message}");
            }
        }

        #endregion

        #region 返回值处理

        /// <summary>
        /// 获取返回值的整数值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool GetReturnValueInt(string functionName, out int value)
        {
            value = 0;
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.GetReturnValueInt(functionName, out value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取返回值整数值失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取返回值的浮点值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool GetReturnValueFloat(string functionName, out float value)
        {
            value = 0.0f;
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.GetReturnValueFloat(functionName, out value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取返回值浮点值失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置返回值的整数值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetReturnValueInt(string functionName, int value)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.SetReturnValueInt(functionName, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设置返回值整数值失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置返回值的浮点值
        /// </summary>
        /// <param name="functionName">函数名称</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetReturnValueFloat(string functionName, float value)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.SetReturnValueFloat(functionName, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设置返回值浮点值失败: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region 超级数据访问

        /// <summary>
        /// 获取超级数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool GetSuperDataInt(int entityIndex, string className, string key, out int value)
        {
            value = 0;
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.GetSuperDataInt(entityIndex, className, key, out value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取超级数据整数值失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取超级数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">输出值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool GetSuperDataFloat(int entityIndex, string className, string key, out float value)
        {
            value = 0.0f;
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.GetSuperDataFloat(entityIndex, className, key, out value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取超级数据浮点值失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置超级数据整数值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetSuperDataInt(int entityIndex, string className, string key, int value)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.SetSuperDataInt(entityIndex, className, key, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设置超级数据整数值失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置超级数据浮点值
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="className">类名</param>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetSuperDataFloat(int entityIndex, string className, string key, float value)
        {
            try
            {
                return AmxModx.Bridge.HamSandwich.NativeMethods.SetSuperDataFloat(entityIndex, className, key, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设置超级数据浮点值失败: {ex.Message}");
                return false;
            }
        }

        #endregion
    }
}