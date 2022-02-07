using System;
using static System.Math;

static class ReadStdin{
    static int Main(){
        System.IO.TextReader stdin = Console.In;
        string s;
        double x;
        Console.Write("x\tcos(x)\tsin(x)\n");
        while(true){
            s = stdin.ReadLine();
            if (s == null){
                break;
            }
            x = double.Parse(s);
            Console.Write("{0}\t{1}\t{2}\n",x, Cos(x), Sin(x));
        }
        
        return 0;
    }
}