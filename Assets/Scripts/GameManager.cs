using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BulletPool BulletPool { get; private set; }

    void Awake()
    {
        BulletPool = new BulletPool(20);
    }
}
