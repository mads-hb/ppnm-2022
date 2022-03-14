SRC = src
RM = out*.txt *.exe *.dll

.PHONY: default clean


## Default target is plots for all three exercises.
default: out3.png


## Draw pyxplot images.
out3.png: exerciseC.txt out3.txt
	pyxplot $<


## Run main program and produce interps in out*.txt files.
out3.txt: main.exe
	mono $<

main.exe: main.cs spline.dll
	mcs -target:exe -out:$@ -reference:$(filter %dll, $^) $<


## Compile library containing splines
spline.dll: $(wildcard $(SRC)/*.cs)
	mcs -target:library -out:$@ $^


## Clean stuff up
clean:
	rm -rf $(RM)
