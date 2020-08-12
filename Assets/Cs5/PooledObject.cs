using UnityEngine;
/// <summary>
/// ObjectPool сілтемесін сақтау үшін қарапайым сценарий. ReturnToPool кеңейту әдісін қолдану үшін бар.
/// </summary>
public class PooledObject : MonoBehaviour
{
    internal ObjectPool owner;
}
/// <summary>
/// GameObject класына жаңа әдіс қосатын сынып: ReturnToPool
/// </summary>
public static class PooledGameObjectExtensions
{
    /*Бұл әдіс кеңейту әдісі болып табылады ("бұл"параметріне назар аударыңыз). 
     * Бұл  бұл сыныптың барлық даналарына қосылатын жаңа әдіс екенін білдіреді 
     * GameObject; сіз оны келесідей атайсыз:  gameObject.ReturnToPool()*/
    // Нысанды ол жасалған нысандар пулына қайтарады
    public static void ReturnToPool(this GameObject gameObject)
    {
        // PooledObject компонентін табыңыз
        var pooledObject = gameObject.GetComponent<PooledObject>();
        // Ол мүлдем бар ма?
        if (pooledObject == null)
        {
            /*Егер олай болмаса, бұл зат ешқашан  пул шықпады дегенді білдіреді*/
            Debug.LogErrorFormat(gameObject, "Cannot return {0} to object pool, because it was not " +
                "created from one.", gameObject);
            return;
        }
        // Біз келген пулге осы нысанды қайтару керек екенін айтыңыз.
        pooledObject.owner.ReturnObject(gameObject);
    }
}