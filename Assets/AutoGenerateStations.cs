using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGenerateStations : MonoBehaviour
{
    int maxNumOfStations = 5  ;
    int coorZ = 770;
    int minT = 1000;// min time interval between the two auto generation
    int maxT = 2000;// max time interval between the two auto generation
    int minDistance = 100;
    int viewWidth = 500;
    int viewHeight = 500;
    public GameObject sample;
    System.Random random;
    ArrayList generatedStations;

    // Start is called before the first frame update
    void Start()
    {
        // print generator configurations
        Debug.Log("Start to auto generate stations.");
        Debug.Log("The number of max allowable auto generated stations is: " + maxNumOfStations);
        Debug.Log("The default z coordinate for ALL the stations is: " + coorZ);
        Debug.Log("The min time interval between generation is (in millisecond): " + minT);
        Debug.Log("The max time interval between generation is (in millisecond): " + maxT);
        Debug.Log("The min distance between two stations is: " + minDistance);
        Debug.Log("The width of view is : " + viewWidth);
        Debug.Log("The height of view is : " + viewHeight);

        generatedStations = new ArrayList();

        random = new System.Random();

        //start to auto generate
        for (int i = 0; i < maxNumOfStations; i++)
        {
            float rx = 0;
            float ry = 0;
            bool InvalidCoord = true;
            while (InvalidCoord)
            {
                rx = random.Next(-viewWidth/2, viewWidth/2);
                ry = random.Next(-viewHeight / 2, viewHeight / 2);
    
                //Debug.Log(isNotCloseToBarriers(rx, ry));
                if (isNotCloseToBarriers(rx, ry))
                {
                    bool allValid = true;
                    foreach (Vector3 currs in generatedStations)
                    {
                        //Debug.Log("The rx is: " + rx + " while the currs.position is: " + currs.transform.position.x);
                        //Debug.Log("The ry is: " + ry + " while the currs.position is: " + currs.transform.position.y);
                        bool isValid = Math.Abs((rx - currs.x)) >= minDistance && Math.Abs((ry - currs.y)) >= minDistance;
                        if (!isValid)
                        {
                            allValid = false;
                        }
                    }
                    if (allValid)
                    {
                        InvalidCoord = false;
                    }
                }
            }

            Debug.Log("The newly generated coordinate is: " + rx + ", " + ry);

            Vector3 pos = new Vector3(rx, ry, coorZ);
            Quaternion q = new Quaternion(100, 100, 100, 100);
            GameObject go = Instantiate(sample, pos, q);
            generatedStations.Add(pos);
        }
        
    }

    private bool isNotCloseToBarriers(float x, float y)
    {
        Vector3 pos = new Vector3(0,0);
        Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            {
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Barrier")
                {
                return false;
                }
          }
        //Debug.Log("4");
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
