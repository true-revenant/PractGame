using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DamagingObjectsPool BulletPool { get; private set; }

    void Awake()
    {
        BulletPool = new DamagingObjectsPool(20);
    }
}
