/* 本插件由 AMXX-Studio 中文版自动生成*/
/* UTF-8 func by www.DT-Club.net */
/*
*	该技能添加系统游戏金钱版于3月14日完成
*	添加技能，不用写技能提示类的文字，直接添加内容即可
*	游戏中$是该升级菜单的升级菜单金钱,游戏中输入/shopmenu打开技能菜单
*	4月13日修改V1.2beta：优化了数集增加还有介绍
*	7月12号V1.3beta：进行大部分优化和全面修改，增加插件外添加技能的功能
*	7月14号V1.31beta：严重BUG修复，修复只显示一个技能名称的BUG
*	7月15号V1.4正式版：修复inc的部分代码无法使用的bug和开局自动执行技能1的BUG
*	V1.4：增加获取 技能点数,队伍,是否活着才可以升级 的inc代码
*	V1.41：添加限制技能升级次数及其限制技能升级时间时间等类型的inc代码
*	V1.42：添加技能升级的forward,即是client_shop(id,itemid)
*	注意：Forward内须执行skill_reset_item_hasbuy(id)
*	V1.43：由测试得已知严重BUG。修复活着与死亡显示信息无法正常显示而且有时候报错的BUG。
*	V2.0：技能升级权限及其不用钱和验证直接给予指定玩家技能的inc代码添加
*	V2.1：添加返回指定技能升级所需的权限，添加每个inc函数的具体用法的例子
*	及其更新inc中的介绍内容，全面让编写者了解此INC代码的用法
*	V2.2：限制开局后多少时间才能开启升级菜单
*	V2.3：技能编号名字的获取及其打开升级菜单无技能的提示
*	V2.31：修复关于开局的时间限制的一些BUG
*	
*	改版自商场模版V2.31正式版本,Halo-QQ351642983
*	V1.2:添加重置技能点数的选项,头文件中增加获取总技能数的语句,修复少数的BUG,重置技能的Forward的添加
*	v1.21修复观察者BUG
*	v1.22对少数测试所得bug进行修复,技能注册在非预缓存区域等级获取错误的修复,显示菜单的判断更加人性化
*/

#include <amxmodx> 
#include <fakemeta>
#include <zombieplague>


#define PLUGIN_NAME	"技能添加系统"
#define PLUGIN_VERSION	"V1.22"
#define PLUGIN_AUTHOR	"Halo-QQ351642983"


//所能支持的最多的技能个数
#define ITEMNUM 64

//按键的绑定(不要绑定的话删除下面一行即可)
#define ANJIAN "l"


new bool:g_showtrue[33];

//判断需要显示什么东西的变量初始化
new ItemSF[33]=0,IteamNums=0,ApackPD[33]=0,ITime[ITEMNUM][33],Limit[ITEMNUM][33]
new bool:g_ShopOpen=true


//技能变量初始化
new g_levelexe[33],g_level[33],g_skilllevel[33][ITEMNUM],g_Maxlevel,g_leveltotalexe[33],g_levelpoint[33]

//CVAR的储存值
new g_sfOpenTime,g_sfHudChannel,g_sfShowMode,g_sfBasicNum,g_sfBasicExe,g_sfEasyGainexe,g_sfzExeGain,g_sfrMode,g_sfrExe,g_sfrPacks

//Forward的储存id值
new g_ForwardID,g_ForwardID1,g_ForwardID2,g_ForwardID3,g_ForwardID4,g_ForwardID5,g_ForwardID6,g_ForwardID7


//定义死亡是否升级技能时显示的状态
new const AliveShow[2][]=
{
	" 死亡 ",
	" 活着 "
};


new MenuItem[ITEMNUM][ITEMNUM]

new ItemTeams[ITEMNUM]

new ItemCost[ITEMNUM]

new ItemIfaLive[ITEMNUM]

new Float:TimeLimit[ITEMNUM]

new TimesLimit[ITEMNUM]

new ItemLevel[ITEMNUM]

new ItemMaxLevel[ITEMNUM]

new ItemRespawn[ITEMNUM]

new ItemRestart[ITEMNUM]

new ItemBuy[ITEMNUM]

new ItemOpen[ITEMNUM]

new ItemSkillDamage[ITEMNUM]

new Array:AritemName

new Arid[33]




public plugin_precache()
{
	AritemName=ArrayCreate(ITEMNUM,1)
}

