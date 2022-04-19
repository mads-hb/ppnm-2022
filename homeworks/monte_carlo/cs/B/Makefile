MATLIB_SRC = ../matlib
MATLIB_FILES = $(wildcard $(MATLIB_SRC)/*.cs)
RM = *.dll *.exe

.PHONY: default clean

default: out.txt out.png

out.png: plot.gpi err.txt
	pyxplot $<

out.txt err.txt: main.exe
	mono $< > out.txt
	cat out.txt

main.exe: main.cs matlib.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<


matlib.dll: $(MATLIB_FILES)
	mcs -target:library -out:$@ $^


clean:
	rm -rf $(RM)