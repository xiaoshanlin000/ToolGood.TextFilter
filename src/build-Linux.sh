
cd  ToolGood.TextFilter.Common

dotnet build -c Release

cd ..

mkdir -p ./libs/Release/

cp -r ./ToolGood.TextFilter.Common/bin/Release/net7.0/*.* ./libs/Release/

cp -r ./ToolGood.TextFilter.Common/bin/Release/net6.0/*.* ./libs/Release/
 

cd ToolGood.TextFilter.Website


dotnet publish -c Linux -p:PublishProfile=Linux  -o bin/Release-Linux
