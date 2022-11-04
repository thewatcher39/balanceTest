using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(LevelManager), typeof(DataManager))]
    public class GameCore : MonoBehaviour
    {
        private static GameCore _mInstance;

        private List<IManager> _managers = new List<IManager>();

        public static GameCore Instance
        {
            get
            {
                return _mInstance;
            }
            private set
            {

            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _mInstance = this;
            Instance = _mInstance;

            Instance._managers.Add(gameObject.GetComponent<DataManager>());
            Instance._managers.Add(gameObject.GetComponent<LevelManager>());

            foreach (var item in Instance._managers)
            {
                item.Init();
            }
        }

        public IManager GetManager<T>()
        {
            foreach (var item in Instance._managers)
            {
                if (item is T)
                    return item;
            }
            Debug.LogError("Manager was not found");
            return null;
        }
    }
}