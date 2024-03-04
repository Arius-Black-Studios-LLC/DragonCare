
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

[System.Serializable]
public class PlayerData
{
    public List<int> unlockedDragonIDs = new List<int>();
    public List<ToDoListItem> toDoListItems = new List<ToDoListItem>();
    public List<journalEntry> journal = new List<journalEntry>();
    public int productivityPoints = 0;
    public int EggPricePerCat = 5;
    public int D_tasks = 0;
    public int R_tasks = 0;
    public int A_tasks = 0;
    public int G_tasks = 0;
    public int O_tasks = 0;
    public int N_tasks = 0;
    public int S_tasks = 0;
    public DateTime LastJournalEntry = DateTime.MinValue;
    public int journalingStreak;


}

public class journalEntry
{
    public string entrytitle;
    public string entryBody;
    public DateTime entryDate;
}


public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    public PlayerData playerData = new PlayerData(); // Use the serializable class
    public int journalingBasePoints = 20;
    public List<GameObject> dragonsInScene = new List<GameObject>();



    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    [ContextMenu("Load")]
    public async Task LoadAllPlayerData()
    {
        try 
        { 
            playerData.toDoListItems = await LoadPlayerData<List<ToDoListItem>>("ToDoListItems");
        }
        catch
        {
            SavePlayerData(playerData.toDoListItems, "ToDoListItems");
        }

        try
        {
            playerData.journal = await LoadPlayerData<List<journalEntry>>("Journal");
        }
        catch
        {
            SavePlayerData(playerData.journal, "Journal");
        }

        try
        {
            playerData.unlockedDragonIDs = await LoadPlayerData<List<int>>("unlockedDragonIDs");
        }
        catch
        {
            SavePlayerData(playerData.unlockedDragonIDs, "unlockedDragonIDs");
        }

        try
        {
            playerData.productivityPoints = await LoadPlayerData<int>("productivityPoints");
        }
        catch
        {
            SavePlayerData(playerData.productivityPoints, "productivityPoints");
        }



        playerData.D_tasks = await LoadPlayerData<int>("D_tasks");
        playerData.R_tasks = await LoadPlayerData<int>("R_tasks");
        playerData.A_tasks = await LoadPlayerData<int>("A_tasks");
        playerData.G_tasks = await LoadPlayerData<int>("G_tasks");
        playerData.O_tasks = await LoadPlayerData<int>("O_tasks");
        playerData.N_tasks = await LoadPlayerData<int>("N_tasks");
        playerData.S_tasks = await LoadPlayerData<int>("S_tasks");

        try
        {
            playerData.LastJournalEntry = await LoadPlayerData<DateTime>("LastJournalEntry");
        }
        catch
        {
            SavePlayerData(playerData.LastJournalEntry, "LastJournalEntry");
        }

        TimeSpan timeSinceLastJournal = DateTime.Now - playerData.LastJournalEntry;
        if( timeSinceLastJournal.Hours <48)
        {
            try
            {
                playerData.journalingStreak = await LoadPlayerData<int>("JournalingStreak");
            }
            catch
            {
                SavePlayerData(playerData.journalingStreak, "JournalingStreak");
            }
        }



        try
        {
            playerData.EggPricePerCat = await LoadPlayerData<int>("EggPricePerCat");
        }
        catch
        {
            SavePlayerData(playerData.EggPricePerCat, "eggPricePerCat");
        }
    }

    [ContextMenu("Save")]
    public async Task SaveAllPlayerData()
    {
        SavePlayerData(playerData.journal, "Journal");
        SavePlayerData(playerData.unlockedDragonIDs, "unlockedDragonIDs");
        SavePlayerData(playerData.toDoListItems, "ToDoListItems");
        SavePlayerData(playerData.journal, "Journal");
        SavePlayerData(playerData.LastJournalEntry, "LastJournalEntry");
        SavePlayerData(playerData.journalingStreak, "JournalingStreak");
        SaveCurencies();

    }




    public void SaveCurencies()
    {
        SavePlayerData(playerData.productivityPoints, "productivityPoints");
        SavePlayerData(playerData.D_tasks, "D_tasks");
        SavePlayerData(playerData.R_tasks, "R_tasks");
        SavePlayerData(playerData.A_tasks, "A_tasks");
        SavePlayerData(playerData.G_tasks, "G_tasks");
        SavePlayerData(playerData.O_tasks, "O_tasks");
        SavePlayerData(playerData.N_tasks, "N_tasks");
        SavePlayerData(playerData.S_tasks, "S_tasks");
        SavePlayerData(playerData.EggPricePerCat, "eggPricePerCat");


    }
    public void IncreaseEggPricePerCat()
    {
        playerData.EggPricePerCat += 2;
    }

    //called from Egg Granter Script when new dragon egg is hatched
    public void AddDragon(int dragonID)
    {
        if (!playerData.unlockedDragonIDs.Contains(dragonID))
        {
            playerData.unlockedDragonIDs.Add(dragonID);
            //save file with new dragon added
            SavePlayerData(playerData.unlockedDragonIDs, "unlockedDragonIDs");
        }
        else
        {
            //grant gems or something, accesories?
        }
    }

    public void AddTask(ToDoListItem item)
    {
        if (!playerData.toDoListItems.Contains(item))
        {
            playerData.toDoListItems.Add(item);
            SavePlayerData<List<ToDoListItem>>(playerData.toDoListItems, "ToDoListItems");
        }
    }


    public void AddNewJournalEntry(journalEntry newReading)
    {
        playerData.LastJournalEntry = newReading.entryDate;
        playerData.journalingStreak++;
        playerData.journal.Add(newReading);
        SavePlayerData<List<journalEntry>>(playerData.journal, "Journal");
    }

    public void GrantPointsForCompleteingTask(ToDoListItem taskItem)
    {
        // You can implement your logic for granting points based on priority
        // For example, you can use a scoring system or other mechanisms
        Debug.Log("Priority: " + taskItem.Priority);
        PlayerManager.instance.playerData.productivityPoints += taskItem.Priority;
        switch (taskItem.taskCategory)
        {
            case TaskCategory.Downtime:
                PlayerManager.instance.playerData.D_tasks++;
                SavePlayerData(playerData.D_tasks, "D_tasks");
                break;
            case TaskCategory.Routine:
                PlayerManager.instance.playerData.R_tasks++;
                SavePlayerData(playerData.R_tasks, "R_tasks");
                break;
            case TaskCategory.Activity:
                PlayerManager.instance.playerData.A_tasks++;
                SavePlayerData(playerData.A_tasks, "A_tasks");
                break;
            case TaskCategory.Growth:
                PlayerManager.instance.playerData.G_tasks++;
                SavePlayerData(playerData.G_tasks, "G_tasks");
                break;
            case TaskCategory.Organize:
                PlayerManager.instance.playerData.O_tasks++;
                SavePlayerData(playerData.O_tasks, "O_tasks");
                break;
            case TaskCategory.Nutrition:
                PlayerManager.instance.playerData.N_tasks++;
                SavePlayerData(playerData.N_tasks, "N_tasks");
                break;
            case TaskCategory.Social:
                PlayerManager.instance.playerData.S_tasks++;
                SavePlayerData(playerData.S_tasks, "S_tasks");
                break;

        }
        SavePlayerData(playerData.productivityPoints, "productivityPoints");
    }

    //called to spawn dragons
    public void SpawnDragonWithBrain(int dragonID,GameObject _Brain)
    {
        GameObject dragonGO = DragonDatabase.instance.getDragonPrefabByID(dragonID);
        if (dragonGO != null)
        {
            Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-10, 10), 5f, UnityEngine.Random.Range(-10, 10));
            GameObject _SpawnedBrain = Instantiate(_Brain, transform.position + randomOffset, Quaternion.identity);
            DragonNPCManager DManager = _SpawnedBrain.GetComponent<DragonNPCManager>();
            DManager.dragonID = dragonID;

            //add a bit of randomness to the spawn point
            Instantiate(dragonGO,_SpawnedBrain.transform);
            DManager.animManager = _SpawnedBrain.GetComponentInChildren<DragonAnimManager>();
            dragonsInScene.Add(_SpawnedBrain);
        }
    }



    //void LoadPlayerData()
    //{
    //    if (File.Exists(Application.persistentDataPath + "/"+saveFilePath))
    //    {
    //        string json = File.ReadAllText(Application.persistentDataPath + "/"+saveFilePath);
    //        Debug.Log("Loaded Dragon IDs: " + json);

    //        // Add this line to check the loaded JSON
    //        Debug.Log("Loaded JSON: " + json);

    //        playerData = JsonUtility.FromJson<PlayerData>(json);
    //    }
    //    else
    //    {
    //        Debug.Log("Save file not found.");
    //    }
    //}

    async Task<T> LoadPlayerData<T>(string key)
    {
        Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { key });

        var dataString = savedData[key];
        Debug.Log("Data loaded: " + dataString);

        T data = JsonConvert.DeserializeObject<T>(dataString);
        return data;
    }

    async void SavePlayerData<T>(T inData, string key)
    {
        string jsonData = JsonConvert.SerializeObject(inData);
        var data = new Dictionary<string, object> { { key, jsonData } };
        Debug.Log("Data saved: " + jsonData);
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
    }

    internal void RemoveTask(ToDoListItem taskItem)
    {
        playerData.toDoListItems.Remove(taskItem);
        SavePlayerData<List<ToDoListItem>>( playerData.toDoListItems, "ToDoListItems");
    }
    internal void RemoveJournalEntry(journalEntry entry)
    {
        playerData.journal.Remove(entry);
        SavePlayerData<List<journalEntry>>(playerData.journal, "Journal");
    }
}
