using System;
using static cmath;
using static complex;


class TestMaths{
    static int Main(){
        double x = cmath.sqrt(2);
        System.Console.WriteLine("sqrt(2)={0}", x);

        complex z1 = cmath.exp(cmath.I);
        System.Console.WriteLine("exp(i)={0}", z1);

        complex z2 = cmath.exp(cmath.I*System.Math.PI);
        System.Console.WriteLine("exp(i*pi)={0}", z2);

        complex z3 = cmath.pow(cmath.I, cmath.I);
        System.Console.WriteLine("i^i={0}", z3);

        complex z4 = cmath.sin(cmath.I*System.Math.PI);
        System.Console.WriteLine("sin(i*pi)={0}",z4);

        System.Console.WriteLine("sinh(2.0)={0}",cmath.sinh(2.0));
        return 0;
    }
}
