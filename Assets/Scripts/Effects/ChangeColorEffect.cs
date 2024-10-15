using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChangeColorEffect : GameEffect
{
    [ColorUsage(false, false)]
    public Color Color;
    public bool IsColorRandom;

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        if (IsColorRandom)
            Color = UnityEngine.Random.ColorHSV();

        gameEvent.GameObject.GetComponent<MeshRenderer>().material.color = Color;

        yield break;
    }

    public override Color GetColor(GameEvent gameEvent)
    {
        return Color.yellow;
    }
}
