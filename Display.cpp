#include "Display.h"
#include <Windows.h>

void SetColor(int mixedcolor)//���ÿ���̨�����ɫ
{
	HANDLE hCon = GetStdHandle(STD_OUTPUT_HANDLE); //��ȡ���������
	SetConsoleTextAttribute(hCon, mixedcolor); //�����ı�������ɫ
}

void addempty(int oril, int tarl)//��ʽ��������ӿո�
{
	int con = tarl - oril;
	while (con)
	{
		cout << " ";
		con--;
	}
}

void PrintTitle(void)//��ӡ����ͷ
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

void PrintTestTitle(void)//��ӡ���Գ���ͷ����ɾ������ģʽ���������ϣ�
{
	cout << "#=================<T>=================#" << endl;
	cout << "#  ��ӭ�㣬����ʦ!   �����ǹ���ģʽ!  #" << endl;
	cout << R"(#  ����<tg>�������ɲ���               #
#  ����<td>���н��ܲ���               #
#  ����<rt>�����ظ�����               #)" << endl;
	cout << "#=================<T>=================#" << endl;
}

void printLine(int len, int mode)//��ӡ�ָ���
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

void printNextO(int mode)//�����һ���˵������ڴ�ɾ����
{
	string opt0 = "|  ��������ֵ������һ��  |"; string opt1 = "|  1����������           |";
	string opt2 = "|  2�������ַ�           |"; string opte = "|  �������뿪            |";
	printLine(20, mode);
	cout << opt0 << endl << opt1 << endl << opt2 << endl << opte << endl;
	printLine(20, mode);
}

void printMenu(int mode)//��ӡ�˵�
{
	string dpt = "��������ֵ�Խ��ж�Ӧ������"; string dp0 = "0/e���뿪";
	string dp1 = "1/a������˺�"; string dp2 = "2/s����ƽ̨�����˺�"; string dp3 = "3/g����ȡָ���˺���Ϣ";
	string dp4 = "4/c���޸�ָ���˺���Ϣ"; string dp5 = "5/d��ɾ��ָ���˺�";
	printLine(27, mode);
	printf("|  ");
	cout << dpt << "   |" << endl;
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

void Displayinf(string d, int pm, int lm, string color)
{
	int cdir = 112;
	if (color == "yellow")
		cdir = 126;
	else if (color == "red")
		cdir = 124;
	else if (color == "green")
		cdir = 122;
	else
		cdir = 112;
	SetColor(cdir);
	int len = d.length();
	if (pm)
	{
		printLine(len - 6, lm);
		cout << d << endl;
		printLine(len - 6, lm);
	}
	else
	{
		printLine(len, lm);
		cout << "|  " << d << "  |" << endl;
		printLine(len, lm);
	}
	SetColor(112);
}

void printDevloping()//��ӡ��������ʾ����Ч��������ɾ����
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

void printmLine(int len, int cp)//��ӡ�������
{
	//int con = len + 4;
	printf("+");
	int flag = cp;
	for (int i = 0; i < len + 4; i++)
	{
		if (i == cp && flag)
			printf("+");
		else
			printf("-");
	}
	printf("+\n");
}
