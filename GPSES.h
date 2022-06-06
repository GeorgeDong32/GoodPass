#ifndef __GPSES_H__
#define __GPSES_H__
/********************************************************************************
*																			    *
* GPSES.h Provides GoodPass Symmetrical Encryption System						*
* The system contains GPBES(GoodPass Base Encryption System) and AES-256 system *
* Copyright(c) GeorgeDong32(Github).All rights reserved.						*
* GPSES.h version 2.5.0 dev                                                     *
*																			    *
********************************************************************************/
#include <iostream>
#include <string>
using std::string;

class GPBES
{
public:
	GPBES() :in(""), mode(0) {
		numpos = new int[20];
		charpos = new int[30];
	}
	GPBES(string ip, bool md) : in(ip), mode(md) {
		numpos = new int[20];
		charpos = new int[30];
	}
	~GPBES()
	{
		delete[] numpos;
		delete[] charpos;
	}
	void findpos();
	char numenc(char n, int pos);
	char charenc(char c, int pos);
	char numdec(char n, int pos);
	char chardec(char c, int pos);
	string encrypt();
	string decrypt();
	void decrypt_old(string ori, string & final);
private:
	bool mode;//0为加密模式，1为解密模式
	string in;
	int* numpos;
	int* charpos;
};

void fpnum(string ori, int* npos, int* cpos);

#endif