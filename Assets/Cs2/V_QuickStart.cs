
// Сериялаудың практикалық мысалы / десериализация



using UnityEngine;
using System.IO;
using CLR = System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class V_QuickStart  {
    public static void _Main() {
        // Кейіннен ағынға серияландыру үшін объектілер графигін жасау
        var objectGraph = new List<CLR.String>() {
            "Daulet", "Nauriz", "Ayazhan"
        };
        Stream stream = SerializeToMemory(objectGraph);
        // Біз осы мысал үшін бәрін нөлдейміз
        stream.Position = 0;
        objectGraph = null;

        // Объектілерді десериализациялау және олардың жұмыс қабілеттілігін тексеру

        objectGraph = DeserializeFromMemory(stream);
        foreach (var item in objectGraph)
        {
            MonoBehaviour.print(item);
        }
        
    }

    private static List<string> DeserializeFromMemory(Stream stream)
    {
        // Серияландыру кезінде пішімдеу тапсырмасы
        BinaryFormatter formatter = new BinaryFormatter();

        // Пішімдеу модулін ағыннан нысандарды десериализациялау
        return (List<CLR.String>)formatter.Deserialize(stream);
    }
    private static MemoryStream  SerializeToMemory(List<string> objectGraph)
    {
        // Құрамында болатын ағынды жобалау серияланған Нысандар
         MemoryStream stream = new MemoryStream();
         
         // Серияландыру кезінде пішімдеу тапсырмасы
         BinaryFormatter formatter = new BinaryFormatter();

         // Пішімдеу модулін нысандарды ағынға сериялауға мәжбүр етеміз
         formatter.Serialize(stream, objectGraph);

         // Серияланған объектілердің ағынын шақыру әдісіне қайтару
         return stream;
    }
}