SRCDIR = src
LIBDIR = $(SRCDIR)/lib
DOCDIR = doc
IMGDIR = img
RM = *.dll *.exe

.PHONY: default clean

default: out.txt $(IMGDIR)/eig_spectrum.png $(IMGDIR)/timing.png report.pdf

report.pdf: $(DOCDIR)/report.tex
	pdflatex -output-directory $(DOCDIR) $<
	pdflatex -output-directory $(DOCDIR) $<
	cp $(patsubst %.tex, %.pdf, $<) $@

$(IMGDIR)/eig_spectrum.png: eig_spectrum.gpi data/eig_spectrum.txt
	pyxplot $<

$(IMGDIR)/timing.png: timing.gpi data/timing.txt
	pyxplot $<

out.txt data/timing.txt data/fitting.txt data/eig_spectrum.txt: main.exe
	mono $< > out.txt

main.exe: $(SRCDIR)/main.cs lib.dll
	mcs -out:$@ -target:exe -reference:$(filter %dll, $^) $<

lib.dll: $(wildcard $(LIBDIR)/*.cs)
	mcs -target:library -out:$@ $^

clean:
	rm -rf $(RM)
