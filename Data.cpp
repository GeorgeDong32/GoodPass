#include "Data.h"
#include "GPSES.h"
#include "Display.h"
#include <fstream>
#include <Windows.h>
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
	Displayinf(finaldis, 1, 0, "green");
}

void Data::printData()
{
	string dispw;;
	string title = "�˺���Ϣ";
	int pl = 0; int al = 0; int pwl = 0;
	GPBES pD(encp, 1);
	dispw = pD.decrypt();
	pl = platform.length();
	al = account.length();
	pwl = dispw.length();
	int ml = pl + al + pwl + 8;
	printLine(ml - 6, 0);
	cout << "| " << setw(ml - 4) << left << title << " |" << endl;
	printmLine(ml - 6, 0);
	cout << "| " << platform << "  " << account << "  " << dispw << " |" << endl;
	printLine(ml - 6, 0);
}

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
	GPBES gp(encp, 1);
	re = gp.decrypt();
	return re;
}

bool Data::resetData(string npw)
{
	string temp = encp;
	encp = "";
	GPBES rd(npw, 0);
	encp = rd.encrypt();
	if (encp != "" && encp != temp)
		return true;
	else
		return false;
}

void Data::selfupdate()
{
	string* mid = new string;
	GPBES gp(encp, 1);
	*mid = gp.decrypt();
	int check = resetData(*mid);
	if (check == 0)
	{
		GPBES gp(encp, 1);
		*mid = gp.decrypt();
	}
	delete mid;
}

void Manager::fuzzysearch(string platform)//��ƽ̨�����˺�
{
	Data* p = head;
	int count = 0;
	string s_account[40]; string sp[40]; string counts;
	string display1 = "���ҵ�����"; string display2 = "��"; string display3 = "���˺�:";
	string title;
	while (p != NULL)
	{
		if (count > 40)
		{
			SetColor(126);
			printf("!---------------<!>---------------!\n");
			printf("|  :(                             |\n");
			printf("|  Too many accounts are found!   |\n");
			printf("!---------------<!>---------------!\n");
			SetColor(112);
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
	{
		int tn = count / 10;
		int gn = count % 10;
		if (tn > 0)
		{
			counts += '0' + tn;
			counts += '0' + gn;
		}
		else
		{
			counts = '0' + gn;
		}
	}
	//sort content
	for (int i = 0; i < count; i++)
	{
		for (int j = i; j < count; j++)
		{
			if (sp[i] > sp[j])
			{
				string midp = sp[i];
				sp[i] = sp[j];
				sp[j] = midp;
				string mida = s_account[i];
				s_account[i] = s_account[j];
				s_account[j] = mida;
			}
			else if (sp[i] == sp[j])
			{
				if (s_account[i] > s_account[j])
				{
					string midp = sp[i];
					sp[i] = sp[j];
					sp[j] = midp;
					string mida = s_account[i];
					s_account[i] = s_account[j];
					s_account[j] = mida;
				}
			}
		}
	}
	title = display1 + platform + display2 + counts + display3;
	int mlp = 0; int mln = 0; int ml = 0;
	for (int i = 0; i < count; i++)
	{
		mlp = fmax(sp[i].length(), mlp);
		mln = fmax(s_account[i].length(), mln);
	}
	ml = fmax(title.length(), mlp + mln + 5);
	printLine(ml - 2, 0);
	cout << "| " << setw(ml - 2) << left << title << " |" << endl;
	for (int i = 0; i < count; i++)
	{
		printmLine(ml - 2, mlp + 2);
		printf("| ");
		cout << setw(mlp) << left << sp[i] << " |";
		cout << " " << setw(ml - mlp - 3) << left << s_account[i]; printf(" |\n");
	}
	printLine(ml - 2, 0);
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
		Displayinf(Dis_accs, 1, 0, "red");
	}
	return NULL;
}

void Manager::addData_User(string p, string a, string e)
{
	string mid = e; string aD_U_AS = "|  �����ɹ���  |";
	e = "";
	Data* pd = head;
	GPBES adu(mid, 0);
	e = adu.encrypt();
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
		Displayinf(aD_U_AS, 1, 0, "green");
	}
	else
	{
		delete nd;
		string disp1 = "|  �˺��Ѵ��ڣ���ǰ���޸Ľ���  |";
		Displayinf(disp1, 1, 0, "red");
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
	else if (pd != head && pd != NULL)
	{
		pb->next = pd->next;
		delete pd;
		Data_Delete_S(p, a);
	}
	else if (pd == NULL)
	{
		string Dis = "|  δ�ҵ���Ӧ�˺ţ���������ԣ�   |";
		SetColor(118);
		cout << "*----------------<!>----------------*" << endl;
		cout << Dis << endl;
		cout << "*----------------<!>----------------*" << endl;
		SetColor(112);
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
		Displayinf("������������", 0, 0, "ori");
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
				GPBES rd(newp, 0);
				temp1 = rd.encrypt();
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
				Displayinf(dp_rD2, 1, 0, "green");
			}
			else if (recheck == -1)
			{
				Displayinf(dp_rD3, 1, 0, "red");
			}
			else if (recheck == 0)
			{
				Displayinf(dp_rD4, 1, 0, "red");
			}
		}
		else
		{
			Displayinf(dp_rD1, 1, 0, "red");
		}
	}
	else
	{
		Displayinf(display1, 1, 0, "yellow");
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
			GPBES rd(newp, 0);
			temp1 = rd.encrypt();
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
		SetColor(124);
		printLine(dis_sD.length() - 6, 0);
		cout << dis_sD << endl;
		printLine(dis_sD.length() - 6, 0);
		SetColor(112);
	}
}

void Manager::showData(Data& d)
{
	d.printData();
}

void Manager::dataupdate()
{
	Data* p = head;
	while (p != NULL)
	{
		p->selfupdate();
		if (p->next != NULL)
		{
			p = p->next;
		}
		else
			break;
	}
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
	string fdis = "The data file has been updated!";
	Displayinf(fdis, 0, 0, "green");
	outfile.close();
}