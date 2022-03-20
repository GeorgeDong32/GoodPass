//Code Information
/*
<GoodPass 密码管理器>
Written by GeorgeDong32
Copyright (c) GeorgeDong32(Github). All rights reserved.
*/
//代码日志
//见"CodeBlog.h"
char version[] = "1.7.5 ";//更新版本号！char[7]

//函数&头文件
#include <fstream>
#include <string>
#include <stdio.h>
#include <Windows.h>
using namespace std;
#include "Generate.h"
#include "PPF_cryption.h"
#include "FileOperate.h"
#include "MKeyProcess.h"
int start_option(char control);
int Test_Mode_Control = 1;//测试模式调控符
//加密基数
int PI[40] = { 1,4,1,5,9,2,6,5,3,5,8,9,7,9,3,2,3,8,4,6,2,6,4,3,3,8,3,2,7,9,5,0,2,8,8,4,1,9,7,1 };
string  inplat = "|  输入平台名称： |"; string inaccout = "|  输入账号名： |"; 
string indate = "|  输入日期：  |"; string inRTmod2 = "|  输入<rg>测试生成,<rd>测试解密  |";
string inRTmod1 = "| 请输入循环模式                  |"; string inNOT = "|  请输入测例个数  |";
string inname = "|  输入用户名称(可选)，输入0以跳过  |"; string MKC1 = "|  请输入主密码进行校验  |";
string MKC0 = "|  请设置您的主密码  |"; string MKC2 = "|  请重新设置主密码  |";

