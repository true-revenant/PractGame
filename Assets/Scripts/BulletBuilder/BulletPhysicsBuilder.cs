using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class BulletPhysicsBuilder : BulletBuilder
{
    public BulletPhysicsBuilder(GameObject gO) : base(gO) { }

    public BulletPhysicsBuilder AddSphereCollider()
    {
        var component = GetOrAddComponent<SphereCollider>();
        component.isTrigger = true;
        return this;
    }

    public BulletPhysicsBuilder AddRigidBody()
    {
        var component = GetOrAddComponent<Rigidbody>();
        component.mass = 0.001f;
        component.drag = 0;
        component.angularDrag = 0.05f;
        component.useGravity = false;
        return this;
    }

    public BulletPhysicsBuilder AddScriptBullet()
    {
        GetOrAddComponent<Bullet>();
        return this;
    }

    private T GetOrAddComponent<T>() where T : Component
    {
        var result = _gameObject.GetComponent<T>();
        if (!result)
        {
            result = _gameObject.AddComponent<T>();
        }
        return result;
    }
}
