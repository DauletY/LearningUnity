using UnityEngine;

public class V_MoverManager : V_Singleton<MonoBehaviour> {
    private void Awake() {
        ManageMovers();
    }
    public void ManageMovers() {
         Debug.Log("Doing some useful work!");
    }

    private void Update() {
        V_MoverManager.instance.transform.Translate(Vector2.right * 2f * Time.deltaTime);
    }
}