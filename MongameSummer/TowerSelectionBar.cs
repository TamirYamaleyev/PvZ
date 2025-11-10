using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MongameSummer
{
    internal class TowerSelectionBar : IDrawable, IUpdateable
    {
        private List<string> towerNames;
        private List<Rectangle> slots;
        private int slotSize = 80;
        private int spacing = 10;
        private Vector2 startPos = new Vector2(10, 10);

        Color defaultColor = Color.Gray;
        Color selectedColor = Color.Yellow;

        float paddingPercentage = 0.1f;

        public string SelectedTower { get; private set; }
        private int selectedIndex = -1;

        public TowerSelectionBar(List<string> towerNames)
        {
            this.towerNames = towerNames;
            slots = new List<Rectangle>();

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
                        SelectedTower = towerNames[i];
                        break;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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
            spriteBatch.Draw(SpriteManager.GetSprite("pixel").texture, slotRect, bgColor);
        }

        private void DrawTowerSprite(SpriteBatch spriteBatch, int index)
        {
            var slotRect = slots[index];
            var sprite = SpriteManager.GetSprite(towerNames[index]);

            int paddingX = (int)(slotRect.Width * paddingPercentage);
            int paddingY = (int)(slotRect.Height * paddingPercentage);

            Rectangle dest = new Rectangle(
            slotRect.X + paddingX,
            slotRect.Y + paddingY,
            slotRect.Width - 2 * paddingX,
            slotRect.Height - 2 * paddingY
            );

            spriteBatch.Draw(sprite.texture, dest, Color.White);
        }

    }
}
