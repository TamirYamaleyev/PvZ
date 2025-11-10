using Microsoft.Xna.Framework;

public class Tile
{
    public int Row { get; }
    public int Col { get; }

    public Rectangle Bounds { get; }

    public Tower Occupant { get; private set; }

    public bool IsEmpty => Occupant == null;

    public Tile(int row, int col, Rectangle bounds)
    {
        Row = row;
        Col = col;
        Bounds = bounds;
    }

    public void PlaceTower(Tower tower)
    {
        Occupant = tower;
    }

    public void RemoveTower()
    {
        Occupant = null;
    }
}
