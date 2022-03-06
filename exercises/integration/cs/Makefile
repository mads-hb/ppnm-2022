LIBDIR = lib
RM = *.dll *.exe err.txt

.PHONY: default clean

default: out.png

out.png: plot.gpi err.txt
	pyxplot $ $<

out.txt err.txt: main.exe
	mono $< > $@

main.exe: main.cs math.dll
	mcs -out:$@ -target:exe -reference:$(filter %dll, $^) $<

math.dll: $(wildcard $(LIBDIR)/*.cs)
	mcs -target:library -out:$@ $^

clean:
	rm -rf $(RM)
