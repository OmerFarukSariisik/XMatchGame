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
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float widthHeightPercentageFactor = 1.15f;
    [SerializeField] private int defaultSize = 4;

    private int _matchCount = 0;

    private void Awake()
    {
        rebuildButton.onClick.AddListener(OnRebuildClicked);
        gridManager.OnMatch += OnMatch;
    }

    private void Start()
    {
        gridManager.SetSize(defaultSize);
        gridManager.BuildGrid();
        UpdateGridScaleAndPosition(defaultSize);
    }

    private void OnRebuildClicked()
    {
        var isParsed = int.TryParse(sizeText.text, out var size);
        if (!isParsed)
            return;
        gridManager.RebuildGrid(size);
        _matchCount = 0;
        UpdateMatchCountText();
        UpdateGridScaleAndPosition(size);
    }

    private void UpdateGridScaleAndPosition(int size)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        var screenWorldSize =
            mainCamera.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, mainCamera.nearClipPlane));
        
        var width = screenWorldSize.x * 2f;
        var scaleFactor = Mathf.Min(width, screenWorldSize.y * widthHeightPercentageFactor);
        var scale = scaleFactor / size;
        var yPos = screenWorldSize.y - (scale * size / 2f);
        gridManager.SetScaleAndPosition(scale, yPos);
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