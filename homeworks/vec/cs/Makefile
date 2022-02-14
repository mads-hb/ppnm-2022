SRC = src
RM = *.dll *.exe

.PHONY: default clean

default: out.txt

out.txt: main.exe
	mono $< > $@
	cat $@

main.exe: main.cs vec.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<

vec.dll: $(wildcard $(SRC)/*.cs)
	mcs -target:library -out:$@ $^

clean:
	rm -rf $(RM)