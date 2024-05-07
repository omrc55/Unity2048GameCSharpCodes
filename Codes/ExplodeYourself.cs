using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeYourself : MonoBehaviour
{
    Manager manager;
    bool delete;


    void Start()
    {
        manager = GetComponent<Square>().manager.GetComponent<Manager>();
    }


    void Update()
    {
        if (delete)
        {
            transform.GetComponent<Square>().enabled = false;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(3, 3, 3), Time.deltaTime * 5);
        }

        if (transform.localScale.x > 2.5f)
        {
            Destroy(gameObject);
        }
    }


    public void DeleteSquare()
    {
        manager.Hammer(true);
        delete = true;
    }
}
