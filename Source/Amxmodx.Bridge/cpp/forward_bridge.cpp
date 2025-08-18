// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

#include "forward_bridge.h"
#include "amxmodx.h"
#include "CForward.h"
#include "CPlugin.h"

// 参数类型映射
static ForwardParam ConvertToForwardParam(int type) {
    switch (type) {
        case 0: return FP_CELL;
        case 1: return FP_FLOAT;
        case 2: return FP_STRING;
        case 3: return FP_STRINGEX;
        case 4: return FP_ARRAY;
        case 5: return FP_CELL_BYREF;
        case 6: return FP_FLOAT_BYREF;
        default: return FP_DONE;
    }
}

// 执行类型映射
static ForwardExecType ConvertToExecType(int type) {
    switch (type) {
        case 0: return ET_IGNORE;
        case 1: return ET_STOP;
        case 2: return ET_STOP2;
        case 3: return ET_CONTINUE;
        default: return ET_IGNORE;
    }
}

/**
 * 创建多插件转发器
 */
extern "C" int AmxModx_Bridge_CreateMultiForward(const char* funcName, int execType, int numParams, int* paramTypes) {
    if (!funcName || numParams < 0 || numParams > FORWARD_MAX_PARAMS)
        return -1;

    ForwardParam params[FORWARD_MAX_PARAMS];
    for (int i = 0; i < numParams; i++) {
        params[i] = ConvertToForwardParam(paramTypes[i]);
    }

    int forwardId = g_forwards.registerForward(funcName, ConvertToExecType(execType), numParams, params);
    return forwardId;
}

/**
 * 创建单插件转发器
 */
extern "C" int AmxModx_Bridge_CreateOneForward(int pluginId, const char* funcName, int numParams, int* paramTypes) {
    if (!funcName || numParams < 0 || numParams > FORWARD_MAX_PARAMS)
        return -1;

    // 查找插件
    CPluginMngr::CPlugin* plugin = nullptr;
    for (CPluginMngr::iterator iter = g_plugins.begin(); iter != g_plugins.end(); ++iter) {
        CPluginMngr::CPlugin& p = *iter;
        if (p.isValid() && p.getId() == pluginId) {
            plugin = &p;
            break;
        }
    }

    if (!plugin)
        return -1;

    ForwardParam params[FORWARD_MAX_PARAMS];
    for (int i = 0; i < numParams; i++) {
        params[i] = ConvertToForwardParam(paramTypes[i]);
    }

    int forwardId = g_forwards.registerSPForward(funcName, plugin->getAMX(), numParams, params);
    return forwardId;
}

/**
 * 销毁转发器
 */
extern "C" int AmxModx_Bridge_DestroyForward(int forwardId) {
    if (forwardId < 0)
        return 0;

    // 检查是否为单插件转发器
    if (forwardId & 1) {
        g_forwards.unregisterSPForward(forwardId);
        return 1;
    }

    // 多插件转发器不支持销毁
    return 0;
}

/**
 * 执行转发器
 */
extern "C" int AmxModx_Bridge_ExecuteForward(int forwardId, int* params, int numParams) {
    if (forwardId < 0)
        return 0;

    cell* cellParams = nullptr;
    if (numParams > 0 && params) {
        cellParams = new cell[numParams];
        for (int i = 0; i < numParams; i++) {
            cellParams[i] = static_cast<cell>(params[i]);
        }
    }

    cell result = g_forwards.executeForwards(forwardId, cellParams);

    if (cellParams) {
        delete[] cellParams;
    }

    return static_cast<int>(result);
}

/**
 * 准备数组参数
 */
extern "C" int AmxModx_Bridge_PrepareArray(int* arrayData, int size, int copyBack) {
    if (!arrayData || size <= 0)
        return 0;

    // 简化实现：返回数组地址作为句柄
    return reinterpret_cast<int>(arrayData);
}

/**
 * 获取转发器参数数量
 */
extern "C" int AmxModx_Bridge_GetForwardParamCount(int forwardId) {
    if (forwardId < 0)
        return -1;

    // 简化实现，返回-1表示不支持
    return -1;
}

/**
 * 获取转发器参数类型
 */
extern "C" int AmxModx_Bridge_GetForwardParamType(int forwardId, int paramIndex) {
    if (forwardId < 0 || paramIndex < 0)
        return -1;

    // 简化实现，返回-1表示不支持
    return -1;
}