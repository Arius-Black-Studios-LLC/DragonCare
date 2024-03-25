using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class ShopUIManger : MonoBehaviour
{
    [Header("Stats Window UI")]
    public TMP_Text productivityPointsText;
    public TMP_Text credits;
    public TMP_Text eggPrice;


    [Header("UI")]
    public Button BuyDragon_ProductivtyPoints;
    public Button BuyDragon_Credits;

    public Button BuyAccesory_ProductivityPoints;
    public Button BuyAccesory_Credits;


    [Header("Sub-window")]
    public GameObject ConformationWindowGO;
    public TMP_Text ConfirmationText;
    public Image NewItemSprite;




    private void Awake()
    {
        FillStatsWindow();


    }

    void Start()
    {


        // Assign BuyDragonWithProductivtyPoints function to BuyDragon_ProductivtyPoints button
        BuyDragon_ProductivtyPoints.onClick.AddListener(BuyDragonWithProductivtyPoints);
        BuyDragon_ProductivtyPoints.interactable = PlayerManager.instance.playerData.productivityPoints >= PlayerManager.instance.playerData.EggPrice;

        // Assign BuyDragonWithCredits function to BuyDragon_Credits button
        BuyDragon_Credits.onClick.AddListener(BuyDragonWithCredits);
        BuyDragon_Credits.interactable = PlayerManager.instance.playerData.credits >= 100;


        // Assign BuyDAccesoryWithProductivtyPoints function to BuyAccesory_ProductivityPoints button
        BuyAccesory_ProductivityPoints.onClick.AddListener(BuyDAccesoryWithProductivtyPoints);
        BuyAccesory_ProductivityPoints.interactable = PlayerManager.instance.playerData.productivityPoints >= PlayerManager.instance.playerData.AccesoryPrice;

        // Assign BuyAccesoryWithCredits function to BuyAccesory_Credits button
        BuyAccesory_Credits.onClick.AddListener(BuyAccesoryWithCredits);
        BuyAccesory_Credits.interactable = PlayerManager.instance.playerData.credits >= 100;

    }

    private void OnEnable()
    {
        FillStatsWindow();

    }



    //Called by Dragon Granting UI
    public void BuyDragonWithProductivtyPoints()
    {
        PlayerManager.instance.AddRandomDragon();
        FillStatsWindow();
    }

    public void BuyDragonWithCredits()
    {
        PlayerManager.instance.playerData.credits -= 100;
        PlayerManager.instance.AddRandomDragon(true);
        FillStatsWindow();
    }

    public void BuyDAccesoryWithProductivtyPoints()
    {

        PlayerManager.instance.AddAccessories();
        FillStatsWindow();
    }

    public void BuyAccesoryWithCredits()
    {

        PlayerManager.instance.playerData.credits -= 100;
        PlayerManager.instance.AddAccessories(true);
        FillStatsWindow();
    }


    public void ShowConfirmationWindow(string message, Sprite sprite = null)
    {
        ConformationWindowGO.SetActive(true);
        ConfirmationText.text = message;
        if (sprite != null)
        {
            NewItemSprite.enabled = true;
            NewItemSprite.sprite = sprite;
        }
        else
        {
            NewItemSprite.enabled = false;
        }
    }

    private void FillStatsWindow()
    {
        productivityPointsText.text = PlayerManager.instance.playerData.productivityPoints.ToString();
        credits.text = PlayerManager.instance.playerData.credits.ToString();
        eggPrice.text = "egg Price: " + PlayerManager.instance.playerData.EggPrice.ToString();
    }


}




