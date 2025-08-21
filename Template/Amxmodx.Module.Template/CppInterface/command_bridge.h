// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

#ifndef COMMAND_BRIDGE_H
#define COMMAND_BRIDGE_H

#ifdef __cplusplus
extern "C" {
#endif

/**
 * 命令类型枚举
 */
enum CommandType
{
    CMD_TYPE_CONSOLE = 0,    // 控制台命令
    CMD_TYPE_CLIENT = 1,      // 客户端命令
    CMD_TYPE_SERVER = 2       // 服务器命令
};

/**
 * 命令标志位
 */
enum CommandFlags
{
    CMD_FLAG_ADMIN = 1,       // 管理员命令
    CMD_FLAG_RCON = 2,        // RCON命令
    CMD_FLAG_PUBLIC = 4,      // 公共命令
    CMD_FLAG_HIDDEN = 8       // 隐藏命令
};

/**
 * 注册控制台命令
 * @param pluginId 插件ID
 * @param funcId 函数ID
 * @param cmd 命令名称
 * @param info 命令描述
 * @param flags 命令标志位
 * @param listable 是否可列出
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_RegisterConsoleCommand(
    int pluginId,
    int funcId,
    const char* cmd,
    const char* info,
    int flags,
    bool listable);

/**
 * 注册客户端命令
 * @param pluginId 插件ID
 * @param funcId 函数ID
 * @param cmd 命令名称
 * @param info 命令描述
 * @param flags 命令标志位
 * @param listable 是否可列出
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_RegisterClientCommand(
    int pluginId,
    int funcId,
    const char* cmd,
    const char* info,
    int flags,
    bool listable);

/**
 * 注册服务器命令
 * @param pluginId 插件ID
 * @param funcId 函数ID
 * @param cmd 命令名称
 * @param info 命令描述
 * @param flags 命令标志位
 * @param listable 是否可列出
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_RegisterServerCommand(
    int pluginId,
    int funcId,
    const char* cmd,
    const char* info,
    int flags,
    bool listable);

/**
 * 获取命令数量
 * @param type 命令类型
 * @param access 访问级别
 * @return 命令数量
 */
int AmxModx_Bridge_GetCommandCount(int type, int access);

/**
 * 获取命令信息
 * @param type 命令类型
 * @param index 命令索引
 * @param access 访问级别
 * @param cmd 返回命令名称缓冲区
 * @param cmdSize 命令名称缓冲区大小
 * @param info 返回命令描述缓冲区
 * @param infoSize 命令描述缓冲区大小
 * @param flags 返回命令标志位
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_GetCommandInfo(
    int type,
    int index,
    int access,
    char* cmd,
    int cmdSize,
    char* info,
    int infoSize,
    int* flags);

/**
 * 执行控制台命令
 * @param cmd 命令字符串
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_ExecuteConsoleCommand(const char* cmd);

/**
 * 执行客户端命令
 * @param playerId 玩家ID
 * @param cmd 命令字符串
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_ExecuteClientCommand(int playerId, const char* cmd);

/**
 * 执行服务器命令
 * @param cmd 命令字符串
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_ExecuteServerCommand(const char* cmd);

/**
 * 检查命令是否存在
 * @param type 命令类型
 * @param cmd 命令名称
 * @return 存在返回true，否则返回false
 */
bool AmxModx_Bridge_CommandExists(int type, const char* cmd);

/**
 * 移除命令
 * @param pluginId 插件ID
 * @param cmd 命令名称
 * @return 移除的命令数量
 */
int AmxModx_Bridge_RemoveCommands(int pluginId, const char* cmd);

#ifdef __cplusplus
}
#endif

#endif // COMMAND_BRIDGE_H