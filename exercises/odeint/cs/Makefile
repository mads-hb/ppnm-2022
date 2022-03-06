LIBDIR = lib
RM = *.dll *.exe diff.txt

.PHONY: default clean

default: out.png

out.png: plot.gpi diff.txt
	pyxplot $ $<

out.txt diff.txt: main.exe
	mono $< > out.txt

main.exe: main.cs math.dll
	mcs -out:$@ -target:exe -reference:$(filter %dll, $^) $<

math.dll: $(wildcard $(LIBDIR)/*.cs)
	mcs -target:library -out:$@ $^

clean:
	rm -rf $(RM)
