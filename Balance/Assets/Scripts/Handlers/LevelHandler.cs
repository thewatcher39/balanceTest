using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

namespace Handlers
{
    public class LevelHandler : MonoBehaviour
    {
        public static GameObject pickedObject;

        [SerializeField] private Transform _pickZone;
        [SerializeField] private Image _background;

        private List<GameObject> _poolOfPlayablebjects = new List<GameObject>();
        private List<GameObject> _poolOfPlatform = new List<GameObject>();

        private DataManager dataManager;
        private LevelManager levelManager;

        private LevelConfig _levelConfig;

        private void Awake()
        {
            CacheManagers();
            LoadLevel();
        }

        private void CacheManagers()
        {
            dataManager = (DataManager)GameCore.Instance.GetManager<DataManager>();
            levelManager = (LevelManager)GameCore.Instance.GetManager<LevelManager>();
        }

        private void LoadLevel()
        {
            _levelConfig = levelManager.GetLevelData(dataManager.GetPlayerData().currentLevel);
            SpawnLevelObjects();
            _background.sprite = _levelConfig.background;
        }

        private void SpawnLevelObjects()
        {
            for (int i = 0; i < _levelConfig.platformObjects.Count; i++)
            {
                _poolOfPlatform.Add(
                    Instantiate(_levelConfig.platformObjects[i].platformPrefab,
                    _levelConfig.platformObjects[i].platformPosition,
                    Quaternion.identity)
                    );
            }

            float xPos = -1.7f;

            for (int i = 0; i < _levelConfig.playableObjects.Count; i++)
            {
                _poolOfPlayablebjects.Add(
                Instantiate(_levelConfig.playableObjects[i],
                new Vector3(xPos, 3.37f, 0),
                Quaternion.identity));

                xPos += ((float)5 / (float)_levelConfig.playableObjects.Count);
            }
        }

        public void Put()
        {
            pickedObject.GetComponent<Collider2D>().isTrigger = false;
            pickedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            pickedObject = null;
        }

        private IEnumerator StartFinishProcess()
        {
            yield return new WaitForSeconds(Constants.Vars.TIME_TO_WAIT_BEFORE_FINISH_LEVEL);
        }
    }
}