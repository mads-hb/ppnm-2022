CC = gcc
CFLAGS = -Wall -O -std=gnu11
RM = *.o out*.txt read_cmdline read_file read_stdin
LDLIBS = -lm


.PHONY: default clean
default: read_cmdline read_stdin read_file 


## read_cmdline
read_cmdline: read_cmdline.o write_data.o
	$(CC) -o $@ $(CFLAGS) $^ $(LDLIBS)
	./$@ $(cat input.txt) > out1.txt

read_cmdline.o: read_cmdline.c
	$(CC) -o $@ $(CFLAGS) -c $^

## read_stdin
read_stdin: read_stdin.o write_data.o
	$(CC) -o $@ $(CFLAGS) $^ $(LDLIBS) 
	./$@ < input.txt > out2.txt

read_stdin.o: read_stdin.c
	$(CC) -o $@ $(CFLAGS) -c $^

## read_file
read_file: read_file.o write_data.o
	$(CC) -o $@ $(CFLAGS) $^ $(LDLIBS) 
	./$@ input.txt out3.txt

read_file.o: read_file.c
	$(CC) -o $@ $(CFLAGS) -c $^


## Compile lib to use
write_data.o: write_data.c
	$(CC) -o $@ $(CFLAGS) -c $^


clean:
	rm $(RM)