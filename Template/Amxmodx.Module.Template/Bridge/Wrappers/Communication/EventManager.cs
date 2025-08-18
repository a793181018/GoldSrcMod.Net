using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AmxModx.Bridge.Event;

namespace AmxModx.Wrappers.Communication
{
    /// <summary>
    /// 事件管理器高级封装
    /// 提供对AMX Mod X事件系统的高级封装
    /// </summary>
    public static class EventManager
    {
        /// <summary>
        /// 事件回调委托
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="args">事件参数</param>
        public delegate void EventCallback(string eventName, Dictionary<string, object> args);

        private static readonly Dictionary<string, List<EventCallback>> _eventCallbacks = new Dictionary<string, List<EventCallback>>();

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="callback">回调函数</param>
        /// <param name="post">是否为后置事件</param>
        public static void RegisterEvent(string eventName, EventCallback callback, bool post = false)
        {
            if (string.IsNullOrEmpty(eventName) || callback == null)
                return;

            if (!_eventCallbacks.ContainsKey(eventName))
                _eventCallbacks[eventName] = new List<EventCallback>();

            _eventCallbacks[eventName].Add(callback);

            // 注册到AMX Mod X事件系统
            EventBridge.AmxModx_Bridge_RegisterEvent(eventName, post ? 1 : 0);
        }

        /// <summary>
        /// 取消注册事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="callback">回调函数</param>
        public static void UnregisterEvent(string eventName, EventCallback callback)
        {
            if (string.IsNullOrEmpty(eventName) || callback == null)
                return;

            if (_eventCallbacks.ContainsKey(eventName))
            {
                _eventCallbacks[eventName].Remove(callback);
                if (_eventCallbacks[eventName].Count == 0)
                    _eventCallbacks.Remove(eventName);
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="args">事件参数</param>
        public static void FireEvent(string eventName, Dictionary<string, object> args = null)
        {
            if (string.IsNullOrEmpty(eventName))
                return;

            if (_eventCallbacks.ContainsKey(eventName))
            {
                foreach (var callback in _eventCallbacks[eventName])
                {
                    try
                    {
                        callback(eventName, args ?? new Dictionary<string, object>());
                    }
                    catch (Exception ex)
                    {
                        // 记录异常但不中断其他回调
                        Console.WriteLine($"Event callback error: {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 获取事件列表
        /// </summary>
        /// <returns>事件名称数组</returns>
        public static string[] GetEventList()
        {
            // 这里简化处理，实际应该从AMX Mod X获取完整列表
            return new string[]
            {
                "player_death",
                "player_spawn",
                "player_connect",
                "player_disconnect",
                "round_start",
                "round_end",
                "bomb_planted",
                "bomb_defused",
                "hostage_rescued",
                "vip_escaped"
            };
        }

        /// <summary>
        /// 检查事件是否存在
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public static bool EventExists(string eventName)
        {
            if (string.IsNullOrEmpty(eventName))
                return false;

            var events = GetEventList();
            return Array.IndexOf(events, eventName.ToLower()) >= 0;
        }

        /// <summary>
        /// 清除所有事件注册
        /// </summary>
        public static void ClearAllEvents()
        {
            _eventCallbacks.Clear();
        }

        /// <summary>
        /// 获取已注册的事件
        /// </summary>
        /// <returns>事件名称数组</returns>
        public static string[] GetRegisteredEvents()
        {
            var keys = new string[_eventCallbacks.Count];
            _eventCallbacks.Keys.CopyTo(keys, 0);
            return keys;
        }

        /// <summary>
        /// 获取事件回调数量
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns>回调数量</returns>
        public static int GetEventCallbackCount(string eventName)
        {
            if (string.IsNullOrEmpty(eventName))
                return 0;

            return _eventCallbacks.ContainsKey(eventName) ? _eventCallbacks[eventName].Count : 0;
        }
    }
}