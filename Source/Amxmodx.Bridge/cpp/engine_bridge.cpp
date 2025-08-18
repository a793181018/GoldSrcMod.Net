// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X Engine Bridge Implementation
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

#include "engine_bridge.h"
#include "amxmodx.h"
#include <cstring>

extern "C" {

// 服务器信息
const char* AmxModx_Bridge_GetMapName(void)
{
    return STRING(gpGlobals->mapname);
}

const char* AmxModx_Bridge_GetModName(void)
{
    extern ke::AString g_mod_name;
    return g_mod_name.chars();
}

const char* AmxModx_Bridge_GetGameDescription(void)
{
    extern ke::AString g_mod_name;
    return g_mod_name.chars();
}

int AmxModx_Bridge_GetGameTime(void)
{
    return (int)gpGlobals->time;
}

int AmxModx_Bridge_GetGameTimeLeft(void)
{
    // 使用mp_timelimit cvar计算剩余时间
    extern cvar_t* mp_timelimit;
    if (mp_timelimit && mp_timelimit->value > 0)
    {
        float timeLeft = mp_timelimit->value * 60.0f - gpGlobals->time;
        return (int)(timeLeft > 0 ? timeLeft : 0);
    }
    return 0;
}

int AmxModx_Bridge_GetServerTick(void)
{
    return (int)gpGlobals->time * 1000; // 使用时间作为tick的近似值
}

bool AmxModx_Bridge_IsDedicatedServer(void)
{
    // 使用IS_DEDICATED_SERVER宏判断是否为专用服务器
    return IS_DEDICATED_SERVER() != 0;
}

bool AmxModx_Bridge_IsMapValid(const char* mapName)
{
    if (!mapName)
        return false;
    
    return IS_MAP_VALID(const_cast<char*>(mapName)) != 0;
}

// 控制台和日志
void AmxModx_Bridge_ServerPrint(const char* message)
{
    if (!message)
        return;
    
    SERVER_PRINT(const_cast<char*>(message));
}

void AmxModx_Bridge_ServerCommand(const char* command)
{
    if (!command)
        return;
    
    SERVER_COMMAND(const_cast<char*>(command));
}

void AmxModx_Bridge_ClientCommand(int playerId, const char* command)
{
    if (!command || playerId < 1 || playerId > gpGlobals->maxClients)
        return;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    if (!pPlayer || !pPlayer->ingame)
        return;
    
    CLIENT_COMMAND(pPlayer->pEdict, "%s", command);
}

void AmxModx_Bridge_LogMessage(const char* message)
{
    if (!message)
        return;
    
    ALERT(at_logged, "%s", message);
}

// LogError函数已在natives_bridge.cpp中定义，这里移除

// 声音和特效
void AmxModx_Bridge_EmitSound(int entityId, const char* sound, float volume, float attenuation, int channel, int pitch)
{
    if (!sound)
        return;
    
    edict_t* pEdict = nullptr;
    
    if (entityId == 0)
    {
        // 对所有玩家播放
        for (int i = 1; i <= gpGlobals->maxClients; ++i)
        {
            CPlayer* pPlayer = GET_PLAYER_POINTER_I(i);
            if (pPlayer && pPlayer->ingame)
            {
                EMIT_SOUND_DYN2(pPlayer->pEdict, channel, const_cast<char*>(sound), volume, attenuation, 0, pitch);
            }
        }
    }
    else if (entityId > 0 && entityId <= gpGlobals->maxEntities)
    {
        pEdict = TypeConversion.id_to_edict(entityId);
        if (pEdict)
        {
            EMIT_SOUND_DYN2(pEdict, channel, const_cast<char*>(sound), volume, attenuation, 0, pitch);
        }
    }
}

void AmxModx_Bridge_PrecacheSound(const char* sound)
{
    if (!sound)
        return;
    
    PRECACHE_SOUND(const_cast<char*>(sound));
}

void AmxModx_Bridge_PrecacheModel(const char* model)
{
    if (!model)
        return;
    
    PRECACHE_MODEL(const_cast<char*>(model));
}

void AmxModx_Bridge_PrecacheGeneric(const char* resource)
{
    if (!resource)
        return;
    
    PRECACHE_GENERIC(const_cast<char*>(resource));
}

// 地图和实体
int AmxModx_Bridge_CreateEntity(const char* className)
{
    if (!className)
        return 0;
    
    edict_t* pEdict = CREATE_NAMED_ENTITY(MAKE_STRING(className));
    if (!pEdict)
        return 0;
    
    return TypeConversion.edict_to_id(pEdict);
}

bool AmxModx_Bridge_RemoveEntity(int entityId)
{
    if (entityId <= 0 || entityId > gpGlobals->maxEntities)
        return false;
    
    edict_t* pEdict = TypeConversion.id_to_edict(entityId);
    if (!pEdict || pEdict->free)
        return false;
    
    REMOVE_ENTITY(pEdict);
    return true;
}

bool AmxModx_Bridge_IsEntityValid(int entityId)
{
    if (entityId <= 0 || entityId > gpGlobals->maxEntities)
        return false;
    
    edict_t* pEdict = TypeConversion.id_to_edict(entityId);
    return (pEdict != nullptr && !pEdict->free);
}

const char* AmxModx_Bridge_GetEntityClassName(int entityId)
{
    if (!AmxModx_Bridge_IsEntityValid(entityId))
        return nullptr;
    
    edict_t* pEdict = TypeConversion.id_to_edict(entityId);
    return STRING(pEdict->v.classname);
}

int AmxModx_Bridge_FindEntityByClassName(const char* className)
{
    if (!className)
        return 0;
    
    edict_t* pEdict = nullptr;
    while ((pEdict = FIND_ENTITY_BY_CLASSNAME(pEdict, const_cast<char*>(className))) != nullptr)
    {
        if (!pEdict->free)
            return TypeConversion.edict_to_id(pEdict);
    }
    
    return 0;
}

// 全局变量和配置
const char* AmxModx_Bridge_GetLocalInfo(const char* key)
{
    if (!key)
        return nullptr;
    
    static char buffer[256];
    const char* value = LOCALINFO(const_cast<char*>(key));
    if (value)
    {
        strncpy(buffer, value, sizeof(buffer) - 1);
        buffer[sizeof(buffer) - 1] = '\0';
        return buffer;
    }
    
    return nullptr;
}

bool AmxModx_Bridge_SetLocalInfo(const char* key, const char* value)
{
    if (!key || !value)
        return false;
    
    SET_LOCALINFO(const_cast<char*>(key), const_cast<char*>(value));
    return true;
}

const char* AmxModx_Bridge_GetCvarString(const char* cvarName)
{
    if (!cvarName)
        return nullptr;
    
    cvar_t* cvar = CVAR_GET_POINTER(const_cast<char*>(cvarName));
    if (cvar)
        return cvar->string;
    
    return nullptr;
}

float AmxModx_Bridge_GetCvarFloat(const char* cvarName)
{
    if (!cvarName)
        return 0.0f;
    
    cvar_t* cvar = CVAR_GET_POINTER(const_cast<char*>(cvarName));
    if (cvar)
        return cvar->value;
    
    return 0.0f;
}

int AmxModx_Bridge_GetCvarInt(const char* cvarName)
{
    if (!cvarName)
        return 0;
    
    cvar_t* cvar = CVAR_GET_POINTER(const_cast<char*>(cvarName));
    if (cvar)
        return (int)cvar->value;
    
    return 0;
}

bool AmxModx_Bridge_SetCvarString(const char* cvarName, const char* value)
{
    if (!cvarName || !value)
        return false;
    
    cvar_t* cvar = CVAR_GET_POINTER(const_cast<char*>(cvarName));
    if (cvar)
    {
        CVAR_SET_STRING(const_cast<char*>(cvarName), const_cast<char*>(value));
        return true;
    }
    
    return false;
}

bool AmxModx_Bridge_SetCvarFloat(const char* cvarName, float value)
{
    if (!cvarName)
        return false;
    
    cvar_t* cvar = CVAR_GET_POINTER(const_cast<char*>(cvarName));
    if (cvar)
    {
        CVAR_SET_FLOAT(const_cast<char*>(cvarName), value);
        return true;
    }
    
    return false;
}

bool AmxModx_Bridge_SetCvarInt(const char* cvarName, int value)
{
    if (!cvarName)
        return false;
    
    cvar_t* cvar = CVAR_GET_POINTER(const_cast<char*>(cvarName));
    if (cvar)
    {
        CVAR_SET_FLOAT(const_cast<char*>(cvarName), (float)value);
        return true;
    }
    
    return false;
}

} // extern "C"