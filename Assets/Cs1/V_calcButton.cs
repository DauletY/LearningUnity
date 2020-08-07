using UnityEngine;

public class V_calcButton : MonoBehaviour {
    public V_calc2 Calculator;
    public string value;
    private void OnMouseDown() {
        Calculator.SetInputValue(value);
    }
}