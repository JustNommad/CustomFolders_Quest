using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
   [SerializeField] private int width = 512;
   [SerializeField] private int height = 512;

   [SerializeField] private float xOrigin;
   [SerializeField] private float yOrigin;

   [SerializeField] private float scale;

   private Texture2D noiseTexture;
   private Color[] pix;
   private Renderer rend;

   private void Start()
   {
      rend = GetComponent<Renderer>();
      
      noiseTexture = new Texture2D(width, height);
      pix = new Color[noiseTexture.width * noiseTexture.height];
      rend.material.mainTexture = noiseTexture;
   }

   private void CalcNoise()
   {
      var y = 0f;

      while (y < noiseTexture.height)
      {
         var x = 0f;
         while (x < noiseTexture.width)
         {
            var xCoord = xOrigin + x / noiseTexture.width * scale;
            var yCoord = yOrigin + y / noiseTexture.height * scale;

            var sample = Mathf.PerlinNoise(xCoord, yCoord);
            pix[(int)y * noiseTexture.width + (int)x] = new Color(sample,sample, sample);
            x++;
         }
         y++;
      }
      noiseTexture.SetPixels(pix);
      noiseTexture.Apply();
   }

   private void Update()
   {
      CalcNoise();
   }

   [ContextMenu("Save")]
   private void SaveTexture()
   {
      var bytes = noiseTexture.EncodeToPNG();
      var path = Path.Combine(Application.dataPath, "Textures");
      path = Path.Combine(path, "texture.png");

      if (!Directory.Exists(Path.GetDirectoryName(path)))
      {
         Directory.CreateDirectory(Path.GetDirectoryName(path));
      }
      File.WriteAllBytes(path, bytes);
      
      AssetDatabase.Refresh();
   }
}
