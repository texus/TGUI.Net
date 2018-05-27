set -e

cd ../extlibs/
./build.sh
cd ../build

dotnet build TGUI.net.sln --configuration=Release

mkdir -p ../lib
yes | cp -p ../src/bin/Release/netstandard2.0/*.dll ../lib/
yes | cp -rp ../extlibs/TGUI/themes ..
yes | cp -rp ../extlibs/TGUI/gui-builder ..

dotnet build ../examples/examples.sln --configuration=Release

echo Build finished
