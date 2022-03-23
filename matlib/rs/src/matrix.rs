use std::fmt::{Debug, Formatter};
use std::ops::{Add, Sub, Neg};


pub struct Matrix<T> {
    size1: usize,
    size2: usize,
    data: Vec<T>
}

impl Matrix<f32> {

    pub fn new (size1: usize, size2: usize) -> Matrix<f32> {
        Matrix{
            size1,
            size2,
            data: vec![0.0; size1*size2]
        }
    }

    pub fn print(&self) -> () {
        for i in 0..self.size1 {
            for j in 0..self.size2 {
                print!("{}\t", self.get(i, j).expect("Failed reading"));
            }
            println!();
        }
    }

    pub fn set(&mut self, i: usize, j: usize, value: f32) -> () {
        self.data[i + self.size1*j] = value;
    }

    pub fn get(&self, i: usize, j: usize) -> Option<&f32> {
        self.data.get(i + self.size1*j)
    }
}


impl PartialEq for Matrix<f32> {

    fn eq(&self, other: &Self) -> bool {
        if self.size1 != other.size1 { false }
        else if self.size2 != other.size2 { false }
        else {
            for i in 0..self.size1 {
                for j in 0..self.size2 {
                    if self.get(i,j ) != other.get(i,j) { return false; }
                }
            }
            true
        }
    }
}
impl Add for Matrix<f32> {
    type Output = Self;

    fn add(self, rhs: Self) -> Self {
        assert_eq!(self.size1, rhs.size1);
        assert_eq!(self.size2, rhs.size2);
        let mut sum = Matrix::new(self.size1, self.size2);
        for i in 0..self.size1 {
            for j in 0..self.size2 {
                sum.set(i, j, self.get(i,j).expect("Failed getting from matrix") + rhs.get(i,j).expect("Failed getting from matrix"));
            }
        }
        sum
    }
}


impl Neg for Matrix<f32> {
    type Output = Self;

    fn neg(self) -> Self {
        let mut neg = Matrix::new(self.size1, self.size2);
        for i in 0..self.size1 {
            for j in 0..self.size2 {
                neg.set(i, j, -self.get(i,j).expect("Failed getting from matrix").clone());
            }
        }
        neg
    }
}

impl Debug for Matrix<f32> {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        Result::Ok(self.print())
    }
}