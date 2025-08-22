/* 本插件由 AMXX-Studio 中文版自动生成 */
/* UTF-8 func by www.DT-Club.net */
/*
*    网盘地址:351642983.ys168.com QQ群:376642936
*    QQ:351642983【Halo】和516331944【飞飞】
*V1.0系列:
*    V1.2：大幅度更新美化及修复BUG，增加龟波
*    V1.21：修复部分BUG,本版本所属测试版本
*    V1.22：调整高等级时人类过于变态的技能，修复有时候击打僵尸不能获得经验的BUG及优化插件,增加两个技能的特效
*    V1.23：优化插件，增加升级的宏定义,使修改更加容易
*
*V2.0系列:
*    V2.0:在原有的基础上优化插件的格式,并且对特效进行优化，重新定义保存格式,并减少部分头文件的使用
*    V2.01:根据BXY的提供,增加一个显示格式,就是只有一个频道的左下角显示(显示类型3)；再增加GUI显示(右上角,非ACG,类型4)，修复与长征N号连用构成的识别BUG
*    V2.02:显示中增加等级上限的显示,修复满级技能未能学完的BUG
*    V2.1最终版本:重新计算获取经验的方法,修复玩家提出的BUG,并对修改的名字进行检测,降低技能的属性,删除amxx.cfg的配置信息
*    V2.11留念版本:修复选择保存后依然再提示一次的BUG
*/
//技能总注释
/*僵尸技能 7个
  人类技能 15 个
  变量介绍：
  human_LevelNum 人类技能当前等级
  zb_LevelNum    僵尸技能当前等级
  human_LevelSkillMAX 人类技能最大等级
  zb_LevelSkillMAX 僵尸技能当前等级
   human_szLevelName 人类技能名列表 
   zb_szLevelName 僵尸技能名列表 
   可以根据 人类/僵尸技能名列表 与 人类/僵尸技能当前等级
   直接搜索到指定功能！！*/
#include <amxmodx>
#include <fakemeta>
#include <zombieplague>
#include <hamsandwich>
#include <engine>
#include <xs>
#include <cstrike>

#define PLUGIN_NAME    "等级插件"
#define PLUGIN_VERSION    "V2.11留念版本"
#define PLUGIN_AUTHOR    "Halo和飞飞"

#define fm_get_user_oldbutton(%1) pev(%1, pev_oldbuttons)
#define fm_get_user_button(%1) pev(%1, pev_button)
#define fm_get_entity_flags(%1) pev(%1, pev_flags)




//嗜血一击每级所吸的血占攻击的百分比
#define SKILLGAINHEALTH 0.257

//致命一击每级对僵尸增加的伤害倍数
#define SKILLEFFECTHEALTH 3.5

//人类提升血量每级增加的生命
#define SKILLHEALTH 40.0
//提升血量每级增加的生命
#define zb_SKILLHEALTH 1000.0

//人类提升护甲每级增加的护甲
#define SKILLARMOR 15.0

//人类减轻重力每级减少的重力
#define SKILLGRAVITY 120

//人类提升速度每级增加的速度
#define SKILLSPEED 20.0

//人类增加透明度每级增加的透明度
#define SKILLRENDER 75

//人类提升火力每级增加的威力倍数
#define SKILLATTACK 0.35

//手雷补给升级后每多少秒补给一个手雷
#define SKILLHEGRENADE 60.0

//不保存时玩家获得经验的倍率
#define NOSAVEEXEGAIN 5.0

//保存时玩家获得经验的倍率
#define SAVEEXEGAIN 1.0

//购买经验玩家获得经验数
#define BUYEXEGAIN 10000

//购买经验所用的弹药袋
#define BUYEXEPACKS 200

//购买等级所用的弹药袋(公式：等级*n)
#define BUYLEVELPACKS 100

//神龟冲击波对单只僵尸的最高伤害(可群攻)
#define SKILLEDAMAGE 900.0

//神龟冲击波爆炸攻击的范围
#define SFROUND 180.0

//等级信息的显示类型(1为HUD,2为屏幕中央,3为左下角,4为右上角图片显示)
#define SHOWTYPE 3

//经验获取的模式(1为以攻击的伤害数计算,2为杀敌获取)
#define EXEGAIN 1

//若经验获取模式为2,那么,杀敌时经验所能获取的数目的底值为多少
#define EXEGAIN2EXE 10000.0

//经验HUD信息的显示频道(1到4,若有冲突请修改频道)
#define HUDCHANNEL 3

//重置技能点所需的经验
#define RESETPOINTEXE 2000

const m_weapId = 43
const m_flTimeWeaponIdle = 48
const m_weapInReload = 54
const m_flNextAttack = 83
const OFFSET_LINUX_WEAPONS = 4
const OFFSET_LINUX = 5
const m_flNextPrimaryAttack = 46
new const weapon_reloadtime[][] = {
    "-1.0",    //-----
    "-1.0",    //p228
    "-1.0",    //-----
    "-1.0",    //scout
    "-1.0",    //-----
    "-1.0",    //xm1014
    "-1.0",    //-----
    "-1.0",    //mac10
    "-1.0",    //aug
    "-1.0",    //-----
    "-1.0",    //elites
    "-1.0",    //fiveseven
    "-1.0",    //ump45
    "-1.0",    //sg550
    "-1.0",    //galil
    "-1.0",    //famas
    "-1.0",    //usp
    "-1.0",    //glock
    "-1.0",    //awp
    "-1.0",    //mp5navy
    "-1.0",    //m249
    "-1.0",    //m3
    "-1.0",    //m4a1
    "-1.0",    //tmp
    "-1.0",    //g3sg1
    "-1.0",    //-----
    "-1.0",    //deagle
    "-1.0",    //sg552
    "-1.0",    //ak47
    "-1.0", //-----
    "-1.0"    //p90
}
new const weapon_firerate[][] = {
    "-1.0",    //-----
    "-1.0",    //p228
    "-1.0",    //-----
    "-1.0",    //scout
    "-1.0",    //-----
    "-1.0",    //xm1014
    "-1.0",    //-----
    "-1.0",    //mac10
    "-1.0",    //aug
    "-1.0",    //-----
    "-1.0",    //elites
    "-1.0",    //fiveseven
    "-1.0",    //ump45
    "-1.0",    //sg550
    "-1.0",    //galil
    "-1.0",    //famas
    "-1.0",    //usp
    "-1.0",    //glock
    "-1.0",    //awp
    "-1.0",    //mp5navy
    "-1.0",    //m249
    "-1.0",    //m3
    "-1.0",    //m4a1
    "-1.0",    //tmp
    "-1.0",    //g3sg1
    "-1.0",    //-----
    "-1.0",    //deagle
    "-1.0",    //sg552
    "-1.0",    //ak47
    "-1.0", //-----
    "-1.0"    //p90
}
new const weapon_knockback[][] = {
    "-1.0",    //-----
    "-1.0",    //p228
    "-1.0",    //-----
    "-1.0",    //scout
    "-1.0",    //-----
    "-1.0",    //xm1014
    "-1.0",    //-----
    "-1.0",    //mac10
    "-1.0",    //aug
    "-1.0",    //-----
    "-1.0",    //elites
    "-1.0",    //fiveseven
    "-1.0",    //ump45
    "-1.0",    //sg550
    "-1.0",    //galil
    "-1.0",    //famas
    "-1.0",    //usp
    "-1.0",    //glock
    "-1.0",    //awp
    "-1.0",    //mp5navy
    "-1.0",    //m249
    "-1.0",    //m3
    "-1.0",    //m4a1
    "-1.0",    //tmp
    "-1.0",    //g3sg1
    "-1.0",    //-----
    "-1.0",    //deagle
    "-1.0",    //sg552
    "-1.0",    //ak47
    "-1.0",    //knife
    "-1.0"    //p90
}


new const weapon_classname[][]={"","weapon_p228","","weapon_scout","","weapon_xm1014","","weapon_mac10","weapon_aug","","weapon_elite","weapon_fiveseven","weapon_ump45","weapon_sg550","weapon_galil","weapon_famas","weapon_usp","weapon_glock18","weapon_awp","weapon_mp5navy","weapon_m249","weapon_m3","weapon_m4a1","weapon_tmp","weapon_g3sg1","","weapon_deagle","weapon_sg552","weapon_ak47","weapon_p90"}
new const sz_soundplay[]="weapons/explode3.wav"
new const file[]={"\addons\amxmodx\configs\Zp_SimpleLevel.ini"}
new g_LevelPoint[33],g_LevelExe[33],g_Level[33],g_LevelTotalExe[33],g_jump[33]=0,zb_LevelPoint[33]
new bool:g_levelSave[33]=false,bool:g_PD[33],bool:g_explosion[33]=false,bool:g_guishow[33],bool:g_taskid[33],bool:zb_guishow[33]
new g_fwBotForwardRegister,g_MaxLevel=0,sz_exposion,tr_fl
new g_weap_reloadtime[sizeof weapon_classname]
new user_reload_weapon[33]
new bool:user_reload_set[33]
new g_weap_knockback[sizeof weapon_classname]
new cvar_check_bitsum
new g_Status, cc, g_Amount, g_ZombiePlague, g_Nemesis, g_FirstZombie, g_LastZombie,STANDTIME=3,Float:g_fLastThink[33],g_Time=0.5,bool:ready[33];
 
