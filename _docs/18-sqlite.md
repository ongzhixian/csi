# SQLite

## Data Types

Integer
Text
Blob
Real
Numeric

## Data Types (workarounds)

*   Boolean
    Boolean values are stored as integers 0 (false) and 1 (true).

*   Date/Time

    SQLite does not have a storage class set aside for storing dates and/or times. Instead, the built-in Date And Time Functions of SQLite are capable of storing dates and times as TEXT, REAL, or INTEGER values:

    TEXT as ISO8601 strings ("YYYY-MM-DD HH:MM:SS.SSS").
    REAL as Julian day numbers, the number of days since noon in Greenwich on November 24, 4714 B.C. according to the      proleptic Gregorian calendar.
    INTEGER as Unix Time, the number of seconds since 1970-01-01 00:00:00 UTC.

## Create Table

CREATE TABLE "Project" (
	"id"	    INTEGER PRIMARY KEY AUTOINCREMENT,
	"name"	    TEXT NOT NULL,
    "start_date" TEXT,
    "end_date"   TEXT,
    "status"    TEXT,
);
