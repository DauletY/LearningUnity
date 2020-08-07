using UnityEngine;
using UnityEngine.UI;

public class V_str : MonoBehaviour {
    public Text  gt;
    public Text _text;
    private void Start() {
       
    }

    private void Update() {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (gt.text.Length != 0)
                {
                    gt.text = gt.text.Substring(0, gt.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                 _text.text = gt.text + " Welcome!";
            }
            else
            {
                gt.text += c;
            }
        }
    }
}