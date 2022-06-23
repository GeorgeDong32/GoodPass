/* MKeyProcess.cpp version 2.5.1     */
#include "MKeyProcess.h"
#include <fstream>
#include <sstream>
#include <string>
#include "FileOperate.h"
#include "GPHES.h"
using namespace std;

extern int PI[];
int KEY[40] = { -1,0, };

extern ofstream MKconO; extern ifstream MKconI;

void ProcessKEY(string mk, int* pk)
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
	string MKconpath = "D:\\My Project\\GoodPass\\MData\\MKCheck.config";
	string MKC1 = "|  请输入主密码进行校验  |";
	int Check_con = 0;
MKC_Start:
	MKconI.open(MKconpath, ios::in);
	Displayinf(MKC1, 1, 0, "ori");
	cin >> mk;
	int flag = 1;
	string fSHA; string mkSHA;
	ProcessKEY(mk, KEY);
	mkSHA = gphes(mk);
	MKconI >> fSHA;
	if (mkSHA == fSHA)
	{
		flag = 1;
	}
	else
	{
		flag = 0;
	}
	Check_con++;
	if (flag == 1)
	{
		string MKC_succeed = "|  主密码校验成功！欢迎您  |";
		Displayinf(MKC_succeed, 1, 0, "green");
		cout << "<<<------------------------------------------------------------------------------------>>>" << endl;
	}
	else if (flag == 0)
	{
		if (Check_con > 4)
		{
			SetColor(124);
			cout << "*----------------<!>----------------*" << endl;
			cout << "!  :(                               !" << endl;
			cout << "!  错误次数太多了，好好想想再来吧   !" << endl;
			cout << "*----------------<!>----------------*" << endl;
			SetColor(112);
			system("pause");
			exit(0);
		}
		string MKC_failed = "|  主密码校验失败，请重试  |";
		Displayinf(MKC_failed, 1, 0, "red");
		MKconI.close();
		goto MKC_Start;
	}
}

void setConfig(const string& mk)
{
	string MKconpath = "D:\\My Project\\GoodPass\\MData\\MKCheck.config";
	string newMK = mk; bool setcon = 0;
	if (!MKconO.is_open())
		MKconO.open(MKconpath, ios::out);
	while (setcon == false)
	{
		int MKlen = newMK.length();
		if (MKlen >= 15 && MKlen <= 39)
		{
			string fSHA;
			ProcessKEY(newMK, KEY);
			fSHA = gphes(newMK);
			MKconO << fSHA;
			Displayinf("|  密码符合安全要求,设置成功!  |", 1, 0, "green");
			setcon = true;
		}
		else if (MKlen < 15)
		{
			Displayinf("|  密码长度太短，请设置长度为15~39位的密码  |", 1, 0, "red");
			cin >> newMK;
			setcon = false;
		}
		else if (MKlen > 39)
		{
			Displayinf("|  密码长度太长，请设置长度为15~39位的密码  |", 1, 0, "red");
			cin >> newMK;
			setcon = false;
		}
	}
	if (MKconO.is_open())
		MKconO.close();
}
