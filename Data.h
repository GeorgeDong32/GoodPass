#ifndef __GPDATA_H__
#define __GPDATA_H__
/*********************************************************
*                                                        *
* Data.h -- The Data Class for local info process system *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
*                                                        *
**********************************************************/
#include <iostream>
#include <string>
using namespace std;
class Data
{
private:
	string platform;
	string account;
	string encp;
	//int linepos;
public:
	Data* next;
	Data()
	{
		platform = "default";
		account = "default";
		encp = "default";
		next = NULL;
		//linepos = -1;
	}
	Data(string in_platform, string in_account, string in_encstring, Data* in_next)
	{
		platform = in_platform; account = in_account; encp = in_encstring; next = in_next;
	}
	Data(string in_platform, string in_account, string in_encstring)
	{
		platform = in_platform; account = in_account; encp = in_encstring; next = NULL;
	}
	void printData();
	//int getpos();
	string getaccount();
	string getplatform();
	string getEncp();
	string getPassword();
	bool resetData(string newpassword);
};

class Manager
{
private:
	//int total;
	Data* head;
public:
	Manager()
	{
		//total = 0;
		head = NULL;
	}
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
	Data* getHead()
	{
		return head;
	}
	void fuzzysearch(string platform);
	Data* accusearch(string platform, string account, int mode);
	void addData_User(string platform, string account, string password);
	void addData_File(string p, string a, string pw);
	void deleteData(string platform, string account);
	void reviseData(string platform, string account);
	void reviseData(Data& in_data);
	void showData(string platform, string account);
	void showData(Data& in_data);
};

void DataInit(Manager& manager, string Datapath);

void FileUpdate(Manager& manager, string Datapath);

#endif