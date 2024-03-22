using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWardorbManager : MonoBehaviour
{





    public Transform hatAttachmentPoint;    // Bone attachment point for hats


    private GameObject currentHat;  // Current hat object attached to the dragon
    private GameObject currentPet;  // Current pet object following the dragon

    // Start is called before the first frame update


    // Equip a hat to the dragon
    public void EquipHat(GameObject hatPrefab)
    {
        if (hatPrefab != null)
        {

            // Remove current hat if exists
            if (currentHat != null)
                Destroy(currentHat);

            // Instantiate and attach the new hat
            currentHat = Instantiate(hatPrefab, hatAttachmentPoint);
        }
        else
        {
            Debug.Log("prefab is null");
        }
    }

    // Equip a cape to the dragon
 

    // Equip a pet to the dragon
    public void EquipPet(GameObject petPrefab)
    {
        if (petPrefab != null)
        {
            // Remove current pet if exists
            if (currentPet != null)
                Destroy(currentPet);

            // Instantiate the new pet
            currentPet = Instantiate(petPrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Make the pet follow the dragon
        if (currentPet != null)
        {
            currentPet.transform.position = transform.position; // Update pet position to follow the dragon
            // You may add additional logic to make the pet follow the dragon more smoothly
        }
    }
}
