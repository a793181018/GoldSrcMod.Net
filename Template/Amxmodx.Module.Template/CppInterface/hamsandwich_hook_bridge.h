#pragma once

// 前置声明
struct edict_s;
typedef struct edict_s edict_t;

// 回调函数类型定义
typedef void (*HamHookCallback)(int entity, int paramCount, void** parameters);
typedef void (*HamHookCallbackPost)(int entity, int paramCount, void** parameters);

// 注意：HAM函数枚举值已在ham_const.h中定义，这里直接使用

// 钩子管理接口
extern "C" {
    // 注册钩子
    int RegisterHamHook(int function, const char* entityClass, 
                       HamHookCallback preCallback, HamHookCallbackPost postCallback);
    
    int RegisterHamHookFromEntity(int function, int entityId,
                                  HamHookCallback preCallback, HamHookCallbackPost postCallback);
    
    int RegisterHamHookPlayer(int function,
                             HamHookCallback preCallback, HamHookCallbackPost postCallback);
    
    // 钩子控制
    void EnableHamHook(int hookId);
    void DisableHamHook(int hookId);
    int IsHamHookValid(int function);
    
    // 初始化/清理
    void InitializeHamSandwichBridge();
    void CleanupHamSandwichBridge();
}

// 内部使用的前向声明
namespace HamHooks {
    struct HookData;
    struct ForwardData;
}

// 参数访问辅助函数
extern "C" {
    // 返回值处理
    int GetReturnStatus();
    int GetReturnValueInt();
    float GetReturnValueFloat();
    void GetReturnValueVector(float vec[3]);
    int GetReturnValueEntity();
    
    // 设置返回值
    void SetReturnValueInt(int value);
    void SetReturnValueFloat(float value);
    void SetReturnValueVector(const float vec[3]);
    void SetReturnValueEntity(int entity);
    
    // 参数处理
    void SetParameterInt(int paramIndex, int value);
    void SetParameterFloat(int paramIndex, float value);
    void SetParameterVector(int paramIndex, const float vec[3]);
    void SetParameterEntity(int paramIndex, int entity);
}