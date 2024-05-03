using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSil : MonoBehaviour
{
    Yonetim yonetim;
    float say;


    void Start()
    {
        yonetim = GetComponent<Kare>().yonetim.GetComponent<Yonetim>();
    }


    void Update()
    {
        say = say + Time.deltaTime;

        if (say > 2)
        {
            yonetim.kareSayiAdedi[GetComponent<Kare>().secim]--;
            Destroy(gameObject);
        }
    }
}
