namespace Amxmodx.Bridge.ZombieLevel;

public static class ZombieLevelConfig
{
    // 经验设置
    public const int BaseExperiencePerKill = 100;
    public const float ExperiencePerDamage = 0.5f;
    public const int MaxLevel = 50;
    
    // 升级所需经验公式
    public static int GetExperienceRequiredForLevel(int level)
    {
        return 100 * level * level;
    }
    
    // 人类技能设置
    public const int MaxHumanHealthLevel = 10;
    public const int MaxHumanArmorLevel = 10;
    public const int MaxHumanSpeedLevel = 10;
    public const int MaxHumanGravityLevel = 10;
    public const int MaxHumanInvisibilityLevel = 5;
    
    // 僵尸技能设置
    public const int MaxZombieHealthLevel = 10;
    public const int MaxZombieArmorLevel = 10;
    public const int MaxZombieSpeedLevel = 10;
    public const int MaxZombieGravityLevel = 10;
    public const int MaxZombieInvisibilityLevel = 5;
    
    // 属性加成设置
    public const int HealthPerLevel = 20;
    public const int ArmorPerLevel = 15;
    public const float SpeedPerLevel = 0.05f;
    public const float GravityReductionPerLevel = 0.1f;
    public const float InvisibilityPerLevel = 0.1f;
    
    // 经验商店设置
    public const int ExperiencePerAmmoPack = 50;
    public const int MaxAmmoPacksPerPurchase = 100;
    
    // 文件路径
    public const string PlayerDataDirectory = "addons/amxmodx/data/zombie_level";
    public const string PlayerDataFile = "player_data.txt";
    
    // 菜单设置
    public const int MenuDisplayTime = 10; // 秒
    public const int ItemsPerPage = 7;
    
    // HUD设置
    public const int HUDDisplayTime = 5; // 秒
    public const int HUDChannel = 3;
    public const string HUDColor = "\x04"; // 绿色
}