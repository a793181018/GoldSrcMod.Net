//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license
//

#ifndef DEBUGGER_BRIDGE_H
#define DEBUGGER_BRIDGE_H

#include <stddef.h>

#ifdef __cplusplus
extern "C" {
#endif

// 调试器桥接接口
/**
 * 设置错误过滤器
 * @param filter 过滤器类型 (0=全部, 1=警告, 2=错误)
 */
void AmxModx_Bridge_SetErrorFilter(int filter);

/**
 * 记录调试信息
 * @param message 调试信息
 * @param level 日志级别 (0=调试, 1=信息, 2=警告, 3=错误)
 */
void AmxModx_Bridge_LogDebug(const char* message, int level);

/**
 * 获取最后错误信息
 * @return 错误信息字符串
 */
const char* AmxModx_Bridge_GetLastError();

/**
 * 清除最后错误信息
 */
void AmxModx_Bridge_ClearLastError();

/**
 * 启用/禁用调试模式
 * @param enabled 是否启用
 */
void AmxModx_Bridge_SetDebugMode(bool enabled);

/**
 * 检查是否处于调试模式
 * @return 是否处于调试模式
 */
bool AmxModx_Bridge_IsDebugMode();

/**
 * 设置断点
 * @param file 文件名
 * @param line 行号
 * @return 设置是否成功
 */
bool AmxModx_Bridge_SetBreakpoint(const char* file, int line);

/**
 * 移除断点
 * @param file 文件名
 * @param line 行号
 * @return 移除是否成功
 */
bool AmxModx_Bridge_RemoveBreakpoint(const char* file, int line);

/**
 * 获取当前调用栈信息
 * @param buffer 输出缓冲区
 * @param bufferSize 缓冲区大小
 * @return 调用栈信息长度
 */
int AmxModx_Bridge_GetCallStack(char* buffer, int bufferSize);

#ifdef __cplusplus
}
#endif

#endif // DEBUGGER_BRIDGE_H