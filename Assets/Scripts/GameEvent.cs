using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
[Serializable]
public class GameEvent : ScriptableObject
{
    [SerializeReference]
    public List<GameEffect> Effects;
    public GameObject GameObject;

    public IEnumerator Execute()
    {
        foreach (var item in Effects)
        {
            yield return item.Execute(this);
        }
    }
}