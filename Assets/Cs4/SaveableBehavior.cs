using System.Diagnostics;
using LitJson;
using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public abstract class SaveableBehavior : MonoBehaviour, ISaveable, ISerializationCallbackReceiver
{
    // Бұл сыныпты сақталған деп белгілейді
    // Файл  scene редакторда сақталған кезде кодты іске қосуды сұрайды
   
    public abstract JsonData SavedData { get; }
    
    /*/ Бұл _does_ класы SaveID қасиетін жүзеге асырады;
       ол өрісті орайды _saveID.
        (Біз мұны қолмен жасауымыз керек, 
        бірақ  автоматты сипат генерациясын пайдаланбауымыз керек 
        (мысалы, "қоғамдық Жол сақтау  {get;set;}"), 
        өйткені бірлік сахна файлын сақтау кезінде сақтау кезінде автоматты қасиеттерді сақтамайды.
    */
    public string SaveID  {
        get => _saveID;
        set => _saveID = value;
    }
    /*SaveID өрісінде SaveID пайдаланатын нақты деректер сақталады. 
      Біз оны сериялық деп белгілейміз, 
      сондықтан бірлік редакторы оны қалған көрініспен бірге сақтайды және Hideinspector
       ретінде инспекторда көрінбейді (оны өңдеуге ешқандай себеп жоқ).
    */
    [HideInInspector]
    [SerializeField]
    private string _saveID;

    public abstract void LoadFromData(JsonData data);

    //   Onbeforeserialize Unity осы нысанды сақтағысы келгенде шақырылады сахна файлының бөлігі ретінде.
    public void OnBeforeSerialize()
    {
        // Қазіргі уақытта бізде сақтау идентификаторы жоқ па?
        if (_saveID == null) {
            // GUID құру және оның жол мәнін алу арқылы жаңа бірегей идентификатор жасаңыз.
            _saveID = System.Guid.NewGuid().ToString();
        }
    }
    // Onafterdeserialize бірлік бұл нысанды сахна файлының бөлігі ретінде жүктеген кезде шақырылады.
    public void OnAfterDeserialize()
    {
        // Мұнда ерекше ештеңе жасаудың қажеті жоқ, бірақ әдіс ISerializationCallbackReceiver жүзеге асыру үшін болуы керек
    }
   
    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}