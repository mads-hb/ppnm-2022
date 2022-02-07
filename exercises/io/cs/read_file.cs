using System;
using static System.Math;

static class ReadFile{
    static int Main(string[] args){
        if(args.Length != 2) {
            Console.Error.Write("Wrong number of arguments to read file.\n");
            return 1;
            }
        var infile = new System.IO.StreamReader(args[0]);
        var outfile = new System.IO.StreamWriter(args[1]);
        string s;
        double x;
        outfile.Write("x\tcos(x)\tsin(x)\n");
        while(true){
            s = infile.ReadLine();
            if (s == null){
                break;
            }
            x = double.Parse(s);
            outfile.Write("{0}\t{1}\t{2}\n",x, Cos(x), Sin(x));
        }
        
        return 0;
    }
}