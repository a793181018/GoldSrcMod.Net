using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Task
{
    /// <summary>
    /// 任务标志位枚举
    /// </summary>
    [Flags]
    public enum TaskFlags
    {
        /// <summary>
        /// 重复执行
        /// </summary>
        Repeat = 1,
        
        /// <summary>
        /// 循环执行
        /// </summary>
        Loop = 2,
        
        /// <summary>
        /// 在地图开始后执行
        /// </summary>
        AfterStart = 4,
        
        /// <summary>
        /// 在地图结束前执行
        /// </summary>
        BeforeEnd = 8
    }

    /// <summary>
    /// 任务调度系统P/Invoke接口
    /// </summary>
    public static class TaskBridge
    {
        private const string DllName = "amxmodx_mm";

        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <param name="funcId">函数ID</param>
        /// <param name="flags">任务标志</param>
        /// <param name="taskId">任务ID</param>
        /// <param name="interval">执行间隔（秒）</param>
        /// <param name="repeat">重复次数（-1为无限）</param>
        /// <param name="params">参数数组</param>
        /// <param name="paramCount">参数数量</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_CreateTask(
            int pluginId, 
            int funcId, 
            int flags, 
            int taskId, 
            float interval, 
            int repeat, 
            [MarshalAs(UnmanagedType.LPArray)] int[] parameters, 
            int paramCount);

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <param name="taskId">任务ID</param>
        /// <returns>移除的任务数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_RemoveTasks(int pluginId, int taskId);

        /// <summary>
        /// 修改任务间隔
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <param name="taskId">任务ID</param>
        /// <param name="newInterval">新的间隔时间</param>
        /// <returns>修改的任务数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ChangeTaskInterval(
            int pluginId, 
            int taskId, 
            float newInterval);

        /// <summary>
        /// 检查任务是否存在
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <param name="taskId">任务ID</param>
        /// <returns>存在返回true，否则返回false</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_TaskExists(int pluginId, int taskId);

        /// <summary>
        /// 获取当前活动任务数量
        /// </summary>
        /// <returns>活动任务数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetActiveTaskCount();

        /// <summary>
        /// 获取任务信息
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <param name="taskId">任务ID</param>
        /// <param name="interval">返回间隔时间</param>
        /// <param name="repeat">返回重复次数</param>
        /// <param name="flags">返回任务标志</param>
        /// <returns>成功返回true，失败返回false</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AmxModx_Bridge_GetTaskInfo(
            int pluginId, 
            int taskId, 
            out float interval, 
            out int repeat, 
            out int flags);
    }
}