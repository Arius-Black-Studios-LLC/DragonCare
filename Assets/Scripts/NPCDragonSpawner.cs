using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class NPCDragonSpawner : MonoBehaviour
{
    public static NPCDragonSpawner instance; 
    public GameObject NPC_Brain;
    public CinemachineFreeLook cam;
    public TMP_Text dragonName;

    private int dragonIndex;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnNPCDragons()
    {
        foreach(DragonSaveSettings dragon in PlayerManager.instance.playerData.unlockedDragons)
        {

                PlayerManager.instance.SpawnDragonWithBrain(dragon, NPC_Brain);
            

        }
        dragonIndex = Random.Range(0, PlayerManager.instance.dragonsInScene.Count - 1);
        GameObject lookFollow = PlayerManager.instance.dragonsInScene[dragonIndex];
        PlayerManager.instance.dragonInFocus = lookFollow.GetComponent<DragonNPCManager>();
        ShowDragonName();
        cam.Follow = lookFollow.transform;
        cam.LookAt = lookFollow.transform;
    }

    public void SpawnNewUnLockedNPCDragon(DragonSaveSettings newDragon)
    {
        PlayerManager.instance.SpawnDragonWithBrain(newDragon, NPC_Brain);
    }

    public void LookAtNextDragon()
    {
        dragonIndex++;
        if(dragonIndex >= PlayerManager.instance.dragonsInScene.Count)
        {
            dragonIndex = 0;
        }
        GameObject lookFollow = PlayerManager.instance.dragonsInScene[dragonIndex];
        PlayerManager.instance.dragonInFocus = lookFollow.GetComponent<DragonNPCManager>();
        cam.Follow = lookFollow.transform;
        cam.LookAt = lookFollow.transform;
        ShowDragonName();
    }
    public void LookAtLastDragon()
    {
        dragonIndex--;
        if (dragonIndex < 0)
        {
            dragonIndex = PlayerManager.instance.dragonsInScene.Count-1;
        }
        GameObject lookFollow = PlayerManager.instance.dragonsInScene[dragonIndex];
        PlayerManager.instance.dragonInFocus = lookFollow.GetComponent<DragonNPCManager>();
        cam.Follow = lookFollow.transform;
        cam.LookAt = lookFollow.transform;
        ShowDragonName();
    }
    public void ShowDragonName()
    {
        dragonName.text = DragonDatabase.instance.getDragonByID(PlayerManager.instance.dragonsInScene[dragonIndex].GetComponent<DragonNPCManager>().dragonID).DragonName;
    }
}
