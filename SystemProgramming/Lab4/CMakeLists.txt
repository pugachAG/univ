cmake_minimum_required(VERSION 2.8.4)
project(Lab4)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

set(SOURCE_FILES
    main.cpp
    core.cpp
    core.h
    calculations_handler.cpp
    calculations_handler.h
    common.h)
add_executable(Lab4 ${SOURCE_FILES})
target_link_libraries(Lab4 pthread)