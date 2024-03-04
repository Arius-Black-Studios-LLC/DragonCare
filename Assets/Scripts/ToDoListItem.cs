using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[System.Serializable]
public class ToDoListItem
{
    public string taskName;
    public int Priority;
    public TaskCategory taskCategory;
    public TaskFrequency taskFrequency;
    public DateTime lastLogged;


}
public enum TaskCategory { Downtime, Routine, Activity, Growth, Organize, Nutrition, Social }
public enum TaskFrequency { Daily, Weekly, BiWeekly, Monthly }
