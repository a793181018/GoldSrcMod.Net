using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace AmxModx.Bridge.Fakemeta
{
    /// <summary>
    /// Fakemeta模块事件类型枚举
    /// </summary>
    public enum ForwardType
    {
        PrecacheModel = 0,
        PrecacheSound = 1,
        SetModel = 2,
        CreateEntity = 3,
        RemoveEntity = 4,
        Spawn = 5,
        Think = 6,
        Use = 7,
        Touch = 8,
        Blocked = 9,
        ClientCommand = 10,
        ClientUserInfoChanged = 11,
        ServerActivate = 12,
        ServerDeactivate = 13,
        PlayerPreThink = 14,
        PlayerPostThink = 15,
        StartFrame = 16,
        ClientConnect = 17,
        ClientDisconnect = 18,
        ClientPutInServer = 19,
        ClientKill = 20,
        ClientSpawn = 21,
        TraceLine = 22,
        TraceToss = 23,
        TraceMonsterHull = 24,
        TraceHull = 25,
        TraceModel = 26,
        TraceTexture = 27,
        TraceSphere = 28,
        GetAimVector = 29,
        EmitSound = 30,
        EmitAmbientSound = 31,
        LightStyle = 32,
        DecalIndex = 33,
        PointContents = 34,
        MessageBegin = 35,
        MessageEnd = 36,
        WriteByte = 37,
        WriteChar = 38,
        WriteShort = 39,
        WriteLong = 40,
        WriteAngle = 41,
        WriteCoord = 42,
        WriteString = 43,
        WriteEntity = 44,
        CVarGetFloat = 45,
        CVarGetString = 46,
        CVarSetFloat = 47,
        CVarSetString = 48,
        CVarRegister = 49,
        AlertMessage = 50,
        EngineFprintf = 51,
        PvsFindEntity = 52,
        PvsEntitiesInPvs = 53,
        PvsCheckOrigin = 54,
        PvsCheckEntity = 55,
        PvsCheckBox = 56,
        PvsCheckPoint = 57,
        PvsCheckEverything = 58,
        PvsCheckEverything2 = 59,
        PvsCheckEverything3 = 60,
        PvsCheckEverything4 = 61,
        PvsCheckEverything5 = 62,
        PvsCheckEverything6 = 63,
        PvsCheckEverything7 = 64,
        PvsCheckEverything8 = 65,
        PvsCheckEverything9 = 66,
        PvsCheckEverything10 = 67,
        PvsCheckEverything11 = 68,
        PvsCheckEverything12 = 69,
        PvsCheckEverything13 = 70,
        PvsCheckEverything14 = 71,
        PvsCheckEverything15 = 72,
        PvsCheckEverything16 = 73,
        PvsCheckEverything17 = 74,
        PvsCheckEverything18 = 75,
        PvsCheckEverything19 = 76,
        PvsCheckEverything20 = 77,
        PvsCheckEverything21 = 78,
        PvsCheckEverything22 = 79,
        PvsCheckEverything23 = 80,
        PvsCheckEverything24 = 81,
        PvsCheckEverything25 = 82,
        PvsCheckEverything26 = 83,
        PvsCheckEverything27 = 84,
        PvsCheckEverything28 = 85,
        PvsCheckEverything29 = 86,
        PvsCheckEverything30 = 87,
        PvsCheckEverything31 = 88,
        PvsCheckEverything32 = 89,
        PvsCheckEverything33 = 90,
        PvsCheckEverything34 = 91,
        PvsCheckEverything35 = 92,
        PvsCheckEverything36 = 93,
        PvsCheckEverything37 = 94,
        PvsCheckEverything38 = 95,
        PvsCheckEverything39 = 96,
        PvsCheckEverything40 = 97,
        PvsCheckEverything41 = 98,
        PvsCheckEverything42 = 99,
        PvsCheckEverything43 = 100,
        PvsCheckEverything44 = 101,
        PvsCheckEverything45 = 102,
        PvsCheckEverything46 = 103,
        PvsCheckEverything47 = 104,
        PvsCheckEverything48 = 105,
        PvsCheckEverything49 = 106,
        PvsCheckEverything50 = 107,
        PvsCheckEverything51 = 108,
        PvsCheckEverything52 = 109,
        PvsCheckEverything53 = 110,
        PvsCheckEverything54 = 111,
        PvsCheckEverything55 = 112,
        PvsCheckEverything56 = 113,
        PvsCheckEverything57 = 114,
        PvsCheckEverything58 = 115,
        PvsCheckEverything59 = 116,
        PvsCheckEverything60 = 117,
        PvsCheckEverything61 = 118,
        PvsCheckEverything62 = 119,
        PvsCheckEverything63 = 120,
        PvsCheckEverything64 = 121,
        PvsCheckEverything65 = 122,
        PvsCheckEverything66 = 123,
        PvsCheckEverything67 = 124,
        PvsCheckEverything68 = 125,
        PvsCheckEverything69 = 126,
        PvsCheckEverything70 = 127,
        PvsCheckEverything71 = 128,
        PvsCheckEverything72 = 129,
        PvsCheckEverything73 = 130,
        PvsCheckEverything74 = 131
    }

    /// <summary>
    /// 事件执行时机
    /// </summary>
    public enum ForwardTiming
    {
        Pre = 0,    // 前置钩子
        Post = 1    // 后置钩子
    }

    /// <summary>
    /// 事件返回值
    /// </summary>
    public enum ForwardResult
    {
        Ignored = 0,     // 忽略，继续执行
        Handled = 1,     // 已处理，继续执行
        Override = 2,    // 覆盖，继续执行
        Supercede = 3    // 取代，停止执行
    }

    /// <summary>
    /// 事件参数结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EventData
    {
        public int EntityIndex;        // 实体索引
        [MarshalAs(UnmanagedType.LPStr)]
        public string StringValue;     // 字符串值
        public float FloatValue;       // 浮点值
        public int IntValue;           // 整数值
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] VectorValue;    // 向量值
        public IntPtr CustomData;      // 自定义数据
    }

    /// <summary>
    /// 事件回调委托
    /// </summary>
    /// <param name="data">事件数据</param>
    /// <param name="userData">用户数据</param>
    /// <returns>事件处理结果</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ForwardResult ForwardCallback(ref EventData data, IntPtr userData);

    /// <summary>
    /// 简化的回调委托，用于lambda表达式
    /// </summary>
    /// <param name="entityIndex">实体索引</param>
    /// <param name="stringValue">字符串值</param>
    /// <param name="floatValue">浮点值</param>
    /// <param name="intValue">整数值</param>
    /// <param name="vectorValue">向量值</param>
    /// <returns>事件处理结果</returns>
    public delegate ForwardResult SimpleForwardCallback(int entityIndex, string stringValue, float floatValue, int intValue, float[] vectorValue);

    /// <summary>
    /// 实体事件回调委托
    /// </summary>
    /// <param name="entity">实体索引</param>
    /// <returns>事件处理结果</returns>
    public delegate ForwardResult EntityForwardCallback(int entity);

    /// <summary>
    /// 玩家事件回调委托
    /// </summary>
    /// <param name="playerIndex">玩家索引</param>
    /// <param name="command">命令字符串</param>
    /// <returns>事件处理结果</returns>
    public delegate ForwardResult PlayerForwardCallback(int playerIndex, string command = null);

    /// <summary>
    /// 无参数事件回调委托
    /// </summary>
    /// <returns>事件处理结果</returns>
    public delegate ForwardResult VoidForwardCallback();

    internal static class NativeMethods
    {
        private const string DllName = "fakemeta_amxx";

        /// <summary>
        /// 初始化事件系统
        /// </summary>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int FMB_InitializeForwardSystem();

        /// <summary>
        /// 清理事件系统
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FMB_CleanupForwardSystem();

        /// <summary>
        /// 注册事件钩子
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="callback">回调函数</param>
        /// <param name="userData">用户数据</param>
        /// <returns>注册句柄，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int FMB_RegisterForward(ForwardType type, ForwardTiming timing, ForwardCallback callback, IntPtr userData);

        /// <summary>
        /// 注销事件钩子
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="handle">注册句柄</param>
        /// <returns>成功返回1，失败返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int FMB_UnregisterForward(ForwardType type, ForwardTiming timing, int handle);

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="data">事件数据</param>
        /// <returns>事件处理结果</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ForwardResult FMB_ExecuteForwards(ForwardType type, ForwardTiming timing, ref EventData data);

        /// <summary>
        /// 获取事件队列长度
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <returns>事件数量</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int FMB_GetForwardCount(ForwardType type, ForwardTiming timing);

        /// <summary>
        /// 检查事件是否已注册
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="handle">注册句柄</param>
        /// <returns>已注册返回1，未注册返回0</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int FMB_IsForwardRegistered(ForwardType type, ForwardTiming timing, int handle);

        /// <summary>
        /// 获取事件类型名称
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <returns>类型名称</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string FMB_GetForwardTypeName(ForwardType type);

        /// <summary>
        /// 获取事件时机名称
        /// </summary>
        /// <param name="timing">执行时机</param>
        /// <returns>时机名称</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string FMB_GetForwardTimingName(ForwardTiming timing);

        /// <summary>
        /// 获取事件结果名称
        /// </summary>
        /// <param name="result">处理结果</param>
        /// <returns>结果名称</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string FMB_GetForwardResultName(ForwardResult result);
    }

    /// <summary>
    /// 事件管理器 - 提供简化的回调注册接口
    /// </summary>
    public static class ForwardManager
    {
        private static readonly Dictionary<int, ForwardCallback> callbacks = new Dictionary<int, ForwardCallback>();
        private static readonly Dictionary<int, GCHandle> gcHandles = new Dictionary<int, GCHandle>();
        private static int nextHandle = 1;
        private static bool isInitialized = false;

        /// <summary>
        /// 初始化事件系统
        /// </summary>
        public static void Initialize()
        {
            if (!isInitialized)
            {
                int result = NativeMethods.FMB_InitializeForwardSystem();
                if (result == 1)
                {
                    isInitialized = true;
                }
                else
                {
                    throw new InvalidOperationException("Failed to initialize Fakemeta forward system");
                }
            }
        }

        /// <summary>
        /// 清理事件系统
        /// </summary>
        public static void Cleanup()
        {
            if (isInitialized)
            {
                NativeMethods.FMB_CleanupForwardSystem();
                isInitialized = false;
            }
            
            foreach (var handle in gcHandles.Values)
            {
                if (handle.IsAllocated)
                    handle.Free();
            }
            
            callbacks.Clear();
            gcHandles.Clear();
        }

        /// <summary>
        /// 注册事件回调
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="callback">回调函数</param>
        /// <param name="userData">用户数据</param>
        /// <returns>注册句柄</returns>
        public static int RegisterForward(ForwardType type, ForwardTiming timing, ForwardCallback callback, object userData = null)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));
            if (!isInitialized)
                Initialize();

            int handle = nextHandle++;
            callbacks[handle] = callback;

            IntPtr userDataPtr = IntPtr.Zero;
            if (userData != null)
            {
                var gcHandle = GCHandle.Alloc(userData);
                gcHandles[handle] = gcHandle;
                userDataPtr = GCHandle.ToIntPtr(gcHandle);
            }

            int result = NativeMethods.FMB_RegisterForward(type, timing, callback, userDataPtr);
            if (result == 0)
            {
                callbacks.Remove(handle);
                if (userData != null && gcHandles.ContainsKey(handle))
                {
                    gcHandles[handle].Free();
                    gcHandles.Remove(handle);
                }
                return 0;
            }

            return handle;
        }

        /// <summary>
        /// 使用lambda表达式注册事件回调
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="callback">lambda回调</param>
        /// <returns>注册句柄</returns>
        public static int RegisterForward(ForwardType type, ForwardTiming timing, SimpleForwardCallback callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            ForwardCallback nativeCallback = (ref EventData data, IntPtr userData) =>
            {
                return callback(data.EntityIndex, data.StringValue, data.FloatValue, data.IntValue, data.VectorValue);
            };

            return RegisterForward(type, timing, nativeCallback);
        }

        /// <summary>
        /// 注册实体事件回调
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="callback">实体回调</param>
        /// <returns>注册句柄</returns>
        public static int RegisterEntityForward(ForwardType type, ForwardTiming timing, EntityForwardCallback callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            ForwardCallback nativeCallback = (ref EventData data, IntPtr userData) =>
            {
                return callback(data.EntityIndex);
            };

            return RegisterForward(type, timing, nativeCallback);
        }

        /// <summary>
        /// 注册玩家事件回调
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="callback">玩家回调</param>
        /// <returns>注册句柄</returns>
        public static int RegisterPlayerForward(ForwardType type, ForwardTiming timing, PlayerForwardCallback callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            ForwardCallback nativeCallback = (ref EventData data, IntPtr userData) =>
            {
                return callback(data.EntityIndex, data.StringValue);
            };

            return RegisterForward(type, timing, nativeCallback);
        }

        /// <summary>
        /// 注册无参数事件回调
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="callback">无参数回调</param>
        /// <returns>注册句柄</returns>
        public static int RegisterVoidForward(ForwardType type, ForwardTiming timing, VoidForwardCallback callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            ForwardCallback nativeCallback = (ref EventData data, IntPtr userData) =>
            {
                return callback();
            };

            return RegisterForward(type, timing, nativeCallback);
        }

        /// <summary>
        /// 注销事件回调
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="handle">注册句柄</param>
        /// <returns>是否成功</returns>
        public static bool UnregisterForward(ForwardType type, ForwardTiming timing, int handle)
        {
            if (!callbacks.ContainsKey(handle))
                return false;
            if (!isInitialized)
                return false;

            int result = NativeMethods.FMB_UnregisterForward(type, timing, handle);
            if (result != 0)
            {
                callbacks.Remove(handle);
                if (gcHandles.ContainsKey(handle))
                {
                    gcHandles[handle].Free();
                    gcHandles.Remove(handle);
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="data">事件数据</param>
        /// <returns>事件处理结果</returns>
        public static ForwardResult ExecuteForwards(ForwardType type, ForwardTiming timing, ref EventData data)
        {
            if (!isInitialized)
                return ForwardResult.Ignored;

            return NativeMethods.FMB_ExecuteForwards(type, timing, ref data);
        }

        /// <summary>
        /// 获取事件数量
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <returns>事件数量</returns>
        public static int GetForwardCount(ForwardType type, ForwardTiming timing)
        {
            if (!isInitialized)
                return 0;

            return NativeMethods.FMB_GetForwardCount(type, timing);
        }

        /// <summary>
        /// 检查事件是否已注册
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="timing">执行时机</param>
        /// <param name="handle">注册句柄</param>
        /// <returns>是否已注册</returns>
        public static bool IsForwardRegistered(ForwardType type, ForwardTiming timing, int handle)
        {
            if (!isInitialized)
                return false;

            return NativeMethods.FMB_IsForwardRegistered(type, timing, handle) != 0;
        }
    }

    /// <summary>
    /// Fakemeta回调管理器 - 提供高级封装
    /// </summary>
    public static class FakemetaCallbacks
    {
        /// <summary>
        /// 注册实体创建回调
        /// </summary>
        /// <param name="callback">创建回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnEntityCreate(EntityForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterEntityForward(ForwardType.CreateEntity, timing, callback);
        }

        /// <summary>
        /// 注册实体销毁回调
        /// </summary>
        /// <param name="callback">销毁回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnEntityRemove(EntityForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterEntityForward(ForwardType.RemoveEntity, timing, callback);
        }

        /// <summary>
        /// 注册实体生成回调
        /// </summary>
        /// <param name="callback">生成回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnEntitySpawn(EntityForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterEntityForward(ForwardType.Spawn, timing, callback);
        }

        /// <summary>
        /// 注册实体思考回调
        /// </summary>
        /// <param name="callback">思考回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnEntityThink(EntityForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterEntityForward(ForwardType.Think, timing, callback);
        }

        /// <summary>
        /// 注册实体使用回调
        /// </summary>
        /// <param name="callback">使用回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnEntityUse(EntityForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterEntityForward(ForwardType.Use, timing, callback);
        }

        /// <summary>
        /// 注册实体触碰回调
        /// </summary>
        /// <param name="callback">触碰回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnEntityTouch(EntityForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterEntityForward(ForwardType.Touch, timing, callback);
        }

        /// <summary>
        /// 注册实体阻挡回调
        /// </summary>
        /// <param name="callback">阻挡回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnEntityBlocked(EntityForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterEntityForward(ForwardType.Blocked, timing, callback);
        }

        /// <summary>
        /// 注册玩家连接回调
        /// </summary>
        /// <param name="callback">连接回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnPlayerConnect(PlayerForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterPlayerForward(ForwardType.ClientConnect, timing, callback);
        }

        /// <summary>
        /// 注册玩家断开连接回调
        /// </summary>
        /// <param name="callback">断开连接回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnPlayerDisconnect(PlayerForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterPlayerForward(ForwardType.ClientDisconnect, timing, callback);
        }

        /// <summary>
        /// 注册玩家命令回调
        /// </summary>
        /// <param name="callback">命令回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnPlayerCommand(PlayerForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterPlayerForward(ForwardType.ClientCommand, timing, callback);
        }

        /// <summary>
        /// 注册玩家生成回调
        /// </summary>
        /// <param name="callback">生成回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnPlayerSpawn(PlayerForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterPlayerForward(ForwardType.ClientSpawn, timing, callback);
        }

        /// <summary>
        /// 注册玩家预思考回调
        /// </summary>
        /// <param name="callback">预思考回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnPlayerPreThink(PlayerForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterPlayerForward(ForwardType.PlayerPreThink, timing, callback);
        }

        /// <summary>
        /// 注册玩家后思考回调
        /// </summary>
        /// <param name="callback">后思考回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnPlayerPostThink(PlayerForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterPlayerForward(ForwardType.PlayerPostThink, timing, callback);
        }

        /// <summary>
        /// 注册服务器激活回调
        /// </summary>
        /// <param name="callback">激活回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnServerActivate(VoidForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterVoidForward(ForwardType.ServerActivate, timing, callback);
        }

        /// <summary>
        /// 注册服务器停用回调
        /// </summary>
        /// <param name="callback">停用回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnServerDeactivate(VoidForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterVoidForward(ForwardType.ServerDeactivate, timing, callback);
        }

        /// <summary>
        /// 注册每帧回调
        /// </summary>
        /// <param name="callback">每帧回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnStartFrame(VoidForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterVoidForward(ForwardType.StartFrame, timing, callback);
        }

        /// <summary>
        /// 注册模型预缓存回调
        /// </summary>
        /// <param name="callback">预缓存回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnPrecacheModel(PlayerForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterPlayerForward(ForwardType.PrecacheModel, timing, callback);
        }

        /// <summary>
        /// 注册声音预缓存回调
        /// </summary>
        /// <param name="callback">预缓存回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnPrecacheSound(PlayerForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterPlayerForward(ForwardType.PrecacheSound, timing, callback);
        }

        /// <summary>
        /// 注册模型设置回调
        /// </summary>
        /// <param name="callback">模型设置回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnSetModel(PlayerForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterPlayerForward(ForwardType.SetModel, timing, callback);
        }

        /// <summary>
        /// 注册追踪线回调
        /// </summary>
        /// <param name="callback">追踪线回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnTraceLine(PlayerForwardCallback callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return ForwardManager.RegisterPlayerForward(ForwardType.TraceLine, timing, callback);
        }
    }

    /// <summary>
    /// 事件扩展方法，提供lambda表达式支持
    /// </summary>
    public static class ForwardExtensions
    {
        /// <summary>
        /// 使用lambda表达式注册实体创建事件
        /// </summary>
        /// <param name="callback">lambda回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnEntityCreate(this Func<int, ForwardResult> callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return FakemetaCallbacks.OnEntityCreate(callback, timing);
        }

        /// <summary>
        /// 使用lambda表达式注册玩家连接事件
        /// </summary>
        /// <param name="callback">lambda回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnPlayerConnect(this Func<int, string, ForwardResult> callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return FakemetaCallbacks.OnPlayerConnect(callback, timing);
        }

        /// <summary>
        /// 使用lambda表达式注册每帧事件
        /// </summary>
        /// <param name="callback">lambda回调</param>
        /// <param name="timing">执行时机</param>
        /// <returns>注册句柄</returns>
        public static int OnStartFrame(this Func<ForwardResult> callback, ForwardTiming timing = ForwardTiming.Pre)
        {
            return FakemetaCallbacks.OnStartFrame(callback, timing);
        }
    }
}