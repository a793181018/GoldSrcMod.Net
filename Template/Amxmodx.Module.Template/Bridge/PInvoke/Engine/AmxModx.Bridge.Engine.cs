using System;
using System.Runtime.InteropServices;

namespace AmxModx.Bridge.Engine
{
    /// <summary>
    /// Engine模块的C#桥接接口
    /// 提供对AMX Mod X引擎功能的访问
    /// </summary>
    public static class EngineBridge
    {
        private const string EngineBridgeDll = "engine_amxx";

        #region 基本类型定义

        /// <summary>
        /// 3D向量结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Vector3
        {
            public float X;
            public float Y;
            public float Z;

            public Vector3(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public override string ToString() => $"({X}, {Y}, {Z})";

            public static implicit operator Vector3(float[] array)
            {
                if (array == null || array.Length < 3)
                    return new Vector3(0, 0, 0);
                return new Vector3(array[0], array[1], array[2]);
            }

            public static implicit operator float[](Vector3 vec)
            {
                return new float[] { vec.X, vec.Y, vec.Z };
            }
        }

        /// <summary>
        /// 追踪结果信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TraceResultInfo
        {
            public int AllSolid;
            public int StartSolid;
            public int InOpen;
            public int InWater;
            public float Fraction;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] EndPos;
            public float PlaneDist;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] PlaneNormal;
            public int HitEntity;
            public int HitGroup;

            public TraceResultInfo()
            {
                EndPos = new float[3];
                PlaneNormal = new float[3];
            }
        }

