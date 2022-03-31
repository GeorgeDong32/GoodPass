#include "Display.h"

void PrintTitle(void)
{

	{
		extern char version[];
		printf("*==============================<*>==============================*\n");
		printf("||  Welcome to GoodPass %s                                 ||\n", version);
		printf("||  Copyright (c)  GeorgeDong32(Github). All rights reserved.  ||\n");
		printf("*==============================<*>==============================*\n");
		printf("\n");
	}
}

void PrintTestTitle(void)
{
	{
		cout << "#=================<T>=================#" << endl;
		cout << "#  ��ӭ�㣬����ʦ!   �����ǹ���ģʽ!  #" << endl;
		cout << R"(#  ����<tg>�������ɲ���               #
#  ����<td>���н��ܲ���               #
#  ����<rt>�����ظ�����               #)" << endl;
		cout << "#=================<T>=================#" << endl;
	}
}

void printLine(int len, int mode)
{

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
}

void printNextO(int mode)
{
	string opt0 = "|  ��������ֵ������һ��  |"; string opt1 = "|  1����������           |";
	string opt2 = "|  2�������ַ�           |"; string opte = "|  �������뿪            |";
	printLine(20, mode);
	cout << opt0 << endl << opt1 << endl << opt2 << endl << opte << endl;
	printLine(20, mode);
}
