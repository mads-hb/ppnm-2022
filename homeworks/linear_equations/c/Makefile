BIN = bin
OBJ = obj
SRC = src
INC = include

SRCS = $(wildcard $(SRC)/*.c)
OBJS = $(patsubst $(SRC)/%.c, $(OBJ)/%.o, $(SRCS))

CC = gcc
CFLAGS = -g -Wall -O -std=c11 -I$(INC) 
LDLIBS = -lm -lgsl
RM = main $(OBJ)/* timing.txt

.PHONY: default clean

default: out.txt out.png

out.png: exerciseC.txt timing.txt
	pyxplot $<


out.txt timing.txt: main
	./$^ > $@
	cat $@

main: $(OBJS)
	$(CC) $(CFLAGS) $(OBJS) -o $@ $(LDLIBS)

$(OBJ)/%.o: $(SRC)/%.c
	$(CC) $(CFLAGS) -c $< -o $@ $(LDLIBS)

clean:
	rm -rf $(RM)
