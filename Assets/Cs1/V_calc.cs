using UnityEngine;
using UnityEngine.UI;
public class V_calc : MonoBehaviour {
    public Text one;
    public Text two;

    public Text write_screen;
    public int oneC = 1, twoC = 2, equal = 0;
    private void Start() {
        
    }
    // add
    public void _click1() {
        oneC = 1;
        // write to the screen
        write_screen.text = oneC.ToString();
    }
    public void _click2() {
        twoC = 2;
        write_screen.text = twoC.ToString();
    }
    public void Equals() {
    
        write_screen.text = equal.ToString();
    }

    public void Operation(string oper) {
        switch (oper) {
            case "+":
                
            break;
        }
    }
    public void Plus() {
         equal = (oneC + twoC);
    }

    
    public void Zero() {
        equal = 0;
        oneC = 0;
        twoC = 0;


    }
}