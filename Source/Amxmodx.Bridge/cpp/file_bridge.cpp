//
// AMX Mod X, based on AMX Mod by Aleksander Naszko ("OLO").
// Copyright (C) The AMX Mod X Development Team.
//
// This software is licensed under the GNU General Public License, version 3 or higher.
// Additional exceptions apply. For full license details, see LICENSE.txt or visit:
//     https://alliedmods.net/amxmodx-license
//

#include "file_bridge.h"
#include "amxmodx.h"
#include "CFileSystem.h"
#include "CLibrarySys.h"
#include "CDataPack.h"
#include "CGameConfigs.h"
#include "CVault.h"
#include "eiface.h"
#include <amtl/am-autoptr.h>
#include <cstdlib>
#include <cstring>

using namespace ke;

// 全局变量
extern Vault g_vault;

// 文件系统桥接实现
char* AmxModx_Bridge_ReadFile(const char* filePath, int* size) {
    if (!filePath || !size) return nullptr;
    
    const char* realPath = build_pathname("%s", filePath);
    AutoPtr<SystemFile> fp(SystemFile::Open(realPath, "rb"));
    
    if (!fp) {
        *size = 0;
        return nullptr;
    }
    
    fp->Seek(0, SEEK_END);
    *size = static_cast<int>(fp->Tell());
    fp->Seek(0, SEEK_SET);
    
    if (*size <= 0) {
        return nullptr;
    }
    
    char* buffer = new char[*size + 1];
    if (!buffer) {
        *size = 0;
        return nullptr;
    }
    
    size_t bytesRead = fp->Read(buffer, *size);
    buffer[bytesRead] = '\0';
    
    if (bytesRead != *size) {
        delete[] buffer;
        *size = 0;
        return nullptr;
    }
    
    return buffer;
}

bool AmxModx_Bridge_WriteFile(const char* filePath, const char* data, int size) {
    if (!filePath || !data || size < 0) return false;
    
    const char* realPath = build_pathname("%s", filePath);
    AutoPtr<SystemFile> fp(SystemFile::Open(realPath, "wb"));
    
    if (!fp) {
        return false;
    }
    
    return fp->Write(data, size) == static_cast<size_t>(size);
}

void AmxModx_Bridge_FreeBuffer(char* buffer) {
    if (buffer) {
        delete[] buffer;
    }
}

bool AmxModx_Bridge_FileExists(const char* fileName) {
    if (!fileName) return false;
    
    const char* realPath = build_pathname("%s", fileName);
    AutoPtr<SystemFile> fp(SystemFile::Open(realPath, "r"));
    return fp != nullptr;
}

bool AmxModx_Bridge_ReadFileString(const char* fileName, char* buffer, int bufferSize) {
    if (!fileName || !buffer || bufferSize <= 0) return false;
    
    const char* realPath = build_pathname("%s", fileName);
    AutoPtr<SystemFile> fp(SystemFile::Open(realPath, "r"));
    
    if (!fp) return false;
    
    char temp[4096];
    int totalRead = 0;
    
    while (totalRead < bufferSize - 1 && fp->ReadLine(temp, sizeof(temp))) {
        int len = strlen(temp);
        if (len > 0 && temp[len - 1] == '\n') {
            temp[len - 1] = '\0';
            len--;
        }
        if (len > 0 && temp[len - 1] == '\r') {
            temp[len - 1] = '\0';
            len--;
        }
        
        if (totalRead + len >= bufferSize - 1) break;
        
        strcpy(buffer + totalRead, temp);
        totalRead += len;
    }
    
    buffer[totalRead] = '\0';
    return totalRead > 0;
}

bool AmxModx_Bridge_WriteFileString(const char* fileName, const char* content) {
    if (!fileName || !content) return false;
    
    const char* realPath = build_pathname("%s", fileName);
    AutoPtr<SystemFile> fp(SystemFile::Open(realPath, "w"));
    
    if (!fp) return false;
    
    return fp->Write(content, strlen(content)) == strlen(content);
}

bool AmxModx_Bridge_DeleteFile(const char* fileName) {
    if (!fileName) return false;
    
    const char* realPath = build_pathname("%s", fileName);
    return SystemFile::Delete(realPath);
}

// CDataPack桥接实现
CDataPack* AmxModx_Bridge_CreateDataPack() {
    return new CDataPack();
}

void AmxModx_Bridge_DestroyDataPack(CDataPack* pack) {
    if (pack) {
        delete pack;
    }
}

int AmxModx_Bridge_GetDataPackPosition(CDataPack* pack) {
    return pack ? static_cast<int>(pack->GetPosition()) : 0;
}

