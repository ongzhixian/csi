# Debug UART

1. The DEBUG UART is typically the highest numbered COM port (of the three new COM ports) reported by Windows Device Manager, for the Starter Kit’s FTDI USB interface

2. To view the output of this serial port, open Tera Term (or other serial console application, eg putty) and configure it for 

* the noted COM number, 
* with UART set for 115200 8N1 communication rate

3) Connect the Tera Term terminal then press the RESET button on the Starter Kit. Startup debug text similar to the following should appear on the terminal screen
4) 

Note! Terminal connection to the Debug Interface must be closed before attempting to use the RECOVERY interface! That is to say, the following commands:

azsphere device show-ota-status
azsphere device recover --images <path to OS Recovery Images>