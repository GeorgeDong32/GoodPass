#include "GPBase.h"
#include "Display.h"
#include "Generate.h"
#include <Windows.h>

int start_option(char control)
{
	switch (control)
	{
	case 'G':
	case 'g':
		return 1;
	case 'D':
	case 'd':
		return 2;
	case 't':
		return 3;
	default:
		return 4;
	}
}

void GP_add(Manager &m)
{
	string GP_add_dis1 = "|  ������ƽ̨����  |"; string GP_add_dis2 = "|  �������¼��/�û���  |";
	string GP_add_dis3 = "|  ����������  |"; string dis = "��������Ϊ��";
	string p, a, pw; char opt = '#'; int ml = 0; Data* check = NULL;
	Displayinf(GP_add_dis1, 1, 0, "ori");
	cin >> p;
	Displayinf(GP_add_dis2, 1, 0, "ori");
	cin >> a;
	check = m.accusearch(p, a, 0);
	if (check == NULL)
	{
		printaddMenu();
		cin >> opt;
		switch (opt)
		{
		case'm':
		case '1':
			Displayinf(GP_add_dis3, 1, 0, "ori");
			cin >> pw;
			break;
		case 'g':
		case '2':
			pw = Generatepw(p, a);
			ml = fmax(dis.length(), pw.length());
			printLine(ml, 0);
			cout << "|  " << dis; addempty(dis.length(), ml);
			cout << "  |" << endl;
			cout << "|  " << pw << "  |" << endl;
			printLine(ml, 0);
		}
		m.addData_User(p, a, pw);
	}
	else
	{
		string disp1 = "|  �˺��Ѵ��ڣ���ǰ���޸Ľ���  |";
		Displayinf(disp1, 1, 0,"yellow");
	}
}

void GP_search(Manager& m)
{
	string dis = "|  ������ƽ̨����  |";
	string p;
	Displayinf(dis, 1, 0, "ori");
	cin >> p;
	m.fuzzysearch(p);
}

void GP_get(Manager& m)
{
	string dis1 = "|  ������ƽ̨����  |"; string dis2 = "|  �������¼��/�û���  |";
	string p, a;
	Displayinf(dis1, 1, 0, "ori");
	cin >> p;
	Displayinf(dis2, 1, 0, "ori");
	cin >> a;
	m.showData(p,a);
}

void GP_change(Manager& m)
{
	string dis1 = "|  ������ƽ̨����  |"; string dis2 = "|  �������¼��/�û���  |";
	string p, a;
	Displayinf(dis1, 1, 0, "ori");
	cin >> p;
	Displayinf(dis2, 1, 0, "ori");
	cin >> a;
	m.reviseData(p, a);
}

void GP_delete(Manager& m)
{
	const string dis1 = "|  ������Ҫɾ�����˺�����ƽ̨  |"; const string dis2 = "|  ������Ҫɾ�����˺�  |";
	string p, a;
	Displayinf(dis1, 1, 0, "ori");
	cin >> p;
	Displayinf(dis2, 1, 0, "ori");
	cin >> a;
	m.deleteData(p, a);
}

string Getsystime()
{
	SYSTEMTIME now{ 0 };
	GetLocalTime(&now);
	string y, m, d, final;
	int mid = 0; int i = 0;
	y = "0000"; m = "00"; d = "00";
	mid = now.wYear;
	for (i = 3; i >= 0; i--)
	{
		y[i] = mid % 10 + '0';
		mid /= 10;
	}
	mid = now.wMonth; i = 1;
	while (mid)
	{
		m[i] = '0' + mid % 10;
		mid /= 10;
	}
	mid = now.wDay; i = 1;
	while (mid)
	{
		d[i] = '0' + mid % 10;
		mid /= 10;
	}
	final = y + m + d;
	return final;
}
