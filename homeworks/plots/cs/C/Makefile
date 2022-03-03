RM = *.exe *.dll
SRC = src


.PHONY: clean

out.png: plot.gpi out.txt
	pyxplot $<

out.txt: main.exe
	mono $^ > $@

# Compile main.cs into bytecode and link to library cmath.dll
main.exe: main.cs math.dll
	mcs -reference:$(filter %dll,$^) -out:$@ $<

# Compile cmath.cs and complex into a library
math.dll: $(wildcard $(SRC)/*.cs)
	mcs -target:library -out:$@ $^


clean:
	rm -rf $(RM)
