using UnityEngine;

[System.Serializable]
public class KareOlustur
{
    public string sayi;
    public int deger;
    public Color32 renk;

    public KareOlustur(string yeniSayi, int yeniDeger, Color32 yeniRenk)
    {
        sayi = yeniSayi;
        deger = yeniDeger;
        renk = yeniRenk;
    }
}
