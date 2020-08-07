using UnityEngine;

public class V_StoringDataonDisk : MonoBehaviour {
    public string _filename;
    private void Start () {
        Save(filename: _filename);
    }

    private string Save(string filename) {
        //- Қосымша.persistentDataPath деректерді қауіпсіз  сақтай алатын жолды қамтиды
        var folderToStoreFilesIn = Application.persistentDataPath;

        //Жүйесі.ИО.Жол.Комбайн ағымдағы жүйелік каталог сепараторы арқылы екі жолды біріктіреді 
        // ( \ Windows, / / / барлық басқа платформалар үшін)
        var path = System.IO.Path.Combine(folderToStoreFilesIn, filename);
        return path;
    }
}