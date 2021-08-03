using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletBuilderExtensions
{
    public static GameObject AddName(this GameObject gameObject, string name)
    {
        gameObject.name = name;
        return gameObject;
    }

    public static GameObject SetBulletScale(this GameObject gameObject, float scale)
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        return gameObject;
    }

    public static GameObject AddMeshRenderer(this GameObject gameObject, Material material)
    {
        var component = gameObject.GetOrAddComponent<MeshRenderer>();
        component.material = material;
        return gameObject;
    }

    public static GameObject AddBulletScript(this GameObject gameObject)
    {
        gameObject.GetOrAddComponent<Bullet>();
        return gameObject;
    }

    public static GameObject AddSphereCollider(this GameObject gameObject)
    {
        var component = gameObject.GetOrAddComponent<SphereCollider>();
        component.isTrigger = true;
        return gameObject;
    }

    public static GameObject AddRigidBody(this GameObject gameObject)
    {
        var component = gameObject.GetOrAddComponent<Rigidbody>();
        component.mass = 0.001f;
        component.drag = 0;
        component.angularDrag = 0.05f;
        component.useGravity = false;
        return gameObject;
    }

    private static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        var result = gameObject.GetComponent<T>();
        if (!result)
        {
            result = gameObject.AddComponent<T>();
        }
        return result;
    }
}
