LIBDIR = src

.PHONY: default clean
default: out.txt
clean:
	rm *.txt *.dll *.exe

# Compile cmath.cs and complex into a library
cmath.dll: $(LIBDIR)/cmath.cs $(LIBDIR)/complex.cs
	mcs -target:library -out:cmath.dll $^

# Compile main.cs into bytecode and link to library cmath.dll
main.exe: main.cs cmath.dll
	mcs -reference:cmath.dll -out:$@ $<

# Run program using mono and write to txt. Display outputs.
out.txt: main.exe
	mono $< > $@
	cat $@
