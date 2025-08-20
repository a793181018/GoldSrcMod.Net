#pragma once

#include <amx/amx.h>

#ifdef __cplusplus
extern "C" {
#endif

// 原生函数类型定义
typedef int (*NativeFunction)(AMX* amx, cell* params);

// 原生函数桥接接口
int AmxModx_Bridge_RegisterNative(AMX* amx, const char* name, NativeFunction func);
int AmxModx_Bridge_UnregisterNative(AMX* amx, const char* name);
int AmxModx_Bridge_RegisterLibrary(AMX* amx, const char* libname);
int AmxModx_Bridge_GetNativeCount(AMX* amx);
const char* AmxModx_Bridge_GetNativeName(AMX* amx, int index);
int AmxModx_Bridge_NativeExists(AMX* amx, const char* name);
void AmxModx_Bridge_ClearLibraries(AMX* amx);

// 参数处理接口
const char* AmxModx_Bridge_GetString(AMX* amx, int param, int* length);
int AmxModx_Bridge_SetString(AMX* amx, int param, const char* str, int maxlen);
cell AmxModx_Bridge_GetParam(AMX* amx, int index);
cell* AmxModx_Bridge_GetParamAddress(AMX* amx, int index);
int AmxModx_Bridge_SetParam(AMX* amx, int index, cell value);
float AmxModx_Bridge_GetFloat(AMX* amx, int index);
int AmxModx_Bridge_SetFloat(AMX* amx, int index, float value);
int AmxModx_Bridge_GetArray(AMX* amx, int param, cell* dest, int size);
int AmxModx_Bridge_SetArray(AMX* amx, int param, const cell* source, int size);

// 错误处理接口
void AmxModx_Bridge_LogError(AMX* amx, int err, const char* message);

// 新增：获取原生函数指针
NativeFunction AmxModx_Bridge_GetNativeFunction(AMX* amx, const char* functionName);

// 新增：调用原生函数
int AmxModx_Bridge_CallNativeFunction(NativeFunction func, const cell* params);
int AmxModx_Bridge_CallNativeFunctionEx(AMX* amx, NativeFunction func, const cell* params);

#ifdef __cplusplus
}
#endif