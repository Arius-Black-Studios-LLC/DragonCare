using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DressUPInventoryButton : MonoBehaviour
{
    public DragonAccesory accesory;
    public Image ButtonIcon;
    public TMP_Text buttonLabel;

    public void SetUpButton(DragonAccesory accesory)
    {
        this.accesory = accesory;
        buttonLabel.text = accesory.accesory_name;
        if (accesory.icon != null)
        {
            ButtonIcon.sprite = accesory.icon;
        }


    }
    public void EquiptThisAccesoryOnDragon()
    {
        if (accesory)
        {
            DragonNPCScriptable currDragon = DragonDatabase.instance.getDragonByID(PlayerManager.instance.dragonInFocus.dragonID);
            if(accesory.accesory_type == AccesoryType.Hat)
            {
                currDragon.Hat_id = accesory.accesory_id;
                PlayerManager.instance.dragonInFocus.wardorbManager.EquipHat(accesory.accesory_GO);


            }
            else if (accesory.accesory_type == AccesoryType.Back)
            {
                currDragon.Backpack_id = accesory.accesory_id;
                PlayerManager.instance.dragonInFocus.wardorbManager.EquipBackpackItem(accesory.accesory_GO);


            }
            else if (accesory.accesory_type == AccesoryType.Hold)
            {
                currDragon.holding_id = accesory.accesory_id;
                PlayerManager.instance.dragonInFocus.wardorbManager.EquipHoldingItem(accesory.accesory_GO);


            }
            else if (accesory.accesory_type == AccesoryType.Rideable)
            {
                currDragon.Rideable_id = accesory.accesory_id;
                PlayerManager.instance.dragonInFocus.wardorbManager.EquipRideableItem(accesory.accesory_GO);


            }
            else
            {
                currDragon.pet_id = accesory.accesory_id;
                PlayerManager.instance.dragonInFocus.wardorbManager.EquipPet(accesory.accesory_GO);

            }

            PlayerManager.instance.SaveDragonChanges(currDragon);

        }

    }



}
