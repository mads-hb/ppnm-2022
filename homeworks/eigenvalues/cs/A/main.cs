using static System.Console;

static class MainProgram{

    public static int Main(){
        int n = 7;
        matrix V = new matrix(n, n);
        matrix A = matrix.random_symmetric(n);
        matrix A2 = A.copy();
        WriteLine("The matrix A is:");
        A.print();
        WriteLine();
        int sweeps = Jacobi.diag(A, V);
        A.print("After using eigenvalue decomposition the eigenvalues are:");
        WriteLine();
        V.print("and the eigenvectors are the columns of:");
        WriteLine($"The total number of sweeps before convergence: {sweeps}");
        WriteLine("\n\n\n");
        WriteLine("Now we check that VT*A*V = D");
        matrix D = ((V.T * A2) * V);
        D.print("VT*A*V is:");
        A.print("and D is:");
        WriteLine($"Comparing these gives (V.T * A * V).approx(D) => {A.approx(D)}");

        WriteLine("\n\n\n");
        WriteLine("Now we check that VT*A*V = D");
        matrix D2 = ((V * A) * V.T);
        D2.print("V*D*VT is:");
        A2.print("and A is:");
        WriteLine($"Comparing these gives (V * D * V.T).approx(A) => {D2.approx(A2)}");

        WriteLine("\n\n\n");
        WriteLine("Finally we check that V is orthogonal");
        matrix V2 = (V.T * V);
        matrix V3 = (V * V.T);
        V2.print("VT*V is:");
        V3.print("and V*VT is:");
        WriteLine($"Comparing these gives (V.T* V).approx(V*V.T) => {V2.approx(V3)}");
        WriteLine($"Comparing to identity gives (V.T* V).approx(matrix.id({n})) => {V2.approx(matrix.id(n))}");
        return 0;
    }
}