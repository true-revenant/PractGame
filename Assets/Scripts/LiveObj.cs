using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LiveObj : MonoBehaviour
{
    protected int maxHP;
    protected int currentHP;

    public bool IsAlive { get; protected set; }
}
