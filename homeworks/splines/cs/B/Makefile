SRC = src
RM = out*.txt *.exe *.dll

.PHONY: default clean


## Default target is plots for all three exercises.
default: out2.png


## Draw pyxplot images.
out2.png: exerciseB.txt out2.txt
	pyxplot $<


## Run main program and produce interps in out*.txt files.
out2.txt: main.exe
	mono $<

main.exe: main.cs spline.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll, $^) $<


## Compile library containing splines
spline.dll: $(wildcard $(SRC)/*.cs)
	mcs -target:library -out:$@ $^


## Clean stuff up
clean:
	rm -rf $(RM)
