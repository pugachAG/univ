#include "core.h"
#include "calculations_handler.h"
#include "common.h"
#include <string>
#include <iostream>
#include <unistd.h>

using namespace std;

void core::run()
{
    int n = (int)funcs.size();
    cout<<"Input x:"<<endl;
    int x = 10;
    cin>>x;
    for(int i = 0; i < n; i++)
    {
        handlers.push_back(unique_ptr<calculations_handler>(new calculations_handler(funcs[i], x, this)));
        handlers[i]->start();
    }
    while(!finished)
    {
        string ans;
        cout<<endl<<"Terminate calculations? (yes if you want)"<<endl;
        cin>>ans;
        if(ans == "yes")
        {
            terminate_all();
            break;
        }
        sleep(WAIT_INTERVAL);
    }
    cout<<"run() exit"<<endl;
}


void core::on_ready()
{
    finished = true;
    cout<<"Calculations finished"<<endl;
    terminate_all();
    int result = 1;
    for(int i = 0; i < vals.size(); i++)
        result *= vals[i];
    cout<<"*** RESULT "<<result<<" ***"<<endl;
    exit(0);
}



void core::handle_value(int val)
{
    vals_guard.lock();
    if(!finished)
    {
        vals.push_back(val);
        cout << "Got calculated value " << val << endl;
        if (vals.size() == funcs.size() || val == 0)
        {
            on_ready();
        }
    }
    vals_guard.unlock();
}

void core::terminate_all()
{
    for(int i = 0; i < handlers.size(); i++)
    {
        if(handlers[i]->get_status() == processing)
            handlers[i]->terminate();
    }
}

