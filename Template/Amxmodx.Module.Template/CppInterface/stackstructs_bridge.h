// stackstructs_bridge.h
// 栈管理接口

#ifndef STACKSTRUCTS_BRIDGE_H
#define STACKSTRUCTS_BRIDGE_H

#include "../amxmodx.h"

#ifdef __cplusplus
extern "C" {
#endif

// 创建栈
int AmxModx_Bridge_CreateStack();

// 销毁栈
int AmxModx_Bridge_DestroyStack(int* handle);

// 压入单元格值
int AmxModx_Bridge_PushStackCell(int handle, int value);

// 压入字符串
int AmxModx_Bridge_PushStackString(int handle, const char* str);

// 压入数组
int AmxModx_Bridge_PushStackArray(int handle, int arrayHandle);

// 弹出单元格值
int AmxModx_Bridge_PopStackCell(int handle, int* value);

// 弹出字符串
int AmxModx_Bridge_PopStackString(int handle, char* buffer, int size);

// 弹出数组
int AmxModx_Bridge_PopStackArray(int handle, int* arrayHandle);

// 获取栈深度
int AmxModx_Bridge_GetStackDepth(int handle);

// 清空栈
int AmxModx_Bridge_ClearStack(int handle);

#ifdef __cplusplus
}
#endif

#endif