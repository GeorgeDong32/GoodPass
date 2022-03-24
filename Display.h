#ifndef _GPDISPLAY_H_
#define _GPDISPLAY_H_

void PrintTitle(void)
{
	extern char version[];
	printf("*==============================<*>==============================*\n");
	printf("||  Welcome to GoodPass %s                                 ||\n", version);
	printf("||  Copyright (c)  GeorgeDong32(Github). All rights reserved.  ||\n");
	printf("*==============================<*>==============================*\n");
	printf("\n");
}

void PrintTestTitle(void)
{
	cout << "#=================<T>=================#" << endl;
	cout << "#  欢迎你，工程师!   现在是工程模式!  #" << endl;
	cout << R"(#  输入<tg>进行生成测试               #
#  输入<td>进行解密测试               #
#  输入<rt>进行重复测试               #)" << endl;
	cout << "#=================<T>=================#" << endl;
}

void printLine(int len, int mode)
{
	int con = len + 4;
	if (mode)
	{
		printf("#");
		while (con)
		{
			printf("-");
			con--;
		}
		printf("#\n");
	}
	else {
		printf("*");
		while (con)
		{
			printf("-");
			con--;
		}
		printf("*\n");
	}
}

void printNextO(int mode)
{
	string opt0 = "|  输入以下值进行下一步  |"; string opt1 = "|  1：生成密码           |";
	string opt2 = "|  2：解密字符           |"; string opte = "|  其他：离开            |";
	printLine(21, mode);
	cout << opt0 << endl << opt1 << endl << opt2 << endl << opte << endl;
	printLine(21, mode);
}

#endif