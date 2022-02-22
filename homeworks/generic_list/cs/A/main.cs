using static System.Console;
using static System.Math;

public static class MainProgram{

    public static int Main(){
        // Define delimiting characters
        var list = new GenericList<double>();
        char[] delimiters = {' ','\t','\n'};
        var options = System.StringSplitOptions.RemoveEmptyEntries;
        // Read all lines until null (EOF)
        for(string line = ReadLine(); line != null; line = ReadLine()){
            // Split based on delimiters into  array
            var words = line.Split(delimiters,options);
            // WriteLine("{}", words[0]);
            foreach(var word in words){
                double x = double.Parse(word);
                list.push(x);
            }
        }
        list.print();
        return 0;
    }
}