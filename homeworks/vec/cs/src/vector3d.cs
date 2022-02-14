using System;


interface IVector<T>{
    double dot(T other);
    T cross(T other);
    double norm();
}


public class Vector3d: IVector<Vector3d>{
    public Vector3d(double x, double y, double z){
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3d(){ x = y = z = 0; }

    // Define handy constructors.
    public static Vector3d unit_x {get => new Vector3d(1, 0, 0);}
    public static Vector3d unit_y {get => new Vector3d(0, 1, 0);}
    public static Vector3d unit_z {get => new Vector3d(0, 0, 1);}
    public static Vector3d null_vector {get => new Vector3d(0,0,0);}

    // Define getters
    public double x { get; }
    public double y { get; }
    public double z { get; }

    // Override ToString, Equals and GetHasCode methods.
    public override string ToString() => $"({x}, {y}, {z})";

    public override bool Equals(Object obj){
        if ((obj == null) || ! this.GetType().Equals(obj.GetType())){
            return false;
        } else {
            Vector3d other = (Vector3d) obj;
            return this.x == other.x && this.y == other.y && this.z == other.z;
        }
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y, z);
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

    public static Vector3d operator/(Vector3d v, double c){
        return new Vector3d(v.x/c,v.y/c,v.z/c);
    }

    // Methods
    public double dot(Vector3d other){
        return this.x * other.x + this.y * other.y + this.z * other.z;
    }
    public Vector3d cross(Vector3d other){
        double i, j, k;
        i = this.y*other.z-this.z*other.y;
        j = -this.x*other.z+this.z*other.x;
        k = this.x*other.y-this.y*other.x;
        return new Vector3d(i, j, k);

    }

    public double norm(){
        return Math.Sqrt(Math.Pow(this.x ,2) 
            + Math.Pow(this.y ,2) 
            + Math.Pow(this.z ,2));
    }


    private static bool is_close(double a, double b, double rtol, double atol){
        return (Math.Abs(a - b) < atol) || (Math.Abs(a-b)/(Math.Abs(a) + Math.Abs(b)) < rtol);
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