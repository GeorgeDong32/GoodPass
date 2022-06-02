#include "Display.h"
#include <Windows.h>

void SetColor(int mixedcolor)//设置控制台输出颜色
{
	HANDLE hCon = GetStdHandle(STD_OUTPUT_HANDLE); //获取缓冲区句柄
	SetConsoleTextAttribute(hCon, mixedcolor); //设置文本及背景色
}

void addempty(int oril, int tarl)//格式函数：添加空格
{
	int con = tarl - oril;
	while (con)
	{
		cout << " ";
		con--;
	}
}

void PrintTitle(void)//打印程序头
{
	extern string version;
	string dp = "||  Welcome to GoodPass "; string dpend = "  ||";
	cout << "*==============================<*>==============================*" << endl;
	cout << dp << version;
	addempty(dp.length() + version.length() + 6, 67);
	cout << dpend << endl;
	cout << "||  Copyright (c)  GeorgeDong32(Github). All rights reserved.  ||" << endl;
	cout << "*==============================<*>==============================*" << endl << endl;
}

void PrintTestTitle(void)//打印测试程序头（已删除测试模式，函数报废）
{
	cout << "#=================<T>=================#" << endl;
	cout << "#  欢迎你，工程师!   现在是工程模式!  #" << endl;
	cout << R"(#  输入<tg>进行生成测试               #
#  输入<td>进行解密测试               #
#  输入<rt>进行重复测试               #)" << endl;
	cout << "#=================<T>=================#" << endl;
}

void printLine(int len, int mode)//打印分割线
{
	int con = len + 4;
	if (mode)
	{
		printf("#");
		while (con)
		{
			printf("-");
			con--;
		}
		printf("#\n");
	}
	else {
		printf("*");
		while (con)
		{
			printf("-");
			con--;
		}
		printf("*\n");
	}
}

void printNextO(int mode)//输出下一步菜单（过期待删除）
{
	string opt0 = "|  输入以下值进行下一步  |"; string opt1 = "|  1：生成密码           |";
	string opt2 = "|  2：解密字符           |"; string opte = "|  其他：离开            |";
	printLine(20, mode);
	cout << opt0 << endl << opt1 << endl << opt2 << endl << opte << endl;
	printLine(20, mode);
}

void printMenu(int mode)//打印菜单
{
	string dpt = "输入以下值以进行对应操作："; string dp0 = "0/e：离开";
	string dp1 = "1/a：添加账号"; string dp2 = "2/s：按平台搜索账号"; string dp3 = "3/g：获取指定账号信息";
	string dp4 = "4/c：修改指定账号信息"; string dp5 = "5/d：删除指定账号";
	printLine(27, mode);
	printf("|  ");
	cout << dpt << "   |" << endl;
	cout << "|  " << dp0; addempty(dp0.length(), 27);
	cout << "  |" << endl;
	cout << "|  " << dp1; addempty(dp1.length(), 27);
	cout << "  |" << endl;
	cout << "|  " << dp2; addempty(dp2.length(), 27);
	cout << "  |" << endl;
	cout << "|  " << dp3; addempty(dp3.length(), 27);
	cout << "  |" << endl;
	cout << "|  " << dp4; addempty(dp4.length(), 27);
	cout << "  |" << endl;
	cout << "|  " << dp5; addempty(dp5.length(), 27);
	cout << "  |" << endl;
	printLine(27, mode);
}

void Displayinf(string d, int pm, int lm, string color)
{
	int cdir = 112;
	if (color == "yellow")
		cdir = 126;
	else if (color == "red")
		cdir = 124;
	else if (color == "green")
		cdir = 122;
	else
		cdir = 112;
	SetColor(cdir);
	int len = d.length();
	if (pm)
	{
		printLine(len - 6, lm);
		cout << d << endl;
		printLine(len - 6, lm);
	}
	else
	{
		printLine(len, lm);
		cout << "|  " << d << "  |" << endl;
		printLine(len, lm);
	}
	SetColor(112);
}

void printDevloping()//打印开发中提示（无效函数，待删除）
{
	cout << "/------------------------------+" << endl;
	cout << "|  :)                          |" << endl;
	cout << "|  新功能正在开发中！敬请期待  |" << endl;
	cout << "+------------------------------/" << endl;
}

void printaddMenu()
{
	string dis1 = "|  请选择添加密码方式：  |"; string dis2 = "|  m/1：手动添加         |";
	string dis3 = "|  g/2：生成器生成       |";
	printLine(20, 0);
	cout << dis1 << endl << dis2 << endl << dis3 << endl;
	printLine(20, 0);
}

void printmLine(int len, int cp)//打印表格中线
{
	//int con = len + 4;
	printf("+");
	int flag = cp;
	for (int i = 0; i < len + 4; i++)
	{
		if (i == cp && flag)
			printf("+");
		else
			printf("-");
	}
	printf("+\n");
}
