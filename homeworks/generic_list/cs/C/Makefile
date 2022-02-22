SRC = src
RM = *.dll *.exe
INPUT_DATA = input.txt

.PHONY: default clean

default: out.txt

out.txt: main.exe
	cat $(INPUT_DATA) | mono $< > $@
	cat $@

main.exe: main.cs linkedlist.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<

linkedlist.dll: linkedlist.cs
	mcs -target:library -out:$@ $^

clean:
	rm -rf $(RM)