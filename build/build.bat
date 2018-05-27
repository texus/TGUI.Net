@echo off

if not defined DevEnvDir (
    call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvarsall.bat" x64 || goto :error_before_build
)

pushd ..\extlibs\
call build.bat || goto :error
popd

dotnet build TGUI.net.sln --configuration=Release || goto :error

if not exist ..\lib mkdir ..\lib
copy ..\src\bin\Release\netstandard2.0\*.dll ..\lib\ /Y > nul || goto :error
xcopy ..\extlibs\TGUI\themes ..\themes /Y /s /i > nul || goto :error
xcopy ..\extlibs\TGUI\gui-builder ..\gui-builder /Y /s /i > nul || goto :error
copy ..\extlibs\lib\sfml-system-2.dll ..\gui-builder\ /Y > nul || goto :error
copy ..\extlibs\lib\sfml-window-2.dll ..\gui-builder\ /Y > nul || goto :error
copy ..\extlibs\lib\sfml-graphics-2.dll ..\gui-builder\ /Y > nul || goto :error
copy ..\extlibs\lib\tgui.dll ..\gui-builder\ /Y > nul || goto :error

dotnet build ..\examples\examples.sln --configuration=Release || goto :error

echo Build finished
goto :EOF
:error
exit /b %errorlevel%
