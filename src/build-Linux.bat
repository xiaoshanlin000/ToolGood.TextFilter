
cd  ToolGood.TextFilter.Common

dotnet build -c Release

cd ..

xcopy /y ".\ToolGood.TextFilter.Common\bin\Release\net5.0\*.*" ".\libs\Release\"

xcopy /y ".\ToolGood.TextFilter.Common\bin\Release\net6.0\*.*" ".\libs\Release\"
 

cd ToolGood.TextFilter.Website


dotnet publish -c Linux -p:PublishProfile=Linux  -o bin/Release-Linux

pause