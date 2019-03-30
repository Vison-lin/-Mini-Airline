using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddStation : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject river = GameObject.FindGameObjectWithTag("River");
        //float x = river.transform.localPosition.x;
        //float y = river.transform.localPosition.y;
        //Debug.Log(x + "," + y);

        for (int i = 0; i < 3; i++)
        {
        //Sample code for generating station
        Vector3 pos = new Vector3(0, i * 100, 770);
        Quaternion q = new Quaternion(100,100,100,100);
        Instantiate(prefab, pos, q);
        }
        
        //samle code for getting barriers
        GameObject[] barriers = GameObject.FindGameObjectsWithTag("Station");
        Debug.Log("There are " + barriers.Length + " of barriers");
        foreach (GameObject barrier in barriers)
        {
            float x = barrier.transform.localPosition.x;
            float y = barrier.transform.localPosition.y;
            Debug.Log(x + "," + y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log("Im update");
        addOnClickListenerForStation();
    }

    //Method to detect the click over the station
    void addOnClickListenerForStation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.position.y);
                if (hit.transform.tag == "Station")
                {
                    Debug.Log(hit.transform.position.y);
                }
            }
        }
    }

}
