using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MongameSummer;

using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteManager _spriteManager;
    private static List<IDrawable> _drawables = new List<IDrawable>();
    private static List<IUpdateable> _updateables= new List<IUpdateable>();

    public static int ScreenWidth = 1920;
    public static int ScreenHeight = 1080;

    public static Player player;

    public static bool IsGameOver = false;
    string gameOverText = "GAME OVER";

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        player = new Player(150);

        ScreenCenterWidth = GraphicsDevice.Viewport.Width * 0.5f;
        ScreenCenterHeight = GraphicsDevice.Viewport.Height * 0.5f;
        
        base.Initialize();
    }

    public static SpriteFont oswaldFont;
    
    public static float ScreenCenterWidth;
    public static float ScreenCenterHeight;

    public static SoundEffect EnemyDeathSound;
    public static SoundEffect ArrowHitSound;
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spriteManager = new SpriteManager(Content);
        
        SpriteManager.AddSprite("pixel", "Images/pixel");
        SpriteManager.AddSprite("defaultTower", "Images/Towers/Tower", 6, 1);
        SpriteManager.AddSprite("defaultTowerPreview", "Images/Towers/TowerThumbnail");
        SpriteManager.AddSprite("goldTowerPreview", "Images/Towers/GoldTowerThumbnail");
        SpriteManager.AddSprite("bombConsumablePreview", "Images/Consumables/BombConsumablePreview");
        SpriteManager.AddSprite("bombConsumable", "Images/Consumables/BombConsumable", 3, 1);
        SpriteManager.AddSprite("bombExplosion", "Images/Consumables/Explosion", 6, 1);
        SpriteManager.AddSprite("arrow", "Images/Arrow");
        SpriteManager.AddSprite("goldCoin", "Images/gold");
        SpriteManager.AddSprite("goldTower", "Images/Towers/GoldTower", 4, 1);
        SpriteManager.AddSprite("goblinEnemy", "Images/Enemies/Goblin", 6, 1);
        SpriteManager.AddSprite("slimeEnemy", "Images/Enemies/Slime", 6, 1);
        SpriteManager.AddSprite("houndEnemy", "Images/Enemies/Hound", 6, 1);

        oswaldFont = Content.Load<SpriteFont>("Fonts/Oswald");

        EnemyDeathSound = Content.Load<SoundEffect>("Audio/EnemyDeath");
        ArrowHitSound = Content.Load<SoundEffect>("Audio/ArrowHit");


        var gridScene = SceneManager.Create<GridScene>();

        //SpriteManager.AddSprite("egret", "Images/Birds/Bird3_Egret4", 4, 4);
        //SpriteManager.AddSprite("duck", "Images/Birds/Bird2 Duck_1", 4, 4);

        Content.Load<Texture2D>("Images/logo");
        Content.Load<Texture2D>("Images/pong-atlas");
        
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        if (!IsGameOver)
            SceneManager.Instance.Update(gameTime);

        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(30, 94, 10));

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
        SceneManager.Instance.Draw(_spriteBatch);

        if (IsGameOver)
        {
            Vector2 size = oswaldFont.MeasureString(gameOverText);
            Vector2 pos = new Vector2(ScreenCenterWidth - size.X * 0.5f, ScreenCenterHeight - size.Y * 0.5f);

            _spriteBatch.DrawString(oswaldFont, gameOverText, pos, Color.White);
        }
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}