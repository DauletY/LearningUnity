
using LitJson;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public static class SavingService {
    /*// Қате теруден туындаған проблемаларды болдырмау үшін біз 
    JSON-да элементтерді тұрақты жолдар ретінде сақтау және іздеу үшін қолданатын жол атауларын сақтаймыз  */
    private const string ACTIVE_SCENE_KEY = "activeScene"; 
    private const string SCENES_KEY = "scenes"; 
    private const string OBJECTS_KEY = "objects"; 
    private const string SAVEID_KEY = "$saveID";
    // Нысанның күйін қалпына келтіретін көріністі жүктегеннен кейін іске қосылатын делегатқа сілтеме.
    static UnityAction<Scene,LoadSceneMode> LoadObjectsAfterSceneLoad;

    // (соқтығысу ықтималдығын азайту үшін мұнда сақтау идентификаторы үшін күтпеген " $ " таңбасын қолданыңыз // )
    // Ойынды сақтайды және оны тұрақты деректер каталогында fileName деп аталатын файлға жазады.
    public static void SaveGame (string fileName ) {
        // Jsondata жасаңыз, біз оны дискіге жазамыз
        var result = new JsonData();
        /* Барлық MonoBehaviours-ті табыңыз, алдымен әрбір жеке  MonoBehaviour-ны тауып, оны тек 
            ISaveable-ді қосу үшін сүзгіден өткізіңіз.
         */
        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
        // Бізде сақтау нысандары бар ма?
        if(allSaveableObjects.Count() > 0) {
            
            var savedObjects = new JsonData();
            // Біз сақтағымыз келетін әр нысанды сұрыптаңыз
            foreach (var saveableObject in allSaveableObjects)
            {
                // Сақталған нысан деректерін алу
                var data = saveableObject.SavedData;
                /* Біз бұл объект болады деп күтеміз (JSON сөздікке арналған термин ), 
                    өйткені Save объектісінің идентификаторын қосу керек*/
                if(data.IsObject) {
                    // Осы нысан үшін сақтау идентификаторын жазыңыз
                    data[SAVEID_KEY] = saveableObject.SaveID;
                    // Сақталған нысан деректерін жинаққа қосыңыз
                    savedObjects.Add(data);
                }else {

                    //  Осы нысанды сақтай алмайтынымыз туралы пайдалы ескерту беріңіз.
                    var behaviour = saveableObject as MonoBehaviour; 

                    Debug.LogWarningFormat
                    ( behaviour, 
                         "{0}'s save data is not a dictionary. The " + "object was not saved.",
                     behaviour.name
                    );
                }
            }
            // Нәтижесінде сақталған нысандар жиынтығын сақтаңыз
            result[OBJECTS_KEY] = savedObjects;
        }else {
            // Бізде құтқару үшін заттар жоқ. Жақсы ескерту беріңіз.
            Debug.LogWarningFormat( "The scene did not include any saveable objects.");
        }
          /* Содан кейін біз қандай көріністердің ашылғанын жазуымыз керек. 
         Бірлік сізге бір уақытта бірнеше көріністерді ашуға мүмкіндік береді,
         сондықтан біз олардың барлығын, сондай-ақ "белсенді" көріністі сақтауымыз керек
         (жаңа нысандар қосылатын және ойын үшін жарықтандыру параметрлерін басқаратын сахна).*/
        // Ашық көріністер тізімін сақтайтын JsonData жасаңыз
        var openScenes = new JsonData();

        /* Сахна менеджерінен қанша көріністің ашық екенін сұраңыз және олардың әрқайсысы үшін сахнаның атын сақтаңыз.*/
        var sceneCount = SceneManager.sceneCount;

        for(int i = 0; i < sceneCount; i++) {
            var scene = SceneManager.GetSceneAt(i);
            openScenes.Add(scene.name);
        }
        // Ашық көріністер тізімін сақтаңыз
        result[SCENES_KEY] = openScenes;
        // Белсенді сахнаның атын сақтаңыз
        result[ACTIVE_SCENE_KEY] = SceneManager.GetActiveScene().name;
        /*Енді біз сақталған деректерді жасауды аяқтадық және оларды дискіге жазу уақыты келді.*/
        /* Деректерге тұрақты жолды осы әдіспен алынған файл атауымен параметр ретінде біріктіру арқылы файлды қайда қою керектігін біліңіз.*/
        var outputPath = Path.Combine(Application.persistentDataPath, fileName);
        /* JsonWriter жасаңыз және оны "әдемі деректерді басып шығаруға" теңшеңіз. 
          Бұл міндетті емес (сіз result-ке қоңырау шала аласыз.Tojson () 
          no JsonWriter параметрімен және жолды алу арқылы), 
          бірақ осылайша JSON-ны дамыту кезінде не пайдалы екенін оқып, түсіну оңай.
          */
          var writer = new JsonWriter();
          writer.PrettyPrint = true;

          // Сақталған деректерді JSON мәтініне түрлендіріңіз.
          result.ToJson(writer);

          // JSON мәтінін дискіге жазыңыз.
          File.WriteAllText(outputPath, writer.ToString());  
          // Сақталған ойынды қайдан табуға болатынын айтыңыз
          Debug.LogFormat("Wrote saved game to {0}", outputPath);
          /*
             Біз мұнда көп жад бөлдік,яғни қоқыс жинаушы болашақта іске қосылатын ықтималдығы жоғары. 
             Барлығын ретке келтіру үшін біз сақталған деректер сілтемесін шығарамыз, 
             содан кейін қоқыс жинаушыдан дереу іске қосуды сұраймыз. 
             Бұл коллектор іске қосылған кезде өнімділіктің аздап төмендеуіне әкеледі, 
             бірақ бұл жағдайда бұл қалыпты жағдай,  өйткені пайдаланушылар сақтау  
                  ойындар бір секундқа тоқтайды деп күтеді.
          */
          result =  null;
          System.GC.Collect();
    }
    // Берілген файлдан ойынды жүктейді және оның күйін қалпына келтіреді
    public static bool LoadGame(string fileName) {
        // Файлды қайдан табуға болатындығын біліңіз
        var dataPath = Path.Combine(Application.persistentDataPath, fileName);
        // Файл шынымен бар екеніне көз жеткізіңіз.
        if (File.Exists(dataPath) == false) { 
            Debug.LogErrorFormat("No file exists at {0}", dataPath); 
            return false;
        }
        // Деректерді JSON форматында оқыңыз.
        var text = File.ReadAllText(dataPath);
        var data = JsonMapper.ToObject(text);

        /*Деректерді сәтті оқығанымызға және оның нысан екеніне көз жеткізіңіз (яғни JSON сөздігі)*/
        if (data == null || data.IsObject == false) { 
            Debug.LogErrorFormat( "Data at {0} is not a JSON object", dataPath);
            return false; 
        }
        // Біз қандай көріністерді жүктейтінімізді білуіміз керек.
        if (!data.ContainsKey("scenes")) {
            Debug.LogWarningFormat( "Data at {0} does not contain any scenes; not " + "loading any!", dataPath); 
            return false; 
        }
        // Көріністер тізімін алыңыз
        var scenes = data[SCENES_KEY];
        int sceneCount = scenes.Count;
        if (sceneCount == 0) { 
            Debug.LogWarningFormat( "Data at {0} doesn't specify any scenes to load.", dataPath); 
            return false; 
        }
        // Әрбір белгілі бір көрініске жүктеме.
        for(int i = 0; i < sceneCount; i++) {
            var scene = (string)scenes[i];
            // Егер бұл біз жүктейтін алғашқы көрініс болса, оны жүктеп алыңыз және 
            // барлық басқа белсенді көріністерді ауыстырыңыз.
            if(i == 0) {
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }else {
                // Әйтпесе, бұл көріністі барлардың үстіне жүктеңіз.
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        }
        // Белсенді көріністі тауып, оны орнатыңыз
        if (data.ContainsKey(ACTIVE_SCENE_KEY)) {
            var activeSceneName = (string)data[ACTIVE_SCENE_KEY];
            var activeScene = SceneManager.GetSceneByName(activeSceneName);
            if (activeScene.IsValid() == false) {
                Debug.LogErrorFormat("Data at {0} specifies an active scene that " + "doesn't exist. Stopping loading here.",dataPath );
                return false;
            }
            SceneManager.SetActiveScene(activeScene);
        }else {
            // Бұл қате емес, өйткені тізімдегі алғашқы көрініс Белсенді деп саналады, бірақ бұл туралы ескерту керек.
            Debug.LogWarningFormat("Data at {0} does not specify an " + "active scene.", dataPath);
        }
        // Сахнадағы барлық нысандарды тауып, оларды жүктеңіз
        if (data.ContainsKey(OBJECTS_KEY)) {
            var objects = data[OBJECTS_KEY];
            /*
               Біз нысандардың күйін бірден жаңарта алмаймыз,  өйткені бірлік болашақта сахнаны жүктеуді аяқтамайды.
               Біз нысандарға енгізген кез-келген өзгерістер бастапқы сахнада қалай анықталғанына қайтарылады. 
               Нәтижесінде сахна менеджер сахнаның жүктеуді аяқтағанын хабарлағаннан кейін кодты іске қосу керек.
             
                Ол үшін біз нысандарды жүктеу кодын қамтитын жаңа делегат құрып, 
                оны  LoadObjectsAfterSceneLoad файлында сақтаймыз. 
                Бұл делегат сахнаны жүктеу аяқталғаннан кейін оны іске қосатын 
                SceneManager-де sceneLoaded оқиғасына қосылады.
                
             */
             LoadObjectsAfterSceneLoad = (scene, loadSceneMode) => {
                 /*Барлық қол жетімді нысандарды табыңыз және олардың сақталған идентификаторларын
                   объектімен салыстыратын сөздік құрыңыз (сондықтан біз оларды тез іздей аламыз 
                   )*/
                   var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToDictionary(o => o.SaveID, o => o );
                   // Жүктеу керек Нысандар жинағын алыңыз
                   var objectsCount = objects.Count;
                   // Тізімдегі әрбір элемент үшін...
                   for (int i = 0; i < objectsCount; i++) {
                       // Сақталған деректерді алу
                       var objectData = objects[i];
                       // Осы деректерден сақтау идентификаторын алыңыз
                       var saveID = (string)objectData[SAVEID_KEY];
                       // Осы сақтау идентификаторы бар сахнада (көріністерде) нысанды табуға тырысыңыз
                       if (allLoadableObjects.ContainsKey(saveID)) {
                               var loadableObject = allLoadableObjects[saveID];
                               // Осы деректерден нысанды жүктеуді сұраңыз
                               loadableObject.LoadFromData(objectData);
                           }
                       }
                       // Өзіңізге тапсырыс беріңіз; осы делегатты оқиғадан алып тастаңыз келесі жолы шақырылмауы үшін sceneLoaded
                       SceneManager.sceneLoaded -= LoadObjectsAfterSceneLoad;
                       // Делегатқа сілтеме жіберіңіз
                       LoadObjectsAfterSceneLoad = null;
                       /*(/ 
                          Және қоқыс жинаушыдан өзіңізді тәртіпке келтіруді сұраңыз тағы да, 
                          бұл  өнімділіктің бұзылуына әкеледі, бірақ пайдаланушылар бұған қарсы емес, 
                          өйткені олар сахнаны жүктеудің аяқталуын күтуде 
                        )*/
                        System.GC.Collect();
                   };
                   // Сахнадан кейін іске қосу үшін объектінің жүктеу кодын тіркеңіз  
                   SceneManager.sceneLoaded += LoadObjectsAfterSceneLoad;
        }
        return true;
    }
}
// Any MonoBehaviour that implements the ISaveable interface will be saved in the scene, and loaded back
public interface ISaveable {
    // The Save ID is a unique string that identifies a component in the 
    // save data. It's used for finding that object again when the game is loaded. 
    string SaveID { get; }

    // The SavedData is the content that will be written to disk. It's  asked for when the game is saved. 
    JsonData SavedData { get; }
    
    // LoadFromData is called when the game is being loaded. The object is 
    // provided with the data that was read, and is expected to use that
     // information to restore its previous state.
     /*LoadFromData ойынды жүктеу кезінде шақырылады. Нысан келесідей 
    // оқылған деректермен берілген және олар пайдаланылуы күтілуде
     // оның алдыңғы күйін қалпына келтіруге арналған ақпарат*/
     void LoadFromData(JsonData data);

}