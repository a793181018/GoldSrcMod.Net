using System;
using AmxModx.Wrappers.System;
using AmxModx.Wrappers.Engine;
using AmxModx.Wrappers.Data;
using AmxModx.Wrappers.Communication;

namespace AmxModx.Wrappers
{
    /// <summary>
    /// AMX Mod X统一入口管理器
    /// 提供对所有模块高级封装的统一访问
    /// </summary>
    public static class AmxModxManager
    {
        /// <summary>
        /// 初始化所有模块
        /// </summary>
        public static void Initialize()
        {
            // 初始化系统模块
            CoreManager.Initialize();
            NativeManager.Initialize();
            CommandManager.Initialize();
            TaskManager.Initialize();

            // 初始化引擎模块
            EngineManager.Initialize();

            // 初始化数据模块
            CVarManager.Initialize();

            // 初始化通信模块
            EventManager.Initialize();
            ForwardManager.Initialize();
        }

        /// <summary>
        /// 清理所有模块
        /// </summary>
        public static void Cleanup()
        {
            // 清理通信模块
            ForwardManager.ClearAllForwards();
            EventManager.ClearAllEvents();

            // 清理数据模块
            CVarManager.Cleanup();

            // 清理引擎模块
            EngineManager.Cleanup();

            // 清理系统模块
            TaskManager.Cleanup();
            CommandManager.Cleanup();
            NativeManager.Cleanup();
            CoreManager.Cleanup();
        }

        /// <summary>
        /// 获取系统模块
        /// </summary>
        public static SystemModule System => new SystemModule();

        /// <summary>
        /// 获取引擎模块
        /// </summary>
        public static EngineModule Engine => new EngineModule();

        /// <summary>
        /// 获取数据模块
        /// </summary>
        public static DataModule Data => new DataModule();

        /// <summary>
        /// 获取通信模块
        /// </summary>
        public static CommunicationModule Communication => new CommunicationModule();
    }

    /// <summary>
    /// 系统模块包装类
    /// </summary>
    public class SystemModule
    {
        /// <summary>
        /// 核心管理器
        /// </summary>
        public CoreManager Core => CoreManager.Instance;

        /// <summary>
        /// 本地函数管理器
        /// </summary>
        public NativeManager Native => NativeManager.Instance;

        /// <summary>
        /// 命令管理器
        /// </summary>
        public CommandManager Command => CommandManager.Instance;

        /// <summary>
        /// 任务管理器
        /// </summary>
        public TaskManager Task => TaskManager.Instance;
    }

    /// <summary>
    /// 引擎模块包装类
    /// </summary>
    public class EngineModule
    {
        /// <summary>
        /// 引擎管理器
        /// </summary>
        public EngineManager Engine => EngineManager.Instance;
    }

    /// <summary>
    /// 数据模块包装类
    /// </summary>
    public class DataModule
    {
        /// <summary>
        /// CVar管理器
        /// </summary>
        public CVarManager CVar => CVarManager.Instance;
    }

    /// <summary>
    /// 通信模块包装类
    /// </summary>
    public class CommunicationModule
    {
        /// <summary>
        /// 事件管理器
        /// </summary>
        public EventManager Event => EventManager.Instance;

        /// <summary>
        /// 转发管理器
        /// </summary>
        public ForwardManager Forward => ForwardManager.Instance;
    }

    // 为静态类添加实例属性以便统一访问
    public static class CoreManager
    {
        public static CoreManager Instance => null; // 返回null因为CoreManager是静态类
        public static void Initialize() { }
        public static void Cleanup() { }
    }

    public static class NativeManager
    {
        public static NativeManager Instance => null; // 返回null因为NativeManager是静态类
        public static void Initialize() { }
        public static void Cleanup() { }
    }

    public static class EngineManager
    {
        public static EngineManager Instance => null; // 返回null因为EngineManager是静态类
        public static void Initialize() { }
        public static void Cleanup() { }
    }

    public static class CVarManager
    {
        public static CVarManager Instance => null; // 返回null因为CVarManager是静态类
        public static void Initialize() { }
        public static void Cleanup() { }
    }

    public static class EventManager
    {
        public static EventManager Instance => null; // 返回null因为EventManager是静态类
        public static void Initialize() { }
        public static void Cleanup() { }
    }

    public static class ForwardManager
    {
        public static ForwardManager Instance => null; // 返回null因为ForwardManager是静态类
        public static void Initialize() { }
        public static void Cleanup() { }
    }
}