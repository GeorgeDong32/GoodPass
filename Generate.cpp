/* Generate.cpp version 2.5.1     */
#include "Generate.h"
#include "GPBase.h"
#include "GPSES.h"
#include <fstream>
#include <string>
using namespace std;
extern int PI[];

void Date_Warning()
{
	cout << "+-----------------<!>----------------+" << endl;
	cout << "!  日期输入不正确，请检查后重新输入  !" << endl;
	cout << "+-----------------<!>----------------+" << endl;
}

bool Date_check(int date)
{
	int Date_Check = 0;
	while (date != 0)
	{
		date /= 10;
		Date_Check++;
	}
	if (Date_Check != 8)
		return true;
	else
		return false;
}

void ProcessPatch(string& patch, int& date)
{
	//cout << "in ProcessPatch" << endl;
fPP_Start:
	patch = "aaaa";
	int pd1 = 0; int pd2 = 0;
	int pn1 = 0; int pn2 = 0;
	int datec = date;
	pd2 = (datec + PI[datec % 10]) % 10;
	if (Date_check(date))
	{
		Date_Warning();
		cin >> date;
		goto fPP_Start;
	}
	pn1 = PI[pd1];
	pn2 = pd2 % 3;
	patch[0] = '0' + pn1;
	patch[1] = '0' + pn2;
	patch[2] = 'A' + pd2;
	if (pd1 >= 26)
		patch[3] = 'A' + 6 + pd1;
	else
		patch[3] = 'A' + pd1;
	//cout << "Patch: " << patch << endl;
	//cout << "out ProcessPatch" << endl;
}

void datetoStr(string& dates, int& date)
{
	//cout << "in datetoStr" << endl;
FdtS_Start:
	int cp = 7;
	if (Date_check(date))
	{
		Date_Warning();
		cin >> date;
		goto FdtS_Start;
	}
	while (date != 0)
	{
		dates[cp] = date % 10 + '0';
		date /= 10;
		cp--;
	}
	//cout << "datestr: " << dates << endl;
	//cout << "out datetoStr" << endl;
}

void ProcessPf(string& pfname, int& date)
{
	//cout << "in ProcessGf" << endl;
fPPf_Start:
	//定义区
	int pp1 = 0; int pp2 = 0; int temp1 = date; int temp2 = date;
	int sl = pfname.length();
	int sum = 0; int Time_Out = 1;
	//日期检查区
	if (Date_check(date))
	{
		Date_Warning();
		cin >> date;
		goto fPPf_Start;
	}
	//大写定位区
	while (temp2 != 0)
	{
		sum += temp2 % 10;
		temp2 /= 10;
	}
	pp2 = PI[sum];
	pp1 = temp1 % sl;
	while (pp2 > sl)
	{
		pp2 -= sl;
		if (Time_Out >= 3)
			break;
	}
	//检查区
	/*cout << "pp1 = " << pp1 << endl;
	cout << "pp2 = " << pp2 << endl;*/
	//大写改写区
	pfname[pp1] = pfname[pp1] - 'a' + 'A';
	if (pfname[pp2] > 'a' && pfname[pp2] < 'z')
		pfname[pp2] += 'A' - 'a';
	//cout << "pfname: " << pfname << endl;
	//cout << "date: " << date << endl;
	//cout << "out ProcessPf" << endl;
}

string Generatepw(string pl, string un)
{
	string dates = Getsystime();
	string G_encr = "";  //加密结果
	string final = ""; //结果数组
	string patch;//密码加强补丁
	string oripla = pl;
	string name;
	if (un.length() <= 4)
	{
		name = un;
	}
	else
	{
		name = "!!!!";
		for (int i = 0; i < 4; i++)
		{
			name[i] = un[i];
		}
	}
	int date = 0;
	//日期转换
	for (int i = 0; i < 8; i++)
	{
		date += (dates[i] - '0') * pow(10, 7 - i);
	}
	//处理区
	ProcessPf(pl, date);
	ProcessPatch(patch, date);
	final += pl;
	final += name;
	final += patch;
	GPBES gpg(final, 0);
	G_encr = gpg.encrypt();
	//日志更新区
	//日志文件初始化
	ofstream blog;
	string blogp = "D:\\My Project\\GoodPass\\Blog\\GeneratorBlog.csv";
	blog.open(blogp, ios_base::app);
	//blog写入
	blog << oripla << ",";//第一列（平台）
	blog << un << ",";//第二列（账号）
	blog << name << ",";//第三列（用户名）
	blog << G_encr << ",";//第四列（密码加密串）
	blog << dates << endl;//第五列（日期）
	blog.close();//安全关闭
	return final;
}