/* FileOperate.cpp version 2.6.0     */
#include "FileOperate.h"
#include <direct.h>
#include <io.h>
#include <Windows.h>
#include "Display.h";
ofstream fblog; ofstream fdata;
ofstream MKconO;
ifstream MKconI;

void FloderInit(int mode)
{
	string L0path = "D:\\My Project";
	string L1fpath = "D:\\My Project\\GoodPass";
	string L2Bpath = "D:\\My Project\\GoodPass\\Blog";
	string L2Dpath = "D:\\My Project\\GoodPass\\MData";
	if (mode)
		printLine(33);
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
		printLine(33);
}

void BlogInit(int mode)
{
	string blogpath = "D:\\My Project\\GoodPass\\Blog\\GeneratorBlog.csv";
	fblog.open(blogpath, ios_base::app);
}

int MKconInit()
{
	string MKconpath = "D:\\My Project\\GoodPass\\MData\\MKCheck.config";
	if (!_access(MKconpath.c_str(), 00))//检测配置文件是否存在
	{
		MKconI.open(MKconpath, ios::in);
		char check;
		if (MKconI >> check)
		{
			MKconI.close();
			return 1;
		}
		else
		{
			SetColor(14);
			printf("+-------------------<!>------------------+\n");
			printf("!  :(                                    !\n");
			printf("!  Error! MainKey config has a problem!  !\n");
			printf("+-------------------<!>------------------+\n");
			SetColor(15);
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

void DataInit()
{
	string MDpath = "D:\\My Project\\GoodPass\\MData\\Data.csv";
	fdata.open(MDpath, ios::app);
}
