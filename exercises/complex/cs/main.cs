using System;
using static cmath;
using static complex;


class TestMaths{
    static int Main(){

        double x = cmath.sqrt(-1);
        System.Console.WriteLine("(double) sqrt(-1) = {0}", x);

        complex z = cmath.sqrt(-1);
        System.Console.WriteLine("sqrt(-1)={0}", z);
        System.Console.WriteLine("System.Math.Sqrt(-1)={0}", System.Math.Sqrt(-1));

        complex z1 = cmath.sqrt(cmath.I);
        System.Console.WriteLine("sqrt(i)={0}", z1);

        complex z2 = cmath.exp(cmath.I);
        System.Console.WriteLine("exp(i)={0}", z2);

        complex z3 = cmath.exp(cmath.I*System.Math.PI);
        System.Console.WriteLine("exp(i*pi)={0}", z3);

        complex z4 = cmath.pow(cmath.I, cmath.I);
        System.Console.WriteLine("i^i={0}", z4);

        complex z5= cmath.log(cmath.I);
        System.Console.WriteLine("i^i={0}", z5);

        complex z6 = cmath.sin(cmath.I*System.Math.PI);
        System.Console.WriteLine("sin(i*pi)={0}",z6);

        complex z7 = cmath.sinh(cmath.I);
        System.Console.WriteLine("sinh(i)={0}",z7);

        complex z8 = cmath.cosh(cmath.I);
        System.Console.WriteLine("cosh(i)={0}",z8);

        return 0;
    }
}
