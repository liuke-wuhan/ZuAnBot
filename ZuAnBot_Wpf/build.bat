::安装nuget包：Costura.Fody
dotnet add package Costura.Fody

::release模式生成exe
dotnet build --configuration Release

::卸载nuget包：Costura.Fody
dotnet remove package Costura.Fody

pause