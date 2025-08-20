using System;
using AmxModx.Wrappers.System;
using AmxModx.Wrappers.Engine;
using AmxModx.Wrappers.Data;
using AmxModx.Wrappers.Communication;
using AmxModx.Bridge.Task;
using AmxModx.Bridge.Command;

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
        public static class Core
        {
            // 静态类访问
        }

        /// <summary>
        /// 本地函数管理器
        /// </summary>
        public static class Native
        {
            // 静态类访问
        }

        /// <summary>
        /// 命令管理器
        /// </summary>
        public static class Command
        {
            // 静态类访问
        }

        /// <summary>
        /// 任务管理器
        /// </summary>
        public static class Task
        {
            // 静态类访问
        }
    }

    /// <summary>
    /// 引擎模块包装类
    /// </summary>
    public class EngineModule
    {
        /// <summary>
        /// 引擎管理器
        /// </summary>
        public static class Engine
        {
            // 静态类访问
        }
    }

    /// <summary>
    /// 数据模块包装类
    /// </summary>
    public class DataModule
    {
        /// <summary>
        /// CVar管理器
        /// </summary>
        public static class CVar
        {
            // 静态类访问
        }
    }

    /// <summary>
    /// 通信模块包装类
    /// </summary>
    public class CommunicationModule
    {
        /// <summary>
        /// 事件管理器
        /// </summary>
        public static class Event
        {
            // 静态类访问
        }

        /// <summary>
        /// 转发管理器
        /// </summary>
        public static class Forward
        {
            // 静态类访问
        }
    }

    // 这些管理器类在其他文件中定义，这里不需要重复定义
}