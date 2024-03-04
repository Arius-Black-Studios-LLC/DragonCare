using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ShopUIManger : MonoBehaviour
{
    [Header("Stats Window UI")]
    public TMP_Text productivityPointsText;
    public TMP_Text D_taskText, R_taskText, A_taskText, G_TaskText, O_TaskText, N_TaskTexts, S_TasksText;


    [Header("Egg Icons")]

    public Image D_eggImage;
    public Image  R_eggImage, A_eggImage, G_eggImage, O_eggImage, N_eggImage, S_eggImage;

    [Header("Buttons")]
    public Button D_EggButton;
    public Button R_EggButton, A_EggButton, G_EggButton, O_EggButton, N_EggButton, S_EggButton;

    [Header("Dragon Categories")]
    public DRAGONCategory D_cat;
    public DRAGONCategory R_cat, A_cat, G_cat,O_cat,N_cat,S_cat;


    public TMP_Text eggPrice;


    private void Awake()
    {
        FillStatsWindow();
        ButtonActiveOrInactive();


        D_eggImage.sprite = D_cat.eggIcon;
        R_eggImage.sprite = R_cat.eggIcon;
        A_eggImage.sprite = A_cat.eggIcon;
        G_eggImage.sprite = G_cat.eggIcon;
        O_eggImage.sprite = O_cat.eggIcon;
        N_eggImage.sprite = N_cat.eggIcon;
        S_eggImage.sprite = S_cat.eggIcon;

    }

    private void OnEnable()
    {
        FillStatsWindow();
        ButtonActiveOrInactive();
    }


    public void BuyD_Egg()
    {
        DragonNPCScriptable[] sublistOfDRagons = DragonDatabase.instance.dragons.Where(dragon => DragonDatabase.instance.getDragonCategoryByID(dragon.DragonID) == D_cat).ToArray();
        int dragonID = sublistOfDRagons[UnityEngine.Random.Range(0, sublistOfDRagons.Length)].DragonID;
        PlayerManager.instance.AddDragon(dragonID);
        NPCDragonSpawner.instance.SpawnNewUnLockedNPCDragon(dragonID);
        PlayerManager.instance.playerData.productivityPoints -= 100;
        PlayerManager.instance.playerData.D_tasks -= PlayerManager.instance.playerData.EggPricePerCat;
        PlayerManager.instance.IncreaseEggPricePerCat();
        PlayerManager.instance.SaveCurencies();
        ButtonActiveOrInactive();
        FillStatsWindow();

    }
    public void BuyR_Egg()
    {
        DragonNPCScriptable[] sublistOfDRagons = DragonDatabase.instance.dragons.Where(dragon => DragonDatabase.instance.getDragonCategoryByID(dragon.DragonID) == R_cat).ToArray();
        int dragonID = sublistOfDRagons[UnityEngine.Random.Range(0, sublistOfDRagons.Length)].DragonID;
        PlayerManager.instance.AddDragon(dragonID);
        NPCDragonSpawner.instance.SpawnNewUnLockedNPCDragon(dragonID);
        PlayerManager.instance.playerData.productivityPoints -= 100;
        PlayerManager.instance.playerData.R_tasks -= PlayerManager.instance.playerData.EggPricePerCat;
        PlayerManager.instance.SaveCurencies();
        ButtonActiveOrInactive();
        FillStatsWindow();

    }

    public void BuyA_Egg()
    {
        DragonNPCScriptable[] sublistOfDRagons = DragonDatabase.instance.dragons.Where(dragon => DragonDatabase.instance.getDragonCategoryByID(dragon.DragonID) == A_cat).ToArray();
        int dragonID = sublistOfDRagons[UnityEngine.Random.Range(0, sublistOfDRagons.Length)].DragonID;
        PlayerManager.instance.AddDragon(dragonID);
        NPCDragonSpawner.instance.SpawnNewUnLockedNPCDragon(dragonID);
        PlayerManager.instance.playerData.productivityPoints -= 100;
        PlayerManager.instance.playerData.A_tasks -= PlayerManager.instance.playerData.EggPricePerCat;
        PlayerManager.instance.SaveCurencies();
        ButtonActiveOrInactive();
        FillStatsWindow();

    }

    public void BuyG_Egg()
    {
        DragonNPCScriptable[] sublistOfDRagons = DragonDatabase.instance.dragons.Where(dragon => DragonDatabase.instance.getDragonCategoryByID(dragon.DragonID) == G_cat).ToArray();
        int dragonID = sublistOfDRagons[UnityEngine.Random.Range(0, sublistOfDRagons.Length)].DragonID;
        PlayerManager.instance.AddDragon(dragonID);
        NPCDragonSpawner.instance.SpawnNewUnLockedNPCDragon(dragonID);
        PlayerManager.instance.playerData.productivityPoints -= 100;
        PlayerManager.instance.playerData.G_tasks -= PlayerManager.instance.playerData.EggPricePerCat;
        PlayerManager.instance.SaveCurencies();
        ButtonActiveOrInactive();
        FillStatsWindow();

    }

    public void BuyO_Egg()
    {
        DragonNPCScriptable[] sublistOfDRagons = DragonDatabase.instance.dragons.Where(dragon => DragonDatabase.instance.getDragonCategoryByID(dragon.DragonID) == O_cat).ToArray();
        int dragonID = sublistOfDRagons[UnityEngine.Random.Range(0, sublistOfDRagons.Length)].DragonID;
        PlayerManager.instance.AddDragon(dragonID);
        NPCDragonSpawner.instance.SpawnNewUnLockedNPCDragon(dragonID);
        PlayerManager.instance.playerData.productivityPoints -= 100;
        PlayerManager.instance.playerData.O_tasks -= PlayerManager.instance.playerData.EggPricePerCat;
        PlayerManager.instance.SaveCurencies();
        ButtonActiveOrInactive();
        FillStatsWindow();

    }

    public void BuyN_Egg()
    {
        DragonNPCScriptable[] sublistOfNRagons = DragonDatabase.instance.dragons.Where(dragon => DragonDatabase.instance.getDragonCategoryByID(dragon.DragonID) == N_cat).ToArray();
        int dragonID = sublistOfNRagons[UnityEngine.Random.Range(0, sublistOfNRagons.Length)].DragonID;
        PlayerManager.instance.AddDragon(dragonID);
        NPCDragonSpawner.instance.SpawnNewUnLockedNPCDragon(dragonID);
        PlayerManager.instance.playerData.productivityPoints -= 100;
        PlayerManager.instance.playerData.N_tasks -= PlayerManager.instance.playerData.EggPricePerCat;
        PlayerManager.instance.SaveCurencies();
        ButtonActiveOrInactive();
        FillStatsWindow();

    }

    public void BuyS_Egg()
    {
        DragonNPCScriptable[] sublistOfDRagons = DragonDatabase.instance.dragons.Where(dragon => DragonDatabase.instance.getDragonCategoryByID(dragon.DragonID) == S_cat).ToArray();
        int dragonID = sublistOfDRagons[UnityEngine.Random.Range(0, sublistOfDRagons.Length)].DragonID;
        PlayerManager.instance.AddDragon(dragonID);
        NPCDragonSpawner.instance.SpawnNewUnLockedNPCDragon(dragonID);
        PlayerManager.instance.playerData.productivityPoints -= 100;
        PlayerManager.instance.playerData.S_tasks -= PlayerManager.instance.playerData.EggPricePerCat;
        PlayerManager.instance.SaveCurencies();
        ButtonActiveOrInactive();
        FillStatsWindow();

    }


    private void FillStatsWindow()
    {
        productivityPointsText.text = PlayerManager.instance.playerData.productivityPoints.ToString();
        D_taskText.text = PlayerManager.instance.playerData.D_tasks.ToString();
        R_taskText.text = PlayerManager.instance.playerData.R_tasks.ToString();
        A_taskText.text = PlayerManager.instance.playerData.A_tasks.ToString();
        G_TaskText.text = PlayerManager.instance.playerData.G_tasks.ToString();
        O_TaskText.text = PlayerManager.instance.playerData.O_tasks.ToString();
        N_TaskTexts.text = PlayerManager.instance.playerData.N_tasks.ToString();
        S_TasksText.text = PlayerManager.instance.playerData.S_tasks.ToString();

        eggPrice.text = "egg Price: " + PlayerManager.instance.playerData.EggPricePerCat.ToString() + "category points + 100 productivity points";
    }
    private void ButtonActiveOrInactive()
    {
        if (PlayerManager.instance.playerData.productivityPoints < 100 || PlayerManager.instance.playerData.D_tasks < PlayerManager.instance.playerData.EggPricePerCat)
        {
            D_EggButton.interactable = false;
        }
        else
        {
            D_EggButton.interactable = true;
        }

        if (PlayerManager.instance.playerData.productivityPoints < 100 || PlayerManager.instance.playerData.R_tasks < PlayerManager.instance.playerData.EggPricePerCat)
        {
          R_EggButton.interactable = false;
        }
        else
        {
            R_EggButton.interactable = true;
        }

        if (PlayerManager.instance.playerData.productivityPoints < 100 || PlayerManager.instance.playerData.A_tasks < PlayerManager.instance.playerData.EggPricePerCat)
        {
            A_EggButton.interactable = false;
        }
        else
        {
            A_EggButton.interactable = true;
        }

        if (PlayerManager.instance.playerData.productivityPoints < 100 || PlayerManager.instance.playerData.G_tasks < PlayerManager.instance.playerData.EggPricePerCat)
        {
            G_EggButton.interactable = false;
        }
        else
        {
            G_EggButton.interactable = true;
        }

        if (PlayerManager.instance.playerData.productivityPoints < 100 || PlayerManager.instance.playerData.O_tasks < PlayerManager.instance.playerData.EggPricePerCat)
        {
            O_EggButton.interactable = false;
        }
        else
        {
            O_EggButton.interactable = true;
        }

        if (PlayerManager.instance.playerData.productivityPoints < 100 || PlayerManager.instance.playerData.N_tasks < PlayerManager.instance.playerData.EggPricePerCat)
        {
            N_EggButton.interactable = false;
        }
        else
        {
            N_EggButton.interactable = true;
        }

        if (PlayerManager.instance.playerData.productivityPoints < 100 || PlayerManager.instance.playerData.S_tasks < PlayerManager.instance.playerData.EggPricePerCat)
        {
            S_EggButton.interactable = false;
        }
        else
        {
            S_EggButton.interactable = true;
        }
    }
}
