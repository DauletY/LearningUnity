using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Создать учетную запись в ОС -> создать меню, которое позволяет легко создавать  новых активов подобного типа
[CreateAssetMenu]
//Не забудьте изменить родительский класс от 'скрипта' в ScriptableObject
public class ObjectColor : ScriptableObject
{
    public Color color;   
}
