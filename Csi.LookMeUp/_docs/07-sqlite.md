# SQLite

Each value stored in an SQLite database (or manipulated by the database engine) has one of the following storage classes:

NULL. The value is a NULL value.

INTEGER. The value is a signed integer, stored in 1, 2, 3, 4, 6, or 8 bytes depending on the magnitude of the value.

REAL. The value is a floating point value, stored as an 8-byte IEEE floating point number.

TEXT. The value is a text string, stored using the database encoding (UTF-8, UTF-16BE or UTF-16LE).

BLOB. The value is a blob of data, stored exactly as it was input.

--- 
## Data Types

Boolean

    Boolean values are stored as integers 0 (false) and 1 (true).

Date and Time Datatype

    TEXT    as ISO8601 strings ("YYYY-MM-DD HH:MM:SS.SSS").
    REAL    as Julian day numbers, the number of days since noon in Greenwich on November 24, 4714 B.C. 
            according to the proleptic Gregorian calendar.
    INTEGER as Unix Time, the number of seconds since 1970-01-01 00:00:00 UTC.
