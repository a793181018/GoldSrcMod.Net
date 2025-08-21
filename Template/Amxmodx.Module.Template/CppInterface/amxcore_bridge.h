/*
 * AMX Mod X Core Bridge Header
 * 实现amxcore.cpp中核心接口的C#桥接
 */
#ifndef AMXCORE_BRIDGE_H
#define AMXCORE_BRIDGE_H

#ifdef __cplusplus
extern "C" {
#endif

/**
 * @brief 获取当前函数的参数数量
 * @return 参数数量
 */
int AmxModx_Bridge_GetNumArgs(void);

/**
 * @brief 获取指定索引的参数值
 * @param index 参数索引（从0开始）
 * @param offset 数组偏移量（对于数组参数）
 * @return 参数值
 */
int AmxModx_Bridge_GetArg(int index, int offset);

/**
 * @brief 设置指定索引的参数值
 * @param index 参数索引（从0开始）
 * @param offset 数组偏移量（对于数组参数）
 * @param value 要设置的值
 * @return 成功返回1，失败返回0
 */
int AmxModx_Bridge_SetArg(int index, int offset, int value);

/**
 * @brief 获取可用堆空间大小
 * @return 堆空间字节数
 */
int AmxModx_Bridge_GetHeapSpace(void);

/**
 * @brief 根据函数名获取函数索引
 * @param functionName 函数名称
 * @return 函数索引，-1表示未找到
 */
int AmxModx_Bridge_GetFunctionIndex(const char* functionName);

/**
 * @brief 交换32位整数的字节顺序
 * @param value 要交换的值
 * @return 交换后的值
 */
int AmxModx_Bridge_SwapChars(int value);

/**
 * @brief 获取属性值
 * @param id 属性ID
 * @param name 属性名称
 * @param value 属性值
 * @param buffer 输出缓冲区
 * @param maxLen 缓冲区最大长度
 * @return 属性值
 */
int AmxModx_Bridge_GetProperty(int id, const char* name, int value, char* buffer, int maxLen);

/**
 * @brief 设置属性值
 * @param id 属性ID
 * @param name 属性名称
 * @param value 属性值
 * @param newName 新属性名称（可选）
 * @return 之前的属性值
 */
int AmxModx_Bridge_SetProperty(int id, const char* name, int value, const char* newName);

/**
 * @brief 删除属性
 * @param id 属性ID
 * @param name 属性名称
 * @param value 属性值
 * @return 被删除的属性值
 */
int AmxModx_Bridge_DeleteProperty(int id, const char* name, int value);

/**
 * @brief 检查属性是否存在
 * @param id 属性ID
 * @param name 属性名称
 * @param value 属性值
 * @return 存在返回1，不存在返回0
 */
int AmxModx_Bridge_PropertyExists(int id, const char* name, int value);

#ifdef __cplusplus
}
#endif

#endif // AMXCORE_BRIDGE_H