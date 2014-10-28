#include <stdio.h>
#include <unistd.h>
#include <sys/types.h>
#include <cstring>
#include <sstream>
#include <vector>

using namespace std;

int f(int x)
{
	int s = 0;
	for(int i = 0; i < x; i++)
		s += i;
	return s;
}

int g(int x)
{
	sleep(1);
	return x;
}

vector<int> read_fds;

void func_fork(int (*func)(int), int x)
{
	int fd[2];
	pipe(fd);
	
	
	int childpid = fork();
	
	if(childpid == -1)
	{
		printf("Error fork");
		return;
	}
		
	if(childpid == 0)
	{
		//child process
		close(fd[0]);
		int val = func(x);
		char buff[32];
		sprintf(buff, "%d", val);
		write(fd[1], buff, sizeof(buff)); 
		close(fd[1]);
	}
	else
	{
		//parent process
		close(fd[1]);
		read_fds.push_back(fd[0]);
		char read_buff[32];
		read(fd[0],  read_buff, sizeof(read_buff));
		printf("%s\n", read_buff);
		//printf("%d\n", f(x));
	}
}

int main(int argc, char* argv[])
{
	if(argc == 2)
	{
		int x;
		sscanf(argv[1], "%d", &x);
		func_fork(f, x);
	}
} 
