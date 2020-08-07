using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class V_testScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) {
            var operation = SceneManager.LoadSceneAsync("Menu");
            //Don't proceed to the scene once loading has finished
            operation.allowSceneActivation = false;
            // Start a coroutine that will run while scene loads, and 
            // will run some code after loading finishes
            StartCoroutine(WaitForLoading(operation));
        }
    }
    IEnumerator WaitForLoading(AsyncOperation operation) {
        while (operation.progress < 0.9f) {
            yield return null;
        }
        // We're done!
        Debug.Log("Loading complede!");
        // Enabling scene activation will immediately start loading  the scene

        operation.allowSceneActivation = true;
    }
}
