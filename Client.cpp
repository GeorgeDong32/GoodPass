//Code Information
/***********************************************************
*                                                          *
* <GoodPass Password Manager>                              *
* Written by GeorgeDong32                                  *
* Copyright (c) GeorgeDong32(Github). All rights reserved. *
*                                                          *
***********************************************************/
//������־
//��"CodeBlog.h"

//����&ͷ�ļ�
#include "Generate.h"
#include "FileOperate.h"
#include "Data.h"
#include "MKeyProcess.h"
#include "Display.h"
#include "GPBase.h"
int Test_Mode_Control = 0;//����ģʽ���ط�

string version = "2.3.0";//���°汾��!

//���ܻ���
int PI[40] = { 1,4,1,5,9,2,6,5,3,5,8,9,7,9,3,2,3,8,4,6,2,6,4,3,3,8,3,2,7,9,5,0,2,8,8,4,1,9,7,1 };
/*string  inplat = "|  ����ƽ̨���ƣ� |"; string inaccout = "|  �����˺����� |";
string indate = "|  �������ڣ�  |"; string inRTmod2 = "|  ����<rg>��������,<rd>���Խ���  |";
string inRTmod1 = "| ������ѭ��ģʽ                  |"; string inNOT = "|  �������������  |";
string inname = "|  �����û�����(��ѡ)������0������  |";*/
string MKC0 = "|  ����������������  |"; string MKC2 = "|  ����������������  |";

