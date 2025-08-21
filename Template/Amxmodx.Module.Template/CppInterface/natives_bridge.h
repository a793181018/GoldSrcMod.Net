// vim: set ts=4 sw=4 tw=99 noet:
#ifndef _INCLUDE_NATIVES_BRIDGE_H
#define _INCLUDE_NATIVES_BRIDGE_H

#include <stddef.h>

// 前向声明AMX结构体
#include "amx.h"

#ifdef __cplusplus
extern "C" {
#endif

// 原生函数注册管理
int AmxModx_Bridge_RegisterNative(AMX *amx, const char *name, AMX_NATIVE func);
int AmxModx_Bridge_UnregisterNative(AMX *amx, const char *name);
int AmxModx_Bridge_RegisterLibrary(AMX *amx, const char *libname);
int AmxModx_Bridge_GetNativeCount(AMX *amx);
const char *AmxModx_Bridge_GetNativeName(AMX *amx, int index);
bool AmxModx_Bridge_NativeExists(AMX *amx, const char *name);
void AmxModx_Bridge_ClearLibraries(AMX *amx);

// 参数处理
const char *AmxModx_Bridge_GetString(AMX *amx, cell param, int *length);
int AmxModx_Bridge_SetString(AMX *amx, cell param, const char *string, int maxlen);
cell AmxModx_Bridge_GetParam(AMX *amx, int index);
cell *AmxModx_Bridge_GetParamAddress(AMX *amx, int index);
int AmxModx_Bridge_SetParam(AMX *amx, int index, cell value);

// 浮点数处理
float AmxModx_Bridge_GetFloat(AMX *amx, int index);
int AmxModx_Bridge_SetFloat(AMX *amx, int index, float value);

// 数组处理
int AmxModx_Bridge_GetArray(AMX *amx, cell param, cell *dest, int size);
int AmxModx_Bridge_SetArray(AMX *amx, cell param, const cell *source, int size);

// 错误处理
void AmxModx_Bridge_LogError(AMX *amx, int err, const char *message);

#ifdef __cplusplus
}
#endif

#endif // _INCLUDE_NATIVES_BRIDGE_H