#ifndef __CRYPTION_H__
#define __CRYPTION_H__
#include <iostream>
#include <string>
using namespace std;
void encrypt(string& ori, string& final);//���ڼ��ܴ洢���룬����Ѱ������

char numenc(char n, int pos);//�������ֵļ���

char charenc(char c, int pos);//������ĸ�ļ���

void findnum(string ori, string & final, int* numpos, int* charpos);//Ѱ���ַ����е����ֲ����

void decrypt(string& ori, string & final); //���ܺ���

char decnum(char n, int pos);//���ֽ���

char decchar(char c, int pos);//�ַ�����

void fpnum(string ori, int* npos, int* cpos);//��λ���ֺ���ĸ

#endif