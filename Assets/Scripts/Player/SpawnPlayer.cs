using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnPlayer : MonoBehaviour {

    public void Construct(Object prefab)
    {
        PrefabUtility.InstantiatePrefab(prefab);
    }
}
