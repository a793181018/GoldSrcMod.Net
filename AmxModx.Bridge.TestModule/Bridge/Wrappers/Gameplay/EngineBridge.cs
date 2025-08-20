using System;
using System.Text;
using AmxModx.Bridge.Engine;

namespace AmxModx.Bridge.Wrappers.Gameplay
{
    /// <summary>
    /// 引擎桥接包装器
    /// 提供与GoldSrc引擎的高级交互接口
    /// </summary>
    public static class EngineBridge
    {
        private const int MaxBufferSize = 256;

        /// <summary>
        /// 获取服务器名称
        /// </summary>
        /// <returns>服务器名称</returns>
        public static string GetServerName()
        {
            try
            {
                byte[] buffer = new byte[MaxBufferSize];
                int length = NativeMethods.AmxModx_Bridge_GetServerName(buffer, MaxBufferSize);
                return length > 0 ? Encoding.ASCII.GetString(buffer, 0, length).TrimEnd('\0') : "Unknown";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 获取服务器名称失败: {ex.Message}");
                return "Error";
            }
        }

        /// <summary>
        /// 获取游戏时间
        /// </summary>
        /// <returns>当前游戏时间（秒）</returns>
        public static float GetGameTime()
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_GetGameTime();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 获取游戏时间失败: {ex.Message}");
                return 0.0f;
            }
        }

        /// <summary>
        /// 获取地图名称
        /// </summary>
        /// <returns>当前地图名称</returns>
        public static string GetMapName()
        {
            try
            {
                byte[] buffer = new byte[MaxBufferSize];
                int length = NativeMethods.AmxModx_Bridge_GetMapName(buffer, MaxBufferSize);
                return length > 0 ? Encoding.ASCII.GetString(buffer, 0, length).TrimEnd('\0') : "Unknown";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 获取地图名称失败: {ex.Message}");
                return "Error";
            }
        }

        /// <summary>
        /// 获取最大玩家数
        /// </summary>
        /// <returns>服务器最大玩家数</returns>
        public static int GetMaxPlayers()
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_GetMaxPlayers();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 获取最大玩家数失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 获取当前玩家数
        /// </summary>
        /// <returns>当前在线玩家数</returns>
        public static int GetPlayerCount()
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_GetPlayerCount();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 获取当前玩家数失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 向客户端打印消息
        /// </summary>
        /// <param name="clientIndex">客户端索引</param>
        /// <param name="message">消息内容</param>
        /// <returns>操作是否成功</returns>
        public static bool ClientPrint(int clientIndex, string message)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_ClientPrint(clientIndex, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 向客户端打印消息失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 向所有客户端打印消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns>操作是否成功</returns>
        public static bool ClientPrintAll(string message)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_ClientPrintAll(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 向所有客户端打印消息失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取引擎版本
        /// </summary>
        /// <returns>引擎版本号</returns>
        public static int GetEngineVersion()
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_GetEngineVersion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 获取引擎版本失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 获取游戏目录
        /// </summary>
        /// <returns>游戏目录路径</returns>
        public static string GetGameDir()
        {
            try
            {
                byte[] buffer = new byte[MaxBufferSize];
                int length = NativeMethods.AmxModx_Bridge_GetGameDir(buffer, MaxBufferSize);
                return length > 0 ? Encoding.ASCII.GetString(buffer, 0, length).TrimEnd('\0') : "Unknown";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 获取游戏目录失败: {ex.Message}");
                return "Error";
            }
        }

        /// <summary>
        /// 预缓存模型
        /// </summary>
        /// <param name="modelName">模型文件名</param>
        /// <returns>预缓存索引</returns>
        public static int PrecacheModel(string modelName)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_PrecacheModel(modelName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 预缓存模型失败: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// 预缓存声音
        /// </summary>
        /// <param name="soundName">声音文件名</param>
        /// <returns>预缓存索引</returns>
        public static int PrecacheSound(string soundName)
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_PrecacheSound(soundName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 预缓存声音失败: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// 获取最大实体数
        /// </summary>
        /// <returns>最大实体数</returns>
        public static int GetMaxEntities()
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_GetMaxEntities();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 获取最大实体数失败: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 获取当前实体数
        /// </summary>
        /// <returns>当前实体数</returns>
        public static int GetEntityCount()
        {
            try
            {
                return NativeMethods.AmxModx_Bridge_GetEntityCount();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EngineBridge] 获取当前实体数失败: {ex.Message}");
                return 0;
            }
        }
    }
}