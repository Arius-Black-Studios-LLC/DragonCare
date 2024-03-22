using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum AccesoryType { Hat, Pet}
[CreateAssetMenu(menuName = "DragonCare/Accesory")]
public class DragonAccesory : ScriptableObject
{
    public int accesory_id;
    public string accesory_name;
    public GameObject accesory_GO;
    public AccesoryType accesory_type;
    public Sprite icon;
}
 