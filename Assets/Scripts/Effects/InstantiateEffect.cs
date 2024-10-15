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
        GameObject obj;

        if (gameEvent.GameObject != null)
            obj = GameObject.Instantiate(Prefab, gameEvent.GameObject.transform.position, Quaternion.identity);
        else 
            obj = GameObject.Instantiate(Prefab, Vector3.zero, Quaternion.identity);

        if (IsRef)
            gameEvent.GameObject = obj;

        yield break;
    }

    public override Color GetColor(GameEvent gameEvent)
    {
        return Color.red;
    }
}
