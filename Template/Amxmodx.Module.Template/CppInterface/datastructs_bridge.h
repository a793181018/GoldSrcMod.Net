// datastructs_bridge.h
// 数据结构桥接接口定义
#pragma once

#include "../amxmodx.h"

#ifdef __cplusplus
extern "C" {
#endif

/**
 * 创建动态数组
 * @param cellsize 每个元素的大小（以cell为单位）
 * @param reserved 初始预留容量
 * @return 数组句柄，失败返回0
 */
int AmxModx_Bridge_ArrayCreate(int cellsize, int reserved = 32);

/**
 * 销毁数组并释放内存
 * @param handle 数组句柄（传引用，成功后会置0）
 * @return 是否成功销毁
 */
int AmxModx_Bridge_ArrayDestroy(int* handle);

/**
 * 获取数组当前元素数量
 * @param handle 数组句柄
 * @return 元素数量，失败返回-1
 */
int AmxModx_Bridge_ArraySize(int handle);

/**
 * 调整数组大小
 * @param handle 数组句柄
 * @param count 新的元素数量
 * @return 是否成功调整
 */
int AmxModx_Bridge_ArrayResize(int handle, int count);

/**
 * 获取数组元素（单元格值）
 * @param handle 数组句柄
 * @param index 元素索引
 * @param block 数据块索引（默认为0）
 * @return 单元格值，失败返回0
 */
int AmxModx_Bridge_ArrayGetCell(int handle, int index, int block = 0);

/**
 * 设置数组元素（单元格值）
 * @param handle 数组句柄
 * @param index 元素索引
 * @param value 要设置的值
 * @param block 数据块索引（默认为0）
 * @return 是否成功设置
 */
int AmxModx_Bridge_ArraySetCell(int handle, int index, int value, int block = 0);

/**
 * 向数组末尾添加单元格值
 * @param handle 数组句柄
 * @param value 要添加的值
 * @return 新元素的索引，失败返回-1
 */
int AmxModx_Bridge_ArrayPushCell(int handle, int value);

/**
 * 从数组获取字符串
 * @param handle 数组句柄
 * @param index 元素索引
 * @param buffer 输出缓冲区
 * @param size 缓冲区大小
 * @return 实际复制的字符数
 */
int AmxModx_Bridge_ArrayGetString(int handle, int index, char* buffer, int size);

/**
 * 向数组设置字符串
 * @param handle 数组句柄
 * @param index 元素索引
 * @param str 要设置的字符串
 * @return 实际复制的字符数
 */
int AmxModx_Bridge_ArraySetString(int handle, int index, const char* str);

/**
 * 克隆数组
 * @param handle 原数组句柄
 * @return 新数组句柄，失败返回0
 */
int AmxModx_Bridge_ArrayClone(int handle);

/**
 * 清空数组内容
 * @param handle 数组句柄
 * @return 是否成功清空
 */
int AmxModx_Bridge_ArrayClear(int handle);

/**
 * 删除指定索引的元素
 * @param handle 数组句柄
 * @param index 要删除的索引
 * @return 是否成功删除
 */
int AmxModx_Bridge_ArrayDeleteItem(int handle, int index);

/**
 * 交换两个元素的位置
 * @param handle 数组句柄
 * @param index1 第一个索引
 * @param index2 第二个索引
 * @return 是否成功交换
 */
int AmxModx_Bridge_ArraySwap(int handle, int index1, int index2);

/**
 * 在数组中查找字符串
 * @param handle 数组句柄
 * @param str 要查找的字符串
 * @return 找到返回索引，未找到返回-1
 */
int AmxModx_Bridge_ArrayFindString(int handle, const char* str);

/**
 * 在数组中查找数值
 * @param handle 数组句柄
 * @param value 要查找的值
 * @return 找到返回索引，未找到返回-1
 */
int AmxModx_Bridge_ArrayFindValue(int handle, int value);

#ifdef __cplusplus
}
#endif