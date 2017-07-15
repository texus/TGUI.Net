cmake_minimum_required(VERSION 3.5)
project(build-extlibs)
include(ExternalProject)

ExternalProject_Add(sfml
                    SOURCE_DIR ${PROJECT_SOURCE_DIR}/SFML
                    CMAKE_ARGS "-DCMAKE_INSTALL_PREFIX=${PROJECT_BINARY_DIR};-DCMAKE_BUILD_TYPE=Release;-DBUILD_SHARED_LIBS=TRUE")

ExternalProject_Add(csfml
                    DEPENDS sfml
                    SOURCE_DIR ${PROJECT_SOURCE_DIR}/CSFML
                    CMAKE_ARGS "-DCMAKE_INSTALL_PREFIX=${PROJECT_BINARY_DIR};-DCMAKE_MODULE_PATH=${PROJECT_SOURCE_DIR}/SFML/cmake/Modules;-DCMAKE_BUILD_TYPE=Release;-DBUILD_SHARED_LIBS=TRUE;-DCSFML_LINK_SFML_STATICALLY=FALSE")

ExternalProject_Add(tgui
                    DEPENDS sfml
                    SOURCE_DIR ${PROJECT_SOURCE_DIR}/TGUI
                    CMAKE_ARGS "-DCMAKE_INSTALL_PREFIX=${PROJECT_BINARY_DIR};-DCMAKE_BUILD_TYPE=Release;-DTGUI_SHARED_LIBS=TRUE")

ExternalProject_Add(ctgui
                    DEPENDS sfml csfml tgui
                    SOURCE_DIR ${PROJECT_SOURCE_DIR}/CTGUI
                    CMAKE_ARGS "-DCMAKE_INSTALL_PREFIX=${PROJECT_BINARY_DIR};-DCMAKE_BUILD_TYPE=Release;-DBUILD_SHARED_LIBS=TRUE;-DCTGUI_LINK_DEPENDENCIES_STATICALLY=FALSE")

