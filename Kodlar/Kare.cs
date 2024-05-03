using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kare : MonoBehaviour
{
    public Temalar temalar;

    public GameObject yonetim;
    public GameObject puanYazi;
    public GameObject artilar;
    public GameObject baslangicNoktasi;

    public GameObject objectMevcut;
    public GameObject objectUst;
    public GameObject objectSag;
    public GameObject objectSol;
    public GameObject artiDegerPref;
    
    public int deger;
    public int secim;
    public int siraSayi;

    public int temaSecimi;

    public bool usttenAl;
    public bool sagdanAl;
    public bool soldanAl;
    public bool mevcutAl;

    bool ustHareket;
    bool sagHareket;
    bool solHareket;
    bool mevcutHareket;

    bool buyume;

    bool artiGoster;
    string artiDegerYazi;
    bool artiTip;

    bool izin;

    float say;

    public bool reklamGoster;


    void Start()
    {
        objectMevcut = baslangicNoktasi;
        KareAyari();
        izin = true;
    }


    void Update()
    {
        objectUst = null;
        objectSag = null;
        objectSol = null;

        for (int a = 0; a < yonetim.GetComponent<Yonetim>().izgara.Length; a++)
        {
            if (char.GetNumericValue(yonetim.GetComponent<Yonetim>().izgara[a].name[0]) == char.GetNumericValue(objectMevcut.name[0]) - 1 && yonetim.GetComponent<Yonetim>().izgara[a].name[1] == objectMevcut.name[1])
            {
                objectUst = yonetim.GetComponent<Yonetim>().izgara[a];
            }

            if (yonetim.GetComponent<Yonetim>().izgara[a].name[0] == objectMevcut.name[0] && char.GetNumericValue(yonetim.GetComponent<Yonetim>().izgara[a].name[1]) == char.GetNumericValue(objectMevcut.name[1]) - 1)
            {
                objectSol = yonetim.GetComponent<Yonetim>().izgara[a];
            }

            if (yonetim.GetComponent<Yonetim>().izgara[a].name[0] == objectMevcut.name[0] && char.GetNumericValue(yonetim.GetComponent<Yonetim>().izgara[a].name[1]) == char.GetNumericValue(objectMevcut.name[1]) + 1)
            {
                objectSag = yonetim.GetComponent<Yonetim>().izgara[a];
            }
        }

        if (objectUst != null && objectUst.GetComponent<Izgara>().kare == null)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, objectUst.transform.position, Time.deltaTime * 10);
            //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, objectUst.transform.position, Time.deltaTime);

            if((gameObject.transform.position - objectUst.transform.position).magnitude < 0.1f)
            {
                gameObject.transform.position = objectUst.transform.position;
                objectMevcut.GetComponent<Izgara>().kare = null;
                objectMevcut = objectUst;
                objectMevcut.GetComponent<Izgara>().kare = gameObject;
            }
        }
        else
        {
            Komsular();
        }

        if (ustHareket && (!yonetim.GetComponent<Yonetim>().islem || yonetim.GetComponent<Yonetim>().isleyenSayi == siraSayi))
        {
            yonetim.GetComponent<Yonetim>().islem = true;
            yonetim.GetComponent<Yonetim>().isleyenSayi = siraSayi;
            say = say + Time.deltaTime;
            objectUst.GetComponent<Izgara>().kare.GetComponent<Kare>().enabled = false;
            objectUst.GetComponent<Izgara>().kare.GetComponent<ObjectSil>().enabled = true;

            objectUst.GetComponent<Izgara>().kare.transform.position = Vector3.MoveTowards(objectUst.GetComponent<Izgara>().kare.transform.position, gameObject.transform.position, Time.deltaTime * 15);
            //objectUst.GetComponent<Izgara>().kare.transform.position = Vector3.MoveTowards(objectUst.GetComponent<Izgara>().kare.transform.position, gameObject.transform.position, Time.deltaTime);

            if (transform.localScale.x > 1.9f)
            {
                buyume = false;
            }

            if (!buyume && transform.localScale.x < 1.8f)
            {
                say = 0;
                artiGoster = true;
                artiTip = true;

                yonetim.GetComponent<Yonetim>().kareSayiAdedi[objectUst.GetComponent<Izgara>().kare.GetComponent<Kare>().secim]--;

                Destroy(objectUst.GetComponent<Izgara>().kare);
                ustHareket = false;
                izin = true;
                yonetim.GetComponent<Yonetim>().islem = false;
                yonetim.GetComponent<Yonetim>().isleyenSayi = 0;
            }
        }

        if (sagHareket && (!yonetim.GetComponent<Yonetim>().islem || yonetim.GetComponent<Yonetim>().isleyenSayi == siraSayi))
        {
            yonetim.GetComponent<Yonetim>().islem = true;
            yonetim.GetComponent<Yonetim>().isleyenSayi = siraSayi;
            say = say + Time.deltaTime;
            objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().enabled = false;
            objectSag.GetComponent<Izgara>().kare.GetComponent<ObjectSil>().enabled = true;
            objectSag.GetComponent<Izgara>().kare.transform.position = Vector3.MoveTowards(objectSag.GetComponent<Izgara>().kare.transform.position, gameObject.transform.position, Time.deltaTime * 15);
            //objectSag.GetComponent<Izgara>().kare.transform.position = Vector3.MoveTowards(objectSag.GetComponent<Izgara>().kare.transform.position, gameObject.transform.position, Time.deltaTime);

            if (transform.localScale.x > 1.9f)
            {
                buyume = false;
            }

            if (!buyume && transform.localScale.x < 1.8f)
            {
                say = 0;
                artiGoster = true;
                artiTip = false;

                yonetim.GetComponent<Yonetim>().kareSayiAdedi[objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().secim]--;

                Destroy(objectSag.GetComponent<Izgara>().kare);
                sagHareket = false;
                izin = true;
                yonetim.GetComponent<Yonetim>().islem = false;
                yonetim.GetComponent<Yonetim>().isleyenSayi = 0;
            }
        }

        if (solHareket && (!yonetim.GetComponent<Yonetim>().islem || yonetim.GetComponent<Yonetim>().isleyenSayi == siraSayi))
        {
            yonetim.GetComponent<Yonetim>().islem = true;
            yonetim.GetComponent<Yonetim>().isleyenSayi = siraSayi;
            say = say + Time.deltaTime;
            objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().enabled = false;
            objectSol.GetComponent<Izgara>().kare.GetComponent<ObjectSil>().enabled = true;
            objectSol.GetComponent<Izgara>().kare.transform.position = Vector3.MoveTowards(objectSol.GetComponent<Izgara>().kare.transform.position, gameObject.transform.position, Time.deltaTime * 15);
            //objectSol.GetComponent<Izgara>().kare.transform.position = Vector3.MoveTowards(objectSol.GetComponent<Izgara>().kare.transform.position, gameObject.transform.position, Time.deltaTime);

            if (transform.localScale.x > 1.9f)
            {
                buyume = false;
            }

            if (!buyume && transform.localScale.x < 1.8f)
            {
                say = 0;
                artiGoster = true;
                artiTip = false;

                yonetim.GetComponent<Yonetim>().kareSayiAdedi[objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().secim]--;

                Destroy(objectSol.GetComponent<Izgara>().kare);
                solHareket = false;
                izin = true;
                yonetim.GetComponent<Yonetim>().islem = false;
                yonetim.GetComponent<Yonetim>().isleyenSayi = 0;
            }
        }

        if (mevcutHareket && (!yonetim.GetComponent<Yonetim>().islem || yonetim.GetComponent<Yonetim>().isleyenSayi == siraSayi))
        {
            mevcutAl = false;
            yonetim.GetComponent<Yonetim>().islem = true;
            yonetim.GetComponent<Yonetim>().isleyenSayi = siraSayi;
            say = say + Time.deltaTime;
            objectMevcut.GetComponent<Izgara>().kare.GetComponent<Kare>().enabled = false;
            objectMevcut.GetComponent<Izgara>().kare.GetComponent<ObjectSil>().enabled = true;
            //objectMevcut.GetComponent<Izgara>().kare.transform.position = Vector3.MoveTowards(objectMevcut.GetComponent<Izgara>().kare.transform.position, gameObject.transform.position, Time.deltaTime * 15);
            //objectSol.GetComponent<Izgara>().kare.transform.position = Vector3.MoveTowards(objectSol.GetComponent<Izgara>().kare.transform.position, gameObject.transform.position, Time.deltaTime);

            if (transform.localScale.x > 1.9f)
            {
                buyume = false;
            }

            if (!buyume && transform.localScale.x < 1.1f)
            {
                say = 0;
                artiGoster = true;
                artiTip = false;

                yonetim.GetComponent<Yonetim>().kareSayiAdedi[objectMevcut.GetComponent<Izgara>().kare.GetComponent<Kare>().secim]--;

                Destroy(objectMevcut.GetComponent<Izgara>().kare);
                objectMevcut.GetComponent<Izgara>().kare = gameObject;
                mevcutHareket = false;
                izin = true;
                yonetim.GetComponent<Yonetim>().islem = false;
                yonetim.GetComponent<Yonetim>().isleyenSayi = 0;
            }
        }

        if (buyume)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), Time.deltaTime * 15);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 15);
        }

        if (artiGoster)
        {
            GameObject artiDeger = Instantiate(artiDegerPref, gameObject.transform.position, Quaternion.identity);
            artiDeger.transform.GetChild(0).GetComponent<Text>().text = artiDegerYazi;
            artiDeger.transform.SetParent(artilar.transform);
            artiDeger.transform.localScale = new Vector3(1, 1, 1);
            artiDeger.GetComponent<ArtiDegerKodu>().yonetim = yonetim;
            artiDeger.GetComponent<ArtiDegerKodu>().puanYazi = puanYazi;
            artiDeger.GetComponent<ArtiDegerKodu>().artiTip = artiTip;
            artiDeger.GetComponent<ArtiDegerKodu>().reklamGoster = reklamGoster;
            artiGoster = false;
            reklamGoster = false;
        }
    }


    void Komsular()
    {
        usttenAl = false;
        sagdanAl = false;
        soldanAl = false;

        if (objectUst != null && objectUst.GetComponent<Izgara>().kare != null)
        {
            if (objectUst.GetComponent<Izgara>().kare.GetComponent<Kare>().secim == secim)
            {
                usttenAl = true;
            }
        }
        
        if(objectSag != null && objectSag.GetComponent<Izgara>().kare != null)
        {
            if (objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().secim == secim && siraSayi > objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().siraSayi && (objectSag.GetComponent<Izgara>().kare.transform.position - objectSag.transform.position).magnitude < 0.1f)
            {
                sagdanAl = true;
            }
        }

        if (objectSol != null && objectSol.GetComponent<Izgara>().kare != null)
        {
            if (objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().secim == secim && siraSayi > objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().siraSayi && (objectSol.GetComponent<Izgara>().kare.transform.position - objectSol.transform.position).magnitude < 0.1f)
            {
                soldanAl = true;
            }
        }

        if (izin)
        {
            yonetim.GetComponent<Yonetim>().kareSayiAdedi[secim]--;

            if (usttenAl && sagdanAl && soldanAl)
            {
                yonetim.GetComponent<Yonetim>().puan = yonetim.GetComponent<Yonetim>().puan + objectUst.GetComponent<Izgara>().kare.GetComponent<Kare>().deger + objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().deger + objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().deger;

                ustHareket = true;
                sagHareket = true;
                solHareket = true;
                buyume = true;
                izin = false;

                secim = secim + 3;
                artiDegerYazi = "+ " + (objectUst.GetComponent<Izgara>().kare.GetComponent<Kare>().deger + objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().deger + objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().deger);
                KareAyari();
            }
            else if (usttenAl && sagdanAl)
            {
                yonetim.GetComponent<Yonetim>().puan = yonetim.GetComponent<Yonetim>().puan + objectUst.GetComponent<Izgara>().kare.GetComponent<Kare>().deger + objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().deger;

                ustHareket = true;
                sagHareket = true;
                buyume = true;
                izin = false;

                secim = secim + 2;
                artiDegerYazi = "+ " + (objectUst.GetComponent<Izgara>().kare.GetComponent<Kare>().deger + objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().deger);
                KareAyari();
            }
            else if (usttenAl && soldanAl)
            {
                yonetim.GetComponent<Yonetim>().puan = yonetim.GetComponent<Yonetim>().puan + objectUst.GetComponent<Izgara>().kare.GetComponent<Kare>().deger + objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().deger;

                ustHareket = true;
                solHareket = true;
                buyume = true;
                izin = false;

                secim = secim + 2;
                artiDegerYazi = "+ " + (objectUst.GetComponent<Izgara>().kare.GetComponent<Kare>().deger + objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().deger);
                KareAyari();
            }
            else if (sagdanAl && soldanAl)
            {
                yonetim.GetComponent<Yonetim>().puan = yonetim.GetComponent<Yonetim>().puan + objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().deger + objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().deger;

                sagHareket = true;
                solHareket = true;
                buyume = true;
                izin = false;

                secim = secim + 2;
                artiDegerYazi = "+ " + (objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().deger + objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().deger);
                KareAyari();
            }
            else if (usttenAl)
            {
                yonetim.GetComponent<Yonetim>().puan = yonetim.GetComponent<Yonetim>().puan + objectUst.GetComponent<Izgara>().kare.GetComponent<Kare>().deger;

                ustHareket = true;
                buyume = true;
                izin = false;

                secim++;
                artiDegerYazi = "+ " + (objectUst.GetComponent<Izgara>().kare.GetComponent<Kare>().deger);
                KareAyari();
            }
            else if (sagdanAl)
            {
                yonetim.GetComponent<Yonetim>().puan = yonetim.GetComponent<Yonetim>().puan + objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().deger;

                sagHareket = true;
                buyume = true;
                izin = false;

                secim++;
                artiDegerYazi = "+ " + (objectSag.GetComponent<Izgara>().kare.GetComponent<Kare>().deger);
                KareAyari();
            }
            else if (soldanAl)
            {
                yonetim.GetComponent<Yonetim>().puan = yonetim.GetComponent<Yonetim>().puan + objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().deger;

                solHareket = true;
                buyume = true;
                izin = false;

                secim++;
                artiDegerYazi = "+ " + (objectSol.GetComponent<Izgara>().kare.GetComponent<Kare>().deger);
                KareAyari();
            }
            else if (mevcutAl)
            {
                yonetim.GetComponent<Yonetim>().puan = yonetim.GetComponent<Yonetim>().puan + objectMevcut.GetComponent<Izgara>().kare.GetComponent<Kare>().deger;

                mevcutHareket = true;
                buyume = true;
                izin = false;

                secim++;
                artiDegerYazi = "+ " + (objectMevcut.GetComponent<Izgara>().kare.GetComponent<Kare>().deger);
                KareAyari();
            }

            yonetim.GetComponent<Yonetim>().kareSayiAdedi[secim]++;

            if (yonetim.GetComponent<Yonetim>().seviye == 4)
            {
                if (secim > 10)
                {
                    reklamGoster = true;
                }
            }
            else if (yonetim.GetComponent<Yonetim>().seviye == 3)
            {
                if (secim > 9)
                {
                    reklamGoster = true;
                }
            }
            else
            {
                if (secim > 8)
                {
                    reklamGoster = true;
                }
            }
        }
    }


    void KareAyari()
    {
        gameObject.GetComponent<Image>().color = temalar.temalar[temaSecimi].kareler[secim].renk;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = temalar.temalar[temaSecimi].kareler[secim].sayi;
        deger = temalar.temalar[temaSecimi].kareler[secim].deger;
    }
}
