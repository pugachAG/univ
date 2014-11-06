#pragma once

#include <stdio.h>
#include <ostream>

#define debug_print(...) { printf(__VA_ARGS__); printf("\n"); fflush(stdout); }

const int WAIT_INTERVAL = 5;