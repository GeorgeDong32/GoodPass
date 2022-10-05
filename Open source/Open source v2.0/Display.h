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

void Displayinf(string display, int printmode, int linemode, string color);//打印带框的文本
//printmode:文本是否带有侧边框，有为1，无为0
//linemode:文本框格式，1为工程模式，0为普通模式

void printDevloping();

void printaddMenu();

void printmLine(int len, int connectpoint);

#endif