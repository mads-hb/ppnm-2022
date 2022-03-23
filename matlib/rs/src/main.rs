use crate::matrix::Matrix;

mod matrix;

fn main() {
    let mat = Matrix::new(2,2);
    println!("{}", mat.get(1,1).expect("Failed"));

    let mut mat2 = Matrix::new(10,5);
    mat2.set(0,1, 100.0);
    mat2.set(1,1, 200.0);
    mat2.print();
}