set -e

cd ../extlibs/
./build.sh
cd ../build

mkdir -p ../lib
yes | cp -p ../extlibs/SFML.Net/lib/*.dll ../lib/

dotnet build TGUI.net.sln --configuration=Release

yes | cp -p ../src/bin/Release/netstandard2.0/tgui.net.dll ../lib/
yes | cp -rp ../extlibs/TGUI/themes ..
yes | cp -rp ../extlibs/TGUI/gui-builder ..

dotnet build ../examples/examples.sln --configuration=Release

echo Build finished
