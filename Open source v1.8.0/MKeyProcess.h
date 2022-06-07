#pragma once
#include "FileOperate.h"
#include "PPF_cryption.h"
#include "Generate.h"
#include <sstream>
int KEY[40] = { -1,0, };
string Check[4] = { "1", "GoodPass001Check1", "GoodPass002Check2", "Check3GoodPass003" };
extern int PI[]; extern int Test_Mode_Control; extern string MKC1;
extern ofstream MKconO; extern string MKconpath;
extern ifstream MKconI;
int Check_Count = 0;

void ProcessKEY(string mk, int* pk)//主密码生成明文加密数组
{
	int len = mk.length();
	int mid = 0; int kp = 0;
	for (int i = 0; i < len; i++)
	{
		if (mk[i] >= 'a' && mk[i] <= 'z')
		{
			mid = mk[i] - 'A';
			if (mid >= 10)
			{
				*(pk + kp) = mid / 10;
				kp++;
				*(pk + kp) = mid % 10;
			}
			else
			{
				*(pk + kp) = mid;
			}
			kp++;
		}
		else if (mk[i] >= 'A' && mk[i] <= 'Z')
		{
			mid = mk[i];
			if (mid >= 10)
			{
				*(pk + kp) = mid / 10;
				kp++;
				*(pk + kp) = mid % 10;
			}
			else
			{
				*(pk + kp) = mid;
			}
			kp++;
		}
		else if (mk[i] >= '0' && mk[i] <= '9')
		{
			*(pk + kp) = mk[i] - '0';
			kp++;
		}
	}
	if (kp < 40)
	{
		for (; kp < 40; kp++)
		{
			*(pk + kp) = PI[kp];
		}
	}
	/*check
	int ct = 0;
	for (int i = 0; i < 40; i++)
	{
		cout << KEY[i];
		ct++;
		if (ct % 10 == 0)
			cout << endl;
	}*/
}

void checkConfig(string& mk)
{
	int Check_con = 0;
MKC_Start:
		MKconI.open(MKconpath, ios::in);
	printLine(MKC1.length() - 6, Test_Mode_Control);
	cout << MKC1 << endl;
	printLine(MKC1.length() - 6, Test_Mode_Control);
	cin >> mk;
	int flag = 1;
	int i = 0;
	string Mcheck;
	string line, excheck, midcheck;
	ProcessKEY(mk, KEY);
	int l = 18;
	while (getline(MKconI, excheck))
	{
		/*cout << "line " << line << endl;//check
		istringstream sin(line);*/
		//cout << "excheck " << excheck << endl;
		//getline(line, excheck);
		//cout << "excheck new " << excheck << endl;
		decrypt(excheck, Mcheck);
		//cout << "Mcheck " << Mcheck << endl;//check
		if (Mcheck == Check[i])
			flag *= 1;
		else
			flag *= 0;
		excheck = "";
		//cout << "flag is " << flag << endl;//check
		Mcheck = "";
		i++;
	}
	Check_con++;
	if (flag == 1)
	{
		string MKC_succeed = "|  主密码校验成功！欢迎您  |";
		printLine(MKC_succeed.length() - 6, 0);
		cout << MKC_succeed << endl;
		printLine(MKC_succeed.length() - 6, 0);
		cout << "<<<------------------------------------------------------------------------------------>>>" << endl;
	}
	else if (flag == 0)
	{
		if (Check_con > 4)
		{
			cout << "*----------------<!>----------------*" << endl;
			cout << "!  :)                               !" << endl;
			cout << "!  错误次数太多了，好好想想再来吧   !" << endl;
			cout << "*----------------<!>----------------*" << endl;
			system("pause");
			exit(0);
		}
		string MKC_failed = "|  主密码校验失败，请重试  |";
		printLine(MKC_failed.length()-6, 0);
		cout << MKC_failed << endl;
		printLine(MKC_failed.length()-6, 0);
		MKconI.close();
		goto MKC_Start;
	}
}

void setConfig(const string& mk)//设置完主密码后将Checko的三个串放入MData\\MainKeyCheck.csv或config
{
	if (!MKconO.is_open())
		MKconO.open(MKconpath, ios::out);
	string mid;
	ProcessKEY(mk, KEY);
	for (int i = 0; i < 4; i++)
	{
		mid = "";
		encrypt(Check[i], mid);
		MKconO << mid;
		if (i < 3)
			MKconO << endl;
	}
}