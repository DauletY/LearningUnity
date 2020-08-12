using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour
{
    [SerializeField] ObjectData objectData;
    public float speed = 0f;
    public float borderZ, borderTarget;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        objectData.vector = new Vector3(0,0,speed * Time.deltaTime);
        transform.Translate(objectData.vector);
        if(transform.position.z <= borderZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, borderTarget);
        }
    }
}
