using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressUpUIManager : MonoBehaviour
{
    [SerializeField] private Transform scrollView;
    [SerializeField] private DressUPInventoryButton buttonPrefab;
    private List<DressUPInventoryButton> buttonList = new List<DressUPInventoryButton>();
    public void PopulateAllOwnedHats()
    {
        DestoyAllButtons();
        SpawnButtonsOfAccesoryByType(AccesoryType.Hat);
    }

    public void PopulateAllOwnedPets()
    {
        DestoyAllButtons();
        SpawnButtonsOfAccesoryByType(AccesoryType.Pet);
    }

    private void SpawnButtonsOfAccesoryByType(AccesoryType accesoryType)
    {
        for (int i = 0; i < PlayerManager.instance.playerData.unlockedAccesoryIDs.Count; i++)
        {
            DragonAccesory accesory = DragonDatabase.instance.getAccesoryByID(PlayerManager.instance.playerData.unlockedAccesoryIDs[i]);
            if (accesory.accesory_type == accesoryType)
            {
                DressUPInventoryButton dressUpButton = Instantiate(buttonPrefab, scrollView);
                buttonList.Add(dressUpButton);
                dressUpButton.SetUpButton(accesory);
            }


        }
    }
    private void DestoyAllButtons()
    {
        foreach(DressUPInventoryButton butt in buttonList)
        {
            Destroy(butt.gameObject);
        }
        buttonList.Clear();
    }
}
