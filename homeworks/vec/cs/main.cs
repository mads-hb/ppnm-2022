using System;
using static System.Math;

static class MainProgram{

    private static bool is_close(double a, double b){
        double rtol=1e-5;
        double atol=1e-8;
        return (Math.Abs(a - b) < atol) || (Math.Abs(a-b)/(Math.Abs(a) + Math.Abs(b)) < rtol);
    }

    static int Main(){
        int return_code=0;
        bool test;

        Vector3d x = Vector3d.unit_x;
        Vector3d y = Vector3d.unit_y;
        Vector3d z = Vector3d.unit_z;

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


        Console.Write("Testing x.norm() == 1 ... ");
        test = x.norm() == 1;
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        Console.Write("Testing y.norm() == 1 ... ");
        test = y.norm() == 1;
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        Console.Write("Testing x.dot(y) == 0 ... ");
        test = x.dot(y) == 0;
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}

        Console.Write("Testing x.dot(x) == 1 ... ");
        test = x.dot(x) == 1;
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        Vector3d unit_xy = (x + y) / Sqrt(2);
        Console.Write("Testing unit_xy.dot(unit_xy) approx 1 ... ");
        test = is_close(unit_xy.dot(unit_xy), 1);
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}

        Console.Write("Testing x.cross(y).approx(z) ... ");
        test = x.cross(y).approx(z);
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}

        Console.Write("Testing x.cross(x).approx(null_vector) ... ");
        test = x.cross(x).approx(Vector3d.null_vector);
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}

        Console.Write("Testing x.approx(a) ... ");
        test = x.approx(x);
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}

        Console.Write("Testing x.approx(y) ... ");
        test = x.approx(y);
        if(!test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}

        Console.Write("Testing (x*2).approx(x+x) ... ");
        test = (2*x).approx(x+x);
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        Console.Write("Testing (x-x).approx(null_vector) ... ");
        test = (x-x).approx(Vector3d.null_vector);
        if(test) Console.Write(" ...passed\n");
        else {Console.Write("...FAILED\n"); return_code += 1;}


        // Finish tests
        if(return_code==0)
            Console.Write("all tests passed :)\n");
        else 
            Console.Write("{0} tests FAILED :(\n",return_code);
        return_code = 0;
        return return_code;
    }
}