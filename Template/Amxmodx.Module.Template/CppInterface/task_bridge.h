// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

#ifndef TASK_BRIDGE_H
#define TASK_BRIDGE_H

#include "amxmodx.h"

// 任务标志位定义
#define TASK_FLAG_REPEAT 1
#define TASK_FLAG_LOOP 2
#define TASK_FLAG_AFTER_START 4
#define TASK_FLAG_BEFORE_END 8

// 任务调度系统桥接接口
extern "C" {
    // 创建任务
    int AmxModx_Bridge_CreateTask(int pluginId, int funcId, int flags, int taskId, float interval, int repeat, cell* params, int paramCount);
    
    // 移除任务
    int AmxModx_Bridge_RemoveTasks(int pluginId, int taskId);
    
    // 修改任务间隔
    int AmxModx_Bridge_ChangeTaskInterval(int pluginId, int taskId, float newInterval);
    
    // 检查任务是否存在
    bool AmxModx_Bridge_TaskExists(int pluginId, int taskId);
    
    // 获取当前活动任务数量
    int AmxModx_Bridge_GetActiveTaskCount();
    
    // 获取任务信息
    bool AmxModx_Bridge_GetTaskInfo(int pluginId, int taskId, float* interval, int* repeat, int* flags);
}

#endif // TASK_BRIDGE_H