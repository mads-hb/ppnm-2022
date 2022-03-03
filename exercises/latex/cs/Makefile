RM = *.dll *.exe *.aux *.log


.PHONY: default clean

default: report.pdf 


report.pdf: report.tex out.png
	pdflatex $<
	pdflatex $<


out.png: plot.gpi out.txt
	pyxplot $<


out.txt: main.exe
	mono $< > $@


main.exe: main.cs
	mcs -out:$@ -target:exe $<


clean:
	rm -rf $(RM)

