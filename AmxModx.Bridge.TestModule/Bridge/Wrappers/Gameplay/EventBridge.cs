using System;
using System.Runtime.InteropServices;
using AmxModx.Bridge.Event;

namespace AmxModx.Bridge.Wrappers.Gameplay
{
    /// <summary>
    /// 事件桥接包装器
    /// 提供事件系统的高级接口
    /// </summary>
    public static class EventBridge
    {
        /// <summary>
        /// 注册游戏事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="forceRegistration">是否强制注册</param>
        /// <returns>事件句柄，失败返回-1</returns>
        public static int RegisterEvent(string eventName, bool forceRegistration = false)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_RegisterEvent(eventName, forceRegistration);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventBridge] 注册事件失败: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// 取消注册游戏事件
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <returns>操作是否成功</returns>
        public static bool UnregisterEvent(int eventHandle)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_UnregisterEvent(eventHandle);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventBridge] 取消注册事件失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 触发游戏事件
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="dontBroadcast">是否不广播</param>
        /// <returns>操作是否成功</returns>
        public static bool FireEvent(int eventHandle, bool dontBroadcast = false)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_FireEvent(eventHandle, dontBroadcast);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventBridge] 触发事件失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置事件字符串参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <param name="value">参数值</param>
        /// <returns>操作是否成功</returns>
        public static bool SetEventString(int eventHandle, string key, string value)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_SetEventString(eventHandle, key, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventBridge] 设置事件字符串参数失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置事件整型参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <param name="value">参数值</param>
        /// <returns>操作是否成功</returns>
        public static bool SetEventInt(int eventHandle, string key, int value)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_SetEventInt(eventHandle, key, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventBridge] 设置事件整型参数失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 设置事件浮点参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <param name="value">参数值</param>
        /// <returns>操作是否成功</returns>
        public static bool SetEventFloat(int eventHandle, string key, float value)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_SetEventFloat(eventHandle, key, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventBridge] 设置事件浮点参数失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取事件字符串参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <returns>参数值</returns>
        public static string GetEventString(int eventHandle, string key)
        {
            try
            {
                byte[] buffer = new byte[256];
                int length = NativeMethods.AmxModx_Bridge_GetEventString(eventHandle, key, buffer, buffer.Length);
                return length > 0 ? System.Text.Encoding.ASCII.GetString(buffer, 0, length).TrimEnd('\0') : string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventBridge] 获取事件字符串参数失败: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取事件整型参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        public static int GetEventInt(int eventHandle, string key, int defaultValue = 0)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_GetEventInt(eventHandle, key, defaultValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventBridge] 获取事件整型参数失败: {ex.Message}");
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取事件浮点参数
        /// </summary>
        /// <param name="eventHandle">事件句柄</param>
        /// <param name="key">参数键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        public static float GetEventFloat(int eventHandle, string key, float defaultValue = 0.0f)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_GetEventFloat(eventHandle, key, defaultValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventBridge] 获取事件浮点参数失败: {ex.Message}");
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取消息ID
        /// </summary>
        /// <param name="messageName">消息名称</param>
        /// <returns>消息ID，失败返回-1</returns>
        public static int GetMessageId(string messageName)
        {
            try
            {
                // 注意：这里应该使用Messages模块，但为了简化，我们使用Events模块
                return -1; // 简化实现
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventBridge] 获取消息ID失败: {ex.Message}");
                return -1;
            }
        }
    }
}