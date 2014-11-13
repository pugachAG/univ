#include <iostream>
#include <istream>
#include <fstream>
#include <cstdio>
#include <string>
#include <vector>
#include <map>
#include <queue>
#include <set>

using namespace std;

vector<int> fins;
int start;
map<pair<int, char>, vector<int>> mp;

void readInput(ifstream& input)
{
	int tmp;
	input >> tmp >> tmp; // just ignore two first numbers
	input >> start;
	int n;
	input >> n;
	fins.assign(n, 0);
	for (int i = 0; i < n; i++)
		input >> fins[i];
	
	while (!input.eof())
	{
		int from, to;
		char label;
		input >> from >> label >> to;
		mp[make_pair(from, label)].push_back(to);
	}
}

vector<int> bfs(int init)
{
	vector<int> res;
	set<int> used;
	queue<int> q;

	auto tryPush = [&](int k)
	{
		if (used.count(k) == 0)
		{
			res.push_back(k);
			used.insert(k);
			q.push(k);
			//cout << k << endl;
		}
	};

	tryPush(init);
	
	while (q.size())
	{
		int cur = q.front();
		q.pop();
		
		for (auto pr : mp)
			if (pr.first.first == cur)
				for (auto k : pr.second)
					tryPush(k);
	}

	return res;
}

vector<int> move(int st, const string& str)
{
	set<int> res;
	res.insert(st);
	for (char ch : str)
	{
		set<int> cur;
		for (int ss : res)
			for (int a : mp[make_pair(ss, ch)])
				cur.insert(a);
		res = cur;
	}
	return vector<int>(res.begin(), res.end());
}

bool intersects(vector<int> v1, vector<int> v2)
{
	bool ok = false;
	for (int a : v1)
		for (int b : v2)
			if (a == b)
				ok = true;
	return ok;
}

bool solve(string str)
{
	vector<int> reachable = bfs(start);
	vector<int> sts;
	for (int st : reachable)
	{
		vector<int> all = bfs(st);
		bool ok = intersects(all, fins);
		if (ok)
			sts.push_back(st);
	}

	for (int st : sts)
	{
		vector<int> r = move(st, str);
		if (intersects(r, sts))
			return true;
	}

	return false;
}




int main()
{
	ifstream istream;
	istream.open("input.txt");
	readInput(istream);
	string str;
	while (1)
	{
		cin >> str;
		cout << solve(str) << endl;
	}
	system("pause");
}