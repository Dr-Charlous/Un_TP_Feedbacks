using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class InstantiateEffect : GameEffect
{
    public GameObject Prefab;
    public bool IsRef;

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        var obj = GameObject.Instantiate(Prefab, gameEvent.GameObject.transform.position, Quaternion.identity);

        if (IsRef)
            gameEvent.GameObject = obj;

        yield break;
    }
}
