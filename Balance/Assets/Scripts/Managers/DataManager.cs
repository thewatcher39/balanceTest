using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class DataManager : MonoBehaviour, IManager
    {
        public class Player
        {
            public int currentLevel;
            public int unlockedLevels;

            public Player()
            {
                unlockedLevels = 1;
            }

            public void Save()
            {

            }

            public void Load()
            {

            }
        }

        private Player _player;

        public void Init()
        {
            if (!PlayerPrefs.HasKey(Constants.Vars.PlayerPrefsKeys.IS_FIRST_LAUNCH))
                _player = new Player();
            else
                _player.Load();
        }

        public Player GetPlayerData()
        {
            return _player;
        }
    }
}