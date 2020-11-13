using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameAnalyticsSDK.Setup;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour
{
    [SerializeField] private CharacterButton baseButton;
    private CharacterConfig config;

    private GameObject _chosenPrefab;
    private void Start()
    {
        config = Resources.Load<CharacterConfig>("CharacterConfig");
        var names = config.CharacterType;
        foreach (var objName in names)
        {
            var btn = Instantiate(baseButton, transform);
            btn.Setup(objName, OnCharacterButton);
        }
    }

    private void OnCharacterButton(string id)
    {
        foreach (var i in config.CharacterType)
        {
            var item = GameObject.Find(i + "(Clone)");
            if(item != null)
                Destroy(item);
        }
        var asset = config.GetCharacter(id);
        _chosenPrefab = Instantiate(asset, Vector3.zero, new Quaternion(0f,180f,0f, 0f));
        
        SkinsButtons(id);
    }

    private void OnSkinButton(string id)
    {
        var skinTypes = id.Split('_');

        var directoryInfo = new DirectoryInfo(Application.streamingAssetsPath + "/Skins/" + skinTypes[0]);
        print($"Streaming path: {Application.streamingAssetsPath}");

        var skins = directoryInfo.GetFiles("*.*");
        var f = skins.FirstOrDefault(s => s.FullName.Contains(id) && !s.FullName.Contains("meta"));
        
        var texture2d = TGALoader.LoadTGA(f.FullName);

        var prefabSkin = _chosenPrefab.GetComponentsInChildren<SkinnedMeshRenderer>()
        .FirstOrDefault(s => s.name.Contains(skinTypes[1]));
        prefabSkin.sharedMaterial.mainTexture = texture2d;
    }
    private void SkinsButtons(string id)
    {
        var directoryInfo = new DirectoryInfo(Application.streamingAssetsPath + "/Skins/" + id);
        print($"Streaming path: {Application.streamingAssetsPath}");

        var skins = directoryInfo.GetFiles("*.*");
        foreach (var f in skins)
        {
            Debug.Log($"File name {f}");

            if (f.Name.Contains("meta"))
                continue;

            var btn = Instantiate(baseButton, transform);
            btn.Setup(f.Name, OnSkinButton);
        }
    }
}
