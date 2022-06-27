//Code Information
/***********************************************************
*                                                          *
* <GoodPass Password Manager>                              *
* Written by GeorgeDong32                                  *
* Copyright (c) GeorgeDong32(Github). All rights reserved. *
*                                                          *
***********************************************************/
//代码日志
//见"CodeBlog.h"

//函数&头文件
#include "Generate.h"
#include "FileOperate.h"
#include "Data.h"
#include "MKeyProcess.h"
#include "Display.h"
#include "GPBase.h"
int Test_Mode_Control = 0;//测试模式调控符

string version = "2.6.0    ";//更新版本号! 
//替换时格式为 "n.x.y    "(4个空格) 或 "n.x.y dev"

//加密基数
int PI[40] = { 1,4,1,5,9,2,6,5,3,5,8,9,7,9,3,2,3,8,4,6,2,6,4,3,3,8,3,2,7,9,5,0,2,8,8,4,1,9,7,1 };
/*string  inplat = "|  输入平台名称： |"; string inaccout = "|  输入账号名： |";
string indate = "|  输入日期：  |"; string inRTmod2 = "|  输入<rg>测试生成,<rd>测试解密  |";
string inRTmod1 = "| 请输入循环模式                  |"; string inNOT = "|  请输入测例个数  |";
string inname = "|  输入用户名称(可选)，输入0以跳过  |";*/
string MKC0 = "|  请设置一个长度位15~39位的密码  |"; string MKC2 = "|  请重新设置主密码,长度应为15~39位  |";

int main(void)
{
	system("color 70");//设置控制台颜色，7为背景，0为字体
	//定义区
	string name, account, platform, patch, oripla, RTmod;
	string MDpath = "D:\\My Project\\GoodPass\\MData\\Data.csv";
	string G_encr;  //生成阶段加密结果
	string final; //结果数组
	string Test_Direct;//测试模式导向符
	string D_encr; string D_pwdc;//解密阶段字符串
	string MainKey;
	int namel = 0; int rek = 0; int date = 0; int fopc = 0;//输出控制符
	int RT_Control = 0;//循环测试控制符
	int ipc = 1; //检测输入次数
	int ipfl = 0; int NOT = 0;//NOT为循环次数控制符
	char direct = '0';  //开始操作判断符
	int next = -1;  //结束操作判断符
	string dates = "00000000";
	char opt = '@';
	//程序头打印区
	PrintTitle();
	//主密码配置检查
	int mkc = MKconInit();
	FloderInit(Test_Mode_Control);//文件夹初始化
	switch (mkc)//主密码配置检查导向
	{
	case 2://配置异常，重设密码
		Displayinf(MKC2, 1, 0, "ori");
		cin >> MainKey;
		setConfig(MainKey);
		break;
	case 1:
		checkConfig(MainKey);//配置正常，检查密码
		break;
	case 0://初次进入设置密码
		Displayinf(MKC0, 1, 0, "ori");
		cin >> MainKey;
		setConfig(MainKey);
		break;
	}
	//Manager初始化
	Manager gpm;
	DataInit();
	DataInit(gpm, MDpath);
	gpm.dataupdate();
	printMenu(Test_Mode_Control);
	while (cin >> opt)//选择菜单
	{
		switch (opt)
		{
		case 'e':
		case '0':
			FileUpdate(gpm, MDpath, 1);
			gpm.~Manager();
			system("pause");
			exit(0);
			break;
		case 'a':
		case '1':
			GP_add(gpm);
			break;
		case 's':
		case '2':
			GP_search(gpm);
			break;
		case 'g':
		case '3':
			GP_get(gpm);
			break;
		case 'c':
		case '4':
			GP_change(gpm);
			break;
		case 'd':
		case '5':
			GP_delete(gpm);
			break;
		case '6':
			GP_showall(gpm);
			break;
		default:
			Displayinf("请检查您的输入", 0, 0, "yellow");
			break;
		}
		printMenu(Test_Mode_Control);
	}
	FileUpdate(gpm, MDpath, 1);//数据保护
	gpm.~Manager();
	return 0;
}