        /// <summary>
        /// 用户命令信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct UserCmdInfo
        {
            public float ForwardMove;
            public float SideMove;
            public float UpMove;
            public float LerpMsec;
            public float Msec;
            public float LightLevel;
            public int Buttons;
            public int WeaponSelect;
            public int ImpactIndex;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] ViewAngles;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] ImpactPosition;

            public UserCmdInfo()
            {
                ViewAngles = new float[3];
                ImpactPosition = new float[3];
            }
        }

        #endregion

        #region 委托定义

        /// <summary>
        /// 脉冲处理委托
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <param name="impulse">脉冲值</param>
        public delegate void ImpulseCallback(int client, int impulse);

        /// <summary>
        /// 触碰处理委托
        /// </summary>
        /// <param name="touched">被触碰实体</param>
        /// <param name="toucher">触碰实体</param>
        public delegate void TouchCallback(int touched, int toucher);

        /// <summary>
        /// 思考处理委托
        /// </summary>
        /// <param name="entity">实体索引</param>
        public delegate void ThinkCallback(int entity);

        #endregion

        #region 时间相关

        /// <summary>
        /// 获取游戏时间
        /// </summary>
        /// <returns>当前游戏时间（秒）</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Engine_GetGameTime();

        #endregion

        #region 实体操作

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="className">实体类名</param>
        /// <returns>实体ID，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_CreateEntity(string className);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_RemoveEntity(int entityId);

        /// <summary>
        /// 检查实体是否有效
        /// </summary>
        /// <param name="entityId">实体ID</param>
        /// <returns>有效返回1，无效返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_IsValidEntity(int entityId);

        /// <summary>
        /// 获取当前实体数量
        /// </summary>
        /// <returns>实体数量</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetEntityCount();

        /// <summary>
        /// 计算两个实体间的距离
        /// </summary>
        /// <param name="entityA">实体A</param>
        /// <param name="entityB">实体B</param>
        /// <returns>距离值</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Engine_GetEntityDistance(int entityA, int entityB);

        #endregion

        #region 追踪系统

        /// <summary>
        /// 线条追踪
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="ignoreEntity">忽略的实体ID（0表示不忽略）</param>
        /// <param name="result">追踪结果</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_TraceLine(Vector3 start, Vector3 end, int ignoreEntity, out TraceResultInfo result);

        /// <summary>
        /// 包围盒追踪
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="hullType">包围盒类型</param>
        /// <param name="ignoreEntity">忽略的实体ID</param>
        /// <param name="result">追踪结果</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_TraceHull(Vector3 start, Vector3 end, int hullType, int ignoreEntity, out TraceResultInfo result);

        /// <summary>
        /// 法线追踪
        /// </summary>
        /// <param name="entity">实体ID</param>
        /// <param name="start">起始点</param>
        /// <param name="end">结束点</param>
        /// <param name="normal">返回的法线向量</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_TraceNormal(int entity, Vector3 start, Vector3 end, out Vector3 normal);

        /// <summary>
        /// 前向追踪
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="angles">角度</param>
        /// <param name="give">追踪距离</param>
        /// <param name="ignoreEntity">忽略的实体ID</param>
        /// <param name="result">追踪结果</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_TraceForward(Vector3 start, Vector3 angles, float give, int ignoreEntity, out TraceResultInfo result);

        #endregion

        #region 游戏事件

        /// <summary>
        /// 播放游戏事件
        /// </summary>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_PlaybackEvent(int flags, int invoker, ushort eventIndex, float delay,
                                                     Vector3 origin, Vector3 angles, float fparam1, float fparam2,
                                                     int iparam1, int iparam2, int bparam1, int bparam2);

        #endregion

        #region 范围伤害

        /// <summary>
        /// 范围伤害
        /// </summary>
        /// <param name="origin">伤害中心点</param>
        /// <param name="damageMultiplier">伤害倍数</param>
        /// <param name="radiusMultiplier">半径倍数</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_RadiusDamage(Vector3 origin, int damageMultiplier, int radiusMultiplier);

        #endregion

        #region 点内容检查

        /// <summary>
        /// 检查点的内容类型
        /// </summary>
        /// <param name="point">检查点</param>
        /// <returns>内容类型值</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_PointContents(Vector3 point);

        #endregion

        #region 字符串和索引

        /// <summary>
        /// 获取贴花索引
        /// </summary>
        /// <param name="decalName">贴花名称</param>
        /// <returns>索引值，失败返回-1</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetDecalIndex(string decalName);

        /// <summary>
        /// 获取信息键缓冲区
        /// </summary>
        /// <param name="entity">实体ID（-1表示全局）</param>
        /// <param name="buffer">接收缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>实际字符串长度</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetInfoKeyBuffer(int entity, byte[] buffer, int bufferSize);

        /// <summary>
        /// 获取引擎字符串
        /// </summary>
        /// <param name="stringId">字符串ID</param>
        /// <param name="buffer">接收缓冲区</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>实际字符串长度</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetEngineString(int stringId, byte[] buffer, int bufferSize);

        #endregion

        #region 用户命令

        /// <summary>
        /// 获取用户命令
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <param name="type">命令类型</param>
        /// <param name="cmd">命令结构</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetUserCmd(int client, int type, out UserCmdInfo cmd);

        /// <summary>
        /// 设置用户命令
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <param name="type">命令类型</param>
        /// <param name="cmd">命令结构</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_SetUserCmd(int client, int type, ref UserCmdInfo cmd);

        #endregion

        #region 说话权限

        /// <summary>
        /// 设置说话权限
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <param name="speakFlags">说话标志</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_SetSpeak(int client, int speakFlags);

        /// <summary>
        /// 获取说话权限
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <returns>说话标志</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_GetSpeak(int client);

        #endregion

        #region 视角控制

        /// <summary>
        /// 设置玩家视角
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <param name="viewEntity">视角实体ID</param>
        /// <param name="viewType">视角类型</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_SetView(int client, int viewEntity, int viewType);

        /// <summary>
        /// 附加视角到目标实体
        /// </summary>
        /// <param name="client">客户端索引</param>
        /// <param name="targetEntity">目标实体ID</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_AttachView(int client, int targetEntity);

        #endregion

        #region 光照

        /// <summary>
        /// 设置光照
        /// </summary>
        /// <param name="lights">光照字符串</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_SetLights(string lights);

        #endregion

        #region 地面放置

        /// <summary>
        /// 将实体放置到地面
        /// </summary>
        /// <param name="entity">实体ID</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_DropToFloor(int entity);

        #endregion

        #region 可见性检查

        /// <summary>
        /// 检查实体间是否可见
        /// </summary>
        /// <param name="srcEntity">源实体</param>
        /// <param name="destEntity">目标实体</param>
        /// <returns>可见返回1，不可见返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_IsVisible(int srcEntity, int destEntity);

        /// <summary>
        /// 检查点是否在视角锥内
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="target">目标点</param>
        /// <returns>在锥内返回1，不在返回0</returns>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_IsInViewCone(int entity, Vector3 target);

        #endregion

        #region 事件注册

        /// <summary>
        /// 注册脉冲处理
        /// </summary>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_RegisterImpulse(int impulse, ImpulseCallback callback);

        /// <summary>
        /// 注册触碰处理
        /// </summary>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_RegisterTouch(string touchedClass, string toucherClass, TouchCallback callback);

        /// <summary>
        /// 注册思考处理
        /// </summary>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_RegisterThink(string className, ThinkCallback callback);

        /// <summary>
        /// 注销脉冲处理
        /// </summary>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_UnregisterImpulse(int registerId);

        /// <summary>
        /// 注销触碰处理
        /// </summary>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_UnregisterTouch(int registerId);

        /// <summary>
        /// 注销思考处理
        /// </summary>
        [DllImport(EngineBridgeDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Engine_UnregisterThink(int registerId);

        #endregion
    }
}