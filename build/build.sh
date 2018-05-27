set -e

cd ../extlibs/
./build.sh
cd ../build

dotnet build TGUI.net.sln --configuration=Release

mkdir -p ../lib
cp -pu ../src/bin/Release/netstandard2.0/*.dll ../lib/
cp -rpu ../extlibs/TGUI/themes ..
cp -rpu ../extlibs/TGUI/gui-builder ..

dotnet build ../examples/examples.sln --configuration=Release
