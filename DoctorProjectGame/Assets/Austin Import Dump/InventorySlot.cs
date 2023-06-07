using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool potionPocket;
    private Cauldron cauldron;
    private MortarAndPestle mortarAndPestle;
    private PotionMixer potionMixer;
    private Trash trash;

    public void Awake()
    {
        cauldron = gameObject.GetComponent<Cauldron>();
        mortarAndPestle = gameObject.GetComponent<MortarAndPestle>();
        potionMixer = gameObject.GetComponent<PotionMixer>();
        trash = gameObject.GetComponent<Trash>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if(transform.childCount == 0 && !potionPocket)
        {
            draggableItem.parentAfterDrag = transform;
            AddToSlots(dropped);
        }

        if(transform.childCount == 0 && potionPocket)
        {
            if(dropped.tag == "Potion")
            {
                draggableItem.parentAfterDrag = transform;
            }
        }
    }

    public void AddToSlots(GameObject draggedItem)
    {
        if(cauldron != null)
        {
            cauldron.NewIngredient(draggedItem);
        }
        if(mortarAndPestle != null)
        {
            mortarAndPestle.NewIngredient(draggedItem);
        }
        if(potionMixer != null)
        {
            potionMixer.NewIngredient(draggedItem);
        }
        if(trash != null)
        {
            trash.newObject = draggedItem;
        }
    }

    public void RemoveFromSlots()
    {
        if(cauldron != null)
        {
            cauldron.NewIngredient(null);
        }
        if(mortarAndPestle != null)
        {
            mortarAndPestle.NewIngredient(null);
        }
        if(potionMixer != null)
        {
            potionMixer.NewIngredient(null);
        }
        if (trash != null)
        {
            trash.newObject = null;
        }
    }
}
