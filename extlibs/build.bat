@echo off

set VSVersion="Visual Studio 15 2017"

if not defined DevEnvDir (
  if not exist "%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" (
    echo "You need VS 2017 version 15.2 or later"
    exit /b 1
  )

  for /f "usebackq tokens=*" %%i in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath`) do (
    set InstallDir=%%i
  )
)
if not defined DevEnvDir (
  if exist "%InstallDir%\VC\Auxiliary\Build\vcvars64.bat" (
    call "%InstallDir%\VC\Auxiliary\Build\vcvars64.bat"
  )   else (
    echo "Could not find %InstallDir%\VC\Auxiliary\Build\vcvars64.bat"
    exit /b 1
  )
)


if not exist build mkdir build
cd build

cmake -G %VSVersion% -A x64 .. || goto :error
msbuild build-extlibs.sln /p:Configuration=Release /p:Platform=x64 /m || goto :error

if not exist ..\lib mkdir ..\lib
copy bin\*.dll ..\lib\ /Y > nul || goto :error
cd ..

cd SFML.Net
if not exist lib mkdir lib
dotnet build build\SfmlCore\SfmlCore.sln --configuration=Release || goto :error
copy src\Graphics\bin\Release\netstandard2.0\*.dll lib\ /Y > nul || goto :error
cd ..

goto :EOF
:error
set StoredErrorLevel=%errorlevel%
cd ..
exit /b %StoredErrorLevel%