int main(void)
{
	system("color 70");//���ÿ���̨��ɫ��7Ϊ������0Ϊ����
	//������
	string name, account, platform, patch, oripla, RTmod;
	string MDpath = "D:\\My Project\\GoodPass\\MData\\Data.csv";
	string G_encr;  //���ɽ׶μ��ܽ��
	string final; //�������
	string Test_Direct;//����ģʽ�����
	string D_encr; string D_pwdc;//���ܽ׶��ַ���
	string MainKey;
	int namel = 0; int rek = 0; int date = 0; int fopc = 0;//������Ʒ�
	int RT_Control = 0;//ѭ�����Կ��Ʒ�
	int ipc = 1; //����������
	int ipfl = 0; int NOT = 0;//NOTΪѭ���������Ʒ�
	char direct = '0';  //��ʼ�����жϷ�
	int next = -1;  //���������жϷ�
	string dates = "00000000";
	char opt = '@';
	//����ͷ��ӡ��
	PrintTitle();
	//���������ü��
	int mkc = MKconInit();
	FloderInit(Test_Mode_Control);//�ļ��г�ʼ��
	switch (mkc)
	{
	case 2:
		Displayinf(MKC2, 1, 0, "ori");
		cin >> MainKey;
		setConfig(MainKey);
		break;
	case 1:
		checkConfig(MainKey);
		break;
	case 0:
		Displayinf(MKC0, 1, 0, "ori");
		cin >> MainKey;
		setConfig(MainKey);
		break;
	}
	//Manager��ʼ��
	Manager gpm;
	DataInit(gpm, MDpath);
	gpm.dataupdate();
	printMenu(Test_Mode_Control);
	while (cin >> opt)
	{
		switch (opt)
		{
		case 'e':
		case '0':
			FileUpdate(gpm, MDpath);
			gpm.~Manager();
			system("pause");
			exit(0);
			break;
		case 'a':
		case '1':
			GP_add(gpm);
			break;
		case 's':
		case '2':
			GP_search(gpm);
			break;
		case 'g':
		case '3':
			GP_get(gpm);
			break;
		case 'c':
		case '4':
			GP_change(gpm);
			break;
		case 'd':
		case '5':
			GP_delete(gpm);
			break;
		default:
			Displayinf("������������", 0, 0, "yellow");
			break;
		}
		printMenu(Test_Mode_Control);
	}
	FileUpdate(gpm, MDpath);//���ݱ���
	gpm.~Manager();
	return 0;
	/*��ʼ������
	printf("+---------------------------+\n");
	printf("|  ����G/g�����������ɳ���  |\n");
	printf("|  ����D/d���������ܳ���  |\n");
	printf("+---------------------------+\n");
	cin >> direct;
Start_option:
	int Option = start_option(direct);
	switch(Option)
	{
	case 1:
		Test_Mode_Control = 0;
		//�ļ��г�ʼ��
		FloderInit(Test_Mode_Control);
		goto Generator; break;
	case 2:
		Test_Mode_Control = 0;
		//�ļ��г�ʼ��
		FloderInit(Test_Mode_Control);
		goto Decryptor; break;
	case 3:
		//�ļ��г�ʼ��
		FloderInit(Test_Mode_Control);
		goto Test_Mode; break;
	case 4:
		Test_Mode_Control = 0;
		//�ļ��г�ʼ��
		FloderInit(Test_Mode_Control);
		goto Direct_Error; break;
	}
Direct_Error:
	while (direct != 'G' && direct != 'g' && direct != 'D' && direct != 'd' && direct != 't')
	{
		int Direct_Waring_Control = 0;//�����������
		ipfl = direct != 'G' && direct != 'g' && direct != 'D' && direct != 'd';
		ipc++;
		if (ipc > 1 && ipfl == 1 && Direct_Waring_Control < 1)
		{
			printf("+---------------------------+\n");
			printf("|  ��������ȷֵ             |\n");
			printf("|  ����G/g�����������ɳ���  |\n");
			printf("|  ����D/d���������ܳ���  |\n");
			printf("+---------------------------+\n");
			Direct_Waring_Control++;
			cin >> direct;
		}
	}
	goto Start_option;
Test_Mode:
	Test_Mode_Control = 1;
	PrintTestTitle();
	cin >> Test_Direct;
	if (Test_Direct == "tg")
		goto Generator;
	else if (Test_Direct == "td")
		goto Decryptor;
	else if (Test_Direct == "rt")
	{
		RT_Control = 1;
		goto Rep_Test;
	}
	//����������
Generator:
	G_encr = "";  //���ܽ��
	final = ""; //�������
	printLine(inplat.length() - 6, Test_Mode_Control);
	cout << inplat << endl;
	printLine(inplat.length() - 6, Test_Mode_Control);
	cin >> platform;
	oripla = platform;
	printLine(inaccout.length() - 6, Test_Mode_Control);
	cout << inaccout << endl;
	printLine(inaccout.length() - 6, Test_Mode_Control);
	cin >> account;
	printLine(inname.length() - 6, Test_Mode_Control);
	cout << inname << endl;
	printLine(inname.length() - 6, Test_Mode_Control);
	cin >> name;
	if (name[0] == '0')
		name = "noname";
	printLine(indate.length() - 6, Test_Mode_Control);
	cout << indate << endl;
	printLine(indate.length() - 6, Test_Mode_Control);
	cin >> date;
	//������
	ProcessPf(platform, date);
	ProcessPatch(patch, date);
	datetoStr(dates, date);
	final += platform;
	final += account;
	final += patch;
	encrypt(final, G_encr);
	//��־������
	//��־�ļ���ʼ��
	BlogInit(Test_Mode_Control);
	DataInit();
	extern ofstream fblog; extern ofstream fdata;
	//blogд��
	fblog << oripla << ",";//��һ�У�ƽ̨��
	fblog << account << ",";//�ڶ��У��˺ţ�
	fblog << name << ",";//�����У��û�����
	fblog << G_encr << ",";//�����У�������ܴ���
	fblog << dates << endl;//�����У����ڣ�
	fblog.close();//��ȫ�ر�
	//dataд��
	// fdata << oripla << ",";//��һ�У�ƽ̨��
	// fdata << account << ",";//�ڶ��У��˺ţ�
	// fdata << name << ",";//�����У��û�����
	// fdata << G_encr << ",";//�����У�������ܴ���
	// fdata.close();
	//�����
	printf("+----------------+\n");
	printf("|  ��������Ϊ��  |\n");
	printf("+----------------+\n");
	fopc = final.length();
	printLine(fopc, Test_Mode_Control);
	cout << "|  " << final << "  |" << endl;
	printLine(fopc, Test_Mode_Control);
	if (Test_Mode_Control == 1)
	{
		printLine(G_encr.length() + 8, Test_Mode_Control);
		cout << "|  encr is " << G_encr <<"  |" << endl;
		printLine(G_encr.length() + 8, Test_Mode_Control);
	}
	//������
	if (RT_Control == 0)
	{
		next = -1;
		printNextO(Test_Mode_Control);
		cin >> next;
		if (next == 1)
			goto Generator;
		else if (next == 2)
			goto Decryptor;
		else
		{
			fblog.close();
			return 0;
		}
	}
	/*else
	{
		RT_Control = 0;
		goto RT_Loop1;
	}*/
	/*������
Decryptor:
	D_encr = ""; D_pwdc = "";
	printf("+---------------------+\n");
	printf("|  ��������ܺ���:  |\n");
	printf("+---------------------+\n");
	cin >> D_encr;
	decrypt(D_encr, D_pwdc);
	printf("+-----------------+\n");
	printf("|  ���ܺ���Ϊ:  |\n");
	printf("+-----------------+\n");
	fopc = D_pwdc.length();
	printLine(fopc, Test_Mode_Control);
	cout << "|  " << D_pwdc << "  |" << endl;
	printLine(fopc, Test_Mode_Control);
	//������
	if (RT_Control == 0)
	{
		next = -1;
		printNextO(Test_Mode_Control);
		cin >> next;
		if (next == 1)
			goto Generator;
		else if (next == 2)
			goto Decryptor;
		else
		{
			fblog.close();
			return 0;
		}
	}
	else
	{
		RT_Control = 0;
		goto RT_Loop2;
	}
Rep_Test:
	NOT = 0; RTmod = "";
	printLine(inNOT.length() - 6, Test_Mode_Control);
	cout << inNOT << endl;
	printLine(inNOT.length() - 6, Test_Mode_Control);
	cin >> NOT;
	printLine(inRTmod1.length() - 6, Test_Mode_Control);
	cout << inRTmod1 << endl << inRTmod2 << endl;
	printLine(inRTmod1.length() - 6, Test_Mode_Control);
	cin >> RTmod;

	if (RTmod == "rg")
	{
	RT_Loop1:
		RT_Control = 1;
		if (NOT)
		{
			NOT--;
			goto Generator;
		}
		else
		{
			fblog.close();
			return 0;
		}
	}
	else if (RTmod == "rd")
	{
	RT_Loop2:
		RT_Control = 1;
		if (NOT)
		{
			NOT--;
			goto Decryptor;
		}
		else
		{
			fblog.close();
			return 0;
		}
	}*/
}
