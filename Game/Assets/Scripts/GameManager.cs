using System.Collections;
using System.Collections.Generic;

public class GameManager
{
    public void StartGame()
    {
        
    }
    
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }
}
