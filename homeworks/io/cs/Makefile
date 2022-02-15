RM = *.exe *.dll
INPUT_DATA = input.txt


.PHONY: default, clean

default: outA.txt outB.txt outC.txt

## read_stdin
outA.txt: mainA.exe
	cat $(INPUT_DATA) | mono $< > $@


## read_cmdline 
# This method doesn't seem to work.
# outB.txt: mainB.exe
# 	mono $< $(cat $(INPUT_DATA)) > $@
outB.txt: mainB.exe
	mono $< 1 2 3 4 5 > $@


## read_args
outC.txt: mainC.exe
	mono $< -input:$(INPUT_DATA) -output:$@


# Compile main. Set entrypoint according to exe filename.
main%.exe: main.cs
	mcs -out:$@ -target:exe $< -main:Exercise$(subst main,,$(basename $@))


clean:
	rm -rf $(RM)