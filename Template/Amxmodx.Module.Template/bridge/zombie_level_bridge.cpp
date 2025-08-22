// zombie_level_bridge.cpp
// 僵尸等级系统桥接层实现

#include "zombie_level_bridge.h"

// 模拟AMXX模块头文件定义
#ifdef _WIN32
#define DLLEXPORT __declspec(dllexport)
#else
#define DLLEXPORT
#endif

// 模拟AMXX函数定义
extern "C" {
    int IsPlayerValid(int playerId);
    int IsPlayerBot(int playerId);
    int IsPlayerZombie(int playerId);
    int GetPlayerHealth(int playerId);
    void SetPlayerHealth(int playerId, int health);
    int GetPlayerArmor(int playerId);
    void SetPlayerArmor(int playerId, int armor);
    float GetPlayerGravity(int playerId);
    void SetPlayerGravity(int playerId, float gravity);
    float GetPlayerMaxSpeed(int playerId);
    void SetPlayerMaxSpeed(int playerId, float speed);
    void ShowMessage(int playerId, const char* message);
    void ShowHudMessage(int playerId, const char* message, float x, float y, int r, int g, int b, int channel);
    void ShowScreenFade(int playerId, float duration, int holdTime, int r, int g, int b, int alpha);
    int GetPlayerCurrentWeapon(int playerId);
    void GivePlayerItem(int playerId, const char* itemName);
    void CreateExplosion(float* origin, float magnitude, float radius, int bits);
    const char* ReadFile(const char* filePath);
    void WriteFile(const char* filePath, const char* content, int append);
    void ClientCommand(int playerId, const char* command);
}

#include <fstream>
#include <string>

// 玩家有效性检查
int IsPlayerValid(int playerId) {
    return (playerId > 0 && playerId <= gpGlobals->maxClients) ? 1 : 0;
}

// 检查是否为机器人
int IsPlayerBot(int playerId) {
    if (!IsPlayerValid(playerId)) return 0;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return 0;
    
    return (pEdict->v.flags & FL_FAKECLIENT) ? 1 : 0;
}

// 检查是否为僵尸
int IsPlayerZombie(int playerId) {
    if (!IsPlayerValid(playerId)) return 0;
    
    // 使用ZP原生函数检查
    static AMX_NATIVE zp_is_player_zombie = nullptr;
    if (!zp_is_player_zombie) {
        zp_is_player_zombie = MF_GetAmxNative("zp_is_player_zombie");
    }
    
    if (zp_is_player_zombie) {
        cell params[2] = {1, playerId};
        return zp_is_player_zombie(params);
    }
    
    return 0;
}

// 获取玩家血量
int GetPlayerHealth(int playerId) {
    if (!IsPlayerValid(playerId)) return 0;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return 0;
    
    return (int)pEdict->v.health;
}

// 设置玩家血量
void SetPlayerHealth(int playerId, int health) {
    if (!IsPlayerValid(playerId)) return;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return;
    
    pEdict->v.health = health;
}

// 获取玩家护甲
int GetPlayerArmor(int playerId) {
    if (!IsPlayerValid(playerId)) return 0;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return 0;
    
    return (int)pEdict->v.armorvalue;
}

// 设置玩家护甲
void SetPlayerArmor(int playerId, int armor) {
    if (!IsPlayerValid(playerId)) return;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return;
    
    pEdict->v.armorvalue = armor;
}

// 获取玩家重力
float GetPlayerGravity(int playerId) {
    if (!IsPlayerValid(playerId)) return 1.0f;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return 1.0f;
    
    return pEdict->v.gravity;
}

// 设置玩家重力
void SetPlayerGravity(int playerId, float gravity) {
    if (!IsPlayerValid(playerId)) return;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return;
    
    pEdict->v.gravity = gravity;
}

// 获取玩家最大速度
float GetPlayerMaxSpeed(int playerId) {
    if (!IsPlayerValid(playerId)) return 250.0f;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return 250.0f;
    
    return pEdict->v.maxspeed;
}

