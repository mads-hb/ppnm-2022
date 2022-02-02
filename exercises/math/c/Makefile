CC = gcc
CFLAGS = -O -std=gnu11
LDLIBS = -lm

# Set up phony targets
.PHONY: default clean
default: out.txt

clean:
	rm *.txt *.o ./main

out.txt: main
	./$< > $@
	cat $@

main: main.o
	$(CC) -o $@ $< $(LDLIBS)

main.o: main.c
	$(CC) $(CFLAGS) -c $<

