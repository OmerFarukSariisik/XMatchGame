using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileComponent : MonoBehaviour, IPointerClickHandler
{
    public Action<TileComponent> OnTileClicked;
    
    private int _row;
    private int _column;
    private bool _isClicked;
    
    public void Setup(int row, int column)
    {
        _row = row;
        _column = column;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClicked)
            return;
        
        _isClicked = true;
        OnTileClicked?.Invoke(this);
    }
}
