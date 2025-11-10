using Microsoft.Xna.Framework;

public class Tile
{
    public int Row { get; }
    public int Col { get; }

    public Rectangle Bounds { get; }

    public Tower PlacedTower { get; private set; }

    public bool IsEmpty => PlacedTower == null;

    public Tile(int row, int col, Rectangle bounds)
    {
        Row = row;
        Col = col;
        Bounds = bounds;
    }

    public void PlaceTower(Tower tower)
    {
        PlacedTower = tower;
        tower.Tile = this;
    }

    public void RemoveTower()
    {
        if (PlacedTower != null)
            PlacedTower.Tile = null;
        PlacedTower = null;
    }
}
