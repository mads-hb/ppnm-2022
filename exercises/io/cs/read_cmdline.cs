using System;
using static System.Math;

static class ReadCMDLine{
    static int Main(string[] args){
        if(args.Length == 0) {
            Console.Error.Write("There was no argument\n");
            return 1;
            }
        else {
            Console.Write("x\tcos(x)\tsin(x)\n");
            for(int i = 0; i < args.Length; i++){
                double x = double.Parse(args[i]);
                Console.Write("{0}\t{1}\t{2}\n",x, Cos(x), Sin(x));
                }        
            }
        return 0;
    }
}