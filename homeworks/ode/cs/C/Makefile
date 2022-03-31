MATLIB_SRC = ../matlib
MATLIB_FILES = $(wildcard $(MATLIB_SRC)/*.cs)
RM = *.dll *.exe

.PHONY: default clean

default: three_body.gif

three_body.gif: gif.gpi diff.txt
	gnuplot $<

diff.txt: main.exe
	mono $<

main.exe: main.cs matlib.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<


matlib.dll: $(MATLIB_FILES)
	mcs -target:library -out:$@ $^


clean:
	rm -rf $(RM)