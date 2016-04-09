using UnityEngine;
using System.Collections.Generic;
using TileDraw.Map;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{

    public Animator animator;
    public GameObject spell;
    public GameObject[] spells;     //spells the player can cast
    public enum weapons { sword, spear, crossbow, tome, none };
    //Text
    public Font myFont;
    private float currentTime = 0.0f; //Time text starts to display
    private float textTime = 5.0f; //Time text is displayed for
    //Movement
    private Vector3 startPos;
    private float startTime = 0.0f; //Time start to move
    public float duration = 1f; //Moving duration
    private bool showPaths;
    private bool waitForUserInput;
    //Booleans
    private bool isMoving = false;
    private bool isTurning = false;
    private bool isAttacking = false;
    public bool isDead { get; set; }
    public bool myTurn;
    //Booleans : GUI
    private bool showLabel1 = false;
    private bool showLabel2 = false;
    private bool showLabel3 = false;
    private bool showLabel4 = false;
    //Class Information
    public BaseCharacter myClass;
    public int myAgility;
    public int myStrength;
    public int myIntellect;
    public float myHealth;
    public float myMana;
    public int myDefense;
    //Other classes/Objects needed
    private PathFinding pf;
    private Cell c;
    private Weapons weapon;
    //action menu
    private GameObject actionBar;
    public Button a1;
    private Button a2;
    private Button a3;
    private Button a4;
    private Button next;
    //Path to shield and melee objects
    private string melee;
    private string shield;
    private string melee1 = "Root2/Spine/Chest/r_clavicle/r_shoulder/r_elbow/r_wrist/r_hand/Melee";
    private string shield1 = "Root2/Spine/Chest/l_clavicle/l_shoulder/l_elbow/l_wrist/Shield Mount";
    //private string melee3 = "mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/Melee";
    //private string shield3 = "mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:LeftShoulder/mixamorig:LeftArm/mixamorig:LeftForeArm/mixamorig:LeftHand/Shield Mount";
    private string melee2 = "Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Melee"; 
    private string shield2 = "Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Shield Mount";
    //Attack Information
    //public List<string> attackTiles; --> abilities class
    private float myPhysicalDamage;
    private float myMagicDamage;
    //Abilities
    SpearAttack sp;
    DaggerAttack da;
    

    void Start()
    {
        //Instantiate
        spells = Resources.LoadAll<GameObject>("Spells");   //load spells from resources into spell array
        pf = transform.parent.GetComponentInParent<PathFinding>();
        c = transform.parent.parent.Find("(0,0)").GetComponent<Cell>();
        animator = GetComponent<Animator>();

        //action menu
        actionBar= transform.parent.parent.Find("Canvas/Panel").gameObject;
        a1 = actionBar.transform.GetChild(0).GetComponent<Button>();
        a2 = actionBar.transform.GetChild(1).GetComponent<Button>();
        a3 = actionBar.transform.GetChild(2).GetComponent<Button>();
        a4 = actionBar.transform.GetChild(3).GetComponent<Button>();

        //Set player stats
        myAgility = 100;
        myStrength = 0;
        myIntellect = 0;
        myHealth = 100;

        //Set tile entity string
        Vector2 myPointInCell = c.convertWorldPosToIndex(transform.position.x, transform.position.z);
        Tile myTile = c.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
        myTile.SetEntityString(tag);

        //Damage calculations
        myPhysicalDamage = 10 + (myStrength / 100 * 30);
        myMagicDamage = 10 + (myIntellect / 100 * 30);

        isDead = false;
        myTurn = false;
        animator.SetBool("grounded", true);

        //Set melee and shield game object paths
        if (tag == "enemyCharacter1")
        {
            melee = melee2;
            shield = shield2;
        }
        else if (tag == "friendlyCharacter1")
        {
            melee = melee1;
            shield = shield1;
        }

        //Set correct (default) weapon
        weapon = transform.Find(melee).GetComponent<Weapons>();
        sp = new SpearAttack(c);
        da = new DaggerAttack(c);

        if (myClass.PlayerClass == BaseCharacterClass.CharacterClasses.Archer)
        {
            weapon.setCurrentWeapon((int)weapons.crossbow);
        }
        else if (myClass.PlayerClass == BaseCharacterClass.CharacterClasses.Apprentice)
        {
            weapon.setCurrentWeapon((int)weapons.tome);
        }
        else if (myClass.PlayerClass == BaseCharacterClass.CharacterClasses.Thief)
        {
            weapon.setCurrentWeapon((int)weapons.sword);
            transform.Find(shield).GetComponent<ShieldManager>().hasSword = true;
            myClass.skills.Add(da);     
        }
        else if (myClass.PlayerClass == BaseCharacterClass.CharacterClasses.Squire)
        {
            weapon.setCurrentWeapon((int)weapons.spear);
            //transform.Find(shield).GetComponent<ShieldManager>().hasShield = true;
            myClass.skills.Add(sp);
        }

        //Debug.Log(gameObject.name + " : current Weapon : " + weapon.getCurrentWeapon());
    }
    /*Attack
     *@param tiles : The list of tiles to apply damage to.  
     *@param damage : The damage to be applied.      
     */
    public void attack( List<string> tiles, bool isPhysical)
    {
        isAttacking = false;

        foreach ( string str in tiles)
        {
            Debug.Log("[ " + str + " ]" );
            Tile t = c.ConvertStringToTile(str);

            //Check if there is a player in the tile
            if ((t.GetEntityString().Contains("enemy") && tag.Contains("friendly")) || (t.GetEntityString().Contains("friendly") && tag.Contains("enemy")))
            {
                //Get transform of gameobject in tile
                float rayLength = 5f;
                Vector2 pos = c.GetPointInCellFromTileIndex(t.TileIndex);
                Vector2 worldPos = c.convertIndexToWorldPos((int)pos.x, (int)pos.y);
                Vector3 start = new Vector3(worldPos.x,t.GetHeight(),worldPos.y) + (rayLength * Vector3.up);
                RaycastHit hit;
                Debug.DrawRay(start, (-Vector3.up) * rayLength, Color.cyan, 20);
                if (Physics.Raycast(start, -Vector3.up, out hit, rayLength))
                {
                    if ( isPhysical )
                        hit.transform.SendMessage("applyDamage", myPhysicalDamage, SendMessageOptions.DontRequireReceiver);
                    else
                        hit.transform.SendMessage("applyDamage", myMagicDamage, SendMessageOptions.DontRequireReceiver);
                    
                }
                else
                {
                    throw new UnityException ("ERROR : getting transform of object in tile --> Produced by Toni");
                }
            }
        }
        exitAttackPhase();

    }
    /*Apply Damage
     *@param theDamage: Damage to apply.
     */
    public void applyDamage(int theDamage)
    {
        //Defense mitigates damage
        myHealth -= theDamage;
    }
    //Moves Player
    private void move()
    {
        int error = -9; //Starting error

        //Show paths
        if (showPaths)
        {
            Instantiate(Resources.Load("SquareProjector"), new Vector3(transform.position.x, 1, transform.position.z), Quaternion.Euler(90, 0, 0));
            pf.showAvailableMoves(transform.position, myAgility);
            showPaths = false;
            waitForUserInput = true;
            
        }
        //Do untill mouse is clicked
        if(waitForUserInput) {
              
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                error = pf.FindPath(transform.position, hit.point);
            }
        }
        // Moves the Player if the Left Mouse Button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Error
            if (error == -9)
            {
                return;
            }

            if (error == -1)
            {
                showLabel4 = true;
                currentTime = Time.time;
                return;
            }

            animator.SetFloat("Run", 2.0f);
            waitForUserInput = false;
            //Change current tile str to ""
            Vector2 myPointInCell = c.convertWorldPosToIndex(transform.position.x, transform.position.z);
            Tile myTile = c.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
            myTile.SetEntityString("");
            //Destroy red prjector
            Destroy(GameObject.FindGameObjectWithTag("SquareProjector"));
            //Destroy all green projectors
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("GreenProj");
            for (var i = 0; i < objectsToDestroy.Length; i++)
                Destroy(objectsToDestroy[i]);

        }
        //Apply movement as long as movement path has elements
        if (!waitForUserInput && pf.mPath.Count > 0 )
        {
            Vector2 wp = c.GetPointInCellFromTileIndex(pf.mPath[0].TileIndex);
            Vector2 dest = c.convertIndexToWorldPos((int)wp.x, (int)wp.y);
            float height = pf.mPath[0].GetHeight();
            //Adjust height if using specific prefab
            if (tag == "enemyCharacter1")
                height += 0.05f;
            Vector3 d = new Vector3(dest.x, height, dest.y); //Vector3 -> The destination

            //4 possible locations of next tile in path
            Vector3 rot = transform.rotation.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            //Turn down
            if (startPos.z == d.z && startPos.x < d.x)
                rot.y = 90;
            //Turn up
            else if (startPos.z == d.z && startPos.x > d.x)
                rot.y = 270;
            //Turn right
            else if (startPos.x == d.x && startPos.z < d.z)
                rot.y = 0;
            //Turn left
            else if (startPos.x == d.x && startPos.z > d.z)
                rot.y = 180;
            transform.rotation = Quaternion.Euler(rot);

            //First movement : Make sure correct animation is playing before we start
            if (startTime == 0.0f)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run CB") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run SP") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run SW"))
                {
                    startTime = Time.time;
                    startPos = transform.position;
                }
            }
            if (startTime > 0.0f)
            {
                transform.position = Vector3.Lerp(startPos, d, (Time.time - startTime) / duration);

                //If we are at next tile remove it from movement list, and continue
                if (transform.position == d)
                {
                    startTime = Time.time;
                    startPos = transform.position;
                    pf.mPath.RemoveAt(0);
                }
                //Debug.Log("startpos = [" + startPos.x + "," + startPos.y + "," + startPos.z + "] currPos = " + transform.position.x + "," + transform.position.y + "," + transform.position.z + "] currentTime = " + (Time.time - startTime)); //DEBUG
            }
        }
        //Exiting movement phase
        if ((!waitForUserInput && pf.mPath.Count == 0))
        {
            exitMovePhase();
        }
    }
    //Exit phases functions
    public void exitMovePhase()
    {
        currentTime = Time.time;
        animator.SetFloat("Run", 0.0f);
        startTime = 0.0f;
        showLabel1 = false;
        showLabel2 = true;
        isMoving = false;
        isTurning = true;
        showPaths = true;

        //Set entity strings
        foreach (Tile x in pf.availMoveTiles)
        {
            x.EntityString = "";
        }

        Vector2 myPointInCell = c.convertWorldPosToIndex(transform.position.x, transform.position.z);
        Tile myTile = c.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
        myTile.SetEntityString(tag);

        //Destroy red prjector
        Destroy(GameObject.FindGameObjectWithTag("SquareProjector"));
        //Destroy all green projectors
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("GreenProj");
        for (var i = 0; i < objectsToDestroy.Length; i++)
            Destroy(objectsToDestroy[i]);
    }
    public void exitAttackPhase()
    {
        isAttacking = false;
        showLabel3 = false;
        currentTime = 0.0f;
        myTurn = false;
        actionBar.SetActive(false);
        a1.onClick.RemoveAllListeners();
        a2.onClick.RemoveAllListeners();
        a3.onClick.RemoveAllListeners();
        a4.onClick.RemoveAllListeners();
        next.onClick.RemoveAllListeners();
        Destroy(GameObject.FindGameObjectWithTag("SquareProjector"));
    }
    //Check Button Clicks
    public void clickButton()
    {
        if (isMoving)
        {
            //Debug.Log("Exiting move phase");
            exitMovePhase();
            return;
        }
        else if (isTurning)
        {
          
            isTurning = false;
            isAttacking = true;
            currentTime = Time.time;
            showLabel2 = false;
            showLabel3 = true;
            showPaths = true;
            //Debug.Log("Exiting turn phase" + showPaths);
            return;
        }
        else if (isAttacking)
        {
            //Debug.Log("Exiting attack phase");
            exitAttackPhase();
            return;
        }

    }
    private void skillClicked(int i)
    {
        myClass.skills[i].updateLoc(this.gameObject);
        myClass.skills[i].attack();
    }

    //Rotate Player
    private void rotate()
    {
        if (showPaths && isTurning)
        {
            Instantiate(Resources.Load("SquareProjector"), new Vector3(transform.position.x, 1, transform.position.z), Quaternion.Euler(90, 0, 0));
            showPaths = false;
        }
        
        //Rotating
        if (isTurning)
        {
            Vector3 rot = transform.rotation.eulerAngles;

            rot.x = 0;
            rot.z = 0;

            if (Input.GetKeyDown(KeyCode.RightArrow))
                rot.y += 90;

            transform.rotation = Quaternion.Euler(rot);
        }
    }

    /*
     * Attack
     * @param cwp = the current weapon equipped 
     */
    private void attack(int cwp)
    {
        if (showPaths)
        {
            actionBar.SetActive(true);
            a1.onClick.AddListener(() => skillClicked(0));
            a2.onClick.AddListener(() => skillClicked(1));
            a3.onClick.AddListener(() => skillClicked(2));
            a4.onClick.AddListener(() => skillClicked(3));

            showPaths = false;
        }
    }

    //Display GUI
    public void OnGUI()
    {
        GUI.skin.font = myFont;
        if (showLabel1)
            GUI.Label(new Rect(0, Screen.height - 20, Screen.width, Screen.height), "1: It's your turn: To move (use mouse). Click 'Next' to continue...");
        else if (showLabel2)
            GUI.Label(new Rect(0, Screen.height - 20, Screen.width, Screen.height), "2: It's your turn: To rotate (use right arrow key to rotate clockwise). Click 'Next' to continue...");
        else if (showLabel3)
            GUI.Label(new Rect(0, Screen.height - 20, Screen.width, Screen.height), "3: It's your turn: To attack. Click 'Next' to finish.");
        else if (showLabel4)
            GUI.Label(new Rect(0, Screen.height -20 , Screen.width, Screen.height), "HAHA! Thats not a valid move, funny guy! ( valid moves are shown in green )");
    }


    //Occurs every frame
    void Update()
    {

        //load current weapon from weapon class
        int cwp = transform.Find(melee).GetComponent<Weapons>().getCurrentWeapon();

        animator.SetBool("IdleSW", false);
        animator.SetBool("IdleSP", false);
        animator.SetBool("IdleCB", false);
        animator.SetBool("Idle", false);

        //Correct Idle animation
        if (cwp == 0)
        {
            animator.SetBool("IdleSW", true);
        }
        else if (cwp == 1)
        {
            animator.SetBool("IdleSP", true);
        }
        else if (cwp == 2)
        {
            animator.SetBool("IdleCB", true);
        }
        else if (cwp == 4)
        {
            animator.SetBool("Idle", true);
        }
        if (isDead)
        {
            animator.SetBool("Dead", true);
        }

        if (myTurn)
        {

            //check for jump
            if (Input.GetKeyDown("space"))
            {
                animator.SetBool("Jump", true);
            }

            //This happens only once
            if (currentTime == 0.0f)
            {
                next = transform.parent.parent.Find("Canvas/Next").GetComponent<Button>();
                next.onClick.AddListener(() => clickButton());
                isMoving = true;
                showPaths = true;
                currentTime = Time.time;
                showLabel1 = true;
            }

            //Handle Label expiration
            if (Time.time - currentTime > textTime)
            {
                if (showLabel1)
                {
                    showLabel1 = false;
                }
                if (showLabel2)
                {
                    showLabel2 = false;
                }
                if (showLabel3)
                {
                    showLabel3 = false;
                }
                if (showLabel4)
                {
                    showLabel4 = false;
                }

            }

            //MOVE
            if( isMoving )
                move();

            //ROTATE
            rotate();

            //ATTACK
            if (isAttacking)
                attack(cwp);  
        }
    }
}