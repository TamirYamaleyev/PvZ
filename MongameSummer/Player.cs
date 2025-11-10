using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MongameSummer;

public class Player 
{
    public int Gold { get; private set; }

    public Player(int startingGold = 500)
    {
        Gold = startingGold;
    }

    public bool SpendGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            return true;
        }

        return false;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }
}