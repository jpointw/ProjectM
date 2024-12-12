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
    public Action<int,int> OnItemUpdated;
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
        _gameData.level += value;
        SaveGameData();
        OnGoldUpdated?.Invoke(_gameData.level);
    }
    
    public void UpdateExp(int value)
    {
        _gameData.exp += value;
        SaveGameData();
        OnGoldUpdated?.Invoke(_gameData.exp);
    }
    
    public void UpdateGold(int value)
    {
        _gameData.gold += value;
        SaveGameData();
        OnGoldUpdated?.Invoke(_gameData.gold);
    }
    
    public void UpdateGreenGem(int value)
    {
        _gameData.greenGem += value;
        SaveGameData();
        OnGreenGemUpdated?.Invoke(_gameData.greenGem);
    }
    
    public void UpdateRedGem(int value)
    {
        _gameData.redGem += value;
        SaveGameData();
        OnRedGemUpdated?.Invoke(_gameData.redGem);
    }

    public int UpdateItemAmout(int itemId, int value)
    {
        _gameData.itemAmount[itemId] += value;
        SaveGameData();
        OnItemAmountUpdated?.Invoke(itemId, _gameData.itemAmount[itemId]);
        return _gameData.itemAmount[itemId];
    }

    public int UpdateMiningTool(int itemId, int value)
    {
        _gameData.miningToolLevel[itemId] += value;
        OnItemUpdated?.Invoke(itemId, _gameData.miningToolLevel[itemId]);
        return 0;
    }
    
    
    public void UpdateCurrentMinerCount(int value)
    {
        _gameData.minerCount += value;
        SaveGameData();
        OnMinerCountUpdated?.Invoke(_gameData.minerCount);
    }
    
    public void UpdateBGMState(bool state)
    {
        _gameData.BGMOn = state;
        SaveGameData();
        OnBGMStateChanged?.Invoke(_gameData.BGMOn);
    }
    public void UpdateMineValue(int mineIndex, int value)
    {
        _gameData.mine += value;
        SaveGameData();
        OnMineValueUpdated?.Invoke(mineIndex, _gameData.mine);
    }
}