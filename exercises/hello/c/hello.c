#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>


int main(){
	// Declare pointer to hold username
	char *buf;
	// Allocate memory
	buf = (char *) malloc(10*sizeof(buf));
	// Get user name by copying the return value of
	// getlogin() into the memoery pointer created by malloc
	strcpy(buf, getlogin());
	// Print
	printf("Hello, %s!\n", buf);
	// Deallocate memory
	free(buf);
	return 0;
}
