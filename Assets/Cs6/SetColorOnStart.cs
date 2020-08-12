using UnityEngine;
using System.Collections;

public class SetColorOnStart : MonoBehaviour
{
    // Біз деректерді алатын ScriptableObject
    [SerializeField] ObjectColor objectColor;
    // Мұны инициализация үшін пайдаланыңыз
    void Start()
    {

    }

    // Жаңарту кадрға бір рет шақырылады
    void Update()
    {
        //Obj objectcolor, егер ол берілмеген болса
        if (objectColor == null)
        {
            return;
        }
        GetComponent<Renderer>().material.color = objectColor.color;
        /*Сіз бұл әдісті объектінің деректерін оның логикасынан бөліп, ойындағы бірнеше түрлі нысандар үшін деректерді қайта пайдалану үшін қолдана аласыз. Бұл тәсіл 
         * Сонымен қатар объект үшін барлық мәліметтер жиынтығын тез ауыстыруды жеңілдетеді—қолданылатын активті өзгерту керек.*/
    }
}
