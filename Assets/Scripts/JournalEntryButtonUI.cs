using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JournalEntryButtonUI : MonoBehaviour
{
    public TMP_Text journalTitle;
    public journalEntry entry;
    public JournalUIManager journalUIManager;
    private void Awake()
    {
        journalUIManager = GetComponentInParent<JournalUIManager>();
    }



    public void RemoveTaskForever()
    {
        PlayerManager.instance.RemoveJournalEntry(entry);
        Destroy(gameObject);
    }

 

    public void openJournalEntry()
    {
        journalUIManager.ShowOldEntry(entry);
    }
}
