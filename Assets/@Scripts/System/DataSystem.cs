using System;
using UnityEngine;
using static Define;

public class DataSystem
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } private set { _gameData = value; } }

    const string SAVE_KEY = "GameData";
    
    
    /// <summary>
    /// DataChange Detect
    /// </summary>
    public Action<int> OnCoinUpdated;
    public Action<int> OnMinerCountUpdated;
    public Action<bool> OnBGMStateChanged;
    public Action<int, int> OnMineValueUpdated;

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
    
    public void UpdateCoin(int value)
    {
        _gameData.Coin = value;
        SaveGameData();
        OnCoinUpdated?.Invoke(value);
    }

    public void UpdateCurrentMinerCount(int value)
    {
        _gameData.MinerCount = value;
        SaveGameData();
        OnMinerCountUpdated?.Invoke(value);
    }

    public void UpdateBGMState(bool state)
    {
        _gameData.BGMOn = state;
        SaveGameData();
        OnBGMStateChanged?.Invoke(state);
    }
    public void UpdateMineValue(int mineIndex, int value)
    {
        _gameData.Mine = value;
        SaveGameData();
        OnMineValueUpdated?.Invoke(mineIndex, value);
    }
    
    
}