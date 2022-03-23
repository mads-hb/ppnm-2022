extern crate core;

mod matrix;


#[cfg(test)]
mod tests {
    // use core::panicking::assert_failed;
    use crate::matrix::Matrix;

    #[test]
    fn it_works() {
        let result = 2 + 2;
        assert_eq!(result, 4);
    }

    #[test]
    fn new_matrix() {
        let mat = Matrix::new(2,2);
        assert_eq!(&0.0, mat.get(1,1).expect(""))
    }

    #[test]
    fn get_set_matrix() {
        let mut mat = Matrix::new(2,2);
        mat.set(1,1, 200.0);
        assert_eq!(&200.0, mat.get(1,1).expect(""));
        mat.set(1,1, 0.0);
        assert_eq!(&0.0, mat.get(1,1).expect(""));
    }

    #[test]
    fn eq() {
        let a = Matrix::new(2,2);
        let b = Matrix::new(2,2);
        assert_eq!(a, b);

        let mut a1 = Matrix::new(2,2);
        a1.set(1,1,1.0);
        let b1 = Matrix::new(2,2);
        assert_ne!(a1, b1);
    }

    #[test]
    fn add() {
        let mut a = Matrix::new(2,2);
        let b = Matrix::new(2,2);
        a.set(0,0, 1.0);
        let c = a + b;
        assert_eq!(&1.0, c.get(0,0).expect(""));
    }

    #[test]
    fn neg() {
        let a = Matrix::new(2, 2);
        let b = -a;
        assert_eq!(a, b);
    }
}
