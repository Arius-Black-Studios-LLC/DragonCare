using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ToDoListManager : MonoBehaviour
{
    [Header("To Do List Item View")]
    public Transform ToDoListScrollRect;
    public GameObject ToDoListItemGO;
    public List<GameObject> buttonsShowing = new List<GameObject>();

    [Header("Add Item View")]
    public TMP_InputField taskNameInputField;
    public Slider prioritySlider;
    public TMP_Dropdown categoryDropdown;
    public TMP_Dropdown frequencyDropdown;




    //to be called when the category buttons are pressed
    #region SHOW TASKS
    public void PopulateToDo_D()
    {
        PopulateToDo_Enum(TaskCategory.Downtime);
    }

    public void PopulateToDo_R()
    {
        PopulateToDo_Enum(TaskCategory.Routine);
    }


    public void PopulateToDo_A()
    {
        PopulateToDo_Enum(TaskCategory.Activity);
    }

    public void PopulateToDo_G()
    {
        PopulateToDo_Enum(TaskCategory.Growth);
    }

    public void PopulateToDo_O()
    {
        PopulateToDo_Enum(TaskCategory.Organize);
    }

    public void PopulateToDo_N()
    {
        PopulateToDo_Enum(TaskCategory.Nutrition);
    }

    public void PopulateToDo_S()
    {
        PopulateToDo_Enum(TaskCategory.Social);
    }

    //helper functions

    private void PopulateToDo_Enum(TaskCategory category)
    {
        ClearToDoListButtons();
        foreach (ToDoListItem item in PlayerManager.instance.playerData.toDoListItems)
        {
            if(item.taskCategory == category)
            {
                //TODO check if the last time a task was done is beyond the amount of days meant to pass between tasks.
                //if the item is the right category AND is up to be done again spawn button
                if(ShouldShowTask(item))
                    SpawnToDoListButton(item);
            }
        }
    }
    private void SpawnToDoListButton(ToDoListItem item)
    {
        //set up and spawn object
        GameObject buttonGO = Instantiate(ToDoListItemGO, ToDoListScrollRect);
        if (buttonGO != null)
        {
            buttonsShowing.Add(buttonGO);
            ToDoItemUI itemUI = buttonGO.GetComponent<ToDoItemUI>();
            itemUI.taskItem = item;
            itemUI.taskName.text = item.taskName;
        }
    }

    private void ClearToDoListButtons()
    {
        // Destroy previously shown buttons
        foreach (GameObject button in buttonsShowing)
        {
            Destroy(button);
        }
        buttonsShowing.Clear();
    }
    private bool ShouldShowTask(ToDoListItem item)
    {
        // Get the current date and time
        DateTime currentDate = DateTime.Now;

        // Calculate the time difference between the current date and the lastLogged date
        TimeSpan timeDifference = currentDate - item.lastLogged;

        // Calculate the difference in days
        int daysSinceLastLogged = (int)timeDifference.TotalDays;

        // Check if the number of days since the last task is greater than or equal to the task frequency
        if (daysSinceLastLogged >= GetFrequencyInDays(item.taskFrequency))
        {
            return true;
        }

        return false;
    }

    private int GetFrequencyInDays(TaskFrequency frequency)
    {
        // Define how many days each frequency represents
        switch (frequency)
        {
            case TaskFrequency.Daily:
                return 1;
            case TaskFrequency.Weekly:
                return 7;
            case TaskFrequency.BiWeekly:
                return 14;
            case TaskFrequency.Monthly:
                return 30; // Assuming an average of 30 days per month for simplicity
            default:
                return 1; // Default to daily if an unsupported frequency is provided
        }
    }
    #endregion


    #region CREATE NEW TASK

    public void AddToDoItem()
    {
        string taskName = taskNameInputField.text;
        int priority = (int)prioritySlider.value;
        TaskCategory category = (TaskCategory)categoryDropdown.value;
        TaskFrequency frequency = (TaskFrequency)frequencyDropdown.value;

        // Create a new to-do item and add it to the list
        ToDoListItem newItem = new ToDoListItem
        {
            taskName = taskName,
            Priority = priority,
            taskCategory = category,
            taskFrequency = frequency,
            lastLogged = DateTime.Now - TimeSpan.FromDays(GetFrequencyInDays(frequency)+1)// You can set the lastLogged date to the current date and time
        };

        PlayerManager.instance.AddTask(newItem);


        // Clear the input fields
        taskNameInputField.text = "";
        prioritySlider.value = 0;
        categoryDropdown.value = 0; // Set the default category
        frequencyDropdown.value = 0; // Set the default frequency
    }

    #endregion

}
