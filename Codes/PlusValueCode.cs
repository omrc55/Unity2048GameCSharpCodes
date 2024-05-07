using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusValueCode : MonoBehaviour
{
    public GameObject manager;
    public GameObject pointText;
    public bool plus;
    public bool showAd;


    void Start()
    {
        if (plus)
        {
            gameObject.transform.GetChild(0).transform.position = gameObject.transform.GetChild(1).transform.position;
        }
    }


    void Update()
    {
        if (plus)
        {
            //gameObject.transform.GetChild(0).transform.position = Vector3.Lerp(gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(2).transform.position, Time.deltaTime * 3);
            gameObject.transform.GetChild(0).transform.position = Vector3.MoveTowards(gameObject.transform.GetChild(0).transform.position, pointText.transform.position, Time.deltaTime * 3);

            //if ((gameObject.transform.GetChild(0).transform.position - gameObject.transform.GetChild(2).transform.position).magnitude < 0.1f)
            if ((gameObject.transform.GetChild(0).transform.position - pointText.transform.position).magnitude < 0.1f)
            {
                if (showAd)
                {
                    manager.GetComponent<Manager>().CallAds();
                }

                manager.GetComponent<Manager>().pointAnimPermission = true;
                Destroy(gameObject);
            }
        }
        else
        {
            //gameObject.transform.GetChild(0).transform.position = Vector3.Lerp(gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(1).transform.position, Time.deltaTime * 3);
            gameObject.transform.GetChild(0).transform.position = Vector3.MoveTowards(gameObject.transform.GetChild(0).transform.position, pointText.transform.position, Time.deltaTime * 3);

            //if ((gameObject.transform.GetChild(0).transform.position - gameObject.transform.GetChild(1).transform.position).magnitude < 0.1f)
            if ((gameObject.transform.GetChild(0).transform.position - pointText.transform.position).magnitude < 0.1f)
            {
                if (showAd)
                {
                    manager.GetComponent<Manager>().CallAds();
                }

                manager.GetComponent<Manager>().pointAnimPermission = true;
                Destroy(gameObject);
            }
        }
    }
}
