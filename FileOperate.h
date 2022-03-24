#pragma once
#include <iostream>
#include <fstream>
#include <direct.h>
#include <io.h>
#include <string>
#include "Display.h"
using namespace std;
ofstream fblog; ofstream fdata;
ofstream MKconO;
ifstream MKconI;
string L0path = "D:\\My Project";
string L1fpath = "D:\\My Project\\GoodPass";
string L2Bpath = "D:\\My Project\\GoodPass\\Blog";
string blogpath = "D:\\My Project\\GoodPass\\Blog\\GeneratorBlog.csv";
string L2Dpath = "D:\\My Project\\GoodPass\\MData";
string MKconpath = "D:\\My Project\\GoodPass\\MData\\MKCheck.config";
string MDpath = "D:\\My Project\\GoodPass\\MData\\Data.csv";

void FloderInit(int mode)
{
	if (mode)
		printLine(33, mode);
	if (_access(L0path.c_str(), 00))//检测文件夹是否存在
	{
		int flag = _mkdir(L0path.c_str());//创建文件夹
		if (flag == 0 && mode == 1)
			cout << "|  My Project floder created          |" << endl;
		else if (mode == 1)
			cout << "|  Failed to create L0        floder  |" << endl;
	}
	else if (mode == 1)
		cout << "|  The My Project floder exists       |" << endl;
	if (_access(L1fpath.c_str(), 00))//检测文件夹是否存在
	{
		int flag = _mkdir(L1fpath.c_str());//创建文件夹
		if (flag == 0 && mode == 1)
			cout << "|  GoodPass floder created            |" << endl;
		else if (mode == 1)
			cout << "|  Failed to create GoodPass floder  |" << endl;
	}
	else if (mode == 1)
		cout << "|  The GoodPassM floder exists        |" << endl;
	if (_access(L2Bpath.c_str(), 00))
	{
		int flag = _mkdir(L2Bpath.c_str());
		if (flag == 0 && mode == 1)
			cout << "|  Blog floder created                |" << endl;
		else if (mode == 1)
			cout << "|  Failed to create Blog floder       |" << endl;
	}
	else if (mode == 1)
		cout << "|  The Blog floder exists             |" << endl;
	if (_access(L2Dpath.c_str(), 00))
	{
		int flag = _mkdir(L2Dpath.c_str());
		if (flag == 0 && mode == 1)
			cout << "|  Data floder created                |" << endl;
		else if (mode == 1)
			cout << "|  Failed to create Data floder       |" << endl;
	}
	else if (mode == 1)
		cout << "|  The Data floder exists             |" << endl;
	if (mode)
		printLine(33, mode);
}

void BlogInit(int mode)
{
	fblog.open(blogpath, ios_base::app);
}

void DataInit()
{
	fdata.open(MDpath, ios::app);
}

int MKconInit()
{
	if (!_access(MKconpath.c_str(), 00))//检测配置文件是否存在
	{
		MKconI.open(MKconpath, ios::in);
		char check;
		MKconI >> check;
		if (check != NULL)
		{
			MKconI.close();
			return 1;
		}
		else
		{
			cout << "+-------------------<!>------------------+" << endl;
			cout << "!  :)                                    !" << endl;
			cout << "!  Error! MainKey config has a problem!  !" << endl;
			cout << "+-------------------<!>------------------+" << endl;
			MKconI.close();
		}
		return 2;
	}
	else
	{
		MKconO.open(MKconpath, ios::out);
		return 0;
	}
}