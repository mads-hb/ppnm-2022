using static System.Console;
using static System.Math;

public static class ExerciseA{

    private static int ReadStdIn(){
        // Define delimiting characters
        char[] delimiters = {' ','\t','\n'};
        var options = System.StringSplitOptions.RemoveEmptyEntries;
        // Read all lines until null (EOF)
        for(string line = ReadLine(); line != null; line = ReadLine()){
            // Split based on delimiters into  array
            var words = line.Split(delimiters,options);
            foreach(var word in words){
                double x = double.Parse(word);
                WriteLine($"{x}\t{Sin(x)}\t{Cos(x)}");
            }
        }
        return 0;
    }

    public static int Main(){
        WriteLine("x\tSin(x)\tCos(x)");
        return ReadStdIn();
    }

}


public static class ExerciseB{

    public static int Main(string[] args){
        WriteLine("x\tSin(x)\tCos(x)");
        foreach(var arg in args){
            double x = double.Parse(arg);
            WriteLine($"{x}\t{Sin(x)}\t{Cos(x)}");
            }
        return 0;
    }

}


public static class ExerciseC{

    /** Parse arguments and return infile, outfile. Argumentlist can only
     * contain -input:* and -output:*.
     * @param A string of commandlinearguments
     * @throws Throw an ArgumentException if argumentlist is incomplete.
     **/
    private static (string, string) ParseArgs(string[] args){
        string infile=null,outfile=null;
        foreach(var arg in args){
            var words=arg.Split(':');
            if(words[0]=="-input"){
                infile=words[1];
            } else if(words[0]=="-output"){
                outfile=words[1];
            } else {
                throw new System.ArgumentException($"Illegal argument {words}."); 
            }
        }
        if (infile == null || outfile == null){
            throw new System.ArgumentException($"Missing either input or output argument.");
        }
        return (infile, outfile);
    }

    public static int Main(string[] args){
        (var infile, var outfile) = ParseArgs(args);
        // Open disposables through using directive
        using (var instream = new System.IO.StreamReader(infile))
        using (var outstream = new System.IO.StreamWriter(outfile)){
            outstream.WriteLine("x\tSin(x)\tCos(x)");
            // Parse all items in input and write them to output.
            for(string line=instream.ReadLine();line!=null;line=instream.ReadLine()){
                double x=double.Parse(line);
                outstream.WriteLine($"{x}\t{Sin(x)}\t{Cos(x)}");
            }
        }
        return 0;
    }

}