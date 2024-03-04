using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDatabase : MonoBehaviour
{

    public static DragonDatabase instance;
    public DragonNPCScriptable[] dragons;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance );
        }
    }

    public string getDragonNameByID(int dragonID)
    {
        foreach (var dragon in dragons)
        {
            if(dragonID == dragon.DragonID)
            {
                return dragon.DragonName;
            }
        }

        return null;
    }

    public GameObject getDragonPrefabByID(int dragonID)
    {
        foreach (var dragon in dragons)
        {
            if (dragonID == dragon.DragonID)
            {
                return dragon.DragonObject;
            }
        }

        return null;
    }

    public DRAGONCategory getDragonCategoryByID(int dragonID)
    {
        foreach (var dragon in dragons)
        {
            if (dragonID == dragon.DragonID)
            {
                return dragon.DragonCategory;
            }
        }

        return null;
    }


}
