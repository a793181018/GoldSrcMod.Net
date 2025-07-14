/*
*				信息的引索
*	itemteam：	
*	1表示只能僵尸的队伍显示,2表示人类的队伍显示,3为任何队伍都显示
*
*	itembuyalive：
*	1表示只能活着的时候可以升级,2表示只能死亡才可以升级，3表示都可以升级
*	
*	itemcosts：
*	技能的技能点数，设置如果为0表示在升级菜单菜单中不显示技能点数
*
*	itemid：
*	技能在升级菜单中的ID,每种技能有自己的id，在创建时会自动生成
*
*	times：
*	升级技能时所限制的次数，默认0（无限次数）
*
*	long：
*	技能时所限制的时间，默认0
*
*	level:
*	升级技能所需要的权限,默认ADMIN_ALL
*
*	skillmaxlevel:
*	所注册的技能的最高等级
*
*	Respawns:
*	表示重生时是否执行本技能的skill_select(id,itemid)
*	2表示开局的重生不执行，1表示任何重生都执行,0表示不执行
*
*	Restart:
*	表示回合开始时是否执行本技能的skill_select(id,itemid)
*	2表示静止秒数完后执行，1表示开局直接执行,0表示不执行
*
*	Buy:
*	表示购买时是否执行本技能的skill_select(id,itemid)
*	1表示购买时执行,0表示不执行
*	
*	ShopOpen:
*	表示每局的升级菜单允许开放时触发
*	1表示允许时执行,0表示不执行
*
*	Damage:
*	表示技能的攻击触发
*	skill_damage_selected(itemid,attacker,victim,damage,weapon,hitplace,ta)
*	2表示被攻击时触发,1表示攻击执行,0表示不触发
*
*	提示：如果返回值-1或者-1.0表示错误（技能过多可增加主插件的ITEMNUM宏定义以修正错误）
*	如果返回值为0以上表示代码正常执行
*/


#if defined _skillmenu_included
  #endinput
#endif
#define _skillmenu_included


//---------------native---------------




//添加(创建)升级菜单技能的基本属性并返回整数值技能ID
native skill_add_item(itemname[],itemteam,itemcosts,itembuyalive,skillmaxlevel)
/*
*	itemname写的是你想要添加的技能的名字,得带引号
*
*	itemteam写的是能显示该技能的队伍，1表示只能僵尸的队
*	伍显示,2表示人类的队伍显示,3为任何队伍都显示
*
*	itemcosts写的是升级改技能需要花费多少点数技能，0表示不显示技能点数
*
*	itembuyalive写的是是否需要活着才可以升级，1表示只能活着
*	的时候可以升级,2表示只能死亡才可以升级，3表示都可以升级
*
*	skillmaxlevel写的是所要注册的技能的最高等级是多少

*	返回值 技能的ID
*/




//设置技能的触发模式
native skill_set_item_mode(itemid,Respawns,Restart,Buy,ShopOpen,Damage)
/*
*
*对应Forward:skill_selected(id,itemid)
*	Respawns表示重生时是否执行本技能对应Forward
*	2表示开局的重生不执行，1表示任何重生都执行,0表示不执行
*
*2对应Forward:skill_restart_selected_post(id,itemid)
*1对应Forward:skill_restart_selected_pre(id,itemid)
*	Restart表示回合开始时是否执行本技能对应Forward
*	2表示静止秒数完后执行，1表示开局直接执行,0表示不执行
*
*对应Forward:skill_selected(id,itemid)
*	Buy表示购买时是否执行本技能对应Forward
*	1表示购买时执行,0表示不执行
*	
*对应Forward:skill_selected(id,itemid)
*	ShopOpen表示每局的升级菜单允许开放时触发对应Forward
*	1表示允许时执行,0表示不执行
*
*
*对应Forward:skill_damage_selected(itemid,attacker,victim,damage,weapon,hitplace,ta)
*	Damage表示技能的攻击
*	触发对应Forward
*	2表示被攻击时触发,1表示攻击执行,0表示不触发
*/


//返回技能ID的值
native skill_get_item_id(const itemname[])
/*
*	itemname写的是你想要的搜索技能ID的技能的名字,得带引号
*
*	返回值 整数值
*/





//删除升级菜单中指定技能ID技能
native skill_reset_item(itemid)
/*
*	itemid写的是你想要的删除的技能的ID
*
*	当返回值 -1 表示查无此此技能
*/






//修改升级菜单中技能的基本属性
native skill_set_item(itemid,const itemname[],itemteam,itemcosts,itembuyalive,skillmaxlevel)
/*
*	itemid写的是你想要的修改的技能的ID
*
*	itemname写的是修改后的技能的名字,得带引号
*
*	itemteam写的是修改后能显示该技能的队伍，1表示只能T的队
*	伍显示,2表示CT的队伍显示,3为任何队伍都显示
*
*	itemcosts写的是修改后升级改技能需要花费多少钱，0表示不显示技能点数
*
*	itembuyalive写的是修改后是否需要活着才可以升级，1表示只能活着
*	的时候可以升级,2表示只能死亡才可以升级，3表示都可以升级
*
*	skillmaxlevel写的是所要注册的技能的最高等级是多少
*
*	当返回值 -1 表示查无此此技能
*/






//技能是否被指定对像升级，是的话返回值1，否的话0（与Think联用，与client_shop相比效率更低）
native skill_get_item_hasbuy(index,itemid)
/*
*	返回值 整数值
*/





//把保存在升级菜单插件中的指定对像的技能升级的返回值设置为0（清除升级信息）
native skill_reset_item_hasbuy(index)
/*
*	返回值 整数值
*/






