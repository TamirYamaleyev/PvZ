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

    private TowerSelectionBar selectionBar;

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

        selectionBar = new TowerSelectionBar(new List<string> { "defaultTower", "defaultTower", "defaultTower" }, Game1.oswaldFont);
        SceneManager.Add(selectionBar);

        spawner = new EnemySpawner(grid);
        SceneManager.Add(spawner);
    }

    public void Update(GameTime gameTime)
    {
        var mouse = Mouse.GetState();
        Vector2 mousePos = new Vector2(mouse.X, mouse.Y);

        if (mouse.LeftButton == ButtonState.Pressed)
        {
            OnLMB(mousePos);
        }

        if (mouse.RightButton == ButtonState.Pressed)
        {
            OnRMB(mousePos);
        }
    }

    private void OnLMB(Vector2 mousePos)
    {
        if (grid.TryGetTileAt(mousePos, out Tile tile) && tile.IsEmpty)
        {
            if (selectionBar.SelectedTower != null)
            {
                var tower = TowerFactory.CreateTower(selectionBar.SelectedTower.SpriteName);
                if (tower != null)
                {
                    if (Game1.player.SpendGold(tower.Cost))
                    {
                        tower.position = tile.Bounds.Center.ToVector2();
                        tile.PlaceTower(tower);
                        SceneManager.Add(tower);
                    }
                }
            }
        }
    }

    private void OnRMB(Vector2 mousePos)
    {
        if (grid.TryGetTileAt(mousePos, out Tile tile) && !tile.IsEmpty)
        {
            SceneManager.Remove(tile.PlacedTower);
            tile.RemoveTower();
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
