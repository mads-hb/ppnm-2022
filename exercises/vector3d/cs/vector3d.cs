using System;


interface IVector<T>{
    double dot_product(T other);
    T vector_product(T other);
}


public struct Vector3d: IVector<Vector3d>{
    public Vector3d(double x, double y, double z){
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public double x { get; }
    public double y { get; }
    public double z { get; }

    public override string ToString() => $"({x}, {y}, {z})";

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

}