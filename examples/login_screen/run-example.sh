#!/usr/bin/env bash

if [ "$(uname -s)" == "Darwin" ]; then
  DYLD_LIBRARY_PATH=../../extlibs/lib/ DYLD_FRAMEWORK_PATH=../../extlibs/lib/ dotnet run
elif [ "$(uname -s)" == "Linux" ]; then
  LD_LIBRARY_PATH=../../extlibs/lib/ dotnet run
else
  dotnet run
fi
