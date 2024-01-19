using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : SingletonMonoBehaviour<EquipmentUI>
{
    [SerializeField] private Transform weaponRow;
    [SerializeField] private Transform accessoryRow;

    private List<Slot> weaponSlots;
    private List<Slot> accessorySlots;

    public Texture rankEmpty;
    public Texture rankFilled;

    private class Slot
    {
        public bool isUsed;
        public Image image;
        public Type type;

        public GameObject ranksGrid; 
        public RawImage[] rankImages;
    }

    protected override void Awake()
    {
        base.Awake();
        InitalizeCells();
    }

    public void AddItem(Sprite icon, Type id, int maxRank)
    {
        Slot emptySlot = GetSlot(id);

        emptySlot.image.sprite = icon;
        emptySlot.image.enabled = true;
        
        emptySlot.type = id;
        emptySlot.isUsed = true;
        /*
        foreach (RawImage rankImage in emptySlot.rankImages)
        {
            if (rankImage != null)
            {
                Destroy(rankImage.gameObject);
            }
        } */

        PopulateRankIndicators(emptySlot, maxRank);
    }

    private void PopulateRankIndicators(Slot slot, int maxRank)
    {
        slot.rankImages = new RawImage[maxRank];

        for (int i = 0; i < maxRank; i++)
        {
            GameObject rankGameObject = new GameObject("RankImage");
            rankGameObject.transform.SetParent(slot.ranksGrid.transform, false);

            RawImage rankImage = rankGameObject.AddComponent<RawImage>();
            rankImage.texture = rankEmpty;

            slot.rankImages[i] = rankImage;
        }

        slot.rankImages[0].texture = rankFilled;
    }

    public void UpdateItemRank(Type type, int currentRank)
    {
        Slot slot;

        if (type.IsSubclassOf(typeof(Weapon)))
        {
            slot = weaponSlots.First(slot => slot.type == type);
        }
        else
        {
            slot = accessorySlots.First(slot => slot.type == type);
        }

        slot.rankImages[currentRank].texture = rankFilled;
    }

    private Slot GetSlot(Type id)
    {
        Slot slot;

        if (id.IsSubclassOf(typeof(Weapon)))
        {
            slot = weaponSlots.First(slot => slot.isUsed == false);
        }
        else
        {
            slot = accessorySlots.First(slot => slot.isUsed == false);
        }

        return slot;
    }

    private void InitalizeCells()
    {
        weaponSlots = new List<Slot>();
        accessorySlots = new List<Slot>();

        foreach (Transform cellTransform in weaponRow)
        {
            Image itemImage = cellTransform.Find("ItemImage")?.gameObject.GetComponent<Image>();
            GameObject ranksGrid = cellTransform.Find("RanksGrid")?.gameObject;

            Slot slot = new Slot
            {
                isUsed = false,
                image = itemImage,
                ranksGrid = ranksGrid
            };

            itemImage.enabled = false;
            weaponSlots.Add(slot);
        }

        foreach (Transform cellTransform in accessoryRow)
        {
            Image itemImage = cellTransform.Find("ItemImage")?.gameObject.GetComponent<Image>();
            GameObject ranksGrid = cellTransform.Find("RanksGrid")?.gameObject;

            Slot slot = new Slot
            {
                isUsed = false,
                image = itemImage,
                ranksGrid = ranksGrid
            };

            itemImage.enabled = false;
            accessorySlots.Add(slot);
        }
    }
}
