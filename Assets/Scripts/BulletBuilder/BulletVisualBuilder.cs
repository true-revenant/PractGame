using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class BulletVisualBuilder : BulletBuilder
{
    public BulletVisualBuilder(GameObject gO) : base(gO) 
    {
        _gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public BulletVisualBuilder AddName(string name)
    {
        _gameObject.name = name;
        return this;
    }

    //public BulletVisualBuilder AddFilter(Mesh mesh)
    //{
    //    var component = GetOrAddComponent<MeshFilter>();
    //    component.mesh = mesh;
    //    return this;
    //}

    public BulletVisualBuilder AddRenderer(Material mat)
    {
        var component = GetOrAddComponent<MeshRenderer>();
        component.material = mat;
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
