static class MainProgram{

    static int Main(){
        Vector3d x = new Vector3d(1,0,0);
        Vector3d y = new Vector3d(0,1,0);
        Vector3d z = new Vector3d(0,0,1);


        System.Console.WriteLine("The vector x is: {0}", x);
        System.Console.WriteLine("x*y = {0}", x.dot_product(y));
        System.Console.WriteLine("x*x = {0}", x.dot_product(x));
        System.Console.WriteLine("x+y = {0}", x+y);
        System.Console.WriteLine("x-y = {0}", x-y);
        System.Console.WriteLine("x cross y = {0}", x.vector_product(y));
        return 0;
    }
}