using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDelete : MonoBehaviour
{
    Manager manager;
    float counter;


    void Start()
    {
        manager = GetComponent<Square>().manager.GetComponent<Manager>();
    }


    void Update()
    {
        counter = counter + Time.deltaTime;

        if (counter > 2)
        {
            manager.squareQuantity[GetComponent<Square>().select]--;
            Destroy(gameObject);
        }
    }
}
