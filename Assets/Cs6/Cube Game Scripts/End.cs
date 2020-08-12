using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class End  : MonoBehaviour {
    public TMP_Text mP_Text = null;
    public Button back = null;
    public float x, y, z;
   public float startTime ;
   public float time;
    
    private void Start() {
        back.gameObject.SetActive(false);
       
    }
    private void Update() {
        Vector3 text_dir = new Vector3(x * Time.deltaTime, 
                                       y * Time.deltaTime, 
                                       z * Time.deltaTime);
        mP_Text.rectTransform.Translate(text_dir);
   
        // eger uakittan kein button paida bolsa
        time += Time.deltaTime;
        if(time >= startTime) {
            time = 0;
             back.gameObject.SetActive(true);
        }
        if(mP_Text) {  /* Ойынды дайындаган...назарынызга рахмет 
                        Мени осында каруга болады: 
                        Ойын версиясы 
                         Жоба аяталган уакыты
                         Казыргы уакыт*/
             mP_Text.text = "The game wrote  " + STD.CubeUI.LevelTag[5] + " thank you for your attention!" + "\n" + 
                       "I'm here " + Application.absoluteURL + "https://github.com/DauletY" + "\n" + 
                       "Game version: " + Application.version + "\n" + 
                       "The project is completed : " + "12:08:20 " + "Time: 15:05 " + "\n" + 
                       "Date time now: " + System.DateTime.Now +  " ".ToString();
        }
    }
    public void Back() {
        StartCoroutine(CubeManager.StartGame(3f, STD.CubeUI.LevelTag[0]));
    }
}