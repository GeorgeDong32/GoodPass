/* GPSES.cpp version 2.5.0 pre */
#include "GPSES.h"
extern int KEY[40];

void GPBES::findpos()
{
	int len = in.length();
	int i = 0;
	int np = 1; int cp = 1;
	numpos[0] = 0; charpos[0] = 0;
	switch (mode)
	{
	case 0:
		goto enc_fn;
		break;
	case 1:
		goto dec_fn;
		break;
	}
enc_fn:
	for (i = 0; i < len; i++)
	{
		if (in[i] <= '9' && in[i] >= '0')
		{
			numpos[0]++;
			numpos[np] = i;
			np++;
		}
		else
		{
			charpos[0]++;
			charpos[cp] = i;
			cp++;
		}
	}
	return;
dec_fn:
	int	decl = 0;
	int	npl = 0;
	string mid;
	cp = 1; np = 1;
	charpos[0] = 0; numpos[0] = 0;
	if (in[0] != '#')//新模式
	{
		npl = in[0] - KEY[0] - 'A';
		numpos[0] = npl;
		for (int i = 1; i <= npl; i++)
			in[i] -= KEY[i];
		for (int i = 1; i <= npl; i++)
		{
			if (in[i] <= 'z' && in[i] >= 'a')
			{
				numpos[i] = in[i] - 'a';
			}
			else
			{
				numpos[i] = in[i] - 'A';
			}
		}
		len = in.length();
		int rsl = len - npl - 1;
		int flag = 0;
		for (int i = 0; i < rsl; i++)
		{
			flag = 0;
			for (int j = 1; j <= numpos[0]; j++)
			{
				if (i == numpos[j])
					flag = 1;
			}
			if (flag == 0)
			{
				charpos[cp] = i;
				cp++;
				charpos[0]++;
			}
		}
	}
	return;
}

char GPBES::numenc(char n, int pos)
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
}

char GPBES::charenc(char c, int pos)
{
	char re;
	if (c >= 'a' && c <= 'z')
		re = c + 'A' - 'a';
	else
		re = c - 'A' + 'a';
	re += KEY[pos];
	return re;
}

char GPBES::numdec(char n, int pos)
{
	char re;
	if (pos % 2 == 0)
		re = n - KEY[pos] + '0' - 'e';
	else
		re = n - 'O' + '0' - KEY[pos];
	return re;
}

char GPBES::chardec(char c, int pos)
{
	char re;
	if (c >= 'A' && c < '`')
		re = c + 'a' - 'A';
	else
		re = c + 'A' - 'a';
	re -= KEY[pos];
	return re;
}

string GPBES::encrypt()
{
	string head; string final; string mid = in;
	//重生成位置数组
	findpos();
	head = "A";
	head[0] = 65 + numpos[0];
	//生成加密串头
	for (int i = 1; i <= numpos[0]; i++)
	{
		if (numpos[i] % 2 == 0)
		{
			head += 'A' + numpos[i];
		}
		else
		{
			head += 'a' + numpos[i];
		}
	}
	//加密原字符串
	for (int i = 1; i <= numpos[0]; i++)
	{
		int p = numpos[i];
		mid[p] = numenc(mid[p], p);
	}
	for (int i = 1; i <= charpos[0]; i++)
	{
		int p = charpos[i];
		mid[p] = charenc(mid[p], p);
	}
	//加密串头
	for (int i = 0; i < numpos[0] + 1; i++)
	{
		head[i] += KEY[i];
	}
	final = head + mid;
	return final;
}

string GPBES::decrypt()
{
	string mid;
	char check = in[0];
	if (check == 35)
	{
		decrypt_old(in, mid);
		return mid;
	}
	//重生成位置数组
	findpos();
	int len = 0; int rsl = 0; int hl = numpos[0] + 1;
	len = in.length();
	rsl = len - hl;
	for (int i = 0; i < rsl; i++)
	{
		mid += 'X';
	}
	for (int i = hl; i < len; i++)
	{
		mid[i - hl] = in[i];
	}
	for (int i = 1; i <= numpos[0]; i++)
	{
		int p = numpos[i];
		mid[p] = numdec(mid[p], p);
	}
	for (int i = 1; i <= charpos[0]; i++)
	{
		int p = charpos[i];
		mid[p] = chardec(mid[p], p);
	}
	return mid;
}

void GPBES::decrypt_old(string ori, string & final)
{
	//int numpos[31] = { 0, }; int charpos[31] = { 0, };//字母和位置数组
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
		mid[np] = numdec(mid[np], np);
	}
	for (int i = 1; i < charpos[0]; i++)
	{
		int cp = charpos[i];
		mid[cp] = chardec(mid[cp], cp);
	}
	final += mid;
}

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