CC = gcc
CFLAGS = -Wall -O -std=gnu11
LDLIBS = -lm
RM =


default: out.txt

debug: CFLAGS+=-DDEBUG_ON
debug: out.txt

.PHONY: default clean debug


out.txt: main
	./$< > $@
	cat $@

main: main.o vector.o
	$(CC) $^ $(LDLIBS) -o $@

main.o: main.c vector.h
	$(CC) $(CFLAGS) -c $< -o $@

vector.o: vector.c vector.h
	$(CC) $(CFLAGS) -c $< -o $@

clean:
	rm *.txt *.o ./main