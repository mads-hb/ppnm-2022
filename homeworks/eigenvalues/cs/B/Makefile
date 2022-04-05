MATLIB_SRC = ../matlib
MATLIB_FILES = $(wildcard $(MATLIB_SRC)/*.cs)
RM = *.dll *.exe rmax.txt eigenfunc.txt analytical.txt npoints.txt

.PHONY: default clean

default: out.png rmax.png npoints.png

out.png: plot.txt eigenfunc.txt
	pyxplot $<


rmax.png: plot_r.txt rmax.txt
	pyxplot $<

npoints.png: plot_n.txt npoints.txt
	pyxplot $<


out.txt eigenfunc.txt rmax.txt npoints.txt: main.exe
	mono $< > out.txt
	cat out.txt

main.exe: main.cs matlib.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll,$^) $<


matlib.dll: $(MATLIB_FILES)
	mcs -target:library -out:$@ $^


clean:
	rm -rf $(RM)