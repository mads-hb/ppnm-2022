RM = *.exe *.dll

.PHONY: clean

out.png: plot.gpi out.txt
	pyxplot $<

out.txt: main.exe
	mono $^ > $@

main.exe: main.cs
	mcs -out:$@ -target:exe $^

clean:
	rm -rf $(RM)
