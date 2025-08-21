// gameconfigs_bridge.h
// 游戏配置系统桥接接口定义
#pragma once

#include "../amxmodx.h"
#include "../gameconfigs.h"

#ifdef __cplusplus
extern "C" {
#endif

/**
 * 加载游戏配置文件
 * @param fileName 配置文件名
 * @param errorBuffer 错误信息缓冲区
 * @param errorBufferSize 缓冲区大小
 * @return 游戏配置句柄，失败返回0
 */
int AmxModx_Bridge_LoadGameConfigFile(const char* fileName, char* errorBuffer, int errorBufferSize);

/**
 * 获取游戏配置偏移量
 * @param handle 游戏配置句柄
 * @param key 配置键名
 * @return 偏移量值，失败返回-1
 */
int AmxModx_Bridge_GameConfGetOffset(int handle, const char* key);

/**
 * 获取类配置偏移量
 * @param handle 游戏配置句柄
 * @param className 类名
 * @param key 配置键名
 * @return 偏移量值，失败返回-1
 */
int AmxModx_Bridge_GameConfGetClassOffset(int handle, const char* className, const char* key);

/**
 * 获取游戏配置键值
 * @param handle 游戏配置句柄
 * @param key 配置键名
 * @param buffer 输出缓冲区
 * @param bufferSize 缓冲区大小
 * @return 是否成功获取
 */
int AmxModx_Bridge_GameConfGetKeyValue(int handle, const char* key, char* buffer, int bufferSize);

/**
 * 获取游戏配置地址
 * @param handle 游戏配置句柄
 * @param name 地址名
 * @return 地址值，失败返回0
 */
uintptr_t AmxModx_Bridge_GameConfGetAddress(int handle, const char* name);

/**
 * 关闭游戏配置文件
 * @param handle 游戏配置句柄（传引用，成功后会置0）
 * @return 是否成功关闭
 */
int AmxModx_Bridge_CloseGameConfigFile(int* handle);

#ifdef __cplusplus
}
#endif