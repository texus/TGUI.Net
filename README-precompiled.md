TGUI.Net
========

TGUI.Net is a .Net binding for the [TGUI library](https://github.com/texus/TGUI).


Usage
-----

The platform of your project should be set to "x64". If it is set to "Any CPU", go into the Configuration Manager to add a new "x64" platform, copied from the "Any CPU" settings.

Add the files from the `lib` folder to the references of your project.

Build the project so that the project folder contains a `bin/x64/Debug` or `bin/x64/Release` folder with the exe. Now copy the dlls from `extlibs` next to this exe.

Make sure to use all dlls, not only the ones from TGUI. You must use the exact SFML.Net, CSFML and SFML files as provided with TGUI.Net.
