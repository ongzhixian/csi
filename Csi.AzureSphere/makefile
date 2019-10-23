########################################
# Define make variables
########################################

# the compiler: gcc for C program, define as g++ for C++
CC=gcc

# The version of SDK that we are using 1/2/3
SYSROOT=C:/Apps/AzureSphere/Sysroots/2

########################################
# Compilation flags
########################################

# -c 							# Compiles only; doesn’t link the program
# -x c  						# Explicitly identifies the source language as C
# -I "$(SYSROOT)\usr\include"	# Adds the specified directory to the list of directories to be searched for header files during preprocessing.
# -g2 							# Generates level 2 (default) debugging information.
# -gdwarf-2  					# Generates debugging information in DWARF format version 2.
# -Wall 						# Enables all warnings.

# -O0 							# Sets code optimization level 0 (default) to reduce compilation time and make debugging produce the expected results.
# -fno-strict-aliasing 			# Does not allow the compiler to assume the strictest C-language aliasing rules. This option disables optimizations based on the type of expressions.
# -fno-omit-frame-pointer 		# Stores the frame pointer in a register whether or not it is required.

# -D "_POSIX_C_SOURCE" 			# Defines _POSIX_C_SOURCE as a macro. (Preprocessor)

# -fno-exceptions 				# Disables exception handling.
# -std=c11 						# Uses the 2011 revision of the ISO C standard.

# -march=armv7ve 				# Specifies the armv7 architecture with virtualization exceptions.
# -mthumb 						# Generates code that executes in Thumb state.
# -mfpu=neon 					# Specifies NEON-vfpv3 as the floating-point hardware.
# -mfloat-abi=hard 				# Specifies the ‘hard’ floating-point ABI. The ‘hard’ ABI allows generation of floating-point instructions and uses FPU-specific calling conventions.
# -mcpu=cortex-a7 				# Specifies the name of the target ARM processor.

# --sysroot=$(SYSROOT)			# Uses dir as the logical root directory for headers and libraries.
# -o "main.o"					# Specifies the name of the output file.
# main.c


C_FLAGS=-c -x c -std=c11 -Wall --sysroot=$(SYSROOT) -D "_POSIX_C_SOURCE" -fno-exceptions
DEBUG_FLAGS=-g2 -gdwarf-2
OPTIMIZATION_FLAGS=-O0 -fno-strict-aliasing -fno-omit-frame-pointer
ARM_FLAGS=-march=armv7ve -mthumb -mfpu=neon -mfloat-abi=hard -mcpu=cortex-a7
HEADER_DIR=-I $(SYSROOT)/usr/include 
#COMPILE_FLAGS=-c -x c -I $(SYSROOT)\usr\include -g2 -gdwarf-2 -Wall -O0 -fno-strict-aliasing -fno-omit-frame-pointer -D "_POSIX_C_SOURCE" -fno-exceptions -std=c11 $(ARM_FLAGS) --sysroot=$(SYSROOT)
COMPILE_FLAGS=$(C_FLAGS) $(DEBUG_FLAGS) $(OPTIMIZATION_FLAGS) $(ARM_FLAGS) $(HEADER_DIR)

# Link flags

# --sysroot="C:\Apps\AzureSphere\Sysroots\2"      # Uses dir as the logical root directory for headers and libraries.
# -Wl,--no-undefined                              # Passes --no-undefined as a linker option, so that the linker reports any references that are unresolved after linking.
# -nodefaultlibs                                  # Does not use the standard system libraries when linking. Only the libraries you specify are passed to the linker.
# -B "C:\Apps\AzureSphere\Sysroots\2\tools\gcc"   # Specifies the path to the gcc compiler.

# -march=armv7ve                                  # Specifies the armv7 architecture with virtualization exceptions.
# -mcpu=cortex-a7                                 # Specifies the name of the target ARM processor.
# -mthumb                                         # Generates code that executes in Thumb state.
# -mfpu=neon                                      # Specifies NEON-vfpv3 as the floating-point hardware.
# -mfloat-abi=hard                                # Specifies the ‘hard’ floating-point ABI. The ‘hard’ ABI allows generation of floating-point instructions and uses FPU-specific calling conventions.

# -lapplibs                                       # Searches the applibs library during linking.
# -lpthread                                       # Searches the pthread library during linking.
# -lgcc_s                                         # Searches the gcc_s library during linking.
# -lc                                             # Searches the C library during linking.

LIB_DIR=-lapplibs -lpthread -lgcc_s -lc
LINK_FLAGS=--sysroot=$(SYSROOT) -Wl,--no-undefined -nodefaultlibs -B $(SYSROOT)/tools/gcc $(ARM_FLAGS) $(LIB_DIR)



# the build target executable:

TARGET=Blink.out


SRCS    := $(wildcard *.c)
OBJS    := $(patsubst %.c,%.o,$(SRCS))

########################################
# Define make process
########################################

all: $(TARGET)

$(TARGET): $(OBJS)
	@echo Building TARGET [$@] from [$^]
	@$(CC) $(LINK_FLAGS) -o $(TARGET) $^
	

%.o: %.c
	@echo -e Building [$@]    src:[$<]
	@$(CC) $(COMPILE_FLAGS) -o $@ $<

clean:
	@echo Cleaning...$@
	$(RM) $(TARGET)
	$(RM) $(OBJS)
