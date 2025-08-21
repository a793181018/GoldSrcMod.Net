// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

#ifndef _FORWARD_BRIDGE_H_
#define _FORWARD_BRIDGE_H_

#ifdef __cplusplus
extern "C" {
#endif

/**
 * 创建多插件转发器
 * @param funcName 转发器名称
 * @param execType 执行类型 (0=忽略, 1=停止, 2=停止2, 3=继续)
 * @param numParams 参数数量
 * @param paramTypes 参数类型数组
 * @return 转发器ID，失败返回-1
 */
int AmxModx_Bridge_CreateMultiForward(const char* funcName, int execType, int numParams, int* paramTypes);

/**
 * 创建单插件转发器
 * @param pluginId 插件ID
 * @param funcName 函数名称
 * @param numParams 参数数量
 * @param paramTypes 参数类型数组
 * @return 转发器ID，失败返回-1
 */
int AmxModx_Bridge_CreateOneForward(int pluginId, const char* funcName, int numParams, int* paramTypes);

/**
 * 销毁转发器
 * @param forwardId 转发器ID
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_DestroyForward(int forwardId);

/**
 * 执行转发器
 * @param forwardId 转发器ID
 * @param params 参数数组
 * @param numParams 参数数量
 * @return 执行结果
 */
int AmxModx_Bridge_ExecuteForward(int forwardId, int* params, int numParams);

/**
 * 准备数组参数
 * @param arrayData 数组数据
 * @param size 数组大小
 * @param copyBack 是否复制回数据
 * @return 数组句柄
 */
int AmxModx_Bridge_PrepareArray(int* arrayData, int size, int copyBack);

/**
 * 获取转发器参数数量
 * @param forwardId 转发器ID
 * @return 参数数量，失败返回-1
 */
int AmxModx_Bridge_GetForwardParamCount(int forwardId);

/**
 * 获取转发器参数类型
 * @param forwardId 转发器ID
 * @param paramIndex 参数索引
 * @return 参数类型，失败返回-1
 */
int AmxModx_Bridge_GetForwardParamType(int forwardId, int paramIndex);

#ifdef __cplusplus
}
#endif

#endif // _FORWARD_BRIDGE_H_