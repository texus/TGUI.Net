set -e

cd ../extlibs/
./build.sh
cd ../build

dotnet build TGUI.net.sln

mkdir -p ../lib
cp -pu ../src/bin/Debug/netstandard2.0/*.dll ../src/bin/Debug/netstandard2.0/*.pdb ../lib/
cp -rpu ../extlibs/TGUI/themes ..
cp -rpu ../extlibs/TGUI/gui-builder ..
