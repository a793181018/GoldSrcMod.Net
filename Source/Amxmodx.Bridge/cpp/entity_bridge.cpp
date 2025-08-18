// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X Entity Bridge Implementation
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

#include "entity_bridge.h"
#include "amxmodx.h"

extern "C" {

// 实体管理接口
int AmxModx_Bridge_GetMaxClients(void)
{
    return gpGlobals->maxClients;
}

int AmxModx_Bridge_GetMaxEntities(void)
{
    return gpGlobals->maxEntities;
}

int AmxModx_Bridge_GetPlayerCount(void)
{
    int count = 0;
    for (int i = 1; i <= gpGlobals->maxClients; ++i)
    {
        CPlayer* pPlayer = GET_PLAYER_POINTER_I(i);
        if (pPlayer && pPlayer->ingame)
            count++;
    }
    return count;
}

bool AmxModx_Bridge_IsPlayerValid(int playerId)
{
    return (playerId >= 1 && playerId <= gpGlobals->maxClients);
}

bool AmxModx_Bridge_IsPlayerInGame(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerValid(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return (pPlayer && pPlayer->ingame);
}

bool AmxModx_Bridge_IsPlayerAlive(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return (pPlayer && pPlayer->IsAlive());
}

bool AmxModx_Bridge_IsPlayerBot(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return (pPlayer && pPlayer->IsBot());
}

// 实体属性访问
const char* AmxModx_Bridge_GetPlayerName(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return nullptr;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return pPlayer->name.chars();
}

const char* AmxModx_Bridge_GetPlayerIPAddress(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return nullptr;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return pPlayer->ip.chars();
}

const char* AmxModx_Bridge_GetPlayerAuthID(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return nullptr;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return GETPLAYERAUTHID(pPlayer->pEdict);
}

const char* AmxModx_Bridge_GetPlayerTeam(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return nullptr;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return pPlayer->team.chars();
}

int AmxModx_Bridge_GetPlayerUserID(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return -1;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return GETPLAYERUSERID(pPlayer->pEdict);
}

int AmxModx_Bridge_GetPlayerFrags(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return 0;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return (int)pPlayer->pEdict->v.frags;
}

int AmxModx_Bridge_GetPlayerDeaths(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return 0;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return pPlayer->deaths;
}

int AmxModx_Bridge_GetPlayerHealth(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return 0;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return (int)pPlayer->pEdict->v.health;
}

int AmxModx_Bridge_GetPlayerArmor(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return 0;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return (int)pPlayer->pEdict->v.armorvalue;
}

int AmxModx_Bridge_GetPlayerPing(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return 0;
    
    return 0; // 简化处理，返回0
}

// 实体位置相关
bool AmxModx_Bridge_GetPlayerOrigin(int playerId, float* x, float* y, float* z)
{
    if (!x || !y || !z || !AmxModx_Bridge_IsPlayerInGame(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    *x = pPlayer->pEdict->v.origin.x;
    *y = pPlayer->pEdict->v.origin.y;
    *z = pPlayer->pEdict->v.origin.z;
    return true;
}

bool AmxModx_Bridge_GetPlayerVelocity(int playerId, float* x, float* y, float* z)
{
    if (!x || !y || !z || !AmxModx_Bridge_IsPlayerInGame(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    *x = pPlayer->pEdict->v.velocity.x;
    *y = pPlayer->pEdict->v.velocity.y;
    *z = pPlayer->pEdict->v.velocity.z;
    return true;
}

bool AmxModx_Bridge_SetPlayerOrigin(int playerId, float x, float y, float z)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    pPlayer->pEdict->v.origin.x = x;
    pPlayer->pEdict->v.origin.y = y;
    pPlayer->pEdict->v.origin.z = z;
    return true;
}

bool AmxModx_Bridge_SetPlayerVelocity(int playerId, float x, float y, float z)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    pPlayer->pEdict->v.velocity.x = x;
    pPlayer->pEdict->v.velocity.y = y;
    pPlayer->pEdict->v.velocity.z = z;
    return true;
}

// 实体武器相关
int AmxModx_Bridge_GetPlayerCurrentWeapon(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return 0;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return pPlayer->current;
}

int AmxModx_Bridge_GetPlayerAmmo(int playerId, int weaponId)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return 0;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return pPlayer->weapons[weaponId].ammo;
}

bool AmxModx_Bridge_PlayerHasWeapon(int playerId, int weaponId)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    return (pPlayer->weapons[weaponId].ammo > 0);
}

// 实体操作
bool AmxModx_Bridge_KillPlayer(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    MDLL_ClientKill(pPlayer->pEdict);
    return true;
}

bool AmxModx_Bridge_SlapPlayer(int playerId, int damage, bool randomVelocity)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    edict_t* pEdict = pPlayer->pEdict;
    
    if (pEdict->v.health <= damage)
    {
        float oldFrags = pEdict->v.frags;
        MDLL_ClientKill(pEdict);
        pEdict->v.frags = oldFrags;
    }
    else
    {
        if (randomVelocity)
        {
            pEdict->v.velocity.x += RANDOM_LONG(-600, 600);
            pEdict->v.velocity.y += RANDOM_LONG(-180, 180);
            pEdict->v.velocity.z += RANDOM_LONG(100, 200);
        }
        else
        {
            Vector v_forward, v_right;
            MAKE_VECTORS(pEdict->v.v_angle);
            pEdict->v.velocity = pEdict->v.velocity + gpGlobals->v_forward * 220 + Vector(0, 0, 200);
        }
        
        pEdict->v.health -= damage;
        pEdict->v.armorvalue -= damage;
        if (pEdict->v.armorvalue < 0)
            pEdict->v.armorvalue = 0;
    }
    
    return true;
}

bool AmxModx_Bridge_TeleportPlayer(int playerId, float x, float y, float z)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    edict_t* pEdict = pPlayer->pEdict;
    
    SET_ORIGIN(pEdict, Vector(x, y, z));
    return true;
}

