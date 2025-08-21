// vim: set ts=4 sw=4 tw=99 noet:
//
// Counter-Strike Module C# Bridge
// C++ side bridge header
//

#ifndef CSTRIKE_BRIDGE_H
#define CSTRIKE_BRIDGE_H

#ifdef WIN32
#define BRIDGE_API __declspec(dllexport)
#else
#define BRIDGE_API __attribute__((visibility("default")))
#endif

#ifdef __cplusplus
extern "C" {
#endif

// Player API
BRIDGE_API int CsGetUserMoney(int playerIndex);
BRIDGE_API void CsSetUserMoney(int playerIndex, int money, int flash = 1);
BRIDGE_API int CsGetUserArmor(int playerIndex);
BRIDGE_API void CsSetUserArmor(int playerIndex, int armor);
BRIDGE_API int CsGetUserTeam(int playerIndex);
BRIDGE_API void CsSetUserTeam(int playerIndex, int team);
BRIDGE_API bool CsGetUserVip(int playerIndex);
BRIDGE_API void CsSetUserVip(int playerIndex, bool vip);
BRIDGE_API int CsGetUserDeaths(int playerIndex);
BRIDGE_API void CsSetUserDeaths(int playerIndex, int deaths, bool updateScoreboard = true);

// Weapon API
BRIDGE_API int CsGetWeaponId(int weaponEntity);
BRIDGE_API bool CsGetWeaponSilenced(int weaponEntity);
BRIDGE_API void CsSetWeaponSilenced(int weaponEntity, bool silenced, int drawAnimation = 1);
BRIDGE_API bool CsGetWeaponBurstMode(int weaponEntity);
BRIDGE_API void CsSetWeaponBurstMode(int weaponEntity, bool burstMode, int drawAnimation = 1);
BRIDGE_API int CsGetWeaponAmmo(int weaponEntity);
BRIDGE_API void CsSetWeaponAmmo(int weaponEntity, int ammo);

// Game State API
BRIDGE_API bool CsGetUserInsideBuyzone(int playerIndex);
BRIDGE_API int CsGetUserMapzones(int playerIndex);
BRIDGE_API bool CsGetUserHasPrimary(int playerIndex);
BRIDGE_API bool CsGetUserDefusekit(int playerIndex);
BRIDGE_API void CsSetUserDefusekit(int playerIndex, bool hasKit);
BRIDGE_API bool CsGetUserNvg(int playerIndex);
BRIDGE_API void CsSetUserNvg(int playerIndex, bool hasNvg);

// Model API
BRIDGE_API void CsGetUserModel(int playerIndex, char* buffer, int bufferSize);
BRIDGE_API void CsSetUserModel(int playerIndex, const char* model);
BRIDGE_API void CsResetUserModel(int playerIndex);

// Entity API
BRIDGE_API int CsCreateEntity(const char* className);
BRIDGE_API int CsFindEntityByClass(int startIndex, const char* className);
BRIDGE_API int CsFindEntityByOwner(int startIndex, int ownerIndex);
BRIDGE_API bool CsSetEntityClass(int entityIndex, const char* className);

// Item API
BRIDGE_API int CsGetItemId(const char* itemName);
BRIDGE_API bool CsGetItemAlias(int itemId, char* buffer, int bufferSize);
BRIDGE_API bool CsGetTranslatedItemAlias(int itemId, char* buffer, int bufferSize);

// Hostage API
BRIDGE_API int CsGetHostageId(int hostageIndex);
BRIDGE_API bool CsGetHostageFollow(int hostageIndex);
BRIDGE_API void CsSetHostageFollow(int hostageIndex, bool follow);

// Bomb API
BRIDGE_API float CsGetC4ExplodeTime();
BRIDGE_API void CsSetC4ExplodeTime(float time);
BRIDGE_API bool CsGetC4Defusing();
BRIDGE_API void CsSetC4Defusing(bool defusing);

// Event delegates
typedef void (*CsPlayerDeathCallback)(int victim, int killer, int weapon);
typedef void (*CsBombEventCallback)(int eventType, int player);
typedef void (*CsWeaponPickupCallback)(int player, int weaponId);

BRIDGE_API void CsRegisterPlayerDeathCallback(CsPlayerDeathCallback callback);
BRIDGE_API void CsRegisterBombEventCallback(CsBombEventCallback callback);
BRIDGE_API void CsRegisterWeaponPickupCallback(CsWeaponPickupCallback callback);

#ifdef __cplusplus
}
#endif

#endif // CSTRIKE_BRIDGE_H