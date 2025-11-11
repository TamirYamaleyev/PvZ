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

            towers = new List<Tower>();

            foreach (var name in towerNames)
            {
                var tower = TowerFactory.CreateTower(name);
                if (tower != null)
                {
                    // Ensure each tower has a dedicated preview sprite name
                    if (string.IsNullOrEmpty(tower.PreviewSpriteName))
                        tower.PreviewSpriteName = tower.SpriteName; // fallback to full sprite if not set

                    towers.Add(tower);
                }
            }

            slots = new List<Rectangle>();
            int totalWidth = towers.Count * slotSize + (towers.Count - 1) * spacing;
            float startX = Game1.ScreenCenterWidth - totalWidth * 0.5f;

            startPos = new Vector2(startX, backgroundPadding + topPadding);

            for (int i = 0; i < towers.Count; i++)
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
                DrawTowerPreview(spriteBatch, i);
            }
        }

        private void DrawSlotBackground(SpriteBatch spriteBatch, int index)
        {
            var slotRect = slots[index];
            Color bgColor = (index == selectedIndex) ? selectedColor : defaultColor;

            // Sprite cell (top)
            int spriteCellHeight = (int)(slotRect.Height * 0.7f);
            Rectangle spriteCell = new Rectangle(slotRect.X, slotRect.Y, slotRect.Width, spriteCellHeight);
            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, spriteCell, bgColor);

            // Cost cell (bottom)
            int costCellHeight = slotRect.Height - spriteCellHeight;
            Rectangle costCell = new Rectangle(slotRect.X, spriteCell.Bottom, slotRect.Width, costCellHeight);
            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, costCell, Color.DarkGray);
        }

        private void DrawTowerPreview(SpriteBatch spriteBatch, int index)
        {
            var slotRect = slots[index];
            var tower = towers[index];

            var previewSprite = SpriteManager.GetSprite(tower.PreviewSpriteName);

            int spriteCellHeight = (int)(slotRect.Height * 0.7f);
            Rectangle spriteCell = new Rectangle(slotRect.X, slotRect.Y, slotRect.Width, spriteCellHeight);

            // Maintain aspect ratio
            float spriteAspect = (float)previewSprite.texture.Width / previewSprite.texture.Height;
            int drawWidth = spriteCell.Width - 2 * (int)(spriteCell.Width * cellPaddingPercentage);
            int drawHeight = (int)(drawWidth / spriteAspect);

            if (drawHeight > spriteCell.Height - 2 * (int)(spriteCell.Height * cellPaddingPercentage))
            {
                drawHeight = spriteCell.Height - 2 * (int)(spriteCell.Height * cellPaddingPercentage);
                drawWidth = (int)(drawHeight * spriteAspect);
            }

            int paddingX = (spriteCell.Width - drawWidth) / 2;
            int paddingY = (spriteCell.Height - drawHeight) / 2;

            Rectangle dest = new Rectangle(
                spriteCell.X + paddingX,
                spriteCell.Y + paddingY,
                drawWidth,
                drawHeight
            );

            spriteBatch.Draw(previewSprite.texture, dest, Color.White);

            // Draw cost
            int costCellHeight = slotRect.Height - spriteCellHeight;
            Rectangle costCell = new Rectangle(slotRect.X, spriteCell.Bottom, slotRect.Width, costCellHeight);

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

            int slotWidth = slots[0].Width;
            int slotHeight = slots[0].Height;
            int totalTowersWidth = slots.Count * slotWidth + (slots.Count - 1) * spacing;
            int goldWidth = slotSize;
            int totalWidth = totalTowersWidth + spacing + goldWidth;
            int totalHeight = slotHeight + 2 * backgroundPadding;
            int startX = (int)startPos.X - backgroundPadding - goldWidth - spacing;
            int startY = (int)startPos.Y - backgroundPadding;

            Rectangle backgroundRect = new Rectangle(startX, startY, totalWidth + 2 * backgroundPadding, totalHeight);
            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, backgroundRect, BackgroundColor);
        }

        private void DrawGold(SpriteBatch spriteBatch)
        {
            int goldCellSize = slotSize;
            int spacingY = 2;

            Rectangle goldCell = new Rectangle(
                (int)(startPos.X - goldCellSize - spacing),
                (int)startPos.Y,
                goldCellSize,
                goldCellSize
            );

            var coinSprite = SpriteManager.GetSprite("goldCoin");
            int coinPadding = (int)(goldCellSize * 0.1f);
            int coinOffsetUp = 4;
            Rectangle coinRect = new Rectangle(
                goldCell.X + coinPadding,
                goldCell.Y + coinPadding - coinOffsetUp,
                goldCell.Width - 2 * coinPadding,
                goldCell.Height - 2 * coinPadding - spacingY
            );
            spriteBatch.Draw(coinSprite.texture, coinRect, Color.White);

            string goldText = Game1.player.Gold.ToString();
            Vector2 textSize = font.MeasureString(goldText);
            float scale = Math.Min((goldCell.Width - 8) / textSize.X, (goldCell.Height / 3f) / textSize.Y);
            Vector2 textPos = new Vector2(
                goldCell.X + goldCell.Width * 0.5f - textSize.X * 0.5f * scale,
                coinRect.Bottom + spacingY
            );

            Rectangle backgroundRect = new Rectangle(
                (int)(textPos.X - 2),
                (int)(textPos.Y - 2),
                (int)(textSize.X * scale + 4),
                (int)(textSize.Y * scale + 4)
            );

            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, backgroundRect, new Color(245, 222, 179));
            spriteBatch.DrawString(font, goldText, textPos, Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
