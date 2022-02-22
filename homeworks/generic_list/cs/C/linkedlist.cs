public class Node<T>{
    
    // Self
    public T item;

    // Next item
    public Node<T> next;

    // Constructor
    public Node(T item){
        this.item=item;
    }
}

public class LinkedList<T>{
    // Initialize first element and current element to null
    public Node<T> first=null,current=null;

    // Push to list
    public void push(T item){
            if(first == null){
                    first = new Node<T>(item);
                    current=first;
            }
            else{
                    current.next = new Node<T>(item);
                    current=current.next;
            }
    }
    public void start(){
        current = first;
    }
    public void next(){current = current.next; }
}