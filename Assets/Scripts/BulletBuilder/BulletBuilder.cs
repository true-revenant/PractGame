using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BulletBuilder
{
    protected GameObject _gameObject;

    public BulletBuilder() => _gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    protected BulletBuilder(GameObject gO) => _gameObject = gO;

    public BulletVisualBuilder Visual => new BulletVisualBuilder(_gameObject);

    public BulletPhysicsBuilder Physics => new BulletPhysicsBuilder(_gameObject);

    public static implicit operator GameObject(BulletBuilder builder)
    {
        return builder._gameObject;
    }
}
