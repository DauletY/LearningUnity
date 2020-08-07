using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class V_scene1 : MonoBehaviour {
    private void Update() {
        if(Input.GetKey(KeyCode.Alpha1)) {
            // Ағымдағы жүктелген көріністерге көріністі қосады.
            SceneManager.LoadScene("Scene1" ,LoadSceneMode.Additive);

            // Асинхронды операция сахна түсіру, қалай жүктеу мүмкін
            var unloadOperatoin = SceneManager.UnloadSceneAsync("Scene1");
            
            //// Егер сіз жүктеу аяқталғаннан кейін кодты іске қосқыңыз келсе, 
            // бағдарламаны іске қосыңыз (қайтадан жүктеу сияқты)
            StartCoroutine(WaitForUnload(unloadOperatoin));
        
        }else if(Input.GetKey(KeyCode.Alpha2)) {
            StartCoroutine(WaitScene());
        }   
    }

    private IEnumerator WaitForUnload(AsyncOperation unloadOperatoin)
    {
        // Берілген делегат true мәнін қабылдағанға дейін бағдарлама орындалуын тоқтатады.
        yield return new WaitUntil(() => unloadOperatoin.isDone);


        /* Түсіру аяқталды және сахнада болған барлық нысандар жойылды. 
            Алайда, бірлік текстуралар сияқты осы нысандарға сілтеме жасайтын активтерді жүктемейді.
            Олар басқа нысандарды кейіннен пайдалану үшін жадта қалады;
             егер жадты босатқыңыз келсе, оны жасаңыз:
        */
        // Пайдаланылмаған активтерді түсіреді.
        Resources.UnloadUnusedAssets();
    }
    private IEnumerator WaitScene() {
          // Барлық жүктелген көріністерді жабады және сахнаны жүктейді.
           SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
            yield return null;
    }
}