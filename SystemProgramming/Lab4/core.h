#pragma once

#include <vector>
#include <functional>
#include <mutex>
#include <memory>

class calculations_handler;

class core
{
public:
    core(const std::vector<std::function<int(int)>>& functions)
            : funcs(functions), finished(false), vals(), vals_guard(), handlers() {}

    void run();
    void handle_value(int val);

private:
    std::vector<std::function<int(int)>> funcs;
    std::vector<int> vals;
    std::mutex vals_guard;
    std::vector<std::unique_ptr<calculations_handler>> handlers;
    bool finished;


    void on_ready();
    void terminate_all();
};