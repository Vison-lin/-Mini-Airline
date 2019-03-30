using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject river = GameObject.FindGameObjectWithTag("River");
        float x = river.transform.localPosition.x;
        float y = river.transform.localPosition.y;
        Debug.Log(x + "," + y);
        //Vector3 pos = new Vector3(500, 500, 0);
        //Quaternion q = new Quaternion(1,1,1,1);
        //Instantiate(prefab, pos, q);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
