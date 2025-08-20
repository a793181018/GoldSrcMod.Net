using System;
using System.Runtime.InteropServices;
using AmxModx.Bridge.Engine;

namespace AmxModx.Wrappers.Engine
{
    /// <summary>
    /// 引擎管理器高级封装
    /// 提供对AMX Mod X引擎功能的高级封装
    /// </summary>
    public static class EngineManager
    {
        /// <summary>
        /// 初始化引擎管理器
        /// </summary>
        public static void Initialize()
        {
            // 初始化引擎系统
        }

        /// <summary>
        /// 清理引擎管理器资源
        /// </summary>
        public static void Cleanup()
        {
            // 清理引擎系统资源
        }
        /// <summary>
        /// 获取当前游戏时间（秒）
        /// </summary>
        public static float GameTime => EngineBridge.Engine_GetGameTime();

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="className">实体类名</param>
        /// <returns>实体ID，失败返回0</returns>
        public static int CreateEntity(string className)
        {
            if (string.IsNullOrEmpty(className))
                return 0;

            return EngineBridge.Engine_CreateEntity(className);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool RemoveEntity(int entityId)
        {
            if (entityId <= 0)
                return false;

            return EngineBridge.Engine_RemoveEntity(entityId) == 1;
        }

        /// <summary>
        /// 检查实体是否有效
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>有效返回true，无效返回false</returns>
        public static bool IsValidEntity(int entityId)
        {
            if (entityId <= 0)
                return false;

            return EngineBridge.Engine_IsValidEntity(entityId) == 1;
        }

        /// <summary>
        /// 获取当前实体数量
        /// </summary>
        public static int EntityCount => EngineBridge.Engine_GetEntityCount();

        /// <summary>
        /// 计算两个实体间的距离
        /// </summary>
        /// <param name="entityA">实体A</param>
        /// <param name="entityB">实体B</param>
        /// <returns>距离值，实体无效返回-1</returns>
        public static float GetEntityDistance(int entityA, int entityB)
        {
            if (!IsValidEntity(entityA) || !IsValidEntity(entityB))
                return -1f;

            return EngineBridge.Engine_GetEntityDistance(entityA, entityB);
        }

        /// <summary>
        /// 执行线条追踪
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="ignoreEntity">忽略的实体ID（0表示不忽略）</param>
        /// <returns>追踪结果</returns>
        public static TraceResult TraceLine(Vector3 start, Vector3 end, int ignoreEntity = 0)
        {
            TraceResultInfo result;
            bool success = EngineBridge.Engine_TraceLine(start, end, ignoreEntity, out result) != 0;
            return new TraceResult(result, success);
        }

        /// <summary>
        /// 执行包围盒追踪
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="hullType">包围盒类型</param>
        /// <param name="ignoreEntity">忽略的实体ID</param>
        /// <returns>追踪结果</returns>
        public static TraceResult TraceHull(Vector3 start, Vector3 end, int hullType, int ignoreEntity = 0)
        {
            TraceResultInfo result;
            bool success = EngineBridge.Engine_TraceHull(start, end, hullType, ignoreEntity, out result) != 0;
            return new TraceResult(result, success);
        }

        /// <summary>
        /// 获取实体位置
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>实体位置</returns>
        public static Vector3 GetEntityOrigin(int entityId)
        {
            Vector3 origin;
            if (EngineBridgeExtensions.GetEntityOrigin(entityId, out origin))
            {
                return origin;
            }
            return new Vector3(0, 0, 0);
        }

        /// <summary>
        /// 设置实体位置
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="origin">新位置</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetEntityOrigin(int entityId, Vector3 origin)
        {
            if (!IsValidEntity(entityId))
                return false;

            return EngineBridgeExtensions.SetEntityOrigin(entityId, origin);
        }

        /// <summary>
        /// 获取实体角度
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>实体角度</returns>
        public static Vector3 GetEntityAngles(int entityId)
        {
            if (!IsValidEntity(entityId))
                return new Vector3(0, 0, 0);

            Vector3 angles;
            if (EngineBridgeExtensions.GetEntityAngles(entityId, out angles))
                return angles;

            return new Vector3(0, 0, 0);
        }

        /// <summary>
        /// 设置实体角度
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="angles">新角度</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetEntityAngles(int entityId, Vector3 angles)
        {
            if (!IsValidEntity(entityId))
                return false;

            return EngineBridgeExtensions.SetEntityAngles(entityId, angles);
        }

        /// <summary>
        /// 获取实体速度
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>实体速度</returns>
        public static Vector3 GetEntityVelocity(int entityId)
        {
            if (!IsValidEntity(entityId))
                return new Vector3(0, 0, 0);

            Vector3 velocity;
            if (EngineBridgeExtensions.GetEntityVelocity(entityId, out velocity))
                return velocity;

            return new Vector3(0, 0, 0);
        }

        /// <summary>
        /// 设置实体速度
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="velocity">新速度</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetEntityVelocity(int entityId, Vector3 velocity)
        {
            if (!IsValidEntity(entityId))
                return false;

            return EngineBridgeExtensions.SetEntityVelocity(entityId, velocity);
        }

        /// <summary>
        /// 获取实体类名
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>实体类名</returns>
        public static string GetEntityClassName(int entityId)
        {
            if (!IsValidEntity(entityId))
                return string.Empty;

            return EngineBridgeExtensions.GetEntityClassName(entityId);
        }

        /// <summary>
        /// 获取实体模型名
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>实体模型名</returns>
        public static string GetEntityModelName(int entityId)
        {
            if (!IsValidEntity(entityId))
                return string.Empty;

            return EngineBridgeExtensions.GetEntityModelName(entityId);
        }

        /// <summary>
        /// 获取实体健康值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>健康值</returns>
        public static int GetEntityHealth(int entityId)
        {
            if (!IsValidEntity(entityId))
                return 0;

            return EngineBridgeExtensions.GetEntityHealth(entityId);
        }

        /// <summary>
        /// 设置实体健康值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="health">健康值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetEntityHealth(int entityId, int health)
        {
            if (!IsValidEntity(entityId) || health < 0)
                return false;

            return EngineBridgeExtensions.SetEntityHealth(entityId, health);
        }

        /// <summary>
        /// 获取实体护甲值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>护甲值</returns>
        public static int GetEntityArmor(int entityId)
        {
            if (!IsValidEntity(entityId))
                return 0;

            return EngineBridgeExtensions.GetEntityArmor(entityId);
        }

        /// <summary>
        /// 设置实体护甲值
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <param name="armor">护甲值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetEntityArmor(int entityId, int armor)
        {
            if (!IsValidEntity(entityId) || armor < 0)
                return false;

            return EngineBridgeExtensions.SetEntityArmor(entityId, armor);
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>实体ID数组</returns>
        public static int[] GetAllEntities()
        {
            int count = EntityCount;
            if (count <= 0)
                return new int[0];

            return EngineBridgeExtensions.GetAllEntities(count);
        }

        /// <summary>
        /// 按类名查找实体
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns>实体ID数组</returns>
        public static int[] FindEntitiesByClass(string className)
        {
            if (string.IsNullOrEmpty(className))
                return new int[0];

            return EngineBridgeExtensions.FindEntitiesByClass(className);
        }
    }

    /// <summary>
    /// 追踪结果封装类
    /// </summary>
    public class TraceResult
    {
        private readonly TraceResultInfo _info;
        private readonly bool _success;

        public TraceResult(TraceResultInfo info, bool success)
        {
            _info = info;
            _success = success;
        }

        /// <summary>
        /// 追踪是否成功
        /// </summary>
        public bool Success => _success;

        /// <summary>
        /// 是否完全在固体中
        /// </summary>
        public bool AllSolid => _info.AllSolid != 0;

        /// <summary>
        /// 是否开始于固体中
        /// </summary>
        public bool StartSolid => _info.StartSolid != 0;

        /// <summary>
        /// 是否在开放空间
        /// </summary>
        public bool InOpen => _info.InOpen != 0;

        /// <summary>
        /// 是否在水中
        /// </summary>
        public bool InWater => _info.InWater != 0;

        /// <summary>
        /// 追踪分数（0-1）
        /// </summary>
        public float Fraction => _info.Fraction;

        /// <summary>
        /// 结束位置
        /// </summary>
        public Vector3 EndPosition => _info.EndPos != null ? 
            new Vector3(_info.EndPos[0], _info.EndPos[1], _info.EndPos[2]) : 
            new Vector3();

        /// <summary>
        /// 平面距离
        /// </summary>
        public float PlaneDistance => _info.PlaneDist;

        /// <summary>
        /// 平面法线
        /// </summary>
        public Vector3 PlaneNormal => _info.PlaneNormal != null ? 
            new Vector3(_info.PlaneNormal[0], _info.PlaneNormal[1], _info.PlaneNormal[2]) : 
            new Vector3();

        /// <summary>
        /// 命中的实体
        /// </summary>
        public int HitEntity => _info.HitEntity;

        /// <summary>
        /// 命中部位
        /// </summary>
        public int HitGroup => _info.HitGroup;
    }
}