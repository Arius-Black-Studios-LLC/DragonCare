using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerEggGranter : MonoBehaviour
{
    private bool isFirstTimePlaying;
    private void Start()
    {
        Debug.Log("Dragons unlocked :" + PlayerManager.instance.playerData.unlockedDragons.Count);
        isFirstTimePlaying = PlayerManager.instance.playerData.unlockedDragons.Count <= 0;

        if (!isFirstTimePlaying)
        {
            NPCDragonSpawner.instance.SpawnNPCDragons();
            gameObject.SetActive(false);
        }
    }

    public void GiveDragonToPlayer()
    {
        int newDragon = DragonDatabase.instance.dragons[UnityEngine.Random.Range(0, DragonDatabase.instance.dragons.Length)].DragonID;
        PlayerManager.instance.AddDragon(newDragon);
        gameObject.SetActive(false);
    }
}
