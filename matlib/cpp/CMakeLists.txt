cmake_minimum_required(VERSION 3.20)  # CMake version check
project(matlib)               # Create project "simple_example"
set(CMAKE_CXX_STANDARD 14)            # Enable c++14 standard

include_directories(include)
add_subdirectory(src)

list(APPEND LIBRARIES project_lib)

# Add main.cpp file of project root directory as source file
set(SOURCE_FILES main.cpp)

# Add executable target with source files listed in SOURCE_FILES variable
add_executable(main ${SOURCE_FILES})
target_link_libraries(main ${LIBRARIES})
target_include_directories(main PRIVATE
        "${PROJECT_BINARY_DIR}"
        "${PROJECT_SOURCE_DIR}/include/"
        )