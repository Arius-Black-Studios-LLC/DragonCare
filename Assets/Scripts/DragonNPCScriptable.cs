using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum DRAGONCategory { Any, D, R, A, G, O, N, S }
[CreateAssetMenu(menuName = "DragonCare/Dragon")]

public class DragonNPCScriptable : ScriptableObject
{
    public int DragonID;
    public string DragonName;
    public GameObject DragonObject;


    public int Hat_id = -1;
    public int Backpack_id = -1;
    public int pet_id = -1;
    public int holding_id = -1;
    public int Rideable_id;
}
