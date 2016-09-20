using UnityEngine;
using System.Collections;

/// <summary>
/// Helper class to construct game objects with
/// special behaviour in a uniform way
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class ObjectBuilder
{
    private GameObject result = null;

    /// <summary>
    /// Creates the base object from the given prefab.
    /// Its initial state is set to inactive.
    /// </summary>
    /// <param name="prefab">prefab with minimal required components, e.g. a collider</param>
    public void BuildObject(GameObject prefab)
    {
        result = MonoBehaviour.Instantiate(prefab);
        result.SetActive(false);
    }

    public GameObject GetResult()
    {
        return result;
    }

    public T BuildComponent<T>() where T : Component
    {
        return result.AddComponent<T>();
    }
}
