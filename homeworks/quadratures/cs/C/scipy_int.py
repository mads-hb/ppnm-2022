from scipy.integrate import quad
import numpy as np

def f1(x: float) -> float:
    return 1/x**2

def f2(x:float) -> float:
    return x*np.exp(-x*x)


kw = dict(epsabs=0.001, epsrel=0.001, full_output=True)
res1, err1, inf1 = quad(f1, 1, np.inf, **kw)
res2, err2, inf2 = quad(f2, -np.inf, np.inf, **kw)

print("Now using scipy to integrate the same functions.")
print(f"The integral of 1/sqrt(x) is {res1} and took {inf1['neval']} evaluations.")
print(f"The integral of log(x)/sqrt(x) is {res2} and took {inf2['neval']} evaluations.")

