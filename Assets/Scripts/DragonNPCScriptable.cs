using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(menuName = "DragonCare/Dragon")]
public class DragonNPCScriptable : ScriptableObject
{
    public int DragonID;
    public string DragonName;
    public GameObject DragonObject;
    public DRAGONCategory DragonCategory;

    public int Hat_id = -1;
    public int jacket_id = -1;
    public int pet_id = -1;
}
