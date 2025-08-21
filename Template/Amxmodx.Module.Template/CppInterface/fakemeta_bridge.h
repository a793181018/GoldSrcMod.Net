#pragma once

#include <vector>
#include <unordered_map>
#include <string>
#include <functional>

// C++版本的事件类型枚举，与C#保持一致
enum class FMB_ForwardType
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
};

// 事件执行时机
enum class FMB_ForwardTiming
{
    Pre = 0,    // 前置钩子
    Post = 1    // 后置钩子
};

// 事件处理结果
enum class FMB_ForwardResult
{
    Ignored = 0,     // 忽略，继续执行
    Handled = 1,     // 已处理，继续执行
    Override = 2,    // 覆盖，继续执行
    Supercede = 3    // 取代，停止执行
};

// 事件数据结构体，与C#保持二进制兼容
struct FMB_EventData
{
    int entityIndex;        // 实体索引
    const char* stringValue; // 字符串值
    float floatValue;       // 浮点值
    int intValue;           // 整数值
    float vectorValue[3];   // 向量值
    void* customData;       // 自定义数据
};

// 回调函数类型定义
using FMB_ForwardCallback = FMB_ForwardResult(*)(FMB_EventData&, void*);

// 回调信息结构体
struct FMB_CallbackInfo
{
    FMB_ForwardCallback callback;
    void* userData;
    int handle;
    int priority; // 优先级，数值越高优先级越高
};

// 事件系统管理类
class FMB_ForwardSystem
{
public:
    static FMB_ForwardSystem& Instance();

    // 初始化事件系统
    bool Initialize();

    // 清理事件系统
    void Cleanup();

    // 注册事件钩子
    int RegisterForward(FMB_ForwardType type, FMB_ForwardTiming timing, FMB_ForwardCallback callback, void* userData);

    // 注销事件钩子
    bool UnregisterForward(FMB_ForwardType type, FMB_ForwardTiming timing, int handle);

    // 触发事件
    FMB_ForwardResult ExecuteForwards(FMB_ForwardType type, FMB_ForwardTiming timing, FMB_EventData& data);

    // 获取事件数量
    int GetForwardCount(FMB_ForwardType type, FMB_ForwardTiming timing);

    // 检查事件是否已注册
    bool IsForwardRegistered(FMB_ForwardType type, FMB_ForwardTiming timing, int handle);

    // 获取事件类型名称
    const char* GetForwardTypeName(FMB_ForwardType type);

    // 获取事件时机名称
    const char* GetForwardTimingName(FMB_ForwardTiming timing);

    // 获取事件结果名称
    const char* GetForwardResultName(FMB_ForwardResult result);

private:
    FMB_ForwardSystem() = default;
    ~FMB_ForwardSystem() = default;

    // 禁用拷贝构造和赋值
    FMB_ForwardSystem(const FMB_ForwardSystem&) = delete;
    FMB_ForwardSystem& operator=(const FMB_ForwardSystem&) = delete;

    // 内部方法
    std::vector<FMB_CallbackInfo>& GetCallbacks(FMB_ForwardType type, FMB_ForwardTiming timing);
    void SortCallbacksByPriority(std::vector<FMB_CallbackInfo>& callbacks);

    // 数据结构
    std::unordered_map<int, std::vector<FMB_CallbackInfo>> preCallbacks;
    std::unordered_map<int, std::vector<FMB_CallbackInfo>> postCallbacks;
    int nextHandle = 1;
    bool isInitialized = false;
};

// C接口导出函数
extern "C"
{
    // 初始化事件系统
    __declspec(dllexport) int FMB_InitializeForwardSystem();

    // 清理事件系统
    __declspec(dllexport) void FMB_CleanupForwardSystem();

    // 注册事件钩子
    __declspec(dllexport) int FMB_RegisterForward(FMB_ForwardType type, FMB_ForwardTiming timing, FMB_ForwardCallback callback, void* userData);

    // 注销事件钩子
    __declspec(dllexport) int FMB_UnregisterForward(FMB_ForwardType type, FMB_ForwardTiming timing, int handle);

    // 触发事件
    __declspec(dllexport) FMB_ForwardResult FMB_ExecuteForwards(FMB_ForwardType type, FMB_ForwardTiming timing, FMB_EventData& data);

    // 获取事件数量
    __declspec(dllexport) int FMB_GetForwardCount(FMB_ForwardType type, FMB_ForwardTiming timing);

    // 检查事件是否已注册
    __declspec(dllexport) int FMB_IsForwardRegistered(FMB_ForwardType type, FMB_ForwardTiming timing, int handle);

    // 获取事件类型名称
    __declspec(dllexport) const char* FMB_GetForwardTypeName(FMB_ForwardType type);

    // 获取事件时机名称
    __declspec(dllexport) const char* FMB_GetForwardTimingName(FMB_ForwardTiming timing);

    // 获取事件结果名称
    __declspec(dllexport) const char* FMB_GetForwardResultName(FMB_ForwardResult result);
}