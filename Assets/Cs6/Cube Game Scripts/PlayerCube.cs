using STD;
using UnityEngine;
using System.Collections;



namespace STD {
    using  TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.VFX;

    [System.Serializable]
    public class CubeUI {
        
        public static string[] LevelTag = {
              "MenuCubeGame", "CubeLevel1", "CubeLevel2" , "CubeLevel3", "Score",
              "Erekeshov Daulet", "End"
        };
        public TMP_Text m_start_text = null;
        public TMP_Text m_win_text = null;
        public TMP_Text m_score = null;
        public Image image = null;
        public void SetActiveUI(bool value ){
            m_start_text.gameObject.SetActive(value);
            image.gameObject.SetActive(value);
        }
    }
}
public class PlayerCube : MonoBehaviour
{
    [SerializeField] ObjectData objectData;
    [SerializeField] TimeScripts timeScripts;
    [SerializeField] Camera cameraC;
    [SerializeField] CubeUI cubeUI = new CubeUI();
    [SerializeField] GameObject ScoreObject;
    public float m_start_time = 1f;
    int score = 0;
    // Use this for initialization
    void Start()
    {
        // set false-<
         cubeUI.SetActiveUI(false);
         cubeUI.m_win_text.gameObject.SetActive(false);
        Init(); // speed 10ms
        cubeUI.m_score.text = CubeUI.LevelTag[4] + ": " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        objectData.X = Input.GetAxis("Horizontal") * objectData.speed;
        objectData.vector = new Vector3(objectData.X, 0f, 0f);
        transform.Translate(objectData.vector * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("level1"))
        {
            print("level1");
            cubeUI.m_win_text.gameObject.SetActive(true); // win text
            StartCoroutine(CubeManager.StartGame(m_start_time, CubeUI.LevelTag[2]));  
        }
        else if(other.CompareTag("level2"))
        {
            print("level2");
            cubeUI.m_win_text.gameObject.SetActive(true); // win text
            StartCoroutine(CubeManager.StartGame(m_start_time, CubeUI.LevelTag[3]));
            GetComponent<Renderer>().material.color = Color.white;
        }
        else if(other.CompareTag("level3")) {
    
            StartCoroutine(WinGame(3f,CubeUI.LevelTag[6]));
        }
        else if(other.CompareTag("Score")) {
            score++;
            cubeUI.m_score.text = CubeUI.LevelTag[4] + ":" + score.ToString();
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Exit")
        {
            StartCoroutine(CubeManager.StartGame(m_start_time,CubeUI.LevelTag[1]));
        }
        else if (other.gameObject.tag == "CubeEnemy")
        {
            GetComponent<Renderer>().material.color = Color.red;
            print("dead Cube player");
            StartCoroutine(PlayerDie(timeScripts.seconds));
        }
    }
    private void Init() {
        objectData.speed = 10f;
    }
    private void ResetCube() {
        objectData.vector = new Vector3(0f,0f,0f);
        objectData.speed = 0f;
    }
    private void ResetScore() {
          ScoreObject.SetActive(false);
    }
    private IEnumerator PlayerDie(float seconds) {
        cubeUI.SetActiveUI(true);
        ResetCube();
        score = 0;
        ResetScore(); // score same value
        yield return new WaitForSeconds(seconds);
        UnityEngine.SceneManagement.SceneManager.LoadScene(CubeUI.LevelTag[0]);
    }
    IEnumerator WinGame(float t, string n) {
        cubeUI.m_win_text.gameObject.SetActive(true); // win text 
        yield return new WaitForSeconds(t);
        Debug.LogFormat("You min");        print("level3");
        UnityEngine.SceneManagement.SceneManager.LoadScene(n);
    }
}
