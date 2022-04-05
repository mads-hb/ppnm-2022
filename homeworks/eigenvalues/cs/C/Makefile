MATLIB_SRC = ../matlib
MATLIB_FILES = $(wildcard $(MATLIB_SRC)/*.cs)
RM = *.dll *.exe *_opt_*.txt

.PHONY: default clean

default: out.png

out.png: exerciseC.txt timing_opt_True.txt timing_opt_False.txt
	pyxplot $<


timing_opt_True.txt timing_opt_False.txt fit_opt_True.txt fit_opt_False.txt out.txt: main.exe
	mono $< > out.txt
	cat out.txt

main.exe: main.cs matlib.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<


matlib.dll: $(MATLIB_FILES)
	mcs -target:library -out:$@ $^


clean:
	rm -rf $(RM)