new g_weap_firerate[sizeof weapon_classname]
//总共多少个技能
#define human_SKILLNUM 15//人类技能总数
#define zb_SKILLNUM 7 //僵尸技能总数
new human_LevelNum[33][human_SKILLNUM]
new zb_LevelNum[33][zb_SKILLNUM]
//技能的最高等级
new human_LevelSkillMAX[human_SKILLNUM]=
{
    5,    //"血量提升"
    8,    //"护甲提升"
    4,    //"重力降低"
    3,    //"速度提升"
    3,    //"隐身之术"
    3,    //"武器火力"
    6,    //"无后坐力"
    2,    //"致命一击"
    3,    //"嗜血一击"
    1,    //"手雷补给"
    1,    //"多重跳跃"
    1,    //"神龟冲击波"
    5,
    5,
    5
    
}

new zb_LevelSkillMAX[zb_SKILLNUM]=
{
    5,    //"血量提升"
    4,    //"重力降低"
    3,    //"速度提升"
    3,    //"隐身之术"
    3,    //"武器火力"
    1,    //"多重跳跃"
    5
    
}

//升级所需的技能点
new human_LevelSkillPonit[human_SKILLNUM]=
{
    1,    //"血量提升"
    2,    //"护甲提升"
    3,    //"重力降低"
    3,    //"速度提升"
    4,    //"隐身之术"
    7,    //"武器火力"
    4,    //"无后坐力"
    5,    //"致命一击"
    3,    //"嗜血一击"
    3,    //"手雷补给"
    7,    //"多重跳跃"
    10,   //"神龟冲击波"
    3,    
    3,
    3
}
new zb_LevelSkillPonit[zb_SKILLNUM]=
{
    1,    //"血量提升"
    3,    //"重力降低"
    3,    //"速度提升"
    4,    //"隐身之术"
    7,    //"武器火力"
    7,    //"多重跳跃"
    3
}
new human_szLevelName[human_SKILLNUM][]=
{
    "血量提升",
    "护甲提升",
    "重力降低",
    "速度提升",
    "隐身之术",
    "武器火力",
    "后坐力减少",
    "致命一击",
    "嗜血一击",
    "手雷补给",
    "多重跳跃",
    "神龟冲击波",
    "弹药更换速度提升",
    "开火速度提升",
    "武器击退提升"
   
}//人类技能列表

new zb_szLevelName[zb_SKILLNUM][]=
{
    "血量提升",
    "重力降低",
    "速度提升",
    "隐身之术",
    "破防之爪",
    "多重跳跃",
    "抗武器击退"
}//僵尸技能列表
public plugin_natives()
{
    register_native("ZPLevel_GetMaxHealth","ZP_GetMaxHealth",1)
}
public plugin_init()
{
    register_plugin(PLUGIN_NAME, PLUGIN_VERSION, PLUGIN_AUTHOR);
    
       g_Status = register_cvar("zp_regeneration", "1");
       g_Time = register_cvar("zp_regen_time", "1");
       g_Amount = register_cvar("zp_regen_amount", "25");
       g_Nemesis = register_cvar("zp_regen_nemesis", "1");
       g_FirstZombie = register_cvar("zp_regen_firstzombie", "1");
       g_LastZombie = register_cvar("zp_regen_lastzombie", "1");
       register_event("Damage", "SetRegeneration", "be", "2>0");
       g_ZombiePlague = get_cvar_pointer("zp_on");
    register_clcmd("say /levelmenu","mainMenu")
    register_event("ResetHUD","set_human","b")
    register_event("ResetHUD","set_zombie","b")
    register_clcmd("say resetlevel","resetlevel")
    RegisterHam(Ham_Touch,"info_target","npc_remove")
    register_forward(FM_ClientUserInfoChanged, "fw_ClientUserInfoChanged")
    //register_clcmd("say /leveltest","leveltest")
       new cvar_string[32]
    for(new i=0;i<sizeof weapon_classname;i++)
    {
        if(strlen(weapon_classname[i])==0)
            continue
         formatex(cvar_string, charsmax(cvar_string), "fuzhi", weapon_classname[i][7])
        g_weap_reloadtime[i] = register_cvar(cvar_string, weapon_reloadtime[i])
        g_weap_firerate[i] = register_cvar(cvar_string, weapon_firerate[i])
        g_weap_knockback[i] = register_cvar(cvar_string, weapon_knockback[i])
        RegisterHam(Ham_Weapon_PrimaryAttack,weapon_classname[i],"fw_WeapPriAttack",1)
        RegisterHam(Ham_Item_PostFrame, weapon_classname[i], "fw_Item_PostFrame", 1)
        RegisterHam(Ham_Weapon_Reload, weapon_classname[i], "fw_WeaponReload", 1)
         RegisterHam(Ham_Weapon_PrimaryAttack, weapon_classname[i], "kaihuosudu", 1)
        
          
	  
    }
     RegisterHam(Ham_TraceAttack, "player", "fw_TraceAttack")
      
    #if defined SUPPORT_CZBOT
    // CZBot support
    cvar_botquota = get_cvar_pointer("bot_quota")
    #endif
    for(new i=0;i<human_SKILLNUM;i++)
        g_MaxLevel+=human_LevelSkillMAX[i]*human_LevelSkillPonit[i]
    g_MaxLevel+=1
    
    RegisterHam(Ham_TakeDamage, "player", "HAM_TakeDamage")
    g_fwBotForwardRegister = register_forward(FM_PlayerPostThink, "fw_BotForwardRegister_Post", 1)
}
public float:ZP_GetMaxHealth()
{
  new float: health;
  health=zp_get_zombie_maxhealth()+zb_SKILLHEALTH*zb_LevelNum[id][0];
   return health;
}
public fuzhi(weapon)
{
    static owner
    owner=pev(weapon,pev_owner)
    cs_get_weapon_id(owner)
    
    
    
    
    
}
public fw_Item_PostFrame(weapon)
{
    if (!pev_valid(weapon))
        return HAM_IGNORED;
    
    static weap_id
    weap_id = fm_get_weaponid(weapon)
    
    static owner
    owner = pev(weapon, pev_owner)
    
    if (is_weapon_in_reload(weapon))
    {
        user_reload_weapon[owner] = weap_id
    }
    else
    {
        if (user_reload_weapon[owner] != -1)
        {
            if (weap_id == user_reload_weapon[owner] && user_reload_set[owner])
            {
                if (weap_id == CSW_USP && !cs_get_weapon_silen(weapon))
                    SendWeaponAnim(owner, 8)
                else if (weap_id == CSW_M4A1 && !cs_get_weapon_silen(weapon))
                    SendWeaponAnim(owner, 7)
                else
                    SendWeaponAnim(owner, 0)
                    engfunc(EngFunc_EmitSound, owner, CHAN_ITEM, "weapons/clipin1.wav", 1.0, ATTN_NORM, 0, PITCH_NORM)
                    user_reload_set[owner] = false
            }
        }
        user_reload_weapon[owner] = -1
    }
    return HAM_IGNORED;
}

public fw_WeaponReload(weapon)//弹药更换速度
{
    if (!pev_valid(weapon))
        return HAM_IGNORED;
    
    if (!is_weapon_in_reload(weapon))
        return HAM_IGNORED;
    
  
    
    static owner
    owner = pev(weapon, pev_owner)
    
    static Float:multiplier
    multiplier =1-human_LevelNum[owner][12]*0.15
    
    if (multiplier < 0.0 || multiplier == 1.0)
    {
        user_reload_set[owner] = false
        return HAM_IGNORED;
    }
    
    static Float:time
    time = get_user_next_attack(owner) * multiplier
    set_user_next_attack(owner, time)
    set_weapon_idle_time(weapon, time + 0.5)
    set_pev(owner, pev_frame, 200.0)
    
    user_reload_set[owner] = true
    
    return HAM_IGNORED;
}
public kaihuosudu(weapon)//武器射速
{
    if (!pev_valid(weapon))
        return HAM_IGNORED;
    
    static owner
    owner = pev(weapon, pev_owner)
    
    static Float:multiplier
    multiplier =1-human_LevelNum[owner][13]*0.15
    
    if (multiplier <= 0.0)
        return HAM_IGNORED;
    
    static Float:next_attack_delay
    next_attack_delay = get_weapon_next_attack_dealy(weapon) * multiplier
    set_weapon_next_attack_dealy(weapon, next_attack_delay)
    
    return HAM_IGNORED;
}
public fw_TraceAttack(victim, attacker, Float:damage, Float:direction[3], tracehandle, damage_type)
{
    // Non-player damage or self damage
    if (victim == attacker || !is_user_connected(attacker))
        return HAM_IGNORED;
    
    // If not bullet damage
    if (!(damage_type & DMG_BULLET))
        return HAM_IGNORED;
    
    static weap_id
    weap_id = get_user_weapon(attacker)
    
    if (!((1<<weap_id) & cvar_check_bitsum))
        return HAM_IGNORED;
    
    // Get knockback multiplier
    static Float:multiplier
    multiplier = 1+human_LevelNum[attacker][14]*0.15-zb_LevelNum[victim][6]*0.15
    
    // Use weapon power on knockback calculation
    if (multiplier < 0.0)
        return HAM_IGNORED;
    
    static Float:hit_direction[3]
    hit_direction = direction
    
    // Get victim's velocity
    static Float:velocity[3]
    pev(victim, pev_velocity, velocity)
    
    // Get knockback direction value
    xs_vec_mul_scalar(hit_direction, multiplier, hit_direction)
    xs_vec_mul_scalar(hit_direction, damage, hit_direction)
    xs_vec_add(velocity, hit_direction, hit_direction)
    
    // If use knife attack, set knockback direction change vertical angle up 15.0
    if (weap_id == CSW_KNIFE)
        set_vector_change_angle2(hit_direction, 0.0, 15.0, hit_direction)
    
    // Set the knockback'd victim's velocity
    set_pev(victim, pev_velocity, hit_direction)
    
    SetHamParamVector(4, Float:{0.0, 0.0, 0.0});
    
    return HAM_IGNORED;
}


