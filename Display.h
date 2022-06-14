#ifndef __GPDISPLAY_H__
#define __GPDISPLAY_H__
/*********************************************************
*                                                        *
* Display.h -- Provides the visual output functions      *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
* Display.h version 2.5.0 pre                            *
*                                                        *
**********************************************************/
#include <iostream>
#include <string>
#include <iomanip>
using namespace std;

void SetColor(int mixedcolor);//���ÿ���̨�����ɫ

void addempty(int oril, int tarl);//��ʽ��������ӿո�

void PrintTitle(void);//��ӡ����ͷ

void PrintTestTitle(void);//��ӡ���Գ���ͷ����ɾ������ģʽ���������ϣ�

void printLine(int len);//��ӡ�ָ��ߣ��߳�Ϊlen+4��modeΪ0ʱ�����ͨ�ߣ�

void printNextO(int mode);//�����һ���˵������ڴ�ɾ����

void printMenu(int mode);//��ӡ�˵�

void Displayinf(string display, int printmode, int linemode, string color);//��ӡ������ı�
//printmode:�ı��Ƿ���в�߿���Ϊ1����Ϊ0
//linemode:�ı����ʽ��1Ϊ����ģʽ��0Ϊ��ͨģʽ

void printDevloping();//��ӡ��������ʾ

void printaddMenu();//��ӡ��������ʾ����Ч��������ɾ����

void printmLine(int len, int connectpoint);//��ӡ�������

#endif