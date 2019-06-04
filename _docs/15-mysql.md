# Notes on MySql

## Defining tables

Collation: You will probably want to use use `utf8mb4_unicode_ci`
Engine:    You will probably want to use use `InnoDB`

```
CREATE TABLE `Project` (
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(50) NOT NULL,
	PRIMARY KEY (`Id`)
)
COLLATE='utf8mb4_unicode_ci'
;
```

## Sql Engines

The SHOW ENGINES command shows all available engines that the server supports.

```
mysql> SHOW ENGINES\G
```

CSV                         CSV stores data in CSV files. 
                            It provides great flexibility because data in this format is easily integrated into other applications.
Blackhole                   The Blackhole storage engine accepts but does not store data. 
                            Retrievals always return an empty set. 
                            The functionality can be used in distributed database design where data is automatically replicated, but not stored locally. This storage engine can be used to perform performance tests or other testing.
MyISAM                      MyISAM is the original storage engine. It is a fast storage engine. It does not support transactions. MyISAM                             provides table-level locking. It is used mostly in Web and data warehousing.
MRG_MYISAM                  Merge operates on underlying MyISAM tables. 
                            Merge tables help manage large volumes of data more easily. 
                            It logically groups a series of identical MyISAM tables, and references them as one object. 
                            Good for data warehousing environments.
MEMORY                      Memory storage engine creates tables in memory. It is the fastest engine. 
                            It provides table-level locking. 
                            It does not support transactions. 
                            Memory storage engine is ideal for creating temporary tables or quick lookups. 
ARCHIVE                     Archive storage engine is optimised for high speed inserting. 
                            It compresses data as it is inserted. 
                            It does not support transactions. 
                            It is ideal for storing and retrieving large amounts of seldom referenced historical, archived data.
PERFORMANCE_SCHEMA          The MySQL Performance Schema is a feature for monitoring MySQL Server execution at a low level.
                            Tables in the Performance Schema are in-memory tables that use no persistent on-disk storage. The contents are repopulated beginning at server startup and discarded at server shutdown.
InnoDB                      InnoDB is the most widely used storage engine with transaction support. 
                            It is an ACID compliant storage engine. 
                            It supports row-level locking, crash recovery and multi-version concurrency control. 
                            It is the only engine which provides foreign key referential integrity constraint. 
                            Oracle recommends using InnoDB for tables except for specialized use cases.

## Data Types

In MySql there are three main data types: string, numeric, and date and time.

String data types:

* CHAR(size)	    A FIXED length string (can contain letters, numbers, and special characters). 
                    The size parameter specifies the column length in characters - can be from 0 to 255. Default is 1
* VARCHAR(size)     A VARIABLE length string (can contain letters, numbers, and special characters). 
                    The size parameter specifies the maximum column length in characters - can be from 0 to 65535
* BINARY(size)	    Equal to CHAR(), but stores binary byte strings. 
                    The size parameter specifies the column length in bytes. Default is 1
* VARBINARY(size)   Equal to VARCHAR(), but stores binary byte strings. The size parameter specifies the maximum column length in bytes.
TINYBLOB	For BLOBs (Binary Large OBjects). Max length: 255 bytes
TINYTEXT	Holds a string with a maximum length of 255 characters
TEXT(size)	Holds a string with a maximum length of 65,535 bytes
BLOB(size)	For BLOBs (Binary Large OBjects). Holds up to 65,535 bytes of data
MEDIUMTEXT	Holds a string with a maximum length of 16,777,215 characters
MEDIUMBLOB	For BLOBs (Binary Large OBjects). Holds up to 16,777,215 bytes of data
LONGTEXT	Holds a string with a maximum length of 4,294,967,295 characters
LONGBLOB	For BLOBs (Binary Large OBjects). Holds up to 4,294,967,295 bytes of data
ENUM(val1, val2, val3, ...)	A string object that can have only one value, chosen from a list of possible values. You can list up to 65535 values in an ENUM list. If a value is inserted that is not in the list, a blank value will be inserted. The values are sorted in the order you enter them
SET(val1, val2, val3, ...)	A string object that can have 0 or more values, chosen from a list of possible values. You can list up to 64 values in a SET list

Numeric data types:

BIT(size)	A bit-value type. The number of bits per value is specified in size. The size parameter can hold a value from 1 to 64. The default value for size is 1.
TINYINT(size)	A very small integer. Signed range is from -128 to 127. Unsigned range is from 0 to 255. The size parameter specifies the maximum display width (which is 255)
BOOL	Zero is considered as false, nonzero values are considered as true.
BOOLEAN	Equal to BOOL
SMALLINT(size)	A small integer. Signed range is from -32768 to 32767. Unsigned range is from 0 to 65535. The size parameter specifies the maximum display width (which is 255)
MEDIUMINT(size)	A medium integer. Signed range is from -8388608 to 8388607. Unsigned range is from 0 to 16777215. The size parameter specifies the maximum display width (which is 255)
INT(size)	A medium integer. Signed range is from -2147483648 to 2147483647. Unsigned range is from 0 to 4294967295. The size parameter specifies the maximum display width (which is 255)
INTEGER(size)	Equal to INT(size)
BIGINT(size)	A large integer. Signed range is from -9223372036854775808 to 9223372036854775807. Unsigned range is from 0 to 18446744073709551615. The size parameter specifies the maximum display width (which is 255)
FLOAT(p)	A floating point number. MySQL uses the p value to determine whether to use FLOAT or DOUBLE for the resulting data type. If p is from 0 to 24, the data type becomes FLOAT(). If p is from 25 to 53, the data type becomes DOUBLE()
DOUBLE(size, d)	A normal-size floating point number. The total number of digits is specified in size. The number of digits after the decimal point is specified in the d parameter
DECIMAL(size, d)	An exact fixed-point number. The total number of digits is specified in size. The number of digits after the decimal point is specified in the d parameter. The maximum number for size is 65. The maximum number for d is 30. The default value for size is 10. The default value for d is 0.
DEC(size, d)	Equal to DECIMAL(size,d)


Date time data types

DATE	A date. Format: YYYY-MM-DD. The supported range is from '1000-01-01' to '9999-12-31'
DATETIME(fsp)	A date and time combination. Format: YYYY-MM-DD hh:mm:ss. The supported range is from '1000-01-01 00:00:00' to '9999-12-31 23:59:59'. Adding DEFAULT and ON UPDATE in the column definition to get automatic initialization and updating to the current date and time
TIMESTAMP(fsp)	A timestamp. TIMESTAMP values are stored as the number of seconds since the Unix epoch ('1970-01-01 00:00:00' UTC). Format: YYYY-MM-DD hh:mm:ss. The supported range is from '1970-01-01 00:00:01' UTC to '2038-01-09 03:14:07' UTC. Automatic initialization and updating to the current date and time can be specified using DEFAULT CURRENT_TIMESTAMP and ON UPDATE CURRENT_TIMESTAMP in the column definition
TIME(fsp)	A time. Format: hh:mm:ss. The supported range is from '-838:59:59' to '838:59:59'
YEAR	A year in four-digit format. Values allowed in four-digit format: 1901 to 2155, and 0000.
MySQL 8.0 does not support year in two-digit format.

## MySQL Boolean data type

MySQL does not have the built-in  BOOLEAN or BOOL data type. 
To represent Boolean values, MySQL uses the smallest integer type which isTINYINT(1). 
In other words, BOOLEAN and BOOL are synonyms for TINYINT(1).

