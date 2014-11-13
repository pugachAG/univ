#pragma once

#include <functional>
#include <thread>

enum calculations_handler_status
{
    none,
    processing,
    terminated,
    done
};

class core;

class calculations_handler
{
public:
    calculations_handler(std::function<int(int)> func, int x, core* core);

    void start();
    void terminate();
    void do_calculate();

    int get_result() const;
    calculations_handler_status get_status() const;


private:
    calculations_handler_status calc_status;
    int childpid;
    int result;
    int x;
    core* result_handler;
    std::function<int(int)> func;
    std::thread process_thread;

    void on_finished();

};