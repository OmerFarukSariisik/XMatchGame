using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileComponent : MonoBehaviour, IPointerClickHandler
{
    public Action<TileComponent> OnTileClicked;
    
    private GameObject _xGameObject;
    
    private int _row;
    private int _column;
    private bool _isClicked;
    private bool _isFlagged;

    public void Setup(int row, int column, Vector3 position)
    {
        _row = row;
        _column = column;
        transform.localPosition = position;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClicked)
            return;
        
        _isClicked = true;
        OnTileClicked?.Invoke(this);
    }

    public (int row, int column) GetRowAndColumn()
    {
        return (_row, _column);
    }

    public bool IsClicked()
    {
        return _isClicked;
    }
    
    public bool IsFlagged()
    {
        return _isFlagged;
    }

    public void SetIsFlagged(bool isFlagged)
    {
        _isFlagged = isFlagged;
    }

    public void CreateXGameObject(GameObject xGameObject)
    {
        if (_xGameObject == null)
            _xGameObject = Instantiate(xGameObject, transform);
        else
            _xGameObject.SetActive(true);
    }

    public void RemoveX()
    {
        _isClicked = false;

        if (_xGameObject == null)
        {
            Debug.LogError("XGameObject is null!");
            return;
        }
        
        _xGameObject.SetActive(false);
    }
}
