





using UnityEngine;
using System.Collections;
/// <summary>
/// Нысандар пулын қолдану мысалы
/// </summary>
public class ObjectPoolDemo : MonoBehaviour
{
    // Біз өз нысандарымызды алатын нысандар пулы
    [SerializeField] private ObjectPool pool;
    IEnumerator Start()
    {
        // Әр 0,1-0,5 секунд сайын пулдан нысанды алыңыз және орналастырыңыз
        while (true)
        {
            // Пулдан объектіні алу (немесе жасау, бізге бәрібір)
            var o = pool.GetObject();
            // 4 радиус сферасының ішіндегі нүктені таңдаңыз
            var position = Random.insideUnitSphere * 4;
            // Оны орналастыру
            o.transform.position = position;
            // 0,1-ден 0,5 секундқа дейін күтіңіз және оны қайтадан жасаңыз
            var delay = Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(delay);
        }
    }
}
