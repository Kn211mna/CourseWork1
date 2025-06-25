//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static UnityEditor.Progress;

//public class InventoryManager : MonoBehaviour
//{
//    public GameObject UIPanel;
//    public GameObject crosshair;
//    public Transform inventoryPanel;
//    public List<InventorySlot> slots = new List<InventorySlot>();
//    public bool isOpened;
//    public float reachDistance = 3f;
//    private Camera mainCamera;
//    // Start is called before the first frame update
//    private void Awake()
//    {
//        UIPanel.SetActive(true);
//    }
//    void Start()
//    {
//        mainCamera = Camera.main;
//        for (int i = 0; i < inventoryPanel.childCount; i++)
//        {
//            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
//            {
//                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
//            }
//        }
//        UIPanel.SetActive(false);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.I))
//        {
//            isOpened = !isOpened;
//            if (isOpened)
//            {
//                UIPanel.SetActive(true);
//                crosshair.SetActive(false);
//            }
//            else
//            {
//                UIPanel.SetActive(false);
//                crosshair.SetActive(true);
//            }
//        }
//        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hit;

//        if (Physics.Raycast(ray, out hit, reachDistance))
//        {
//            if (Input.GetKeyDown(KeyCode.E))
//            {
//                if (hit.collider.gameObject.GetComponent<Item>() != null)
//                {
//                    AddItem(hit.collider.gameObject.GetComponent<Item>().item, hit.collider.gameObject.GetComponent<Item>().amount);
//                    Destroy(hit.collider.gameObject);
//                }
//            }
//            Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.green);
//        }
//        else
//        {
//            Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.red);
//        }
//    }
//    private void AddItem(ItemScriptableObject _item, int _amount)
//    {
//        foreach (InventorySlot slot in slots)
//        {
//            if (slot.item == _item)
//            {
//                slot.amount += _amount;
//                slot.itemAmountText.text = slot.amount.ToString();
//                return;
//            }
//        }
//        foreach (InventorySlot slot in slots)
//        {
//            if (slot.isEmpty == true)
//            {
//                slot.item = _item;
//                slot.amount = _amount;
//                slot.isEmpty = false;
//                slot.SetIcon(_item.icon);
//                slot.itemAmountText.text = _amount.ToString();
//                break;
//            }
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject UIBG;
    public Transform inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public bool isOpened;
    public float reachDistance = 3f;
    private Camera mainCamera;
    private CameraController cameraController;
    private Item nearbyItem; // Ссылка на ближайший предмет

    private void Awake()
    {
        UIBG.SetActive(true);
    }
    void Start()
    {
        mainCamera = Camera.main;
        cameraController = mainCamera.GetComponent<CameraController>();

        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        UIBG.SetActive(false); // скрыть фон
        inventoryPanel.gameObject.SetActive(false); // скрыть панель инвентаря
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpened = !isOpened;
            UIBG.SetActive(isOpened);
            inventoryPanel.gameObject.SetActive(isOpened);

            // Вимикаємо/вмикаємо керування камерою
            if (cameraController != null)
                cameraController.enabled = !isOpened;

            // Керування курсором
            if (isOpened)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && nearbyItem != null)
        {
            AddItem(nearbyItem.item, nearbyItem.amount);
            Destroy(nearbyItem.gameObject);
            nearbyItem = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            nearbyItem = item;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && item == nearbyItem)
        {
            nearbyItem = null;
        }
    }

    public void AddItem(ItemScriptableObject _item, int _amount)
    {
        foreach (InventorySlot slot in slots)
        {
            // Стакаем предметы вместе
            // В слоте уже имеется этот предмет
            if (slot.item == _item)
            {
                if (slot.amount + _amount <= _item.maximumAmount)
                {
                    slot.amount += _amount;
                    slot.itemAmountText.text = slot.amount.ToString();
                    return;
                }
                break;
            }
        }
        foreach (InventorySlot slot in slots)
        {
            // добавляем предметы в свободные ячейки
            if (slot.isEmpty == true)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                if (slot.item.maximumAmount != 1) // added this if statement for single items
                {
                    slot.itemAmountText.text = _amount.ToString();
                }
                break;
            }
        }
    }
}