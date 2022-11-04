using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class LevelElement : MonoBehaviour
{
    [SerializeField] private GameObject _lockHolder;
    [SerializeField] private Button _button;
    [SerializeField] private TMPro.TextMeshProUGUI _levelNumber;

    public void Init(int levelNumber, bool isLocked, UnityAction<int, bool> onClick)
    {
        _levelNumber.text = levelNumber.ToString();
        _levelNumber.gameObject.SetActive(!isLocked);
        _lockHolder.SetActive(isLocked);
        _button.onClick.AddListener(() => { onClick(levelNumber, isLocked); });
    }

}
