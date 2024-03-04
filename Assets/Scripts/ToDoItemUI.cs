using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ToDoItemUI : MonoBehaviour
{
    public TMP_Text taskName;

    public ToDoListItem taskItem;

    public void LogTaskAsComplete()
    {
        // Find the taskItem in PlayerManager's list of task items
        // and update its lastLogged date to today
        UpdateLastLoggedDate();

        // Grant the player points according to priority
        PlayerManager.instance.GrantPointsForCompleteingTask(taskItem);

        // Remove this button from the ToDoListManager's list
        RemoveFromToDoList();

        // Destroy this button's GameObject
        Destroy(gameObject);
    }

    public void RemoveTaskForever()
    {
        PlayerManager.instance.RemoveTask(taskItem);

        Destroy(gameObject);
    }

    private void UpdateLastLoggedDate()
    {
        // Set the taskItem's lastLogged date to the current date (today)
        taskItem.lastLogged = DateTime.Now;
    }



    private void RemoveFromToDoList()
    {
        // Get a reference to the ToDoListManager
        ToDoListManager listManager = FindObjectOfType<ToDoListManager>();

        if (listManager != null)
        {
            // Remove this button from the list of displayed buttons in ToDoListManager
            listManager.buttonsShowing.Remove(gameObject);
        }
    }
}
