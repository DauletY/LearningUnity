




using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public Transform pathHolder;
  
    private void Start()
    {
        // жол нүктелері
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x , waypoints[i].y);
        }
        StartCoroutine(FollowPath(waypoints));
    }
    private void Update()
    {
    }
    private void BoxAuto()
    {
      
    }
    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];
        int targetWayPsIndex = 1;
        Vector3 targetWayPs = waypoints[targetWayPsIndex];

        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWayPs, speed * Time.deltaTime);
            if(transform.position == targetWayPs)
            {
                targetWayPsIndex = (targetWayPsIndex + 1) % waypoints.Length;
                targetWayPs = waypoints[targetWayPsIndex];
                yield return new WaitForSeconds(waitTime);
            }

            yield return null;
        }
    }
    private void OnDrawGizmos()
    {
        Vector2 startPos = pathHolder.GetChild(0).position;
        Vector2 previousPos = startPos;
        foreach (Transform item in pathHolder)
        {
            Gizmos.DrawSphere(item.position, .3f);
            Gizmos.DrawLine(previousPos, item.position);
            previousPos = item.position;
        }
        Gizmos.DrawLine(previousPos, startPos);
    }
    private Vector3 BoxManager()
    {
         Vector3 boxVec = default;

        float speed = 10f * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
        {
            boxVec = Vector3.right * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            boxVec = Vector3.left * speed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            boxVec = Vector3.forward * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            boxVec = Vector3.back * speed;
        }
        transform.Translate(boxVec * speed * Time.deltaTime);

        return boxVec;
    }
}