public plugin_init()
{
	register_plugin(PLUGIN_NAME, PLUGIN_VERSION, PLUGIN_AUTHOR)
	register_event("HLTV","resetlimit","a","1=0","2=0")
	register_event("ResetHUD","Respawn","b")
	register_logevent("resetRound",2,"1=Round_Start")
	register_clcmd("say /skillmenu","IMenuShow")
	
	g_ForwardID=CreateMultiForward("skill_selected", ET_CONTINUE,FP_CELL,FP_CELL)
	g_ForwardID1=CreateMultiForward("skill_damage_selected", ET_CONTINUE,FP_CELL,FP_CELL,FP_CELL,FP_CELL,FP_CELL,FP_CELL,FP_CELL)
	g_ForwardID3=CreateMultiForward("skill_restart_selected_pre",ET_IGNORE,FP_CELL,FP_CELL)
	g_ForwardID4=CreateMultiForward("skill_open_selected", ET_IGNORE,FP_CELL,FP_CELL)
	g_ForwardID5=CreateMultiForward("skill_respawn_selected", ET_IGNORE,FP_CELL,FP_CELL)
	g_ForwardID6=CreateMultiForward("skill_restart_selected_post", ET_IGNORE,FP_CELL,FP_CELL)
	g_ForwardID2=CreateMultiForward("skill_levelup", ET_CONTINUE,FP_CELL)
	g_ForwardID7=CreateMultiForward("skill_levelreset", ET_CONTINUE,FP_CELL)
	
	g_sfOpenTime=register_cvar("skill_start_open","0.0")
	g_sfHudChannel=register_cvar("skill_hud_channel","3")
	g_sfShowMode=register_cvar("skill_show_mode","3")
	g_sfBasicNum=register_cvar("skill_basic_num","1.57")
	g_sfBasicExe=register_cvar("skill_basic_exe","1000")
	
	//如果有装上保存经验这项建议设置为0.0
	g_sfEasyGainexe=register_cvar("skill_esaygain_exe","1.0")
	
	
	g_sfzExeGain=register_cvar("skill_infector_exe","1000")
	g_sfrMode=register_cvar("skill_reset_mode","1")
	g_sfrExe=register_cvar("skill_reset_exe","2000")
	g_sfrPacks=register_cvar("skill_reset_packs","20")
	set_task(0.5,"getmaxlevel")
	
	
}
public getmaxlevel()
{
	g_Maxlevel=0
	for(new i=0;i<=IteamNums-1;i++)
		g_Maxlevel+=ItemMaxLevel[i]*ItemCost[i]
	g_Maxlevel++
}
public client_putinserver(id)
{
	set_task(1.0,"InfoShow",id+1000,_,_,"b")
}
public client_connect(id)
{
	ApackPD[id]=0
	ItemSF[id]=0
	get_all(Limit,ITEMNUM-1,0,id)
	get_all(ITime,ITEMNUM-1,0,id)
	for(new i=0;i<=IteamNums-1;i++)
		g_skilllevel[id][i]=0
	remove_task(id)
	remove_task(id+1000)
	
	g_levelexe[id]=0
	g_leveltotalexe[id]=get_pcvar_num(g_sfBasicExe)
	g_level[id]=1
	g_levelpoint[id]=0
	#if defined ANJIAN
		client_cmd(id,"bind %s ^"say /skillmenu",ANJIAN)
	#endif
}

public InfoShow(pid)
{
	new id=pid-1000
	if(is_user_connected(id))
	{
		if(is_user_alive(id))
		{
			if(get_pcvar_num(g_sfShowMode)==1)
			{
				set_hudmessage(127, 255, 42, -1.0, 0.05, 0, 6.0, 1.1,_,_,get_pcvar_num(g_sfHudChannel))		
				show_hudmessage(id, "等级:%d/%d^n经验:%d/%d^n技能点数:%d点",g_level[id],g_Maxlevel,g_levelexe[id],g_leveltotalexe[id],g_levelpoint[id])
			}
			else if(get_pcvar_num(g_sfShowMode)==2)
			{
				client_print(id,print_center, "等级:%d/%d  经验:%d/%d  技能点数:%d点",g_level[id],g_Maxlevel,g_levelexe[id],g_leveltotalexe[id],g_levelpoint[id])
			}
			else if(get_pcvar_num(g_sfShowMode)==3)
			{
				new szMsg[128]
				format(szMsg,127,"等级:%d/%d  经验:%d/%d  技能点数:%d点",g_level[id],g_Maxlevel,g_levelexe[id],g_leveltotalexe[id],g_levelpoint[id])
				message_begin(MSG_ONE_UNRELIABLE, get_user_msgid("StatusText"), _, id)
				write_byte(0)
				write_string(szMsg)
				message_end()
			}
		}
		else
		{
			new aid=pev(id,pev_iuser2)
			if(is_user_alive(aid))
			{
				
				static opcion[64],name[33]
				new hudinfo[1560]
				get_user_name(aid,name,33)
				formatex(hudinfo, charsmax(hudinfo),"玩家:^n=== %s ===^n^n等级:%d",name,g_level[aid])
				ItemSF[aid]=0
				for(new i=0;i<=(IteamNums-1);i++)
				{
					if((zp_get_user_zombie(aid)?1:2)==ItemTeams[i]||ItemTeams[i]==3)
					{
						if(ItemCost[i]>0&&ItemIfaLive[i]!=0)
						{
							ItemSF[aid]+=1
							if(ItemLevel[i]==0)
							{
								if(g_skilllevel[aid][i]!=ItemMaxLevel[i])
									formatex(opcion, charsmax(opcion),"%s %d级", MenuItem[i],g_skilllevel[aid][i])
								else formatex(opcion, charsmax(opcion),"%s MAX!", MenuItem[i])
							}
							else{
								if(g_skilllevel[aid][i]!=ItemMaxLevel[i])
									formatex(opcion, charsmax(opcion),"%s MAX!", MenuItem[i])
							}
						}
						else if(ItemCost[i]==0&&ItemIfaLive[i]!=0)
						{
							ItemSF[aid]+=1
							if(ItemLevel[i]==0)
							{
								if(g_skilllevel[id][i]!=ItemMaxLevel[i])
									formatex(opcion, charsmax(opcion),"%s %d级", MenuItem[i],g_skilllevel[aid][i])
								else formatex(opcion, charsmax(opcion),"%s Max!", MenuItem[i])
							}
							else{
								if(g_skilllevel[id][i]!=ItemMaxLevel[i])
									formatex(opcion, charsmax(opcion),"%s %d级", MenuItem[i],g_skilllevel[aid][i])
								else formatex(opcion, charsmax(opcion),"%s Max!", MenuItem[i])
							}
						}
						formatex(hudinfo, charsmax(hudinfo),"%s^n%s",hudinfo,opcion)
					}
				}
				if(ItemSF[aid]==0)
				{
					formatex(hudinfo, charsmax(hudinfo),"      (该队伍无技能菜单)")
				}
				set_hudmessage(127, 212, 255, 0.73, 0.12, 1, 6.0,  1.1,_,_,get_pcvar_num(g_sfHudChannel))
				show_hudmessage(id, "%s",hudinfo)
				
			}
		}
	}
	else
	{
		remove_task(id+1000)
	}
}

public client_damage(attacker,victim,damage,weapon,hitplace,ta)
{
	if(is_user_connected(attacker)&&is_user_connected(victim))
	{
		if(zp_get_user_zombie(attacker)!=zp_get_user_zombie(victim))
			g_levelexe[attacker]+=floatround(damage*get_pcvar_float(g_sfEasyGainexe))
		
		for(new i=0;i<=IteamNums-1;i++)
		{
			if(ItemSkillDamage[i]==1&&g_skilllevel[attacker][i]>0)
			{
				new g_num
				if(is_user_alive(attacker)&&ItemIfaLive[i]==1||ItemIfaLive[i]==3)
				{
					if(zp_get_user_zombie(attacker)&&ItemTeams[i]==1||ItemTeams[i]==3)
						ExecuteForward(g_ForwardID1,g_num,i,attacker,victim,damage,weapon,hitplace,ta)
					else if((!zp_get_user_zombie(attacker))&&ItemTeams[i]==2)
						ExecuteForward(g_ForwardID1,g_num,i,attacker,victim,damage,weapon,hitplace,ta)
				}
				else if((!is_user_alive(attacker))&&(ItemIfaLive[i]==2))
				{
					if(zp_get_user_zombie(attacker)&&ItemTeams[i]==1||ItemTeams[i]==3)
						ExecuteForward(g_ForwardID1,g_num,i,attacker,victim,damage,weapon,hitplace,ta)
					else if((!zp_get_user_zombie(attacker))&&ItemTeams[i]==2)
						ExecuteForward(g_ForwardID1,g_num,i,attacker,victim,damage,weapon,hitplace,ta)
				}
			}
			else if(ItemSkillDamage[i]==2&&g_skilllevel[victim][i]>0)
			{
				new g_num
				if(is_user_alive(victim)&&ItemIfaLive[i]==1||ItemIfaLive[i]==3)
				{
					if(zp_get_user_zombie(victim)&&ItemTeams[i]==1||ItemTeams[i]==3)
						ExecuteForward(g_ForwardID1,g_num,i,attacker,victim,damage,weapon,hitplace,ta)
					else if((!zp_get_user_zombie(victim))&&ItemTeams[i]==2)
						ExecuteForward(g_ForwardID1,g_num,i,attacker,victim,damage,weapon,hitplace,ta)
				}
				else if((!is_user_alive(victim))&&(ItemIfaLive[i]==2))
				{
					if(zp_get_user_zombie(victim)&&ItemTeams[i]==1||ItemTeams[i]==3)
						ExecuteForward(g_ForwardID1,g_num,i,attacker,victim,damage,weapon,hitplace,ta)
					else if((!zp_get_user_zombie(victim))&&ItemTeams[i]==2)
						ExecuteForward(g_ForwardID1,g_num,i,attacker,victim,damage,weapon,hitplace,ta)
				}
			}
		}
	}
}

public zp_user_infected_pre(id,infector)
{
	g_levelexe[infector]+=get_pcvar_num(g_sfzExeGain)
}

public resetlimit()
{
	g_ShopOpen=false
	for(new id=0;id<=get_maxplayers();id++)
	{
		if(is_user_connected(id))
		{
			remove_task(id)
			get_all(Limit,ITEMNUM-1,0,id)
			get_all(ITime,ITEMNUM-1,0,id)
			for(new i=0;i<=IteamNums-1;i++)
			{
				if(g_skilllevel[id][i]>0)
					if(ItemRestart[i]==1)
					{
						new g_num
						if(is_user_alive(id)&&ItemIfaLive[i]==1||ItemIfaLive[i]==3)
						{
							if(zp_get_user_zombie(id)&&ItemTeams[i]==1||ItemTeams[i]==3)
								ExecuteForward(g_ForwardID3,g_num,id,i)
							else if((!zp_get_user_zombie(id))&&ItemTeams[i]==2)
								ExecuteForward(g_ForwardID3,g_num,id,i)
						}
						else if((!is_user_alive(id))&&(ItemIfaLive[i]==2))
						{
							if(zp_get_user_zombie(id)&&ItemTeams[i]==1||ItemTeams[i]==3)
								ExecuteForward(g_ForwardID3,g_num,id,i)
							else if((!zp_get_user_zombie(id))&&ItemTeams[i]==2)
								ExecuteForward(g_ForwardID3,g_num,id,i)
						}
					}
			}
		}
	}
	set_task(get_pcvar_float(g_sfOpenTime),"OpenShop")
}

public Respawn(id)
{
	if(is_user_connected(id))
	{
		for(new i=0;i<=IteamNums-1;i++)
		{
			if(g_skilllevel[id][i]>0)
				if(ItemRespawn[i]==1)
				{
					new g_num
					if(is_user_alive(id)&&ItemIfaLive[i]==1||ItemIfaLive[i]==3)
					{
						if(zp_get_user_zombie(id)&&ItemTeams[i]==1||ItemTeams[i]==3)
							ExecuteForward(g_ForwardID5,g_num,id,i)
						else if((!zp_get_user_zombie(id))&&ItemTeams[i]==2)
							ExecuteForward(g_ForwardID5,g_num,id,i)
					}
					else if((!is_user_alive(id))&&(ItemIfaLive[i]==2))
					{
						if(zp_get_user_zombie(id)&&ItemTeams[i]==1||ItemTeams[i]==3)
							ExecuteForward(g_ForwardID5,g_num,id,i)
						else if((!zp_get_user_zombie(id))&&ItemTeams[i]==2)
							ExecuteForward(g_ForwardID5,g_num,id,i)
					}
				}
		}
	}
}

public resetRound()
{
	for(new id=1;id<=get_maxplayers();id++)
	{
		if(is_user_connected(id))
		{
			for(new i=0;i<=IteamNums-1;i++)
			{
				if(g_skilllevel[id][i]>0)
					if(ItemRestart[i]==2)
					{
						new g_num
						if(is_user_alive(id)&&ItemIfaLive[i]==1||ItemIfaLive[i]==3)
						{
							if(zp_get_user_zombie(id)&&ItemTeams[i]==1||ItemTeams[i]==3)
								ExecuteForward(g_ForwardID6,g_num,id,i)
							else if((!zp_get_user_zombie(id))&&ItemTeams[i]==2)
								ExecuteForward(g_ForwardID6,g_num,id,i)
						}
						else if((!is_user_alive(id))&&(ItemIfaLive[i]==2))
						{
							if(zp_get_user_zombie(id)&&ItemTeams[i]==1||ItemTeams[i]==3)
								ExecuteForward(g_ForwardID6,g_num,id,i)
							else if((!zp_get_user_zombie(id))&&ItemTeams[i]==2)
								ExecuteForward(g_ForwardID6,g_num,id,i)
						}
					}
			}
		}
	}
}

public OpenShop()
{
	g_ShopOpen=true
	for(new id=1;id<=get_maxplayers();id++)
	{
		if(is_user_connected(id))
		{
			for(new i=0;i<=IteamNums-1;i++)
			{
				if(g_skilllevel[id][i]>0)
					if(ItemOpen[i]==1)
					{
						new g_num
						if(is_user_alive(id)&&ItemIfaLive[i]==1||ItemIfaLive[i]==3)
						{
							if(zp_get_user_zombie(id)&&ItemTeams[i]==1||ItemTeams[i]==3)
								ExecuteForward(g_ForwardID4,g_num,id,i)
							else if((!zp_get_user_zombie(id))&&ItemTeams[i]==2)
								ExecuteForward(g_ForwardID4,g_num,id,i)
						}
						else if((!is_user_alive(id))&&(ItemIfaLive[i]==2))
						{
							if(zp_get_user_zombie(id)&&ItemTeams[i]==1||ItemTeams[i]==3)
								ExecuteForward(g_ForwardID4,g_num,id,i)
							else if((!zp_get_user_zombie(id))&&ItemTeams[i]==2)
								ExecuteForward(g_ForwardID4,g_num,id,i)
						}
					}
			}
		}
	}
}

public zp_round_started(gamemode,id)
{
	for(new i=1;i<=32&&is_user_connected(i);i++)
	{
		if(is_user_connected(i))
			g_showtrue[i]=true;
		else g_showtrue[i]=false;
	}
		
}
public client_PreThink(id)
{
	if(is_user_connected(id))
	{
		if(is_user_alive(id))
		{
			if(g_levelexe[id]>=g_leveltotalexe[id]&&g_level[id]<g_Maxlevel)
			{
				new g_num
				g_levelexe[id]-=g_leveltotalexe[id]
				g_level[id]+=1
				g_leveltotalexe[id]=floatround(get_pcvar_num(g_sfBasicExe)*g_level[id]*get_pcvar_float(g_sfBasicNum))
				g_levelpoint[id]+=1
				ExecuteForward(g_ForwardID2,g_num,id)
				if(g_showtrue[id])
					IMenuShow(id)
			}
			if(g_levelexe[id]<0)
			{
				g_levelexe[id]+=floatround(get_pcvar_num(g_sfBasicExe)*(g_level[id]-1)*((g_level[id]-1)==1?1.0:get_pcvar_float(g_sfBasicNum)))
				g_level[id]-=1
				if(g_levelpoint[id]>0)
					g_levelpoint[id]-=1
				g_leveltotalexe[id]=floatround(get_pcvar_num(g_sfBasicExe)*g_level[id]*(g_level[id]==1?1.0:get_pcvar_float(g_sfBasicNum)))
			}
			if(g_levelexe[id]>g_leveltotalexe[id]&&g_level[id]==g_Maxlevel)	
				g_levelexe[id]=floatround(get_pcvar_num(g_sfBasicExe)*g_Maxlevel*get_pcvar_float(g_sfBasicNum))
		}
	}
	else if(g_showtrue[id])
	{
		g_showtrue[id]=false
	}
}

public plugin_natives()
{
	register_native("skill_add_item","add_skillitem",1)		//注册技能的基本信息并返回技能的ID值
	register_native("skill_get_item_id","get_skillitem",1)		//搜索技能返回技能ID的值
	register_native("skill_reset_item","reset_skillitem",1)		//删除升级菜单中指定技能
	register_native("skill_set_item","set_skillitem",1)		//修改升级菜单中技能的基本属性
	register_native("skill_get_item_hasbuy","hasbuy",1)		//技能是否被升级，是的话返回值1，否的话0，并保存在主插件
	register_native("skill_reset_item_hasbuy","resethasbuy",1)	//把技能升级的返回值设置为0
	register_native("skill_get_item_cost","getmoney",1)		//返回指定技能所需的技能点数
	register_native("skill_get_item_team","getteam",1)		//返回指定技能所代表的队伍
	register_native("skill_is_item_alive","isalive",1)		//返回指定技能是否需要活着才可以升级
	register_native("skill_limit_item","limititem",1)		//限制技能的每局升级次数
	register_native("skill_limit_item_time","limittime",1)		//限制技能的升级时间
	register_native("skill_remove_item_limit","removetimeslimit",1)	//移除指定技能的限制的次数
	register_native("skill_remove_item_time","removetime",1)		//移除指定技能的限制的时间
	register_native("skill_reset_item_limit","limitreset",1)		//把玩家当前限定的次数清零
	register_native("skill_reset_item_time","timereset",1)		//把玩家当前限制的时间清零
	register_native("skill_get_item_time","gettime",1)		//返回技能限制的时间的值
	register_native("skill_get_item_limit","getlimit",1)		//返回技能限制的次数的值
	register_native("skill_give_item","giveiditem",1)		//不用钱和验证直接给予指定玩家技能
	register_native("skill_set_item_flag","limitlevel",1)		//技能升级权限的限定
	register_native("skill_get_item_flag","getlimitlevel",1)	//返回指定技能升级所需的权限
	register_native("skill_get_item_name","getitemidname",1)		//技能名字的获取
	register_native("skill_get_item_maxlevel","getitemidmaxlevel",1)	//技能的最高等级的获取
	register_native("skill_get_item_respawn","getitemidrespawn",1)	//技能的重生所执行的模式的获取
	register_native("skill_get_item_restart","getitemidrestart",1)	//技能的重新开始所执行的模式的获取
	register_native("skill_get_item_openmode","getitemidopenmode",1)	//技能的升级菜单开放时所执行的模式的获取
	register_native("skill_get_item_buymode","getitemidbuymode",1)	//技能的升级技能时所执行的模式的获取
	register_native("skill_set_item_mode","setmode",1)		//设置技能的触发模式
	register_native("skill_get_item_damagemode","getdamagemode",1)	//获取技能的攻击触发模式
	register_native("skill_set_level_exe","setlevelexe",1)		//设置等级的经验值
	register_native("skill_get_level_exe","getlevelexe",1)		//获取该等级的经验值
	register_native("skill_set_level","setlevel",1)			//设置等级
	register_native("skill_get_level","getlevel",1)			//获取等级
	register_native("skill_set_level_point","setlevelpoint",1)		//设置技能点
	register_native("skill_get_level_point","getlevelpoint",1)		//获取技能点
	register_native("skill_set_skill_level","setlevelskill",1)		//设置技能的等级
	register_native("skill_get_skill_level","getlevelskill",1)		//获取技能的等级
	register_native("skill_reset_skill_point","resetlevelskill",1)		//重置技能点数
	register_native("skill_get_skill_num","getlevelskillnum",1)		//获取技能的数目
	
}

//native注册技能的基本信息并返回技能的ID值
public add_skillitem(const itemname[],itemteam,itemcosts,itembuyalive,skillmaxlevel)
{
	param_convert(1)
	ArrayPushString(AritemName,itemname)
	ArrayGetString(AritemName,IteamNums,MenuItem[IteamNums],ITEMNUM-1)
	ItemTeams[IteamNums]=itemteam
	ItemCost[IteamNums]=itemcosts
	ItemIfaLive[IteamNums]=itembuyalive
	ItemMaxLevel[IteamNums]=skillmaxlevel
	IteamNums+=1
	return IteamNums-1
}

//native技能的重生所执行的forward的模式的获取
public getitemidrespawn(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return ItemRespawn[itemid]
}


//native技能的重新开始所执行的forward的模式的获取
public getitemidrestart(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return ItemRestart[itemid]
}


//native搜索技能返回技能ID的值
public get_skillitem(const itemname[])
{
	param_convert(1)
	static i, item_name[32]
	for (i = 0; i < IteamNums; i++)
	{
		ArrayGetString(AritemName, i, item_name, charsmax(item_name))
		if (equali(itemname, item_name))
			return i;
	}
	
	return -1;
}
//native删除升级菜单中指定技能
public reset_skillitem(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	new item[]={""}
	format(MenuItem[itemid],ITEMNUM-1,"%s",item)
	ItemTeams[itemid]=0
	ItemCost[itemid]=0
	ItemIfaLive[itemid]=0
	ItemMaxLevel[itemid]=0
	ItemRespawn[itemid]=0
	ItemRestart[itemid]=0
	ItemBuy[itemid]=0
	ItemOpen[itemid]=0
	ItemSkillDamage[itemid]=0
	return 0
}
//native修改升级菜单中技能的基本属性
public set_skillitem(itemid,const itemname[],itemteam,itemcosts,itembuyalive,skillmaxlevel)
{
	if(itemid>ITEMNUM)
		return -1
	
	param_convert(2)
	ArraySetString(AritemName,itemid,itemname)
	ArrayGetString(AritemName,itemid,MenuItem[itemid],ITEMNUM-1)
	ItemTeams[itemid]=itemteam
	ItemCost[itemid]=itemcosts
	return 0
} 
//native技能是否被升级，是的话返回值1，否的话0，并保存在本插件
public hasbuy(id,itemid)
{
	if(itemid>ITEMNUM)
		return -1
	if(itemid+1==ApackPD[id])
		return 1
	return 0
}
//native把技能升级的返回值设置为0
public resethasbuy(id)
{
	ApackPD[id]=0
}
//native返回指定技能所需的技能点数
public getmoney(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return ItemCost[itemid]
}
//native返回指定技能所代表的队伍
public getteam(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return ItemTeams[itemid]
}
//native返回指定技能是否需要活着才可以升级
public isalive(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return ItemIfaLive[itemid]
}
//native限制技能的每局升级次数
public limititem(itemid,times)
{
	if(itemid>ITEMNUM)
		return -1
	
	TimesLimit[itemid]=times
	return 0
}
//native限制技能的升级时间
public limittime(itemid,Float:long)
{
	if(itemid>ITEMNUM)
		return -1
	
	TimeLimit[itemid]=long
	return 0
}
//native移除指定技能的限制的次数
public removetimeslimit(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	TimesLimit[itemid]=0
	return 0
}
//native移除指定技能的限制的时间
public removetime(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	TimeLimit[itemid]=0.0
	return 0
}
//native把玩家升级指定技能限定的次数清零
public limitreset(id,itemid)
{
	if(itemid>ITEMNUM)
		return -1
	Limit[itemid][id]=0
	return 0
}
//native把玩家当前限制的时间清零
public timereset(id,itemid)
{
	if(itemid>ITEMNUM)
		return -1
	ITime[itemid][id]=0
	return 0
}
//native返回技能限制的时间的值
public Float:gettime(itemid)
{
	if(itemid>ITEMNUM)
		return -1.0
	
	return TimeLimit[itemid]
}
//native返回技能限制的次数的值
public getlimit(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return TimesLimit[itemid]
}

//native不用技能点数和验证直接升级指定玩家技能
public giveiditem(id,itemid)
{
	if(itemid>ITEMNUM)
		return -1
	new g_num
	ApackPD[id]=itemid+1
	ExecuteForward(g_ForwardID,g_num,id,itemid)
	
	return 0
}
//native技能升级权限的限定
public limitlevel(itemid,level)
{
	if(itemid>ITEMNUM)
		return -1
	
	ItemLevel[itemid]=level
	
	return 0
}
//native返回指定技能升级所需的权限
public getlimitlevel(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return ItemLevel[itemid]
}
//native技能名字的获取
public getitemidname(itemid,itemname[],len)
{
	if(itemid>ITEMNUM)
		return -1
	
	format(itemname,len,"%s",MenuItem[itemid])
	
	return 0
}
//native技能的最高等级的获取
public getitemidmaxlevel(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return ItemMaxLevel[itemid]
}
//native设置技能的触发模式
public setmode(itemid,Respawns,Restart,Buy,ShopOpen,Damage)
{
	if(itemid>ITEMNUM)
		return -1
	ItemRespawn[itemid]=Respawns
	ItemRestart[itemid]=Restart
	ItemBuy[itemid]=Buy
	ItemOpen[itemid]=ShopOpen
	ItemSkillDamage[itemid]=Damage
	
	return 0
}
//native技能的升级菜单开放时所执行的模式的获取
public getitemidopenmode(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return ItemOpen[itemid]
}
//native技能的升级技能时所执行的模式的获取
public getitemidbuymode(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return ItemBuy[itemid]
}
//native获取技能的攻击触发模式
public getdamagemode(itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return ItemSkillDamage[itemid]
}
//native设置经验
public setlevelexe(index,exe)
{
	new g_exe
	if(g_level[index]>1)
	{
		for(new i=2;i<=g_level[index]-1;i++)
		{
			g_exe+=floatround(get_pcvar_num(g_sfBasicExe)*i*get_pcvar_float(g_sfBasicNum))
		}
		g_exe+=get_pcvar_num(g_sfBasicExe)
		g_exe+=g_levelexe[index]
	}
	else g_exe=g_levelexe[index]
	g_levelexe[index]+=exe-g_exe
}
//native得到经验
public getlevelexe(index)
{
	new g_exe
	if(g_level[index]>1)
	{
		for(new i=2;i<=g_level[index]-1;i++)
		{
			g_exe+=floatround(get_pcvar_num(g_sfBasicExe)*i*get_pcvar_float(g_sfBasicNum))
		}
		g_exe+=get_pcvar_num(g_sfBasicExe)
		g_exe+=g_levelexe[index]
	}
	else g_exe=g_levelexe[index]
	return g_exe
}
//native设置等级
public setlevel(index,level)
{
	g_level[index]=level
}
//native获取等级
public getlevel(index)
{
	return g_level[index]
}

//native设置技能点数
public setlevelpoint(index,point)
{
	g_levelpoint[index]=point
}
//native获取技能点数
public getlevelpoint(index)
{
	return g_levelpoint[index]
}
//native设置技能的等级
public setlevelskill(id,itemid,level)
{
	if(itemid>ITEMNUM)
		return -1
	if(ItemMaxLevel[itemid]>=level)
	{
		g_skilllevel[id][itemid]=level
	}
	else return 1
	
	return 0
}
//native获取技能的等级
public getlevelskill(id,itemid)
{
	if(itemid>ITEMNUM)
		return -1
	
	return g_skilllevel[id][itemid]
}
//native重置技能点数
public resetlevelskill(id)
{
	new g_num
	g_levelpoint[id]=g_level[id]-1
	for(new i=0;i<=IteamNums-1;i++)
		g_skilllevel[id][i]=0
	ExecuteForward(g_ForwardID7,g_num,id)
	
	return 0
}
//native获取技能的数目
public getlevelskillnum(mode)
{
	new g_num=0
	if(mode!=3)
	{
		for(new i=0;i<=(IteamNums-1);i++)
		{
			if(mode==ItemTeams[i]||ItemTeams[i]==3)
			{
				g_num++	
			}
		}
		return g_num
	}
	else return (IteamNums-1)
	
	return 0
}



public removelimits(id)
{
	ITime[Arid[id]][id]=0
	Arid[id]=0
}


public Choose(id, menu, item)
{
	if(item==MENU_EXIT)
	{
		menu_destroy(menu)
		return PLUGIN_HANDLED
	}
	new command[6], name[64], access, callback
	menu_item_getinfo(menu, item, access, command, sizeof command - 1, name, sizeof name - 1, callback)
	switch(item)
	{
		case 0..ITEMNUM:
		{
			if(g_ShopOpen)
			{
				new itemid=select_to_num(id,ItemTeams[0],ITEMNUM,item+1)
				if(get_pcvar_num(g_sfrMode)!=0)
				{
					if(ItemSF[id]==item)
					{
						if((get_pcvar_num(g_sfrMode)==1)?getlevelexe(id)>=get_pcvar_num(g_sfrExe):zp_get_user_ammo_packs(id)>=get_pcvar_num(g_sfrPacks))
						{
							new g_num
							g_levelpoint[id]=g_level[id]-1
							for(new i=0;i<=IteamNums-1;i++)
								g_skilllevel[id][i]=0
							if(get_pcvar_num(g_sfrMode)==1)
								g_levelexe[id]-=get_pcvar_num(g_sfrExe)
								
							else zp_set_user_ammo_packs(id,zp_get_user_ammo_packs(id)-get_pcvar_num(g_sfrPacks))
							ExecuteForward(g_ForwardID7,g_num,id)
							client_color(id,"/y恭喜你！技能点重置成功...")
						}
						else
						{
							client_color(id,"/y对不起,您的/g%s/y不够,无法重置技能点数",(get_pcvar_num(g_sfrMode)==1)?"经验":"弹药袋")
						}
						return PLUGIN_CONTINUE;
					}
				}
				if(get_user_flags(id)&ItemLevel[itemid]||ItemLevel[itemid]==0)
				{
					if(is_user_alive(id)&&ItemIfaLive[itemid]==1||!is_user_alive(id)&&ItemIfaLive[itemid]==2||ItemIfaLive[itemid]==3)
					{
						if(Limit[itemid][id]<TimesLimit[itemid]||TimesLimit[itemid]==0)
						{
							if(ITime[itemid][id]==0)
							{
								if(g_skilllevel[id][itemid]!=ItemMaxLevel[itemid])
								{
									if(getlevelpoint(id)<ItemCost[itemid]&&item<ItemSF[id]&&ItemCost[itemid]!=0)
									{
										client_color(id,"/y技能点数/y不足/g%d/y 升级/ctr%s/y失败！！！",ItemCost[itemid], MenuItem[itemid])
									}
									else if(getlevelpoint(id)>=ItemCost[itemid]&&item<ItemSF[id]&&ItemCost[itemid]!=0)
									{
										new g_num
										setlevelpoint(id,getlevelpoint(id)-ItemCost[itemid])
										g_skilllevel[id][itemid]+=1
										if(g_skilllevel[id][itemid]!=ItemMaxLevel[itemid])
											client_color(id,"/y恭喜你！！！/y升级/ctr%s/y成功！花费/g%d/y技能点,现在的等级是 /g%d级", MenuItem[itemid],ItemCost[itemid],g_skilllevel[id][itemid])
										else client_color(id,"/y恭喜你！！！/ctr%s/y已经升到满级！花费/g%d/y技能点", MenuItem[itemid],ItemCost[itemid])
										ApackPD[id]=itemid+1
										ExecuteForward(g_ForwardID,g_num,id,ApackPD[id]-1)
										if(g_num==1)
										{
											ApackPD[id]=0
										}
										if(TimesLimit[itemid]>0)
											Limit[itemid][id]+=1
										if(TimeLimit[itemid]>0)
										{
											ITime[itemid][id]=1
											Arid[id]=itemid
											set_task(TimeLimit[itemid],"removelimits",id)
										}
									}
									else if(item<ItemSF[id]&&ItemCost[itemid]==0)
									{
										new g_num
										client_color(id,"/y你选择了/ctr%s/y！！！", MenuItem[itemid])
										ApackPD[id]=itemid+1
										if(ItemBuy[ApackPD[id]-1]==1)
											ExecuteForward(g_ForwardID,g_num,id,ApackPD[id]-1)
										if(g_num==1)
										{
											ApackPD[id]=0
										}
										if(TimesLimit[itemid]>0)
											Limit[itemid][id]+=1
										if(TimeLimit[itemid]>0)
										{
											ITime[itemid][id]=1
											Arid[id]=itemid
											set_task(TimeLimit[itemid],"removelimits",id)
										}
									}
									
								}
								else client_color(id,"/ctr%s/y已经升到满级了,无法继续为您升级", MenuItem[itemid])
								if(g_levelpoint[id]>0)
									IMenuShow(id)
							}
							else
							{
								client_color(id,"/ctr%s/y被限制/g每%.1f秒/y升级,请/g等待/y后再升级", MenuItem[itemid],TimeLimit[itemid])
							}
						}
						else
						{
							client_color(id,"/ctr%s/y被限制每局只能升级/g%d/y次,请/g下局/y再升级", MenuItem[itemid],TimesLimit[itemid])
						}
					}
					else
					{
						client_color(id,"/y你现在处于/g%s/y的状态,不能升级 /ctr%s/y",AliveShow[ItemIfaLive[itemid]-1],MenuItem[itemid])
					}
				}
				else
				{
					client_color(id,"/y你的/g权限/y不足以升级/ctr%s/y,请提升权限再行尝试",MenuItem[itemid])
				}
			}
			else
			{
				client_color(id,"/ctr技能/y被限制开局后/g%d/y秒才可以升级，请/g等待.",get_pcvar_num(g_sfOpenTime))
			}
		}
	}
	
	menu_destroy(menu)
	
	return PLUGIN_HANDLED
}


public IMenuShow(id)
{
	if(g_ShopOpen)
	{
		static opcion[64]
		formatex(opcion, charsmax(opcion),"\w升级菜单\y||\w技能点数:\r%d",g_levelpoint[id])
		new iMenu=menu_create(opcion,"Choose")			//执行菜单命令的目标
		new i,szTempid[10]
		ItemSF[id]=0
		for(i=0;i<=(IteamNums-1);i++)
		{
			if(((zp_get_user_zombie(id)?1:2)==ItemTeams[i]||ItemTeams[i]==3)&&get_user_team(id)!=3)
			{
				if(ItemCost[i]>0&&ItemIfaLive[i]!=0)
				{
					ItemSF[id]+=1
					if(ItemLevel[i]==0)
					{
						if(g_skilllevel[id][i]!=ItemMaxLevel[i])
							formatex(opcion, charsmax(opcion),"%s \y(\r%d/%d/%d\y) ", MenuItem[i],ItemCost[i],g_skilllevel[id][i],ItemMaxLevel[i])
						else formatex(opcion, charsmax(opcion),"%s \rMAX!", MenuItem[i])
					}
					else{
						if(g_skilllevel[id][i]!=ItemMaxLevel[i])
							formatex(opcion, charsmax(opcion),"%s \rMAX! \yADMIN", MenuItem[i])
					}
				}
				else if(ItemCost[i]==0&&ItemIfaLive[i]!=0)
				{
					ItemSF[id]+=1
					if(ItemLevel[i]==0)
					{
						if(g_skilllevel[id][i]!=ItemMaxLevel[i])
							formatex(opcion, charsmax(opcion),"%s \y(\r%d/%d\y)", MenuItem[i],g_skilllevel[id][i],ItemMaxLevel[i])
						else formatex(opcion, charsmax(opcion),"%s \rMax!", MenuItem[i])
					}
					else{
						if(g_skilllevel[id][i]!=ItemMaxLevel[i])
							formatex(opcion, charsmax(opcion),"%s \y(\r%d/%d\y) \yADMIN", MenuItem[i],g_skilllevel[id][i],ItemMaxLevel[i])
						else formatex(opcion, charsmax(opcion),"%s \rMax! \yADMIN", MenuItem[i])
					}
				}
				menu_additem(iMenu, opcion, szTempid,0)
			}
		}
		if(ItemSF[id]==0)
		{
			client_color(id,"/y该/ctr队伍/y的升级菜单/g无技能/y,/ctr无法打开/g升级菜单")
		}
		else if(get_pcvar_num(g_sfrMode)!=0)
		{
			formatex(opcion, charsmax(opcion),"\y重置技能点数\w(\r%d%s\w)",(get_pcvar_num(g_sfrMode)==1)?get_pcvar_num(g_sfrExe):get_pcvar_num(g_sfrPacks),(get_pcvar_num(g_sfrMode)==1)?"经验":"弹药袋")
			menu_additem(iMenu, opcion, szTempid,0)
		}
		menu_setprop(iMenu, MPROP_EXIT, MEXIT_ALL)
		formatex(opcion, charsmax(opcion),"\w返回")	//返回菜单的名字
		menu_setprop(iMenu, MPROP_BACKNAME, opcion)
		formatex(opcion, charsmax(opcion),"\w下一页")	//下一页菜单的名字
		menu_setprop(iMenu, MPROP_NEXTNAME, opcion)
		formatex(opcion, charsmax(opcion),"\w退出")	//退出菜单的名字
		menu_setprop(iMenu, MPROP_EXITNAME, opcion)
		menu_setprop(iMenu, MPROP_NUMBER_COLOR, "\r")		//菜单前面颜色的数字
		menu_display(id, iMenu, 0)
	}
	else
	{
		client_color(id,"/ctr升级菜单/y被限制开局后/g%d/y秒才可以打开，请/g等待.",get_pcvar_num(g_sfOpenTime))
	}
	
	return PLUGIN_HANDLED
}

public client_disconnect(id)
{
	ApackPD[id]=0
	ItemSF[id]=0
	get_all(Limit,ITEMNUM-1,0,id)
	get_all(ITime,ITEMNUM-1,0,id)
	for(new i=0;i<=IteamNums-1;i++)
		g_skilllevel[id][i]=0
	remove_task(id)
	remove_task(id+1000)
	g_levelexe[id]=0
	g_leveltotalexe[id]=get_pcvar_num(g_sfBasicExe)
	g_level[id]=1
	g_levelpoint[id]=0
}
	


/*============================华丽分割线===============================*/

stock client_color(id, const input[], any:...)
{
	static iPlayersNum[32], iCount; iCount = 1
	static szMsg[191]
	
	vformat(szMsg, charsmax(szMsg), input, 3)
	
	replace_all(szMsg, 190, "/g", "^4") 			// 绿色
	replace_all(szMsg, 190, "/y", "^1") 			// 橙色
	replace_all(szMsg, 190, "/ctr", "^3") 			// 队伍色
	replace_all(szMsg, 190, "/w", "^0") 			// 黄色
	
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

//判断出当前选择的技能是否符合设置的队伍,符合的话返回该技能在队伍中所对应的位置
stock select_to_num(index,numbers[],len,item)
{
	new empty=0
	for(new i=0;i<len;i++)
	{
		if(((zp_get_user_zombie(index)?1:2)==numbers[i]||numbers[i]==3)&&get_user_team(index)!=3)
			empty+=1
		if(empty==item)
			return i
	}
	return 0
}


stock get_all(num[][],len,change,index)
{
	for(new i=0;i<len-1;i++)
	{
		num[i][index]=change
	}
}
