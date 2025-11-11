using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MongameSummer
{
    internal class TowerSelectionBar : IDrawable, IUpdateable
    {
        private SpriteFont font;

        private List<Tower> towers;
        private List<Rectangle> slots;

        private int slotSize = 80;
        private int spacing = 10;
        private Vector2 startPos = new Vector2(10, 10);

        private int costCellHeight = 20;

        Color defaultColor = Color.Gray;
        Color selectedColor = Color.Yellow;

        float cellPaddingPercentage = 0.1f;

        public Tower SelectedTower { get; private set; }
        private int selectedIndex = -1;

        public int topPadding = 8;
        public int backgroundPadding = 10;
        public Color BackgroundColor { get; set; } = new Color(139, 69, 19); // Brown

        public TowerSelectionBar(List<string> towerNames, SpriteFont font)
        {
            this.font = font;

            // for Tower Factory
            towers = new List<Tower>();
            foreach (var name in towerNames)
            {
                var tower = TowerFactory.CreateTower(name);
                if (tower != null)
                    towers.Add(tower);
            }

            slots = new List<Rectangle>();
            int totalWidth = towerNames.Count * slotSize + (towerNames.Count - 1) * spacing;
            float startX = Game1.ScreenCenterWidth - totalWidth * 0.5f;

            startPos = new Vector2(startX, backgroundPadding + topPadding);

            for (int i = 0; i < towerNames.Count; i++)
            {
                int x = (int)(startPos.X + i * (slotSize + spacing));
                int y = (int)startPos.Y;
                slots.Add(new Rectangle(x, y, slotSize, slotSize));
            }

            SelectedTower = null;
        }

        public void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                Vector2 mousePos = new Vector2(mouse.X, mouse.Y);

                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i].Contains(mousePos))
                    {
                        selectedIndex = i;
                        SelectedTower = towers[i];
                        break;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawBackground(spriteBatch);
            DrawGold(spriteBatch);

            for (int i = 0; i < slots.Count; i++)
            {
                DrawSlotBackground(spriteBatch, i);
                DrawTowerSprite(spriteBatch, i);
            }
        }

        private void DrawSlotBackground(SpriteBatch spriteBatch, int index)
        {
            var slotRect = slots[index];
            Color bgColor = (index == selectedIndex) ? selectedColor : defaultColor;

            // Draw sprite cell (top)
            int spriteCellHeight = (int)(slotRect.Height * 0.7f);
            Rectangle spriteCell = new Rectangle(slotRect.X, slotRect.Y, slotRect.Width, spriteCellHeight);
            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, spriteCell, bgColor);

            // Draw cost cell (bottom)
            int costCellHeight = slotRect.Height - spriteCellHeight;
            Rectangle costCell = new Rectangle(slotRect.X, spriteCell.Bottom, slotRect.Width, costCellHeight);
            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, costCell, Color.DarkGray); // different color if you want
        }

        private void DrawTowerSprite(SpriteBatch spriteBatch, int index)
        {
            var slotRect = slots[index];
            var tower = towers[index];
            var sprite = SpriteManager.GetSprite(tower.SpriteName);

            // Sprite cell
            int spriteCellHeight = (int)(slotRect.Height * 0.7f);
            Rectangle spriteCell = new Rectangle(slotRect.X, slotRect.Y, slotRect.Width, spriteCellHeight);

            // Maintain aspect ratio
            float spriteAspect = (float)sprite.texture.Width / sprite.texture.Height;
            int drawWidth = spriteCell.Width - 2 * (int)(spriteCell.Width * cellPaddingPercentage);
            int drawHeight = (int)(drawWidth / spriteAspect);

            if (drawHeight > spriteCell.Height - 2 * (int)(spriteCell.Height * cellPaddingPercentage))
            {
                drawHeight = spriteCell.Height - 2 * (int)(spriteCell.Height * cellPaddingPercentage);
                drawWidth = (int)(drawHeight * spriteAspect);
            }

            int paddingX = (spriteCell.Width - drawWidth) / 2;
            int paddingY = (spriteCell.Height - drawHeight) / 2;

            Rectangle spriteDest = new Rectangle(
                spriteCell.X + paddingX,
                spriteCell.Y + paddingY,
                drawWidth,
                drawHeight
            );

            spriteBatch.Draw(sprite.texture, spriteDest, Color.White);

            // Cost cell (same as before)
            int costCellHeight = slotRect.Height - spriteCellHeight;
            Rectangle costCell = new Rectangle(slotRect.X, spriteCell.Bottom, slotRect.Width, costCellHeight);

            // Draw cost text centered in cost cell
            string costText = tower.Cost.ToString();
            Vector2 textSize = font.MeasureString(costText);
            float scale = Math.Min((costCell.Width - 4) / textSize.X, (costCell.Height - 2) / textSize.Y);
            Vector2 textPos = new Vector2(
                costCell.X + costCell.Width * 0.5f - textSize.X * 0.5f * scale,
                costCell.Y + costCell.Height * 0.5f - textSize.Y * 0.5f * scale
            );
            spriteBatch.DrawString(font, costText, textPos, Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        private void DrawBackground(SpriteBatch spriteBatch)
        {
            if (slots.Count == 0) return;

            // Tower slot width/height
            int slotWidth = slots[0].Width;
            int slotHeight = slots[0].Height;

            // Sprite + cost cell height
            int spriteCellHeight = (int)(slotHeight * 0.7f);
            int costCellHeight = slotHeight - spriteCellHeight;

            // Total width of tower slots
            int totalTowersWidth = slots.Count * slotWidth + (slots.Count - 1) * spacing;

            // Gold cell width
            int goldWidth = slotSize; // same as full slot
            int totalWidth = totalTowersWidth + spacing + goldWidth;

            // Total height: sprite + cost
            int totalHeight = slotHeight; // already includes sprite + cost
            totalHeight += 2 * backgroundPadding; // padding top+bottom

            // Background position: include padding and gold
            int startX = (int)startPos.X - backgroundPadding - goldWidth - spacing;
            int startY = (int)startPos.Y - backgroundPadding;

            Rectangle backgroundRect = new Rectangle(
                startX,
                startY,
                totalWidth + 2 * backgroundPadding,
                totalHeight
            );

            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, backgroundRect, BackgroundColor);
        }

        private void DrawGold(SpriteBatch spriteBatch)
        {
            int goldCellSize = slotSize; // the coin "cell"
            int spacingY = 2;

            // Coin cell
            Rectangle goldCell = new Rectangle(
                (int)(startPos.X - goldCellSize - spacing),
                (int)startPos.Y,
                goldCellSize,
                goldCellSize
            );

            // Draw the coin higher in its cell
            var coinSprite = SpriteManager.GetSprite("goldCoin");
            int coinPadding = (int)(goldCellSize * 0.1f);
            int coinOffsetUp = 4; // shift coin upward
            Rectangle coinRect = new Rectangle(
                goldCell.X + coinPadding,
                goldCell.Y + coinPadding - coinOffsetUp,
                goldCell.Width - 2 * coinPadding,
                goldCell.Height - 2 * coinPadding - spacingY
            );
            spriteBatch.Draw(coinSprite.texture, coinRect, Color.White);

            // Gold text
            string goldText = Game1.player.Gold.ToString();
            Vector2 textSize = font.MeasureString(goldText);

            // White background rectangle for "note"
            float scale = Math.Min(
                (goldCell.Width - 8) / textSize.X,
                (goldCell.Height / 3f) / textSize.Y
            );

            Vector2 textPos = new Vector2(
                goldCell.X + goldCell.Width * 0.5f - textSize.X * 0.5f * scale,
                coinRect.Bottom + spacingY // further below coin
            );

            Rectangle backgroundRect = new Rectangle(
                (int)(textPos.X - 2),
                (int)(textPos.Y - 2),
                (int)(textSize.X * scale + 4),
                (int)(textSize.Y * scale + 4)
            );

            // Draw white "note" behind text
            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, backgroundRect, new Color(245, 222, 179));

            // Draw the text on top
            spriteBatch.DrawString(font, goldText, textPos, Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
