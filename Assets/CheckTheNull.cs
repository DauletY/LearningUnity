using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Camera))]
public class CheckTheNull : MonoBehaviour
{
    public Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInParent<Camera>();
    }
    void Test() {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        if (mesh == null) return;
    }
    // Update is called once per frame
    void Update()
    {
        this.cam.transform.Rotate(Vector3.back * 5 * Time.deltaTime);
    }
}
