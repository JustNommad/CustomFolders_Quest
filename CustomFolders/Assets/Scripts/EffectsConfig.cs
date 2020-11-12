using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu]
public class EffectsConfig : ScriptableObject
{
    [SerializeField] private string[] effects;

    public string[] Effects => effects;

    public GameObject GetRandomEffect()
    {
        var effectName = effects[Random.Range(0, effects.Length)];
        return LoadObject(effectName);
    }

    public GameObject GetEffect(string effectName)
    {
        var objName = effects.FirstOrDefault(e => e == effectName);
        return string.IsNullOrEmpty(objName) ? null : LoadObject(effectName);
    }
    private GameObject LoadObject(string effectName)
    {
        return Resources.Load<GameObject>($"Effects/{effectName}");
    }
    
    #if UNITY_EDITOR
    private void Reset()
    {
        var objects = Resources.LoadAll<GameObject>("Effects");
        effects = new string[objects.Length];

        for (int i = 0; i < effects.Length; i++)
            effects[i] = objects[i].name;
    }
    #endif
}
