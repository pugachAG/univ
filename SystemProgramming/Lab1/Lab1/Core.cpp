#include "Core.h"

#include <cmath>
#include <iostream>
#include <algorithm>

using namespace std;

DataProvider::DataProvider(string filePath)
{
	this->filePath = filePath;
	this->file.open(filePath, ios::in|ios::binary|ios::ate);
	this->file.seekg (0, ios::end);
	this->fileSize = file.tellg();
	this->file.seekg (0, ios::beg);
	if(!this->file)
		throw exception("Error open file");
}

string DataProvider::GetNextChunk()
{
	LOGI("GetNextChunk start");
	int readSize = min(bufferSize, fileSize - file.tellg());
	buff[readSize] = '\0';
	file.read(buff, bufferSize);
	LOGI("GetNextChunk end");
	return string(buff);
}

bool DataProvider::CanGetNextChunk()
{
	LOGI("File tellg() = %d", (int)file.tellg());
	return !file.eof();
}

DataHandler::DataHandler()
{
}

void DataHandler::HandleChunk(const string& str)
{
	LOGI("HandleChunk start");
	string query = previousQueryResidue + str;
	string word;
	LOGI("HandleChunk iterations");
	int wordIndexStart = 0;
	for(int i = 0; i < query.size(); i++)
	{
		if(IsSeparator(query[i]))
		{
			if(wordIndexStart != i)
			{
				int wordLength = i - wordIndexStart;
				words[query.substr(wordIndexStart, wordLength)]++;
			}
			wordIndexStart = i + 1;
		}
	}
	previousQueryResidue = query.substr(wordIndexStart, query.size() - wordIndexStart);
	LOGI("HandleChunk end");
}

bool DataHandler::IsSeparator(char& ch)
{
	return ch >= -1 && ch <= 255 && !isalpha(ch);
}

pair<vector<string>, int> DataHandler::GetMostFrequentWords()
{
	LOGI("GetMostFrequentWords start");
	if(previousQueryResidue.length())
	{
		words[previousQueryResidue]++;
		previousQueryResidue = "";
	}
	pair<vector<string>, int> result(vector<string>(), 0);
	LOGI("GetMostFrequentWords iterations start");
	for(map<string, int>::iterator it = words.begin(); it != words.end(); ++it)
	{
		if(it->second >= result.second)
		{
			if(it->second > result.second)
			{
				result.first.clear();
				result.second = it->second;
			}
			result.first.push_back(it->first);
		}
	}
	LOGI("GetMostFrequentWords iterations end");
	return result;
}

void Core::ProcessQuery(const string& filePath)
{
	try
	{
		DataProvider provider(filePath);
		DataHandler handler;
		while (provider.CanGetNextChunk())
		{
			handler.HandleChunk(provider.GetNextChunk());
		}

		pair<vector<string>, int> result = handler.GetMostFrequentWords();
		if(result.first.size())
		{
			cout<<"The most frequent words: "<<endl;
			for(int i = 0; i < result.first.size(); i++)
			{
				cout<<result.first[i]<<endl;
			}
			cout<<"Count of each word in file: "<<result.second<<endl;
		}
		else
		{
			cout<<"No words found"<<endl;
		}
	}
	catch(exception& ex)
	{
		cout<<"Error: "<<ex.what()<<endl;
	}
	
	
}
