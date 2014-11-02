#include "core.h"
#include "calculations_handler.h"
#include "common.h"
#include <unistd.h>
#include <sys/wait.h>


using namespace std;

calculations_handler::calculations_handler(std::function<int(int)> f, int val, core* core)
    : func(f), x(val), calc_status(none), result_handler(core)
{
}

void calculations_handler::start()
{
    debug_print("calculations_handler::start()");
    this->process_thread = thread(&calculations_handler::do_calculate, this);
}

void calculations_handler::terminate()
{
    debug_print("Kill process");
    kill(this->childpid, SIGKILL);
    process_thread.join();
}


void calculations_handler::do_calculate()
{
    int fds[2];
    pipe(fds);
    char buff[32];
    this->childpid = fork();

    if(childpid == 0)
    {
        //child process
        close(fds[0]);
        //get result
        int val = func(x);
        //write result to pipe
        sprintf(buff, "%d", val);
        write(fds[1], buff, sizeof(buff));
        //close pipe
        close(fds[1]);
        exit(0);
    }
    else
    {
        //parent process
        calc_status = processing;
        close(fds[1]);
        int status;
        debug_print("Wait for child process execution...");
        int ret_code = waitpid(childpid, &status, 0);
        debug_print("Child proccess executed, return code %d", ret_code);
        bool is_normally = WIFEXITED(status);
        if(is_normally)
        {
            debug_print("Read return value from pipe...")
            read(fds[0], buff, sizeof(buff));
            int function_value;
            sscanf(buff, "%d", &function_value);
            calc_status = done;
            this->result = function_value;
            debug_print("Read value: %d ", function_value);
            on_finished();
        }
        else
        {
            debug_print("Child process has been terminated");
            calc_status = terminated;
        }
        close(fds[0]);
    }
}

void calculations_handler::on_finished()
{
    if(result_handler)
    {
        result_handler->handle_value(result);
    }
}

int calculations_handler::get_result() const
{
    return this->result;
}

calculations_handler_status calculations_handler::get_status() const
{
    return this->calc_status;
}
