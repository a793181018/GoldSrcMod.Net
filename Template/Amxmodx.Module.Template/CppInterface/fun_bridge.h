// vim: set ts=4 sw=4 tw=99 noet:
//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license

//
// Fun Module Bridge
//

#pragma once

#include <stddef.h>
#include <stdint.h>

#ifdef __cplusplus
extern "C" {
#endif

// 玩家状态管理
int GetClientListening(int receiver, int sender);
int SetClientListening(int receiver, int sender, int listen);
int SetUserGodmode(int index, int godmode);
int GetUserGodmode(int index);
int SetUserHealth(int index, float health);
float GetUserHealth(int index);
int GiveItem(int index, const char* item);
int SpawnEntity(int index);
int SetUserFrags(int index, int frags);
int GetUserFrags(int index);
int SetUserArmor(int index, int armor);
int GetUserArmor(int index);
int SetUserOrigin(int index, const float origin[3]);
int GetUserOrigin(int index, float *origin);
int SetUserRendering(int index, int renderMode, int renderAmount, int renderFx, const float *renderColor);
int GetUserRendering(int index, int *renderMode, int *renderAmount, int *renderFx, float *renderColor);
int SetUserMaxspeed(int index, float maxspeed);
float GetUserMaxspeed(int index);
int SetUserGravity(int index, float gravity);
float GetUserGravity(int index);
int SetUserHitzones(int index, int zones);
int GetUserHitzones(int index);
int SetUserNoclip(int index, int noclip);
int GetUserNoclip(int index);
int SetUserFootsteps(int index, int footsteps);
int GetUserFootsteps(int index);
int StripUserWeapons(int index);

#ifdef __cplusplus
}
#endif