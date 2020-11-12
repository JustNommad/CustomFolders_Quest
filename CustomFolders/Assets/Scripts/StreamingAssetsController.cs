using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StreamingAssetsController : MonoBehaviour
{
    [SerializeField] private ImageData baseIcon;

    private void Start()
    {
        baseIcon.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        baseIcon.gameObject.SetActive(true);
        
        var directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        print($"Streaming path: {Application.streamingAssetsPath}");

        var allFiles = directoryInfo.GetFiles("*.*");
        foreach (var f in allFiles)
        {
            Debug.Log($"File name {f}");

            if (f.Name.Contains("meta"))
                continue;

            var imageData = Instantiate(baseIcon, baseIcon.transform.parent);

            var bytes = File.ReadAllBytes(f.FullName);
            var texture2d = new Texture2D(1,1);

            texture2d.LoadImage(bytes);
            
            var rect = new Rect(0,0,texture2d.width, texture2d.height);
            var pivot = new Vector2(0.5f, 0.5f);

            var sprite = Sprite.Create(texture2d, rect, pivot);
            imageData.Image.sprite = sprite;
            imageData.Text.text = f.Name;
        }
        baseIcon.gameObject.SetActive(false);
    }
}
