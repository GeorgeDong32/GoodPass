#include "PPF_cryption.h"
extern int KEY[];
//加密组
void encrypt(string& ori, string & final)
{
	//定义区
	int numpos[31] = { 0, };//数字位置
	int charpos[31] = { 0, };//字母位置
	int np = 0; int cp = 0;
	string mid = ori;
	//加密区
	findnum(ori, final, numpos, charpos);
	for (int i = 1; i < numpos[0]; i++)
	{
		np = numpos[i];
		mid[np] = numenc(ori[np], np);
	}
	for (int i = 1; i < charpos[0]; i++)
	{
		cp = charpos[i];
		mid[cp] = charenc(ori[cp], cp);
	}
	final += mid;
	//检查区
	//cout << "mid is " << mid << endl << "final is " << final << endl;
}

void findnum(string ori, string & final, int* numpos, int* charpos)
{
	int len = 0; int fcon = 1; int i = 0; int np = 1; int cp = 1;
	len = ori.length(); //初始数组长
	final += '#';
	for (i = 0; i < len; i++)
	{
		if (ori[i] >= '0' && ori[i] <= '9')
		{
			*(numpos + np) = i;
			final += 'A' + i;
			fcon++; np++;
			*(numpos) = np;
		}
		else if (ori[i] != '@')
		{
			*(charpos + cp) = i;
			cp++;
			*(charpos) = cp;
		}
	}
	final += '#';
	fcon++;
}//checked

char numenc(char n, int pos)
{
	char re;
	if (pos % 2 == 0)
	{
		re = 'e' + n - '0' + KEY[pos];
	}
	else
	{
		re = 'O' + n - '0' + KEY[pos];
	}
	return re;
}//applies to strings

char charenc(char c, int pos)
{
	char re;
	if (c >= 'a' && c <= 'z')
		re = c + 'A' - 'a';
	else
		re = c - 'A' + 'a';
	re += KEY[pos];
	return re;
}//applies to strings

//解密组
void decrypt(string& ori, string & final)
{
	int numpos[31] = { 0, }; int charpos[31] = { 0, };//字母和位置数组
	string mid;
	int len = ori.length();//原字符串长
	int pl = 0; int mdp = 0;
	fpnum(ori, numpos, charpos);
	pl = numpos[0] + 1;
	int fl = len - pl;
	for (int i = 0; i < fl; i++)
	{
		mid += '0';
	}
	for (int i = pl; i < len; i++)
	{
		mid[mdp] = ori[i];
		mdp++;
	}
	for (int i = 1; i < numpos[0]; i++)
	{
		int np = numpos[i];
		mid[np] = decnum(mid[np], np);
	}
	for (int i = 1; i < charpos[0]; i++)
	{
		int cp = charpos[i];
		mid[cp] = decchar(mid[cp], cp);
	}
	final += mid;
	//cout << "final is " << final << endl;
}

char decnum(char n, int pos)
{
	char re;
	if (pos % 2 == 0)
		re = n - KEY[pos] + '0' - 'e';
	else
		re = n - 'O' + '0' - KEY[pos];
	return re;
}

char decchar(char c, int pos)
{
	char re;
	if (c >= 'a' && c <= 'z')
		re = c + 'A' - 'a';
	else
		re = c - 'A' + 'a';
	re -= KEY[pos];
	return re;
}//applies to strings

void fpnum(string ori, int* npos, int* cpos)
{
	int p = 0; int np = 1; int flag = 0; int len = 0; int cp = 1;
	char check; int nlen = 0; int rp = 0;
	len = ori.length();
	for (int i = 0; i < len; i++)
	{
		check = ori[i];
		if (flag <= 1)
		{
			if (check == '#')
				flag++;
			if (check <= 'Z' && check >= 'A')
			{
				*(npos + np) = check - 'A';
				np++;
			}
			nlen = np;
			*(npos) = np;
		}
	}
	for (int i = nlen + 1; i < len; i++)//找字母
	{
		flag = 0; check = ori[i];
		rp = i - nlen - 1;
		for (int j = 1; j < nlen; j++)
		{
			if (rp == npos[j])
				flag = 1;
		}
		if (flag != 1 && check != '@')
		{
			*(cpos + cp) = rp;
			cp++;
		}
		*(cpos) = cp;
	}
}