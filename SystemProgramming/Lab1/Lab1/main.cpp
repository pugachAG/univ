#include <iostream>
#include <string>

#include "Core.h"

using namespace std;

string testDataPathPrefix = "..\\Lab1\\";
string testDoc1 = "TestDocument1.txt";
string testDoc2 = "TestDocument2.txt";
string theHobbit = "The_Hobbit.txt";
string toBeOrNotToBe = "toBeOrNotToBe.txt";

int main()
{
	Core::ProcessQueryVariation10(testDataPathPrefix + testDoc2);
	getchar();
}