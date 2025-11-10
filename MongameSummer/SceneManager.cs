using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MongameSummer;

public class SceneManager : IDrawable
{
    private static List<IUpdateable> updatables = new List<IUpdateable>();
    private static List<IDrawable> drawables = new List<IDrawable>();

    private static SceneManager _instance = null;

    public static SceneManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SceneManager();
            
            return _instance;
        }
    }
    
    public static T Create<T>() where T : IUpdateable, new()
    {
        T obj = new T();
        Add(obj);
        return obj;
    }

    public static void Add<T>(T obj) where T : IUpdateable
    {
        updatables.Add(obj);
        
        if (obj is IDrawable drawable) 
            drawables.Add(drawable);
    }
    
    
    public static void Remove<T>(T obj) where T : IUpdateable
    {
        // check if exist before delete
        updatables.Remove(obj);
        
        if (obj is IDrawable drawable) 
            drawables.Remove(drawable);
    }

    public void Update(GameTime gameTime)
    {
        for (int i = 0; i < updatables.Count; i++)
            updatables[i].Update(gameTime);
        
       /* foreach (var UpdateVar in updatables)
        {
            UpdateVar.Update(gameTime);
        }*/
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var DrawVar in drawables)
        {
            DrawVar.Draw(spriteBatch);
        }
    }

    public IEnumerable<IUpdateable> GetAllUpdatables()
    {
        return updatables;
    }
}