//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license
//

#include "debugger_bridge.h"
#include "amxmodx.h"
#include "amxdbg.h"
#include <cstring>

// 全局调试状态
static int g_debugFilter = 0;
static bool g_debugMode = false;
static char g_lastError[1024] = {0};

// 调试器桥接实现
void AmxModx_Bridge_SetErrorFilter(int filter)
{
    g_debugFilter = filter;
}

void AmxModx_Bridge_LogDebug(const char* message, int level)
{
    if (!message)
        return;

    // 根据过滤器级别决定是否记录
    if (level < g_debugFilter)
        return;

    // 记录到AMX Mod X日志系统
    switch (level)
    {
        case 0: // 调试
            MF_Log("[DEBUG] %s", message);
            break;
        case 1: // 信息
            MF_Log("[INFO] %s", message);
            break;
        case 2: // 警告
            MF_Log("[WARNING] %s", message);
            break;
        case 3: // 错误
            MF_Log("[ERROR] %s", message);
            strncpy(g_lastError, message, sizeof(g_lastError) - 1);
            g_lastError[sizeof(g_lastError) - 1] = '\0';
            break;
    }
}

const char* AmxModx_Bridge_GetLastError()
{
    return g_lastError;
}

void AmxModx_Bridge_ClearLastError()
{
    g_lastError[0] = '\0';
}

void AmxModx_Bridge_SetDebugMode(bool enabled)
{
    g_debugMode = enabled;
}

bool AmxModx_Bridge_IsDebugMode()
{
    return g_debugMode;
}

bool AmxModx_Bridge_SetBreakpoint(const char* file, int line)
{
    if (!file || line <= 0)
        return false;

    // 这里应该实现实际的断点设置逻辑
    // 由于AMX Mod X的调试器实现较复杂，这里返回模拟值
    return true;
}

bool AmxModx_Bridge_RemoveBreakpoint(const char* file, int line)
{
    if (!file || line <= 0)
        return false;

    // 这里应该实现实际的断点移除逻辑
    return true;
}

int AmxModx_Bridge_GetCallStack(char* buffer, int bufferSize)
{
    if (!buffer || bufferSize <= 0)
        return 0;

    // 获取当前AMX调用栈信息
    CPluginMngr::CPlugin* plugin = g_plugins.GetRunningPlugin();
    if (!plugin || !plugin->getAMX())
    {
        strncpy(buffer, "No active plugin", bufferSize - 1);
        buffer[bufferSize - 1] = '\0';
        return strlen(buffer);
    }

    AMX* amx = plugin->getAMX();
    
    // 构建调用栈信息字符串
    char stackInfo[512];
    snprintf(stackInfo, sizeof(stackInfo), 
             "Plugin: %s\nAMX: 0x%p\nFrame: %d", 
             plugin->getName(), 
             (void*)amx, 
             amx->stk);

    strncpy(buffer, stackInfo, bufferSize - 1);
    buffer[bufferSize - 1] = '\0';
    
    return strlen(buffer);
}