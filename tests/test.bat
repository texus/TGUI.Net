@echo off

if not defined DevEnvDir (
    call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvarsall.bat" x64 || goto :error
)

python validateImports.py || goto :error

goto :EOF
:error
exit /b %errorlevel%
