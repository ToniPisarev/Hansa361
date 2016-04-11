using UnityEngine;
using System;
using System.Collections.Generic;
using TileDraw.Map;

[System.Serializable]
public abstract class Abilities
{

    protected Tile myTile;
    protected Vector2 myPointInCell;
    protected Vector3 rot;
    protected Cell grid;
    protected Transform trans;
    protected List<String> attackTiles;

    protected const int offset = 25;
    public bool isPhysical; // true: physical, false: magical

    public Abilities(Cell pCell) 
    {
        grid = pCell;
    }

    public void updateLoc(GameObject pObj)
    {

        trans = pObj.transform;
        attackTiles = new List<string>();

        myPointInCell = grid.convertWorldPosToIndex(trans.position.x, trans.position.z);
        myTile = grid.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
        rot = trans.rotation.eulerAngles;

    }

    public abstract void attack();
}

public class DaggerAttack : Abilities
{
    public DaggerAttack(Cell pCell) : base(pCell) { }

    public override void attack()
    {

        isPhysical = true;

        //This goes in ability class

        //Default attack : Range of 1 tile infront of character
        if (rot.y < 0 + offset && rot.y > 0 - offset)
        {
            // neighbour on the right
            directionalAttact(0);
        }
        else if (rot.y < 270 + offset && rot.y > 270 - offset)
        {
            // neighbour in the front
            directionalAttact(1);
        }
        else if (rot.y < 180 + offset && rot.y > 180 - offset)
        {
            directionalAttact(2);
        }
        else if (rot.y < 90 + offset && rot.y > 90 - offset)
        {
            directionalAttact(3);
        }

        trans.GetComponent<Animator>().Play("Attack SP");
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical);
    }

    // helper function
    private void directionalAttact(int neighbourIndex)
    {
        if (myTile.Neighbours[neighbourIndex] != "None")
            attackTiles.Add(myTile.Neighbours[neighbourIndex]);

    }
}

public class SpearAttack : Abilities
{

    public SpearAttack (Cell pCell) : base(pCell) { }

    public override void attack()
    {

        isPhysical = true;

        //Default attack : Range of 1 tile infront of character
        if (rot.y < 0 + offset && rot.y > 0 - offset)
        {
            // neighbour on the right
            directionalAttact(0);
        }
        else if (rot.y < 270 + offset && rot.y > 270 - offset)
        {
            // neighbour in the front
            directionalAttact(1);
        }
        else if (rot.y < 180 + offset && rot.y > 180 - offset)
        {
            directionalAttact(2);
        }
        else if (rot.y < 90 + offset && rot.y > 90 - offset)
        {
            directionalAttact(3);

        }

        trans.GetComponent<Animator>().Play("Attack SP");
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical);
    }

    // helper function
    private void directionalAttact(int neighbourIndex)
    {
        if (myTile.Neighbours[neighbourIndex] != "None")
            attackTiles.Add(myTile.Neighbours[neighbourIndex]);
        if (grid.ConvertStringToTile(myTile.Neighbours[neighbourIndex]).Neighbours[neighbourIndex] != "None")
        {
            attackTiles.Add(grid.ConvertStringToTile(myTile.Neighbours[neighbourIndex]).Neighbours[neighbourIndex]);
            if (grid.ConvertStringToTile(grid.ConvertStringToTile(myTile.Neighbours[neighbourIndex]).Neighbours[neighbourIndex]).Neighbours[neighbourIndex] != "None")
                attackTiles.Add(grid.ConvertStringToTile(grid.ConvertStringToTile(myTile.Neighbours[neighbourIndex]).Neighbours[neighbourIndex]).Neighbours[neighbourIndex]);
        }


    }
}