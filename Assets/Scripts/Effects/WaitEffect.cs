using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaitEffect : GameEffect
{
    public float Timer;

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        yield return new WaitForSeconds(Timer);
    }

    public override Color GetColor(GameEvent gameEvent)
    {
        return Color.green;
    }
}
