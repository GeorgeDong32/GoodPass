#include "Data.h"
#include "GP_cryption.h"
#include "Display.h"
#include <fstream>
#include <sstream>

bool fuzzymatch(string o, string t)//�ַ���ģ��ƥ��
{
	int lo = o.length(); int lt = t.length();
	if (lo < lt)
		return false;
	else
	{
		int flag = 1;
		for (int i = 0; i < lt; i++)
		{
			char check = t[i];
			if (check <= 'Z' && check >= 'A')
			{
				if (o[i] == check || o[i] == check + 'a' - 'A')
					flag *= 1;
				else flag = 0;
			}
			else if (check <= 'z' && check >= 'a')
			{
				if (o[i] == check || o[i] == check - 'a' + 'A')
					flag *= 1;
				else flag = 0;
			}
			else
			{
				if (o[i] == check)
					flag *= 1;
				else
					flag = 0;
			}
		}
		if (flag == 1)
			return true;
		else
			return false;
	}
}

void Data_Delete_S(string p, string a)
{
	string dis1 = "|  �ѳɹ�ɾ��"; string dis2 = "�˺ţ�";
	string finaldis = dis1 + p + dis2 + a + "  |";
	printLine(finaldis.length() - 6, 0);
	cout << finaldis << endl;
	printLine(finaldis.length() - 6, 0);
}

void Data::printData()
{
	string mid;
	string dis1, dis2, dis3;
	decrypt(encp, mid);
	dis1 = platform + "ƽ̨";
	dis2 = "�˺�Ϊ��" + account;
	dis3 = "����Ϊ��" + mid;
	int ml = fmax(dis1.length(), fmax(dis2.length(), dis3.length()));
	printLine(ml, 0);
	cout << "|  " << dis1;
	addempty(dis1.length(), ml);
	cout << "  |" << endl;
	cout << "|  " << dis2;
	addempty(dis2.length(), ml);
	cout << "  |" << endl;
	cout << "|  " << dis3;
	addempty(dis3.length(), ml);
	cout << "  |" << endl;
	printLine(ml, 0);
}

/*int Data::getpos()
{
	return linepos;
}*/

string Data::getaccount()
{
	return account;
}

string Data::getplatform()
{
	return platform;
}

string Data::getEncp()
{
	return encp;
}

string Data::getPassword()
{
	string re;
	decrypt(encp, re);
	return re;
}

bool Data::resetData(string npw)
{
	string temp = encp;
	encp = "";
	encrypt(npw, encp);
	if (encp != "" && encp != temp)
		return true;
	else
		return false;
}

void Manager::fuzzysearch(string platform)//��ƽ̨�����˺�
{
	Data* p = head; int ml = 0; int nl = 0;
	int count = 0; int countt = 0;
	string s_account[40]; string sp[40]; string final[40];
	string display1 = "���ҵ�����"; string display2 = "��"; string display3 = "���˺�:";
	while (p != NULL)
	{
		if (count > 40)
		{
			cout << "!---------------<!>---------------!" << endl;
			cout << "|  :(                             |" << endl;
			cout << "|  Too many accounts are found!   |" << endl;
			cout << "!---------------<!>---------------!" << endl;
		}
		if (fuzzymatch(p->getplatform(), platform))
		{
			sp[count] = p->getplatform();
			s_account[count] = p->getaccount();
			count++;
		}
		if (p->next != NULL)
		{
			p = p->next;
		}
		else
			break;
	}
	for (int i = 0; i < count; i++)
	{
		final[i] = sp[i] + "�˺ţ�" + s_account[i];
	}
	//display2 += ('0' + count);
	countt = count;
	while (countt)
	{
		countt /= 10;
		nl++;
	}
	string finaldisplay = display1 + platform + display2;
	ml = finaldisplay.length() + 7 + nl;
	for (int i = 0; i < count; i++)
	{
		ml = fmax(final[i].length(), ml);
	}
	printLine(ml, 0);
	cout << "|  " << finaldisplay << count << display3; addempty(finaldisplay.length() + 7 + nl, ml); cout << "  |" << endl;
	for (int i = 0; i < count; i++)
	{
		printLine(ml, 0);
		cout << "|  " << final[i];
		addempty(final[i].length(), ml);
		cout << "  |" << endl;
		//printLine(ml, 0);
	}
	printLine(ml, 0);
}

