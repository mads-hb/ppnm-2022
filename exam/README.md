# Exam
The two last digits in my student number are $76$ which means that I have solved problem $76\%23=7$, which is called 'Symmetric rank-1 update of a size-n symmetric eigenvalue problem'. The problem is concerned with finding the eigenvalues of a matrix, $\mathbf{A}$:

$$
\mathbf{A}=\mathbf{D}+\sigma \mathbf{u}\mathbf{u}^\intercal,
$$

where $\mathbf{D}$ is a diagonal matrix, $\mathbf{u}$ is a column vector, and $\sigma$ is some real number. The problem of finding eigenvalues of $\mathbf{A}$ can be solved in $O(n^2)$ time.

## Build
All source files for the project are found in the `src` directory. The directory `src/lib` contains all the source files that are compiled to a dynamic link library (DLL) and the file `src/main.cs` is the main executable. This obviously must be linked with the dynamic library. Building the project, running it, and compiling a report can be done with the `make` command.

## Description
In this project I have implemented an algorithm for finding the eigenvalues of a matrix on the form:

$$
\mathbf{A}=\mathbf{D}+\sigma \mathbf{u}\mathbf{u}^\intercal,
$$

where $\mathbf{D}$ is a diagonal matrix, $\mathbf{u}$ is a column vector, and $\sigma$ is some real number. I have done this using the Newton-Raphson method that we implemented earlier in the course. After experiencing numerical instabilities when finding roots of a vector function describing all eigenvalues at once, I have decided to implement a method finding the eigenvalues sequentially.

The implemented algorithm is compared with the Jacobi diagonalization routine that we have also implemented earlier in the course. Comparing the two methods I have found that both methods yield identical eigenvalues as seen in figure `img/eig_spectrum.png`, but the rank-1 update method has better time complexity as seen in figure `img/timing.png`. However, I have also found that the rank-1 update method is not as robust as the Jacobi diagonalization routine as some eigenvalues take a long time to converge and some do not converge. This is also seen in figure `img/timing.png` where some of the rank-1 method data points are outliers to the polynomial fit.

## Evaluation
Please refer to `report.pdf` for at more detailed description of the project.
