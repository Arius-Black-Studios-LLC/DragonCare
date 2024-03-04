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
        foreach(int id in PlayerManager.instance.playerData.unlockedDragonIDs)
        {
            PlayerManager.instance.SpawnDragonWithBrain(id,NPC_Brain);

        }
        dragonIndex = Random.Range(0, PlayerManager.instance.dragonsInScene.Count - 1);
        GameObject lookFollow = PlayerManager.instance.dragonsInScene[dragonIndex];
        ShowDragonName();
        cam.Follow = lookFollow.transform;
        cam.LookAt = lookFollow.transform;
    }

    public void SpawnNewUnLockedNPCDragon(int id)
    {
        PlayerManager.instance.SpawnDragonWithBrain(id, NPC_Brain);
    }

    public void LookAtNextDragon()
    {
        dragonIndex++;
        if(dragonIndex >= PlayerManager.instance.dragonsInScene.Count)
        {
            dragonIndex = 0;
        }
        GameObject lookFollow = PlayerManager.instance.dragonsInScene[dragonIndex];
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
        cam.Follow = lookFollow.transform;
        cam.LookAt = lookFollow.transform;
        ShowDragonName();
    }
    public void ShowDragonName()
    {
        dragonName.text = DragonDatabase.instance.getDragonNameByID(PlayerManager.instance.dragonsInScene[dragonIndex].GetComponent<DragonNPCManager>().dragonID);
    }
}
