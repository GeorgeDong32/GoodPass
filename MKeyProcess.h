#ifndef __MKEYPROCESS_H__
#define __MKEYPROCESS_H__
/*********************************************************
*                                                        *
* MKeyProcess.h -- Provide MainKey-related fuctions      *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
* MKeyProcess.h version 2.5.1                            *
*                                                        *
**********************************************************/
#include <string>
using namespace std;

void ProcessKEY(string mk, int* pk);//�������������ļ�������

void setConfig(const string& mk);

void checkConfig(string& mk);
//�������������Checko������������MData\\MainKeyCheck.csv��config

#endif