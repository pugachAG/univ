#include <string>
#include <vector>
#include <map>
#include <fstream>

#define LOGI(...) // { printf("info: "); printf(__VA_ARGS__); printf("\n");  std::cout.flush();}

using namespace std;

class DataProvider
{
public:
	DataProvider(string filePath);

	string GetNextChunk();

	bool CanGetNextChunk();

private:
	static const long long bufferSize = 1024;
	char buff[bufferSize + 1];
	string filePath;
	ifstream file;
	int fileSize;
};

class DataHandler
{
public:
	DataHandler();

	void HandleChunk(string& str);

	pair<vector<string>, int> GetMostFrequentWords();

private:
	map<string, int> words;
	string previousQueryResidue;

	bool IsSeparator(char& ch);

};


class Core
{
public:
	static void ProcessQuery(string filePath); 
};