public message_cur_weapon(msg_id, msg_dest, id)
{
    // Player not alive or not an active weapon
    if (!is_user_alive(id) || get_msg_arg_int(1) != 1)
        return;
    
    user_reload_weapon[id] = -1
    user_reload_set[id] = false
}

stock fm_get_weaponid(entity)
{
    return get_pdata_int(entity, m_weapId, OFFSET_LINUX_WEAPONS);
}

stock is_weapon_in_reload(entity)
{
    return get_pdata_int(entity, m_weapInReload, OFFSET_LINUX_WEAPONS);
}

stock Float:get_user_next_attack(id)
{
    return get_pdata_float(id, m_flNextAttack, OFFSET_LINUX)
}

stock set_user_next_attack(id, Float:time)
{
    set_pdata_float(id, m_flNextAttack, time, OFFSET_LINUX)
}

stock set_weapon_idle_time(entity, Float:time)
{
    set_pdata_float(entity, m_flTimeWeaponIdle, time, OFFSET_LINUX_WEAPONS)
}

stock SendWeaponAnim(id, iAnim)
{
    set_pev(id, pev_weaponanim, iAnim)
    
    message_begin(MSG_ONE_UNRELIABLE, SVC_WEAPONANIM, _, id)
    write_byte(iAnim)
    write_byte(pev(id, pev_body))
    message_end()
}
stock set_weapon_next_attack_dealy(entity, Float:time)
{
    set_pdata_float(entity, m_flNextPrimaryAttack, time, OFFSET_LINUX_WEAPONS)
}
stock Float:get_weapon_next_attack_dealy(entity)
{
    return get_pdata_float(entity, m_flNextPrimaryAttack, OFFSET_LINUX_WEAPONS)
}
stock set_vector_change_angle2(const Float:velocity[3], Float:angle, Float:vertical_angle, Float:new_velocity[3])
{
    new Float:vec_angles[3]
    vector_to_angle(velocity, vec_angles)
    
    vec_angles[1] += angle
    while (vec_angles[1] < 0.0)
        vec_angles[1] += 360.0
    
    vec_angles[0] += vertical_angle
    while (vec_angles[0] < 0.0)
        vec_angles[0] += 360.0
    
    new Float:vec_length
    vec_length  = vector_length(velocity)
    
    new Float:temp
    temp = vec_length * floatcos(vec_angles[0], degrees)
    
    new_velocity[0] = temp * floatcos(vec_angles[1], degrees)
    new_velocity[1] = temp * floatsin(vec_angles[1], degrees)
    new_velocity[2] = vec_length * floatsin(vec_angles[0], degrees)
}


public plugin_end()
{
    for(new id=1;id<=get_maxplayers();id++)
    {
        if(is_user_connected(id))
        {
            if(g_levelSave[id])
                save_score(id)
            remove_task(id)
            g_LevelExe[id]=0
            g_LevelTotalExe[id]=1000
            g_Level[id]=1
            g_LevelPoint[id]=0
            for(new i=0;i<human_SKILLNUM;i++)
                human_LevelNum[id][i]=0
        }
    }
}

public fw_ClientUserInfoChanged(id)
{
    if (is_user_bot(id))
        return FMRES_IGNORED
    
    new name[33]
    get_user_name(id,name,32)
    
    if(contain(name,"`")!=-1)
    {
        server_cmd("kick #%d ^"名字含有非法字符,已被系统踢出^"",get_user_userid(id))
        remove_task(id)
        g_LevelExe[id]=0
        g_LevelTotalExe[id]=1000
        g_Level[id]=1
        g_LevelPoint[id]=0
        for(new i=0;i<human_SKILLNUM;i++)
            human_LevelNum[id][i]=0
    }
    
    return FMRES_IGNORED
}

public zp_user_humanized_post(id)
{
   set_task(3.0,"set_human",  id)
}
public zp_user_infected_post(id)
{
    set_task(3.0,"set_zombie",  id)

}
public leveltest(id)
{
    g_LevelExe[id]+=10000000
}

public plugin_precache()
{
    sz_exposion=precache_model("sprites/explosion1.spr")
    tr_fl=precache_model("sprites/white.spr")
}

public resetlevel(id)
{
    g_LevelExe[id]=0
    g_LevelTotalExe[id]=1000
    g_Level[id]=1
    g_LevelPoint[id]=0
    g_PD[id]=true
    g_levelSave[id]=false
    g_taskid[id]=false
    save_score(id)
    client_color(id,"等级设置已全部重置")
}

public set_human(id)
{
    if(!zp_get_user_zombie(id))
    {
        new Float:fStandFor
        pev(id,pev_health,fStandFor)
        set_pev(id,pev_health,fStandFor+SKILLHEALTH*human_LevelNum[id][0])
        pev(id,pev_armorvalue,fStandFor)
        set_pev(id,pev_gravity,1.0-(float(SKILLGRAVITY)/1000)*human_LevelNum[id][2])
        set_pev(id,pev_armorvalue,SKILLARMOR*human_LevelNum[id][1])
        set_pev(id,pev_maxspeed,250.0+SKILLSPEED*human_LevelNum[id][3])
        fm_set_rendering(id,kRenderFxGlowShell,0,0,0,kRenderTransAlpha, 255-SKILLRENDER*human_LevelNum[id][4])
        set_task(3.0,"set_gravity",id)
    }
   
    if(SHOWTYPE==4&&!g_guishow[id])
    {
        g_guishow[id]=true
    }
    if(g_PD[id])
    {
        if(!g_taskid[id])
        {
            set_task(15.0,"savemenu",id+320)
        }
        else
        {
            g_PD[id]=false
            g_levelSave[id]=true
            client_color(id,"由于你没进行选择,自动帮你设置为保存经验")
        }
    }
}
public set_zombie(id)
{
    if(zp_get_user_zombie(id))
    {
      if(!zp_get_user_first_zombie(id))
      {
        new Float:fStandFor
      /*  pev(id,pev_max_health,fStandFor)
        set_pev(id,pev_health,fStandFor*2+zb_SKILLHEALTH*zb_LevelNum[id][0])*/
        	set_pev(id,pev_health,zp_get_zombie_maxhealth()+zb_SKILLHEALTH*zb_LevelNum[id][0])
        }else
	{        new Float:fStandFor
                    /* pev(id,pev_health,fStandFor)
                     set_pev(id,pev_health,fStandFor+zb_SKILLHEALTH*zb_LevelNum[id][0])*/
                     set_pev(id,pev_health,zp_get_zombie_maxhealth()+zb_SKILLHEALTH*zb_LevelNum[id][0])
	}
        new Float:fStandFor
        pev(id,pev_gravity,fStandFor)
        set_pev(id,pev_gravity,fStandFor-(float(SKILLGRAVITY)/1000)*zb_LevelNum[id][1])   
        set_pev(id,pev_maxspeed,250.0+SKILLSPEED*zb_LevelNum[id][2])
        fm_set_rendering(id,kRenderFxGlowShell,0,0,0,kRenderTransAlpha, 255-SKILLRENDER*zb_LevelNum[id][3])
        set_task(3.0,"set_gravity",id)
    }
    if(SHOWTYPE==4&&!g_guishow[id])
    {
        g_guishow[id]=true
    }
    if(g_PD[id])
    {
        if(!g_taskid[id])
        {
            set_task(15.0,"savemenu",id+320)
        }
        else
        {
            g_PD[id]=false
            g_levelSave[id]=true
            client_color(id,"由于你没进行选择,自动帮你设置为保存经验")
        }
    }
}


