using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Managers;
using DG.Tweening;

namespace Handlers
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _levelPickPanel;
        [SerializeField] private GameObject _lockedPanel;
        [SerializeField] private GameObject _levelPrefab;

        [SerializeField] private GameObject[] _stages;

        private DataManager _dataManager;
        private LevelManager _levelManager;

        private void Start()
        {
            _dataManager = (DataManager)GameCore.Instance.GetManager<DataManager>();
            _levelManager = (LevelManager)GameCore.Instance.GetManager<LevelManager>();
            GenerateLevels();
        }

        private void GenerateLevels()
        {
            int stagesIndex = 1;

            for (int i = 0; i < _levelManager.GetLevelCount(); i++)
            {
                Instantiate(_levelPrefab, _stages[stagesIndex - 1].transform).
                    GetComponent<LevelElement>().Init(
                    i + 1, _dataManager.GetPlayerData().unlockedLevels < i + 1,
                    OnLevelElementButtonClick);

                if (i % 10 == 0 && i != 0) stagesIndex++;
            }
        }

        private void OnLevelElementButtonClick(int levelNumber, bool isLocked)
        {
            if (isLocked)
            {
                _lockedPanel.SetActive(true);
            }
            else
            {
                _dataManager.GetPlayerData().currentLevel = levelNumber;
                SceneManager.LoadScene("GameScene");
            }
        }

        public void OpenLevelPanel()
        {
            _levelPickPanel.SetActive(true);
            _levelPickPanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.InOutBack);
        }

        public void CloseLevelPanel()
        {
            _levelPickPanel.SetActive(true);
            _levelPickPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -4350), 0.5f).SetEase(Ease.InOutBack);
        }
    }
}