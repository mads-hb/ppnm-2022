RM = *.exe *.dll

.PHONY: default clean

default: out.txt

out.txt: main.exe
	mono $< > $@

main.exe: main.cs
	mcs -out:$@ -target:exe $<

clean:
	rm -rf $(RM)
