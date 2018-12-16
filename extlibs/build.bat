@echo off

set ARCH=x64
set VCVARS=vcvars64
FOR %%A IN (%*) DO (
  if %%A == x86 (
    set ARCH=Win32
    set VCVARS=vcvars32
  ) else (
    if NOT %%A == x64 (
      echo Unrecognised option: %%A
      goto :error
    )
  )
)

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
  if exist "%InstallDir%\VC\Auxiliary\Build\%VCVARS%.bat" (
    call "%InstallDir%\VC\Auxiliary\Build\%VCVARS%.bat"
  )   else (
    echo "Could not find %InstallDir%\VC\Auxiliary\Build\%VCVARS%.bat"
    exit /b 1
  )
)


if not exist build mkdir build
cd build
cmake -A %ARCH% .. || goto :error
msbuild build-extlibs.sln /p:Configuration=Release /p:Platform=%ARCH% /m || goto :error

if not exist ..\lib mkdir ..\lib
copy bin\*.dll ..\lib\ /Y > nul || goto :error
cd ..

cd SFML.Net
if not exist lib mkdir lib
dotnet build build\SfmlCore\SfmlCore.sln --configuration=Release || goto :error
copy src\Graphics\bin\Release\netstandard2.0\*.dll lib\ /Y > nul || goto :error
copy src\Audio\bin\Release\netstandard2.0\sfml-audio.dll lib\ /Y > nul || goto :error
cd ..

goto :EOF
:error
set StoredErrorLevel=%errorlevel%
cd ..
exit /b %StoredErrorLevel%
