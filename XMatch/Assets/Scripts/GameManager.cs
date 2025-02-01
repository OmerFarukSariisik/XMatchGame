using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text matchCountText;
    [SerializeField] private TMP_Text sizeText;
    [SerializeField] private Button rebuildButton;

    private void Awake()
    {
        rebuildButton.onClick.AddListener(OnRebuildClicked);
    }

    private void OnRebuildClicked()
    {
        
    }

    private void OnDestroy()
    {
        rebuildButton.onClick.RemoveAllListeners();
    }
}
