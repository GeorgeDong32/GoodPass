#pragma once
#include "FileOperate.h"
#include "PPF_cryption.h"
int KEY[40] = { -1,0, };
extern int PI[];
extern ofstream MKconO;
extern ifstream MKconI;

void setConfig(const string& mk, int mode)
{
	//设置完主密码后将Checko的三个串放入MData\\MainKeyCheck.csv或config
}

void checkConfig(const string& mk)
{
	int flag = 0;
	string Checko1 = "GoodPass001Check1"; string ckeck //
	string Checko2 = "GoodPass002Check2"; string 
	string Checko3 = "Check3GoodPass003"; string 
	ProcessKEY(mk, KEY);
	flag = 
}

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
}