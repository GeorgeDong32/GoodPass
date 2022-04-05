#include "Display.h"

void addempty(int oril, int tarl)//��ʽ��������ӿո�
{
	int con = tarl - oril;
	while (con)
	{
		cout << " ";
		con--;
	}
}

void PrintTitle(void)
{
		extern string version;
		string dp = "||  Welcome to GoodPass "; string dpend = "  ||";
		cout << "*==============================<*>==============================*" << endl;
		cout << dp << version;
		addempty(dp.length() + version.length() + 6, 67);
		cout << dpend << endl;
		cout << "||  Copyright (c)  GeorgeDong32(Github). All rights reserved.  ||" << endl;
		cout << "*==============================<*>==============================*" << endl << endl;
}

void PrintTestTitle(void)
{
		cout << "#=================<T>=================#" << endl;
		cout << "#  ��ӭ�㣬����ʦ!   �����ǹ���ģʽ!  #" << endl;
		cout << R"(#  ����<tg>�������ɲ���               #
#  ����<td>���н��ܲ���               #
#  ����<rt>�����ظ�����               #)" << endl;
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
	string opt0 = "|  ��������ֵ������һ��  |"; string opt1 = "|  1����������           |";
	string opt2 = "|  2�������ַ�           |"; string opte = "|  �������뿪            |";
	printLine(20, mode);
	cout << opt0 << endl << opt1 << endl << opt2 << endl << opte << endl;
	printLine(20, mode);
}

void printMenu(int mode)
{
	string dpt = "��������ֵ�Խ��ж�Ӧ������"; string dp0 = "0/e���뿪";
	string dp1 = "1/a������˺�"; string dp2 = "2/s����ƽ̨�����˺�"; string dp3 = "3/g����ȡָ���˺���Ϣ";
	string dp4 = "4/c���޸�ָ���˺���Ϣ"; string dp5 = "5/d��ɾ��ָ���˺�";
	printLine(27, mode);
	cout << "|  " << dpt << "   |" << endl;
	cout << "|  " << dp0; addempty(dp0.length(), 27);
	cout << "  |" << endl;
	cout << "|  " << dp1; addempty(dp1.length(), 27);
	cout << "  |" << endl;
	cout << "|  " << dp2; addempty(dp2.length(), 27);
	cout << "  |" << endl;
	cout << "|  " << dp3; addempty(dp3.length(), 27);
	cout << "  |" << endl;
	cout << "|  " << dp4; addempty(dp4.length(), 27);
	cout << "  |" << endl;
	cout << "|  " << dp5; addempty(dp5.length(), 27);
	cout << "  |" << endl;
	printLine(27, mode);
}

void Displayinf(string d, int pm, int lm)
{
	if (pm == 1)
	{
		printLine(d.length() - 6, lm);
		cout << d << endl;
		printLine(d.length() - 6, lm);
	}
	else if(pm == 0)
	{
		printLine(d.length(), lm);
		cout << "|  " << d << "  |" << endl;
		printLine(d.length(), lm);
	}
}

void printDevloping()
{
	cout << "/------------------------------+" << endl;
	cout << "|  :)                          |" << endl;
	cout << "|  �¹������ڿ����У������ڴ�  |" << endl;
	cout << "+------------------------------/" << endl;
}

void printaddMenu()
{
	string dis1 = "|  ��ѡ��������뷽ʽ��  |"; string dis2 = "|  m/1���ֶ����         |"; 
	string dis3 = "|  g/2������������       |";
	printLine(20, 0);
	cout << dis1 << endl << dis2 << endl << dis3 << endl;
	printLine(20, 0);
}