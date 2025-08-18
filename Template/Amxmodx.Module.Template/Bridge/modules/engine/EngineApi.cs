using System;
using System.Text;

namespace AmxModx.Bridge.Engine
{
    /// <summary>
    /// 引擎API的高级封装
    /// 提供更友好的C#接口
    /// </summary>
    public static class EngineApi
    {
        #region 常量定义

        /// <summary>
        /// 说话权限标志
        /// </summary>
        public static class SpeakFlags
        {
            public const int Normal = 0;
            public const int Muted = 1;
            public const int All = 2;
            public const int ListenAll = 4;
        }

        /// <summary>
        /// 视角类型
        /// </summary>
        public static class ViewTypes
        {
            public const int None = 0;
            public const int ThirdPerson = 1;
            public const int UpLeft = 2;
            public const int TopDown = 3;
        }

        /// <summary>
        /// 用户命令类型
        /// </summary>
        public static class UserCmdTypes
        {
            public const int ForwardMove = 0;
            public const int SideMove = 1;
            public const int UpMove = 2;
            public const int LerpMsec = 3;
            public const int Msec = 4;
            public const int LightLevel = 5;
            public const int Buttons = 6;
            public const int WeaponSelect = 7;
            public const int ImpactIndex = 8;
            public const int ViewAngles = 9;
            public const int ImpactPosition = 10;
        }

        #endregion

        #region 时间相关

        /// <summary>
        /// 获取当前游戏时间
        /// </summary>
        public static float GameTime => EngineBridge.Engine_GetGameTime();

        #endregion

