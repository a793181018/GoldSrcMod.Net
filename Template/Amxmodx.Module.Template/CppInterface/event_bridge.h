// vim: set ts=4 sw=4 tw=99 noet:
//
// Event System Bridge - C++ Header
// Copyright (C) The AMX Mod X Development Team
//
// This software is licensed under the GNU General Public License, version 3 or higher.

#pragma once

#include <amxmodx.h>
#include "CEvent.h"

#ifdef __cplusplus
extern "C" {
#endif

/**
 * @brief 注册游戏事件
 * @param eventName 事件名称
 * @param callbackFunc 回调函数名
 * @param flags 事件标志
 * @param conditions 事件条件数组
 * @param conditionCount 条件数量
 * @return 事件句柄，失败返回0
 */
int AmxModx_Bridge_RegisterEvent(const char* eventName, const char* callbackFunc, int flags, const char** conditions, int conditionCount);

/**
 * @brief 扩展注册游戏事件
 * @param eventName 事件名称
 * @param callbackFunc 回调函数名
 * @param flags 事件标志
 * @param conditions 事件条件数组
 * @param conditionCount 条件数量
 * @return 事件句柄，失败返回0
 */
int AmxModx_Bridge_RegisterEventEx(const char* eventName, const char* callbackFunc, int flags, const char** conditions, int conditionCount);

/**
 * @brief 启用事件
 * @param eventHandle 事件句柄
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_EnableEvent(int eventHandle);

/**
 * @brief 禁用事件
 * @param eventHandle 事件句柄
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_DisableEvent(int eventHandle);

/**
 * @brief 获取事件ID
 * @param eventName 事件名称
 * @return 事件ID，0表示无效事件
 */
int AmxModx_Bridge_GetEventId(const char* eventName);

/**
 * @brief 检查事件是否有效
 * @param eventHandle 事件句柄
 * @return 有效返回1，无效返回0
 */
int AmxModx_Bridge_IsEventValid(int eventHandle);

/**
 * @brief 获取事件名称列表
 * @param buffer 存储事件名称的缓冲区
 * @param bufferSize 缓冲区大小
 * @return 事件数量
 */
int AmxModx_Bridge_GetEventList(char** buffer, int bufferSize);

/**
 * @brief 获取事件参数数量
 * @param eventName 事件名称
 * @return 参数数量，-1表示无效事件
 */
int AmxModx_Bridge_GetEventParamCount(const char* eventName);

/**
 * @brief 获取事件参数名称
 * @param eventName 事件名称
 * @param paramIndex 参数索引(从0开始)
 * @param buffer 存储参数名称的缓冲区
 * @param bufferSize 缓冲区大小
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_GetEventParamName(const char* eventName, int paramIndex, char* buffer, int bufferSize);

/**
 * @brief 获取事件参数类型
 * @param eventName 事件名称
 * @param paramIndex 参数索引(从0开始)
 * @return 参数类型字符串，NULL表示无效事件或索引
 */
const char* AmxModx_Bridge_GetEventParamType(const char* eventName, int paramIndex);

#ifdef __cplusplus
}
#endif