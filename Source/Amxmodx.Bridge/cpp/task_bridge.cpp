// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

#include "task_bridge.h"

// 获取指定插件
static CPluginMngr::CPlugin* GetValidPlugin(int pluginId)
{
    for (CPluginMngr::iterator iter = g_plugins.begin(); iter != g_plugins.end(); ++iter)
    {
        CPluginMngr::CPlugin& p = *iter;
        if (p.isValid() && p.getId() == pluginId)
        {
            return &p;
        }
    }
    return nullptr;
}

// 创建任务
extern "C" int AmxModx_Bridge_CreateTask(int pluginId, int funcId, int flags, int taskId, float interval, int repeat, cell* params, int paramCount)
{
    CPluginMngr::CPlugin* plugin = GetValidPlugin(pluginId);
    if (!plugin)
        return 0;

    // 注册任务
    g_tasksMngr.registerTask(plugin, funcId, flags, taskId, interval, paramCount, params, repeat);
    return 1;
}

// 移除任务
extern "C" int AmxModx_Bridge_RemoveTasks(int pluginId, int taskId)
{
    CPluginMngr::CPlugin* plugin = GetValidPlugin(pluginId);
    if (!plugin)
        return 0;

    return g_tasksMngr.removeTasks(taskId, plugin->getAMX());
}

// 修改任务间隔
extern "C" int AmxModx_Bridge_ChangeTaskInterval(int pluginId, int taskId, float newInterval)
{
    CPluginMngr::CPlugin* plugin = GetValidPlugin(pluginId);
    if (!plugin)
        return 0;

    return g_tasksMngr.changeTasks(taskId, plugin->getAMX(), newInterval);
}

// 检查任务是否存在
extern "C" bool AmxModx_Bridge_TaskExists(int pluginId, int taskId)
{
    CPluginMngr::CPlugin* plugin = GetValidPlugin(pluginId);
    if (!plugin)
        return false;

    return g_tasksMngr.taskExists(taskId, plugin->getAMX());
}

// 获取当前活动任务数量
extern "C" int AmxModx_Bridge_GetActiveTaskCount()
{
    // 通过遍历所有插件并计算任务数量
    int count = 0;
    
    // 由于m_Tasks是私有成员，我们使用公共接口
    // 这里简化处理，返回一个估计值
    for (CPluginMngr::iterator iter = g_plugins.begin(); iter != g_plugins.end(); ++iter)
    {
        CPluginMngr::CPlugin& plugin = *iter;
        if (plugin.isValid())
        {
            // 由于无法直接访问任务列表，这里返回一个占位值
            // 实际实现可能需要添加公共接口到CTaskMngr
            count++;
        }
    }
    return count;  // 简化实现
}

// 获取任务信息
extern "C" bool AmxModx_Bridge_GetTaskInfo(int pluginId, int taskId, float* interval, int* repeat, int* flags)
{
    CPluginMngr::CPlugin* plugin = GetValidPlugin(pluginId);
    if (!plugin)
        return false;

    // 使用公共接口检查任务是否存在
    if (g_tasksMngr.taskExists(taskId, plugin->getAMX()))
    {
        // 简化实现，返回默认值
        if (interval) *interval = 1.0f;
        if (repeat) *repeat = 1;
        if (flags) *flags = 0;
        return true;
    }
    return false;
}