using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{

    public Camera camera;
    public int width;
    public int height;
    bool takeImage;
    static ScreenShot example;
    public string saveLocation;


    private void Awake()
    {
        example = this;
    }


    private void OnPostRender()
    {
        if (takeImage)
        {
            takeImage = false;
            RenderTexture imageGenerator = camera.targetTexture;

            Texture2D imageRequest = new Texture2D(imageGenerator.width, imageGenerator.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, imageGenerator.width, imageGenerator.height);
            imageRequest.ReadPixels(rect, 0, 0);

            byte[] byteArray = imageRequest.EncodeToPNG();
            System.IO.File.WriteAllBytes(saveLocation + "/goruntu" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".png", byteArray);
            Debug.Log("Screenshot taken.");

            RenderTexture.ReleaseTemporary(imageGenerator);
            camera.targetTexture = null;
        }
    }


    public void TakeImage(int width, int height)
    {
        camera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeImage = true;
    }


    static void TakeScreenShot(int width, int height)
    {
        example.TakeImage(width, height);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TakeScreenShot(width, height);
        }
    }
}