



using System.Collections;
using UnityEngine;

/// <summary>
/// Нысандар пулымен жұмыс істейтін сценарийдің мысалы. Бұл нысан бір секунд күтеді, содан кейін пулге оралады.
/// </summary>
public class ReturnAfterDelay : MonoBehaviour, IObjectPoolNotifier
{
    /// <summary>
    /// Пулде жасалғаннан немесе жойылғаннан кейін бізге қажет кез-келген параметрді жасау мүмкіндігі
    /// </summary>
    /// <param name="created"></param>
    public void OnCreatedOrDequeuedFromPool(bool created)
    {
        Debug.Log("Dequeued from object pool!");
        StartCoroutine(DoReturnAfterDelay());
    }
    /// <summary>
    /// Біз Пулге оралғанда қоңырау шалдық
    /// </summary>
    public void OnEnqueuedToPool()
    {
        Debug.Log("Enqueued to object pool!");
    }
    IEnumerator DoReturnAfterDelay()
    {
        // Бір секунд күтіңіз, содан кейін пулге оралыңыз
        yield return new WaitForSeconds(1.0f);
        // Бұл затты ол келген пулге қайтарыңыз
        gameObject.ReturnToPool();
    }
}