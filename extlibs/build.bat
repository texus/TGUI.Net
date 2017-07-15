@echo off

if not defined DevEnvDir (
    call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvarsall.bat" x64 || goto :error_before_build
)

if not exist "build" mkdir "build"
cd build

cmake -G "Visual Studio 15 2017" -A x64 .. || goto :error
msbuild build-extlibs.sln /p:Configuration=Release /p:Platform=x64 /m || goto :error

msbuild ..\SFML.Net\build\vc2010\SFML.net.sln /p:Configuration=Release /p:Platform=x64 /t:sfml-graphics /t:sfml-window /t:sfml-system /t:sfml-audio /m || goto :error
copy ..\SFML.Net\lib\x64\*.dll bin /Y > nul || goto :error

if not exist "..\lib" mkdir "..\lib"
copy lib\*.lib ..\lib /Y > nul || goto :error
xcopy include ..\include /Y /s /i > nul || goto :error
xcopy bin ..\bin /Y /s /i > nul || goto :error

cd ..

echo Done
goto :EOF
:error
cd ..
:error_before_build
exit /b %errorlevel%
