# MsTest

tldr;

```
dotnet test --logger:console;verbosity=detailed --diag Diagnostics/diagnostics.log;tracelevel=verbose --output Output --results-directory Results --configuration debug /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=CodeCoverage/coverlet.xml
```

--logger:console[;verbosity=<Defaults to "minimal">]
Argument "verbosity" define the verbosity level of console logger. 
Allowed values for verbosity are "quiet", "minimal", "normal" and "detailed".

-d, --diag <LOG_FILE>                    Enable verbose logging to the specified file.
--diag diagnostics.log[;tracelevel=<Defaults to "verbose">]
Allowed values for tracelevel: off, error, warning, info and verbose.

-o, --output <OUTPUT_DIR>                The output directory to place built artifacts in.
-r, --results-directory <RESULTS_DIR>    The directory where the test results will be placed.
                                        The specified directory will be created if it does not exist.
--collect <DATA_COLLECTOR_NAME>          The friendly name of the data collector to use for the test run.
                                        More info here: https://aka.ms/vstest-collect



## Output messages in tests

The output from the default test command `dotnet test` may not be very helpful.


A sample output

```
C:\src\github.com\ongzhixian\csi\Csi.Data.Tests>dotnet test
Test run for C:\src\github.com\ongzhixian\csi\Csi.Data.Tests\bin\Debug\netcoreapp2.1\Csi.Data.Tests.dll(.NETCoreApp,Version=v2.1)
Microsoft (R) Test Execution Command Line Tool Version 16.0.1
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

Total tests: 4. Passed: 4. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 1.5707 Seconds
```




```
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace Csi.Data.Tests
{
    [TestClass]
    public class SampleTests
    {
        [TestMethod]
        public void OutputTest()
        {
            // Arrange
            // Act
            // Assert

            System.Console.WriteLine("Console WriteLine message");

            System.Diagnostics.Debug.WriteLine("Debug WriteLine message");

            System.Diagnostics.Trace.WriteLine("Trace WriteLine message");

            Logger.LogMessage("A log message");

        }
    }
}
```

Output that you will see when running verbose test.
Note that there's no "Trace WriteLine message".

```
Standard Output Messages:
 Console WriteLine message
 A log message


Debug Trace:
Trace WriteLine message
```

## Reference
https://github.com/Microsoft/vstest-docs/blob/master/docs/report.md#syntax-of-default-loggers

Test platform diagnostics
https://github.com/microsoft/vstest-docs/blob/master/docs/diagnose.md

Monitor and analyze test run
https://github.com/Microsoft/vstest-docs/blob/master/docs/analyze.md

Unit testing best practices with .NET Core and .NET Standard
https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices