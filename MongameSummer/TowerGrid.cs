using Microsoft.Xna.Framework;

public class TowerGrid
{
    private readonly Tile[,] tiles;

    public int Rows { get; }
    public int Cols { get; }

    public Tile this[int row, int col] => tiles[row, col];

    public TowerGrid(int rows, int cols, int tileWidth, int tileHeight, Vector2 start)
    {
        Rows = rows;
        Cols = cols;
        tiles = new Tile[rows, cols];

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
            {
                var x = (int)(start.X + c * tileWidth);
                var y = (int)(start.Y + r * tileHeight);

                var rect = new Rectangle(x, y, tileWidth, tileHeight);

                tiles[r, c] = new Tile(r, c, rect);
            }
    }

    public bool TryGetTileAt(Vector2 pos, out Tile tile)
    {
        Point p = pos.ToPoint();

        for (int r = 0; r < Rows; r++)
            for (int c = 0; c < Cols; c++)
            {
                if (tiles[r, c].Bounds.Contains(p))
                {
                    tile = tiles[r, c];
                    return true;
                }
            }

        tile = null;
        return false;
    }
}
