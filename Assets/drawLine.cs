using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLine : MonoBehaviour
{

    // Start is called before the first frame update
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    public Transform origin;
    public Transform destination;
    public float lineDrawSpeed = 6f;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetWidth(.45f, .45f);
        dist = Vector3.Distance(origin.position, destination.position);


    }

    // Update is called once per frame
    void Update()
    {
        if(counter < dist)
        {
            counter += .1f / lineDrawSpeed;
            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;

            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
            lineRenderer.SetPosition(1, pointAlongLine);
        }
        
    }
}
