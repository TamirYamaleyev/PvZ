using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MongameSummer;
using System.Collections.Generic;
using System.Data;
using IDrawable = MongameSummer.IDrawable;

public class GridScene : IDrawable
{
    private EnemySpawner spawner;
    private float spawnInterval = 5f;

    int cellBorderThickness = 1;
    Color borderColor = Color.SlateGray;

    private TowerGrid grid;

    Color gridColor = new Color(139, 69, 19);

    int gRows = 5;
    int gCols = 11;
    int gTileWidth = 150;
    int gTileHeight = 120;
    int gLeftMargin = 100;

    public GridScene()
    {
        grid = new TowerGrid(
            rows: gRows,
            cols: gCols,
            tileWidth: gTileWidth,
            tileHeight: gTileHeight,
            start: new Vector2(gLeftMargin, Game1.ScreenCenterHeight - (gRows * gTileHeight) * 0.5f)
        );

        spawner = new EnemySpawner(grid, spawnInterval);
        SceneManager.Add(spawner);
    }

    public void Update(GameTime gameTime)
    {
        var mouse = Mouse.GetState();

        // Only act when left mouse button is pressed
        if (mouse.LeftButton == ButtonState.Pressed)
        {
            Vector2 mousePos = new Vector2(mouse.X, mouse.Y);

            // Find the tile under the mouse
            if (grid.TryGetTileAt(mousePos, out Tile tile) && tile.IsEmpty)
            {
                // Create a tower and register it with SceneManager
                var tower = SceneManager.Create<defaultTower>(); // your Animation tower class
                tower.position = tile.Bounds.Center.ToVector2();  // center it on the tile

                // Mark the tile as occupied
                tile.PlaceTower(tower);
            }
        }
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var tile in GetAllTiles())
        {
            // Draw the solid brown tile
            spriteBatch.Draw(
                SpriteManager.GetSprite("pixel").texture,
                tile.Bounds,
                gridColor
            );

            // Top
            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, new Rectangle(tile.Bounds.X, tile.Bounds.Y, tile.Bounds.Width, cellBorderThickness), borderColor);
            // Bottom
            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, new Rectangle(tile.Bounds.X, tile.Bounds.Bottom - cellBorderThickness, tile.Bounds.Width, cellBorderThickness), borderColor);
            // Left
            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, new Rectangle(tile.Bounds.X, tile.Bounds.Y, cellBorderThickness, tile.Bounds.Height), borderColor);
            // Right
            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, new Rectangle(tile.Bounds.Right - cellBorderThickness, tile.Bounds.Y, cellBorderThickness, tile.Bounds.Height), borderColor);
        }
    }


    private IEnumerable<Tile> GetAllTiles()
    {
        for (int r = 0; r < grid.Rows; r++)
            for (int c = 0; c < grid.Cols; c++)
                yield return grid[r, c];
    }
}
