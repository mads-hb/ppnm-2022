using System;
using System.IO;
using static System.Math;
using static System.Console;

public partial class Vector{

    private double[] data;
    public int size{ get{return data.Length;} }

    public double this[int i]{
        get{return data[i];}
        set{data[i]=value;}
    }

    public Vector(int n){data=new double[n];}
    public Vector(double[] a){data=a;}


    public static implicit operator Vector (double[] a){ return new Vector(a); }
    public static implicit operator double[] (Vector v){ return v.data; }

    public void print(string s="",string format="{0,10:g3} "){
        this.fprint(Console.Out,s,format);
        }

    public void fprint(TextWriter file,string s="",string format="{0,10:g3} "){
        file.Write(s);
        for(int i=0;i<size;i++) file.Write(format,this[i]);
        file.Write("\n");
    }

    public static Vector operator+(Vector v, Vector u){
        Vector r=new Vector(v.size);
        for(int i=0;i<r.size;i++)r[i]=v[i]+u[i];
        return r; }

    public static Vector operator-(Vector v){
        Vector r=new Vector(v.size);
        for(int i=0;i<r.size;i++)r[i]=-v[i];
        return r; }

    public static Vector operator-(Vector v, Vector u){
        Vector r=new Vector(v.size);
        for(int i=0;i<r.size;i++)r[i]=v[i]-u[i];
        return r; }

    public static Vector operator*(Vector v, double a){
        Vector r=new Vector(v.size);
        for(int i=0;i<v.size;i++)r[i]=a*v[i];
        return r; }

    public static Vector operator*(double a, Vector v){
        return v*a; }

    public static Vector operator/(Vector v, double a){
        Vector r=new Vector(v.size);
        for(int i=0;i<v.size;i++)r[i]=v[i]/a;
        return r; }

    public double dot(Vector o){
        double sum=0;
        for(int i=0;i<size;i++)sum+=this[i]*o[i];
        return sum;
        }
    public static double operator%(Vector a,Vector b){
        return a.dot(b);
        }

    public double norm(){
        double meanabs=0;
        for(int i=0;i<size;i++)meanabs+=Abs(this[i]);
        if(meanabs==0)meanabs=1;
        meanabs/=size;
        double sum=0;
        for(int i=0;i<size;i++)sum+=(this[i]/meanabs)*(this[i]/meanabs);
        return meanabs*Sqrt(sum);
        }

    public Vector copy(){
        Vector b=new Vector(this.size);
        for(int i=0;i<this.size;i++)b[i]=this[i];
        return b;
    }

    public static bool approx(double x, double y, double acc=1e-9, double eps=1e-9){
        if(Abs(x-y)<acc)return true;
        if(Abs(x-y)/Max(Abs(x),Abs(y))<eps)return true;
        return false;
        }

    public static bool approx(Vector a,Vector b,double acc=1e-9,double eps=1e-9){
        if(a.size!=b.size)return false;
        for(int i=0;i<a.size;i++)
            if(!approx(a[i],b[i],acc,eps))return false;
        return true;
    }
    public bool approx(Vector o){
        for(int i=0;i<size;i++)
            if(!approx(this[i],o[i]))return false;
        return true;
    }

}