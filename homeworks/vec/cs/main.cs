using System;
using static System.Math;

static class MainProgram{

    static int Main(){
        Vector3d x = new Vector3d(1,0,0);
        Vector3d y = new Vector3d(0,1,0);
        Vector3d z = new Vector3d(0,0,1);


        int return_code=0;
        bool test;
        var rnd=new Random();
        int n=9;
        Vector3d[] zs = new Vector3d[n];
        for(int i = 0; i < n; i++)
            zs[i] = new Vector3d(2*rnd.NextDouble()-1,2*rnd.NextDouble()-1, 2*rnd.NextDouble()-1);

        Console.Write("Testing x == x ... ");
        test = x == x;
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        Console.Write("Testing x == y ... ");
        test = x == y;
        if(!test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        Console.Write("Testing x != z ... ");
        test = x != z;
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        Console.Write("Testing x.Equals(new Vector3d(1,0,0)) ... ");
        test = x.Equals(new Vector3d(1,0,0));
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        Console.Write("Testing x != new Vector3d(1,0,0) ... ");
        test = x != new Vector3d(1,0,0);
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        Console.Write("Testing x.magnitude() == 1 ... ");
        test = x.magnitude() == 1;
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        Console.Write("Testing y.magnitude() == 1 ... ");
        test = y.magnitude() == 1;
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        Console.Write("Testing x.dot_product(y) == 0 ... ");
        test = x.dot_product(y) == 0;
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}

        Console.Write("Testing x.dot_product(x) == 1 ... ");
        test = x.dot_product(y) == 1;
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}

        Console.WriteLine("{0}", x.dot_product(x));




        // Finish tests
        if(return_code==0)
            Console.Write("all tests passed :)\n");
        else 
            Console.Write("{0} tests FAILED :(\n",return_code);
        return_code = 0;
        return return_code;
    }
}