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

void ProcessPf(string& pfname, int& date);//用于处理平台名称

void ProcessPatch(string& patch, int& date);//生成补强块

void datetoStr(string& dates, int& date);//将日期转为字符串，便于写入日志

void Date_Warning();//输出日期警告

bool Date_check(int date);//检查日期

string Generatepw(string platform, string username);
#endif