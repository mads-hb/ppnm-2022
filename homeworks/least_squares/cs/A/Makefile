SRC = src
SRC_FILES = $(wildcard $(SRC)/*.cs)
RM = *.dll *.exe

.PHONY: default clean

default: out.png out.txt

out.png: exerciseA.txt fit.txt
	pyxplot $<


out.txt fit.txt: main.exe
	mono $< > out.txt
	cat out.txt

main.exe: main.cs matrix.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<


matrix.dll: $(SRC_FILES)
	mcs -target:library -out:$@ $^


clean:
	rm -rf $(RM)