REM Shortcut to run dotnet test with all the various parameters

dotnet test --logger:console;verbosity=detailed --diag Diagnostics/diagnostics.log;tracelevel=verbose --output Output --results-directory Results --configuration debug /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=CodeCoverage/coverlet.xml
REM Because the above command uses the --output parameter,
REM Any config file(s) that we reference in the unit test should be copied there as well.

REM Instead of generating the report every time we test, assign this role to mk.cmd
REM reportgenerator "-reports:CodeCoverage/coverlet.xml" "-targetdir:CodeCoverage"