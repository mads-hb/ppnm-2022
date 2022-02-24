OBJ = obj
SRC = src
INC = include

SRCS = $(wildcard $(SRC)/*.c)
OBJS = $(patsubst $(SRC)/%.c, $(OBJ)/%.o, $(SRCS))

CC = gcc
CFLAGS = -g -Wall -O -std=c11 -I$(INC) -pthread
LDLIBS = -lm
RM = main $(OBJ)/* 

.PHONY: default clean

default: out.txt

out.txt: main
	./$^ > $@
	cat $@

main: $(OBJS)
	$(CC) $(CFLAGS) $(LDLIBS) $(OBJS) -o $@ 

$(OBJ)/%.o: $(SRC)/%.c
	$(CC) $(CFLAGS) $(LDLIBS) -c $< -o $@ 
clean:
	rm -rf $(RM)
