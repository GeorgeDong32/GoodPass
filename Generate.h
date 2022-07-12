#ifndef __GENERATET_H__
#define __GENERATET_H__
/*********************************************************
*                                                        *
* Generate.h -- Provide password generating functions    *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
* Generate.h version 2.6.0                               *
*                                                        *
**********************************************************/
#include <string>
#include <stdlib.h>
using namespace std;
//character list字符表
char all_list[] =
{
'@', '#', '!', '$', '%', '^', '&', '*', '(', ')',
'-', '_', '+','=', '[', ']', '{', '}', '\\', '|', '\'', '\"', ';', ':',
',', '.', '<', '>', '/', '?', '`', '~', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
};

char ln_list[] =
{
'1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
};

char letter_list[] =
{
'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
};

void ProcessPf(string& pfname, int& date);//用于处理平台名称

void ProcessPatch(string& patch, int& date);//生成补强块

void datetoStr(string& dates, int& date);//将日期转为字符串，便于写入日志

void Date_Warning();//输出日期警告

bool Date_check(int date);//检查日期

string Generatepw(string platform, string username);

int randomNum(int start, int end);

string randomPW(int len, int mode);
#endif