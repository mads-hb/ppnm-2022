RM = *.exe *.dll

.PHONY: clean default

default: out.png out2.png

out2.png: plot_log.gpi out2.txt
	pyxplot $<

out.png: plot.gpi out.txt
	pyxplot $<

out.txt out2.txt: main.exe
	mono $^ > $@

main.exe: main.cs
	mcs -out:$@ -target:exe $^

clean:
	rm -rf $(RM)
