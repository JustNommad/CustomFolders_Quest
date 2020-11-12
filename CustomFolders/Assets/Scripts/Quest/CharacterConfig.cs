using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Tools/Character")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] private string[] _characterType;

    public string[] CharacterType => _characterType;

    public GameObject GetCharacter(string characterType)
    {
        var objName = _characterType.FirstOrDefault(e => e == characterType);
        return string.IsNullOrEmpty(objName) ? null : LoadObject(characterType);
    }
    private GameObject LoadObject(string effectName)
    {
        return Resources.Load<GameObject>($"Prefabs/Character/{effectName}");
    }
#if UNITY_EDITOR
    private void Reset()
    {
        var objects = Resources.LoadAll<GameObject>("Prefabs/Character");
        _characterType = new string[objects.Length];

        for (int i = 0; i < _characterType.Length; i++)
            _characterType[i] = objects[i].name;
    }
#endif
}
