# Overview

Some loggers use with Csi projects or unit tests.

1. Csi.Loggers.LinearConsoleLoggerProvider

Prints log output in a single line with no line breaks.
In contrast to the default .NET Console which prints in with line breaks like the below:

```Sample output
dbug: Csi.ExtensionsConsole.Program[0]
      LogLevel Debug         (dbug) test.
```

2. Csi.Loggers.TraceLoggerProvider

Log to Trace. Use for unit testing log messages.