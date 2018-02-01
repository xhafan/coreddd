cd ..\CoreIntegrationTest.Nhibernate
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreNserviceBusTest
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\

cd ..\CoreTest
..\..\.nuget\nuget.exe pack Package.nuspec -OutputDirectory ..\