int main(void)
{
	system("color 70");//设置控制台颜色，7为背景，0为字体
	//cout << inplat.length() << endl;
	//定义区
	string name, account, platform, patch, oripla, RTmod;
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
	//程序头打印区
	PrintTitle();
	//主密码配置检查
	int mkc = MKconInit();
	switch(mkc)
	{
		case 2:
			printLine(MKC2.length() - 6, Test_Mode_Control);
			cout << MKC2 << endl;
			printLine(MKC2.length() - 6, Test_Mode_Control);
			cin >> MainKey;
			setConfig(MainKey);
			break;
		case 1:
			checkConfig(MainKey);
			break;
		case 0:
			printLine(MKC0.length() - 6, Test_Mode_Control);
			cout << MKC0 << endl;
			printLine(MKC0.length() - 6, Test_Mode_Control);
			cin >> MainKey;
			setConfig(MainKey);
			break;
	}
	//初始定向区
	printf("+----------------------------------------------------+\n");
	printf("|  输入G/g进入密码生成程序，输入D/d进入结果解密程序  |\n");
	printf("+----------------------------------------------------+\n");
	cin >> direct;
Start_option:
	int Option = start_option(direct);
	switch(Option)
	{
	case 1:
		Test_Mode_Control = 0;
		//文件夹初始化
		FloderInit(Test_Mode_Control);
		goto Generator; break;
	case 2:
		Test_Mode_Control = 0;
		//文件夹初始化
		FloderInit(Test_Mode_Control);
		goto Decryptor; break;
	case 3: 
		//文件夹初始化
		FloderInit(Test_Mode_Control);
		goto Test_Mode; break;
	case 4:
		Test_Mode_Control = 0;
		//文件夹初始化
		FloderInit(Test_Mode_Control);
		goto Direct_Error; break;
	}
Direct_Error:
	while (direct != 'G' && direct != 'g' && direct != 'D' && direct != 'd' && direct != 't')
	{
		int Direct_Waring_Control = 0;//警告输出控制
		ipfl = direct != 'G' && direct != 'g' && direct != 'D' && direct != 'd';
		ipc++;
		if (ipc > 1 && ipfl == 1 && Direct_Waring_Control < 1)
		{
			printf("+----------------------------------------------------+\n");
			printf("|  请输入正确值                                      |\n");
			printf("|  输入G/g进入密码生成程序，输入D/d进入结果解密程序  |\n");
			printf("+----------------------------------------------------+\n");
			Direct_Waring_Control++;
			cin >> direct;
		}
	}
	goto Start_option;
Test_Mode:
	Test_Mode_Control = 1;
	PrintTestTitle();
	cin >> Test_Direct;
	if (Test_Direct == "tg")
		goto Generator;
	else if (Test_Direct == "td")
		goto Decryptor;
	else if (Test_Direct == "rt")
	{
		RT_Control = 1;
		goto Rep_Test;
	}
	//密码生成区
Generator:
	G_encr = "";  //加密结果
	final = ""; //结果数组
	printLine(inplat.length() - 6, Test_Mode_Control);
	cout << inplat << endl;
	printLine(inplat.length() - 6, Test_Mode_Control);
	cin >> platform;
	oripla = platform;
	printLine(inaccout.length() - 6, Test_Mode_Control);
	cout << inaccout << endl;
	printLine(inaccout.length() - 6, Test_Mode_Control);
	cin >> account;
	printLine(inname.length() - 6, Test_Mode_Control);
	cout << inname << endl;
	printLine(inname.length() - 6, Test_Mode_Control);
	cin >> name;
	if (name[0] == '0')
		name = "";
	printLine(indate.length() - 6, Test_Mode_Control);
	cout << indate << endl;
	printLine(indate.length() - 6, Test_Mode_Control);
	cin >> date;
	//处理区
	ProcessPf(platform, date);
	ProcessPatch(patch, date);
	datetoStr(dates, date);
	final += platform;
	final += account;
	final += patch;
	encrypt(final, G_encr);
	//日志更新区
	//日志文件初始化
	BlogInit(Test_Mode_Control);
	DataInit();
	extern ofstream fblog; extern ofstream fdata;
	//blog写入
	fblog << oripla << ",";//第一列（平台）
	fblog << account << ",";//第二列（账号）
	fblog << name << ",";//第三列（用户名）
	fblog << G_encr << ",";//第四列（密码加密串）
	fblog << dates << endl;//第五列（日期）
	fblog.close();//安全关闭
	//data写入
	// fdata << oripla << ",";//第一列（平台）
	// fdata << account << ",";//第二列（账号）
	// fdata << name << ",";//第三列（用户名）
	// fdata << G_encr << ",";//第四列（密码加密串）
	// fdata.close();
	//输出区
	printf("+----------------+\n");
	printf("|  最终密码为：  |\n");
	printf("+----------------+\n");
	fopc = final.length();
	printLine(fopc, Test_Mode_Control);
	cout << "|  " << final << "  |" << endl;
	printLine(fopc, Test_Mode_Control);
	if (Test_Mode_Control == 1)
	{
		printLine(G_encr.length() + 8, Test_Mode_Control);
		cout << "|  encr is " << G_encr <<"  |" << endl;
		printLine(G_encr.length() + 8, Test_Mode_Control);
	}
	//继续区
	if (RT_Control == 0)
	{
		next = -1;
		printf("+----------------------------------------------------------------+\n");
		printf("|  输入值以进行对应操作                                          |\n");
		printf("|  按下1继续进行密码生成，按下2进入解密程序，输入其他值结束程序  |\n");
		printf("+----------------------------------------------------------------+\n");
		cin >> next;
		if (next == 1)
			goto Generator;
		else if (next == 2)
			goto Decryptor;
		else
		{
			fblog.close();
			return 0;
		}
	}
	/*else
	{
		RT_Control = 0;
		goto RT_Loop1;
	}*/
	//解密区
Decryptor:
	D_encr = ""; D_pwdc = "";
	printf("+---------------------+\n");
	printf("|  请输入加密后结果:  |\n");
	printf("+---------------------+\n");
	cin >> D_encr;
	decrypt(D_encr, D_pwdc);
	printf("+-----------------+\n");
	printf("|  解密后结果为:  |\n");
	printf("+-----------------+\n");
	fopc = D_pwdc.length();
	printLine(fopc, Test_Mode_Control);
	cout << "|  " << D_pwdc << "  |" << endl;
	printLine(fopc, Test_Mode_Control);
	//继续区
	if (RT_Control == 0)
	{
		next = -1;
		printf("+----------------------------------------------------------------+\n");
		printf("|  输入值以进行对应操作                                          |\n");
		printf("|  按下1继续进行密码生成，按下2进入解密程序，输入其他值结束程序  |\n");
		printf("+----------------------------------------------------------------+\n");
		cin >> next;
		if (next == 1)
			goto Generator;
		else if (next == 2)
			goto Decryptor;
		else
		{
			fblog.close();
			return 0;
		}	
	}
	else
	{
		RT_Control = 0;
		goto RT_Loop2;
	}
Rep_Test:
	NOT = 0; RTmod = "";
	printLine(inNOT.length() - 6, Test_Mode_Control);
	cout << inNOT << endl;
	printLine(inNOT.length() - 6, Test_Mode_Control);
	cin >> NOT;
	printLine(inRTmod1.length() - 6, Test_Mode_Control);
	cout << inRTmod1 << endl << inRTmod2 << endl;
	printLine(inRTmod1.length() - 6, Test_Mode_Control);
	cin >> RTmod;
	
	if (RTmod == "rg")
	{
	RT_Loop1:
		RT_Control = 1;
		if (NOT)
		{
			NOT--;
			goto Generator;
		}
		else
		{
			fblog.close();
			return 0;
		}
	}
	else if (RTmod == "rd")
	{
	RT_Loop2:
		RT_Control = 1;
		if (NOT)
		{
			NOT--;
			goto Decryptor;
		}
		else
		{
			fblog.close();
			return 0;
		}
	}
}

int start_option(char control)
{
	switch (control)
	{
	case 'G':
	case 'g':
		return 1;
	case 'D':
	case 'd':
		return 2;
	case 't':
		return 3;
	default:
		return 4;
	}
}