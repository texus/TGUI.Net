set -e

mkdir -p build
cd build

cmake ..
make -j2

cp -pR lib ../
cd ..

cd SFML.Net
dotnet build build/SfmlCore/SfmlCore.sln
mkdir -p lib
cp -pR src/Graphics/bin/Debug/netstandard2.0/*.dll ../SFML.Net/src/Graphics/bin/Debug/netstandard2.0/*.pdb lib/
cd ..
