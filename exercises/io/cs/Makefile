RM = out*.txt *.exe

.PHONY: default, clean

default: read_stdin read_file read_cmdline

## read_cmdline
read_cmdline: read_cmdline.exe
	mono $^ $(cat input.txt) > out1.txt


read_cmdline.exe: read_cmdline.cs
	mcs -out:$@ -target:exe $<

## read_stdin
read_stdin: read_stdin.exe
	mono $^ < input.txt > out2.txt


read_stdin.exe: read_stdin.cs
	mcs -out:$@ -target:exe $<


## read_file
read_file: read_file.exe
	mono $^ input.txt out3.txt


read_file.exe: read_file.cs
	mcs -out:$@ -target:exe $<


clean:
	rm $(RM)