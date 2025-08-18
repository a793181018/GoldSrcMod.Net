using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Fakemeta
{
    /// <summary>
    /// Fakemeta事件管理器的高级封装
    /// </summary>
    public static class FakemetaEvents
    {
        private static readonly Dictionary<int, ForwardCallback> _callbacks = new Dictionary<int, ForwardCallback>();
        private static readonly Dictionary<int, EventHandlerRegistration> _registrations = new Dictionary<int, EventHandlerRegistration>();

        /// <summary>
        /// 事件处理器注册信息
        /// </summary>
        private class EventHandlerRegistration
        {
            public ForwardType Type { get; set; }
            public ForwardTiming Timing { get; set; }
            public Delegate Handler { get; set; }
            public int Handle { get; set; }
        }

        /// <summary>
        /// 实体事件处理器委托
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <returns>处理结果</returns>
        public delegate ForwardResult EntityEventHandler(int entityIndex);

        /// <summary>
        /// 字符串事件处理器委托
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>处理结果</returns>
        public delegate ForwardResult StringEventHandler(string value);

        /// <summary>
        /// 实体字符串事件处理器委托
        /// </summary>
        /// <param name="entityIndex">实体索引</param>
        /// <param name="value">字符串值</param>
        /// <returns>处理结果</returns>
        public delegate ForwardResult EntityStringEventHandler(int entityIndex, string value);

        /// <summary>
        /// 追踪事件处理器委托
        /// </summary>
        /// <param name="startPos">起始位置</param>
        /// <param name="endPos">结束位置</param>
        /// <param name="noMonsters">是否忽略怪物</param>
        /// <param name="skipEntity">跳过的实体</param>
        /// <returns>处理结果</returns>
        public delegate ForwardResult TraceEventHandler(float[] startPos, float[] endPos, int noMonsters, int skipEntity);

        /// <summary>
        /// 注册实体事件处理器
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="handler">事件处理器</param>
        /// <returns>注册句柄</returns>
        public static int RegisterEntityEvent(ForwardType type, ForwardTiming timing, EntityEventHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            ForwardCallback callback = (data) =>
            {
                var args = Marshal.PtrToStructure<EntityForwardArgs>(data);
                return handler(args.EntityIndex);
            };

            int handle = ForwardManager.RegisterForward(type, timing, callback);
            if (handle > 0)
            {
                _callbacks[handle] = callback;
                _registrations[handle] = new EventHandlerRegistration
                {
                    Type = type,
                    Timing = timing,
                    Handler = handler,
                    Handle = handle
                };
            }
            return handle;
        }

        /// <summary>
        /// 注册字符串事件处理器
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="handler">事件处理器</param>
        /// <returns>注册句柄</returns>
        public static int RegisterStringEvent(ForwardType type, ForwardTiming timing, StringEventHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            ForwardCallback callback = (data) =>
            {
                var args = Marshal.PtrToStructure<StringForwardArgs>(data);
                return handler(args.StringValue);
            };

            int handle = ForwardManager.RegisterForward(type, timing, callback);
            if (handle > 0)
            {
                _callbacks[handle] = callback;
                _registrations[handle] = new EventHandlerRegistration
                {
                    Type = type,
                    Timing = timing,
                    Handler = handler,
                    Handle = handle
                };
            }
            return handle;
        }

        /// <summary>
        /// 注册实体字符串事件处理器
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="handler">事件处理器</param>
        /// <returns>注册句柄</returns>
        public static int RegisterEntityStringEvent(ForwardType type, ForwardTiming timing, EntityStringEventHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            ForwardCallback callback = (data) =>
            {
                var args = Marshal.PtrToStructure<EntityStringForwardArgs>(data);
                return handler(args.EntityIndex, args.StringValue);
            };

            int handle = ForwardManager.RegisterForward(type, timing, callback);
            if (handle > 0)
            {
                _callbacks[handle] = callback;
                _registrations[handle] = new EventHandlerRegistration
                {
                    Type = type,
                    Timing = timing,
                    Handler = handler,
                    Handle = handle
                };
            }
            return handle;
        }

        /// <summary>
        /// 注册追踪事件处理器
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="handler">事件处理器</param>
        /// <returns>注册句柄</returns>
        public static int RegisterTraceEvent(ForwardType type, ForwardTiming timing, TraceEventHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            ForwardCallback callback = (data) =>
            {
                var args = Marshal.PtrToStructure<TraceForwardArgs>(data);
                return handler(args.StartPos, args.EndPos, args.NoMonsters, args.SkipEntity);
            };

            int handle = ForwardManager.RegisterForward(type, timing, callback);
            if (handle > 0)
            {
                _callbacks[handle] = callback;
                _registrations[handle] = new EventHandlerRegistration
                {
                    Type = type,
                    Timing = timing,
                    Handler = handler,
                    Handle = handle
                };
            }
            return handle;
        }

        /// <summary>
        /// 注销事件处理器
        /// </summary>
        /// <param name="handle">注册句柄</param>
        /// <returns>是否成功</returns>
        public static bool UnregisterEvent(int handle)
        {
            if (_registrations.TryGetValue(handle, out var registration))
            {
                bool result = ForwardManager.UnregisterForward(registration.Type, registration.Timing, handle);
                if (result)
                {
                    _registrations.Remove(handle);
                    _callbacks.Remove(handle);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 获取指定类型的事件数量
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <returns>事件数量</returns>
        public static int GetEventCount(ForwardType type, ForwardTiming timing)
        {
            return ForwardManager.GetForwardCount(type, timing);
        }

        /// <summary>
        /// 检查事件处理器是否已注册
        /// </summary>
        /// <param name="handle">注册句柄</param>
        /// <returns>是否已注册</returns>
        public static bool IsEventRegistered(int handle)
        {
            return _registrations.ContainsKey(handle);
        }

        /// <summary>
        /// 清理所有注册的事件处理器
        /// </summary>
        public static void UnregisterAllEvents()
        {
            var handles = new List<int>(_registrations.Keys);
            foreach (var handle in handles)
            {
                UnregisterEvent(handle);
            }
        }

        /// <summary>
        /// 常用的快捷注册方法
        /// </summary>
        public static class CommonEvents
        {
            /// <summary>
            /// 注册实体生成事件
            /// </summary>
            public static int OnEntitySpawn(EntityEventHandler handler, ForwardTiming timing = ForwardTiming.Pre)
            {
                return RegisterEntityEvent(ForwardType.Spawn, timing, handler);
            }

            /// <summary>
            /// 注册实体移除事件
            /// </summary>
            public static int OnEntityRemove(EntityEventHandler handler, ForwardTiming timing = ForwardTiming.Pre)
            {
                return RegisterEntityEvent(ForwardType.RemoveEntity, timing, handler);
            }

            /// <summary>
            /// 注册模型设置事件
            /// </summary>
            public static int OnSetModel(EntityStringEventHandler handler, ForwardTiming timing = ForwardTiming.Pre)
            {
                return RegisterEntityStringEvent(ForwardType.SetModel, timing, handler);
            }

            /// <summary>
            /// 注册客户端连接事件
            /// </summary>
            public static int OnClientConnect(EntityEventHandler handler, ForwardTiming timing = ForwardTiming.Pre)
            {
                return RegisterEntityEvent(ForwardType.ClientConnect, timing, handler);
            }

            /// <summary>
            /// 注册追踪线事件
            /// </summary>
            public static int OnTraceLine(TraceEventHandler handler, ForwardTiming timing = ForwardTiming.Pre)
            {
                return RegisterTraceEvent(ForwardType.TraceLine, timing, handler);
            }
        }
    }
}