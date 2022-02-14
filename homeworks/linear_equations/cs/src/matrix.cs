using System;
using static System.Math;

public partial class Matrix{

    public readonly int size1, size2;
    public double[][] data;

    public Matrix(int n, int m){
        size1=n; size2=m; data = new double[size2][];
        for(int j=0;j<size2;j++) data[j]=new double[size1];
        }

    public double this[int r,int c]{
        get{return data[c][r];}
        set{data[c][r]=value;}
        }

    public Vector this[int c]{
        get{return (Vector)data[c];}
        set{data[c]=(double[])value;}
        }

    public static Matrix operator+ (Matrix a, Matrix b){
        Matrix c = new Matrix(a.size1,a.size2);
        for(int j=0;j<a.size2;j++)
            for(int i=0;i<a.size1;i++)
                c[i,j]=a[i,j]+b[i,j];
        return c;
        }

    public static Matrix operator-(Matrix a){
        Matrix c = new Matrix(a.size1,a.size2);
        for(int j=0;j<a.size2;j++)
            for(int i=0;i<a.size1;i++)
                c[i,j]=-a[i,j];
        return c;
        }

    public static Matrix operator- (Matrix a, Matrix b){
        Matrix c = new Matrix(a.size1,a.size2);
        for(int j=0;j<a.size2;j++)
            for(int i=0;i<a.size1;i++)
                c[i,j]=a[i,j]-b[i,j];
        return c;
        }

    public static Matrix operator/(Matrix a, double x){
        Matrix c=new Matrix(a.size1,a.size2);
        for(int j=0;j<a.size2;j++)
            for(int i=0;i<a.size1;i++)
                c[i,j]=a[i,j]/x;
        return c;
    }

    public static Matrix operator*(double x, Matrix a){ return a*x; }
    public static Matrix operator*(Matrix a, double x){
        Matrix c=new Matrix(a.size1,a.size2);
        for(int j=0;j<a.size2;j++)
            for(int i=0;i<a.size1;i++)
                c[i,j]=a[i,j]*x;
        return c;
    }

    public static Matrix operator* (Matrix a, Matrix b){
            var c = new Matrix(a.size1,b.size2);
            for (int k=0;k<a.size2;k++)
            for (int j=0;j<b.size2;j++)
            {
                    double bkj=b[k,j];
                    var cj=c.data[j];
                    var ak=a.data[k];
            int n=a.size1;
                    for (int i=0;i<n;i++){
                            //c[i,j]+=a[i,k]*b[k,j];
                          cj[i]+=ak[i]*bkj;
                        }
                }
            return c;
            }

    public static Vector operator* (Matrix a, Vector v){
        var u = new Vector(a.size1);
        for(int k=0;k<a.size2;k++)
        for(int i=0;i<a.size1;i++)
            u[i]+=a[i,k]*v[k];
        return u;
        }

    public static Vector operator% (Matrix a, Vector v){
        var u = new Vector(a.size2);
        for(int k=0;k<a.size1;k++)
        for(int i=0;i<a.size2;i++)
            u[i]+=a[k,i]*v[k];
        return u;
        }

    public Matrix(Vector e) : this(e.size,e.size) { for(int i=0;i<e.size;i++)this[i,i]=e[i]; }

    public void set(int r, int c, double value){ this[r,c]=value; }
    public static void set(Matrix A, int i, int j, double value){ A[i,j]=value; }
    public double get(int i, int j){ return this[i,j]; }
    public static double get(Matrix A, int i, int j){ return A[i,j]; }

    public Matrix rows(int a, int b){
      Matrix m = new Matrix(b-a+1,size2);
      for(int i=0;i<m.size1;i++)
        for(int j=0;j<m.size2;j++)
                m[i,j]=this[i+a,j];
      return m;
    }

    public Matrix cols(int a, int b){
      Matrix m = new Matrix(size1,b-a+1);
      for(int i=0;i<m.size1;i++)for(int j=0;j<m.size2;j++)
        m[i,j]=this[i,j+a];
      return m;
      }

    public void set_identity(){ this.set_unity(); }
    public void set_unity(){
        for(int i=0;i<size1;i++){
            this[i,i]=1;
            for(int j=i+1;j<size2;j++){
                this[i,j]=0;this[j,i]=0;
            }
        }
    }
    public void setid(){
        for(int i=0;i<size1;i++){
            this[i,i]=1;
            for(int j=i+1;j<size2;j++){ this[i,j]=0;this[j,i]=0; }
        }
        }
    public static Matrix id(int n){
        Matrix m=new Matrix(n,n); m.setid(); return m;
        }

    public void set_zero(){
        for(int j=0;j<size2;j++)
            for(int i=0;i<size1;i++)
                this[i,j]=0;
        }

    public static Matrix outer(Vector u, Vector v){
        Matrix c = new Matrix(u.size,v.size);
        for(int i=0;i<c.size1;i++)for(int j=0;j<c.size2;j++) c[i,j]=u[i]*v[j];
        return c;
    }

    public void update(Vector u, Vector v, double s=1){
        for(int i=0;i<size1;i++)
        for(int j=0;j<size2;j++)
            this[i,j]+=u[i]*v[j]*s;
        }

    public Matrix copy(){
        Matrix c = new Matrix(size1,size2);
        for(int j=0;j<size2;j++)
            for(int i=0;i<size1;i++)
                c[i,j]=this[i,j];
        return c;
        }


    public Matrix T{
            get{return this.transpose();}
            set{}
    }


    public Matrix transpose(){
        Matrix c = new Matrix(size2,size1);
        for(int j=0;j<size2;j++)
            for(int i=0;i<size1;i++)
                c[j,i]=this[i,j];
        return c;
        }

    public void print(string s="",string format="{0,10:g3} "){
        System.Console.WriteLine(s);
        for(int ir=0;ir<this.size1;ir++){
        for(int ic=0;ic<this.size2;ic++)
            System.Console.Write(format,this[ir,ic]);
            System.Console.WriteLine();
            }
        }

    public static bool approx(double a, double b, double acc=1e-6, double eps=1e-6){
        if(Abs(a-b)<acc)return true;
        if(Abs(a-b)/Max(Abs(a),Abs(b)) < eps)return true;
        return false;
    }

    public bool approx(Matrix B,double acc=1e-6, double eps=1e-6){
        if(this.size1!=B.size1)return false;
        if(this.size2!=B.size2)return false;
        for(int i=0;i<size1;i++)
            for(int j=0;j<size2;j++)
                if(!approx(this[i,j],B[i,j],acc,eps))
                    return false;
        return true;
    }

}