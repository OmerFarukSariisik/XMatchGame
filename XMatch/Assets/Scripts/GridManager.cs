using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GridManager : MonoBehaviour
{
    [SerializeField] private TileComponent tileComponent;
    [SerializeField] private GameObject xPrefab;
    [SerializeField] private int defaultSize = 4;
    
    [Inject] private MatchChecker _matchChecker;
    
    private TileComponent[,] _tiles;
    private int _size;
    
    public Action OnMatch;

    private void Start()
    {
        SetSize(defaultSize);
        BuildGrid();
    }

    private void SetSize(int size)
    {
        _size = size;
        _matchChecker.SetSize(_size);
    }

    public void RebuildGrid(int size)
    {
        SetSize(size);
        UnregisterFromEvents();
        foreach (var tile in _tiles)
            Destroy(tile.gameObject);
        
        BuildGrid();
    }

    private void BuildGrid()
    {
        _tiles = new TileComponent[_size, _size];
        for (var i = 0; i < _size; i++)
        {
            for (var j = 0; j < _size; j++)
            {
                var xPos = j - (_size - 1) / 2f;
                var yPos = i - (_size - 1) / 2f;
                var position = new Vector3(xPos, yPos, 0);

                var newTile = Instantiate(tileComponent, position, Quaternion.identity, transform);
                _tiles[i, j] = newTile;
                newTile.Setup(i, j);
                newTile.OnTileClicked += OnTileClicked;
            }
        }
    }

    private void OnTileClicked(TileComponent clickedTile)
    {
        clickedTile.CreateXGameObject(xPrefab);
        
        var flaggedTiles = _matchChecker.CheckForMatches(clickedTile, _tiles);
        if (flaggedTiles.Count > 0)
        {
            flaggedTiles.ForEach(tile => tile.RemoveX());
            OnMatch?.Invoke();
        }
    }

    private void UnregisterFromEvents()
    {
        foreach (var tile in _tiles)
            tile.OnTileClicked -= OnTileClicked;
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }
}
