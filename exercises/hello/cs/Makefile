# Setup default target
.phony: default
default: out.txt

out.txt: hello.exe
	mono $^ > $@
	cat $@

hello.exe: hello.cs
	mcs $^ -out:$@

.phony: clean
clean:
	rm *.exe *.txt
