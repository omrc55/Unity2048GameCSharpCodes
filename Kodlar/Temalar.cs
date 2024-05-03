using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temalar : MonoBehaviour
{
    public List<TemaOlustur> temalar = new List<TemaOlustur>();

    void Start()
    {
        temalar.Add(
            new TemaOlustur(
                new Color32(50, 60, 70, 255),
                new Color32(230, 240, 250, 255),
                new Color32(255, 255, 255, 255),
                new List<KareOlustur> {
                    new KareOlustur("2", 2, new Color32(200,200,200,255)),
                    new KareOlustur("4", 4, new Color32(255,130,130,255)),
                    new KareOlustur("8", 8, new Color32(220,240,100,255)),
                    new KareOlustur("16", 16, new Color32(255,200,100,255)),
                    new KareOlustur("32", 32, new Color32(150,200,255,255)),
                    new KareOlustur("64", 64, new Color32(150,150,150,255)),
                    new KareOlustur("128", 128, new Color32(200,80,80,255)),
                    new KareOlustur("256", 256, new Color32(170,190,50,255)),
                    new KareOlustur("512", 512, new Color32(200,150,50,255)),
                    new KareOlustur("1024", 1024, new Color32(100,150,200,255)),
                    new KareOlustur("2048", 2048, new Color32(100,100,100,255)),
                    new KareOlustur("4096", 4096, new Color32(150,30,30,255)),
                    new KareOlustur("8192", 8192, new Color32(120,140,0,255)),
                    new KareOlustur("16K", 16384, new Color32(150,100,0,255)),
                    new KareOlustur("32K", 32768, new Color32(50,100,150,255)),
                    new KareOlustur("65K", 65536, new Color32(50,50,50,255)),
                    new KareOlustur("131K", 131072, new Color32(100,0,0,255)),
                    new KareOlustur("262K", 262144, new Color32(70,90,0,255)),
                    new KareOlustur("524K", 524288, new Color32(100,50,0,255)),
                    new KareOlustur("1048K", 1048576, new Color32(0,50,100,255)),
                    new KareOlustur("2097K", 2097152, new Color32(0,0,0,255)),
                    new KareOlustur("4194K", 4194304, new Color32(50,0,0,255)),
                    new KareOlustur("8388K", 8388608, new Color32(20,40,0,255)),
                    new KareOlustur("16M", 16777216, new Color32(25,0,0,255)),
                    new KareOlustur("33M", 33554432, new Color32(0,0,50,255))
                })
            );

        temalar.Add(
            new TemaOlustur(
                new Color32(70, 50, 60, 255),
                new Color32(250, 230, 240, 255),
                new Color32(255, 240, 250, 255),
                new List<KareOlustur> {
                    new KareOlustur("2", 2, new Color32(75,200,255,255)),
                    new KareOlustur("4", 4, new Color32(255,150,190,255)),
                    new KareOlustur("8", 8, new Color32(150,255,150,255)),
                    new KareOlustur("16", 16, new Color32(190,150,250,255)),
                    new KareOlustur("32", 32, new Color32(255,255,100,255)),
                    new KareOlustur("64", 64, new Color32(30,150,200,255)),
                    new KareOlustur("128", 128, new Color32(200,100,140,255)),
                    new KareOlustur("256", 256, new Color32(100,200,100,255)),
                    new KareOlustur("512", 512, new Color32(140,140,200,255)),
                    new KareOlustur("1024", 1024, new Color32(200,200,50,255)),
                    new KareOlustur("2048", 2048, new Color32(0,100,150,255)),
                    new KareOlustur("4096", 4096, new Color32(150,50,90,255)),
                    new KareOlustur("8192", 8192, new Color32(50,150,50,255)),
                    new KareOlustur("16K", 16384, new Color32(90,90,150,255)),
                    new KareOlustur("32K", 32768, new Color32(150,150,0,255)),
                    new KareOlustur("65K", 65536, new Color32(0,50,100,255)),
                    new KareOlustur("131K", 131072, new Color32(100,0,40,255)),
                    new KareOlustur("262K", 262144, new Color32(0,100,0,255)),
                    new KareOlustur("524K", 524288, new Color32(40,40,100,255)),
                    new KareOlustur("1048K", 1048576, new Color32(100,100,0,255)),
                    new KareOlustur("2097K", 2097152, new Color32(0,0,50,255)),
                    new KareOlustur("4194K", 4194304, new Color32(50,0,0,255)),
                    new KareOlustur("8388K", 8388608, new Color32(0,50,0,255)),
                    new KareOlustur("16M", 16777216, new Color32(0,0,50,255)),
                    new KareOlustur("33M", 33554432, new Color32(50,50,0,255))
                })
            );

        temalar.Add(
            new TemaOlustur(
                new Color32(250, 220, 140, 255),
                new Color32(250, 220, 140, 255),
                new Color32(200, 115, 60, 255),
                new List<KareOlustur> {
                    new KareOlustur("2", 2, new Color32(240,160,80,255)),
                    new KareOlustur("4", 4, new Color32(120,150,210,255)),
                    new KareOlustur("8", 8, new Color32(150,200,170,255)),
                    new KareOlustur("16", 16, new Color32(180,150,180,255)),
                    new KareOlustur("32", 32, new Color32(120,150,160,255)),
                    new KareOlustur("64", 64, new Color32(190,110,30,255)),
                    new KareOlustur("128", 128, new Color32(70,100,160,255)),
                    new KareOlustur("256", 256, new Color32(100,150,120,255)),
                    new KareOlustur("512", 512, new Color32(130,100,130,255)),
                    new KareOlustur("1024", 1024, new Color32(70,100,110,255)),
                    new KareOlustur("2048", 2048, new Color32(140,60,0,255)),
                    new KareOlustur("4096", 4096, new Color32(20,50,110,255)),
                    new KareOlustur("8192", 8192, new Color32(50,100,70,255)),
                    new KareOlustur("16K", 16384, new Color32(80,50,80,255)),
                    new KareOlustur("32K", 32768, new Color32(20,50,60,255)),
                    new KareOlustur("65K", 65536, new Color32(90,10,0,255)),
                    new KareOlustur("131K", 131072, new Color32(0,0,60,255)),
                    new KareOlustur("262K", 262144, new Color32(0,50,20,255)),
                    new KareOlustur("524K", 524288, new Color32(30,0,30,255)),
                    new KareOlustur("1048K", 1048576, new Color32(0,0,40,255)),
                    new KareOlustur("2097K", 2097152, new Color32(40,0,0,255)),
                    new KareOlustur("4194K", 4194304, new Color32(0,0,10,255)),
                    new KareOlustur("8388K", 8388608, new Color32(0,0,10,255)),
                    new KareOlustur("16M", 16777216, new Color32(0,0,10,255)),
                    new KareOlustur("33M", 33554432, new Color32(0,0,10,255))
                })
            );

        temalar.Add(
            new TemaOlustur(
                new Color32(247, 247, 247, 255),
                new Color32(247, 247, 247, 255),
                new Color32(220, 10, 22, 255),
                new List<KareOlustur> {
                    new KareOlustur("2", 2, new Color32(220,10,22,255)),
                    new KareOlustur("4", 4, new Color32(220,10,22,255)),
                    new KareOlustur("8", 8, new Color32(220,10,22,255)),
                    new KareOlustur("16", 16, new Color32(220,10,22,255)),
                    new KareOlustur("32", 32, new Color32(220,10,22,255)),
                    new KareOlustur("64", 64, new Color32(220,10,22,255)),
                    new KareOlustur("128", 128, new Color32(220,10,22,255)),
                    new KareOlustur("256", 256, new Color32(220,10,22,255)),
                    new KareOlustur("512", 512, new Color32(220,10,22,255)),
                    new KareOlustur("1024", 1024, new Color32(220,10,22,255)),
                    new KareOlustur("2048", 2048, new Color32(220,10,22,255)),
                    new KareOlustur("4096", 4096, new Color32(220,10,22,255)),
                    new KareOlustur("8192", 8192, new Color32(220,10,22,255)),
                    new KareOlustur("16K", 16384, new Color32(220,10,22,255)),
                    new KareOlustur("32K", 32768, new Color32(220,10,22,255)),
                    new KareOlustur("65K", 65536, new Color32(220,10,22,255)),
                    new KareOlustur("131K", 131072, new Color32(220,10,22,255)),
                    new KareOlustur("262K", 262144, new Color32(220,10,22,255)),
                    new KareOlustur("524K", 524288, new Color32(220,10,22,255)),
                    new KareOlustur("1048K", 1048576, new Color32(220,10,22,255)),
                    new KareOlustur("2097K", 2097152, new Color32(220,10,22,255)),
                    new KareOlustur("4194K", 4194304, new Color32(220,10,22,255)),
                    new KareOlustur("8388K", 8388608, new Color32(220,10,22,255)),
                    new KareOlustur("16M", 16777216, new Color32(220,10,22,255)),
                    new KareOlustur("33M", 33554432, new Color32(220,10,22,255))
                })
            );

        temalar.Add(
            new TemaOlustur(
                new Color32(0, 0, 0, 255),
                new Color32(200, 200, 200, 255),
                new Color32(100, 100, 100, 255),
                new List<KareOlustur> {
                    new KareOlustur("2", 2, new Color32(160, 160, 160, 255)),
                    new KareOlustur("4", 4, new Color32(150, 150, 150, 255)),
                    new KareOlustur("8", 8, new Color32(140, 140, 140, 255)),
                    new KareOlustur("16", 16, new Color32(130, 130, 130, 255)),
                    new KareOlustur("32", 32, new Color32(120, 120, 120, 255)),
                    new KareOlustur("64", 64, new Color32(110, 110, 110, 255)),
                    new KareOlustur("128", 128, new Color32(100, 100, 100, 255)),
                    new KareOlustur("256", 256, new Color32(90, 90, 90, 255)),
                    new KareOlustur("512", 512, new Color32(80, 80, 80, 255)),
                    new KareOlustur("1024", 1024, new Color32(70, 70, 70, 255)),
                    new KareOlustur("2048", 2048, new Color32(60, 60, 60, 255)),
                    new KareOlustur("4096", 4096, new Color32(50, 50, 50, 255)),
                    new KareOlustur("8192", 8192, new Color32(40, 40, 40, 255)),
                    new KareOlustur("16K", 16384, new Color32(30, 30, 30, 255)),
                    new KareOlustur("32K", 32768, new Color32(20, 20, 20, 255)),
                    new KareOlustur("65K", 65536, new Color32(10, 10, 10, 255)),
                    new KareOlustur("131K", 131072, new Color32(10, 10, 10, 255)),
                    new KareOlustur("262K", 262144, new Color32(10, 10, 10, 255)),
                    new KareOlustur("524K", 524288, new Color32(10, 10, 10, 255)),
                    new KareOlustur("1048K", 1048576, new Color32(10, 10, 10, 255)),
                    new KareOlustur("2097K", 2097152, new Color32(10, 10, 10, 255)),
                    new KareOlustur("4194K", 4194304, new Color32(10, 10, 10, 255)),
                    new KareOlustur("8388K", 8388608, new Color32(10, 10, 10, 255)),
                    new KareOlustur("16M", 16777216, new Color32(10, 10, 10, 255)),
                    new KareOlustur("33M", 33554432, new Color32(10, 10, 10, 255))
                })
            );

        temalar.Add(
            new TemaOlustur(
                new Color32(200, 200, 200, 255),
                new Color32(200, 200, 200, 255),
                new Color32(100, 100, 100, 255),
                new List<KareOlustur> {
                    new KareOlustur("2", 2, new Color32(160, 160, 160, 255)),
                    new KareOlustur("4", 4, new Color32(150, 150, 150, 255)),
                    new KareOlustur("8", 8, new Color32(140, 140, 140, 255)),
                    new KareOlustur("16", 16, new Color32(130, 130, 130, 255)),
                    new KareOlustur("32", 32, new Color32(120, 120, 120, 255)),
                    new KareOlustur("64", 64, new Color32(110, 110, 110, 255)),
                    new KareOlustur("128", 128, new Color32(100, 100, 100, 255)),
                    new KareOlustur("256", 256, new Color32(90, 90, 90, 255)),
                    new KareOlustur("512", 512, new Color32(80, 80, 80, 255)),
                    new KareOlustur("1024", 1024, new Color32(70, 70, 70, 255)),
                    new KareOlustur("2048", 2048, new Color32(60, 60, 60, 255)),
                    new KareOlustur("4096", 4096, new Color32(50, 50, 50, 255)),
                    new KareOlustur("8192", 8192, new Color32(40, 40, 40, 255)),
                    new KareOlustur("16K", 16384, new Color32(30, 30, 30, 255)),
                    new KareOlustur("32K", 32768, new Color32(20, 20, 20, 255)),
                    new KareOlustur("65K", 65536, new Color32(10, 10, 10, 255)),
                    new KareOlustur("131K", 131072, new Color32(10, 10, 10, 255)),
                    new KareOlustur("262K", 262144, new Color32(10, 10, 10, 255)),
                    new KareOlustur("524K", 524288, new Color32(10, 10, 10, 255)),
                    new KareOlustur("1048K", 1048576, new Color32(10, 10, 10, 255)),
                    new KareOlustur("2097K", 2097152, new Color32(10, 10, 10, 255)),
                    new KareOlustur("4194K", 4194304, new Color32(10, 10, 10, 255)),
                    new KareOlustur("8388K", 8388608, new Color32(10, 10, 10, 255)),
                    new KareOlustur("16M", 16777216, new Color32(10, 10, 10, 255)),
                    new KareOlustur("33M", 33554432, new Color32(10, 10, 10, 255))
                })
            );
    }
}
