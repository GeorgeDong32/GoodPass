#ifndef __CRYPTION_H__
#define __CRYPTION_H__
#include <iostream>
#include <string>
using namespace std;
void encrypt(string& ori, string& final);//用于加密存储密码，便于寻找密码

char numenc(char n, int pos);//对于数字的加密

char charenc(char c, int pos);//对于字母的加密

void findnum(string ori, string & final, int* numpos, int* charpos);//寻找字符串中的数字并标记

void decrypt(string& ori, string & final); //解密函数

char decnum(char n, int pos);//数字解密

char decchar(char c, int pos);//字符解密

void fpnum(string ori, int* npos, int* cpos);//定位数字和字母

#endif