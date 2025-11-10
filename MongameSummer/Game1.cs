using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MongameSummer;
using System.Collections.Generic;
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteManager _spriteManager;
    private static List<IDrawable> _drawables = new List<IDrawable>();
    private static List<IUpdateable> _updateables= new List<IUpdateable>();
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        ScreenCenterWidth = GraphicsDevice.Viewport.Width * 0.5f;
        ScreenCenterHeight = GraphicsDevice.Viewport.Height * 0.5f;
        
        base.Initialize();
    }

    private Player pacman;
    private Enemy enemy;
    
    private SpriteFont oswaldFont;
    
    public static float ScreenCenterWidth;
    public static float ScreenCenterHeight;
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spriteManager = new SpriteManager(Content);

        var gridScene = SceneManager.Create<GridScene>();
        
        //SpriteManager.AddSprite("player", "Images/pacman");
        SpriteManager.AddSprite("pixel", "Images/pixel");

        
        //SpriteManager.AddSprite("egret", "Images/Birds/Bird3_Egret4", 4, 4);
        //SpriteManager.AddSprite("duck", "Images/Birds/Bird2 Duck_1", 4, 4);

        //pacman = SceneManager.Create<Player>();
        //pacman.Play();
        
        //enemy = SceneManager.Create<Enemy>();
        //enemy.Play();
        
        //enemy.position = new Vector2(Game1.ScreenCenterWidth + 300, Game1.ScreenCenterHeight + 300);
        //enemy.scale = new Vector2(0.2f, 0.2f);
       
        //enemy.LinkWithPlayer(pacman);
        
        Content.Load<Texture2D>("Images/logo");
        Content.Load<Texture2D>("Images/pong-atlas");

        //pacman.position = new Vector2(Game1.ScreenCenterWidth, Game1.ScreenCenterHeight);
        //pacman.scale = new Vector2(0.2f, 0.2f);
        
        oswaldFont = Content.Load<SpriteFont>("Fonts/Oswald");
        
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here


        SceneManager.Instance.Update(gameTime);

        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkRed);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
       
        SceneManager.Instance.Draw(_spriteBatch);
        

        //string text = "Hello Monogame!";
        //_spriteBatch.DrawString(
        //    oswaldFont,
        //    text,
        //    new Vector2(ScreenCenterWidth, 50.0f),
        //    Color.White,
        //    MathHelper.ToRadians(0.0f),
        //    new Vector2(oswaldFont.MeasureString(text).X * 0.5f, oswaldFont.MeasureString(text).Y * 0.5f),
        //    1.0f,
        //    SpriteEffects.None,
        //    0
        //    );
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}