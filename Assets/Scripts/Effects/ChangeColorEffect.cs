using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChangeColorEffect : GameEffect
{
    [ColorUsage(false, false)]
    public Color Color;

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        gameEvent.GameObject.GetComponent<MeshRenderer>().material.color = Color;
        yield break;
    }
}
