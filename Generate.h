#ifndef __GENERATET_H__
#define __GENERATET_H__
/*********************************************************
*                                                        *
* Generate.h -- Provide password generating functions    *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
* Generate.h version 2.5.2                               *
*                                                        *
**********************************************************/
#include <string>
using namespace std;

void ProcessPf(string& pfname, int& date);//���ڴ���ƽ̨����

void ProcessPatch(string& patch, int& date);//���ɲ�ǿ��

void datetoStr(string& dates, int& date);//������תΪ�ַ���������д����־

void Date_Warning();//������ھ���

bool Date_check(int date);//�������

string Generatepw(string platform, string username);
#endif