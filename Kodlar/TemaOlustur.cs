using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TemaOlustur
{
    public Color32 bgRenk;
    public Color32 bgRenkAcik;
    public Color32 yaziRenk;
    public List<KareOlustur> kareler;


    public TemaOlustur(Color32 yeniBgRenk, Color32 yeniBgRenkAcik, Color32 yeniYaziRenk, List<KareOlustur> yeniKareler)
    {
        bgRenk = yeniBgRenk;
        bgRenkAcik = yeniBgRenkAcik;
        yaziRenk = yeniYaziRenk;
        kareler = yeniKareler;
    }
}
