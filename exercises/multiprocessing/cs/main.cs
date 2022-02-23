using System.Threading;
using System.Diagnostics;


/** Class holding all data needed for computing a harmonic sum
 */
public class Data {
    // a is start point and b is endpoint not inclusive
    public int a, b;
    // running sum
    public double sum;

    // constructor
    public Data(int a, int b) {
        this.a = a;
        this.b = b;
    }
    
    public Data(double a, double b) {
        this.a = (int)a;
        this.b = (int)b;
    }

    public void print(){
        System.Console.WriteLine($"Data(a={a}, b={b})");
    }
}


public static class MainProgram{
    
    /** The objective function we want to put into threads.
     */
    private static void HarmonicSum(object obj){
        var x = (Data) obj;
        x.sum = 0;
        for(int i = x.a; i < x.b; i++){
            x.sum += 1.0/i;
        }
    }
    
    private static double ComputeHarmonicSum(int a, int b, int n_threads){
        // Find delta to split interval into equal chunks.
        double delta = (b-a)*1.0/n_threads;
        Thread[] threads = new Thread[n_threads];
        Data[] data = new Data[n_threads];
        
        // Divide interval [a,b[ into n_threads chunks
        // and spin up all threads. 
        for (int i=0; i< n_threads; i++){
            threads[i] = new Thread(HarmonicSum);
            data[i] = new Data(a + delta*i, a + delta*(i+1));

        }
        
        // Start all threads.
        for (int i=0; i < n_threads; i++){
            threads[i].Start(data[i]);
        }
        
        // Join all threads and compute the sum.
        double sum = 0;
        for (int i=0; i < n_threads; i++){
            threads[i].Join();
            sum += data[i].sum;
        }
        return sum;
    }

    public static int Main(){
        int a = 1;
        int b = (int)1e9;
        System.Console.WriteLine($"Compute the harmonic sum on the interval [{a}, {b}[:");
        for (int i = 1; i < 21; i++){
            var sw = new Stopwatch();
            sw.Start();
            double sum = ComputeHarmonicSum(a, b, i);
            sw.Stop();
            System.Console.WriteLine($"The sum using {i} threads is {sum:F3} and it took {sw.ElapsedMilliseconds}ms.");
        }
        System.Console.WriteLine("\nClearly, the time it takes to compute the super is shorter when we use more than 1 thread. ");
        System.Console.WriteLine("However, we cannot use infinitely many threads to go faster. "); 
        System.Console.WriteLine("The results show that after 11 threads we do not gain any performance improvement. ");
        System.Console.WriteLine("At this point the time it takes to spin up a thread is longer than the time "); 
        System.Console.WriteLine("it takes to just compute the harmonic sum on the interval");
        return 0;
    }
}
