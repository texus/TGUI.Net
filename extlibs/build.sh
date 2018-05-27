set -e

mkdir -p build
cd build

cmake ..
make -j2

cp -pR lib ../
cd ..

cd SFML.Net
mkdir -p lib
dotnet build build/SfmlCore/SfmlCore.sln --configuration=Release
yes | cp -pR src/Graphics/bin/Release/netstandard2.0/*.dll lib/
cd ..
