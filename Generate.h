#ifndef _GENERATET_H_
#define _GENERATET_H_
#include <string>
using namespace std;

void ProcessPf(string& pfname, int& date);//���ڴ���ƽ̨����

void ProcessPatch(string& patch, int& date);//���ɲ�ǿ��

void datetoStr(string& dates, int& date);//������תΪ�ַ���������д����־

void Date_Warning();//������ھ���

bool Date_check(int date);//�������

#endif