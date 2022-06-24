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

class Data//���������࣬����ṹ
{
private:
	string platform;//ƽ̨
	string account;//�˺���
	string encp;//���ܵ�����
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
	void printData();//�������
	string getaccount();//����˺���
	string getplatform();//���ƽ̨��
	string getEncp();//������ܴ�
	string getPassword();//�������
	bool resetData(string newpassword);//��������
	void selfupdate();//�����Ը���
};

class Manager//����ܼ��࣬���ڹ�������
{
private:
	Data* head;//ͷ�ڵ��ַ
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
	Data* getHead()//��ȡͷ�ڵ��ַ
	{
		return head;
	}
	void fuzzysearch(string platform);//ģ������������������
	Data* accusearch(string platform, string account, int mode);//��ȷ����������ȡ����
	void addData_User(string platform, string account, string password);//�û�������ݺ���
	void addData_File(string p, string a, string pw);//���ļ���ʼ������
	void deleteData(string platform, string account);//ɾ������
	void reviseData(string platform, string account);//�������ݣ���Ƕaccusearch
	void reviseData(Data& in_data);//��������
	void showData(string platform, string account);//���ָ�����ݣ���Ƕaccusearch
	void showData(Data& in_data);//���ָ������
	void dataupdate();//���ݰ汾����
};

void DataInit(Manager& manager, string Datapath);//�ܼ����ݳ�ʼ��

void FileUpdate(Manager& manager, string Datapath, int mode/*�Ƿ��з������*/);//�ļ����ݸ���

#endif