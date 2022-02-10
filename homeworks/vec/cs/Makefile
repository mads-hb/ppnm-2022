SRC = src
RM = *.txt *.dll *.exe

.PHONY: default clean

default: out.txt

out.txt: main.exe
	mono $< > $@
	cat $@

main.exe: main.cs vector3d.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<

%.dll: $(SRC)/%.cs
	mcs -target:library -out:$@ $<

clean:
	rm $(RM)