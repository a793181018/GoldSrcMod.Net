using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Engine
{
    /// <summary>
    /// 引擎接口桥接定义
    /// 提供与GoldSrc引擎的交互功能
    /// </summary>
    public static class NativeMethods
    {
        private const string EngineDll = "engine_bridge";

        /// <summary>
        /// 获取服务器名称
        /// </summary>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="maxLength">缓冲区最大长度</param>
        /// <returns>实际复制的字符数</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetServerName([Out] byte[] buffer, int maxLength);

        /// <summary>
        /// 获取游戏时间
        /// </summary>
        /// <returns>当前游戏时间（秒）</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float AmxModx_Bridge_GetGameTime();

        /// <summary>
        /// 获取地图名称
        /// </summary>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="maxLength">缓冲区最大长度</param>
        /// <returns>实际复制的字符数</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMapName([Out] byte[] buffer, int maxLength);

        /// <summary>
        /// 获取最大玩家数
        /// </summary>
        /// <returns>服务器最大玩家数</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMaxPlayers();

        /// <summary>
        /// 获取当前玩家数
        /// </summary>
        /// <returns>当前在线玩家数</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetPlayerCount();

        /// <summary>
        /// 向客户端打印消息
        /// </summary>
        /// <param name="clientIndex">客户端索引</param>
        /// <param name="message">消息内容</param>
        /// <returns>操作是否成功</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_ClientPrint(int clientIndex, [MarshalAs(UnmanagedType.LPStr)] string message);

        /// <summary>
        /// 向所有客户端打印消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns>操作是否成功</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool AmxModx_Bridge_ClientPrintAll([MarshalAs(UnmanagedType.LPStr)] string message);

        /// <summary>
        /// 获取引擎版本
        /// </summary>
        /// <returns>引擎版本号</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetEngineVersion();

        /// <summary>
        /// 获取游戏目录
        /// </summary>
        /// <param name="buffer">输出缓冲区</param>
        /// <param name="maxLength">缓冲区最大长度</param>
        /// <returns>实际复制的字符数</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetGameDir([Out] byte[] buffer, int maxLength);

        /// <summary>
        /// 预缓存模型
        /// </summary>
        /// <param name="modelName">模型文件名</param>
        /// <returns>预缓存索引</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_PrecacheModel([MarshalAs(UnmanagedType.LPStr)] string modelName);

        /// <summary>
        /// 预缓存声音
        /// </summary>
        /// <param name="soundName">声音文件名</param>
        /// <returns>预缓存索引</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_PrecacheSound([MarshalAs(UnmanagedType.LPStr)] string soundName);

        /// <summary>
        /// 获取最大实体数
        /// </summary>
        /// <returns>最大实体数</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetMaxEntities();

        /// <summary>
        /// 获取当前实体数
        /// </summary>
        /// <returns>当前实体数</returns>
        [DllImport(EngineDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AmxModx_Bridge_GetEntityCount();
    }
}