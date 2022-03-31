#include "Data.h"
#include "PPF_cryption.h"
#include "Display.h"
void addempty(int oril, int tarl)//格式函数：添加空格
{
	int con = tarl - oril;
	while (con)
	{
		cout << " ";
		con--;
	}
}

bool fuzzymatch(string o, string t)//字符串模糊匹配
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
	string dis1 = "|  已成功删除"; string dis2 = "账号：";
	string finaldis = dis1 + p + dis2 + a + "  |";
	printLine(finaldis.length() - 6, 0);
	cout << finaldis << endl;
	printLine(finaldis.length() - 6, 0);
}

void Data::printData()
{
	string mid;
	decrypt(encp, mid);
	int ml = fmax(platform.length(), fmax(account.length(), mid.length()));
	printLine(ml, 0);
	cout << "|  " << platform;
	addempty(platform.length(), ml);
	cout << "  |" << endl;
	cout << "|  " << account;
	addempty(account.length(), ml);
	cout << "  |" << endl;
	cout << "|  " << mid;
	addempty(mid.length(), ml);
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

void Manager::fuzzysearch(string platform)//按平台搜索账号
{
	Data* p = head; int ml = 0; int nl = 0;
	int count = 0; int countt = 0;
	string s_account[40]; string sp[40]; string final[40];
	string display1 = "已找到关于"; string display2 = "的"; string display3 = "个账号:";
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
		final[i] = sp[i] + "账号：" + s_account[i];
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
		cout << "|  " << final[i];
		addempty(final[i].length(), ml);
		cout << "  |" << endl;
	}
	printLine(ml, 0);
}

Data* Manager::accusearch(string platform, string account, int mode)
{
	Data* p = head;
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
		printLine(33, 0);
		cout << "|  未找到对应账号，请检查后重新搜索  |" << endl;
		printLine(33, 0);
	}
	return NULL;
}

void Manager::addData_User(string p, string a, string e)
{
	string mid = e; string dis = "|  新增成功！  |";
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
		printLine(dis.length() - 6, 0);
		cout << dis << endl;
		printLine(dis.length() - 6, 0);
	}
	else
	{
		delete nd;
		string disp1 = "|  账号已存在，请前往修改界面  |";
		printLine(disp1.length() - 6, 0);
		cout << disp1 << endl;
		printLine(disp1.length() - 6, 0);
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
	else
	{
		pb->next = pd->next;
		delete pd;
		Data_Delete_S(p, a);
	}
}

void Manager::reviseData(string p, string a)
{
	Data* pd = NULL; string display1 = "|  不存在对应的可修改对象，请检查后重试  |";
	pd = accusearch(p, a, 0);
	if (pd != NULL)
	{
		string newp; int sp = 0; string temp1 = pd->getEncp();
		int recheck = 0; string temp2;
		string dp_rD1 = "|  修改失败，请重试  |"; string dp_rD2 = "|  修改成功！  |"; string dp_rD3 = "|  修改出错(-1)，请重试  |";
		string dp_rD4 = "|  修改出错(0)，请重试  |";
		printLine(12, 0);
		cout << "|  请输入新密码  |" << endl;
		printLine(12, 0);
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
			else if (recheck == 0)
			{
				printLine(dp_rD4.length() - 6, 0);
				cout << dp_rD4 << endl;
				printLine(dp_rD4.length() - 6, 0);
			}
		}
		else
		{
			printLine(dp_rD1.length() - 6, 0);
			cout << dp_rD1 << endl;
			printLine(dp_rD1.length() - 6, 0);
		}
	}
	else
	{
		printLine(display1.length() - 6, 0);
		cout << display1 << endl;
		printLine(display1.length() - 6, 0);
	}
}

void Manager::reviseData(Data& d)
{
	string newp; int sp = 0; string temp1 = d.getEncp();
	int recheck = 0; string temp2;
	string dp_rD1 = "|  修改失败，请重试  |"; string dp_rD2 = "|  修改成功！  |"; string dp_rD3 = "|  修改出错，请重试  |";
	printLine(13, 0);
	cout << "|  请输入新密码  |" << endl;
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
	Data* ps = NULL; string dis_sD = "|  未找到对应账号数据，请检查后重试  |";
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