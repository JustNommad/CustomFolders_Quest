using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour
{
    [SerializeField] private CharacterButton baseButton;
    private CharacterConfig config;
    private void Start()
    {
        config = Resources.Load<CharacterConfig>("CharacterConfig");
        var names = config.CharacterType;
        foreach (var objName in names)
        {
            var btn = Instantiate(baseButton, transform);
            btn.Setup(objName, OnEffectButton);
        }
    }

    private void OnEffectButton(string id)
    {
        foreach (var i in config.CharacterType)
        {
            var item = GameObject.Find(i + "(Clone)");
            if(item != null)
                Destroy(item);
        }
        var asset = config.GetCharacter(id);
        Instantiate(asset, Vector3.zero, new Quaternion(0f,180f,0f, 0f));
    }
}
