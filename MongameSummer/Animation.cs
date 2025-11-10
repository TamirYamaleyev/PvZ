using Microsoft.Xna.Framework;

namespace MongameSummer;

public class Animation : Sprite
{
    private double frameCounter = 0;
    private int fps = 60;
    private int x = 0, y = 0;
    private bool isLooping = true;
    private bool isAnimating = false;
    public Animation(string spriteName) : base(spriteName)
    {
        Reset();

        sourceRectangle = _spritesheet[0, 0];

        _origin = new Vector2(sourceRectangle.Value.Width * 0.5f, sourceRectangle.Value.Height * 0.5f);
    }

    public void Play(bool IsLooping = true, int fps = 60)
    {
        isAnimating = true;
        this.fps = fps;
        this.isLooping = IsLooping;

        x = 0;
        y = 0;
        sourceRectangle = _spritesheet[x, y];
        frameCounter = 0;
    }

    public void Pause()
    {
        isAnimating = false;
    }
    
    public void Resume()
    {
        isAnimating = true;
    }
    
    public void Stop()
    {
        Reset();
    }

    void Reset()
    {
        isAnimating = false;
        x = 0;
        y = 0;
        sourceRectangle = _spritesheet[x, y];
        frameCounter = 0;
    }

    bool CanMoveNextFrame(GameTime gameTime)
    {
        double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
        frameCounter += deltaTime;

        if (frameCounter > (1.0f / this.fps))
            return true;
        
        return false;
    }

    void MoveNextFrame()
    {
        frameCounter = 0;
        
        x++;
        if (x == _spritesheet.columns)
        {
            y++;
            x = 0;
            if (y == _spritesheet.rows)
            {
                if (isLooping)
                {
                    x = 0;
                    y = 0;
                }
            }
        }

        sourceRectangle = _spritesheet[x, y];

        _origin = new Vector2(sourceRectangle.Value.Width * 0.5f, sourceRectangle.Value.Height * 0.5f);
    }

    public override void Update(GameTime gameTime)
    {
        if (isAnimating)
        {
            if (CanMoveNextFrame((gameTime)))
                MoveNextFrame();
        }

        base.Update(gameTime);
                
        Rectangle nonNullRect = sourceRectangle ?? new Rectangle(0, 0, 0, 0);
        DestRectangle = GetDestRectangle(nonNullRect);
    } 
}