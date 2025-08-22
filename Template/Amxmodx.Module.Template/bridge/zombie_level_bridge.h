// zombie_level_bridge.h
// 僵尸等级系统桥接层头文件

#ifndef ZOMBIE_LEVEL_BRIDGE_H
#define ZOMBIE_LEVEL_BRIDGE_H

#ifdef __cplusplus
extern "C" {
#endif

// 玩家状态检查
int IsPlayerValid(int playerId);
int IsPlayerBot(int playerId);
int IsPlayerZombie(int playerId);

// 玩家属性获取/设置
int GetPlayerHealth(int playerId);
void SetPlayerHealth(int playerId, int health);
int GetPlayerArmor(int playerId);
void SetPlayerArmor(int playerId, int armor);
float GetPlayerGravity(int playerId);
void SetPlayerGravity(int playerId, float gravity);
float GetPlayerMaxSpeed(int playerId);
void SetPlayerMaxSpeed(int playerId, float speed);

// 消息显示
void ShowMessage(int playerId, const char* message);
void ShowHudMessage(int playerId, const char* message, float x, float y, int r, int g, int b, int channel);
void ShowScreenFade(int playerId, float duration, int holdTime, int r, int g, int b, int alpha);

// 物品和武器
int GetPlayerCurrentWeapon(int playerId);
void GivePlayerItem(int playerId, const char* itemName);
void CreateExplosion(float* origin, float magnitude, float radius, int bits);

// 文件操作
const char* ReadFile(const char* filePath);
void WriteFile(const char* filePath, const char* content, int append);

// 执行客户端命令
void ClientCommand(int playerId, const char* command);

#ifdef __cplusplus
}
#endif

#endif // ZOMBIE_LEVEL_BRIDGE_H