Data* Manager::accusearch(string platform, string account, int mode)//mode��ʾ���ƣ�1ʱ�гɹ������2
{
	Data* p = head;
	string Dis_accs = "|  δ�ҵ���Ӧ�˺ţ��������������  |";
	while (p != NULL)
	{
		if (fuzzymatch(p->getplatform(), platform))
		{
			if (fuzzymatch(p->getaccount(), account))
				return p;
		}
		if (p->next != NULL)
		{
			p = p->next;
		}
		else
			break;
	}
	if (mode == 1)
	{
		Displayinf(Dis_accs, 1, 0);
	}
	return NULL;
}

void Manager::addData_User(string p, string a, string e)
{
	string mid = e; string aD_U_AS = "|  �����ɹ���  |";
	e = "";
	Data* pd = head;
	encrypt(mid, e);
	Data* nd = new Data(p, a, e); Data* check = NULL;
	check = accusearch(p, a, 0);
	if (check == NULL)
	{
		while (pd != NULL)
		{
			if (pd->next != NULL)
			{
				pd = pd->next;
			}
			else
				break;
		}
		if (pd == head && pd == NULL)
			head = nd;
		else
			pd->next = nd;
		Displayinf(aD_U_AS, 1, 0);
	}
	else
	{
		delete nd;
		string disp1 = "|  �˺��Ѵ��ڣ���ǰ���޸Ľ���  |";
		Displayinf(disp1, 1, 0);
	}
}

void Manager::addData_File(string p, string a, string es)
{
	Data* nd = new Data(p, a, es); Data* pd = head;
	Data* check = NULL;
	check = accusearch(p, a, 0);
	if (check == NULL)
	{
		while (pd != NULL)
		{
			if (pd->next != NULL)
			{
				pd = pd->next;
			}
			else
				break;
		}
		if (pd == head && pd == NULL)
			head = nd;
		else
			pd->next = nd;
	}
	else
		delete nd;
}

void Manager::deleteData(string p, string a)
{
	Data* pd = NULL;
	Data* pc = head; Data* pb = NULL;
	while (pc != NULL)
	{
		if (fuzzymatch(pc->getplatform(), p))
		{
			if (fuzzymatch(pc->getaccount(), a))
			{
				pd = pc;
				break;
			}
		}
		if (pc->next != NULL)
		{
			pb = pc;
			pc = pc->next;
		}
		else
			break;
	}
	if (pd == head)
	{
		if (pd->next != NULL)
		{
			head = pd->next;
			delete pd;
			Data_Delete_S(p, a);
		}
		else
		{
			head = NULL;
			delete pd;
			Data_Delete_S(p, a);
		}
	}
	else if(pd != head && pd != NULL)
	{
		pb->next = pd->next;
		delete pd;
		Data_Delete_S(p, a);
	}
	else if (pd == NULL)
	{
		string Dis = "|  δ�ҵ���Ӧ�˺ţ���������ԣ�   |";
		cout << "*----------------<!>----------------*" << endl;
		cout << Dis << endl;
		cout << "*----------------<!>----------------*" << endl;
	}
}

void Manager::reviseData(string p, string a)
{
	Data* pd = NULL; string display1 = "|  �����ڶ�Ӧ�Ŀ��޸Ķ������������  |";
	pd = accusearch(p, a, 0);
	if (pd != NULL)
	{
		string newp; int sp = 0; string temp1 = pd->getEncp();
		int recheck = 0; string temp2;
		string dp_rD1 = "|  �޸�ʧ�ܣ�������  |"; string dp_rD2 = "|  �޸ĳɹ���  |"; string dp_rD3 = "|  �޸ĳ���(-1)��������  |";
		string dp_rD4 = "|  �޸ĳ���(0)��������  |";
		Displayinf("������������", 0, 0);
		cin >> newp;
		sp = pd->resetData(newp);
		if (sp)
		{
			temp2 = pd->getEncp();
			if (temp1 == temp2)
				recheck = 0;
			else
				recheck = 1;
			if (recheck)
			{
				temp1 = "";
				encrypt(newp, temp1);
				if (temp1 == temp2)
					recheck = 1;
				else
					recheck = -1;
			}
		}
		if (sp == 1)
		{
			if (recheck == 1)
			{
				Displayinf(dp_rD2, 1, 0);
			}
			else if (recheck == -1)
			{
				Displayinf(dp_rD3, 1, 0);
			}
			else if (recheck == 0)
			{
				Displayinf(dp_rD4, 1, 0);
			}
		}
		else
		{
			Displayinf(dp_rD1, 1, 0);
		}
	}
	else
	{
		Displayinf(display1, 1, 0);
		printLine(display1.length() - 6, 0);
		cout << display1 << endl;
		printLine(display1.length() - 6, 0);
	}
}

