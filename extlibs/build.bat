@echo off

if not defined DevEnvDir (
    call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvarsall.bat" x64 || goto :error_before_build
)

if not exist build mkdir build
cd build

cmake -G "Visual Studio 15 2017" -A x64 .. || goto :error
msbuild build-extlibs.sln /p:Configuration=Release /p:Platform=x64 /m || goto :error

msbuild ..\SFML.Net\build\vc2010\SFML.net.sln /p:Configuration=Release /p:Platform=x64 /t:sfml-graphics /t:sfml-window /t:sfml-system /t:sfml-audio /m || goto :error

if not exist ..\lib mkdir ..\lib
copy bin\*.dll ..\lib\ /Y > nul || goto :error

cd ..

goto :EOF
:error
cd ..
:error_before_build
exit /b %errorlevel%
