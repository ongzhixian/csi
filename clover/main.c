#include <stdio.h>
#include <syslog.h>


void program_init() {
    printf("[PROGRAM START]\n");
    
}

void program_dispose() {
    printf("[PROGRAM END]\n");
}

void syslog_example() {
    // Setup syslog 
    setlogmask (LOG_UPTO (LOG_NOTICE));
    openlog ("clover", LOG_CONS | LOG_PID | LOG_NDELAY, LOG_LOCAL1);

    syslog (LOG_NOTICE, "Program started by User %d", 213);
    syslog (LOG_INFO, "A tree falls in a forest");
    
    closelog ();
}

int main(int argc, char *argv[]) {
    program_init();

    // DoWork()
    
    program_dispose();
    return 0;
}