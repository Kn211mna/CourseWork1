﻿//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class QuickslotInventory : MonoBehaviour
//{
//    // Объект у которого дети являются слотами
//    public Transform quickslotParent;
//    public InventoryManager inventoryManager;
//    public int currentQuickslotID = 0;
//    public Sprite selectedSprite;
//    public Sprite notSelectedSprite;
//    public Text healthText;
//    public Transform itemContainer;
//    public InventorySlot activeSlot = null;
//    public Transform allWeapons;

//    // Update is called once per frame
//    void Update()
//    {
//        float mw = Input.GetAxis("Mouse ScrollWheel");
//        // Используем колесико мышки
//        if (mw > 0.1)
//        {
//            // Берем предыдущий слот и меняем его картинку на обычную

//            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
//            // Здесь добавляем что случится когда мы УБЕРАЕМ ВЫДЕЛЕНИЕ со слота (Выключить нужный нам предмет, поменять аниматор ...)

//            // Если крутим колесиком мышки вперед и наше число currentQuickslotID равно последнему слоту, то выбираем наш первый слот (первый слот считается нулевым)
//            if (currentQuickslotID >= quickslotParent.childCount - 1)
//            {
//                currentQuickslotID = 0;
//            }
//            else
//            {
//                // Прибавляем к числу currentQuickslotID единичку
//                currentQuickslotID++;
//            }
//            // Берем предыдущий слот и меняем его картинку на "выбранную"

//            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
//            activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
//            ShowItemInHand();
//            // Здесь добавляем что случится когда мы ВЫДЕЛЯЕМ слот (Включить нужный нам предмет, поменять аниматор ...)

//        }
//        if (mw < -0.1)
//        {
//            // Берем предыдущий слот и меняем его картинку на обычную

//            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
//            // Здесь добавляем что случится когда мы УБЕРАЕМ ВЫДЕЛЕНИЕ со слота (Выключить нужный нам предмет, поменять аниматор ...)


//            // Если крутим колесиком мышки назад и наше число currentQuickslotID равно 0, то выбираем наш последний слот
//            if (currentQuickslotID <= 0)
//            {
//                currentQuickslotID = quickslotParent.childCount - 1;
//            }
//            else
//            {
//                // Уменьшаем число currentQuickslotID на 1
//                currentQuickslotID--;
//            }
//            // Берем предыдущий слот и меняем его картинку на "выбранную"

//            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
//            activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
//            ShowItemInHand();
//            // Здесь добавляем что случится когда мы ВЫДЕЛЯЕМ слот (Включить нужный нам предмет, поменять аниматор ...)

//        }
//        // Используем цифры
//        for (int i = 0; i < quickslotParent.childCount; i++)
//        {
//            // если мы нажимаем на клавиши 1 по 5 то...
//            if (Input.GetKeyDown((i + 1).ToString()))
//            {
//                // проверяем если наш выбранный слот равен слоту который у нас уже выбран, то
//                if (currentQuickslotID == i)
//                {
//                    // Ставим картинку "selected" на слот если он "not selected" или наоборот
//                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
//                    {
//                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
//                        activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
//                        ShowItemInHand();
//                        //foreach ...
//                    }
//                    else
//                    {
//                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
//                        activeSlot = null;
//                        HideItemsInHand();
//                        //foreach ...

//                    }
//                }
//                // Иначе мы убираем свечение с предыдущего слота и светим слот который мы выбираем
//                else
//                {
//                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
//                    // ЗДЕСЬ ТЕБЕ НУЖЕН FOREACH ЦИКЛ КОТОРЫЙ БУДЕТ ВЫКЛЮЧАТЬ ВСЕ ОБЪЕКТЫ В МАССИВЕ ITEMS
//                    currentQuickslotID = i;

//                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
//                    activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
//                    ShowItemInHand();
//                    // СЮДА СКОПИРУЙ ТО ЧТО ТЫ ПИСАЛ КОГДА ВКЛЮЧАЛ КАЛАШ <--
//                }
//            }
//        }
//        // Используем предмет по нажатию на левую кнопку мыши
//        if (Input.GetKeyDown(KeyCode.Mouse0))
//        {
//            if (quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item != null)
//            {
//                if (quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item.isConsumeable && !inventoryManager.isOpened && quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == selectedSprite)
//                {
//                    // Применяем изменения к здоровью (будущем к голоду и жажде) 
//                    ChangeCharacteristics();

