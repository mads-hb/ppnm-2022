MATLIB_SRC = ../matlib
MATLIB_FILES = $(wildcard $(MATLIB_SRC)/*.cs)
RM = *.dll *.exe

.PHONY: default clean

default: python


python: scipy_int.py out.txt
	pipenv run python $< >> out.txt
	cat out.txt

out.txt: main.exe
	mono $< > out.txt

main.exe: main.cs matlib.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<


matlib.dll: $(MATLIB_FILES)
	mcs -target:library -out:$@ $^


clean:
	rm -rf $(RM)