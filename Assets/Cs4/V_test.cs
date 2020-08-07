using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_test : MonoBehaviour
{
    public string fileName;
    // Start is called before the first frame update
    void Start()
    {
        SavingService.SaveGame(fileName);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
