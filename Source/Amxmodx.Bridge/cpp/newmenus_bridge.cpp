// newmenus_bridge.cpp
// 新菜单系统桥接实现

#include "newmenus_bridge.h"
#include "../newmenus.h"
#include "../CMisc.h"
#include "../amxmodx.h"

static ke::Vector<Menu*> g_MenuHandles;
static int g_MenuHandleCounter = 0;

static Menu* GetMenuByHandle(int handle)
{
    if (handle < 0 || handle >= g_MenuHandles.length())
        return nullptr;
    return g_MenuHandles[handle];
}

int AmxModx_Bridge_MenuCreate(const char* title, void* handler, int useMultiLanguage)
{
    if (!title || !handler)
        return -1;

    // 创建新菜单
    Menu* menu = new Menu(title, nullptr, (int)(intptr_t)handler, useMultiLanguage != 0);
    if (!menu)
        return -1;

    int handle = g_MenuHandles.length();
    g_MenuHandles.append(menu);
    menu->thisId = handle;

    return handle;
}

int AmxModx_Bridge_MenuDestroy(int* menuHandle)
{
    if (!menuHandle || *menuHandle < 0)
        return 0;

    Menu* menu = GetMenuByHandle(*menuHandle);
    if (!menu)
        return 0;

    delete menu;
    g_MenuHandles[*menuHandle] = nullptr;
    *menuHandle = -1;
    return 1;
}

int AmxModx_Bridge_MenuAddItem(int menuHandle, const char* text, const char* command, int access)
{
    Menu* menu = GetMenuByHandle(menuHandle);
    if (!menu || !text)
        return -1;

    menuitem* item = menu->AddItem(text, command ? command : "", access);
    return item ? static_cast<int>(item->id) : -1;
}

int AmxModx_Bridge_MenuAddBlank(int menuHandle, int slots)
{
    Menu* menu = GetMenuByHandle(menuHandle);
    if (!menu)
        return 0;

    // Menu类没有AddBlank方法，使用AddItem创建空白项
    menu->AddItem("", "", 0);
    return 1;
}

int AmxModx_Bridge_MenuAddText(int menuHandle, const char* text, int slots)
{
    Menu* menu = GetMenuByHandle(menuHandle);
    if (!menu || !text)
        return 0;

    // Menu类没有AddText方法，使用AddItem创建文本项
    menu->AddItem(text, "", 0);
    return 1;
}

int AmxModx_Bridge_MenuDisplay(int menuHandle, int playerIndex, int page)
{
    Menu* menu = GetMenuByHandle(menuHandle);
    if (!menu || playerIndex < 1 || playerIndex > gpGlobals->maxClients)
        return 0;

    CPlayer* player = GET_PLAYER_POINTER_I(playerIndex);
    if (!player || !player->ingame)
        return 0;

    menu->Display(playerIndex, page);
    return 1;
}

int AmxModx_Bridge_MenuCancel(int playerIndex)
{
    if (playerIndex < 1 || playerIndex > gpGlobals->maxClients)
        return 0;

    CPlayer* player = GET_PLAYER_POINTER_I(playerIndex);
    if (!player || !player->ingame)
        return 0;

    return CloseNewMenus(player) ? 1 : 0;
}

int AmxModx_Bridge_MenuSetProperty(int menuHandle, int prop, int value)
{
    Menu* menu = GetMenuByHandle(menuHandle);
    if (!menu)
        return 0;

    switch (prop)
    {
        case 0: // MENUPROP_PERPAGE
            menu->items_per_page = value;
            break;
        case 1: // MENUPROP_BACKNAME
            menu->m_OptNames[abs(MENU_BACK)] = "Back";
            break;
        case 2: // MENUPROP_NEXTNAME
            menu->m_OptNames[abs(MENU_MORE)] = "More";
            break;
        case 3: // MENUPROP_EXITNAME
            menu->m_OptNames[abs(MENU_EXIT)] = "Exit";
            break;
        case 4: // MENUPROP_NEVEREXIT
            menu->m_NeverExit = value != 0;
            break;
        case 5: // MENUPROP_FORCEEXIT
            menu->m_ForceExit = value != 0;
            break;
        case 6: // MENUPROP_AUTOSORT
            // 自动排序属性已弃用
            break;
        default:
            return 0;
    }

    return 1;
}

int AmxModx_Bridge_MenuGetItemInfo(int menuHandle, int itemIndex,
    char* textBuffer, int textBufferSize,
    char* commandBuffer, int commandBufferSize,
    int* access)
{
    Menu* menu = GetMenuByHandle(menuHandle);
    if (!menu || !textBuffer || !commandBuffer || !access)
        return 0;

    menuitem* item = menu->GetMenuItem(itemIndex);
    if (!item)
        return 0;

    strncpy(textBuffer, item->name.chars(), textBufferSize - 1);
    textBuffer[textBufferSize - 1] = '\0';

    strncpy(commandBuffer, item->cmd.chars(), commandBufferSize - 1);
    commandBuffer[commandBufferSize - 1] = '\0';

    *access = item->access;
    return 1;
}

int AmxModx_Bridge_MenuSetItemName(int menuHandle, int itemIndex, const char* newName)
{
    Menu* menu = GetMenuByHandle(menuHandle);
    if (!menu || !newName)
        return 0;

    menuitem* item = menu->GetMenuItem(itemIndex);
    if (!item)
        return 0;

    item->name = newName;
    return 1;
}

int AmxModx_Bridge_MenuSetItemCommand(int menuHandle, int itemIndex, const char* newCommand)
{
    Menu* menu = GetMenuByHandle(menuHandle);
    if (!menu || !newCommand)
        return 0;

    menuitem* item = menu->GetMenuItem(itemIndex);
    if (!item)
        return 0;

    item->cmd = newCommand;
    return 1;
}

int AmxModx_Bridge_MenuGetPages(int menuHandle)
{
    Menu* menu = GetMenuByHandle(menuHandle);
    if (!menu)
        return -1;

    return (int)menu->GetPageCount();
}

int AmxModx_Bridge_MenuGetItems(int menuHandle)
{
    Menu* menu = GetMenuByHandle(menuHandle);
    if (!menu)
        return -1;

    return (int)menu->GetItemCount();
}

int AmxModx_Bridge_MenuFindId(const char* menuName)
{
    if (!menuName)
        return -1;

    return g_menucmds.findMenuId(menuName);
}

int AmxModx_Bridge_PlayerMenuInfo(int playerIndex, int* menuHandle, int* item)
{
    if (playerIndex < 1 || playerIndex > gpGlobals->maxClients || !menuHandle || !item)
        return 0;

    CPlayer* player = GET_PLAYER_POINTER_I(playerIndex);
    if (!player || !player->ingame)
        return 0;

    *menuHandle = player->newmenu;
    *item = 0; // 简化处理，实际应该获取当前选中的菜单项
    return 1;
}