SRC = src
RM = *.dll *.exe

.PHONY: default clean
default: out.txt


# Run program using mono and write to txt. Display outputs.
out.txt: main.exe
	mono $< > $@
	cat $@

# Compile main.cs into bytecode and link to library cmath.dll
main.exe: main.cs cmath.dll
	mcs -reference:$(filter %dll,$^) -out:$@ $<

# Compile cmath.cs and complex into a library
cmath.dll: $(wildcard $(SRC)/*.cs)
	mcs -target:library -out:$@ $^

clean:
	rm -rf $(RM)