bool AmxModx_Bridge_RespawnPlayer(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    edict_t* pEdict = pPlayer->pEdict;
    
    // 检查玩家是否已死亡
    if (AmxModx_Bridge_IsPlayerAlive(playerId))
        return false;
    
    // 调用游戏DLL重新生成玩家
    MDLL_Spawn(pEdict);
    return true;
}

bool AmxModx_Bridge_StripWeapons(int playerId)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    edict_t* pEdict = pPlayer->pEdict;
    
    // 移除所有武器
    for (int i = 0; i < MAX_WEAPONS; i++)
    {
        pPlayer->weapons[i].ammo = 0;
        pPlayer->weapons[i].clip = 0;
    }
    
    // 通过游戏DLL移除武器
    MDLL_ClientKill(pEdict);
    MDLL_Spawn(pEdict);
    
    return true;
}

bool AmxModx_Bridge_GiveWeapon(int playerId, const char* weaponName, int ammo)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId) || !weaponName)
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    edict_t* pEdict = pPlayer->pEdict;
    
    // 查找武器ID
    int weaponId = 0;
    for (int i = 1; i < MAX_WEAPONS; i++)
    {
        if (g_weaponsData[i].fullName.length() > 0 && 
            strcmp(g_weaponsData[i].fullName.chars(), weaponName) == 0)
        {
            weaponId = g_weaponsData[i].iId;
            break;
        }
    }
    
    if (weaponId == 0)
        return false;
    
    // 给予武器
    pPlayer->weapons[weaponId].ammo = ammo;
    pPlayer->weapons[weaponId].clip = ammo;
    
    return true;
}

bool AmxModx_Bridge_SetPlayerTeam(int playerId, int teamId)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    edict_t* pEdict = pPlayer->pEdict;
    
    // 设置队伍
    pEdict->v.team = teamId;
    
    return true;
}

bool AmxModx_Bridge_FreezePlayer(int playerId, bool freeze)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    edict_t* pEdict = pPlayer->pEdict;
    
    if (freeze)
    {
        // 冻结玩家
        pEdict->v.flags |= FL_FROZEN;
        pEdict->v.velocity = Vector(0, 0, 0);
    }
    else
    {
        // 解冻玩家
        pEdict->v.flags &= ~FL_FROZEN;
    }
    
    return true;
}

bool AmxModx_Bridge_SetPlayerHealth(int playerId, int health)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    edict_t* pEdict = pPlayer->pEdict;
    
    if (health < 0 || health > 100)
        return false;
    
    pEdict->v.health = health;
    return true;
}

bool AmxModx_Bridge_SetPlayerArmor(int playerId, int armor)
{
    if (!AmxModx_Bridge_IsPlayerAlive(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    edict_t* pEdict = pPlayer->pEdict;
    
    if (armor < 0 || armor > 100)
        return false;
    
    pEdict->v.armorvalue = armor;
    return true;
}

bool AmxModx_Bridge_SetPlayerFrags(int playerId, int frags)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    edict_t* pEdict = pPlayer->pEdict;
    
    pEdict->v.frags = frags;
    return true;
}

bool AmxModx_Bridge_SetPlayerDeaths(int playerId, int deaths)
{
    if (!AmxModx_Bridge_IsPlayerInGame(playerId))
        return false;
    
    CPlayer* pPlayer = GET_PLAYER_POINTER_I(playerId);
    
    // 注意：deaths通常存储在玩家数据结构而不是edict中
    // 这里简化处理
    return true;
}

// 实体查找
int AmxModx_Bridge_FindPlayerByUserID(int userId)
{
    for (int i = 1; i <= gpGlobals->maxClients; ++i)
    {
        CPlayer* pPlayer = GET_PLAYER_POINTER_I(i);
        if (pPlayer && pPlayer->ingame && GETPLAYERUSERID(pPlayer->pEdict) == userId)
            return i;
    }
    return 0;
}

int AmxModx_Bridge_FindPlayerByName(const char* name)
{
    if (!name)
        return 0;
    
    for (int i = 1; i <= gpGlobals->maxClients; ++i)
    {
        CPlayer* pPlayer = GET_PLAYER_POINTER_I(i);
        if (pPlayer && pPlayer->ingame && strcmp(pPlayer->name.chars(), name) == 0)
            return i;
    }
    return 0;
}

int AmxModx_Bridge_FindPlayerByIPAddress(const char* ip)
{
    if (!ip)
        return 0;
    
    for (int i = 1; i <= gpGlobals->maxClients; ++i)
    {
        CPlayer* pPlayer = GET_PLAYER_POINTER_I(i);
        if (pPlayer && pPlayer->ingame && strcmp(pPlayer->ip.chars(), ip) == 0)
            return i;
    }
    return 0;
}

} // extern "C"