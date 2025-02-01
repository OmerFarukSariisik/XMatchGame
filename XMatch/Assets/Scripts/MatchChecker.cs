using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker
{
    private List<TileComponent> _flaggedTiles = new();
    private int _size;
    private const int MIN_TILES_FOR_MATCH = 3;

    public void SetSize(int size)
    {
        _size = size;
    }
    
    public List<TileComponent> CheckForMatches(TileComponent clickedTile, TileComponent[,] tiles)
    {
        _flaggedTiles.Clear();
        CheckTiles(clickedTile, tiles);
        _flaggedTiles.ForEach(tile => tile.SetIsFlagged(false));
        if (_flaggedTiles.Count < MIN_TILES_FOR_MATCH)
            _flaggedTiles.Clear();
        
        return _flaggedTiles;
    }

    private void CheckTiles(TileComponent tile, TileComponent[,] tiles)
    {
        tile.SetIsFlagged(true);
        _flaggedTiles.Add(tile);
        var (row, column) = tile.GetRowAndColumn();

        if (CheckValidity(row + 1, column, tiles))
            CheckTiles(tiles[row + 1, column], tiles);  
        if (CheckValidity(row - 1, column, tiles))
            CheckTiles(tiles[row - 1, column], tiles);
        if (CheckValidity(row, column + 1, tiles))
            CheckTiles(tiles[row, column + 1], tiles);
        if (CheckValidity(row, column - 1, tiles))
            CheckTiles(tiles[row, column - 1], tiles);
    }
    
    private bool CheckValidity(int row, int column, TileComponent[,] tiles)
    {
        var isValid = row >= 0 && row < _size && column >= 0 && column < _size;
        return isValid && CheckTile(tiles[row, column]);
    }
    
    private bool CheckTile(TileComponent tile)
    {
        return tile.IsClicked() && !tile.IsFlagged();
    }
}
