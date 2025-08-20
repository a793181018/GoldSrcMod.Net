using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AmxModx.Bridge.Task;

namespace AmxModx.Bridge.Task
{
    /// <summary>
    /// 任务委托定义
    /// </summary>
    /// <param name="taskId">任务ID</param>
    /// <param name="parameters">参数数组</param>
    public delegate void TaskDelegate(int taskId, params object[] parameters);

    /// <summary>
    /// 任务信息类
    /// </summary>
    public class TaskInfo
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public int TaskId { get; set; }
        
        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public float Interval { get; set; }
        
        /// <summary>
        /// 重复次数（-1为无限）
        /// </summary>
        public int Repeat { get; set; }
        
        /// <summary>
        /// 任务标志
        /// </summary>
        public TaskFlags Flags { get; set; }
    }

    /// <summary>
    /// 任务管理器
    /// </summary>
    public static class TaskManager
    {
        private static int _currentPluginId = 1; // 默认插件ID
        private static readonly Dictionary<int, TaskDelegate> _taskDelegates = new Dictionary<int, TaskDelegate>();
        private static readonly Dictionary<int, object[]> _taskParameters = new Dictionary<int, object[]>();

        /// <summary>
        /// 初始化任务管理器
        /// </summary>
        public static void Initialize()
        {
            _currentPluginId = 1; // 默认插件ID
        }

        /// <summary>
        /// 清理任务管理器资源
        /// </summary>
        public static void Cleanup()
        {
            ClearAllTasks();
        }

        /// <summary>
        /// 设置当前插件ID
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        public static void SetCurrentPluginId(int pluginId)
        {
            _currentPluginId = pluginId;
        }

        /// <summary>
        /// 创建一次性任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="interval">执行间隔（秒）</param>
        /// <param name="callback">回调函数</param>
        /// <param name="parameters">参数</param>
        /// <returns>成功返回true</returns>
        public static bool CreateTask(int taskId, float interval, TaskDelegate callback, params object[] parameters)
        {
            return CreateTask(taskId, interval, 0, TaskFlags.None, callback, parameters);
        }

        /// <summary>
        /// 创建重复任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="interval">执行间隔（秒）</param>
        /// <param name="repeat">重复次数（-1为无限）</param>
        /// <param name="callback">回调函数</param>
        /// <param name="parameters">参数</param>
        /// <returns>成功返回true</returns>
        public static bool CreateRepeatTask(int taskId, float interval, int repeat, TaskDelegate callback, params object[] parameters)
        {
            return CreateTask(taskId, interval, repeat, TaskFlags.Repeat, callback, parameters);
        }

        /// <summary>
        /// 创建循环任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="interval">执行间隔（秒）</param>
        /// <param name="callback">回调函数</param>
        /// <param name="parameters">参数</param>
        /// <returns>成功返回true</returns>
        public static bool CreateLoopTask(int taskId, float interval, TaskDelegate callback, params object[] parameters)
        {
            return CreateTask(taskId, interval, -1, TaskFlags.Loop, callback, parameters);
        }

        /// <summary>
        /// 创建任务（完整版本）
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="interval">执行间隔（秒）</param>
        /// <param name="repeat">重复次数</param>
        /// <param name="flags">任务标志</param>
        /// <param name="callback">回调函数</param>
        /// <param name="parameters">参数</param>
        /// <returns>成功返回true</returns>
        public static bool CreateTask(int taskId, float interval, int repeat, TaskFlags flags, TaskDelegate callback, params object[] parameters)
        {
            if (callback == null)
                return false;

            // 存储委托和参数
            _taskDelegates[taskId] = callback;
            _taskParameters[taskId] = parameters;

            // 转换为cell数组
            int[] cellParams = null;
            int paramCount = 0;
            
            if (parameters != null && parameters.Length > 0)
            {
                cellParams = new int[parameters.Length];
                paramCount = parameters.Length;
                
                for (int i = 0; i < parameters.Length; i++)
                {
                    object param = parameters[i];
                    if (param is int intValue)
                        cellParams[i] = intValue;
                    else if (param is float floatValue)
                        cellParams[i] = (int)floatValue;
                    else if (param is bool boolValue)
                        cellParams[i] = boolValue ? 1 : 0;
                    else
                        cellParams[i] = 0;
                }
            }

            // 创建任务
            int result = TaskBridge.AmxModx_Bridge_CreateTask(
                _currentPluginId,
                taskId, // 使用taskId作为funcId
                (int)flags,
                taskId,
                interval,
                repeat,
                cellParams,
                paramCount);

            return result != 0;
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>移除的任务数量</returns>
        public static int RemoveTask(int taskId)
        {
            int count = TaskBridge.AmxModx_Bridge_RemoveTasks(_currentPluginId, taskId);
            
            if (count > 0)
            {
                _taskDelegates.Remove(taskId);
                _taskParameters.Remove(taskId);
            }
            
            return count;
        }

        /// <summary>
        /// 修改任务间隔
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="newInterval">新的间隔时间</param>
        /// <returns>修改的任务数量</returns>
        public static int ChangeTaskInterval(int taskId, float newInterval)
        {
            return TaskBridge.AmxModx_Bridge_ChangeTaskInterval(_currentPluginId, taskId, newInterval);
        }

        /// <summary>
        /// 检查任务是否存在
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>存在返回true</returns>
        public static bool TaskExists(int taskId)
        {
            return TaskBridge.AmxModx_Bridge_TaskExists(_currentPluginId, taskId);
        }

        /// <summary>
        /// 获取当前活动任务数量
        /// </summary>
        /// <returns>活动任务数量</returns>
        public static int GetActiveTaskCount()
        {
            return TaskBridge.AmxModx_Bridge_GetActiveTaskCount();
        }

        /// <summary>
        /// 获取任务信息
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>任务信息对象，不存在返回null</returns>
        public static TaskInfo GetTaskInfo(int taskId)
        {
            if (TaskBridge.AmxModx_Bridge_GetTaskInfo(_currentPluginId, taskId, 
                out float interval, out int repeat, out int flags))
            {
                return new TaskInfo
                {
                    TaskId = taskId,
                    Interval = interval,
                    Repeat = repeat,
                    Flags = (TaskFlags)flags
                };
            }
            return null;
        }

        /// <summary>
        /// 执行本地任务（供内部调用）
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="parameters">参数</param>
        internal static void ExecuteTask(int taskId, params object[] parameters)
        {
            if (_taskDelegates.TryGetValue(taskId, out TaskDelegate callback))
            {
                try
                {
                    callback(taskId, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"任务执行异常: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 清除所有任务
        /// </summary>
        public static void ClearAllTasks()
        {
            _taskDelegates.Clear();
            _taskParameters.Clear();
        }
    }
}