using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class V_testScene2 : MonoBehaviour {
    [SerializeField] private string sceneName;
    public string SceneName {
        get => sceneName;
        set => sceneName =value;
    }
    [SerializeField] private float seconds;
    
    public float Seconds {
        get => seconds;
        set => seconds = value;
    }


    public void SelectGame() {
        StartCoroutine("LoadScene");
    }
    private IEnumerator LoadScene() {
        Debug.Log("Load scene..");
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneName);
    }
}