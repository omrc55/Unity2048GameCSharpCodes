using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtiDegerKodu : MonoBehaviour
{
    public GameObject yonetim;
    public GameObject puanYazi;
    public bool artiTip;
    public bool reklamGoster;


    void Start()
    {
        if (artiTip)
        {
            gameObject.transform.GetChild(0).transform.position = gameObject.transform.GetChild(1).transform.position;
        }
    }


    void Update()
    {
        if (artiTip)
        {
            //gameObject.transform.GetChild(0).transform.position = Vector3.Lerp(gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(2).transform.position, Time.deltaTime * 3);
            gameObject.transform.GetChild(0).transform.position = Vector3.MoveTowards(gameObject.transform.GetChild(0).transform.position, puanYazi.transform.position, Time.deltaTime * 3);

            //if ((gameObject.transform.GetChild(0).transform.position - gameObject.transform.GetChild(2).transform.position).magnitude < 0.1f)
            if ((gameObject.transform.GetChild(0).transform.position - puanYazi.transform.position).magnitude < 0.1f)
            {
                if (reklamGoster)
                {
                    yonetim.GetComponent<Yonetim>().ReklamCagir();
                }

                yonetim.GetComponent<Yonetim>().puanAnimIzin = true;
                Destroy(gameObject);
            }
        }
        else
        {
            //gameObject.transform.GetChild(0).transform.position = Vector3.Lerp(gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(1).transform.position, Time.deltaTime * 3);
            gameObject.transform.GetChild(0).transform.position = Vector3.MoveTowards(gameObject.transform.GetChild(0).transform.position, puanYazi.transform.position, Time.deltaTime * 3);

            //if ((gameObject.transform.GetChild(0).transform.position - gameObject.transform.GetChild(1).transform.position).magnitude < 0.1f)
            if ((gameObject.transform.GetChild(0).transform.position - puanYazi.transform.position).magnitude < 0.1f)
            {
                if (reklamGoster)
                {
                    yonetim.GetComponent<Yonetim>().ReklamCagir();
                }

                yonetim.GetComponent<Yonetim>().puanAnimIzin = true;
                Destroy(gameObject);
            }
        }
    }
}
