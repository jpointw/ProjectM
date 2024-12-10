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
    public Action<int> OnGoldUpdated;
    public Action<int> OnGreenGemUpdated;
    public Action<int> OnRedGemUpdated;
    public Action<int,int> OnItemAmountUpdated;
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
    public void UpdateLevel(int value)
    {
        _gameData.gold += value;
        SaveGameData();
        OnGoldUpdated?.Invoke(value);
    }
    
    public void UpdateExp(int value)
    {
        _gameData.gold += value;
        SaveGameData();
        OnGoldUpdated?.Invoke(value);
    }
    
    public void UpdateGold(int value)
    {
        _gameData.gold += value;
        SaveGameData();
        OnGoldUpdated?.Invoke(value);
    }
    
    public void UpdateGreenGem(int value)
    {
        _gameData.greenGem += value;
        SaveGameData();
        OnGreenGemUpdated?.Invoke(value);
    }
    
    public void UpdateRedGem(int value)
    {
        _gameData.redGem += value;
        SaveGameData();
        OnRedGemUpdated?.Invoke(value);
    }

    public void UpdateItemAmout(int itemId, int value)
    {
        _gameData.itemAmount[itemId] += value;
        SaveGameData();
        OnItemAmountUpdated?.Invoke(itemId, value);
    }
    
    
    public void UpdateCurrentMinerCount(int value)
    {
        _gameData.minerCount += value;
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
        _gameData.mine += value;
        SaveGameData();
        OnMineValueUpdated?.Invoke(mineIndex, value);
    }
    //
    // public int UpdateLevel(int value)
    // {
    //     _gameData.coin = value;
    //     SaveGameData();
    //     OnCoinUpdated?.Invoke(value);
    // }
    //
    // public int UpdateExp(int value)
    // {
    //     _gameData.coin = value;
    //     SaveGameData();
    //     OnCoinUpdated?.Invoke(value);
    // }
    //
    // public int UpdateCoin(int value)
    // {
    //     _gameData.coin = value;
    //     SaveGameData();
    //     OnCoinUpdated?.Invoke(value);
    // }
    //
    // public int UpdateGreenGem(int value)
    // {
    //     _gameData.greenGem = value;
    //     SaveGameData();
    //     OnGreenGemUpdated?.Invoke(value);
    // }
    //
    // public int UpdateRedGem(int value)
    // {
    //     _gameData.redGem = value;
    //     SaveGameData();
    //     OnRedGemUpdated?.Invoke(value);
    // }
    //
    //
    // public int UpdateCurrentMinerCount(int value)
    // {
    //     _gameData.minerCount = value;
    //     SaveGameData();
    //     OnMinerCountUpdated?.Invoke(value);
    // }
    //
    // public bool UpdateBGMState(bool state)
    // {
    //     _gameData.BGMOn = state;
    //     SaveGameData();
    //     OnBGMStateChanged?.Invoke(state);
    // }
    // public (int,int) UpdateMineValue(int mineIndex, int value)
    // {
    //     _gameData.mine = value;
    //     SaveGameData();
    //     OnMineValueUpdated?.Invoke(mineIndex, value);
    // }
    
    
}