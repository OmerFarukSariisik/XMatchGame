using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private TMP_Text matchCountText;
    [SerializeField] private TMP_InputField sizeText;
    [SerializeField] private Button rebuildButton;
    
    private int _matchCount = 0;

    private void Awake()
    {
        rebuildButton.onClick.AddListener(OnRebuildClicked);
        gridManager.OnMatch += OnMatch;
    }

    private void OnRebuildClicked()
    {
        var isParsed = int.TryParse(sizeText.text, out var size);
        if (!isParsed)
            return;
        gridManager.RebuildGrid(size);
        _matchCount = 0;
        UpdateMatchCountText();
    }
    
    private void OnMatch()
    {
        _matchCount++;
        UpdateMatchCountText();
    }

    private void UpdateMatchCountText()
    {
        matchCountText.text = $"Match Count: {_matchCount}";
    }

    private void OnDestroy()
    {
        rebuildButton.onClick.RemoveAllListeners();
    }
}
