using LitJson;
using UnityEngine;

public class TransformSaver : SaveableBehavior
{
    public string fileName;
    /* Қате мәселелерді болдырмас үшін сақталған ойынға қосылатын кілттерді тұрақтылар ретінде сақтаңыз..*/

    private const string LOCAL_POSITION_KEY = "localPosition"; 
    private const string LOCAL_ROTATION_KEY = "localRotation"; 
    private const string LOCAL_SCALE_KEY = "localScale";

    /*SerializeValue-бұл сақталған ойынға қосыла алатын jsondata-ға сериялауды (мысалы, Vector3, Quaternion және басқалар) білетін объектіні түрлендіретін көмекші функция.*/
    private JsonData SerializeValue(object obj)  {
        /* Бұл өте тиімсіз (біз нысанды JSON-ға айналдырамыз
           мәтін, содан кейін бұл мәтінді бірден JSON көрінісіне қайта талдаңыз,
          бірақ бұл біріктірілген бірлік түрлері үшін сериализация кодын жазудың қажеті жоқ дегенді білдіреді
        */

        return JsonMapper.ToObject(JsonUtility.ToJson(obj));
    }
    /* DeserializeValue керісінше жұмыс істейді: JsonData-ны ескере отырып, егер бұл түрі бірлік болса  қажетті түрдің мәнін шығарады. / */
    private T DeserializeValue<T>(JsonData data) { 
        return JsonUtility.FromJson<T>(data.ToJson());
    }
    // Бұл компонент үшін сақталған деректерді ұсынады.
    public override JsonData SavedData  {
        get {// / Сақталған ойынға қайтаратын JsonData құрыңыз жүйе
            var result = new JsonData();
            // Біздің позициямызды, айналуымызды және масштабты сақтаңыз
            result[LOCAL_POSITION_KEY] = SerializeValue(transform.localPosition);
            result[LOCAL_ROTATION_KEY] = SerializeValue(transform.localRotation);
            result[LOCAL_SCALE_KEY] = SerializeValue(transform.localScale);
            return result;
        }
    }
    // Кейбір жүктелген деректерді ескере отырып, компоненттің күйін жаңартады.
    public override void LoadFromData(JsonData data)
    {
        /*
             Деректерде біз сақтайтын деректердің әр бөлігі болады деп болжай алмаймыз; 
             бағдарламашының мақалын есте сақтаңыз: 
            "сіз жасаған нәрсеңізге қатал болыңыз және қабылдағаныңызды кешір.
          
             Тиісінше, біз әрбір элементтің сақталған  деректерде бар-жоғын тексереміз
        */
        // Позицияны жаңарту
        if (data.ContainsKey(LOCAL_POSITION_KEY)) {
            transform.localPosition = DeserializeValue<Vector3>(data[LOCAL_POSITION_KEY]);
        }
        // Ротацияны жаңарту
        if (data.ContainsKey(LOCAL_ROTATION_KEY)) {
            transform.localRotation = DeserializeValue<Quaternion>(data[LOCAL_ROTATION_KEY]);
        }
        // Жаңарту шкаласы
        if (data.ContainsKey(LOCAL_SCALE_KEY)) {
            transform.localScale = DeserializeValue<Vector3>(data[LOCAL_SCALE_KEY]);
        }
        /*Бұл сценарийді пайдалану үшін ойын нысанына TransformSaver компонентін қосыңыз.
         Ол сақталған ойындарға автоматты түрде қосылады және ойын жүктелген кезде оның күйі қалпына келтіріледі.*/
    }
    private void Start() {
       
    }
    private void Update() {
       
    }
}