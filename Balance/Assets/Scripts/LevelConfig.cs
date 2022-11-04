using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Config #", menuName = "Level Config", order = 1)]
public class LevelConfig : ScriptableObject
{
    public int levelNumber;
    public Sprite background;
    public List<GameObject> playableObjects;
    public List<PlatformData> platformObjects;
}

[System.Serializable]
public class PlatformData
{
    public GameObject platformPrefab;
    public Vector3 platformPosition;
}