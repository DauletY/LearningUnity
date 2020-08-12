

using UnityEngine;
using System.Collections.Generic;
public class ObjectPool : MonoBehaviour
{
    // Құрылатын Префаб.
    [SerializeField] private GameObject prefab;
    // Қазіргі уақытта пайдаланылмайтын объектілердің кезегі
    private Queue<GameObject> inactiveObjects = new Queue<GameObject>();
    /// <summary>
    /// Нысанды пулдан алады. Егер ол кезекте болмаса, жаңа жасалады.
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        // Қайта пайдалану үшін белсенді емес нысандар бар ма?
        if (inactiveObjects.Count > 0)
        {
            // Объектіні кезектен алу
            var dequeuedObject = inactiveObjects.Dequeue();
            /*Кезекте тұрған нысандар пулдың еншілес элементтері болып табылады, сондықтан біз оларды 
             * керісінше түбірге жылжытамыз*/
            dequeuedObject.transform.parent = null;
            dequeuedObject.SetActive(true);
            /*Егер IObjectPoolNotifier-ді іске асыратын осы объектіде MonoBehaviours болса, оларға осы нысан пул тастап кеткенін айтыңыз*/
            var notifiers = dequeuedObject.GetComponents<IObjectPoolNotifier>();
            foreach (var notifier in notifiers)
            {
                // Сценарийді пулдан кеткені туралы хабардар етіңіз
                notifier.OnCreatedOrDequeuedFromPool(false);
            }
            //  Пайдалану үшін нысанды қайтару
            return dequeuedObject;
        }
        else
        {
            // Пул қайта пайдалануға болатын ештеңе жоқ. Жаңа нысанды жасаңыз.
            var newObject = Instantiate(prefab);
            // Пулга оралу үшін пул тегін қосыңыз  аяқтаған кезде.
            var poolTag = newObject.AddComponent<PooledObject>();
            poolTag.owner = this;
            /* Пулдың тегін инспекторда ешқашан көрінбейтіндей етіп белгілеңіз
             * Онда теңшелетін ештеңе жоқ; ол тек оны жасайтын пул сілтеме жасау үшін бар.*/
            poolTag.hideFlags = HideFlags.HideInInspector;
            // Объектіні оның құрылғандығы туралы хабарлау
            var notifiers = newObject.GetComponents<IObjectPoolNotifier>();
            foreach (var notifier in notifiers)
            {
                // хабарлау керек, ол тек қана құрылды
                notifier.OnCreatedOrDequeuedFromPool(true);
            }
            // Жаңадан құрылған нысанды қайтарыңыз.
            return newObject;
        }
    }
    /// <summary>
    ///  Нысанды өшіреді және оны қайта пайдалану үшін кезекке қайтарады
    /// </summary>
    /// <param name="gameObject"></param>
    public void ReturnObject(GameObject gameObject)
    {
        // Хабарлауымыз керек кез-келген компонентті табыңыз
        var notifiers = gameObject.GetComponents<IObjectPoolNotifier>();
        foreach (var notifier in notifiers)
        {
            // Оған пулге оралатынын айтыңыз 
            notifier.OnEnqueuedToPool();
        }
        // Нысанды өшіріп, иерархияны бітеп тастамас үшін оны осы нысанға қатысты жасаңыз
        gameObject.SetActive(false); 
        gameObject.transform.parent = this.transform;
        // Нысанды белсенді емес кезекке қойыңыз
        inactiveObjects.Enqueue(gameObject);
    }
}