void AmxModx_Bridge_SetDataPackPosition(CDataPack* pack, int position) {
    if (pack) {
        pack->SetPosition(static_cast<size_t>(position));
    }
}

bool AmxModx_Bridge_WriteDataPackCell(CDataPack* pack, int value) {
    if (!pack) return false;
    pack->PackCell(static_cast<cell>(value));
    return true;
}

bool AmxModx_Bridge_WriteDataPackFloat(CDataPack* pack, float value) {
    if (!pack) return false;
    pack->PackFloat(value);
    return true;
}

bool AmxModx_Bridge_WriteDataPackString(CDataPack* pack, const char* value) {
    if (!pack || !value) return false;
    pack->PackString(value);
    return true;
}

int AmxModx_Bridge_ReadDataPackCell(CDataPack* pack) {
    return pack ? static_cast<int>(pack->ReadCell()) : 0;
}

float AmxModx_Bridge_ReadDataPackFloat(CDataPack* pack) {
    return pack ? pack->ReadFloat() : 0.0f;
}

const char* AmxModx_Bridge_ReadDataPackString(CDataPack* pack) {
    if (!pack) return "";
    static char buffer[1024];
    const char* str = pack->ReadString(nullptr);
    if (str) {
        strncpy(buffer, str, sizeof(buffer) - 1);
        buffer[sizeof(buffer) - 1] = '\0';
        return buffer;
    }
    return "";
}

// 控制台变量桥接实现
cvar_t* AmxModx_Bridge_CreateCVar(const char* name, const char* value, int flags) {
    if (!name || !value) return nullptr;
    return g_engfuncs.pfnCVarGetPointer(const_cast<char*>(name));
}

cvar_t* AmxModx_Bridge_FindCVar(const char* name) {
    if (!name) return nullptr;
    return g_engfuncs.pfnCVarGetPointer(const_cast<char*>(name));
}

const char* AmxModx_Bridge_GetCVarString(cvar_t* cvar) {
    return cvar ? cvar->string : "";
}

float AmxModx_Bridge_GetCVarFloat(cvar_t* cvar) {
    return cvar ? cvar->value : 0.0f;
}

int AmxModx_Bridge_GetCVarInt(cvar_t* cvar) {
    return cvar ? static_cast<int>(cvar->value) : 0;
}

void AmxModx_Bridge_SetCVarString(cvar_t* cvar, const char* value) {
    if (cvar && value) {
        CVAR_DIRECTSET(cvar, const_cast<char*>(value));
    }
}

void AmxModx_Bridge_SetCVarFloat(cvar_t* cvar, float value) {
    if (cvar) {
        char buffer[64];
        snprintf(buffer, sizeof(buffer), "%f", value);
        CVAR_DIRECTSET(cvar, buffer);
    }
}

void AmxModx_Bridge_SetCVarInt(cvar_t* cvar, int value) {
    if (cvar) {
        char buffer[64];
        snprintf(buffer, sizeof(buffer), "%d", value);
        CVAR_DIRECTSET(cvar, buffer);
    }
}

// 游戏配置桥接实现
IGameConfig* AmxModx_Bridge_LoadGameConfig(const char* filePath) {
    if (!filePath) return nullptr;
    
    char error[256];
    IGameConfig* config = nullptr;
    
    if (ConfigManager.LoadGameConfigFile(filePath, &config, error, sizeof(error))) {
        return config;
    }
    
    return nullptr;
}

void AmxModx_Bridge_UnloadGameConfig(IGameConfig* config) {
    if (config) {
        ConfigManager.CloseGameConfigFile(config);
    }
}

const char* AmxModx_Bridge_GetConfigValue(IGameConfig* config, const char* key) {
    if (!config || !key) return "";
    
    const char* value = config->GetKeyValue(key);
    return value ? value : "";
}

// 持久化存储桥接实现
Vault* AmxModx_Bridge_GetVault() {
    return &g_vault;
}

bool AmxModx_Bridge_VaultExists(Vault* vault, const char* key) {
    return vault && key ? vault->exists(key) : false;
}

bool AmxModx_Bridge_VaultPut(Vault* vault, const char* key, const char* value) {
    if (vault && key && value) {
        vault->put(key, value);
        return true;
    }
    return false;
}

bool AmxModx_Bridge_VaultRemove(Vault* vault, const char* key) {
    if (vault && key) {
        vault->remove(key);
        return true;
    }
    return false;
}

const char* AmxModx_Bridge_VaultGet(const char* key) {
    return key ? g_vault.get(key) : "";
}