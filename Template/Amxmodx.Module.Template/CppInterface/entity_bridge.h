// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X Entity Bridge Header
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

#ifndef _ENTITY_BRIDGE_H
#define _ENTITY_BRIDGE_H

#ifdef __cplusplus
extern "C" {
#endif

// 实体管理接口
int AmxModx_Bridge_GetMaxClients(void);
int AmxModx_Bridge_GetMaxEntities(void);
int AmxModx_Bridge_GetPlayerCount(void);
bool AmxModx_Bridge_IsPlayerValid(int playerId);
bool AmxModx_Bridge_IsPlayerInGame(int playerId);
bool AmxModx_Bridge_IsPlayerAlive(int playerId);
bool AmxModx_Bridge_IsPlayerBot(int playerId);

// 实体属性访问
const char* AmxModx_Bridge_GetPlayerName(int playerId);
const char* AmxModx_Bridge_GetPlayerIPAddress(int playerId);
const char* AmxModx_Bridge_GetPlayerAuthID(int playerId);
const char* AmxModx_Bridge_GetPlayerTeam(int playerId);
int AmxModx_Bridge_GetPlayerUserID(int playerId);
int AmxModx_Bridge_GetPlayerFrags(int playerId);
int AmxModx_Bridge_GetPlayerDeaths(int playerId);
int AmxModx_Bridge_GetPlayerHealth(int playerId);
int AmxModx_Bridge_GetPlayerArmor(int playerId);
int AmxModx_Bridge_GetPlayerPing(int playerId);

// 实体位置相关
bool AmxModx_Bridge_GetPlayerOrigin(int playerId, float* x, float* y, float* z);
bool AmxModx_Bridge_GetPlayerVelocity(int playerId, float* x, float* y, float* z);
bool AmxModx_Bridge_SetPlayerOrigin(int playerId, float x, float y, float z);
bool AmxModx_Bridge_SetPlayerVelocity(int playerId, float x, float y, float z);

// 实体武器相关
int AmxModx_Bridge_GetPlayerCurrentWeapon(int playerId);
int AmxModx_Bridge_GetPlayerAmmo(int playerId, int weaponId);
bool AmxModx_Bridge_PlayerHasWeapon(int playerId, int weaponId);

// 实体操作
bool AmxModx_Bridge_KillPlayer(int playerId);
bool AmxModx_Bridge_SlapPlayer(int playerId, int damage, bool randomVelocity);
bool AmxModx_Bridge_TeleportPlayer(int playerId, float x, float y, float z);
bool AmxModx_Bridge_RespawnPlayer(int playerId);
bool AmxModx_Bridge_StripWeapons(int playerId);
bool AmxModx_Bridge_GiveWeapon(int playerId, const char* weaponName, int ammo);
bool AmxModx_Bridge_SetPlayerTeam(int playerId, int teamId);
bool AmxModx_Bridge_FreezePlayer(int playerId, bool freeze);
bool AmxModx_Bridge_SetPlayerHealth(int playerId, int health);
bool AmxModx_Bridge_SetPlayerArmor(int playerId, int armor);
bool AmxModx_Bridge_SetPlayerFrags(int playerId, int frags);
bool AmxModx_Bridge_SetPlayerDeaths(int playerId, int deaths);

// 实体查找
int AmxModx_Bridge_FindPlayerByUserID(int userId);
int AmxModx_Bridge_FindPlayerByName(const char* name);
int AmxModx_Bridge_FindPlayerByIPAddress(const char* ip);

#ifdef __cplusplus
}
#endif

#endif // _ENTITY_BRIDGE_H