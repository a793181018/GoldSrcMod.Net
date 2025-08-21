// hamsandwich_bridge.h
// HAM Sandwich桥接层头文件

#ifndef HAMSANDWICH_BRIDGE_H
#define HAMSANDWICH_BRIDGE_H

#ifdef __cplusplus
extern "C" {
#endif

// 初始化/清理
void InitializeHamSandwichBridge();
void CleanupHamSandwichBridge();

// 钩子管理
int RegisterHamHook(int function, int entityIndex, void* preCallback, void* postCallback);
bool EnableHamHook(int hookId);
bool DisableHamHook(int hookId);
bool RemoveHamHook(int hookId);

// 返回值处理
int GetReturnStatus();
int GetReturnValueInt();
float GetReturnValueFloat();
int GetReturnValueEntity();
void GetReturnValueVector(float vec[3]);

void SetReturnValueInt(int value);
void SetReturnValueFloat(float value);
void SetReturnValueVector(const float vec[3]);
void SetReturnValueEntity(int entity);

// 参数处理
int GetParameterInt(int paramIndex);
float GetParameterFloat(int paramIndex);
int GetParameterEntity(int paramIndex);
void GetParameterVector(int paramIndex, float vec[3]);

void SetParameterInt(int paramIndex, int value);
void SetParameterFloat(int paramIndex, float value);
void SetParameterVector(int paramIndex, const float vec[3]);
void SetParameterEntity(int paramIndex, int entity);

// 实用工具
bool IsValidHamFunction(int function);
const char* GetHamFunctionName(int function);
int GetHamFunctionCount();

#ifdef __cplusplus
}
#endif

#endif // HAMSANDWICH_BRIDGE_H