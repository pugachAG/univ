#include "calculations_handler.h"
#include "core.h"
#include <unistd.h>

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
    sleep(3);
    return 0;
}

int main(int argc, char* argv[])
{
    vector<function<int(int)>> funcs;
    funcs.push_back(function<int(int)>(f));
    funcs.push_back(function<int(int)>(g));
    core c(funcs);
    c.run();

    exit(0);
} 
