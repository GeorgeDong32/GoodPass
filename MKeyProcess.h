#ifndef __MKEYPROCESS_H__
#define __MKEYPROCESS_H__
/*********************************************************
*                                                        *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
*                                                        *
**********************************************************/
#include <string>
using namespace std;

void ProcessKEY(string mk, int* pk);//主密码生成明文加密数组

void setConfig(const string& mk);

void checkConfig(string& mk);
//设置完主密码后将Checko的三个串放入MData\\MainKeyCheck.csv或config

#endif