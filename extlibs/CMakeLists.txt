cmake_minimum_required(VERSION 3.5)
project(build-extlibs)
include(ExternalProject)

ExternalProject_Add(sfml
                    SOURCE_DIR ${PROJECT_SOURCE_DIR}/SFML
                    CMAKE_ARGS "-DCMAKE_INSTALL_PREFIX=${PROJECT_BINARY_DIR};-DCMAKE_BUILD_TYPE=Release;-DBUILD_SHARED_LIBS=TRUE")

ExternalProject_Add(csfml
                    DEPENDS sfml
                    SOURCE_DIR ${PROJECT_SOURCE_DIR}/CSFML
                    CMAKE_ARGS "-DCMAKE_INSTALL_PREFIX=${PROJECT_BINARY_DIR};-DCMAKE_BUILD_TYPE=Release;-DBUILD_SHARED_LIBS=TRUE;-DCSFML_LINK_SFML_STATICALLY=FALSE;-DCMAKE_MODULE_PATH=${PROJECT_SOURCE_DIR}/TGUI/cmake/Modules;-DSFML_ROOT=${PROJECT_SOURCE_DIR}/build/sfml-prefix/src/sfml-build/lib/Release;-DSFML_INCLUDE_DIR=${PROJECT_SOURCE_DIR}/build/include")

ExternalProject_Add(tgui
                    DEPENDS sfml
                    SOURCE_DIR ${PROJECT_SOURCE_DIR}/TGUI
                    CMAKE_ARGS "-DCMAKE_INSTALL_PREFIX=${PROJECT_BINARY_DIR};-DCMAKE_BUILD_TYPE=Release;-DTGUI_SHARED_LIBS=TRUE;-DTGUI_BUILD_GUI_BUILDER=TRUE;-DTGUI_OPTIMIZE_SINGLE_BUILD=TRUE;-DSFML_DIR=${PROJECT_SOURCE_DIR}/build/sfml-prefix/src/sfml-build")

ExternalProject_Add(ctgui
                    DEPENDS csfml tgui
                    SOURCE_DIR ${PROJECT_SOURCE_DIR}/CTGUI
                    CMAKE_ARGS "-DCMAKE_INSTALL_PREFIX=${PROJECT_BINARY_DIR};-DCMAKE_BUILD_TYPE=Release;-DBUILD_SHARED_LIBS=TRUE;-DTGUI_DIR=${PROJECT_SOURCE_DIR}/build/tgui-prefix/src/tgui-build;-DSFML_DIR=${PROJECT_SOURCE_DIR}/build/sfml-prefix/src/sfml-build;-DCSFML_INCLUDE_DIR=${PROJECT_SOURCE_DIR}/build/include")

