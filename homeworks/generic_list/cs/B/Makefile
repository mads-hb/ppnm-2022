SRC = src
RM = *.dll *.exe
INPUT_DATA = input.txt

.PHONY: default clean

default: out.txt

out.txt: main.exe
	cat $(INPUT_DATA) | mono $< > $@
	cat $@

main.exe: main.cs genlist.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<

genlist.dll: genlist.cs
	mcs -target:library -out:$@ $^

clean:
	rm -rf $(RM)