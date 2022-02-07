# Define source code directory
SRC_DIR = src
# Define header directory
INC_DIR = include

CC = gcc # GNU
RM = *.o out*.txt *.png *.a ./main
LDLIBS = -lm -lgsl # Include math and gsl libraries
CFLAGS = -Wall -O -std=c11 -I$(INC_DIR)# Print all warnings


.PHONY: default clean

## Default target is plots for all three exercises.
default: out1.png out2.png out3.png


## Draw pyxplot images.
out1.png: exerciseA.txt out1.txt
	pyxplot $<

out2.png: exerciseB.txt out2.txt
	pyxplot $<

out3.png: exerciseC.txt out3.txt
	pyxplot $<



## Run main program and produce interps in out*.txt files.
out1.txt out2.txt out3.txt: main
	./$<

# Link main.o with interp library
main: main.o interp.a
	$(CC) -o $@ $(CFLAGS) $^ $(LDLIBS)

# Compile main.c to object.
main.o: main.c
	$(CC) -o $@ $(CFLAGS) -c $^


## Compile lib to use
interp.a: linterp.o qspline.o cspline.o
	ar rcs $@ $^
	rm $^

%.o: $(SRC_DIR)/%.c
	$(CC) $(CFLAGS) -c $< -o $@


clean:
	rm $(RM)