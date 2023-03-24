using System.Collections.Generic;
using UnityEngine;

public class PathOpponent : MonoBehaviour
{
    [SerializeField] Transform targetList;
    [SerializeField] int targetCount = 0;
    [SerializeField] List<Transform> targets = new List<Transform>();
    private void Start()
    {
        foreach (Transform target in targetList)
        {
            targets.Add(target);
        }
    }
    private void Update()
    {
        transform.LookAt(targets[targetCount].position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "target")
        {
            targetCount++;
        }
    }
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Gizmos.color = new Color(.2f, .8f, .2f, .8f);
        foreach (Transform target in targetList)
        {
            Gizmos.DrawSphere(target.position, 0.6f);
            //Gizmos.DrawRay(new Ray(origin, target.position));
            Gizmos.DrawLine(origin, target.position);
            origin = target.position;
        }
    }
}
