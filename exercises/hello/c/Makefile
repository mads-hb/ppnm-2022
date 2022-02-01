CC = gcc
CFLAGS = -O -std=gnu11
LDLIBS = -lm

# Set up phony targets
.PHONY: default clean
default: out.txt

clean:
	rm *.txt *.o ./hello

out.txt: hello
	./$< > $@
	cat $@

hello: hello.o
	$(CC) -o $@ $< $(LDLIBS)

hello.o: hello.c
	$(CC) $(CFLAGS) -c $<

