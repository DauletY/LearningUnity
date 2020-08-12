using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="For game object",menuName = "Object data")]
public class ObjectData : ScriptableObject
{
    public Vector3 vector;
    public float speed;
    public float X { get; set; }
}
