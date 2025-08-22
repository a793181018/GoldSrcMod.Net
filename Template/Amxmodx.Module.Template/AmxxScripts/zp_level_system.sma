#include <zombieplague>

#define PLUGIN "ZP Level System"
#define VERSION "1.0"
#define AUTHOR "GoldSrcMod.Net"

public plugin_init()
{
    register_plugin(PLUGIN, VERSION, AUTHOR)
    
    // 注册事件
    register_event("DeathMsg", "EventDeathMsg", "a")
    register_event("Damage", "EventDamage", "b", "2 > 0")
    
    // 注册命令
    register_clcmd("say /level", "CmdLevelMenu")
    register_clcmd("say /skills", "CmdLevelMenu")
    register_clcmd("say /menu", "CmdLevelMenu")
    
    // 注册菜单
    register_menu("人类技能", 1023, "MenuHumanSkills")
    register_menu("僵尸技能", 1023, "MenuZombieSkills")
    register_menu("经验商店", 1023, "MenuExperienceShop")
}

public plugin_precache()
{
    // 预缓存资源
}

public EventDeathMsg()
{
    new killer = read_data(1)
    new victim = read_data(2)
    
    if (is_user_connected(killer) && is_user_connected(victim))
    {
        if (killer != victim)
        {
            // 击杀获得经验
            GiveExperienceOnKill(killer, victim)
        }
    }
}

public EventDamage(victim, inflictor, attacker, damage, damagebits)
{
    if (is_user_connected(attacker) && is_user_connected(victim))
    {
        if (attacker != victim)
        {
            // 造成伤害获得经验
            GiveExperienceOnDamage(attacker, damage)
        }
    }
}

public CmdLevelMenu(id)
{
    if (!is_user_connected(id))
        return PLUGIN_HANDLED
    
    ShowLevelMenu(id)
    return PLUGIN_HANDLED
}

public MenuHumanSkills(id, menu, item)
{
    if (item == MENU_EXIT)
    {
        menu_cancel(menu)
        return PLUGIN_HANDLED
    }
    
    new skill = item
    UpgradeHumanSkill(id, skill)
    
    // 重新显示菜单
    ShowHumanSkillsMenu(id)
    
    return PLUGIN_HANDLED
}

public MenuZombieSkills(id, menu, item)
{
    if (item == MENU_EXIT)
    {
        menu_cancel(menu)
        return PLUGIN_HANDLED
    }
    
    new skill = item
    UpgradeZombieSkill(id, skill)
    
    // 重新显示菜单
    ShowZombieSkillsMenu(id)
    
    return PLUGIN_HANDLED
}

public MenuExperienceShop(id, menu, item)
{
    if (item == MENU_EXIT)
    {
        menu_cancel(menu)
        return PLUGIN_HANDLED
    }
    
    new option = item
    BuyExperienceWithAmmoPacks(id, option)
    
    // 重新显示菜单
    ShowExperienceShopMenu(id)
    
    return PLUGIN_HANDLED
}

// 原生函数声明
native GiveExperienceOnKill(killer, victim);
native GiveExperienceOnDamage(attacker, Float:damage);
native ShowLevelMenu(player);
native ShowHumanSkillsMenu(player);
native ShowZombieSkillsMenu(player);
native ShowExperienceShopMenu(player);
native UpgradeHumanSkill(player, skill);
native UpgradeZombieSkill(player, skill);
native BuyExperienceWithAmmoPacks(player, option);

// 辅助函数
stock bool:is_user_connected(index)
{
    return (1 <= index <= 32) && (get_user_state(index) != 0);
}

stock Float:get_user_health(index)
{
    new Float:health;
    entity_get_float(index, EV_FL_health, health);
    return health;
}

stock set_user_health(index, Float:health)
{
    entity_set_float(index, EV_FL_health, health);
}