
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
    public List<DragonSaveSettings> unlockedDragons = new List<DragonSaveSettings>();
    public List<int> unlockedAccesoryIDs = new List<int>();
    public List<ToDoListItem> toDoListItems = new List<ToDoListItem>();
    public List<journalEntry> journal = new List<journalEntry>();
    public int productivityPoints = 0;

    public int credits = 0;
    public int EggPrice = 100;
    public int AccesoryPrice = 50;
    public DateTime LastJournalEntry = DateTime.MinValue;
    public int journalingStreak;


}

public class journalEntry
{
    public string entrytitle;
    public string entryBody;
    public DateTime entryDate;
}
[System.Serializable]
public class DragonSaveSettings
{
    public int dragon_id;
    public int Hat_id = -1;
    public int jacket_id = -1;
    public int pet_id = -1;
}


public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    public PlayerData playerData = new PlayerData(); // Use the serializable class
    public int journalingBasePoints = 20;

    public DragonNPCManager dragonInFocus;
    public ShopUIManger shopUIManger;
    public int productivityPoints_multiplyer = 1;




    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            shopUIManger =FindObjectOfType<ShopUIManger>();
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
            playerData.unlockedDragons = await LoadPlayerData<List<DragonSaveSettings>>("unlockedDragons");
        }
        catch
        {
            SavePlayerData(playerData.unlockedDragons, "unlockedDragons");
        }
        try
        {
            playerData.unlockedAccesoryIDs = await LoadPlayerData<List<int>>("unlockedAccesoryIds");
        }
        catch
        {
            SavePlayerData(playerData.unlockedAccesoryIDs, "unlockedAccesoryIds");
        }

        try
        {
            playerData.productivityPoints = await LoadPlayerData<int>("productivityPoints");
        }
        catch
        {
            SavePlayerData(playerData.productivityPoints, "productivityPoints");
        }

        try
        {
            playerData.credits = await LoadPlayerData<int>("Credits");
        }
        catch
        {
            SavePlayerData(playerData.credits, "Credits");
        }

        try
        {
            playerData.AccesoryPrice = await LoadPlayerData<int>("AccesoryPrice");
        }
        catch
        {
            SavePlayerData(playerData.AccesoryPrice, "AccesoryPrice");
        }



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
            playerData.EggPrice = await LoadPlayerData<int>("EggPrice");
        }
        catch
        {
            SavePlayerData(playerData.EggPrice, "EggPrice");
        }
    }




    #region Save Stuff

    public void SaveDragonChanges(DragonNPCScriptable dragonNPCScriptable)
    {
        foreach (DragonSaveSettings dragon in playerData.unlockedDragons)
        {
            if (dragon.dragon_id == dragonNPCScriptable.DragonID)
            {
                dragon.Hat_id = dragonNPCScriptable.Hat_id;
                dragon.pet_id = dragonNPCScriptable.pet_id;
                SavePlayerData(playerData.unlockedDragons, "unlockedDragons");
                return;

            }
        }
    }
    public void SaveCurencies()
    {
        SavePlayerData(playerData.productivityPoints, "productivityPoints");
        SavePlayerData(playerData.credits, "Credits");
        SavePlayerData(playerData.EggPrice, "EggPrice");
        SavePlayerData(playerData.AccesoryPrice, "AccesoryPrice");


    }

    [ContextMenu("Save")]
    public async Task SaveAllPlayerData()
    {
        SavePlayerData(playerData.journal, "Journal");
        SavePlayerData(playerData.unlockedDragons, "unlockedDragons");
        SavePlayerData(playerData.unlockedAccesoryIDs, "unlockedAccesoryIds");
        SavePlayerData(playerData.toDoListItems, "ToDoListItems");
        SavePlayerData(playerData.journal, "Journal");
        SavePlayerData(playerData.LastJournalEntry, "LastJournalEntry");
        SavePlayerData(playerData.journalingStreak, "JournalingStreak");
        SaveCurencies();

    }
    #endregion


    #region Update Player Data
    //Tasks
    public void AddTask(ToDoListItem item)
    {
        if (!playerData.toDoListItems.Contains(item))
        {
            playerData.toDoListItems.Add(item);
            SavePlayerData<List<ToDoListItem>>(playerData.toDoListItems, "ToDoListItems");
        }
    }
    internal void RemoveTask(ToDoListItem taskItem)
    {
        playerData.toDoListItems.Remove(taskItem);
        SavePlayerData<List<ToDoListItem>>(playerData.toDoListItems, "ToDoListItems");
    }

    public void SaveTaskChanges()
    {
        SavePlayerData(playerData.toDoListItems, "ToDoListItems");
    }
    public void GrantPointsForCompleteingTask(ToDoListItem taskItem)
    {
        // You can implement your logic for granting points based on priority
        // For example, you can use a scoring system or other mechanisms
        Debug.Log("Priority: " + taskItem.Priority);
        PlayerManager.instance.playerData.productivityPoints += (taskItem.Priority *(productivityPoints_multiplyer +playerData.unlockedDragons.Count));
        
        SavePlayerData(playerData.productivityPoints, "productivityPoints");
    }


    //Currencies
    public void IncreaseEggPricePerCat()
    {
        playerData.EggPrice *= 2;
    }



    #endregion


   
    public void AddRandomDragon(bool freeEgg = false)
    {
        int dragonID =DragonDatabase.instance.dragons[UnityEngine.Random.Range(0, DragonDatabase.instance.dragons.Length)].DragonID;

        DragonSaveSettings newDragon = new DragonSaveSettings { dragon_id = dragonID };
            playerData.unlockedDragons.Add(newDragon);

            NPCDragonSpawner.instance.SpawnDragonWithBrain(newDragon);
            SavePlayerData(playerData.unlockedDragons, "unlockedDragons");


            if(shopUIManger == null)
            {
                shopUIManger =FindObjectOfType<ShopUIManger>();
            }
            shopUIManger.ShowConfirmationWindow("Congrat's, you")
            //PAY FOR EGG
            if (!freeEgg)
            {
                playerData.productivityPoints -= playerData.EggPrice;

               

                instance.IncreaseEggPricePerCat();
                instance.SaveCurencies();

            }


    }

    //called on special events that grant specific dragon for free or what ever
    public void AddEventDragon(int id)
    {
        DragonSaveSettings newDragon = new DragonSaveSettings { dragon_id = id };
        playerData.unlockedDragons.Add(newDragon);

        NPCDragonSpawner.instance.SpawnDragonWithBrain(newDragon);
        SavePlayerData(playerData.unlockedDragons, "unlockedDragons");
    }


    public void AddAccessories(bool freeAccesory=false)
    {
       DragonAccesory newAccesory = DragonDatabase.instance.accesories[UnityEngine.Random.Range(0,DragonDatabase.instance.accesories.Length)];
        playerData.unlockedAccesoryIDs.Add(newAccesory.accesory_id);

        if (!freeAccesory)
        {
            playerData.productivityPoints -= playerData.AccesoryPrice;
        }
        SavePlayerData(playerData.unlockedAccesoryIDs, "unlockedAccesoryIds");
        SavePlayerData(playerData.AccesoryPrice, "AccesoryPrice");

    }
   
    public void AddNewJournalEntry(journalEntry newReading)
    {
        playerData.LastJournalEntry = newReading.entryDate;
        playerData.journalingStreak++;
        playerData.journal.Add(newReading);
        SavePlayerData<List<journalEntry>>(playerData.journal, "Journal");
    }

    
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
   
    internal void RemoveJournalEntry(journalEntry entry)
    {
        playerData.journal.Remove(entry);
        SavePlayerData<List<journalEntry>>(playerData.journal, "Journal");
    }
}
