using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class CubeManager : MonoBehaviour
{

    public static  IEnumerator StartGame(float time, string name_level)
    {
        print("game over");
        yield return new WaitForSeconds(time);
        print("start game");
        SceneManager.LoadScene(name_level);
       
    }
}
