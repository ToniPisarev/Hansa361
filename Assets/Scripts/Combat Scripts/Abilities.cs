using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TileDraw.Map;

public abstract class Abilities {
    protected float tilesize = 2;
    protected Tile myTile;
    protected Vector2 myPointInCell;
    protected Vector3 rot;
    protected Cell grid;
    protected Transform trans;
    protected List<String> attackTiles;

    protected const int offset = 25;

    public bool isPhysical; // true: physical, false: magical
    public string description;
    public int minRange = 1;
    public int maxRange = 1;
    public int damage;

    protected BaseCharacter myCharacter;
    protected CharController charControl;

    public Abilities(Cell cell, BaseCharacter character) {
        grid = cell;
        myCharacter = character;

    }

    public Abilities() { }

    public void updateLoc(GameObject characterController) {

        trans = characterController.transform;
        attackTiles = new List<string>();

        myPointInCell = grid.convertWorldPosToIndex(trans.position.x, trans.position.z);
        myTile = grid.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
        rot = trans.rotation.eulerAngles;

    }

    public abstract void attack();


    // Get the string of the i-th neigbhour
    protected string getString(int neighbourIndex, int i) {

        if (i == 1) {
            return myTile.Neighbours[neighbourIndex];
        } else {
            return grid.ConvertStringToTile(getString(neighbourIndex, i - 1)).Neighbours[neighbourIndex];
        }
    }


    // Get the tile of the i-th neigbhour
    protected Tile getTile(int neighbourIndex, int i) {
        return grid.ConvertStringToTile(getString(neighbourIndex, i));
    }

    // Get the world position of a Tile
    protected Vector3 getPosition(Tile pTile) {
        Vector2 temp = grid.convertIndexToWorldPos((int)(grid.GetPointInCellFromTileIndex(pTile.TileIndex).x), (int)(grid.GetPointInCellFromTileIndex(pTile.TileIndex).y));
        return new Vector3(temp.x, pTile.GetHeight(), temp.y);
    }

}

/*--------------------------------------------------------------------------------------------------------------------
* 
* Squire Abilities
* 
*---------------------------------------------------------------------------------------------------------------------
*/

public class SpearAttack : Abilities {

    public SpearAttack(Cell cell, BaseCharacter character) : base(cell,character) { 
        description = "Basic Attack\nDamage enemies with your Halberd";
        maxRange = 2;
        damage = character.PhysicalDamage;
    }

    public override void attack() {

        isPhysical = true;

        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            DirectionalAttack(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour in the front
            DirectionalAttack(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            DirectionalAttack(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            DirectionalAttack(3);

        }

        trans.GetComponent<Animator>().Play("Attack SP");
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");
    }

    // helper function:
    private void DirectionalAttack(int neighbourIndex) {
        if (getString(neighbourIndex, 1) != "None")
            attackTiles.Add(getString(neighbourIndex, 1));

        if (getString(neighbourIndex, 2) != "None")
            attackTiles.Add(getString(neighbourIndex, 2));

    }
}

public class HealingLight : Abilities {
    public HealingLight(Cell cell, BaseCharacter character) : base(cell, character) { 

        description = "Healing Light\nIncrease health by 20%";
        minRange = 0;
        maxRange = 0;
        damage = 0;
    }

    public override void attack() {

        isPhysical = false;

        // Load spell particle systems
        UnityEngine.Object.Instantiate(Resources.Load("Spells/HealingLight"), trans.position, Quaternion.identity);

        //TODO: increase strength in CharController

        // Apply damage (to no tile)
        BaseCharacter myCharacter = trans.GetComponent<CharController>().myCharacter;
        myCharacter.CurrentHealth += (int)(myCharacter.Health / 20.00f);
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "heal");
    }

}

/*--------------------------------------------------------------------------------------------------------------------
* 
* Theif Abilities
* 
*---------------------------------------------------------------------------------------------------------------------
*/

public class DoubleStab : Abilities {
    public DoubleStab(Cell cell, BaseCharacter character) : base(cell, character) {

        description = "Double Stab\nStab your opponent with both daggers, deal 1.5x damage.";
        maxRange = 1;
        damage = (int)(character.PhysicalDamage * 1.5f);
    }

