using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour, IManager
    {
        [SerializeField] private List<LevelConfig> _levels;

        public LevelConfig GetLevelData(int levelNumber)
        {
            foreach (var item in _levels)
            {
                if (item.levelNumber == levelNumber) return item;
            }
            
            Debug.LogError("Level not found");

            return null;
        }

        public int GetLevelCount()
        {
            return _levels.Count;
        }

        public void Init()
        {
            
        }
    }
}