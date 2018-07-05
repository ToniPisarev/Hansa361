using UnityEngine;
using System.Collections;
[System.Serializable]

public class BaseEquipment : BaseStatItem {

    public enum EquipmentTypes
    {
        Helmet,
        Armor,
        Gauntlets,
        Grieves
    }

    public int Value { get; set; }
    public EquipmentTypes EquipmentType { get; set; }   
}
