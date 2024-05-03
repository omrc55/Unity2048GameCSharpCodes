using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Yonetim : MonoBehaviour
{
    public Camera kamera;
    public GameObject baslik;
    public GameObject altSistem;
    public GameObject cekicPanel;
    public GameObject degisimPanel;
    public GameObject[] degisimRakamlar;
    public GameObject[] cizgiler;
    public GameObject[] oklar;
    public GameObject[] butonlar;
    public GameObject duraklamaDuvar;
    public GameObject duraklamaButon;

    public GameObject[] izgara;
    public GameObject karePref;
    public GameObject kareler;
    public GameObject artilar;
    public GameObject siradakiKare;
    public GameObject puanYazi;
    public GameObject yuksekPuanYazi;
    public GameObject duraklama;
    public GameObject artiDegerPref;
    public GameObject playTxt;
    public GameObject cikisBtn;
    public GameObject renkBtn;
    public GameObject puanlaPanel;
    public GameObject bilgiPanel;
    public GameObject cekicSorguPanel;
    public GameObject degistirSorguPanel;
    public GameObject seviyeBilgisi;

    public GameObject cekicBtn;
    public GameObject degisimBtn;

    public Reklamlar reklamlar;

    GameObject[] toplamKare;

    int degisimDeger;

    public int puan;
    int puanAnim;
    int secim;
    int siraSayi;

    public bool puanAnimIzin;

    public Temalar temalar;

    bool calis = true;

    bool siradakiKareHarekati;
    bool degerKareHareketi;

    bool artiBuyume;
    bool bilgiPanelAc;
    bool bilgiPanelAcildi;

    bool cekicSorguPanelAc;
    bool degistirSorguPanelAc;

    public bool islem;
    public int isleyenSayi;

    int temaSecimi;

    float finalSay;

    float yolSecimSayaci = 2.0f;
    float reklamSayaci = 60.0f;

    public int[] kareSayiAdedi = new int[25];
    public int seviye;


    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (PlayerPrefs.GetInt("Yarim") == 1)
        {
            playTxt.SetActive(false);
        }
        else
        {
            playTxt.SetActive(true);
        }

        Application.targetFrameRate = 60;

        if (!PlayerPrefs.HasKey("Cekic"))
        {
            CekicYukle(0, true, true);
        }
        else
        {
            CekicYukle(0, false, true);
        }

        if (!PlayerPrefs.HasKey("Degisim"))
        {
            DegisimYukle(0, true, true);
        }
        else
        {
            DegisimYukle(0, false, true);
        }
    }


    void Update()
    {
        if (calis)
        {
            if (temalar.temalar.Count > 0)
            {
                Baslangic();
                SiradakiKareAyari(false, true);

                if (PlayerPrefs.GetInt("Yarim") == 1)
                {
                    for (int a = 0; a < izgara.Length; a++)
                    {
                        if (PlayerPrefs.GetInt("Izgara" + a) == 99)
                        {
                            izgara[a].GetComponent<Izgara>().kare = null;
                        }
                        else
                        {
                            GameObject kare = Instantiate(karePref, izgara[a].transform.position, Quaternion.identity);

                            izgara[a].GetComponent<Izgara>().kare = kare;

                            kare.transform.SetParent(kareler.transform);
                            kare.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                            kare.GetComponent<Kare>().yonetim = gameObject;
                            kare.GetComponent<Kare>().puanYazi = puanYazi;
                            kare.GetComponent<Kare>().artiDegerPref = artiDegerPref;
                            kare.GetComponent<Kare>().artilar = artilar;
                            kare.GetComponent<Kare>().baslangicNoktasi = izgara[a];
                            kare.GetComponent<Kare>().temalar = temalar;
                            kare.GetComponent<Kare>().temaSecimi = temaSecimi;
                            kare.GetComponent<Kare>().secim = PlayerPrefs.GetInt("Izgara" + a);
                            kare.GetComponent<Kare>().siraSayi = PlayerPrefs.GetInt("IzgaraSira" + a);

                            kareSayiAdedi[PlayerPrefs.GetInt("Izgara" + a)]++;
                        }
                    }
                }

                calis = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cikis();
        }

        if (PlayerPrefs.GetInt("YuksekPuan") > 0)
        {
            yuksekPuanYazi.SetActive(true);
        }
        else
        {
            yuksekPuanYazi.SetActive(false);
        }

        yuksekPuanYazi.GetComponent<Text>().text = "Best Score: " + PlayerPrefs.GetInt("YuksekPuan").ToString("N0", CultureInfo.CreateSpecificCulture("ru-RU"));

        if (!islem)
        {
            Kayit();
        }

        if (puanAnim < puan && puanAnimIzin)
        {
            int arti = (puan - puanAnim) / 2;

            if (arti < 300)
            {
                arti = 1;
            }

            puanAnim = puanAnim + arti;

            puanYazi.GetComponent<Text>().text = puanAnim.ToString("N0", CultureInfo.CreateSpecificCulture("ru-RU"));
            //Debug.Log("açık");
        }
        else
        {
            puanAnimIzin = false;
        }

        toplamKare = GameObject.FindGameObjectsWithTag("Kare");

        if (toplamKare.Length < 30)
        {
            PlayerPrefs.SetInt("Yarim", 1);
            finalSay = 0.0f;
        }
        else
        {
            finalSay = finalSay + Time.deltaTime;
            if (finalSay > 1.0f)
            {
                PlayerPrefs.SetInt("Yarim", 0);
                Final();
            }
        }

        if (siradakiKareHarekati || degerKareHareketi)
        {
            siradakiKare.transform.localScale = Vector3.Lerp(siradakiKare.transform.localScale, new Vector3(0.01f, 0.01f, 0.01f), Time.deltaTime * 40);
            if (siradakiKare.transform.localScale.x < 0.05f)
            {
                if (degerKareHareketi)
                {
                    SiradakiKareAyari(true, false);
                }
                else
                {
                    SiradakiKareAyari(false, false);
                }

                siradakiKareHarekati = false;
                degerKareHareketi = false;
            }
        }
        else
        {
            siradakiKare.transform.localScale = Vector3.Lerp(siradakiKare.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 20);
        }

        if (yolSecimSayaci < 0.7f)
        {
            yolSecimSayaci = yolSecimSayaci + Time.deltaTime;
        }

        if (reklamSayaci < 60.0f)
        {
            reklamSayaci = reklamSayaci + Time.deltaTime;
        }

        if (duraklama.activeSelf)
        {
            if (duraklamaButon.transform.localScale.x > 0.99f)
            {
                artiBuyume = false;
            }
            else if (duraklamaButon.transform.localScale.x < 0.91f)
            {
                artiBuyume = true;
            }

            if (artiBuyume)
            {
                duraklamaButon.transform.localScale = Vector3.Lerp(duraklamaButon.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 3);
            }
            else
            {
                duraklamaButon.transform.localScale = Vector3.Lerp(duraklamaButon.transform.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime * 3);
            }
        }

        if (seviye == 0)
        {
            if (kareSayiAdedi[0] == 0)
            {
                if (puan > 30000)
                {
                    seviye = 1;
                }
            }
        }
        else if (seviye == 1)
        {
            if (kareSayiAdedi[0] == 0)
            {
                if (kareSayiAdedi[1] == 0)
                {
                    if (puan > 90000)
                    {
                        seviye = 2;
                    }
                }
            }
            else
            {
                seviye = 0;
            }
        }
        else if (seviye == 2)
        {
            if (kareSayiAdedi[1] == 0)
            {
                if (kareSayiAdedi[2] == 0)
                {
                    if (puan > 270000)
                    {
                        seviye = 3;
                    }
                }
            }
            else
            {
                seviye = 1;
            }
        }
        else if (seviye == 3)
        {
            if (kareSayiAdedi[2] == 0)
            {
                if (kareSayiAdedi[3] == 0)
                {
                    if (puan > 800000)
                    {
                        seviye = 4;
                    }
                }
            }
            else
            {
                seviye = 2;
            }
        }
        else if (seviye == 4)
        {
            if (kareSayiAdedi[3] != 0)
            {
                seviye = 3;
            }
        }

        seviyeBilgisi.GetComponent<Text>().text = "Level " + (seviye + 1);

        if (bilgiPanelAc)
        {
            bilgiPanel.transform.localScale = Vector2.MoveTowards(bilgiPanel.transform.localScale, new Vector2(1, 1), Time.deltaTime * 10);
        }
        else
        {
            bilgiPanel.transform.localScale = Vector2.MoveTowards(bilgiPanel.transform.localScale, new Vector2(1, 0), Time.deltaTime * 10);
        }

        if (cekicSorguPanelAc)
        {
            cekicSorguPanel.transform.localScale = Vector2.MoveTowards(cekicSorguPanel.transform.localScale, new Vector2(1, 1), Time.deltaTime * 10);
        }
        else
        {
            cekicSorguPanel.transform.localScale = Vector2.MoveTowards(cekicSorguPanel.transform.localScale, new Vector2(1, 0), Time.deltaTime * 10);
        }

        if (degistirSorguPanelAc)
        {
            degistirSorguPanel.transform.localScale = Vector2.MoveTowards(degistirSorguPanel.transform.localScale, new Vector2(1, 1), Time.deltaTime * 10);
        }
        else
        {
            degistirSorguPanel.transform.localScale = Vector2.MoveTowards(degistirSorguPanel.transform.localScale, new Vector2(1, 0), Time.deltaTime * 10);
        }
    }


    public void Yenileme()
    {
        CekicYukle(0, false, true);
        DegisimYukle(0, false, true);
        seviye = 0;

        for (int ks = 0; ks < kareSayiAdedi.Length; ks++)
        {
            kareSayiAdedi[ks] = 0;
        }

        toplamKare = GameObject.FindGameObjectsWithTag("Kare");

        if (toplamKare.Length > 0)
        {
            for (int k = 0; k < toplamKare.Length; k++)
            {
                Destroy(toplamKare[k]);
            }
        }

        calis = true;
    }


    public void Baslangic()
    {
        puan = PlayerPrefs.GetInt("Puan");
        if (puan > 99)
        {
            puanAnim = puan - 100;
        }
        else
        {
            puanAnim = 0;
        }

        puanYazi.GetComponent<Text>().text = puanAnim.ToString("N0", CultureInfo.CreateSpecificCulture("ru-RU"));

        izgara = GameObject.FindGameObjectsWithTag("Izgara");

        for (int a = 0; a < izgara.Length; a++)
        {
            izgara[a].GetComponent<Izgara>().kare = null;
        }

        siraSayi = PlayerPrefs.GetInt("SiraSayi");
        islem = false;

        RenkAyari(false);

        finalSay = 0;
        duraklama.SetActive(false);
        siradakiKare.GetComponent<Button>().enabled = false;
        siradakiKare.transform.GetChild(0).gameObject.SetActive(true);
        siradakiKare.transform.GetChild(1).gameObject.SetActive(false);
    }


    public void YolSecimi(GameObject baslangic)
    {
        //Debug.Log(/**/);
        if (yolSecimSayaci > 0.5f)
        {
            if (baslangic.GetComponent<Izgara>().kare == null)
            {
                GameObject kare = Instantiate(karePref, baslangic.transform.position, Quaternion.identity);

                kare.GetComponent<Kare>().mevcutAl = false;
                baslangic.GetComponent<Izgara>().kare = kare;

                kare.transform.SetParent(kareler.transform);
                kare.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                kare.GetComponent<Kare>().yonetim = gameObject;
                kare.GetComponent<Kare>().puanYazi = puanYazi;
                kare.GetComponent<Kare>().artiDegerPref = artiDegerPref;
                kare.GetComponent<Kare>().artilar = artilar;
                kare.GetComponent<Kare>().baslangicNoktasi = baslangic;
                kare.GetComponent<Kare>().temalar = temalar;
                kare.GetComponent<Kare>().temaSecimi = temaSecimi;
                kare.GetComponent<Kare>().secim = secim;
                kare.GetComponent<Kare>().siraSayi = siraSayi;

                kareSayiAdedi[secim]++;

                siraSayi++;

                siradakiKareHarekati = true;
            }
            else if (baslangic.GetComponent<Izgara>().kare.transform.GetChild(0).GetComponent<Text>().text == siradakiKare.transform.GetChild(0).GetComponent<Text>().text)
            {
                GameObject kare = Instantiate(karePref, baslangic.transform.position, Quaternion.identity);

                kare.GetComponent<Kare>().mevcutAl = true;

                kare.transform.SetParent(kareler.transform);
                kare.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                kare.GetComponent<Kare>().yonetim = gameObject;
                kare.GetComponent<Kare>().puanYazi = puanYazi;
                kare.GetComponent<Kare>().artiDegerPref = artiDegerPref;
                kare.GetComponent<Kare>().artilar = artilar;
                kare.GetComponent<Kare>().baslangicNoktasi = baslangic;
                kare.GetComponent<Kare>().temalar = temalar;
                kare.GetComponent<Kare>().temaSecimi = temaSecimi;
                kare.GetComponent<Kare>().secim = secim;
                kare.GetComponent<Kare>().siraSayi = siraSayi;

                kareSayiAdedi[secim]++;

                siraSayi++;

                siradakiKareHarekati = true;
            }

            yolSecimSayaci = 0;
        }

        playTxt.SetActive(false);
    }


    void SiradakiKareAyari(bool degerle, bool mevcut)
    {
        if (mevcut)
        {
            secim = PlayerPrefs.GetInt("SiradakiKare");
        }
        else
        {
            if (degerle)
            {
                secim = degisimDeger;
            }
            else
            {
                if (seviye == 4)
                {
                    secim = Random.Range(4, 10);
                }
                else if (seviye == 3)
                {
                    secim = Random.Range(3, 9);
                }
                else if (seviye == 2)
                {
                    secim = Random.Range(2, 8);
                }
                else if (seviye == 1)
                {
                    secim = Random.Range(1, 7);
                }
                else
                {
                    secim = Random.Range(0, 6);
                }
            }
        }

        siradakiKare.GetComponent<Image>().color = temalar.temalar[temaSecimi].kareler[secim].renk;
        siradakiKare.transform.GetChild(0).GetComponent<Text>().text = temalar.temalar[temaSecimi].kareler[secim].sayi;
    }


    void Kayit()
    {
        if (PlayerPrefs.GetInt("Yarim") == 1)
        {
            for (int a = 0; a < izgara.Length; a++)
            {
                if (izgara[a].GetComponent<Izgara>().kare == null)
                {
                    PlayerPrefs.SetInt("Izgara" + a, 99);
                }
                else
                {
                    PlayerPrefs.SetInt("Izgara" + a, izgara[a].GetComponent<Izgara>().kare.GetComponent<Kare>().secim);
                    PlayerPrefs.SetInt("IzgaraSira" + a, izgara[a].GetComponent<Izgara>().kare.GetComponent<Kare>().siraSayi);
                }
            }

            PlayerPrefs.SetInt("SiradakiKare", secim);
            PlayerPrefs.SetInt("SiraSayi", siraSayi);
            PlayerPrefs.SetInt("Puan", puan);
        }
        else
        {
            for (int a = 0; a < izgara.Length; a++)
            {
                PlayerPrefs.SetInt("Izgara" + a, 99);
            }

            if (PlayerPrefs.GetInt("YuksekPuan") < PlayerPrefs.GetInt("Puan"))
            {
                PlayerPrefs.SetInt("YuksekPuan", PlayerPrefs.GetInt("Puan"));
            }

            PlayerPrefs.SetInt("SiradakiKare", 0);
            PlayerPrefs.SetInt("SiraSayi", 0);
            PlayerPrefs.SetInt("Puan", 0);
        }
    }



    public void RenkAyari(bool renkSecimi)
    {
        if (renkSecimi)
        {
            if (PlayerPrefs.GetInt("TemaSecimi") < temalar.temalar.Count - 1)
            {
                PlayerPrefs.SetInt("TemaSecimi", temaSecimi + 1);
            }
            else
            {
                PlayerPrefs.SetInt("TemaSecimi", 0);
            }
        }

        temaSecimi = PlayerPrefs.GetInt("TemaSecimi");

        kamera.backgroundColor = temalar.temalar[temaSecimi].bgRenk;
        duraklamaDuvar.GetComponent<Image>().color = new Color32(temalar.temalar[temaSecimi].bgRenk.r, temalar.temalar[temaSecimi].bgRenk.g, temalar.temalar[temaSecimi].bgRenk.b, 150);
        altSistem.GetComponent<Image>().color = new Color32(temalar.temalar[temaSecimi].yaziRenk.r, temalar.temalar[temaSecimi].yaziRenk.g, temalar.temalar[temaSecimi].yaziRenk.b, 50);

        cekicPanel.transform.GetChild(0).GetComponent<Image>().color = new Color32(temalar.temalar[temaSecimi].yaziRenk.r, temalar.temalar[temaSecimi].yaziRenk.g, temalar.temalar[temaSecimi].yaziRenk.b, 50);
        cekicPanel.transform.GetChild(1).GetComponent<Text>().color = temalar.temalar[temaSecimi].yaziRenk;
        cekicPanel.transform.GetChild(2).GetComponent<Image>().color = temalar.temalar[temaSecimi].yaziRenk;
        //cekicPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().color = temalar.temalar[temaSecimi].bgRenk;

        degisimPanel.transform.GetChild(0).GetComponent<Image>().color = temalar.temalar[temaSecimi].bgRenk;
        degisimPanel.transform.GetChild(1).GetComponent<Image>().color = new Color32(temalar.temalar[temaSecimi].yaziRenk.r, temalar.temalar[temaSecimi].yaziRenk.g, temalar.temalar[temaSecimi].yaziRenk.b, 50);
        degisimPanel.transform.GetChild(2).GetComponent<Text>().color = temalar.temalar[temaSecimi].yaziRenk;
        degisimPanel.transform.GetChild(3).GetComponent<Image>().color = temalar.temalar[temaSecimi].yaziRenk;

        for (int d = 0; d < degisimRakamlar.Length; d++)
        {
            degisimRakamlar[d].GetComponent<Image>().color = temalar.temalar[temaSecimi].kareler[d].renk;
        }

        for (int c = 0; c < cizgiler.Length; c++)
        {
            cizgiler[c].GetComponent<Image>().color = new Color32(temalar.temalar[temaSecimi].yaziRenk.r, temalar.temalar[temaSecimi].yaziRenk.g, temalar.temalar[temaSecimi].yaziRenk.b, 50);
        }

        for (int o = 0; o < oklar.Length; o++)
        {
            oklar[o].GetComponent<Image>().color = new Color32(temalar.temalar[temaSecimi].yaziRenk.r, temalar.temalar[temaSecimi].yaziRenk.g, temalar.temalar[temaSecimi].yaziRenk.b, 50);
        }

        puanYazi.GetComponent<Text>().color = temalar.temalar[temaSecimi].yaziRenk;
        yuksekPuanYazi.GetComponent<Text>().color = temalar.temalar[temaSecimi].yaziRenk;
        seviyeBilgisi.GetComponent<Text>().color = temalar.temalar[temaSecimi].yaziRenk;

        siradakiKare.GetComponent<Image>().color = temalar.temalar[temaSecimi].kareler[secim].renk;
        siradakiKare.transform.GetChild(0).GetComponent<Text>().text = temalar.temalar[temaSecimi].kareler[secim].sayi;

        toplamKare = GameObject.FindGameObjectsWithTag("Kare");

        if (toplamKare.Length > 0)
        {
            for (int r = 0; r < toplamKare.Length; r++)
            {
                toplamKare[r].GetComponent<Image>().color = temalar.temalar[temaSecimi].kareler[toplamKare[r].GetComponent<Kare>().secim].renk;
            }
        }

        cikisBtn.GetComponent<Image>().color = temalar.temalar[temaSecimi].yaziRenk;
        renkBtn.GetComponent<Image>().color = temalar.temalar[temaSecimi].yaziRenk;

        cekicBtn.GetComponent<Image>().color = temalar.temalar[temaSecimi].yaziRenk;
        degisimBtn.GetComponent<Image>().color = temalar.temalar[temaSecimi].yaziRenk;

        duraklamaButon.GetComponent<Image>().color = temalar.temalar[temaSecimi].yaziRenk;
        duraklamaButon.transform.GetChild(0).GetComponent<Image>().color = temalar.temalar[temaSecimi].bgRenk;

        bilgiPanel.GetComponent<Image>().color = temalar.temalar[temaSecimi].bgRenk;
        bilgiPanel.transform.GetChild(0).GetComponent<Text>().color = temalar.temalar[temaSecimi].yaziRenk;
        bilgiPanel.transform.GetChild(1).GetComponent<Text>().color = new Color32(temalar.temalar[temaSecimi].yaziRenk.r, temalar.temalar[temaSecimi].yaziRenk.g, temalar.temalar[temaSecimi].yaziRenk.b, 150);
        bilgiPanel.transform.GetChild(2).GetComponent<Text>().color = new Color32(temalar.temalar[temaSecimi].yaziRenk.r, temalar.temalar[temaSecimi].yaziRenk.g, temalar.temalar[temaSecimi].yaziRenk.b, 100);
    }

    public void CekicSorguPanelDurum(bool ac)
    {
        if (ac)
        {
            cekicSorguPanelAc = true;
        }
        else
        {
            cekicSorguPanelAc = false;
        }
    }

    public void CekicSorgu()
    {
        cekicSorguPanelAc = false;

        if (reklamlar.cekicReklam.IsLoaded())
        {
            reklamlar.cekicReklam.Show();
        }
        reklamlar.CekicSorgu();
    }


    public void CekicYukle(int rakam, bool doldur, bool goster)
    {
        if (doldur)
        {
            PlayerPrefs.SetInt("Cekic", 5);
        }
        else
        {
            PlayerPrefs.SetInt("Cekic", PlayerPrefs.GetInt("Cekic") - rakam);

            if (PlayerPrefs.GetInt("Cekic") < 0)
            {
                PlayerPrefs.SetInt("Cekic", 0);
            }
        }

        cekicBtn.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("Cekic").ToString();

        if (cekicBtn.transform.GetChild(0).GetChild(0).GetComponent<Text>().text == "0")
        {
            cekicBtn.SetActive(false);
        }
        else
        {
            if (goster)
            {
                cekicBtn.SetActive(true);
            }
            else
            {
                cekicBtn.SetActive(false);
            }
        }
    }


    public void Cekic(bool kapat)
    {
        if (kapat)
        {
            cekicPanel.SetActive(false);

            for (int b = 0; b < butonlar.Length; b++)
            {
                butonlar[b].SetActive(true);
            }

            altSistem.SetActive(true);
            baslik.SetActive(true);
        }
        else
        {
            CekicYukle(1, false, true);
            for (int b = 0; b < butonlar.Length; b++)
            {
                butonlar[b].SetActive(false);
            }

            altSistem.SetActive(false);
            baslik.SetActive(false);
            cekicPanel.SetActive(true);
        }
    }

    public void DegisimSorguPanelDurum(bool ac)
    {
        if (ac)
        {
            degistirSorguPanelAc = true;
        }
        else
        {
            degistirSorguPanelAc = false;
        }
    }

    public void DegisimSorgu()
    {
        degistirSorguPanelAc = false;

        if (reklamlar.degisimReklam.IsLoaded())
        {
            reklamlar.degisimReklam.Show();
        }
        reklamlar.DegisimSorgu();
    }


    public void DegisimYukle(int rakam, bool doldur, bool goster)
    {
        if (doldur)
        {
            PlayerPrefs.SetInt("Degisim", 5);
        }
        else
        {
            PlayerPrefs.SetInt("Degisim", PlayerPrefs.GetInt("Degisim") - rakam);

            if (PlayerPrefs.GetInt("Degisim") < 0)
            {
                PlayerPrefs.SetInt("Degisim", 0);
            }
        }

        degisimBtn.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("Degisim").ToString();

        if (degisimBtn.transform.GetChild(0).GetChild(0).GetComponent<Text>().text == "0")
        {
            degisimBtn.SetActive(false);
        }
        else
        {
            if (goster)
            {
                degisimBtn.SetActive(true);
            }
            else
            {
                degisimBtn.SetActive(false);
            }
        }
    }


    public void SiradakiKareDegistir(int deger)
    {
        Degisim(true);
        degisimDeger = deger;
        degerKareHareketi = true;
        //SiradakiKareAyari(deger, true, false);
    }


    public void Degisim(bool kapat)
    {
        if (kapat)
        {
            degisimPanel.SetActive(false);
            altSistem.SetActive(true);
            baslik.SetActive(true);
        }
        else
        {
            DegisimYukle(1, false, true);

            altSistem.SetActive(false);
            baslik.SetActive(false);
            degisimPanel.SetActive(true);
        }
    }


    public void ReklamCagir()
    {
        if (reklamSayaci > 59.0f)
        {
            if (PlayerPrefs.GetInt("Ziyaret") != 1 && !bilgiPanelAcildi)
            {
                bilgiPanelAc = true;
            }
            else
            {
                if (reklamlar.gecis.IsLoaded())
                {
                    reklamlar.gecis.Show();
                }
                reklamlar.GecisSorgu();
            }

            reklamSayaci = 0;
        }
    }


    public void Bilgi(bool linkeGit)
    {
        if (linkeGit)
        {
            Application.OpenURL("market://details?id=com.reysadijital.mergeblock");
            PlayerPrefs.SetInt("Ziyaret", 1);
        }
        else
        {
            if (bilgiPanelAc)
            {
                bilgiPanelAc = false;
            }
            else
            {
                bilgiPanelAc = true;
            }

            bilgiPanelAcildi = true;
        }
    }


    public void Final()
    {
        CekicYukle(0, true, false);
        DegisimYukle(0, true, false);
        Kayit();
        duraklama.SetActive(true);
        //siradakiKare.GetComponent<Button>().enabled = true;
        //siradakiKare.transform.GetChild(0).gameObject.SetActive(false);
        //siradakiKare.transform.GetChild(1).gameObject.SetActive(true);
    }


    public void Cikis()
    {
        Application.Quit();
    }
}
