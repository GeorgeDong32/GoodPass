#ifndef __GPDATA_H__
#define __GPDATA_H__
/*********************************************************
*                                                        *
* Data.h -- The Data Class for local info process system *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
* Data.h version 2.5.2                                   *
*                                                        *
**********************************************************/
#include <string>
using namespace std;

class Data//密码数据类，链表结构
{
private:
	string platform;//平台
	string account;//账号名
	string encp;//加密的密码
public:
	Data* next;
	Data() :platform("default"), account("default"), encp("default"), next(nullptr) {}
	Data(string in_platform, string in_account, string in_encstring, Data* in_next)
	{
		platform = in_platform; account = in_account; encp = in_encstring; next = in_next;
	}
	Data(string in_platform, string in_account, string in_encstring)
	{
		platform = in_platform; account = in_account; encp = in_encstring; next = NULL;
	}
	void printData();//输出数据
	string getaccount();//输出账号名
	string getplatform();//输出平台名
	string getEncp();//输出加密串
	string getPassword();//输出密码
	bool resetData(string newpassword);//重设密码
	void selfupdate();//数据自更新
};

class Manager//密码管家类，用于管理数据
{
private:
	Data* head;//头节点地址
public:
	Manager() : head(nullptr) {}
	~Manager()
	{
		Data* p = head; Data* temp = p;
		while (p != NULL)
		{
			temp = p;
			if (p->next != NULL)
			{
				p = p->next;
				delete temp;
			}
			else
			{
				delete p;
				break;
			}
		}
	}
	Data* getHead()//获取头节点地址
	{
		return head;
	}
	void fuzzysearch(string platform);//模糊搜索，即搜索功能
	Data* accusearch(string platform, string account, int mode);//精确搜索，即获取功能
	void addData_User(string platform, string account, string password);//用户添加数据函数
	void addData_File(string p, string a, string pw);//从文件初始化数据
	void deleteData(string platform, string account);//删除数据
	void reviseData(string platform, string account);//更改数据，内嵌accusearch
	void reviseData(Data& in_data);//更改数据
	void showData(string platform, string account);//输出指定数据，内嵌accusearch
	void showData(Data& in_data);//输出指定数据
	void dataupdate();//数据版本更新
};

void DataInit(Manager& manager, string Datapath);//管家数据初始化

void FileUpdate(Manager& manager, string Datapath, int mode/*是否有反馈输出*/);//文件数据更新

#endif