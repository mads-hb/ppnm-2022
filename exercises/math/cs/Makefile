RM = *.exe

.PHONY: default clean

default: out.txt

out.txt: main.exe
	mono $< > $@
	cat $@

main.exe: main.cs
	mcs -target:exe -out:$@ $<

clean:
	rm -rf $(RM)