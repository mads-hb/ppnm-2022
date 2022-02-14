CC = gcc
CFLAGS = -g -Wall -O -std=c11 -fwrapv
LDLIBS = -lm -lgsl
RM = main *.o

.PHONY: default clean

default: out.txt


out.txt: main
	./$^ > $@
	cat $@

main: main.o
	$(CC) $(CFLAGS) $(LDLIBS) $< -o $@

%.o: %.c
	$(CC) $(CFLAGS) $(LDLIBS) -c $< -o $@

clean:
	rm -rf $(RM)
