



using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class V_calc2 : MonoBehaviour {
    public Text _input;
    public Text _result;

     string _op;
    float _x, _y;
    
    private void Start() {
        ResetAll();
    }
    //Handle values coming from button clicks
    public void SetInputValue(string s){
		//Clear fields if _input contains letters
		if (_input.text.ToString ().Any (x => char.IsLetter (x))) {
			// ResetAll(); 
           ResetAll();  
		}

		//Handles button input values
		switch (s) {
				case "+":
				case "-":
				case "/":
				case "*":
				case "=":
				 Calculate(s);
				break;
			case "ce":
				  ClearLastInput();
				break;
			case "c":
				 ResetAll();
				break;
			case "del":
			 	DeleteLastCharacter();
				break;
			default:
				 AddCharacter(s);
				break;
		}
	}

    //Add character input to input text string
	private void AddCharacter(string s){
		//Add only one decimal point
		if(s == "."){
			if(!_input.text.ToString().Contains(".")){
				_input.text += s;
			}
		//Remove 0 in front of number
		}else if(_input.text == "0"){
			_input.text = s;
		//Concatenate the input string
		}else{
			_input.text += s;
		}
	}

    //Reset input field
    private void ClearLastInput(){
		_input.text = "0";
            print("ClearLastInput");
	}

    //Prepare values before the calculation Подготовьте значения перед вычислением
    private void Calculate(string s){
		//If result field is empty
		if (_result.text == "" && s != "=") {
			_x = Convert.ToSingle(_input.text);
			_op = s;
			_result.text = _input.text + " " + s;
			_input.text = "0";
			return;
		}

		//Handles division by 0
		if(_op == "/" && _input.text == "0"){
           ResetAll();
			_input.text = "Cannot divide by zero";
			return;
		}

		//Calculates float result
		_y = Convert.ToSingle(_input.text);

		if(s == "="){
			ClearField();
			_input.text = CalculatePair(_x, _y, _op).ToString();
		} else {
			_x = Convert.ToSingle(CalculatePair(_x, _y, _op));
			_result.text = _x.ToString() + " " + s;
			_op = s;
			_input.text = "0";
		}
	}

    private void ClearField(){
		_result.text = "";
		_input.text = "0";
        print("clear");
	}

    private float CalculatePair(float x, float y, string op)
    {
          float result = 0.0f;
          switch(op) {
              case "+": result = x + y; break;
              
              case "*": result = x * y; break;
              
              case "/": result = x / y; break;
              
              case "-": result = x - y; break;

          }
          return result;
    }

    private void ResetAll()
    {
        ClearField();
		_x = 0.0f;
		_y = 0.0f;
		_op = "";
        print("ResetAll");
    }
    //Delete last character of the input string when CE is pressed
	private void DeleteLastCharacter(){
		_input.text = _input.text.ToString().Remove(_input.text.Length - 1);

		if(_input.text == ""){
			_input.text = "0";
		}

            print("DeleteLastCharacter");
	}
}
