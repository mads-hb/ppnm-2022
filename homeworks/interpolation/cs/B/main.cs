static class MainProgram{

    static (double[], double[]) read_numbers(int length){
        double[] x = new double[length], y = new double[length];
        using (var infile = new System.IO.StreamReader("xy_data.txt")){
            string s;
            double xi, yi;
            for(int i=0; i < length; i++){
                s = infile.ReadLine();
                string[] subs = s.Split(' ');
                xi = double.Parse(subs[0]);
                yi = double.Parse(subs[1]);
                x[i] = xi; 
                y[i] = yi;
            }
        }
        return (x, y);
    }

    public static void ExerciseB(int length, double[] x, double[] y){
        using (var outfile = new System.IO.StreamWriter("out2.txt")){
            Spline interp = new QuadraticInterpolation(length, x, y);
            double val = -4.9;
            do {
                double interp_val = interp.eval(val);
                double interp_integ = interp.integ(val);
                double interp_deriv = interp.deriv(val);
                outfile.WriteLine("{0}\t{1}\t{2}\t{3}", val, interp_val, interp_integ, interp_deriv);
                val += 0.1;
            } while (val < 5);
        }
    }

    public static int Main(){
        double[] x, y;
        int length = 29;
        (x, y) = read_numbers(length);
        ExerciseB(length, x, y);
        return 0;
    }
}