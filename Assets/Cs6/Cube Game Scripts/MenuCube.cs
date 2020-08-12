using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCube : MonoBehaviour
{
    public float time = 1f;
    public GameObject cube;
    public float xAngle;
    public float yAngle;
    public float zAngle;

    void Update()
    {
        if(Input.anyKeyDown) {
            StartCoroutine(CubeManager.StartGame(time, STD.CubeUI.LevelTag[1]));
        }
        else if(Input.GetKey(KeyCode.Escape)) 
            Application.Quit();        
        cube.transform.Rotate(xAngle * Time.deltaTime, 
                              yAngle * Time.deltaTime,
                              zAngle * Time.deltaTime,Space.World);
        
    }
}
