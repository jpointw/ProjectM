using System;
using UnityEngine;
using static Define;

[Serializable]
public class GameData
{
    public int Coin;
    public int Gem;
    
    public int[] MiningToolLevel = new int[MININGTOOL_COUNT];
    public int[] MiningToolExp = new int[MININGTOOL_COUNT];

    public int MinerCount = 0;
    
    public int CurrentMiningToolID;
    
    public bool BGMOn = true;
    public bool EffectSoundOn = true;
}

public class DataSystem
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    const string SAVE_KEY = "GameData";

    public void Init()
    {
        if (LoadGameData()) return;
    }
    
    public void SaveGameData()
    {
        ES3.Save(SAVE_KEY, _gameData);
        Debug.Log("Game data saved successfully.");
    }
    
    public bool LoadGameData()
    {
        if (ES3.KeyExists(SAVE_KEY))
        {
            _gameData = ES3.Load<GameData>(SAVE_KEY);
            Debug.Log("Game data loaded successfully.");
            return false;
        }

        Debug.Log("No saved game data found. Using default values.");
        return false;
    }
}