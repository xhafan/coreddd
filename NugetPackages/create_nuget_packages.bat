cd CoreDdd
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreDdd.Nhibernate
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreDdd.Nhibernate.Register.Castle
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreDdd.Register.Castle
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreIntegrationTest.Nhibernate
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreIoC
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreIoC.Castle
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreNserviceBusTest
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreTest
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreUtils
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreWeb
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\
