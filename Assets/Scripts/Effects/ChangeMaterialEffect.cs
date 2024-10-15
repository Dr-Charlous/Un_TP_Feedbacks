using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChangeMaterialEffect : GameEffect
{
    public Renderer RendererToChange;
    public Material Material;

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        gameEvent.GameObject.GetComponent<Renderer>().material = Material;
        yield break;
    }
}
