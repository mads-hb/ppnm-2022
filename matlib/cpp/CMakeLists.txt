cmake_minimum_required(VERSION 3.20)  # CMake version check
project(matlib)               # Create project "simple_example"
set(CMAKE_CXX_STANDARD 14)            # Enable c++14 standard

# Tell cmake where to look for cmake modules..
set(CMAKE_MODULE_PATH "${CMAKE_MODULE_PATH};${CMAKE_SOURCE_DIR}/cmake")


include_directories(include)
add_subdirectory(src)

list(APPEND LIBRARIES project_lib)

############################################
##             Unit Testing               ##
############################################
find_package(UnitTest++)

if(UNITTEST_FOUND)
    message(STATUS "Compiling unit test")
    file(GLOB_RECURSE TEST_SRC
            "tests/*.h"
            "tests/*.cpp")
    add_executable(MatLibTest ${TEST_SRC})
    target_include_directories(MatLibTest PUBLIC ${CMAKE_CURRENT_BINARY_DIR})
    target_link_libraries(MatLibTest ${UNITTEST_LIBRARIES} ${LIBRARIES})
else(UNITTEST_FOUND)
    message(STATUS "Not compiling unit tests. Add libunittest++-dev to your system if you want this")
endif(UNITTEST_FOUND)

# Add main.cpp file of project root directory as source file
set(SOURCE_FILES main.cpp)

# Add executable target with source files listed in SOURCE_FILES variable
add_executable(main ${SOURCE_FILES})
target_link_libraries(main ${LIBRARIES})
target_include_directories(main PRIVATE
        "${PROJECT_BINARY_DIR}"
        "${PROJECT_SOURCE_DIR}/include/"
        )