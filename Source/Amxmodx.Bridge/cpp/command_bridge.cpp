// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

#include "command_bridge.h"
#include "amxmodx.h"

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

// 注册控制台命令
extern "C" int AmxModx_Bridge_RegisterConsoleCommand(
    int pluginId,
    int funcId,
    const char* cmd,
    const char* info,
    int flags,
    bool listable)
{
    CPluginMngr::CPlugin* plugin = GetValidPlugin(pluginId);
    if (!plugin || !cmd || !info)
        return 0;

    // 注册控制台命令
    CmdMngr::Command* command = g_commands.registerCommand(
        plugin, funcId, cmd, info, flags, listable, false);
    
    if (command)
    {
        command->setCmdType(CMD_ConsoleCommand);
        return 1;
    }
    return 0;
}

// 注册客户端命令
extern "C" int AmxModx_Bridge_RegisterClientCommand(
    int pluginId,
    int funcId,
    const char* cmd,
    const char* info,
    int flags,
    bool listable)
{
    CPluginMngr::CPlugin* plugin = GetValidPlugin(pluginId);
    if (!plugin || !cmd || !info)
        return 0;

    // 注册客户端命令
    CmdMngr::Command* command = g_commands.registerCommand(
        plugin, funcId, cmd, info, flags, listable, false);
    
    if (command)
    {
        command->setCmdType(CMD_ClientCommand);
        return 1;
    }
    return 0;
}

// 注册服务器命令
extern "C" int AmxModx_Bridge_RegisterServerCommand(
    int pluginId,
    int funcId,
    const char* cmd,
    const char* info,
    int flags,
    bool listable)
{
    CPluginMngr::CPlugin* plugin = GetValidPlugin(pluginId);
    if (!plugin || !cmd || !info)
        return 0;

    // 注册服务器命令
    CmdMngr::Command* command = g_commands.registerCommand(
        plugin, funcId, cmd, info, flags, listable, false);
    
    if (command)
    {
        command->setCmdType(CMD_ServerCommand);
        return 1;
    }
    return 0;
}

// 获取命令数量
extern "C" int AmxModx_Bridge_GetCommandCount(int type, int access)
{
    return g_commands.getCmdNum(type, access);
}

// 获取命令信息
extern "C" int AmxModx_Bridge_GetCommandInfo(
    int type,
    int index,
    int access,
    char* cmd,
    int cmdSize,
    char* info,
    int infoSize,
    int* flags)
{
    if (!cmd || !info || !flags)
        return 0;

    CmdMngr::Command* command = g_commands.getCmd(index, type, access);
    if (!command)
        return 0;

    // 复制命令名称
    const char* cmdName = command->getCommand();
    if (cmdName)
    {
        strncpy(cmd, cmdName, cmdSize - 1);
        cmd[cmdSize - 1] = '\0';
    }

    // 复制命令描述
    const char* cmdInfo = command->getCmdInfo();
    if (cmdInfo)
    {
        strncpy(info, cmdInfo, infoSize - 1);
        info[infoSize - 1] = '\0';
    }

    // 设置标志位
    *flags = command->getFlags();
    return 1;
}

// 执行控制台命令
extern "C" int AmxModx_Bridge_ExecuteConsoleCommand(const char* cmd)
{
    if (!cmd)
        return 0;

    // 使用服务器命令执行
    SERVER_COMMAND(cmd);
    SERVER_EXECUTE();
    return 1;
}

// 执行客户端命令
extern "C" int AmxModx_Bridge_ExecuteClientCommand(int playerId, const char* cmd)
{
    if (!cmd || playerId < 1 || playerId > gpGlobals->maxClients)
        return 0;

    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free)
        return 0;

    CLIENT_COMMAND(pEdict, cmd);
    return 1;
}

// 执行服务器命令
extern "C" int AmxModx_Bridge_ExecuteServerCommand(const char* cmd)
{
    if (!cmd)
        return 0;

    // 使用服务器命令执行
    SERVER_COMMAND(cmd);
    SERVER_EXECUTE();
    return 1;
}

// 检查命令是否存在
extern "C" bool AmxModx_Bridge_CommandExists(int type, const char* cmd)
{
    if (!cmd)
        return false;

    // 遍历命令列表查找
    for (CmdMngr::iterator iter = g_commands.begin(type); iter; ++iter)
    {
        if ((*iter).matchCommand(cmd))
        {
            return true;
        }
    }
    return false;
}

// 移除命令
extern "C" int AmxModx_Bridge_RemoveCommands(int pluginId, const char* cmd)
{
    CPluginMngr::CPlugin* plugin = GetValidPlugin(pluginId);
    if (!plugin)
        return 0;

    // CmdMngr没有提供移除单个命令的方法，只能清除所有命令
    // 这里暂时返回0表示没有移除任何命令
    // 如果需要实现命令移除功能，需要修改CmdMngr类
    return 0;
}