// 设置玩家最大速度
void SetPlayerMaxSpeed(int playerId, float speed) {
    if (!IsPlayerValid(playerId)) return;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return;
    
    pEdict->v.maxspeed = speed;
}

// 显示消息给玩家
void ShowMessage(int playerId, const char* message) {
    if (!IsPlayerValid(playerId) || !message) return;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return;
    
    CLIENT_PRINT(pEdict, HUD_PRINTTALK, (char*)message);
}

// 显示HUD消息
void ShowHudMessage(int playerId, const char* message, float x, float y, int r, int g, int b, int channel) {
    if (!IsPlayerValid(playerId) || !message) return;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return;
    
    // 使用ShowSyncHudMsg实现
    static int g_hudmsg = 0;
    if (!g_hudmsg) {
        g_hudmsg = CreateHudSyncObj();
    }
    
    ShowSyncHudMsg(pEdict, g_hudmsg, "%s", message);
}

// 显示屏幕淡出效果
void ShowScreenFade(int playerId, float duration, int holdTime, int r, int g, int b, int alpha) {
    if (!IsPlayerValid(playerId)) return;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return;
    
    MESSAGE_BEGIN(MSG_ONE, SVC_TEMPENTITY, NULL, pEdict);
    WRITE_BYTE(TE_FADE);
    WRITE_SHORT((int)(duration * 100));  // 持续时间
    WRITE_SHORT(holdTime);               // 保持时间
    WRITE_SHORT(0);                      // 标志
    WRITE_BYTE(r);                       // 红色
    WRITE_BYTE(g);                       // 绿色
    WRITE_BYTE(b);                       // 蓝色
    WRITE_BYTE(alpha);                   // 透明度
    MESSAGE_END();
}

// 获取玩家当前武器
int GetPlayerCurrentWeapon(int playerId) {
    if (!IsPlayerValid(playerId)) return 0;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return 0;
    
    return ENTINDEX(pEdict->v.weapon);
}

// 给予玩家物品
void GivePlayerItem(int playerId, const char* itemName) {
    if (!IsPlayerValid(playerId) || !itemName) return;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return;
    
    GiveNamedItem(pEdict, (char*)itemName);
}

// 创建爆炸效果
void CreateExplosion(float* origin, float magnitude, float radius, int bits) {
    if (!origin) return;
    
    MESSAGE_BEGIN(MSG_PVS, SVC_TEMPENTITY, origin);
    WRITE_BYTE(TE_EXPLOSION);
    WRITE_COORD(origin[0]);
    WRITE_COORD(origin[1]);
    WRITE_COORD(origin[2]);
    WRITE_SHORT(g_sModelIndexFireball);
    WRITE_BYTE((int)(magnitude * 10));
    WRITE_BYTE(15);
    WRITE_BYTE(0);
    MESSAGE_END();
}

// 文件操作
const char* ReadFile(const char* filePath) {
    if (!filePath) return "";
    
    static std::string content;
    std::ifstream file(filePath);
    
    if (file.is_open()) {
        std::string line;
        content.clear();
        while (std::getline(file, line)) {
            content += line + "\n";
        }
        return content.c_str();
    }
    
    return "";
}

void WriteFile(const char* filePath, const char* content, int append) {
    if (!filePath || !content) return;
    
    std::ofstream file;
    if (append) {
        file.open(filePath, std::ios::app);
    } else {
        file.open(filePath);
    }
    
    if (file.is_open()) {
        file << content;
        file.close();
    }
}

// 执行客户端命令
void ClientCommand(int playerId, const char* command) {
    if (!IsPlayerValid(playerId) || !command) return;
    
    edict_t* pEdict = INDEXENT(playerId);
    if (!pEdict || pEdict->free) return;
    
    CLIENT_COMMAND(pEdict, "%s\n", command);
}