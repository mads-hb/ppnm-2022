using static System.Console;
using static System.Math;

public static class MainProgram{
    public static void Main(){
        var list = new LinkedList<double>();
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

        list.start();
        while (list.current != null){
            for (int i=0; i < 3; i++){
                Write($"{list.current.item}\t");
                list.next();
                if (list.current == null) break;
            }
            Write("\n");
        
        }
    }
}