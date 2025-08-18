using System;
using System.Runtime.InteropServices;
using AmxModx.Bridge.Engine;

namespace AmxModx.Bridge.Engine
{
    /// <summary>
    /// EngineBridge扩展类，提供额外的实体操作方法
    /// </summary>
    public static class EngineBridgeExtensions
    {
        private const string EngineBridgeDll = "engine_amxx";

        #region 实体位置相关

        /// <summary>
        /// 获取实体位置
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="origin">返回的位置</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetEntityOrigin(int entityId, [Out] float[] origin);

        /// <summary>
        /// 设置实体位置
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="origin">新位置</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_SetEntityOrigin(int entityId, [In] float[] origin);

        /// <summary>
        /// 获取实体角度
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="angles">返回的角度</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetEntityAngles(int entityId, [Out] float[] angles);

        /// <summary>
        /// 设置实体角度
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="angles">新角度</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_SetEntityAngles(int entityId, [In] float[] angles);

        /// <summary>
        /// 获取实体速度
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="velocity">返回的速度</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetEntityVelocity(int entityId, [Out] float[] velocity);

        /// <summary>
        /// 设置实体速度
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="velocity">新速度</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_SetEntityVelocity(int entityId, [In] float[] velocity);

        #endregion

        #region 实体属性相关

        /// <summary>
        /// 获取实体类名
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="buffer">缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>实际返回的字符串长度</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetEntityClassName(int entityId, [Out] byte[] buffer, int bufferSize);

        /// <summary>
        /// 获取实体模型名
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="buffer">缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>实际返回的字符串长度</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetEntityModelName(int entityId, [Out] byte[] buffer, int bufferSize);

        /// <summary>
        /// 获取实体健康值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>健康值</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetEntityHealth(int entityId);

        /// <summary>
        /// 设置实体健康值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="health">健康值</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_SetEntityHealth(int entityId, int health);

        /// <summary>
        /// 获取实体护甲值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>护甲值</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetEntityArmor(int entityId);

        /// <summary>
        /// 设置实体护甲值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="armor">护甲值</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_SetEntityArmor(int entityId, int armor);

        #endregion

        #region 实体列表相关

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="maxCount">最大实体数量</param>
        /// <param name="entityIds">返回的实体ID数组</param>
        /// <returns>实际获取的实体数量</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetAllEntities([Out] int[] entityIds, int maxCount);

        /// <summary>
        /// 按类名查找实体
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="maxCount">最大返回数量</param>
        /// <param name="entityIds">返回的实体ID数组</param>
        /// <returns>实际找到的实体数量</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_FindEntitiesByClass(string className, [Out] int[] entityIds, int maxCount);

        #endregion

        #region 便捷包装方法

        /// <summary>
        /// 获取实体位置（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="origin">返回的位置</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool GetEntityOrigin(int entityId, out EngineBridge.Vector3 origin)
        {
            float[] originArray = new float[3];
            bool result = Engine_GetEntityOrigin(entityId, originArray) != 0;
            if (result)
            {
                origin = new EngineBridge.Vector3(originArray[0], originArray[1], originArray[2]);
            }
            else
            {
                origin = new EngineBridge.Vector3(0, 0, 0);
            }
            return result;
        }

        /// <summary>
        /// 设置实体位置（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="origin">新位置</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetEntityOrigin(int entityId, EngineBridge.Vector3 origin)
        {
            float[] originArray = { origin.X, origin.Y, origin.Z };
            return Engine_SetEntityOrigin(entityId, originArray) != 0;
        }

        /// <summary>
        /// 获取实体角度（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="angles">返回的角度</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool GetEntityAngles(int entityId, out EngineBridge.Vector3 angles)
        {
            float[] anglesArray = new float[3];
            bool result = Engine_GetEntityAngles(entityId, anglesArray) != 0;
            if (result)
            {
                angles = new EngineBridge.Vector3(anglesArray[0], anglesArray[1], anglesArray[2]);
            }
            else
            {
                angles = new EngineBridge.Vector3(0, 0, 0);
            }
            return result;
        }

