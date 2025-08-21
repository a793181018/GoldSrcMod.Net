//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license
//

#ifndef FILE_BRIDGE_H
#define FILE_BRIDGE_H

#include <stddef.h>

// 前向声明
class CDataPack;
class IGameConfig;
class Vault;
struct cvar_s;
typedef struct cvar_s cvar_t;

#ifdef __cplusplus
extern "C" {
#endif

// 文件系统桥接接口
/**
 * 读取文件内容到内存缓冲区
 * @param filePath 文件路径
 * @param size 返回文件大小
 * @return 文件内容缓冲区，需要手动释放
 */
char* AmxModx_Bridge_ReadFile(const char* filePath, int* size);

/**
 * 将数据写入文件
 * @param filePath 文件路径
 * @param data 要写入的数据
 * @param size 数据大小
 * @return 写入是否成功
 */
bool AmxModx_Bridge_WriteFile(const char* filePath, const char* data, int size);

/**
 * 释放由ReadFile分配的缓冲区
 * @param buffer 要释放的缓冲区
 */
void AmxModx_Bridge_FreeBuffer(char* buffer);

/**
 * 检查文件是否存在
 * @param fileName 文件名
 * @return 文件是否存在
 */
bool AmxModx_Bridge_FileExists(const char* fileName);

/**
 * 从文件读取字符串内容
 * @param fileName 文件名
 * @param buffer 输出缓冲区
 * @param bufferSize 缓冲区大小
 * @return 读取是否成功
 */
bool AmxModx_Bridge_ReadFileString(const char* fileName, char* buffer, int bufferSize);

/**
 * 将字符串写入文件
 * @param fileName 文件名
 * @param content 要写入的内容
 * @return 写入是否成功
 */
bool AmxModx_Bridge_WriteFileString(const char* fileName, const char* content);

/**
 * 删除文件
 * @param fileName 文件名
 * @return 删除是否成功
 */
bool AmxModx_Bridge_DeleteFile(const char* fileName);

// CDataPack桥接接口
/**
 * 创建新的数据包
 * @return 新创建的数据包指针
 */
CDataPack* AmxModx_Bridge_CreateDataPack();

/**
 * 销毁数据包
 * @param pack 要销毁的数据包
 */
void AmxModx_Bridge_DestroyDataPack(CDataPack* pack);

/**
 * 获取数据包当前位置
 * @param pack 数据包指针
 * @return 当前位置
 */
int AmxModx_Bridge_GetDataPackPosition(CDataPack* pack);

/**
 * 设置数据包位置
 * @param pack 数据包指针
 * @param position 要设置的位置
 */
void AmxModx_Bridge_SetDataPackPosition(CDataPack* pack, int position);

/**
 * 向数据包写入整数
 * @param pack 数据包指针
 * @param value 要写入的值
 * @return 写入是否成功
 */
bool AmxModx_Bridge_WriteDataPackCell(CDataPack* pack, int value);

/**
 * 向数据包写入浮点数
 * @param pack 数据包指针
 * @param value 要写入的值
 * @return 写入是否成功
 */
bool AmxModx_Bridge_WriteDataPackFloat(CDataPack* pack, float value);

/**
 * 向数据包写入字符串
 * @param pack 数据包指针
 * @param value 要写入的字符串
 * @return 写入是否成功
 */
bool AmxModx_Bridge_WriteDataPackString(CDataPack* pack, const char* value);

/**
 * 从数据包读取整数
 * @param pack 数据包指针
 * @return 读取的整数值
 */
int AmxModx_Bridge_ReadDataPackCell(CDataPack* pack);

/**
 * 从数据包读取浮点数
 * @param pack 数据包指针
 * @return 读取的浮点数值
 */
float AmxModx_Bridge_ReadDataPackFloat(CDataPack* pack);

/**
 * 从数据包读取字符串
 * @param pack 数据包指针
 * @return 读取的字符串（静态缓冲区）
 */
const char* AmxModx_Bridge_ReadDataPackString(CDataPack* pack);

// 控制台变量桥接接口
/**
 * 创建控制台变量
 * @param name 变量名
 * @param value 初始值
 * @param flags 变量标志
 * @return 创建的变量指针
 */
cvar_t* AmxModx_Bridge_CreateCVar(const char* name, const char* value, int flags);

/**
 * 查找控制台变量
 * @param name 变量名
 * @return 找到的变量指针
 */
cvar_t* AmxModx_Bridge_FindCVar(const char* name);

/**
 * 获取控制台变量字符串值
 * @param cvar 变量指针
 * @return 字符串值
 */
const char* AmxModx_Bridge_GetCVarString(cvar_t* cvar);

/**
 * 获取控制台变量浮点数值
 * @param cvar 变量指针
 * @return 浮点数值
 */
float AmxModx_Bridge_GetCVarFloat(cvar_t* cvar);

/**
 * 获取控制台变量整数值
 * @param cvar 变量指针
 * @return 整数值
 */
int AmxModx_Bridge_GetCVarInt(cvar_t* cvar);

/**
 * 设置控制台变量字符串值
 * @param cvar 变量指针
 * @param value 要设置的字符串值
 */
void AmxModx_Bridge_SetCVarString(cvar_t* cvar, const char* value);

/**
 * 设置控制台变量浮点数值
 * @param cvar 变量指针
 * @param value 要设置的浮点数值
 */
void AmxModx_Bridge_SetCVarFloat(cvar_t* cvar, float value);

/**
 * 设置控制台变量整数值
 * @param cvar 变量指针
 * @param value 要设置的整数值
 */
void AmxModx_Bridge_SetCVarInt(cvar_t* cvar, int value);

// 游戏配置桥接接口
/**
 * 加载游戏配置文件
 * @param filePath 配置文件路径
 * @return 加载的配置对象指针
 */
IGameConfig* AmxModx_Bridge_LoadGameConfig(const char* filePath);

/**
 * 卸载游戏配置文件
 * @param config 配置对象指针
 */
void AmxModx_Bridge_UnloadGameConfig(IGameConfig* config);

/**
 * 获取配置值
 * @param config 配置对象指针
 * @param key 配置键名
 * @return 配置值字符串
 */
const char* AmxModx_Bridge_GetConfigValue(IGameConfig* config, const char* key);

// 持久化存储桥接接口
/**
 * 获取全局Vault实例
 * @return Vault实例指针
 */
Vault* AmxModx_Bridge_GetVault();

/**
 * 检查键是否存在
 * @param vault Vault实例指针
 * @param key 键名
 * @return 键是否存在
 */
bool AmxModx_Bridge_VaultExists(Vault* vault, const char* key);

/**
 * 存储键值对
 * @param vault Vault实例指针
 * @param key 键名
 * @param value 值
 * @return 存储是否成功
 */
bool AmxModx_Bridge_VaultPut(Vault* vault, const char* key, const char* value);

/**
 * 移除键值对
 * @param vault Vault实例指针
 * @param key 键名
 * @return 移除是否成功
 */
bool AmxModx_Bridge_VaultRemove(Vault* vault, const char* key);

/**
 * 获取键对应的值
 * @param key 键名
 * @return 值字符串
 */
const char* AmxModx_Bridge_VaultGet(const char* key);

#ifdef __cplusplus
}
#endif

#endif // FILE_BRIDGE_H