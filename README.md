TGUI.Net
========

TGUI.Net is a .Net binding for the [TGUI library](https://github.com/texus/TGUI).


Build
-----

Note: These steps are only needed when building from source. It is recommended to use a precompiled version.

First make sure that the submodules are initialized (the folders in the `extlibs` folder should not be empty). If these folders are still empty, run the following command:

``` c++
git submodule update --init --recursive
```

Next, go into the `build` folder and edit the `build.bat` file. At the top it calls a vcvarsall.bat script with a hardcoded path. Verify that this path is correct and change the path according to your Visual Studio installation if necessary.

Then simply run `build.bat` to build TGUI.Net and all of its dependencies.


Usage
-----

The platform of your project should be set to "x64". If it is set to "Any CPU", go into the Configuration Manager to add a new "x64" platform, copied from the "Any CPU" settings.

Add the files from the `lib/x64` folder to the references of your project.

Build the project so that the project folder contains a `bin/x64/Debug` or `bin/x64/Release` folder with the exe. Now copy the dlls from `extlibs/lib` next to this exe.

Make sure to use all dlls, not only the ones from TGUI. You must use the exact SFML.Net, CSFML and SFML files as provided with TGUI.Net.
