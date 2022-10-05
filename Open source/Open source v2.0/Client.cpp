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

string version = "2.6.0    ";//���°汾��! 
//�滻ʱ��ʽΪ "n.x.y    "(4���ո�) �� "n.x.y dev"

//���ܻ���
int PI[40] = { 1,4,1,5,9,2,6,5,3,5,8,9,7,9,3,2,3,8,4,6,2,6,4,3,3,8,3,2,7,9,5,0,2,8,8,4,1,9,7,1 };
/*string  inplat = "|  ����ƽ̨���ƣ� |"; string inaccout = "|  �����˺����� |";
string indate = "|  �������ڣ�  |"; string inRTmod2 = "|  ����<rg>��������,<rd>���Խ���  |";
string inRTmod1 = "| ������ѭ��ģʽ                  |"; string inNOT = "|  �������������  |";
string inname = "|  �����û�����(��ѡ)������0������  |";*/
string MKC0 = "|  ������һ������λ15~39λ������  |"; string MKC2 = "|  ����������������,����ӦΪ15~39λ  |";

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
	switch (mkc)//���������ü�鵼��
	{
	case 2://�����쳣����������
		Displayinf(MKC2, 1, 0, "ori");
		cin >> MainKey;
		setConfig(MainKey);
		break;
	case 1:
		checkConfig(MainKey);//�����������������
		break;
	case 0://���ν�����������
		Displayinf(MKC0, 1, 0, "ori");
		cin >> MainKey;
		setConfig(MainKey);
		break;
	}
	//Manager��ʼ��
	Manager gpm;
	DataInit();
	DataInit(gpm, MDpath);
	gpm.dataupdate();
	printMenu(Test_Mode_Control);
	while (cin >> opt)//ѡ��˵�
	{
		switch (opt)
		{
		case 'e':
		case '0':
			FileUpdate(gpm, MDpath, 1);
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
		case '6':
			GP_showall(gpm);
			break;
		default:
			Displayinf("������������", 0, 0, "yellow");
			break;
		}
		printMenu(Test_Mode_Control);
	}
	FileUpdate(gpm, MDpath, 1);//���ݱ���
	gpm.~Manager();
	return 0;
}
