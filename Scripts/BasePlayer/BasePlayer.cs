using UnityEngine;
using System.Collections;

public class BasePlayer {

    public string PlayerName { get; set; }
    public int PlayerLevel { get; set; }
    public BaseCharacterClass PlayerClass { get; set; }
    public int Defense { get; set; }
    public int Agility { get; set; }
    public int Intellect { get; set; }
    public int Strength { get; set; }
    public int CurrentXP { get; set; }
    public int RequiredXP { get; set; }

}
