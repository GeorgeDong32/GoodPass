#ifndef __GPDISPLAY_H__
#define __GPDISPLAY_H__
/*********************************************************
*                                                        *
* Display.h -- Provides the visual output functions      *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
* Display.h version 2.5.0 pre                            *
*                                                        *
**********************************************************/
#include <iostream>
#include <string>
#include <iomanip>
using namespace std;

void SetColor(int mixedcolor);//设置控制台输出颜色

void addempty(int oril, int tarl);//格式函数：添加空格

void PrintTitle(void);//打印程序头

void PrintTestTitle(void);//打印测试程序头（已删除测试模式，函数报废）

void printLine(int len);//打印分割线（线长为len+4，mode为0时输出普通线）

void printNextO(int mode);//输出下一步菜单（过期待删除）

void printMenu(int mode);//打印菜单

void Displayinf(string display, int printmode, int linemode, string color);//打印带框的文本
//printmode:文本是否带有侧边框，有为1，无为0
//linemode:文本框格式，1为工程模式，0为普通模式

void printDevloping();//打印开发中提示

void printaddMenu();//打印开发中提示（无效函数，待删除）

void printmLine(int len, int connectpoint);//打印表格中线

#endif