using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface ILiveObj
{
    public int maxHP { get; set; }
    public int currentHP { get; set; }
    public bool IsAlive { get; set; }
}
