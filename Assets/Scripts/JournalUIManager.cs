
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;




public class JournalUIManager : MonoBehaviour
{


    
    [Header("New Entry")]
    public GameObject newEntryGO;
    public TMP_InputField new_Title;
    public TMP_InputField new_Body;

    [Header("Old Entry")]
    public GameObject oldEntryGO;
    public TMP_Text old_Title;
    public TMP_Text old_Body;
    public TMP_Text old_Date;

    public GameObject journalButtonGO;
    public Transform journalButtonGroup;
    public List<GameObject> buttonsShowing = new List<GameObject>();



    #region Create New Entry

    public void LogJournalEntry()
    {
        journalEntry entry = new journalEntry();
        entry.entryDate = DateTime.Now;

        entry.entrytitle = new_Title.text;

        entry.entryBody = new_Body.text;
        PlayerManager.instance.playerData.productivityPoints += getProdPointsFromjournal(entry);
        PlayerManager.instance.AddNewJournalEntry(entry);
        PlayerManager.instance.SaveCurencies();

    }

    private int getProdPointsFromjournal(journalEntry item)
    {


       if(PlayerManager.instance.playerData.LastJournalEntry.DayOfYear == item.entryDate.DayOfYear &&
            PlayerManager.instance.playerData.LastJournalEntry.Year == item.entryDate.Year)
        {
            return 0;
        }
        else
        {
            return PlayerManager.instance.journalingBasePoints * (PlayerManager.instance.playerData.journalingStreak + 1);
        }
    }
    #endregion

    #region show old entries
    public void LoadOldEntries() //called when opening journal page
    {
        ClearJournalButtons();
        foreach (journalEntry item in PlayerManager.instance.playerData.journal)
        {
            SpawnJournalButton(item);
            
        }
    }

    public void ShowOldEntry(journalEntry oldEntry) //calleed when clicking on journal entry

    {

        newEntryGO.SetActive(false);
        oldEntryGO.SetActive(true);
        old_Title.text = oldEntry.entrytitle;

        old_Body.text = oldEntry.entryBody;
        old_Date.text = oldEntry.entryDate.Month.ToString() + "-" + oldEntry.entryDate.Day.ToString()+"-"+ oldEntry.entryDate.Year.ToString();
    }
    private void SpawnJournalButton(journalEntry item)
    {
        //set up and spawn object
        GameObject buttonGO = Instantiate(journalButtonGO, journalButtonGroup);
        if (buttonGO != null)
        {
            buttonsShowing.Add(buttonGO);
            JournalEntryButtonUI itemUI = buttonGO.GetComponent<JournalEntryButtonUI>();
            itemUI.entry = item;
            itemUI.journalTitle.text = item.entrytitle;
        }
    }

    private void ClearJournalButtons()
    {
        // Destroy previously shown buttons
        foreach (GameObject button in buttonsShowing)
        {
            Destroy(button);
        }
        buttonsShowing.Clear();
    }


    #endregion
}
