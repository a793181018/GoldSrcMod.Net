using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Engine
{
    /// <summary>
    /// 引擎事件委托定义
    /// </summary>
    public static class EngineEventDelegates
    {
        /// <summary>
        /// 脉冲事件委托
        /// </summary>
        /// <param name="client">客户端ID</param>
        /// <param name="impulse">脉冲值</param>
        public delegate void ImpulseEventHandler(int client, int impulse);

        /// <summary>
        /// 触碰事件委托
        /// </summary>
        /// <param name="touched">被触碰的实体ID</param>
        /// <param name="toucher">触碰者实体ID</param>
        public delegate void TouchEventHandler(int touched, int toucher);

        /// <summary>
        /// 思考事件委托
        /// </summary>
        /// <param name="entity">实体ID</param>
        public delegate void ThinkEventHandler(int entity);

        /// <summary>
        /// 引擎事件管理器
        /// </summary>
        public static class EngineEventManager
        {
            private static ImpulseEventHandler? _impulseHandler;
            private static TouchEventHandler? _touchHandler;
            private static ThinkEventHandler? _thinkHandler;

            private static readonly ImpulseCallback _impulseCallback = OnImpulseCallback;
            private static readonly TouchCallback _touchCallback = OnTouchCallback;
            private static readonly ThinkCallback _thinkCallback = OnThinkCallback;

            /// <summary>
            /// 注册脉冲事件
            /// </summary>
            /// <param name="impulse">脉冲值</param>
            /// <param name="handler">事件处理器</param>
            /// <returns>注册ID，用于取消注册</returns>
            public static int RegisterImpulseEvent(int impulse, ImpulseEventHandler handler)
            {
                _impulseHandler = handler;
                return EngineBridge.RegisterImpulse(impulse, _impulseCallback);
            }

            /// <summary>
            /// 注册触碰事件
            /// </summary>
            /// <param name="touchedClass">被触碰的类名</param>
            /// <param name="toucherClass">触碰者的类名</param>
            /// <param name="handler">事件处理器</param>
            /// <returns>注册ID，用于取消注册</returns>
            public static int RegisterTouchEvent(string touchedClass, string toucherClass, TouchEventHandler handler)
            {
                _touchHandler = handler;
                return EngineBridge.RegisterTouch(touchedClass, toucherClass, _touchCallback);
            }

            /// <summary>
            /// 注册思考事件
            /// </summary>
            /// <param name="className">类名</param>
            /// <param name="handler">事件处理器</param>
            /// <returns>注册ID，用于取消注册</returns>
            public static int RegisterThinkEvent(string className, ThinkEventHandler handler)
            {
                _thinkHandler = handler;
                return EngineBridge.RegisterThink(className, _thinkCallback);
            }

            /// <summary>
            /// 取消注册脉冲事件
            /// </summary>
            /// <param name="registerId">注册ID</param>
            /// <returns>成功返回true，失败返回false</returns>
            public static bool UnregisterImpulseEvent(int registerId)
            {
                return EngineBridge.UnregisterImpulse(registerId) != 0;
            }

            /// <summary>
            /// 取消注册触碰事件
            /// </summary>
            /// <param name="registerId">注册ID</param>
            /// <returns>成功返回true，失败返回false</returns>
            public static bool UnregisterTouchEvent(int registerId)
            {
                return EngineBridge.UnregisterTouch(registerId) != 0;
            }

            /// <summary>
            /// 取消注册思考事件
            /// </summary>
            /// <param name="registerId">注册ID</param>
            /// <returns>成功返回true，失败返回false</returns>
            public static bool UnregisterThinkEvent(int registerId)
            {
                return EngineBridge.UnregisterThink(registerId) != 0;
            }

            private static void OnImpulseCallback(int client, int impulse)
            {
                _impulseHandler?.Invoke(client, impulse);
            }

            private static void OnTouchCallback(int touched, int toucher)
            {
                _touchHandler?.Invoke(touched, toucher);
            }

            private static void OnThinkCallback(int entity)
            {
                _thinkHandler?.Invoke(entity);
            }
        }
    }
}