        /// <summary>
        /// 设置实体角度（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="angles">新角度</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetEntityAngles(int entityId, EngineBridge.Vector3 angles)
        {
            float[] anglesArray = { angles.X, angles.Y, angles.Z };
            return Engine_SetEntityAngles(entityId, anglesArray) != 0;
        }

        /// <summary>
        /// 获取实体速度（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="velocity">返回的速度</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool GetEntityVelocity(int entityId, out EngineBridge.Vector3 velocity)
        {
            float[] velocityArray = new float[3];
            bool result = Engine_GetEntityVelocity(entityId, velocityArray) != 0;
            if (result)
            {
                velocity = new EngineBridge.Vector3(velocityArray[0], velocityArray[1], velocityArray[2]);
            }
            else
            {
                velocity = new EngineBridge.Vector3(0, 0, 0);
            }
            return result;
        }

        /// <summary>
        /// 设置实体速度（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="velocity">新速度</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetEntityVelocity(int entityId, EngineBridge.Vector3 velocity)
        {
            float[] velocityArray = { velocity.X, velocity.Y, velocity.Z };
            return Engine_SetEntityVelocity(entityId, velocityArray) != 0;
        }

        /// <summary>
        /// 获取实体类名（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>实体类名</returns>
        public static string GetEntityClassName(int entityId)
        {
            const int bufferSize = 256;
            byte[] buffer = new byte[bufferSize];
            int length = Engine_GetEntityClassName(entityId, buffer, bufferSize);
            if (length <= 0) return string.Empty;
            
            return System.Text.Encoding.UTF8.GetString(buffer, 0, length);
        }

        /// <summary>
        /// 获取实体模型名（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>实体模型名</returns>
        public static string GetEntityModelName(int entityId)
        {
            const int bufferSize = 256;
            byte[] buffer = new byte[bufferSize];
            int length = Engine_GetEntityModelName(entityId, buffer, bufferSize);
            if (length <= 0) return string.Empty;
            
            return System.Text.Encoding.UTF8.GetString(buffer, 0, length);
        }

        /// <summary>
        /// 获取实体健康值（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>健康值</returns>
        public static int GetEntityHealth(int entityId)
        {
            return Engine_GetEntityHealth(entityId);
        }

        /// <summary>
        /// 设置实体健康值（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="health">健康值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetEntityHealth(int entityId, int health)
        {
            return Engine_SetEntityHealth(entityId, health) != 0;
        }

        /// <summary>
        /// 获取实体护甲值（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>护甲值</returns>
        public static int GetEntityArmor(int entityId)
        {
            return Engine_GetEntityArmor(entityId);
        }

        /// <summary>
        /// 设置实体护甲值（便捷包装）
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="armor">护甲值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetEntityArmor(int entityId, int armor)
        {
            return Engine_SetEntityArmor(entityId, armor) != 0;
        }

        /// <summary>
        /// 获取所有实体（便捷包装）
        /// </summary>
        /// <param name="maxCount">最大实体数量</param>
        /// <returns>实体ID数组</returns>
        public static int[] GetAllEntities(int maxCount)
        {
            if (maxCount <= 0)
                return new int[0];

            int[] entityIds = new int[maxCount];
            int actualCount = Engine_GetAllEntities(entityIds, maxCount);
            
            if (actualCount <= 0)
                return new int[0];

            // 调整数组大小到实际数量
            int[] result = new int[actualCount];
            Array.Copy(entityIds, result, actualCount);
            return result;
        }

        /// <summary>
        /// 按类名查找实体（便捷包装）
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns>实体ID数组</returns>
        public static int[] FindEntitiesByClass(string className)
        {
            if (string.IsNullOrEmpty(className))
                return new int[0];

            // 假设最大返回1000个实体
            const int maxCount = 1000;
            int[] entityIds = new int[maxCount];
            int actualCount = Engine_FindEntitiesByClass(className, entityIds, maxCount);
            
            if (actualCount <= 0)
                return new int[0];

            // 调整数组大小到实际数量
            int[] result = new int[actualCount];
            Array.Copy(entityIds, result, actualCount);
            return result;
        }

        #endregion
    }
}