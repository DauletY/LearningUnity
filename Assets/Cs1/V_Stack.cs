
using UnityEngine;
using UnityEngine.UI;
public class V_Stack : MonoBehaviour {
    private static int SIZE = 10;
    public Button[] stack = new Button[5];
    public int top= -1;
    private void Start() {
        Display();
    }
    public void Display() {
       switch (top) {
             case 0:
                int value = int.Parse(Input.inputString);
                Push(GetComponent<Button>());
                print("push" + value);
              break;
            case 1:
                Pop();
                break;
            case 2:
                Peek();
                break;
            case 3:
                return;
            default:
                print(" Wrong choice , please try again! ");
                break;
       }

    }
    public void Push(Button value) {
        if (top == SIZE - 1)
        {
            print("Stack is Full!");
        }
        else {
            top++;
            stack[top] = value;
            print("Insertion was successful");
        }
    }
    public void Pop() {
        
        if(top == -1) {
            throw new System.IndexOutOfRangeException("Underflow stack is empty!");
        }else {
            print("delete");
            stack[top] = null;
            --top;
        }
    }
    public void Peek() {
        if (top == -1)
        {
            print("Stack is empty");
        }
        else{
            stack[top] = null;
        }
    }
}