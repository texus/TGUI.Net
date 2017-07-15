set -e

mkdir -p build
cd build

cmake ..
make -j2

cp -pR include ../include
cp -pR lib ../lib

cd ..

echo 'Warning: SFML.Net is currently not built by this script and has to be built manually'
