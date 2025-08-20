#include "amxmodx_bridge.h"
#include <amx/amx.h>
#include <amx/amxaux.h>
#include <amx/amxdbg.h>
#include <string>
#include <unordered_map>

// 全局函数注册表
static std::unordered_map<std::string, NativeFunction> g_nativeFunctions;

// 注册原生函数
int AmxModx_Bridge_RegisterNative(AMX* amx, const char* name, NativeFunction func)
{
    if (!amx || !name || !func)
        return 0;
    
    g_nativeFunctions[name] = func;
    return 1;
}

// 注销原生函数
int AmxModx_Bridge_UnregisterNative(AMX* amx, const char* name)
{
    if (!amx || !name)
        return 0;
    
    auto it = g_nativeFunctions.find(name);
    if (it != g_nativeFunctions.end()) {
        g_nativeFunctions.erase(it);
        return 1;
    }
    
    return 0;
}

// 注册库
int AmxModx_Bridge_RegisterLibrary(AMX* amx, const char* libname)
{
    if (!amx || !libname)
        return 0;
    
    // 这里简化处理，实际应该注册到AMX Mod X的库系统
    return 1;
}

// 获取原生函数数量
int AmxModx_Bridge_GetNativeCount(AMX* amx)
{
    if (!amx)
        return 0;
    
    return static_cast<int>(g_nativeFunctions.size());
}

// 获取原生函数名称
const char* AmxModx_Bridge_GetNativeName(AMX* amx, int index)
{
    if (!amx || index < 0 || index >= static_cast<int>(g_nativeFunctions.size()))
        return nullptr;
    
    auto it = g_nativeFunctions.begin();
    std::advance(it, index);
    return it->first.c_str();
}

// 检查原生函数是否存在
int AmxModx_Bridge_NativeExists(AMX* amx, const char* name)
{
    if (!amx || !name)
        return 0;
    
    return g_nativeFunctions.find(name) != g_nativeFunctions.end() ? 1 : 0;
}

// 清除库
void AmxModx_Bridge_ClearLibraries(AMX* amx)
{
    if (!amx)
        return;
    
    g_nativeFunctions.clear();
}

// 获取字符串参数
const char* AmxModx_Bridge_GetString(AMX* amx, int param, int* length)
{
    if (!amx || param < 0)
        return nullptr;
    
    cell* addr = amx_Address(amx, param);
    if (!addr)
        return nullptr;
    
    char* str = (char*)addr;
    if (length)
        *length = strlen(str);
    
    return str;
}

// 设置字符串参数
int AmxModx_Bridge_SetString(AMX* amx, int param, const char* str, int maxlen)
{
    if (!amx || param < 0 || !str)
        return 0;
    
    cell* addr = amx_Address(amx, param);
    if (!addr)
        return 0;
    
    strncpy((char*)addr, str, maxlen);
    return 1;
}

// 获取参数值
cell AmxModx_Bridge_GetParam(AMX* amx, int index)
{
    if (!amx || index < 0)
        return 0;
    
    cell* params = (cell*)amx->prm;
    if (!params || index >= params[0] / sizeof(cell))
        return 0;
    
    return params[index + 1];
}

// 获取参数地址
cell* AmxModx_Bridge_GetParamAddress(AMX* amx, int index)
{
    if (!amx || index < 0)
        return nullptr;
    
    return amx_Address(amx, index);
}

// 设置参数值
int AmxModx_Bridge_SetParam(AMX* amx, int index, cell value)
{
    if (!amx || index < 0)
        return 0;
    
    cell* addr = amx_Address(amx, index);
    if (!addr)
        return 0;
    
    *addr = value;
    return 1;
}

// 获取浮点数参数
float AmxModx_Bridge_GetFloat(AMX* amx, int index)
{
    if (!amx || index < 0)
        return 0.0f;
    
    cell value = AmxModx_Bridge_GetParam(amx, index);
    return amx_ctof(value);
}

// 设置浮点数参数
int AmxModx_Bridge_SetFloat(AMX* amx, int index, float value)
{
    if (!amx || index < 0)
        return 0;
    
    cell cellValue = amx_ftoc(value);
    return AmxModx_Bridge_SetParam(amx, index, cellValue);
}

// 获取数组参数
int AmxModx_Bridge_GetArray(AMX* amx, int param, cell* dest, int size)
{
    if (!amx || param < 0 || !dest || size <= 0)
        return 0;
    
    cell* addr = amx_Address(amx, param);
    if (!addr)
        return 0;
    
    memcpy(dest, addr, size * sizeof(cell));
    return 1;
}

// 设置数组参数
int AmxModx_Bridge_SetArray(AMX* amx, int param, const cell* source, int size)
{
    if (!amx || param < 0 || !source || size <= 0)
        return 0;
    
    cell* addr = amx_Address(amx, param);
    if (!addr)
        return 0;
    
    memcpy(addr, source, size * sizeof(cell));
    return 1;
}

// 记录错误
void AmxModx_Bridge_LogError(AMX* amx, int err, const char* message)
{
    if (!amx || !message)
        return;
    
    // 这里简化处理，实际应该记录到AMX Mod X的日志系统
    printf("[AMX Bridge Error %d]: %s\n", err, message);
}

// 获取原生函数指针
NativeFunction AmxModx_Bridge_GetNativeFunction(AMX* amx, const char* functionName)
{
    if (!amx || !functionName)
        return nullptr;
    
    auto it = g_nativeFunctions.find(functionName);
    if (it != g_nativeFunctions.end())
        return it->second;
    
    // 尝试从AMX Mod X获取
    int index;
    if (amx_FindNative(amx, functionName, &index) == AMX_ERR_NONE)
    {
        return (NativeFunction)amx_GetNative(amx, index);
    }
    
    return nullptr;
}

// 调用原生函数
int AmxModx_Bridge_CallNativeFunction(NativeFunction func, const cell* params)
{
    if (!func || !params)
        return 0;
    
    // 这里简化处理，实际应该使用AMX实例
    return 0;
}

// 调用原生函数（带AMX实例）
int AmxModx_Bridge_CallNativeFunctionEx(AMX* amx, NativeFunction func, const cell* params)
{
    if (!amx || !func || !params)
        return 0;
    
    return func(amx, (cell*)params);
}