void Manager::reviseData(Data& d)
{
	string newp; int sp = 0; string temp1 = d.getEncp();
	int recheck = 0; string temp2;
	string dp_rD1 = "|  �޸�ʧ�ܣ�������  |"; string dp_rD2 = "|  �޸ĳɹ���  |"; string dp_rD3 = "|  �޸ĳ���������  |";
	printLine(13, 0);
	cout << "|  ������������  |" << endl;
	printLine(13, 0);
	cin >> newp;
	sp = d.resetData(newp);
	if (sp)
	{
		temp2 = d.getEncp();
		if (temp1 == temp2)
			recheck = 0;
		else
			recheck = 1;
		if (recheck)
		{
			encrypt(newp, temp1);
			if (temp1 == temp2)
				recheck = 1;
			else
				recheck = -1;
		}
	}
	if (sp == 1)
	{
		if (recheck == 1)
		{
			printLine(dp_rD2.length() - 6, 0);
			cout << dp_rD2 << endl;
			printLine(dp_rD2.length() - 6, 0);
		}
		else if (recheck == -1)
		{
			printLine(dp_rD3.length() - 6, 0);
			cout << dp_rD3 << endl;
			printLine(dp_rD3.length() - 6, 0);
		}
	}
	else
	{
		printLine(dp_rD1.length() - 6, 0);
		cout << dp_rD1 << endl;
		printLine(dp_rD1.length() - 6, 0);
	}
}

void Manager::showData(string p, string a)
{
	Data* ps = NULL; string dis_sD = "|  δ�ҵ���Ӧ�˺����ݣ����������  |";
	ps = accusearch(p, a, 0);
	if (ps != NULL)
		ps->printData();
	else
	{
		printLine(dis_sD.length() - 6, 0);
		cout << dis_sD << endl;
		printLine(dis_sD.length() - 6, 0);
	}
}

void Manager::showData(Data& d)
{
	d.printData();
}

void DataInit(Manager& m, string dp)
{
	ifstream infile(dp, ios::in);
	if (!infile)
	{
		cout << "���ļ�ʧ�ܣ�" << endl;
		exit(1);
	}
	int i = 0;
	string line;
	string field;
	while (getline(infile, line))//getline(inFile, line)��ʾ���ж�ȡCSV�ļ��е�����
	{
		string p, a, e;
		string field;
		istringstream sin(line); //�������ַ���line���뵽�ַ�����sin��
		getline(sin, field, ','); //���ַ�����sin�е��ַ����뵽field�ַ����У��Զ���Ϊ�ָ��� 
		p = field;
		getline(sin, field, ',');
		a = field;
		getline(sin, field, ',');
		e = field;
		m.addData_File(p, a, e);
		//i++;
	}
	infile.close();
}

void FileUpdate(Manager& m, string Datapath)
{
	ofstream outfile(Datapath, ios::out);
	Data* p = m.getHead();
	while (p != NULL)
	{
		outfile << p->getplatform() << ",";
		outfile << p->getaccount() << ",";
		outfile << p->getEncp() << "," << endl;
		if (p->next != NULL)
		{
			p = p->next;
		}
		else
			break;
	}
	cout << "*-----------------------------------*" << endl;
	cout << "|  The data file has been updated!  |" << endl;
	cout << "*-----------------------------------*" << endl;
	outfile.close();
}