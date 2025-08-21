// newmenus_bridge.h
// 新菜单系统桥接接口定义
#pragma once

#include "../amxmodx.h"
#include "../CMenu.h"
#include "../newmenus.h"

#ifdef __cplusplus
extern "C" {
#endif

/**
 * 创建新菜单
 * @param title 菜单标题
 * @param handler 菜单处理函数
 * @param useMultiLanguage 是否使用多语言
 * @return 菜单句柄，失败返回-1
 */
int AmxModx_Bridge_MenuCreate(const char* title, void* handler, int useMultiLanguage);

/**
 * 销毁菜单
 * @param menuHandle 菜单句柄（传引用，成功后会置-1）
 * @return 是否成功销毁
 */
int AmxModx_Bridge_MenuDestroy(int* menuHandle);

/**
 * 添加菜单项
 * @param menuHandle 菜单句柄
 * @param text 菜单项文本
 * @param command 菜单项命令
 * @param access 访问权限
 * @return 菜单项索引，失败返回-1
 */
int AmxModx_Bridge_MenuAddItem(int menuHandle, const char* text, const char* command, int access);

/**
 * 添加空白菜单项
 * @param menuHandle 菜单句柄
 * @param slots 占用的槽位数
 * @return 是否成功添加
 */
int AmxModx_Bridge_MenuAddBlank(int menuHandle, int slots);

/**
 * 添加文本菜单项
 * @param menuHandle 菜单句柄
 * @param text 菜单项文本
 * @param slots 占用的槽位数
 * @return 是否成功添加
 */
int AmxModx_Bridge_MenuAddText(int menuHandle, const char* text, int slots);

/**
 * 显示菜单给玩家
 * @param menuHandle 菜单句柄
 * @param playerIndex 玩家索引
 * @param page 起始页码
 * @return 是否成功显示
 */
int AmxModx_Bridge_MenuDisplay(int menuHandle, int playerIndex, int page);

/**
 * 取消玩家菜单
 * @param playerIndex 玩家索引
 * @return 是否成功取消
 */
int AmxModx_Bridge_MenuCancel(int playerIndex);

/**
 * 设置菜单属性
 * @param menuHandle 菜单句柄
 * @param prop 属性类型
 * @param value 属性值
 * @return 是否成功设置
 */
int AmxModx_Bridge_MenuSetProperty(int menuHandle, int prop, int value);

/**
 * 获取菜单项信息
 * @param menuHandle 菜单句柄
 * @param itemIndex 菜单项索引
 * @param textBuffer 文本输出缓冲区
 * @param textBufferSize 文本缓冲区大小
 * @param commandBuffer 命令输出缓冲区
 * @param commandBufferSize 命令缓冲区大小
 * @param access 访问权限输出
 * @return 是否成功获取
 */
int AmxModx_Bridge_MenuGetItemInfo(int menuHandle, int itemIndex, 
    char* textBuffer, int textBufferSize,
    char* commandBuffer, int commandBufferSize,
    int* access);

/**
 * 设置菜单项名称
 * @param menuHandle 菜单句柄
 * @param itemIndex 菜单项索引
 * @param newName 新名称
 * @return 是否成功设置
 */
int AmxModx_Bridge_MenuSetItemName(int menuHandle, int itemIndex, const char* newName);

/**
 * 设置菜单项命令
 * @param menuHandle 菜单句柄
 * @param itemIndex 菜单项索引
 * @param newCommand 新命令
 * @return 是否成功设置
 */
int AmxModx_Bridge_MenuSetItemCommand(int menuHandle, int itemIndex, const char* newCommand);

/**
 * 获取菜单页数
 * @param menuHandle 菜单句柄
 * @return 页数，失败返回-1
 */
int AmxModx_Bridge_MenuGetPages(int menuHandle);

/**
 * 获取菜单项数
 * @param menuHandle 菜单句柄
 * @return 项数，失败返回-1
 */
int AmxModx_Bridge_MenuGetItems(int menuHandle);

/**
 * 查找菜单ID
 * @param menuName 菜单名称
 * @return 菜单ID，失败返回-1
 */
int AmxModx_Bridge_MenuFindId(const char* menuName);

/**
 * 获取玩家菜单信息
 * @param playerIndex 玩家索引
 * @param menuHandle 菜单句柄输出
 * @param item 选择项输出
 * @return 是否成功获取
 */
int AmxModx_Bridge_PlayerMenuInfo(int playerIndex, int* menuHandle, int* item);

#ifdef __cplusplus
}
#endif