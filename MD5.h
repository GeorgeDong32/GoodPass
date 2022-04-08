#ifndef __GP_MD5_H__
#define __GP_MD5_H__
/*********************************************************
*                                                        *
* MD5.h -- Provides the MD5 encryption functions         *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
*                                                        *
**********************************************************/
#include <iostream>
#include <string>
using namespace std;

void mainLoop(unsigned int M[]);

unsigned int* add(string str);

string changeHex(int a);

string getMD5(string source);

#endif