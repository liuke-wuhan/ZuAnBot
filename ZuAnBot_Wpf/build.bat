::ZuAnBot_Wpf安装nuget包：Costura.Fody
dotnet add package Costura.Fody

::ZuAnBotUpdate安装nuget包：Costura.Fody
cd ..
cd ZuAnBotUpdate
dotnet add package Costura.Fody

::release模式生成exe
cd ..
cd ZuAnBot_Wpf
dotnet build --configuration Release

::ZuAnBotUpdate卸载nuget包：Costura.Fody
cd ..
cd ZuAnBotUpdate
dotnet remove package Costura.Fody

::ZuAnBot_Wpf安装nuget包：Costura.Fody
cd ..
cd ZuAnBot_Wpf
dotnet remove package Costura.Fody

pause