using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsCategoryChanger : MonoBehaviour
{
    [SerializeField] private RectTransform[] _settingsCategoriesRoots;
    [SerializeField] private ScrollRect _scrollRect;

    public void ChangeCategory(int index)
    {
        if (_scrollRect == null)
        {
            throw new ArgumentNullException($"_scrollRect can{"'"}t be null",new ArgumentNullException());
        }

        if (_settingsCategoriesRoots.Length == 0)
        {
          throw new Exception($"_settingsCategoriesRoots has no elements");
        }

        if (index > _settingsCategoriesRoots.Length-1)
        {
            throw new ArgumentOutOfRangeException();
        }

        // Set active all settings Category Root if it isn't an selected settings Category Root in index
        foreach (RectTransform settingsCategoryRoot in _settingsCategoriesRoots)
        {
            settingsCategoryRoot.gameObject.SetActive(settingsCategoryRoot == _settingsCategoriesRoots[index]);
        }

        _scrollRect.content = _settingsCategoriesRoots[index];
    }
}