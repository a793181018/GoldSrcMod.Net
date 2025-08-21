// messages_bridge.h
// 消息系统桥接接口定义
#pragma once

#include "../amxmodx.h"
#include "../messages.h"

#ifdef __cplusplus
extern "C" {
#endif

/**
 * 开始发送消息
 * @param msg_dest 消息目标
 * @param msg_type 消息类型
 * @param origin 消息原点坐标
 * @param edict 实体指针
 */
void AmxModx_Bridge_MessageBegin(int msg_dest, int msg_type, const float* origin, int edict);

/**
 * 结束消息发送
 */
void AmxModx_Bridge_MessageEnd(void);

/**
 * 写入字节数据到消息
 * @param value 字节值
 */
void AmxModx_Bridge_WriteByte(int value);

/**
 * 写入字符数据到消息
 * @param value 字符值
 */
void AmxModx_Bridge_WriteChar(int value);

/**
 * 写入短整型数据到消息
 * @param value 短整型值
 */
void AmxModx_Bridge_WriteShort(int value);

/**
 * 写入长整型数据到消息
 * @param value 长整型值
 */
void AmxModx_Bridge_WriteLong(int value);

/**
 * 写入实体数据到消息
 * @param value 实体索引
 */
void AmxModx_Bridge_WriteEntity(int value);

/**
 * 写入角度数据到消息
 * @param value 角度值
 */
void AmxModx_Bridge_WriteAngle(float value);

/**
 * 写入坐标数据到消息
 * @param value 坐标值
 */
void AmxModx_Bridge_WriteCoord(float value);

/**
 * 写入字符串数据到消息
 * @param str 字符串值
 */
void AmxModx_Bridge_WriteString(const char* str);

/**
 * 注册消息钩子
 * @param msgId 消息ID
 * @param callback 回调函数指针
 * @param post 是否为后置钩子
 * @return 是否注册成功
 */
int AmxModx_Bridge_RegisterMessage(int msgId, void* callback, int post);

/**
 * 注销消息钩子
 * @param msgId 消息ID
 * @param callback 回调函数指针
 * @param post 是否为后置钩子
 * @return 是否注销成功
 */
int AmxModx_Bridge_UnregisterMessage(int msgId, void* callback, int post);

/**
 * 设置消息阻塞
 * @param msgId 消息ID
 * @param blocking 是否阻塞
 */
void AmxModx_Bridge_SetMessageBlock(int msgId, int blocking);

/**
 * 获取消息阻塞状态
 * @param msgId 消息ID
 * @return 阻塞状态
 */
int AmxModx_Bridge_GetMessageBlock(int msgId);

/**
 * 获取消息参数数量
 * @return 参数数量
 */
int AmxModx_Bridge_GetMessageArgs(void);

/**
 * 获取消息参数类型
 * @param argIndex 参数索引
 * @return 参数类型
 */
int AmxModx_Bridge_GetMessageArgType(int argIndex);

/**
 * 获取消息参数整数值
 * @param argIndex 参数索引
 * @return 整数值
 */
int AmxModx_Bridge_GetMessageArgInt(int argIndex);

/**
 * 获取消息参数浮点数值
 * @param argIndex 参数索引
 * @return 浮点数值
 */
float AmxModx_Bridge_GetMessageArgFloat(int argIndex);

/**
 * 获取消息参数字符串值
 * @param argIndex 参数索引
 * @param buffer 输出缓冲区
 * @param bufferSize 缓冲区大小
 * @return 实际复制的字符数
 */
int AmxModx_Bridge_GetMessageArgString(int argIndex, char* buffer, int bufferSize);

/**
 * 设置消息参数整数值
 * @param argIndex 参数索引
 * @param value 整数值
 */
void AmxModx_Bridge_SetMessageArgInt(int argIndex, int value);

/**
 * 设置消息参数浮点数值
 * @param argIndex 参数索引
 * @param value 浮点数值
 */
void AmxModx_Bridge_SetMessageArgFloat(int argIndex, float value);

/**
 * 设置消息参数字符串值
 * @param argIndex 参数索引
 * @param str 字符串值
 */
void AmxModx_Bridge_SetMessageArgString(int argIndex, const char* str);

#ifdef __cplusplus
}
#endif