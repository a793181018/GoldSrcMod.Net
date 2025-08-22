using GoldSrc.Amxmodx.Native;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Module.Global;
#pragma warning disable CS8981
using cell = int;
#pragma warning restore CS8981

namespace Module;

public unsafe class ZombieLevelNative
{
    public static AMX_NATIVE_INFO* nativeInfo = null;
    
    static ZombieLevelNative()
    {
        nativeInfo = (AMX_NATIVE_INFO*)Marshal.AllocHGlobal(sizeof(AMX_NATIVE_INFO) * 13);
        
        nativeInfo[0].name = "GiveExperienceOnKill".GetNativeString();
        nativeInfo[0].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeGiveExperienceOnKill;
        
        nativeInfo[1].name = "GiveExperienceOnDamage".GetNativeString();
        nativeInfo[1].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeGiveExperienceOnDamage;
        
        nativeInfo[2].name = "ShowLevelMenu".GetNativeString();
        nativeInfo[2].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeShowLevelMenu;
        
        nativeInfo[3].name = "ShowHumanSkillsMenu".GetNativeString();
        nativeInfo[3].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeShowHumanSkillsMenu;
        
        nativeInfo[4].name = "ShowZombieSkillsMenu".GetNativeString();
        nativeInfo[4].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeShowZombieSkillsMenu;
        
        nativeInfo[5].name = "ShowExperienceShopMenu".GetNativeString();
        nativeInfo[5].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeShowExperienceShopMenu;
        
        nativeInfo[6].name = "UpgradeHumanSkill".GetNativeString();
        nativeInfo[6].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeUpgradeHumanSkill;
        
        nativeInfo[7].name = "UpgradeZombieSkill".GetNativeString();
        nativeInfo[7].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeUpgradeZombieSkill;
        
        nativeInfo[8].name = "BuyExperienceWithAmmoPacks".GetNativeString();
        nativeInfo[8].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeBuyExperienceWithAmmoPacks;
        
        nativeInfo[9].name = "GetPlayerLevel".GetNativeString();
        nativeInfo[9].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeGetPlayerLevel;
        
        nativeInfo[10].name = "GetPlayerExperience".GetNativeString();
        nativeInfo[10].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeGetPlayerExperience;
        
        nativeInfo[11].name = "ResetPlayerLevel".GetNativeString();
        nativeInfo[11].func = (nint)(delegate* unmanaged[Cdecl]<AMX*, int*, int>)&ZombieLevelNative.NativeResetPlayerLevel;
        
        nativeInfo[12].name = null;
        nativeInfo[12].func = nint.Zero;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeGiveExperienceOnKill(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < 2 * sizeof(cell))
            return 0;
        
        int killer = @params[1];
        int victim = @params[2];
        
        ZombieLevelSystem.GiveExperienceOnKill(killer, victim);
        return 1;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeGiveExperienceOnDamage(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < 2 * sizeof(cell))
            return 0;
        
        int attacker = @params[1];
        float damage = *(float*)&@params[2];
        
        ZombieLevelSystem.GiveExperienceOnDamage(attacker, damage);
        return 1;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeShowLevelMenu(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < sizeof(cell))
            return 0;
        
        int player = @params[1];
        ZombieLevelSystem.ShowLevelMenu(player);
        return 1;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeShowHumanSkillsMenu(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < sizeof(cell))
            return 0;
        
        int player = @params[1];
        ZombieLevelSystem.ShowHumanSkillsMenu(player);
        return 1;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeShowZombieSkillsMenu(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < sizeof(cell))
            return 0;
        
        int player = @params[1];
        ZombieLevelSystem.ShowZombieSkillsMenu(player);
        return 1;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeShowExperienceShopMenu(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < sizeof(cell))
            return 0;
        
        int player = @params[1];
        ZombieLevelSystem.ShowExperienceShopMenu(player);
        return 1;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeUpgradeHumanSkill(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < 2 * sizeof(cell))
            return 0;
        
        int player = @params[1];
        int skill = @params[2];
        ZombieLevelSystem.UpgradeHumanSkill(player, skill);
        return 1;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeUpgradeZombieSkill(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < 2 * sizeof(cell))
            return 0;
        
        int player = @params[1];
        int skill = @params[2];
        ZombieLevelSystem.UpgradeZombieSkill(player, skill);
        return 1;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeBuyExperienceWithAmmoPacks(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < 2 * sizeof(cell))
            return 0;
        
        int player = @params[1];
        int option = @params[2];
        ZombieLevelSystem.BuyExperienceWithAmmoPacks(player, option);
        return 1;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeGetPlayerLevel(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < sizeof(cell))
            return 0;
        
        int player = @params[1];
        return ZombieLevelSystem.GetPlayerLevel(player);
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeGetPlayerExperience(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < sizeof(cell))
            return 0;
        
        int player = @params[1];
        return ZombieLevelSystem.GetPlayerExperience(player);
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static cell NativeResetPlayerLevel(AMX* amx, cell* @params)
    {
        if (@params == null || @params[0] < sizeof(cell))
            return 0;
        
        int player = @params[1];
        ZombieLevelSystem.ResetPlayerLevel(player);
        return 1;
    }
}