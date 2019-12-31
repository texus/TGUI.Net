set -e

mkdir -p build
cd build

cmake ..
make -j2

cp -pR lib ../
cd ..

cd SFML.Net
mkdir -p lib
dotnet build -c Release || goto :error
yes | cp -p src/SFML.Audio/bin/Release/netstandard2.0/* lib/
cd ..
