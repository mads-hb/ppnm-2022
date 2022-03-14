SRC = src
SRC_FILES = $(wildcard $(SRC)/*.cs)
RM = *.dll *.exe timing.txt

.PHONY: default clean

default: out.png

out.png: exerciseC.txt timing.txt
	pyxplot $<


timing.txt out.txt: main.exe
	mono $< > $@
	cat $@

main.exe: main.cs matrix.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<


matrix.dll: $(SRC_FILES)
	mcs -target:library -out:$@ $^


clean:
	rm -rf $(RM)