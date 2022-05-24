MATLIB_SRC = ../matlib
MATLIB_FILES = $(wildcard $(MATLIB_SRC)/*.cs)
RM = *.dll *.exe *.txt

.PHONY: default clean

default: out.txt

plots: hydrogen_wavefunction.png r_convergence.png tol_convergence.png


# Add compile target to check types without running
compile: main.exe

# Implicit rules to always create output figures.
%.png: %.gpi %.txt
	pyxplot $<

%.txt: out.txt

out.txt: main.exe
	mono $< > out.txt
	cat out.txt

main.exe: main.cs matlib.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<


matlib.dll: $(MATLIB_FILES)
	mcs -target:library -out:$@ $^


clean:
	rm -rf $(RM)