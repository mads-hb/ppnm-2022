RM = *.exe *.dll

.PHONEY: default clean
default: out.txt


out.txt: main.exe
	mono $< > $@
	cat $@


%.exe: %.cs
	mcs -out:$@ -target:exe $<


clean:
	rm -rf $(RM)