    public override void attack() {

        isPhysical = true;

        //Default attack : Range of 1 tile infront of character
        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            DirectionalAttack(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour on the front
            DirectionalAttack(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            DirectionalAttack(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            DirectionalAttack(3);
        }

        trans.GetComponent<Animator>().Play("Double Stab");
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");
    }

    // helper function
    private void DirectionalAttack(int neighbourIndex) {
        if (myTile.Neighbours[neighbourIndex] != "None") {
            attackTiles.Add(myTile.Neighbours[neighbourIndex]);
        }


    }
}

public class Stab : Abilities {
    public Stab(Cell cell, BaseCharacter character) : base(cell, character) {

        description = "Stab\nDamage your opponent with one of your daggers.";
        maxRange = 1;
        damage = character.PhysicalDamage;
    }

    public override void attack() {

        isPhysical = true;

        //Default attack : Range of 1 tile infront of character
        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            DirectionalAttack(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour on the front
            DirectionalAttack(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            DirectionalAttack(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            DirectionalAttack(3);
        }

        trans.GetComponent<Animator>().Play("Stab");
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");
    }

    // helper function
    private void DirectionalAttack(int neighbourIndex) {
        if (myTile.Neighbours[neighbourIndex] != "None")
            attackTiles.Add(myTile.Neighbours[neighbourIndex]);

    }
}

public class LegSweep : Abilities {
    public LegSweep(Cell cell, BaseCharacter character) : base(cell, character) {

        description = "Leg Sweep\nLow sweeping kick, ouch!";
        maxRange = 1;
        damage = character.PhysicalDamage;
    }

    public override void attack() {

        isPhysical = true;

        //Default attack : Range of 1 tile infront of character
        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            DirectionalAttack(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour on the front
            DirectionalAttack(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            DirectionalAttack(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            DirectionalAttack(3);
        }

        trans.GetComponent<Animator>().Play("Leg Sweep");
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");
    }

    // helper function
    private void DirectionalAttack(int neighbourIndex) {
        if (myTile.Neighbours[neighbourIndex] != "None")
            attackTiles.Add(myTile.Neighbours[neighbourIndex]);

    }
}

public class TwoTurn : Abilities {
    public TwoTurn(Cell cell, BaseCharacter character) : base(cell, character) {

        description = "Two Turn\nSweep your opponent off his feet, move again";
        maxRange = 1;
        damage = character.PhysicalDamage;

    }

    public override void attack() {

        isPhysical = true;

        //Default attack : Range of 1 tile infront of character
        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            DirectionalAttack(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour on the front
            DirectionalAttack(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            DirectionalAttack(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            DirectionalAttack(3);
        }

        trans.GetComponent<Animator>().Play("Leg Sweep");
        if (trans.GetComponent<CharController>().showLabel5 == false)
            trans.GetComponent<CharController>().takeTwoTurns = true;
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");
    }

    // helper function
    private void DirectionalAttack(int neighbourIndex) {
        if (myTile.Neighbours[neighbourIndex] != "None")
            attackTiles.Add(myTile.Neighbours[neighbourIndex]);

    }
}



/*--------------------------------------------------------------------------------------------------------------------
* 
* Apprentice/Mage Abilities
* 
*---------------------------------------------------------------------------------------------------------------------
*/

public class Fireball : Abilities {

    public Fireball(Cell cell, BaseCharacter character) : base(cell, character) {
        description = "FireBall\nCast a fire ball, damages all enemies in front of you.";
        maxRange = 2;
        damage = character.MagicDamage;
    }

    public override void attack() {
        isPhysical = false;

        // Play animation
        trans.GetComponent<Animator>().Play("Standing 2H Magic Attack 2");

        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            DirectionalAttack(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour in the front
            DirectionalAttack(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            DirectionalAttack(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            DirectionalAttack(3);

        }

        // Apply damage
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");

    }

    // helper function
    private void DirectionalAttack(int neighbourIndex) {

        // get directional offset for the spell prefab
        Vector3 vOffset;
        if (neighbourIndex == 0) {
            vOffset = new Vector3(0f, 0.5f, 0.5f);
        } else if (neighbourIndex == 1) {
            vOffset = new Vector3(-0.5f, 0.5f, 0f);
        } else if (neighbourIndex == 2) {
            vOffset = new Vector3(0f, 0.5f, -0.5f);
        } else {
            vOffset = new Vector3(0.5f, 0.5f, 0f);
        }

        // Load spell particle systems
        UnityEngine.Object.Instantiate(Resources.Load("Spells/Fireball"), trans.position + vOffset, trans.rotation);

        if (getString(neighbourIndex, 1) != "None")
            attackTiles.Add(getString(neighbourIndex, 1));

        if (getString(neighbourIndex, 2) != "None")
            attackTiles.Add(getString(neighbourIndex, 2));

    }


}

public class Lightning : Abilities {

    public Lightning(Cell cell, BaseCharacter character) : base(cell, character) {

        description = "Lightning\nSummon a lightning bolt on each enemy infront of you.";
        maxRange = 4;
        damage = character.MagicDamage;

    }

    public override void attack() {
        isPhysical = false;

        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            DirectionalAttack(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour in the front
            DirectionalAttack(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            DirectionalAttack(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            DirectionalAttack(3);

        }

        // Apply damage
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");


    }

    // helper function
    private void DirectionalAttack(int neighbourIndex) {
        // check if this char is friendly or an enemy
        bool isFriendly = myTile.EntityString.Contains("friendly");

        for (int i = 1; i <= maxRange; i++) {

            if (getString(neighbourIndex, i) != "None") {

                // check if the target is in the opposite team
                if ((getTile(neighbourIndex, i).EntityString.Contains("enemy") && isFriendly) || (getTile(neighbourIndex, i).EntityString.Contains("friendly") && !isFriendly)) {


                    trans.GetComponent<Animator>().Play("Standing 1H Magic Attack");
                    UnityEngine.Object.Instantiate(Resources.Load("Spells/LightningSpark"), getPosition(getTile(neighbourIndex, i)) + new Vector3(0f, 0.5f, 0f), Quaternion.identity);

                    attackTiles.Add(getString(neighbourIndex, i));

                }
            }
        }

        // If no valid target, then do nothing
    }
}


public class ArcaneBlast : Abilities {

    public ArcaneBlast(Cell cell, BaseCharacter character) : base(cell, character) {

        description = "Arcane Blast\nBlast the area, damaging all adjacent enemy targets.";
        maxRange = 1;
        damage = character.MagicDamage;

    }

    // area attack
    public override void attack() {
        isPhysical = false;

        trans.GetComponent<Animator>().Play("Standing 2H Area magic");
        UnityEngine.Object.Instantiate(Resources.Load("Spells/ArcaneBlast"), trans.position + new Vector3(0.0F, 0.5F, 0.0F), trans.rotation);

        bool isFriendly = myTile.EntityString.Contains("friendly");

        for (int x = 0; x <= 3; x++) {
            if (myTile.Neighbours[x] != "None") {

                // check if the target is in the opposite team
                if ((getTile(x, 1).EntityString.Contains("enemy") && isFriendly) || (getTile(x, 1).EntityString.Contains("friendly") && !isFriendly)) {

                    attackTiles.Add(myTile.Neighbours[x]);

                }
            }

        }

        // Apply damage
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");


    }


}


public class Sleep : Abilities {

    public Sleep(Cell cell, BaseCharacter character) : base(cell, character) {

        description = "Sleep\nCause all targets infront of you to sleep and skip their next turn.";
        maxRange = 4;
        damage = 0;

    }

    public override void attack() {
        isPhysical = false;

        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            DirectionalAttack(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour in the front
            DirectionalAttack(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            DirectionalAttack(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            DirectionalAttack(3);

        }

    }

    // helper function: effect a target in a range of 4
    private void DirectionalAttack(int neighbourIndex) {
        // check if this char is friendly or an enemy
        bool isFriendly = myTile.EntityString.Contains("friendly");

        for (int i = 1; i <= maxRange; i++) {

            if (getString(neighbourIndex, i) != "None") {

                // check if the target is in the opposite team
                if ((getTile(neighbourIndex, i).EntityString.Contains("enemy") && isFriendly) || (getTile(neighbourIndex, i).EntityString.Contains("friendly") && !isFriendly)) {

                    SC_SpellDuration.spellDuration = 3f;

                    UnityEngine.Object.Instantiate(Resources.Load("Spells/Sleep"), getPosition(getTile(neighbourIndex, i)) + new Vector3(0f, -0.2f, 0f), Quaternion.identity);
                    attackTiles.Add(getString(neighbourIndex, i));
                }
            }
        }
        trans.GetComponent<Animator>().Play("Standing 1H Magic Attack 02");
        //TODO：make the target to sleep
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "sleep");
        // If no valid target, then do nothing
    }
}


/*--------------------------------------------------------------------------------------------------------------------
* 
* Archer Abilities
* 
*---------------------------------------------------------------------------------------------------------------------
*/

public class Kick : Abilities {
    // similar to DaggerAttack of thief
    public Kick(Cell cell, BaseCharacter character) : base(cell, character) {

        description = "Kick\nKick the opponent in front of you.";
        maxRange = 1;
        damage = character.PhysicalDamage;
    }

    public override void attack() {

        isPhysical = true;

        //Default attack : Range of 1 tile on front of character
        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            directionalAttact(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour on the front
            directionalAttact(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            directionalAttact(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            directionalAttact(3);
        }

        trans.GetComponent<Animator>().Play("Kick");
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");
    }

    // helper function
    private void directionalAttact(int neighbourIndex) {
        if (myTile.Neighbours[neighbourIndex] != "None")
            attackTiles.Add(myTile.Neighbours[neighbourIndex]);

    }
}

public class IceArrow : Abilities {

    public IceArrow(Cell cell, BaseCharacter character) : base(cell, character) {
        description = "Ice Arrow\nShoot an ice arrow that damages all targets in front of you.";
        minRange = 2;
        maxRange = 6;
        damage = character.PhysicalDamage;
    }

    public override void attack() {
        isPhysical = true;


        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            DirectionalAttack(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour in the front
            DirectionalAttack(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            DirectionalAttack(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            DirectionalAttack(3);

        }

        // Apply damage
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");

    }

    // helper function
    private void DirectionalAttack(int neighbourIndex) {
        // get directional offset for the spell prefab
        Vector3 vOffset;
        if (neighbourIndex == 0) {
            vOffset = new Vector3(0f, 0.75f, 0.5f);
        } else if (neighbourIndex == 1) {
            vOffset = new Vector3(-0.5f, 0.75f, 0f);
        } else if (neighbourIndex == 2) {
            vOffset = new Vector3(0f, 0.75f, -0.5f);
        } else {
            vOffset = new Vector3(0.5f, 0.75f, 0f);
        }

        // check if this char is friendly or an enemy
        bool isFriendly = myTile.EntityString.Contains("friendly");

        for (int i = 2; i <= maxRange; i++) {

            if (getString(neighbourIndex, i) != "None") {

                // check if the target is in the opposite team
                if ((getTile(neighbourIndex, i).EntityString.Contains("enemy") && isFriendly) || (getTile(neighbourIndex, i).EntityString.Contains("friendly") && !isFriendly)) {

                    trans.GetComponent<Animator>().Play("Shoot");
                    SC_SpellDuration.spellDuration = (float)i / 6f + 1f;
                    UnityEngine.Object.Instantiate(Resources.Load("Spells/IceArrow"), trans.position + vOffset, trans.rotation);
                    attackTiles.Add(getString(neighbourIndex, i));
                }
            }
        }
        // If no valid target, then do nothing
    }
}


public class Fog : Abilities {

    public Fog(Cell cell, BaseCharacter character) : base(cell, character) {

        description = "Fog\nCreate a poisonous gas around each enemy in front of you.";
        maxRange = 3;
        damage = character.MagicDamage;

    }

    // similar to lightning
    public override void attack() {
        isPhysical = false;

        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            DirectionalAttack(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour in the front
            DirectionalAttack(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            DirectionalAttack(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            DirectionalAttack(3);

        }

        // Apply damage
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");


    }

    // helper function: attack a target in a range of 3
    private void DirectionalAttack(int neighbourIndex) {
        // check if this char is friendly or an enemy
        bool isFriendly = myTile.EntityString.Contains("friendly");

        for (int i = 1; i <= maxRange; i++) {

            if (getString(neighbourIndex, i) != "None") {

                // check if the target is in the opposite team
                if ((getTile(neighbourIndex, i).EntityString.Contains("enemy") && isFriendly) || (getTile(neighbourIndex, i).EntityString.Contains("friendly") && !isFriendly)) {


                    trans.GetComponent<Animator>().Play("Swing");
                    SC_SpellDuration.spellDuration = 3f;
                    UnityEngine.Object.Instantiate(Resources.Load("Spells/Fog"), getPosition(getTile(neighbourIndex, i)) + new Vector3(0f, 0.5f, 0f), Quaternion.identity);

                    attackTiles.Add(getString(neighbourIndex, i));

                }
            }
        }

        // If no valid target, then do nothing
    }
}

public class BladeWind : Abilities {

    public BladeWind(Cell cell, BaseCharacter character) : base(cell, character) {

        description = "Blade Wind\nSummon a gale of wind in front of you, damaging all opponents.";
        maxRange = 4;
        damage = character.MagicDamage;

    }

    // similar to lightning
    public override void attack() {
        isPhysical = false;

        if (rot.y < 0 + offset && rot.y > 0 - offset) {
            // neighbour on the right
            DirectionalAttack(0);
        } else if (rot.y < 270 + offset && rot.y > 270 - offset) {
            // neighbour in the front
            DirectionalAttack(1);
        } else if (rot.y < 180 + offset && rot.y > 180 - offset) {
            DirectionalAttack(2);
        } else if (rot.y < 90 + offset && rot.y > 90 - offset) {
            DirectionalAttack(3);

        }

        // Apply damage
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical, damage, "");


    }

    // helper function: 
    private void DirectionalAttack(int neighbourIndex) {
        // check if this char is friendly or an enemy
        bool isFriendly = myTile.EntityString.Contains("friendly");

        for (int i = 1; i <= maxRange; i++) {

            if (getString(neighbourIndex, i) != "None") {

                // check if the target is in the opposite team
                if ((getTile(neighbourIndex, i).EntityString.Contains("enemy") && isFriendly) || (getTile(neighbourIndex, i).EntityString.Contains("friendly") && !isFriendly)) {


                    trans.GetComponent<Animator>().Play("Swing");
                    SC_SpellDuration.spellDuration = 3f;
                    UnityEngine.Object.Instantiate(Resources.Load("Spells/BladeWind"), getPosition(getTile(neighbourIndex, i)), Quaternion.identity);

                    attackTiles.Add(getString(neighbourIndex, i));

                }
            }
        }

        // If no valid target, then do nothing
    }
}