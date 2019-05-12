# MsTest

tldr;

```
dotnet test --logger:console;verbosity=detailed --diag diagnostics.log;tracelevel=verbose --output Output --results-directory Results --configuration debug
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

## Reference
https://github.com/Microsoft/vstest-docs/blob/master/docs/report.md#syntax-of-default-loggers

Test platform diagnostics
https://github.com/microsoft/vstest-docs/blob/master/docs/diagnose.md

Monitor and analyze test run
https://github.com/Microsoft/vstest-docs/blob/master/docs/analyze.md
