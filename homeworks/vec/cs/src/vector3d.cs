using System;


interface IVector<T>{
    double dot_product(T other);
    T vector_product(T other);
}


public class Vector3d: IVector<Vector3d>{
    public Vector3d(double x, double y, double z){
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3d(){ x = y = z = 0; }


    public double x { get; }
    public double y { get; }
    public double z { get; }

    public override string ToString() => $"({x}, {y}, {z})";

    public override bool Equals(Object obj){
        if ((obj == null) || ! this.GetType().Equals(obj.GetType())){
            return false;
        } else {
            Vector3d other = (Vector3d) obj;
            return this.x == other.x && this.y == other.y && this.z == other.z;
        }
        
    }

    // Operators
    public static Vector3d operator*(Vector3d v, double c){
        return new Vector3d(c*v.x,c*v.y,c*v.z);
    }

    public static Vector3d operator*(double c, Vector3d v){
        return new Vector3d(c*v.x,c*v.y,c*v.z);
    }

    public static Vector3d operator+(Vector3d u, Vector3d v){
        return new Vector3d(u.x+v.x, u.y+v.y, u.z+v.z);
    }

    public static Vector3d operator-(Vector3d u, Vector3d v){
        return new Vector3d(u.x-v.x, u.y-v.y, u.z-v.z);
    }

    // Methods
    public double dot_product(Vector3d other){
        return this.x * other.x + this.y * other.y + this.z * other.z;
    }
    public Vector3d vector_product(Vector3d other){
        double i, j, k;
        i = this.y*other.z-this.z*other.y;
        j = -this.x*other.z+this.z*other.x;
        k = this.x*other.y-this.y*other.x;
        return new Vector3d(i, j, k);

    }

    public double magnitude(){
        return Math.Sqrt(Math.Pow(this.x ,2) 
            + Math.Pow(this.y ,2) 
            + Math.Pow(this.z ,2));
    }


    private static bool is_close(double a, double b, double rtol, double atol){
        return Math.Abs(a - b) <= (atol + rtol * Math.Abs(b));
    }

    /**
     * Use the numpy approach for testing numerically similarity
     **/
    public bool approx(Vector3d other, double rtol=1e-5, double atol=1e-8){
        Func<double, double, bool> comp = (a, b) => is_close(a, b, rtol, atol);
        return comp(this.x, other.x) && comp(this.y, other.y) && comp(this.z, other.z);
    }


    public void print(string s){
        Console.Write(s);
        Console.WriteLine($"Vector3d {this}");
    }
    
    public void print(){
        this.print("");
    }

}