//                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().amount <= 1)
//                    {
//                        quickslotParent.GetChild(currentQuickslotID).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
//                    }
//                    else
//                    {
//                        quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().amount--;
//                        quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().itemAmountText.text = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().amount.ToString();
//                    }
//                }
//            }
//        }
//    }

//    private void ChangeCharacteristics()
//    {
//        // Если здоровье + добавленное здоровье от предмета меньше или равно 100, то делаем вычисления... 
//        if (int.Parse(healthText.text) + quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item.changeHealth <= 100)
//        {
//            float newHealth = int.Parse(healthText.text) + quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item.changeHealth;
//            healthText.text = newHealth.ToString();
//        }
//        // Иначе, просто ставим здоровье на 100
//        else
//        {
//            healthText.text = "100";
//        }
//    }

//    private void ShowItemInHand()
//    {
//        HideItemsInHand();
//        if (activeSlot.item == null)
//        {
//            return;
//        }
//        for (int i = 0; i < allWeapons.childCount; i++)
//        {
//            if (activeSlot.item.inHandName == allWeapons.GetChild(i).name)
//            {
//                allWeapons.GetChild(i).gameObject.SetActive(true);
//            }
//        }
//    }
//    private void HideItemsInHand()
//    {
//        for (int i = 0; i < allWeapons.childCount; i++)
//        {
//            allWeapons.GetChild(i).gameObject.SetActive(false);
//        }
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickslotInventory : MonoBehaviour
{
    // Объект у которого дети являются слотами
    public Transform quickslotParent;
    public InventoryManager inventoryManager;
    public int currentQuickslotID = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;
    public Text healthText;
    public Transform itemContainer;
    public InventorySlot activeSlot = null;
    public Transform allWeapons;

    public HealthIndicator playerHealthIndicator; // Додаємо посилання на скрипт здоров'я

    void Start()
    {
        // Якщо не призначено в інспекторі — шукаємо автоматично
        if (playerHealthIndicator == null)
            playerHealthIndicator = FindObjectOfType<HealthIndicator>();
        // Оновлюємо текст здоров'я при старті
        if (playerHealthIndicator != null && healthText != null)
            healthText.text = playerHealthIndicator.healthAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        // Используем колесико мышки
        if (mw > 0.1)
        {
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;

            if (currentQuickslotID >= quickslotParent.childCount - 1)
            {
                currentQuickslotID = 0;
            }
            else
            {
                currentQuickslotID++;
            }

            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
            activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
            ShowItemInHand();
        }
        if (mw < -0.1)
        {
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;

            if (currentQuickslotID <= 0)
            {
                currentQuickslotID = quickslotParent.childCount - 1;
            }
            else
            {
                currentQuickslotID--;
            }

            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
            activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
            ShowItemInHand();
        }
        // Используем цифры
        for (int i = 0; i < quickslotParent.childCount; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                if (currentQuickslotID == i)
                {
                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                        activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
                        ShowItemInHand();
                    }
                    else
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                        activeSlot = null;
                        HideItemsInHand();
                    }
                }
                else
                {
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    currentQuickslotID = i;
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                    activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
                    ShowItemInHand();
                }
            }
        }
        // Используем предмет по нажатию на левую кнопку мыши
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var slot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
            if (slot.item != null)
            {
                if (slot.item.isConsumeable && !inventoryManager.isOpened && quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == selectedSprite)
                {
                    // Применяем изменения к здоровью (будущем к голоду и жажде) 
                    ChangeCharacteristics();

                    if (slot.amount <= 1)
                    {
                        slot.GetComponentInChildren<DragAndDropItem>().NullifySlotData();
                    }
                    else
                    {
                        slot.amount--;
                        slot.itemAmountText.text = slot.amount.ToString();
                    }
                }
            }
        }
    }

    private void ChangeCharacteristics()
    {
        var slot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
        var item = slot.item;
        if (playerHealthIndicator != null && item != null)
        {
            playerHealthIndicator.healthAmount = Mathf.Min(playerHealthIndicator.healthAmount + item.changeHealth, 100f);
            if (healthText != null)
                healthText.text = playerHealthIndicator.healthAmount.ToString();
        }
    }

    private void ShowItemInHand()
    {
        HideItemsInHand();
        if (activeSlot == null || activeSlot.item == null)
        {
            return;
        }
        for (int i = 0; i < allWeapons.childCount; i++)
        {
            if (activeSlot.item.inHandName == allWeapons.GetChild(i).name)
            {
                allWeapons.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    private void HideItemsInHand()
    {
        for (int i = 0; i < allWeapons.childCount; i++)
        {
            allWeapons.GetChild(i).gameObject.SetActive(false);
        }
    }
}