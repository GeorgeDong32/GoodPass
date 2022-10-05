#ifndef __GPDISPLAY_H__
#define __GPDISPLAY_H__
/*********************************************************
*                                                        *
* Display.h -- Provides the visual output functions      *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
*                                                        *
**********************************************************/
#include <iostream>
#include <string>
#include <iomanip>
using namespace std;

void SetColor(int mixedcolor);

void addempty(int oril, int tarl);

void PrintTitle(void);

void PrintTestTitle(void);

void printLine(int len, int mode);

void printNextO(int mode);

void printMenu(int mode);

void Displayinf(string display, int printmode, int linemode, string color);//��ӡ������ı�
//printmode:�ı��Ƿ���в�߿���Ϊ1����Ϊ0
//linemode:�ı����ʽ��1Ϊ����ģʽ��0Ϊ��ͨģʽ

void printDevloping();

void printaddMenu();

void printmLine(int len, int connectpoint);

#endif