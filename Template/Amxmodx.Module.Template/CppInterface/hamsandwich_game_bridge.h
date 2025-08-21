// hamsandwich_game_bridge.h
// 游戏特定钩子的桥接头文件

#ifndef HAMSANDWICH_GAME_BRIDGE_H
#define HAMSANDWICH_GAME_BRIDGE_H

#ifdef __cplusplus
extern "C" {
#endif

// 游戏特定钩子注册函数
extern "C" {

// Counter-Strike钩子
int RegisterCsPlayerOnTouchingWeaponHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterCsItemGetMaxSpeedHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterCsItemCanDropHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterCsRestartHook(void* preCallback, void* postCallback);
int RegisterCsRoundRespawnHook(void* preCallback, void* postCallback);

// Team Fortress Classic钩子
int RegisterTfcEngineerUseHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterTfcEmpExplodeHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterTfcTakeEmpBlastHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterTfcRadiusDamage2Hook(const char* entityClass, void* preCallback, void* postCallback);

// Day of Defeat钩子
int RegisterDodWeaponSpecialHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterDodRoundRespawnHook(void* preCallback, void* postCallback);
int RegisterDodItemCanDropHook(const char* entityClass, void* preCallback, void* postCallback);

// The Specialists钩子
int RegisterTsBreakableRespawnHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterTsShouldCollideHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterTsCanUsedThroughWallsHook(const char* entityClass, void* preCallback, void* postCallback);

// Natural Selection钩子
int RegisterNsUpdateOnRemoveHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterNsGetPointValueHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterNsAwardKillHook(void* preCallback, void* postCallback);

// Earth's Special Forces钩子
int RegisterEsfWeaponHolsterWhenMeleedHook(const char* entityClass, void* preCallback, void* postCallback);
int RegisterEsfTakeDamage2Hook(const char* entityClass, void* preCallback, void* postCallback);
}
#endif

#endif // HAMSANDWICH_GAME_BRIDGE_H