        #region 实体操作

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="className">实体类名</param>
        /// <returns>实体ID，失败返回0</returns>
        public static int CreateEntity(string className)
        {
            return EngineBridge.Engine_CreateEntity(className);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>是否成功</returns>
        public static bool RemoveEntity(int entityId)
        {
            return EngineBridge.Engine_RemoveEntity(entityId) != 0;
        }

        /// <summary>
        /// 检查实体是否有效
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>是否有效</returns>
        public static bool IsValidEntity(int entityId)
        {
            return EngineBridge.Engine_IsValidEntity(entityId) != 0;
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
        /// <returns>距离值</returns>
        public static float GetEntityDistance(int entityA, int entityB)
        {
            return EngineBridge.Engine_GetEntityDistance(entityA, entityB);
        }

        #endregion

        #region 追踪系统

        /// <summary>
        /// 执行线条追踪
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="ignoreEntity">忽略的实体ID</param>
        /// <returns>追踪结果</returns>
        public static TraceResultInfo TraceLine(Vector3 start, Vector3 end, int ignoreEntity = 0)
        {
            EngineBridge.Engine_TraceLine(start, end, ignoreEntity, out var result);
            return result;
        }

        /// <summary>
        /// 执行包围盒追踪
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="hullType">包围盒类型</param>
        /// <param name="ignoreEntity">忽略的实体ID</param>
        /// <returns>追踪结果</returns>
        public static TraceResultInfo TraceHull(Vector3 start, Vector3 end, int hullType, int ignoreEntity = 0)
        {
            EngineBridge.Engine_TraceHull(start, end, hullType, ignoreEntity, out var result);
            return result;
        }

        /// <summary>
        /// 执行法线追踪
        /// </summary>
        /// <param name="entity">实体ID</param>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <returns>法线向量</returns>
        public static Vector3? TraceNormal(int entity, Vector3 start, Vector3 end)
        {
            var success = EngineBridge.Engine_TraceNormal(entity, start, end, out var normal);
            return success != 0 ? normal : (Vector3?)null;
        }

        /// <summary>
        /// 执行前向追踪
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="angles">角度</param>
        /// <param name="give">追踪距离</param>
        /// <param name="ignoreEntity">忽略的实体ID</param>
        /// <returns>追踪结果</returns>
        public static TraceResultInfo TraceForward(Vector3 start, Vector3 angles, float give = 20.0f, int ignoreEntity = 0)
        {
            EngineBridge.Engine_TraceForward(start, angles, give, ignoreEntity, out var result);
            return result;
        }

        #endregion

        #region 游戏事件

        /// <summary>
        /// 播放游戏事件
        /// </summary>
        public static void PlaybackEvent(int flags, int invoker, ushort eventIndex, float delay,
                                       Vector3 origin, Vector3 angles, float fparam1 = 0, float fparam2 = 0,
                                       int iparam1 = 0, int iparam2 = 0, int bparam1 = 0, int bparam2 = 0)
        {
            EngineBridge.Engine_PlaybackEvent(flags, invoker, eventIndex, delay, origin, angles,
                                              fparam1, fparam2, iparam1, iparam2, bparam1, bparam2);
        }

        #endregion

        #region 范围伤害

        /// <summary>
        /// 对指定范围内的玩家造成伤害
        /// </summary>
        /// <param name="origin">伤害中心点</param>
        /// <param name="damageMultiplier">伤害倍数</param>
        /// <param name="radiusMultiplier">半径倍数</param>
        /// <returns>是否成功</returns>
        public static bool RadiusDamage(Vector3 origin, int damageMultiplier = 1, int radiusMultiplier = 1)
        {
            return EngineBridge.Engine_RadiusDamage(origin, damageMultiplier, radiusMultiplier) != 0;
        }

        #endregion

        #region 点内容检查

        /// <summary>
        /// 检查点的内容类型
        /// </summary>
        /// <param name="point">检查点</param>
        /// <returns>内容类型值</returns>
        public static int PointContents(Vector3 point)
        {
            return EngineBridge.Engine_PointContents(point);
        }

        #endregion

        #region 字符串和索引

        /// <summary>
        /// 获取贴花索引
        /// </summary>
        /// <param name="decalName">贴花名称</param>
        /// <returns>索引值，失败返回-1</returns>
        public static int GetDecalIndex(string decalName)
        {
            return EngineBridge.Engine_GetDecalIndex(decalName);
        }

        /// <summary>
        /// 获取信息键缓冲区
        /// </summary>
        /// <param name="entity">实体ID（-1表示全局）</param>
        /// <returns>信息字符串</returns>
        public static string GetInfoKeyBuffer(int entity = -1)
        {
            var buffer = new byte[1024];
            var length = EngineBridge.Engine_GetInfoKeyBuffer(entity, buffer, buffer.Length);
            return length > 0 ? Encoding.UTF8.GetString(buffer, 0, length) : string.Empty;
        }

        /// <summary>
        /// 获取引擎字符串
        /// </summary>
        /// <param name="stringId">字符串ID</param>
        /// <returns>字符串内容</returns>
        public static string GetEngineString(int stringId)
        {
            var buffer = new byte[256];
            var length = EngineBridge.Engine_GetEngineString(stringId, buffer, buffer.Length);
            return length > 0 ? Encoding.UTF8.GetString(buffer, 0, length) : string.Empty;
        }

        #endregion

        #region 用户命令

        /// <summary>
        /// 获取用户命令
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <param name="type">命令类型</param>
        /// <returns>命令信息</returns>
        public static UserCmdInfo? GetUserCmd(int client, int type)
        {
            var success = EngineBridge.Engine_GetUserCmd(client, type, out var cmd);
            return success != 0 ? cmd : (UserCmdInfo?)null;
        }

        /// <summary>
        /// 设置用户命令
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <param name="type">命令类型</param>
        /// <param name="cmd">命令信息</param>
        /// <returns>是否成功</returns>
        public static bool SetUserCmd(int client, int type, UserCmdInfo cmd)
        {
            return EngineBridge.Engine_SetUserCmd(client, type, ref cmd) != 0;
        }

        #endregion

        #region 说话权限

        /// <summary>
        /// 设置说话权限
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <param name="speakFlags">说话标志</param>
        /// <returns>是否成功</returns>
        public static bool SetSpeak(int client, int speakFlags)
        {
            return EngineBridge.Engine_SetSpeak(client, speakFlags) != 0;
        }

        /// <summary>
        /// 获取说话权限
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <returns>说话标志</returns>
        public static int GetSpeak(int client)
        {
            return EngineBridge.Engine_GetSpeak(client);
        }

        #endregion

        #region 视角控制

        /// <summary>
        /// 设置玩家视角
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <param name="viewEntity">视角实体ID</param>
        /// <param name="viewType">视角类型</param>
        /// <returns>是否成功</returns>
        public static bool SetView(int client, int viewEntity, int viewType)
        {
            return EngineBridge.Engine_SetView(client, viewEntity, viewType) != 0;
        }

        /// <summary>
        /// 附加视角到目标实体
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <param name="targetEntity">目标实体ID</param>
        /// <returns>是否成功</returns>
        public static bool AttachView(int client, int targetEntity)
        {
            return EngineBridge.Engine_AttachView(client, targetEntity) != 0;
        }

        #endregion

        #region 光照

        /// <summary>
        /// 设置光照
        /// </summary>
        /// <param name="lights">光照字符串</param>
        /// <returns>是否成功</returns>
        public static bool SetLights(string lights)
        {
            return EngineBridge.Engine_SetLights(lights) != 0;
        }

        #endregion

        #region 地面放置

        /// <summary>
        /// 将实体放置到地面
        /// </summary>
        /// <param name="entity">实体ID</param>
        /// <returns>是否成功</returns>
        public static bool DropToFloor(int entity)
        {
            return EngineBridge.Engine_DropToFloor(entity) != 0;
        }

        #endregion

        #region 可见性检查

        /// <summary>
        /// 检查实体间是否可见
        /// </summary>
        /// <param name="srcEntity">源实体</param>
        /// <param name="destEntity">目标实体</param>
        /// <returns>是否可见</returns>
        public static bool IsVisible(int srcEntity, int destEntity)
        {
            return EngineBridge.Engine_IsVisible(srcEntity, destEntity) != 0;
        }

        /// <summary>
        /// 检查点是否在视角锥内
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="target">目标点</param>
        /// <returns>是否在锥内</returns>
        public static bool IsInViewCone(int entity, Vector3 target)
        {
            return EngineBridge.Engine_IsInViewCone(entity, target) != 0;
        }

        #endregion

        #region 事件注册

        /// <summary>
        /// 注册脉冲处理
        /// </summary>
        public static int RegisterImpulse(int impulse, EngineBridge.ImpulseCallback callback)
        {
            return EngineBridge.Engine_RegisterImpulse(impulse, callback);
        }

        /// <summary>
        /// 注册触碰处理
        /// </summary>
        public static int RegisterTouch(string touchedClass, string toucherClass, EngineBridge.TouchCallback callback)
        {
            return EngineBridge.Engine_RegisterTouch(touchedClass, toucherClass, callback);
        }

        /// <summary>
        /// 注册思考处理
        /// </summary>
        public static int RegisterThink(string className, EngineBridge.ThinkCallback callback)
        {
            return EngineBridge.Engine_RegisterThink(className, callback);
        }

        /// <summary>
        /// 注销脉冲处理
        /// </summary>
        public static bool UnregisterImpulse(int registerId)
        {
            return EngineBridge.Engine_UnregisterImpulse(registerId) != 0;
        }

        /// <summary>
        /// 注销触碰处理
        /// </summary>
        public static bool UnregisterTouch(int registerId)
        {
            return EngineBridge.Engine_UnregisterTouch(registerId) != 0;
        }

        /// <summary>
        /// 注销思考处理
        /// </summary>
        public static bool UnregisterThink(int registerId)
        {
            return EngineBridge.Engine_UnregisterThink(registerId) != 0;
        }

        #endregion
    }
}