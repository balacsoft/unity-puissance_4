using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public const int ECHIQUIER_COLUMNS = 7;
    public const int ECHIQUIER_LINES = 6;
    public const float GAP_BETWEEN_CELLS_X = 1.4f;
    public const float GAP_BETWEEN_CELLS_Y = 1.4f;
    public const float POSITION_CELL_TOP_LEFT_X = -5.26f;
    public const float POSITION_CELL_TOP_LEFT_Y = 2.89f;

    private Vector2 vectOutOfScreen = new Vector2(-100f, -100f);

    private CaseObject[,] theCases;

    public GameObject redJeton;
    public GameObject yellowJeton;

    public GameObject redArrow;
    public GameObject yellowArrow;

    public GameObject nextPlayerButton;
    public GameObject vicText;
    public GameObject titleText;
    Text leText;
    Text leTextVictoire;

    int currentPlayer;
    int arrowColumn;
    bool gameActive;
    bool gameFinished;
    int playerNb;

    GameParams myGameParams;

    void Awake()
    {

        myGameParams = (GameParams)GameObject.FindObjectOfType(typeof(GameParams));
        playerNb = myGameParams.GetNumberOfPlayers();
    }

    // Use this for initialization
    void Start()
    {
        // Set resolution
        // Switch to 1280 x 720 windowed
        Screen.SetResolution(1280, 720, false);

        // Text
        leText = titleText.GetComponent<Text>();
        leTextVictoire = vicText.GetComponent<Text>();

        // Button
        nextPlayerButton.SetActive(false);
        gameActive = true;
        gameFinished = false;

        // Create all the cases
        theCases = new CaseObject[ECHIQUIER_COLUMNS, ECHIQUIER_LINES];

        for (int column = 0; column < ECHIQUIER_COLUMNS; column++)
        {
            for (int line = 0; line < ECHIQUIER_LINES; line++)
            {
                JetonObject jeton;
                PlatePosition pos;
                pos.x = column;
                pos.y = line;
                jeton = new JetonObject(0);
                theCases[column, line] = new CaseObject(false, jeton, pos);
            }
        }

        // First player 1
        currentPlayer = 1;
        arrowColumn = 0;
        leTextVictoire.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer == 1)
            leText.text = "PLAYER 1 (JAUNE)";
        else
        {
            if (playerNb == 2)
                leText.text = "PLAYER 2 (ROUGE)";
            else
                leText.text = "ORDINATEUR (ROUGE)";
        }
        // Position correct arrow
        SetArrow(currentPlayer, arrowColumn);

        if (!gameFinished)
        {
            if (gameActive)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    // Right pressed
                    arrowColumn++;
                    if (arrowColumn > 6)
                        arrowColumn = 6;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    // Left pressed
                    arrowColumn--;
                    if (arrowColumn < 0)
                        arrowColumn = 0;
                }
                if ((Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.KeypadEnter)) || ((playerNb == 1) && (currentPlayer == 2)))
                {
                    int ligne;
                    int val = arrowColumn;

                    // If one player, the computer plays
                    if ((playerNb == 1) && (currentPlayer == 2))
                    {
                        float fVal;
                        do
                        {
                            fVal = Random.value - 0.01f;
                            fVal = (10 * fVal);
                            val = (int)fVal;
                        } while (val > 6);
                    }

                    ligne = DropJeton(currentPlayer, val);
                    if (ligne >= 0)
                    {
                        DrawThePlateau(currentPlayer, val, ligne);
                        int v = CheckIfSomeoneWins();
                        if (v != 0)
                        {
                            SoundManager.gameSoundManager.PlaySound(SoundManager.SOUND_FINISH);
                            leTextVictoire.text = "VICTOIRE DU JOUEUR " + v;
                            gameFinished = true;
                        }
                        else
                        {
                            currentPlayer = NextPlayer(currentPlayer);
                            SoundManager.gameSoundManager.PlaySound(SoundManager.SOUND_COIN);
                        }
                    }
                }
            }

            if ((Input.GetKey(KeyCode.Space)) || (Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.LeftArrow)) || ((playerNb == 1) && (currentPlayer == 2)))
            {
                NextPlayerButton();
            }

        }
    }

    ///////////////////////////////////////////////////////////
    // PRIVATE FUNCTIONS
    ///////////////////////////////////////////////////////////

    private Vector2 GetRealPos(PlatePosition pos)
    {
        Vector2 vect;

        vect.x = ((pos.x * GAP_BETWEEN_CELLS_X) + POSITION_CELL_TOP_LEFT_X);
        vect.y = (POSITION_CELL_TOP_LEFT_Y - (pos.y * GAP_BETWEEN_CELLS_Y));

        return vect;
    }

    private void DrawThePlateau(int currentPlayer, int column, int line)
    {
        GameObject myLocalJeton;

        JetonObject jeton;
        PlatePosition pos;
        pos.x = column;
        pos.y = line;

        pos = theCases[column, line].GetCasePosition();
        jeton = theCases[column, line].GetCaseJeton();

        if (theCases[column, line].GetCaseOccupied() == true)
        {
            // Draw the jeton
            Vector3 vect3;
            Vector2 vect;
            vect = GetRealPos(pos);
            vect3.x = vect.x;
            vect3.y = vect.y;
            vect3.z = -1;
            if (jeton.GetJetonColor() == JetonObject.JETON_RED)
            {
                myLocalJeton = (GameObject)Instantiate(redJeton);
                myLocalJeton.transform.position = vect3;
            }
            else if (jeton.GetJetonColor() == JetonObject.JETON_YELLOW)
            {
                myLocalJeton = (GameObject)Instantiate(yellowJeton);
                myLocalJeton.transform.position = vect3;
            }
        }
    }

    private void SetArrow(int player, int column)
    {
        Vector3 vect3;
        Vector2 vect;
        PlatePosition pos;
        pos.x = column;
        pos.y = -1;

        vect = GetRealPos(pos);
        vect3.x = vect.x;
        vect3.y = vect.y;
        vect3.z = -2;
        if (player == 1)
        {
            yellowArrow.transform.position = vect3;
            redArrow.transform.position = vectOutOfScreen;
        }
        else
        {
            redArrow.transform.position = vect3;
            yellowArrow.transform.position = vectOutOfScreen;
        }
    }

    private int DropJeton(int player, int column)
    {
        JetonObject jeton = new JetonObject(player);

        if (theCases[column, 0].GetCaseOccupied())
            return -1;

        for (int line = 1; line < ECHIQUIER_LINES; line++)
        {
            if (theCases[column, line].GetCaseOccupied())
            {
                theCases[column, line - 1].SetCaseJeton(jeton);
                return line - 1;
            }
        }

        // Last position
        theCases[column, 5].SetCaseJeton(jeton);

        return 5;
    }

    private int NextPlayer(int curPlayer)
    {
        int next;

        if (curPlayer == 1)
        {
            next = 2;
        }
        else
        {
            next = 1;
        }

        nextPlayerButton.SetActive(true);
        gameActive = false;

        return next;
    }

    public void NextPlayerButton()
    {
        nextPlayerButton.SetActive(false);
        gameActive = true;
    }

    private int CheckIfSomeoneWins()
    {
        int laCase;
        CasePosition casePosition;

        for (int column = 0; column < ECHIQUIER_COLUMNS; column++)
        {
            for (int line = 0; line < ECHIQUIER_LINES; line++)
            {
                laCase = theCases[column, line].GetCaseJeton().GetJetonColor();
                if (theCases[column, line].GetCaseOccupied())
                {
                    for (int direction = 0; direction < 8; direction++)
                    {
                        casePosition = PositionDirection(line, column, direction + 1);
                        if (casePosition.theCase == laCase)
                        {
                            casePosition = PositionDirection(casePosition.l, casePosition.c, direction + 1);
                            if (casePosition.theCase == laCase)
                            {
                                casePosition = PositionDirection(casePosition.l, casePosition.c, direction + 1);
                                if (casePosition.theCase == laCase)
                                {
                                    return laCase;
                                }
                            }
                        }
                    }
                }
            }
        }

        return 0;
    }

    private CasePosition PositionDirection(int lig, int col, int direction)
    {
        CasePosition casePosition;
        casePosition.l = lig;
        casePosition.c = col;
        casePosition.theCase = 3;

        if ((direction == 1) && (lig > 0))
            casePosition.l = lig - 1;
        else if ((direction == 2) && (lig > 0) && (col < 6))
        {
            casePosition.l = lig - 1;
            casePosition.c = col + 1;
        }
        else if ((direction == 3) && (col < 6))
            casePosition.c = col + 1;
        else if ((direction == 4) && (lig < 5) && (col < 6))
        {
            casePosition.l = lig + 1;
            casePosition.c = col + 1;
        }
        else if ((direction == 5) && (lig < 5))
            casePosition.l = lig + 1;
        else if ((direction == 6) && (lig < 5) && (col > 0))
        {
            casePosition.l = lig + 1;
            casePosition.c = col - 1;
        }
        else if ((direction == 7) && (col > 0))
            casePosition.c = col - 1;
        else if ((direction == 8) && (lig > 0) && (col > 0))
        {
            casePosition.l = lig - 1;
            casePosition.c = col - 1;
        }
        else
            return casePosition;

        casePosition.theCase = theCases[casePosition.c, casePosition.l].GetCaseJeton().GetJetonColor();
        return casePosition;
    }


}