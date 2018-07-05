using UnityEngine;
using System.Collections;
[System.Serializable]
//Represents an Area (icon) on the map
public class Area  {

    public enum AreaTypes
    {
        City,
        Plains,
        Desert
    }

    public AreaTypes AreaType { get; set;}
    public string AreaName { get; set; }
    public int AreaID { get; set; }
    
    public Kingdom AttackingKingdom { get; set; }
    public Kingdom DefendingKingdom { get; set; }
    public int AttackCount { get; set; }
    public int DefendCount { get; set; }
	
    
}
