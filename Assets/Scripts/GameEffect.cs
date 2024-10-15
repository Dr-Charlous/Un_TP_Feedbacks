using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEffect
{
    public bool Enabled = true;

    public virtual IEnumerator Execute(GameEvent gameEvent)
    {
        yield break;
    }
}