//返回指定技能所需的技能点数
native skill_get_item_cost(itemid)
/*
*	返回值 整数值
*/



//返回指定技能的攻击触发模式
native skill_get_item_damagemode(itemid)
/*
*	2表示被攻击时触发,1表示攻击执行,0表示不触发
*	返回值 整数值
*/



//返回指定技能所能显示的队伍，详见信息引索itemteam
native skill_get_item_team(itemid)
/*
*	1表示僵尸的队伍显示,2表示人类的队伍显示,3为任何队伍都显示
*	当返回值 整数值
*/






//返回指定技能是否需要活着才可以升级，详见信息引索itembuyalive
native skill_is_item_alive(itemid)
/*
*	当返回值 整数值
*/







//限制指定技能ID技能的每局升级次数(可以做修改用)
native skill_limit_item(itemid,times)
/*
*	times表示升级技能时每局所限制的次数
*	每局可以升级times次,开局自动清零,times必须是正整数值
*/






//限制指定技能ID技能的升级时间(可以做修改用)
native skill_limit_item_time(itemid,Float:long)
/*
*	long表示升级技能时所限制的时间
*	每long秒升级一次,开局自动清零,long必须是正浮点数值
*/







//移除指定技能ID技能的限制的次数（即无限制升级次数）
native skill_remove_item_limit(itemid)
/*
*	也就是清除（移除）技能的升级次数（设置为无限）,详见times的信息的引索
*/







//移除指定技能ID技能的限制的时间
native skill_remove_item_time(itemid)
/*
*	也就是清除（移除）技能的升级时间,详见long的信息的引索
*/






//把玩家升级指定技能ID技能限定的次数清零
native skill_reset_item_limit(index,itemid)
/*
*	清零玩家升级时所限制的次数（是清零玩家的升级次数而不是清除）
*/






//把玩家升级指定技能ID技能限定的时间清零
native skill_reset_item_time(index,itemid)
/*
*	清零玩家升级时正在限制的时间
*/





//返回指定技能ID技能限制的时间的值,返回值为浮点值
native Float:skill_get_item_time(itemid)
/*
*	当返回值 浮点数值
*/





//返回指定技能ID技能限制的次数的值,返回值为整数值
native skill_get_item_limit(itemid)
/*
*	当返回值 整数值
*/




//不用技能点数和验证直接升级指定玩家技能
native skill_give_item(index,itemid)





//指定技能ID技能升级权限的限定
native skill_set_item_flag(itemid,level)
/*
*	level表示玩家所需升级或者选择技能的权限，如ADMIN_LEVEL_A
*/





//获得指定技能ID技能所需要的升级权限
native skill_get_item_flag(itemid)
/*
*	即返回等级的宏定义值,如技能升级所需权限为ADMIN_ALL,则返回值0
*	对应整数值详见amxmodx网站
*	返回值 整数值
*/




//技能名字的获取
native skill_get_item_name(itemid,itemname[],len)
/*
*	itemid表示技能的id
*	itemname[]表示用于存储名字的一维数组变量
*	len表示itemname[]数组的大小
*/





//技能的最高等级的获取
native skill_get_item_maxlevel(itemid)
/*
*	若返回值为-1，表示技能编号超出预定技能数
*	返回值 对应技能的最高等级
*/



//技能的重生所执行的forward的模式的获取
native skill_get_item_respawn(itemid)
/*
*	若返回值为-1，表示技能编号超出预定技能数
*	返回值 重生所执行的forward的模式
*	2表示开局的重生不执行，1表示任何重生都执行,0表示不执行
*/



//技能的重新开始所执行的forward的模式的获取
native skill_get_item_restart(itemid)
/*
*	若返回值为-1，表示技能编号超出预定技能数
*	返回值 重新开始所执行的forward的模式
*	2表示静止秒数完后执行，1表示开局直接执行,0表示不执行
*/

//设置添加经验的值
native skill_set_level_exe(index,exe)

//获取经验的值
native skill_get_level_exe(index)

//设置等级的值
native skill_set_level(index,level)

//获取等级的值
native skill_get_level(index)

//设置技能点的值
native skill_set_level_point(index,point)

//获取技能点的值
native skill_get_level_point(index)


//设置指定技能的等级
native skill_set_skill_level(index,itemid,level)

//获得指定技能的等级
native skill_get_skill_level(index,itemid)

//重置技能点数
native skill_reset_skill_point(index)

//获取技能的数目
native skill_get_skill_num(mode)
/*
*	mode表示要获取技能数的类型
*	1表示获取僵尸的技能总数,2表示获取人类的技能总数,3表示获取总技能数
*/


//---------------Forward---------------


//升级时触发
forward skill_levelup(index)

//升级技能的Forward,升级相应的技能模式的触发
forward skill_selected(id,itemid)
/*
*	在skill_selected内的if(itemid==添加技能id)（判断升级的技能是什么）中必须要执行
*	skill_reset_item_hasbuy(id)
*	id表示升级技能的玩家ID
*	itemid表示所升级的技能的技能ID
*/


//技能攻击时的Forward,设置相应的技能模式的触发
forward skill_damage_selected(itemid,attacker,victim,damage,weapon,hitplace,ta)


//重新开始游戏时触发
forward skill_restart_selected_pre(id,itemid)

//商场开放时触发
forward skill_open_selected(id,itemid)

//复活时触发
forward skill_respawn_selected(id,itemid)

//静止秒数数完触发
forward skill_restart_selected_post(id,itemid)

//重置技能点数时触发
forward skill_levelreset(id)