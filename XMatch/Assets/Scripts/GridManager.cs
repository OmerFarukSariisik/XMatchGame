using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour
{
    [SerializeField] private TileComponent tileComponent;
    [SerializeField] private GameObject xPrefab;
    [SerializeField] private int defaultSize = 4;
    
    private TileComponent[,] _tiles;

    private int _size;

    private void Start()
    {
        _size = defaultSize;
        BuildGrid();
    }

    public void BuildGrid()
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
        Instantiate(xPrefab, clickedTile.transform);
    }

    private void OnDestroy()
    {
        foreach (var tile in _tiles)
            tile.OnTileClicked -= OnTileClicked;
    }
}
