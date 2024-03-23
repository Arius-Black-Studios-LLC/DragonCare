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


    [Header("Buttons")]
    public Button D_EggButton;
    public Button R_EggButton, A_EggButton, G_EggButton, O_EggButton, N_EggButton, S_EggButton;




    public TMP_Text eggPrice;


    private void Awake()
    {
        FillStatsWindow();
        ButtonActiveOrInactive();

    }

    private void OnEnable()
    {
        FillStatsWindow();
        ButtonActiveOrInactive();
    }

    private void BuyDragon(DRAGONCategory category, bool freeEgg = false)
    {
        int newDragon = -1;
        if (category == DRAGONCategory.Any)
        {
            newDragon = DragonDatabase.instance.dragons[UnityEngine.Random.Range(0, DragonDatabase.instance.dragons.Length)].DragonID;
        }
        else
        {
            DragonNPCScriptable[] sublistOfDRagons = DragonDatabase.instance.dragons.Where(dragon => DragonDatabase.instance.getDragonByID(dragon.DragonID).DragonCategory == category).ToArray();
            newDragon = sublistOfDRagons[UnityEngine.Random.Range(0, sublistOfDRagons.Length)].DragonID;

        }

        PlayerManager.instance.AddDragon(newDragon, category, freeEgg);

        ButtonActiveOrInactive();
        FillStatsWindow();
    }
    public void BuyD_Egg()
    {
        BuyDragon(DRAGONCategory.D);

    }
    public void BuyR_Egg()
    {
        BuyDragon(DRAGONCategory.R);

    }

    public void BuyA_Egg()
    {
        BuyDragon(DRAGONCategory.A);

    }

    public void BuyG_Egg()
    {
        BuyDragon(DRAGONCategory.G);

    }

    public void BuyO_Egg()
    {
        BuyDragon(DRAGONCategory.O);

    }

    public void BuyN_Egg()
    {
        BuyDragon(DRAGONCategory.N);

    }

    public void BuyS_Egg()
    {
        BuyDragon(DRAGONCategory.S);

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
