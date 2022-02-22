public class GenericList<T>{
    public T[] data;
    public int size=0, capacity=8;
    public GenericList(){ data = new T[capacity]; }
    public void push(T item){ /* add item to list */
        if(size==capacity){
            T[] newdata = new T[capacity*=2];
            for(int i=0;i<size;i++)newdata[i]=data[i];
            data=newdata;
            }
        data[size]=item;
        size++;
    }
    public void print(){
        System.Console.WriteLine("Length of list is {0}. Capacity of list is {1}.", size, capacity);
        System.Console.Write("[");
        for(int i=0; i<size; i++){
            System.Console.Write($"{data[i]} ");
        }
        System.Console.Write("]\n");
    }
    public void remove(int ix){
        // Move all items larger than ix one index down. Only run loop until 
        // size - 1 since the new vector length will now be one less.        
        if (ix < 0 || size <= ix) {
            throw new System.IndexOutOfRangeException($"The index {ix} is not in list of size {size}.");
        }
        for (int i = ix; i < size - 1; ++i){
            data[i] = data[i+1];
            data[i+1] = default (T); 
        }
        size -= 1;

        // Resize vector if short enough.
        if (size >= 0 && size == capacity / 2 && capacity / 2 >= 8){

            int new_capacity = capacity/2;
            T[] newdata = new T[new_capacity];
            for(int i=0;i<size;i++){
                newdata[i]=data[i];
            }
            data = newdata;
            capacity = new_capacity;
        }
    }

}