public set_gravity(id)
{
    if(!zp_get_user_zombie(id))
    {
        if(is_user_alive(id))
        {
            
            set_pev(id,pev_gravity,1.0-(float(SKILLGRAVITY)/1000)*human_LevelNum[id][2])
        }    
    }
    else{
    	if(is_user_alive(id))
        {
            if(zp_get_user_zombie(id))
    {
      if(!zp_get_user_first_zombie(id))
      {
        new Float:fStandFor
        pev(id,pev_max_health,fStandFor)
        set_pev(id,pev_health,fStandFor*2+zb_SKILLHEALTH*zb_LevelNum[id][0])
        }else
	{        new Float:fStandFor
                     pev(id,pev_health,fStandFor)
                     set_pev(id,pev_health,fStandFor+zb_SKILLHEALTH*zb_LevelNum[id][0])
	}
            set_pev(id,pev_gravity,1.0-(float(SKILLGRAVITY)/1000)*zb_LevelNum[id][1])
        }  
         }
}

public client_damage(attacker,victim,damage,weapon,hitplace,ta)
{
    if(human_LevelNum[attacker][7]>0&&!zp_get_user_zombie(attacker))
    {
        new rand=random_num(0,100)
        if(rand==100)
        {
            Screen_Fade(attacker, 0.2, 0x0000, 150, 20, 20, 130)
            new Float:fHealth
            pev(victim,pev_health,fHealth)
            if(fHealth>human_LevelNum[attacker][7]*SKILLEFFECTHEALTH*damage)
                set_pev(victim,pev_health,fHealth-human_LevelNum[attacker][7]*SKILLEFFECTHEALTH*damage)
            else
            {
                user_silentkill(victim)
                MakeDeath(attacker,victim)
            }
            
            client_color(attacker,"/y技能 /r%s /y发动，对所击打僵尸造成 /g%.1f伤害/y",human_szLevelName[7],human_LevelNum[attacker][7]*SKILLEFFECTHEALTH*damage)
        }
    }
    if(human_LevelNum[attacker][8]&&!zp_get_user_zombie(attacker))
    {
        new rand=random_num(0,100)
        if(rand==100)
        {
            Screen_Fade(attacker, 0.2, 0x0000, 20, 150, 20, 130)
            new Float:fHealth
            pev(attacker,pev_health,fHealth)
            set_pev(attacker,pev_health,fHealth+damage*SKILLGAINHEALTH*human_LevelNum[attacker][8])
            client_color(attacker,"/y技能 /r%s /y发动，吸收击打僵尸的 /g%.1f伤害/y转化为/g自己的生命/y",human_szLevelName[8],damage*SKILLGAINHEALTH*human_LevelNum[attacker][8])
        }
    }
    if(EXEGAIN==1&&g_Level[attacker]<g_MaxLevel&&zp_get_user_zombie(victim)&&!zp_get_user_zombie(attacker))
    {
        add_level_exe(attacker,floatround(damage*5*(g_levelSave[attacker]?SAVEEXEGAIN:NOSAVEEXEGAIN)))
    }
}
public client_death(killer,victim,wpnindex,hitplace,TK)
{
    if(EXEGAIN==2&&g_Level[killer]<g_MaxLevel&&zp_get_user_zombie(victim)&&!zp_get_user_zombie(killer))
    {
        add_level_exe(killer,floatround(EXEGAIN2EXE*5*(g_levelSave[killer]?SAVEEXEGAIN:NOSAVEEXEGAIN)))
    }
}
public client_connect(id)
{
    client_cmd(id,"bind f5 ^"say /levelmenu^"")    
}
public client_putinserver(id)
{
    g_Level[id]=1
    g_LevelTotalExe[id]=1000
    show_levelhud(id)
    g_LevelExe[id]=read_score(id)
    if(g_LevelExe[id]==0) 
    {
        g_PD[id]=true
        g_taskid[id]=false
    }
    else g_levelSave[id]=true
    set_task(1.0,"show_levelhud",id,_,_,"b")
    new name[33]
    get_user_name(id,name,32)
    if(contain(name,"`")!=-1)
    {
        server_cmd("kick #%d ^"名字含有非法字符,已被系统踢出^"",get_user_userid(id))
        remove_task(id)
        g_LevelExe[id]=0
        g_LevelTotalExe[id]=1000
        g_Level[id]=1
        g_LevelPoint[id]=0
        for(new i=0;i<human_SKILLNUM;i++)
            human_LevelNum[id][i]=0
    }
    
}

public client_disconnect(id)
{
    if(g_levelSave[id])
        save_score(id)
    remove_task(id)
    g_LevelExe[id]=0
    g_LevelTotalExe[id]=1000
    g_Level[id]=1
    g_LevelPoint[id]=0
    g_taskid[id]=false
    for(new i=0;i<human_SKILLNUM;i++)
        human_LevelNum[id][i]=0
}
public client_PreThink(id)
{
    if(is_user_connected(id))
    {
        if(g_LevelExe[id]>=g_LevelTotalExe[id]&&g_Level[id]<g_MaxLevel)
        {
            g_LevelExe[id]-=g_LevelTotalExe[id]
            g_Level[id]+=1
            g_LevelTotalExe[id]=floatround(1000*g_Level[id]*1.57)
            g_LevelPoint[id]+=1
	   zb_LevelPoint[id]+=1
            if(is_user_alive(id))
	    if(!zp_get_user_zombie(id))
	    {PlayerMenu(id)
	    }
            else
	    {zbMenu(id)
	    }
        }
        if(g_LevelExe[id]<0)
        {
            g_LevelExe[id]+=floatround(1000*(g_Level[id]-1)*((g_Level[id]-1)==1?1.0:1.57))
            g_Level[id]-=1
            if(g_LevelPoint[id]>0)
                g_LevelPoint[id]-=1
            g_LevelTotalExe[id]=floatround(1000*g_Level[id]*(g_Level[id]==1?1.0:1.57))
        }
        if(g_LevelExe[id]>g_LevelTotalExe[id]&&g_Level[id]==g_MaxLevel)    
            g_LevelExe[id]=floatround(1000*g_MaxLevel*1.57)
        if(is_user_alive(id))
        {
          /*  if(g_jump[id]<human_LevelNum[id][10]&&(fm_get_user_button(id)&IN_JUMP)&&!(fm_get_user_oldbutton(id)&IN_JUMP)&&!zp_get_user_zombie(id))
            {
                fm_jump(id)
                g_jump[id]+=1
            }*/
	  if((fm_get_user_button(id)&IN_JUMP)&&!(fm_get_user_oldbutton(id)&IN_JUMP))
            {
              if(!zp_get_user_zombie(id))
	      {if(g_jump[id]<human_LevelNum[id][10])
	      {
                fm_jump(id)
                g_jump[id]+=1
	      }	      
	      }
	      else
	      {if(g_jump[id]<zb_LevelNum[id][5])
	      {
                fm_jump(id)
                g_jump[id]+=1
	      }
	      }
            }
            if(g_jump[id]!=0&&fm_get_entity_flags(id)&FL_ONGROUND) 
                g_jump[id]=0
            if(!zp_get_user_zombie(id))
                set_pev(id,pev_maxspeed,250.0+SKILLSPEED*human_LevelNum[id][3])
            if((fm_get_user_button(id)&IN_USE)&&!(fm_get_user_oldbutton(id)&IN_USE)&&!zp_get_user_zombie(id)&&human_LevelNum[id][11])
            {
                if(g_explosion[id])
                {
                    
                    new Float:angles[3]
                    new g_origin[3],Float:velocity[3]
                    get_user_origin(id,g_origin,1)
                    for(new i=0;i<=2;i++)
                        velocity[i]=float(g_origin[i])
                    if(!is_user_alive(id) || !is_user_connected(id))
                        return
                    new ent = create_entity("info_target")
                    entity_set_origin(ent,velocity);
                    entity_set_byte(ent,EV_BYTE_controller1,25);
                    entity_set_byte(ent,EV_BYTE_controller2,25);
                    entity_set_byte(ent,EV_BYTE_controller3,25);
                    entity_set_byte(ent,EV_BYTE_controller4,25)
                    entity_set_string(ent,EV_SZ_classname,"info_targetskill")
                    entity_set_model(ent,"models/w_usp.mdl")
                    entity_set_int(ent,EV_INT_solid, SOLID_TRIGGER)        //是否为实体
                    set_pev(ent, pev_movetype, MOVETYPE_FLYMISSILE)
                    get_user_origin(id,g_origin,3)
                    for(new i=0;i<=2;i++)
                        velocity[i]=float(g_origin[i])
                    velocity_by_aim(id, 1000, velocity)
                    set_pev(ent, pev_velocity, velocity)
                    vector_to_angle(velocity, angles)
                    set_pev(ent, pev_angles, angles) 
                    set_pev(ent, pev_v_angle, angles) 
                    set_pev(ent, pev_owner,id) 
                    
                    message_begin(MSG_BROADCAST, SVC_TEMPENTITY)
                    write_byte(TE_BEAMFOLLOW)
                    write_short(ent)        
                    write_short(tr_fl)         
                    write_byte(10)
                    write_byte(15)
                    write_byte(255)
                    write_byte(255)
                    write_byte(255)
                    write_byte(220)
                    message_end()
                    fm_set_rendering(ent,kRenderFxPulseFastWide,255,255,255,kRenderTransAlpha,0)
                    g_explosion[id]=false
                    set_task(60.0,"ResetExplosion",id+450)
                }
                else
                {
                    client_color(id,"/g%s/y正在缓冲,/g请等待一分钟后/y缓冲结束再使用冲击波.",human_szLevelName[11])
                }
            }
            if(SHOWTYPE==4)
            {
                if(g_guishow[id]&&!zp_get_user_zombie(id))
                {
                    new szMsg[128]
                    format(szMsg,127,"Level:%d/%d^nExe:%d/%d ^nPoint:%d",g_Level[id],g_MaxLevel,g_LevelExe[id],g_LevelTotalExe[id],g_LevelPoint[id])
                    message_begin(MSG_ONE_UNRELIABLE, get_user_msgid("TutorText"), _, id)
                    write_byte(id)
                    write_string(szMsg)
                    message_end()
                }
                else if(g_guishow[id])
                {
                    g_guishow[id]=false
                    remove_guishow(id)
                }
            }
        }
        else if(g_guishow[id])
        {
            g_guishow[id]=false
            remove_guishow(id)
        }
    }
}

public remove_guishow(id)
{
    message_begin(MSG_ONE_UNRELIABLE, get_user_msgid("TutorClose"), _, id)
    write_byte(id)
    message_end()
}

public zp_user_humanized_pre(id, survivor)
{
    g_guishow[id]=true
}

public zp_user_infected_pre(id, survivor)
{
    zb_guishow[id]=true
}
public ResetExplosion(pid)
{
    new id=pid-450
    g_explosion[id]=true
    if(!zp_get_user_zombie(id)&&is_user_alive(id))
    {
        client_color(id,"/g%s/y缓冲完毕,按/rE键/y使用冲击波.",human_szLevelName[11])
    }
}

public npc_remove(Ent,id)
{    
    if(!pev_valid(Ent))
        return HAM_IGNORED
    
    new class[32],ent_class[32]
    pev(Ent, pev_classname, class, 31)
    pev(id, pev_classname, ent_class, 31)
    if (!equal(class, "info_targetskill"))
        return HAM_IGNORED
    
    if (equal(ent_class,"info_targetskill")) 
        return HAM_IGNORED
    
    if (equal(ent_class, "weaponbox"))
        return HAM_IGNORED
    
    new own = pev(Ent,pev_owner)
    
    if(id == own) 
        return HAM_IGNORED
    
    ExplosionCreate(Ent+890)
    
    return HAM_IGNORED
}
public ExplosionRemove(Ent)
{
    if(pev_valid(Ent))
        set_pev(Ent,pev_flags, pev(Ent,pev_flags) | FL_KILLME) 
    remove_entity(Ent)
}
public ExplosionCreate(pEnt)
{
    
    new Ent=pEnt-890
    new Float:Org[3],Float:Org1[3]
    pev(Ent,pev_origin, Org)
    message_begin(MSG_BROADCAST,SVC_TEMPENTITY) 
    write_byte(TE_EXPLOSION)
    write_coord(floatround(Org[0]))
    write_coord(floatround(Org[1]))
    write_coord(floatround(Org[2]+70.0))
    write_short(sz_exposion)
    write_byte(20)
    write_byte(10)
    write_byte(TE_EXPLFLAG_NOSOUND)
    message_end()
    emit_sound(0,CHAN_AUTO,sz_soundplay,1.0, ATTN_NORM, 0, PITCH_NORM)
    for(new i=0;i<=3;i++)
    {
        fm_vel2d_over_aiming(Ent,90.0*i,180.0,Org1)
        message_begin(MSG_BROADCAST,SVC_TEMPENTITY) 
        write_byte(TE_EXPLOSION)
        write_coord(floatround(Org1[0]+Org[0]))
        write_coord(floatround(Org1[1]+Org[1]))
        write_coord(floatround(Org1[2]+Org[2]+70.0))
        write_short(sz_exposion)
        write_byte(20)
        write_byte(10)
        write_byte(TE_EXPLFLAG_NOSOUND)
        message_end()
        emit_sound(0,CHAN_AUTO,sz_soundplay,1.0, ATTN_NORM, 0, PITCH_NORM)
        
    }
    ExplosionDamageCreate(Ent)
}
public ExplosionDamageCreate(pid)
{
    new id=pid,Float:g_totaldamage
    for(new i=1;i<=32; i++)
    {
        if(pev_valid(i) <= 0)
            continue
                
        if(pev(i,pev_takedamage) == 0.0)
            continue
                
        new Float:dmg = SKILLEDAMAGE
        new Float:rad = SFROUND
        new dist=floatround(fm_entity_range(id,i))
        if(dist<=rad&&zp_get_user_zombie(i))
        {    
            new Float: damage = dmg - (dmg / rad) * float(dist)
            if(pev(i,pev_health)-damage<=0)
            {
                MakeDeath(pev(id,pev_owner),i)
            }
            fm_set_user_health(i,floatround(pev(i,pev_health)-damage))
            g_totaldamage+=damage
        }
    }
    client_color(pev(id,pev_owner),"/g%s/y对玩家造成的/g总伤害为%.1f",human_szLevelName[11],g_totaldamage)
    emit_sound(0,CHAN_AUTO,sz_soundplay,1.0, ATTN_NORM, 0, PITCH_NORM)
    ExplosionRemove(id)
}


public mainMenu(id)
{
    static opcion[256]
    formatex(opcion, charsmax(opcion),"\y升级菜单")    
    new iMenu=menu_create(opcion,"mainShow")    //执行菜单命令的
    new szTempid[18]
    formatex(opcion, charsmax(opcion),"\b人类技能")
    menu_additem(iMenu, opcion, szTempid,0)
    formatex(opcion, charsmax(opcion),"\b僵尸技能")
    menu_additem(iMenu, opcion, szTempid,0)
    formatex(opcion, charsmax(opcion),"\y购买%d经验(\r%d弹药袋\y)",BUYEXEGAIN,BUYEXEPACKS)
    menu_additem(iMenu, opcion, szTempid,0)
    formatex(opcion, charsmax(opcion),"\y购买等级(\r%d弹药袋\y)",BUYLEVELPACKS*g_Level[id])
    menu_additem(iMenu, opcion, szTempid,0)
    formatex(opcion, charsmax(opcion),"\y重置技能点\w(\r%d经验\w)",RESETPOINTEXE)
    menu_additem(iMenu, opcion, szTempid,0)
    menu_setprop(iMenu, MPROP_EXIT, MEXIT_ALL)
    formatex(opcion, charsmax(opcion),"\w退出")    //退出菜单的名字
    menu_setprop(iMenu, MPROP_EXITNAME, opcion)
    menu_setprop(iMenu, MPROP_NUMBER_COLOR, "\r")    //菜单前面颜色的数字
    menu_display(id, iMenu, 0)
    return PLUGIN_HANDLED
}

public mainShow(id, menu, item)
{
    if( item == MENU_EXIT )
    {
        menu_destroy(menu)
        return PLUGIN_HANDLED
    }
    new command[6], name[64], access, callback
    menu_item_getinfo(menu, item, access, command, sizeof command - 1, name, sizeof name - 1, callback)
    switch(item)
    {        
    	case 0:{
		PlayerMenu(id)
	}
         case 1:{
		zbMenu(id)
	}		
        case 2:{

                if(g_Level[id]!=g_MaxLevel)
                {
                   
                        if(zp_get_user_ammo_packs(id)>=BUYEXEPACKS)
                        {
                            add_level_exe(id,BUYEXEGAIN)
                            zp_set_user_ammo_packs(id,zp_get_user_ammo_packs(id)-BUYEXEPACKS)
                            client_color(id,"购买经验成功")
                        }
                        else{
                            client_color(id,"你的弹药袋不够购买经验")
                        }
                    
                }
                else
                {
                    client_color(id,"/y您的等级满啦,不能再购买了...")
                }
                
        
          
        }
        case 3:{

                if(g_Level[id]<g_MaxLevel)
                {
                 
                        if(zp_get_user_ammo_packs(id)>=BUYLEVELPACKS*g_Level[id])
                        {
                            g_LevelExe[id]=0
                            g_Level[id]+=1
                            g_LevelTotalExe[id]=floatround(1000*g_Level[id]*1.57)
                            g_LevelPoint[id]+=1
			zb_LevelPoint[id]+=1   
                            if(is_user_alive(id))
                                mainMenu(id)
                            zp_set_user_ammo_packs(id,zp_get_user_ammo_packs(id)-BUYLEVELPACKS*g_Level[id])
                            client_color(id,"购买等级成功")
                        }
                        else{
                            client_color(id,"你的弹药袋不够购买等级")
                        }
                    
                }
                else
                {
                    client_color(id,"/y您的等级满啦,不能再购买了...")
                }
          

        }
        case 4:{

                new g_total=g_LevelExe[id]
                if(g_Level[id]>1)
                {
                    for(new i=2;i<=g_Level[id]-1;i++)
                    {
                        g_total+=floatround(1000*i*1.57)
                    }
                    g_total+=1000
                    if(g_total>=RESETPOINTEXE)
                    {
                        g_LevelExe[id]-=RESETPOINTEXE
                        for(new i=0;i<=human_SKILLNUM-1;i++)
                        {
                            g_LevelPoint[id]+=human_LevelNum[id][i]*human_LevelSkillPonit[i]
                            zb_LevelPoint[id]+=human_LevelNum[id][i]*zb_LevelSkillPonit[i]
                            human_LevelNum[id][i]=0
			 zb_LevelNum[id][i]=0

                        }
                        if(is_user_alive(id))
                        {if(!zp_get_user_zombie(id))
                            {set_pev(id,pev_gravity,1.0)
                            if(pev(id,pev_health)>100)
                                set_pev(id,pev_health,100.0)
                            if(pev(id,pev_armorvalue)>0)
                                set_pev(id,pev_armorvalue,0.0)
				}
	              else
		      {
                            {set_pev(id,pev_gravity,1.0)
                            if(pev(id,pev_health)>zp_get_zombie_maxhealth(id))
                                set_pev(id,pev_health,zp_get_zombie_maxhealth(id))
                            if(pev(id,pev_armorvalue)>0)
                                set_pev(id,pev_armorvalue,0.0)
				}
		      }
                        }
                        client_color(id,"技能点已清零")
                    }
                }
                else{
                    client_color(id,"你的等级不够清零技能点")
                }
            

        }    
    
    }
    
    menu_destroy(menu)
    return PLUGIN_HANDLED
}
public PlayerMenu(id)
{
    static opcion[256]
    formatex(opcion, charsmax(opcion),"\y人类升级菜单\r||\y技能点数:\r%d\y点",g_LevelPoint[id])    
    new iMenu=menu_create(opcion,"Show")    //执行菜单命令的
    new szTempid[18]
    for(new i=0;i<human_SKILLNUM;i++)
    {
        if(human_LevelNum[id][i]<human_LevelSkillMAX[i])
            formatex(opcion, charsmax(opcion),"\w%s \y需%d点\r|\y现%d级\r|\y共%d级", human_szLevelName[i],human_LevelSkillPonit[i],human_LevelNum[id][i],human_LevelSkillMAX[i])
        else formatex(opcion, charsmax(opcion),"\w%s \r等级:Max", human_szLevelName[i])
        menu_additem(iMenu, opcion, szTempid,0)
    }
    //formatex(opcion, charsmax(opcion),"\y购买%d经验(\r%d弹药袋\y)",BUYEXEGAIN,BUYEXEPACKS)
    //menu_additem(iMenu, opcion, szTempid,0)
    //formatex(opcion, charsmax(opcion),"\y购买等级(\r%d弹药袋\y)",BUYLEVELPACKS*g_Level[id])
    //menu_additem(iMenu, opcion, szTempid,0)
    //formatex(opcion, charsmax(opcion),"\y重置技能点\w(\r%d经验\w)^n(输入resetlevel重置所有等级设置)",RESETPOINTEXE)
    //menu_additem(iMenu, opcion, szTempid,0)
    menu_setprop(iMenu, MPROP_EXIT, MEXIT_ALL)
    formatex(opcion, charsmax(opcion),"\w返回")    //返回菜单的名字
    menu_setprop(iMenu, MPROP_BACKNAME, opcion)
    formatex(opcion, charsmax(opcion),"\w下一页")    //下一页菜单的名字
    menu_setprop(iMenu, MPROP_NEXTNAME, opcion)
    formatex(opcion, charsmax(opcion),"\w退出")    //退出菜单的名字
    menu_setprop(iMenu, MPROP_EXITNAME, opcion)
    menu_setprop(iMenu, MPROP_NUMBER_COLOR, "\r")    //菜单前面颜色的数字
    menu_display(id, iMenu, 0)
    return PLUGIN_HANDLED
}

public zbMenu(id)
{
    static opcion[256]
    formatex(opcion, charsmax(opcion),"\y僵尸升级菜单\r||\y技能点数:\r%d\y点",zb_LevelPoint[id])    
    new iMenu=menu_create(opcion,"zbShow")    //执行菜单命令的
    new szTempid[18]
    for(new i=0;i<zb_SKILLNUM;i++)
    {
        if(zb_LevelNum[id][i]<zb_LevelSkillMAX[i])
            formatex(opcion, charsmax(opcion),"\w%s \y需%d点\r|\y现%d级\r|\y共%d级", zb_szLevelName[i],zb_LevelSkillPonit[i],zb_LevelNum[id][i],zb_LevelSkillMAX[i])
        else formatex(opcion, charsmax(opcion),"\w%s \r等级:Max", zb_szLevelName[i])
        menu_additem(iMenu, opcion, szTempid,0)
    }
    //formatex(opcion, charsmax(opcion),"\y购买%d经验(\r%d弹药袋\y)",BUYEXEGAIN,BUYEXEPACKS)
    //menu_additem(iMenu, opcion, szTempid,0)
    //formatex(opcion, charsmax(opcion),"\y购买等级(\r%d弹药袋\y)",BUYLEVELPACKS*g_Level[id])
    //menu_additem(iMenu, opcion, szTempid,0)
    //formatex(opcion, charsmax(opcion),"\y重置技能点\w(\r%d经验\w)^n(输入resetlevel重置所有等级设置)",RESETPOINTEXE)
    //menu_additem(iMenu, opcion, szTempid,0)
    menu_setprop(iMenu, MPROP_EXIT, MEXIT_ALL)
    formatex(opcion, charsmax(opcion),"\w返回")    //返回菜单的名字
    menu_setprop(iMenu, MPROP_BACKNAME, opcion)
    formatex(opcion, charsmax(opcion),"\w下一页")    //下一页菜单的名字
    menu_setprop(iMenu, MPROP_NEXTNAME, opcion)
    formatex(opcion, charsmax(opcion),"\w退出")    //退出菜单的名字
    menu_setprop(iMenu, MPROP_EXITNAME, opcion)
    menu_setprop(iMenu, MPROP_NUMBER_COLOR, "\r")    //菜单前面颜色的数字
    menu_display(id, iMenu, 0)
    return PLUGIN_HANDLED
}
public Show(id, menu, item)
{
    if( item == MENU_EXIT )
    {
        menu_destroy(menu)
        return PLUGIN_HANDLED
    }
    new command[6], name[64], access, callback
    menu_item_getinfo(menu, item, access, command, sizeof command - 1, name, sizeof name - 1, callback)
    switch(item)
    {
        case 0..(human_SKILLNUM-1):{


                    if(human_LevelNum[id][item]<human_LevelSkillMAX[item])
                    {
                        if(g_LevelPoint[id]>=human_LevelSkillPonit[item])
                        {
                            human_LevelNum[id][item]+=1
                            new Float:fStandFor
                            switch(item+1)
                            {
                                case 1:
                                {
			    if(!zp_get_user_zombie(id))
                                   {
                                    pev(id,pev_health,fStandFor)
                                    set_pev(id,pev_health,fStandFor+SKILLHEALTH)
                                  }  
                                }
                                case 2:
                                {
                                   if(!zp_get_user_zombie(id))
                                   {
				pev(id,pev_armorvalue,fStandFor)
                                    set_pev(id,pev_armorvalue,fStandFor+SKILLARMOR)
                                }
				}
                                case 3:
                                {    
				if(!zp_get_user_zombie(id))
                                   {
                                    set_pev(id,pev_gravity,1.0-(float(SKILLGRAVITY)/1000)*human_LevelNum[id][2])
                                }
				}
                                case 4:
                                {
    				if(!zp_get_user_zombie(id))
                                   {
				pev(id,pev_maxspeed,fStandFor)
                                    set_pev(id,pev_maxspeed,fStandFor+SKILLSPEED)
				  }  
                                }
                                case 5:
                                { if(!zp_get_user_zombie(id))
                                   {
                                    fm_set_rendering(id,kRenderFxGlowShell,0,0,0,kRenderTransAlpha, 255-SKILLRENDER*human_LevelNum[id][4])
                                }
				 } 
                                case 10:
                                {
                                    set_task(SKILLHEGRENADE,"givehg",id,_,_,"b")
                                }
                                case 12:
                                {
                                    g_explosion[id]=true
                                }
                                                }
                            g_LevelPoint[id]-=human_LevelSkillPonit[item]  
                            if(human_LevelNum[id][item]<human_LevelSkillMAX[item])
                                client_color(id,"/g%s/y技能成功升级,花费/g技能点数 /r%d/y ,现在的等级为 /g%d/y",human_szLevelName[item],human_LevelSkillPonit[item],human_LevelNum[id][item])
                            else if(human_LevelNum[id][item]==human_LevelSkillMAX[item])
                                client_color(id,"/g%s/y技能成功升级,花费/g技能点数 /r%d/y ,现在的/g技能等级/y已升为/g满级",human_szLevelName[item],human_LevelSkillPonit[item],human_LevelNum[id][item])
                            if(g_LevelPoint[id]>0)
                                PlayerMenu(id)
                        }
                        else client_color(id,"/y你的技能点数不足,无法升级 /g%s/y",human_szLevelName[item])
                    }
                    else {
                    client_color(id,"/y技能等级已满,无法继续升级 /g%s/y",human_szLevelName[item])
                    PlayerMenu(id)
                    }
                


        }        

  
    
    }
    
    menu_destroy(menu)
    return PLUGIN_HANDLED
}

public zbShow(id, menu, item)
{
    if( item == MENU_EXIT )
    {
        menu_destroy(menu)
        return PLUGIN_HANDLED
    }
    new command[6], name[64], access, callback
    menu_item_getinfo(menu, item, access, command, sizeof command - 1, name, sizeof name - 1, callback)
    switch(item)
    {
        case 0..(zb_SKILLNUM-1):{


                    if(zb_LevelNum[id][item]<zb_LevelSkillMAX[item])
                    {
                        if(zb_LevelPoint[id]>=zb_LevelSkillPonit[item])
                        {
                            zb_LevelNum[id][item]+=1
                            new Float:fStandFor
                            switch(item+1)
                            {
                                case 1:
                                {if(zp_get_user_zombie(id))
                                   {
                                    pev(id,pev_health,fStandFor)
                                    set_pev(id,pev_health,fStandFor+zb_SKILLHEALTH)
                                    }
                                }
                              
                                case 2:
                                {   if(zp_get_user_zombie(id))
                                   {
				pev(id,pev_gravity,fStandFor)
                                    set_pev(id,pev_gravity,fStandFor-(float(SKILLGRAVITY)/1000))
				    }
                                }
                                case 3:
                                {if(zp_get_user_zombie(id))
                                   {
                                    pev(id,pev_maxspeed,fStandFor)
                                    set_pev(id,pev_maxspeed,fStandFor+SKILLSPEED)
                                }
				}
                                case 4:
                                {if(zp_get_user_zombie(id))
                                   {
                                    fm_set_rendering(id,kRenderFxGlowShell,0,0,0,kRenderTransAlpha, 255-SKILLRENDER*zb_LevelNum[id][3])
				    }
                                }
                                                        
                                case 10:
                                {
                                    g_explosion[id]=true
                                }
                                                }
                            zb_LevelPoint[id]-=zb_LevelSkillPonit[item]
                            if(zb_LevelNum[id][item]<zb_LevelSkillMAX[item])
                                client_color(id,"/g%s/y技能成功升级,花费/g技能点数 /r%d/y ,现在的等级为 /g%d/y",zb_szLevelName[item],zb_LevelSkillPonit[item],zb_LevelNum[id][item])
                            else if(zb_LevelNum[id][item]==zb_LevelSkillMAX[item])
                                client_color(id,"/g%s/y技能成功升级,花费/g技能点数 /r%d/y ,现在的/g技能等级/y已升为/g满级",zb_szLevelName[item],zb_LevelSkillPonit[item],zb_LevelNum[id][item])
                            if(g_LevelPoint[id]>0)
                                zbMenu(id)
                        }
                        else client_color(id,"/y你的技能点数不足,无法升级 /g%s/y",zb_szLevelName[item])
                    }
                    else {
                    client_color(id,"/y技能等级已满,无法继续升级 /g%s/y",zb_szLevelName[item])
                    zbMenu(id)
                    }
                
                
            }

                
        
    }
    
    menu_destroy(menu)
    return PLUGIN_HANDLED
}

public add_level_exe(index,addexe)
{
    g_LevelExe[index]+=addexe
}

public givehg(id)
{
    if(!zp_get_user_zombie(id)&&is_user_alive(id)&&human_LevelNum[id][9])
    {
        fm_give_item(id,"weapon_hegrenade")
    }
}

public show_levelhud(id)
{
    if(is_user_alive(id)&&!zp_get_user_zombie(id)&&SHOWTYPE==1)
    {
        set_hudmessage(127, 255, 42, -1.0, 0.05, 0, 6.0, 1.1,_,_,HUDCHANNEL)        
        show_hudmessage(id, "等级:%d/%d^n经验:%d/%d^n技能点数:%d点",g_Level[id],g_MaxLevel,g_LevelExe[id],g_LevelTotalExe[id],g_LevelPoint[id])
    }
    else if(is_user_alive(id)&&!zp_get_user_zombie(id)&&SHOWTYPE==2)
    {
        client_print(id,print_center, "等级:%d/%d  经验:%d/%d  技能点数:%d点",g_Level[id],g_MaxLevel,g_LevelExe[id],g_LevelTotalExe[id],g_LevelPoint[id])
    }
    else if(is_user_alive(id)&&!zp_get_user_zombie(id)&&SHOWTYPE==3)
    {
        new szMsg[128]
        format(szMsg,127,"等级:%d/%d  经验:%d/%d  技能点数:%d点",g_Level[id],g_MaxLevel,g_LevelExe[id],g_LevelTotalExe[id],g_LevelPoint[id])
        message_begin(MSG_ONE_UNRELIABLE, get_user_msgid("StatusText"), _, id)
        write_byte(0)
        write_string(szMsg)
        message_end()
    }
    else if(!is_user_alive(id))
    {
        new pid=pev(id,pev_iuser2)
        if( !pid ) return
        if(!zp_get_user_zombie(pid))
        {
            new name[64]
            get_user_name(pid,name,63)
            new Explosion[1024]
            for(new i=0;i<human_SKILLNUM;i++)
            {
                new szhuman_SKILLNUM[16]
                if(human_LevelNum[pid][i]!=human_LevelSkillMAX[i]) format(szhuman_SKILLNUM,charsmax(szhuman_SKILLNUM),"%d级",human_LevelNum[pid][i])
                else format(szhuman_SKILLNUM,charsmax(szhuman_SKILLNUM),"MAX")
                
                if(i==0) format(Explosion,charsmax(Explosion),"玩家:%s  等级:%d^n^n%s: %s^n",name,g_Level[pid],human_szLevelName[i],szhuman_SKILLNUM)
                else format(Explosion,charsmax(Explosion),"%s%s: %s^n",Explosion,human_szLevelName[i],szhuman_SKILLNUM)
            }
            set_hudmessage(127, 212, 255, 0.73, 0.12, 1, 6.0,  1.1,_,_,HUDCHANNEL)
            show_hudmessage(id, "%s",Explosion)
        }
    }        
}

public savemenu(pid)
{
    new id=pid-320
    if(g_PD[id])
    {
        g_taskid[id]=true
        static opcion[64]
        formatex(opcion, charsmax(opcion),"保存经验选择")    
        new iMenu=menu_create(opcion,"sChoose")    //执行菜单命令的
        new szTempid[10]
        formatex(opcion, charsmax(opcion),"保存经验(\r升级慢\w)")
        menu_additem(iMenu, opcion, szTempid,0)
        formatex(opcion, charsmax(opcion),"不保存经验(\r升级快\w)")
        menu_additem(iMenu, opcion, szTempid,0)
        formatex(opcion, charsmax(opcion),"\w返回")    //返回菜单的名字
        menu_setprop(iMenu, MPROP_BACKNAME, opcion)
        formatex(opcion, charsmax(opcion),"\w下一页")    //下一页菜单的名字
        menu_setprop(iMenu, MPROP_NEXTNAME, opcion)
        formatex(opcion, charsmax(opcion),"\w退出")    //退出菜单的名字
        menu_setprop(iMenu, MPROP_EXITNAME, opcion)
        menu_setprop(iMenu, MPROP_NUMBER_COLOR, "\y")    //菜单前面颜色的数字
        menu_display(id, iMenu, 0)
    }
    return PLUGIN_HANDLED
}

public sChoose(id, menu, item)
{
    if( item == MENU_EXIT )
    {
        menu_destroy(menu)
        return PLUGIN_HANDLED
    }
    new command[6], name[64], access, callback
    menu_item_getinfo(menu, item, access, command, sizeof command - 1, name, sizeof name - 1, callback)
    switch(item)
    {
        case 0:{
        g_levelSave[id]=true
        client_color(id,"/y系统将会自动帮你/g保存经验，升级慢")
        g_PD[id]=false
    }
        case 1:{
        g_levelSave[id]=false
        client_color(id,"/y系统将/g不会/y自动帮你/g保存经验,升级较快")
        g_PD[id]=false
    }
    
    }
    menu_destroy(menu)
    return PLUGIN_HANDLED
}


public save_score(id)
{
    if(g_Level[id]>1)
    {
        for(new i=2;i<=g_Level[id]-1;i++)
        {
            g_LevelExe[id]+=floatround(1000*i*1.57)
        }
        g_LevelExe[id]+=1000
    }
    if(is_user_bot(id)) return PLUGIN_HANDLED
    new line = 0, textline[1024], len
    new line_name[64] 
    new value[33]
    new ident[33]
    get_user_name(id, ident, charsmax(ident))
    while ((line = read_file(file, line, textline, 1023, len)))
    {
        if (len == 0 || equal(textline, ";", 1))
            continue

        parse (textline, line_name, 63)
        strtok(textline,line_name,charsmax(line_name),value,charsmax(value),'`')

        if(equal(line_name, ident)){
                len = format(textline, 1023, "%s", ident)
                len += format(textline[len], 1023 - len, "`%d",g_LevelExe[id])
                write_file(file, textline, line -1)
                return PLUGIN_HANDLED
        }
    }
    len = format(textline, 255, "%s", ident)
    len += format(textline[len], 255 - len, "`%d",g_LevelExe[id])
    write_file(file, textline)
    return PLUGIN_HANDLED
}

public read_score(id)
{
    new line = 0, textline[1024], len
    new line_name[64] 
    new value[33]
    new ident[33]
    get_user_name(id, ident, charsmax(ident))

    if(!file_exists(file))
    {
        write_file(file, "")
    }

    while((line = read_file(file, line, textline, 1023, len)))
    {
        if (len == 0 || equal(textline, ";", 1))
            continue

        parse (textline, line_name, 63)
        strtok(textline,line_name,charsmax(line_name),value,charsmax(value),'`')
        if(equal(line_name, ident)){
            return str_to_num(value)
        }
    }
    return 0
}




stock client_color(id, const input[], any:...)
{
    static iPlayersNum[32], iCount; iCount = 1
    static szMsg[191]
    
    vformat(szMsg, charsmax(szMsg), input, 3)
    
    replace_all(szMsg, 190, "/g", "^4") // 绿色
    replace_all(szMsg, 190, "/y", "^1") // 橙色
    replace_all(szMsg, 190, "/r", "^3") // 队伍色
    replace_all(szMsg, 190, "/w", "^0") // 黄色
    
    if(id) iPlayersNum[0] = id
    else get_players(iPlayersNum, iCount, "ch")
    
    for (new i = 0; i < iCount; i++)
    {
        if (is_user_connected(iPlayersNum[i]))
        {
            message_begin(MSG_ONE_UNRELIABLE, get_user_msgid("SayText"), _, iPlayersNum[i])
            write_byte(iPlayersNum[i])
            write_string(szMsg)
            message_end()
        }
    }
}

stock fm_set_user_money(iPlayer, money)
{
    set_pdata_int(iPlayer, 115, money, 5)
    message_begin(MSG_ONE, get_user_msgid("Money"), {0,0,0}, iPlayer)
    write_long(money)
    write_byte(1)
    message_end()
}

stock fm_get_user_money(iPlayer)
{
    return get_pdata_int(iPlayer,115)
}


public fw_BotForwardRegister_Post(iPlayer)
{
    if(!is_user_bot(iPlayer))
        return
    
    unregister_forward(FM_PlayerPostThink, g_fwBotForwardRegister, 1)
    RegisterHamFromEntity(Ham_TakeDamage, iPlayer, "HAM_TakeDamage")
}

public HAM_TakeDamage(victim, inflictor, attacker, Float:damage, damagetype)
{
    if (!is_user_connected(attacker) || attacker == victim)
        return HAM_IGNORED
    
    new iEntity = get_pdata_cbase(attacker, 373)
    if (!inflictor || !pev_valid(iEntity))
        return HAM_IGNORED
    
    SetHamParamFloat(4, damage * (human_LevelNum[attacker][5]?(1.0+SKILLATTACK*human_LevelNum[attacker][5]):1.0))
    return HAM_IGNORED
}


public fw_WeapPriAttack(weapon)
{
    static owner
    owner=pev(weapon,pev_owner)
    static Float:multiplier
    multiplier=1-human_LevelNum[owner][6]*0.15
    if(multiplier<0.0)
        return HAM_IGNORED
    new Float:punchangle[3]
    pev(owner,pev_punchangle,punchangle)
    xs_vec_mul_scalar(punchangle,multiplier,punchangle)
    set_pev(owner,pev_punchangle,punchangle)
    return HAM_IGNORED
}




stock fm_jump(id)
{
    new Float:velocity[3] 
    pev(id,pev_velocity,velocity)
    velocity[2] = random_float(265.0,285.0)
    set_pev(id,pev_velocity,velocity)
}


stock Screen_Fade(id, Float:time, fade_type = 0x0000, red, green, blue, alpha)
{
    // 添加影响
    message_begin(MSG_ONE_UNRELIABLE,get_user_msgid("ScreenFade"), _, id)
    write_short((1<<12)*1) 
    write_short(floatround((1<<12)*time))         // 保持时间
    write_short(fade_type)             // 伪造类型 [FADE_IN(0x0000)/FADE_OUT(0x0001)/FADE_OUT(0x0002)/FADE_STAYOUT(0x0004)]
    write_byte(red)     // 红色
    write_byte(green)     // 绿色
    write_byte(blue)    // 蓝色
    write_byte(alpha)     // 亮度
    message_end()
}
stock MakeDeath(attack, victim)
{
    if(!(0<attack<33) || !(0<victim<33)) return

    
    set_pdata_int(victim, 444, get_pdata_int(victim, 444, 5) + 1, 5)

        set_msg_block(get_user_msgid("DeathMsg"), BLOCK_ONCE)
        set_msg_block(get_user_msgid("ScoreInfo"), BLOCK_ONCE)
    
    message_begin(MSG_ALL, get_user_msgid("DeathMsg"))
    write_byte(attack)
    write_byte(victim)
    write_byte(0)
    write_string("Skill")
    message_end()
    
    message_begin(MSG_ALL, get_user_msgid("ScoreInfo"))
    write_byte(victim)
    write_short(pev(victim, pev_frags))
    write_short(get_user_deaths(victim))
    write_short(0)
    write_short(get_pdata_int(victim, 114, 5))
    message_end()
    
    message_begin(MSG_ALL, get_user_msgid("ScoreInfo"))
    write_byte(attack)
    write_short((get_pdata_int(attack, 114, 5) != get_pdata_int(victim, 114, 5))?(pev(attack, pev_frags)+1):(pev(attack, pev_frags)-1))
    write_short(get_user_deaths(attack))
    write_short(0)
    write_short(get_user_team(attack))
    message_end()
}
stock fm_set_rendering(entity, fx = kRenderFxNone, r = 255, g = 255, b = 255, render = kRenderNormal, amount = 16) {
    new Float:RenderColor[3];
    RenderColor[0] = float(r);
    RenderColor[1] = float(g);
    RenderColor[2] = float(b);

    set_pev(entity, pev_renderfx, fx);
    set_pev(entity, pev_rendercolor, RenderColor);
    set_pev(entity, pev_rendermode, render);
    set_pev(entity, pev_renderamt, float(amount));

    return 1;
}

stock Float:fm_entity_range(ent1, ent2) {
    new Float:origin1[3], Float:origin2[3];
    pev(ent1, pev_origin, origin1);
    pev(ent2, pev_origin, origin2);

    return get_distance_f(origin1, origin2);
}
stock fm_give_item(iPlayer, const wEntity[])
{
    new iEntity = engfunc(EngFunc_CreateNamedEntity, engfunc(EngFunc_AllocString,     wEntity))
    new Float:origin[3]
    pev(iPlayer, pev_origin, origin)
    set_pev(iEntity, pev_origin, origin)
    set_pev(iEntity, pev_spawnflags, pev(iEntity, pev_spawnflags) | SF_NORESPAWN)
    dllfunc(DLLFunc_Spawn, iEntity)
    new save = pev(iEntity, pev_solid)
    dllfunc(DLLFunc_Touch, iEntity, iPlayer)
    if(pev(iEntity, pev_solid) != save)
        return iEntity
    engfunc(EngFunc_RemoveEntity, iEntity)
    return -1
}

stock fm_set_user_health(index, health)
{
    health > 0 ? set_pev(index, pev_health, float(health)) : dllfunc(DLLFunc_ClientKill, index)
    return 1
}
stock fm_vel2d_over_aiming(index,Float:Rdegree,Float:sthenth,Float:xyz[3],Float:z_value=0.0)
{
    new Float:fporigin[3],Float:faorigin[3]
    pev(index,pev_origin,fporigin)
    new Float:start[3], Float:view_ofs[3]
    pev(index, pev_origin, start)
    pev(index, pev_view_ofs, view_ofs)
    xs_vec_add(start, view_ofs, start)
    new Float:dest[3]
    pev(index, pev_v_angle, dest)
    engfunc(EngFunc_MakeVectors, dest)
    global_get(glb_v_forward, dest)
    xs_vec_mul_scalar(dest, 9999.0, dest)
    xs_vec_add(start, dest, dest)
    engfunc(EngFunc_TraceLine, start, dest, 0, index, 0)
    get_tr2(0, TR_vecEndPos,faorigin)
    new Float:Angles[3]
    pev(index,pev_angles,Angles)
    Angles[1]=(Angles[1]>0)?Angles[1]:(180.0+(180.0-floatabs(Angles[1])))
    new Float:fvalue=3.1415926535898/180.0*(Rdegree+Angles[1])
    xyz[0]=(floatcos(fvalue))*sthenth
    xyz[1]=(floatsin(fvalue))*sthenth
    if(z_value==-1.0)
        z_value=(faorigin[2]-fporigin[2])/xs_sqrt(floatpower(faorigin[2]-fporigin[2],2.0))*sthenth
    xyz[2]=z_value
}
