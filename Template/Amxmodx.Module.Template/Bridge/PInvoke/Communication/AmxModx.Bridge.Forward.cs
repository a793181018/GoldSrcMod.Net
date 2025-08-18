using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge
{
    /// <summary>
    /// 转发系统桥接接口
    /// 提供AMX Mod X转发器的C#封装
    /// </summary>
    public static class Forward
    {
        /// <summary>
        /// 参数类型枚举
        /// </summary>
        public enum ForwardParamType
        {
            /// <summary>普通整数</summary>
            Cell = 0,
            /// <summary>浮点数</summary>
            Float = 1,
            /// <summary>字符串</summary>
            String = 2,
            /// <summary>可更新字符串</summary>
            StringEx = 3,
            /// <summary>数组</summary>
            Array = 4,
            /// <summary>整数引用</summary>
            CellByRef = 5,
            /// <summary>浮点数引用</summary>
            FloatByRef = 6
        }

        /// <summary>
        /// 执行类型枚举
        /// </summary>
        public enum ForwardExecType
        {
            /// <summary>忽略返回值</summary>
            Ignore = 0,
            /// <summary>遇到PLUGIN_HANDLED时停止</summary>
            Stop = 1,
            /// <summary>遇到PLUGIN_HANDLED时停止，其他值继续</summary>
            Stop2 = 2,
            /// <summary>继续执行，返回最大返回值</summary>
            Continue = 3
        }

        /// <summary>
        /// 创建多插件转发器
        /// </summary>
        /// <param name="funcName">转发器名称</param>
        /// <param name="execType">执行类型</param>
        /// <param name="paramTypes">参数类型数组</param>
        /// <returns>转发器ID，失败返回-1</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_CreateMultiForward(
            [MarshalAs(UnmanagedType.LPStr)] string funcName,
            ForwardExecType execType,
            int numParams,
            [In, MarshalAs(UnmanagedType.LPArray)] int[] paramTypes
        );

        /// <summary>
        /// 创建单插件转发器
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <param name="funcName">函数名称</param>
        /// <param name="paramTypes">参数类型数组</param>
        /// <returns>转发器ID，失败返回-1</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_CreateOneForward(
            int pluginId,
            [MarshalAs(UnmanagedType.LPStr)] string funcName,
            int numParams,
            [In, MarshalAs(UnmanagedType.LPArray)] int[] paramTypes
        );

        /// <summary>
        /// 销毁转发器
        /// </summary>
        /// <param name="forwardId">转发器ID</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_DestroyForward(int forwardId);

        /// <summary>
        /// 执行转发器
        /// </summary>
        /// <param name="forwardId">转发器ID</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>执行结果</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_ExecuteForward(
            int forwardId,
            [In, MarshalAs(UnmanagedType.LPArray)] int[] parameters,
            int numParams
        );

        /// <summary>
        /// 准备数组参数
        /// </summary>
        /// <param name="arrayData">数组数据</param>
        /// <param name="size">数组大小</param>
        /// <param name="copyBack">是否复制回数据</param>
        /// <returns>数组句柄</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_PrepareArray(
            [In, MarshalAs(UnmanagedType.LPArray)] int[] arrayData,
            int size,
            int copyBack
        );

        /// <summary>
        /// 获取转发器参数数量
        /// </summary>
        /// <param name="forwardId">转发器ID</param>
        /// <returns>参数数量，失败返回-1</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetForwardParamCount(int forwardId);

        /// <summary>
        /// 获取转发器参数类型
        /// </summary>
        /// <param name="forwardId">转发器ID</param>
        /// <param name="paramIndex">参数索引</param>
        /// <returns>参数类型，失败返回-1</returns>
        [DllImport("amxmodx_mm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetForwardParamType(int forwardId, int paramIndex);

        /// <summary>
        /// 创建多插件转发器（简化版）
        /// </summary>
        /// <param name="funcName">转发器名称</param>
        /// <param name="execType">执行类型</param>
        /// <param name="paramTypes">参数类型数组</param>
        /// <returns>转发器ID</returns>
        public static int CreateMultiForward(string funcName, ForwardExecType execType, params ForwardParamType[] paramTypes)
        {
            int[] types = Array.ConvertAll(paramTypes, x => (int)x);
            return AmxModx_Bridge_CreateMultiForward(funcName, execType, types.Length, types);
        }

        /// <summary>
        /// 创建单插件转发器（简化版）
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <param name="funcName">函数名称</param>
        /// <param name="paramTypes">参数类型数组</param>
        /// <returns>转发器ID</returns>
        public static int CreateOneForward(int pluginId, string funcName, params ForwardParamType[] paramTypes)
        {
            int[] types = Array.ConvertAll(paramTypes, x => (int)x);
            return AmxModx_Bridge_CreateOneForward(pluginId, funcName, types.Length, types);
        }

        /// <summary>
        /// 执行转发器（简化版）
        /// </summary>
        /// <param name="forwardId">转发器ID</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>执行结果</returns>
        public static int ExecuteForward(int forwardId, params int[] parameters)
        {
            return AmxModx_Bridge_ExecuteForward(forwardId, parameters, parameters.Length);
        }

        /// <summary>
        /// 准备数组参数（简化版）
        /// </summary>
        /// <param name="arrayData">数组数据</param>
        /// <param name="copyBack">是否复制回数据</param>
        /// <returns>数组句柄</returns>
        public static int PrepareArray(int[] arrayData, bool copyBack = false)
        {
            return AmxModx_Bridge_PrepareArray(arrayData, arrayData.Length, copyBack ? 1 : 0);
        }

        /// <summary>
        /// 委托定义：转发回调函数
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <returns>执行结果</returns>
        public delegate int ForwardCallback(params int[] parameters);

        /// <summary>
        /// 事件管理器类
        /// </summary>
        public static class ForwardManager
        {
            /// <summary>
            /// 已注册的转发器字典
            /// </summary>
            private static System.Collections.Generic.Dictionary<string, int> forwards = 
                new System.Collections.Generic.Dictionary<string, int>();

            /// <summary>
            /// 注册全局转发器
            /// </summary>
            /// <param name="name">转发器名称</param>
            /// <param name="execType">执行类型</param>
            /// <param name="paramTypes">参数类型</param>
            /// <returns>转发器ID</returns>
            public static int RegisterForward(string name, ForwardExecType execType, params ForwardParamType[] paramTypes)
            {
                if (forwards.ContainsKey(name))
                    return forwards[name];

                int forwardId = CreateMultiForward(name, execType, paramTypes);
                if (forwardId >= 0)
                    forwards[name] = forwardId;

                return forwardId;
            }

            /// <summary>
            /// 获取转发器ID
            /// </summary>
            /// <param name="name">转发器名称</param>
            /// <returns>转发器ID，不存在返回-1</returns>
            public static int GetForward(string name)
            {
                return forwards.ContainsKey(name) ? forwards[name] : -1;
            }

            /// <summary>
            /// 执行已注册的转发器
            /// </summary>
            /// <param name="name">转发器名称</param>
            /// <param name="parameters">参数</param>
            /// <returns>执行结果</returns>
            public static int ExecuteRegisteredForward(string name, params int[] parameters)
            {
                if (!forwards.ContainsKey(name))
                    return 0;

                return ExecuteForward(forwards[name], parameters);
            }

            /// <summary>
            /// 清理所有转发器
            /// </summary>
            public static void Clear()
            {
                foreach (var forwardId in forwards.Values)
                {
                    DestroyForward(forwardId);
                }
                forwards.Clear();
            }
        }
    }
}