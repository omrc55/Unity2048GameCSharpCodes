using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KendiniPatlat : MonoBehaviour
{
    Yonetim yonetim;
    bool silKomutu;


    void Start()
    {
        yonetim = GetComponent<Kare>().yonetim.GetComponent<Yonetim>();
    }


    void Update()
    {
        if (silKomutu)
        {
            transform.GetComponent<Kare>().enabled = false;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(3, 3, 3), Time.deltaTime * 5);
        }

        if (transform.localScale.x > 2.5f)
        {
            Destroy(gameObject);
        }
    }


    public void KareyiSil()
    {
        yonetim.Cekic(true);
        silKomutu = true;
    }
}
