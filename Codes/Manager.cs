using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Camera camera;
    public GameObject title;
    public GameObject subSystem;
    public GameObject hammerPanel;
    public GameObject changePanel;
    public GameObject[] changeAds;
    public GameObject[] lines;
    public GameObject[] arrows;
    public GameObject[] buttons;
    public GameObject pauseWall;
    public GameObject pauseButton;

    public GameObject[] grid;
    public GameObject squarePref;
    public GameObject squares;
    public GameObject pluses;
    public GameObject nextSquare;
    public GameObject pointText;
    public GameObject highPointText;
    public GameObject pause;
    public GameObject plusValuePref;
    public GameObject playText;
    public GameObject exitButton;
    public GameObject colorButton;
    public GameObject panelWithPoints;
    public GameObject infoPanel;
    public GameObject hammerRequestPanel;
    public GameObject changeRequestPanel;
    public GameObject levelInfo;

    public GameObject hammerButton;
    public GameObject changeButton;

    public Ads ads;

    GameObject[] totalSquare;

    int changeValue;

    public int point;
    int pointAnim;
    int selection;
    int rowNumber;

    public bool pointAnimPermission;

    public Themes themes;

    bool run = true;

    bool nextSquareMove;
    bool valueSquareMove;

    bool plusGrowth;
    bool infoPanelOpen;
    bool infoPanelOpened;

    bool hammerRequestPanelOpen;
    bool changeRequestPanelOpen;

    public bool operation;
    public int operationNumber;

    int themeSelection;

    float finalCounter;

    float pathSelectionCounter = 2.0f;
    float adCounter = 60.0f;

    public int[] squareQuantity = new int[25];
    public int level;


    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (PlayerPrefs.GetInt("Half") == 1)
        {
            playText.SetActive(false);
        }
        else
        {
            playText.SetActive(true);
        }

        Application.targetFrameRate = 60;

        if (!PlayerPrefs.HasKey("Hammer"))
        {
            LoadHammer(0, true, true);
        }
        else
        {
            LoadHammer(0, false, true);
        }

        if (!PlayerPrefs.HasKey("Change"))
        {
            LoadChange(0, true, true);
        }
        else
        {
            LoadChange(0, false, true);
        }
    }


    void Update()
    {
        if (run)
        {
            if (themes.themes.Count > 0)
            {
                Startup();
                NextSquareSet(false, true);

                if (PlayerPrefs.GetInt("Half") == 1)
                {
                    for (int a = 0; a < grid.Length; a++)
                    {
                        if (PlayerPrefs.GetInt("Grid" + a) == 99)
                        {
                            grid[a].GetComponent<Grid>().square = null;
                        }
                        else
                        {
                            GameObject square = Instantiate(squarePref, grid[a].transform.position, Quaternion.identity);

                            grid[a].GetComponent<Grid>().square = square;

                            square.transform.SetParent(squares.transform);
                            square.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                            square.GetComponent<Square>().manager = gameObject;
                            square.GetComponent<Square>().pointText = pointText;
                            square.GetComponent<Square>().plusValuePref = plusValuePref;
                            square.GetComponent<Square>().pluses = pluses;
                            square.GetComponent<Square>().startPoint = grid[a];
                            square.GetComponent<Square>().themes = themes;
                            square.GetComponent<Square>().themeSelection = themeSelection;
                            square.GetComponent<Square>().selection = PlayerPrefs.GetInt("Grid" + a);
                            square.GetComponent<Square>().rowNumber = PlayerPrefs.GetInt("GridRow" + a);

                            squareQuantity[PlayerPrefs.GetInt("Grid" + a)]++;
                        }
                    }
                }

                run = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }

        if (PlayerPrefs.GetInt("HighPoint") > 0)
        {
            highPointText.SetActive(true);
        }
        else
        {
            highPointText.SetActive(false);
        }

        highPointText.GetComponent<Text>().text = "Best Score: " + PlayerPrefs.GetInt("HighPoint").ToString("N0", CultureInfo.CreateSpecificCulture("ru-RU"));

        if (!operation)
        {
            Save();
        }

        if (pointAnim < point && pointAnimPermission)
        {
            int plus = (point - pointAnim) / 2;

            if (plus < 300)
            {
                plus = 1;
            }

            pointAnim = pointAnim + plus;

            pointText.GetComponent<Text>().text = pointAnim.ToString("N0", CultureInfo.CreateSpecificCulture("ru-RU"));
            //Debug.Log("open");
        }
        else
        {
            pointAnimPermission = false;
        }

        totalSquare = GameObject.FindGameObjectsWithTag("Square");

        if (totalSquare.Length < 30)
        {
            PlayerPrefs.SetInt("Half", 1);
            finalCounter = 0.0f;
        }
        else
        {
            finalCounter = finalCounter + Time.deltaTime;
            if (finalCounter > 1.0f)
            {
                PlayerPrefs.SetInt("Half", 0);
                Final();
            }
        }

        if (nextSquareMove || valueSquareMove)
        {
            nextSquare.transform.localScale = Vector3.Lerp(nextSquare.transform.localScale, new Vector3(0.01f, 0.01f, 0.01f), Time.deltaTime * 40);
            if (nextSquare.transform.localScale.x < 0.05f)
            {
                if (valueSquareMove)
                {
                    NextSquareSet(true, false);
                }
                else
                {
                    NextSquareSet(false, false);
                }

                nextSquareMove = false;
                valueSquareMove = false;
            }
        }
        else
        {
            nextSquare.transform.localScale = Vector3.Lerp(nextSquare.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 20);
        }

        if (pathSelectionCounter < 0.7f)
        {
            pathSelectionCounter = pathSelectionCounter + Time.deltaTime;
        }

        if (adCounter < 60.0f)
        {
            adCounter = adCounter + Time.deltaTime;
        }

        if (pause.activeSelf)
        {
            if (pauseButton.transform.localScale.x > 0.99f)
            {
                plusGrowth = false;
            }
            else if (pauseButton.transform.localScale.x < 0.91f)
            {
                plusGrowth = true;
            }

            if (plusGrowth)
            {
                pauseButton.transform.localScale = Vector3.Lerp(pauseButton.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 3);
            }
            else
            {
                pauseButton.transform.localScale = Vector3.Lerp(pauseButton.transform.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime * 3);
            }
        }

        if (level == 0)
        {
            if (squareQuantity[0] == 0)
            {
                if (point > 30000)
                {
                    level = 1;
                }
            }
        }
        else if (level == 1)
        {
            if (squareQuantity[0] == 0)
            {
                if (squareQuantity[1] == 0)
                {
                    if (point > 90000)
                    {
                        level = 2;
                    }
                }
            }
            else
            {
                level = 0;
            }
        }
        else if (level == 2)
        {
            if (squareQuantity[1] == 0)
            {
                if (squareQuantity[2] == 0)
                {
                    if (point > 270000)
                    {
                        level = 3;
                    }
                }
            }
            else
            {
                level = 1;
            }
        }
        else if (level == 3)
        {
            if (squareQuantity[2] == 0)
            {
                if (squareQuantity[3] == 0)
                {
                    if (point > 800000)
                    {
                        level = 4;
                    }
                }
            }
            else
            {
                level = 2;
            }
        }
        else if (level == 4)
        {
            if (squareQuantity[3] != 0)
            {
                level = 3;
            }
        }

        levelInfo.GetComponent<Text>().text = "Level " + (level + 1);

        if (infoPanelOpen)
        {
            infoPanel.transform.localScale = Vector2.MoveTowards(infoPanel.transform.localScale, new Vector2(1, 1), Time.deltaTime * 10);
        }
        else
        {
            infoPanel.transform.localScale = Vector2.MoveTowards(infoPanel.transform.localScale, new Vector2(1, 0), Time.deltaTime * 10);
        }

        if (hammerRequestPanelOpen)
        {
            hammerRequestPanel.transform.localScale = Vector2.MoveTowards(hammerRequestPanel.transform.localScale, new Vector2(1, 1), Time.deltaTime * 10);
        }
        else
        {
            hammerRequestPanel.transform.localScale = Vector2.MoveTowards(hammerRequestPanel.transform.localScale, new Vector2(1, 0), Time.deltaTime * 10);
        }

        if (changeRequestPanelOpen)
        {
            changeRequestPanel.transform.localScale = Vector2.MoveTowards(changeRequestPanel.transform.localScale, new Vector2(1, 1), Time.deltaTime * 10);
        }
        else
        {
            changeRequestPanel.transform.localScale = Vector2.MoveTowards(changeRequestPanel.transform.localScale, new Vector2(1, 0), Time.deltaTime * 10);
        }
    }


    public void Refresh()
    {
        LoadHammer(0, false, true);
        LoadChange(0, false, true);
        level = 0;

        for (int sq = 0; sq < squareQuantity.Length; sq++)
        {
            squareQuantity[sq] = 0;
        }

        totalSquare = GameObject.FindGameObjectsWithTag("Square");

        if (totalSquare.Length > 0)
        {
            for (int s = 0; s < totalSquare.Length; s++)
            {
                Destroy(totalSquare[s]);
            }
        }

        run = true;
    }


    public void Startup()
    {
        point = PlayerPrefs.GetInt("Point");
        if (point > 99)
        {
            pointAnim = point - 100;
        }
        else
        {
            pointAnim = 0;
        }

        pointText.GetComponent<Text>().text = pointAnim.ToString("N0", CultureInfo.CreateSpecificCulture("ru-RU"));

        grid = GameObject.FindGameObjectsWithTag("Grid");

        for (int a = 0; a < grid.Length; a++)
        {
            grid[a].GetComponent<Grid>().square = null;
        }

        rowNumber = PlayerPrefs.GetInt("RowNumber");
        operation = false;

        ColorSetting(false);

        finalCounter = 0;
        pause.SetActive(false);
        nextSquare.GetComponent<Button>().enabled = false;
        nextSquare.transform.GetChild(0).gameObject.SetActive(true);
        nextSquare.transform.GetChild(1).gameObject.SetActive(false);
    }


    public void PathSelection(GameObject start)
    {
        //Debug.Log(/**/);
        if (pathSelectionCounter > 0.5f)
        {
            if (start.GetComponent<Grid>().square == null)
            {
                GameObject square = Instantiate(squarePref, start.transform.position, Quaternion.identity);

                square.GetComponent<Square>().takeExisting = false;
                start.GetComponent<Grid>().square = square;

                square.transform.SetParent(squares.transform);
                square.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                square.GetComponent<Square>().manager = gameObject;
                square.GetComponent<Square>().pointText = pointText;
                square.GetComponent<Square>().plusValuePref = plusValuePref;
                square.GetComponent<Square>().pluses = pluses;
                square.GetComponent<Square>().startPoint = start;
                square.GetComponent<Square>().themes = themes;
                square.GetComponent<Square>().themeSelection = themeSelection;
                square.GetComponent<Square>().selection = selection;
                square.GetComponent<Square>().rowNumber = rowNumber;

                squareQuantity[selection]++;

                rowNumber++;

                nextSquareMove = true;
            }
            else if (start.GetComponent<Grid>().square.transform.GetChild(0).GetComponent<Text>().text == nextSquare.transform.GetChild(0).GetComponent<Text>().text)
            {
                GameObject square = Instantiate(squarePref, start.transform.position, Quaternion.identity);

                square.GetComponent<Square>().takeExisting = true;

                square.transform.SetParent(squares.transform);
                square.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                square.GetComponent<Square>().manager = gameObject;
                square.GetComponent<Square>().pointText = pointText;
                square.GetComponent<Square>().plusValuePref = plusValuePref;
                square.GetComponent<Square>().pluses = pluses;
                square.GetComponent<Square>().startPoint = start;
                square.GetComponent<Square>().themes = themes;
                square.GetComponent<Square>().themeSelection = themeSelection;
                square.GetComponent<Square>().selection = selection;
                square.GetComponent<Square>().rowNumber = rowNumber;

                squareQuantity[selection]++;

                rowNumber++;

                nextSquareMove = true;
            }

            pathSelectionCounter = 0;
        }

        playText.SetActive(false);
    }


    void NextSquareSet(bool assign, bool available)
    {
        if (available)
        {
            selection = PlayerPrefs.GetInt("NextSquare");
        }
        else
        {
            if (assign)
            {
                selection = changeValue;
            }
            else
            {
                if (level == 4)
                {
                    selection = Random.Range(4, 10);
                }
                else if (level == 3)
                {
                    selection = Random.Range(3, 9);
                }
                else if (level == 2)
                {
                    selection = Random.Range(2, 8);
                }
                else if (level == 1)
                {
                    selection = Random.Range(1, 7);
                }
                else
                {
                    selection = Random.Range(0, 6);
                }
            }
        }

        nextSquare.GetComponent<Image>().color = themes.themes[themeSelection].squares[selection].color;
        nextSquare.transform.GetChild(0).GetComponent<Text>().text = themes.themes[themeSelection].squares[selection].count;
    }


    void Save()
    {
        if (PlayerPrefs.GetInt("Half") == 1)
        {
            for (int a = 0; a < grid.Length; a++)
            {
                if (grid[a].GetComponent<Grid>().square == null)
                {
                    PlayerPrefs.SetInt("Grid" + a, 99);
                }
                else
                {
                    PlayerPrefs.SetInt("Grid" + a, grid[a].GetComponent<Grid>().square.GetComponent<Square>().selection);
                    PlayerPrefs.SetInt("GridRow" + a, grid[a].GetComponent<Grid>().square.GetComponent<Square>().rowNumber);
                }
            }

            PlayerPrefs.SetInt("NextSquare", selection);
            PlayerPrefs.SetInt("RowNumber", rowNumber);
            PlayerPrefs.SetInt("Point", point);
        }
        else
        {
            for (int a = 0; a < grid.Length; a++)
            {
                PlayerPrefs.SetInt("Grid" + a, 99);
            }

            if (PlayerPrefs.GetInt("HighPoint") < PlayerPrefs.GetInt("Point"))
            {
                PlayerPrefs.SetInt("HighPoint", PlayerPrefs.GetInt("Point"));
            }

            PlayerPrefs.SetInt("NextSquare", 0);
            PlayerPrefs.SetInt("RowNumber", 0);
            PlayerPrefs.SetInt("Point", 0);
        }
    }



    public void ColorSetting(bool renkSecimi)
    {
        if (renkSecimi)
        {
            if (PlayerPrefs.GetInt("ThemeSelection") < themes.themes.Count - 1)
            {
                PlayerPrefs.SetInt("ThemeSelection", themeSelection + 1);
            }
            else
            {
                PlayerPrefs.SetInt("ThemeSelection", 0);
            }
        }

        themeSelection = PlayerPrefs.GetInt("ThemeSelection");

        camera.backgroundColor = themes.themes[themeSelection].bgColor;
        pauseWall.GetComponent<Image>().color = new Color32(themes.themes[themeSelection].bgColor.r, themes.themes[themeSelection].bgColor.g, themes.themes[themeSelection].bgColor.b, 150);
        subSystem.GetComponent<Image>().color = new Color32(themes.themes[themeSelection].textColor.r, themes.themes[themeSelection].textColor.g, themes.themes[themeSelection].textColor.b, 50);

        hammerPanel.transform.GetChild(0).GetComponent<Image>().color = new Color32(themes.themes[themeSelection].textColor.r, themes.themes[themeSelection].textColor.g, themes.themes[themeSelection].textColor.b, 50);
        hammerPanel.transform.GetChild(1).GetComponent<Text>().color = themes.themes[themeSelection].textColor;
        hammerPanel.transform.GetChild(2).GetComponent<Image>().color = themes.themes[themeSelection].textColor;
        //hammerPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().color = themes.themes[themeSelection].bgColor;

        changePanel.transform.GetChild(0).GetComponent<Image>().color = themes.themes[themeSelection].bgColor;
        changePanel.transform.GetChild(1).GetComponent<Image>().color = new Color32(themes.themes[themeSelection].textColor.r, themes.themes[themeSelection].textColor.g, themes.themes[themeSelection].textColor.b, 50);
        changePanel.transform.GetChild(2).GetComponent<Text>().color = themes.themes[themeSelection].textColor;
        changePanel.transform.GetChild(3).GetComponent<Image>().color = themes.themes[themeSelection].textColor;

        for (int d = 0; d < changeAds.Length; d++)
        {
            changeAds[d].GetComponent<Image>().color = themes.themes[themeSelection].squares[d].color;
        }

        for (int c = 0; c < lines.Length; c++)
        {
            lines[c].GetComponent<Image>().color = new Color32(themes.themes[themeSelection].textColor.r, themes.themes[themeSelection].textColor.g, themes.themes[themeSelection].textColor.b, 50);
        }

        for (int o = 0; o < arrows.Length; o++)
        {
            arrows[o].GetComponent<Image>().color = new Color32(themes.themes[themeSelection].textColor.r, themes.themes[themeSelection].textColor.g, themes.themes[themeSelection].textColor.b, 50);
        }

        pointText.GetComponent<Text>().color = themes.themes[themeSelection].textColor;
        highPointText.GetComponent<Text>().color = themes.themes[themeSelection].textColor;
        levelInfo.GetComponent<Text>().color = themes.themes[themeSelection].textColor;

        nextSquare.GetComponent<Image>().color = themes.themes[themeSelection].squares[selection].color;
        nextSquare.transform.GetChild(0).GetComponent<Text>().text = themes.themes[themeSelection].squares[selection].count;

        totalSquare = GameObject.FindGameObjectsWithTag("Square");

        if (totalSquare.Length > 0)
        {
            for (int r = 0; r < totalSquare.Length; r++)
            {
                totalSquare[r].GetComponent<Image>().color = themes.themes[themeSelection].squares[totalSquare[r].GetComponent<Square>().selection].color;
            }
        }

        exitButton.GetComponent<Image>().color = themes.themes[themeSelection].textColor;
        colorButton.GetComponent<Image>().color = themes.themes[themeSelection].textColor;

        hammerButton.GetComponent<Image>().color = themes.themes[themeSelection].textColor;
        changeButton.GetComponent<Image>().color = themes.themes[themeSelection].textColor;

        pauseButton.GetComponent<Image>().color = themes.themes[themeSelection].textColor;
        pauseButton.transform.GetChild(0).GetComponent<Image>().color = themes.themes[themeSelection].bgColor;

        infoPanel.GetComponent<Image>().color = themes.themes[themeSelection].bgColor;
        infoPanel.transform.GetChild(0).GetComponent<Text>().color = themes.themes[themeSelection].textColor;
        infoPanel.transform.GetChild(1).GetComponent<Text>().color = new Color32(themes.themes[themeSelection].textColor.r, themes.themes[themeSelection].textColor.g, themes.themes[themeSelection].textColor.b, 150);
        infoPanel.transform.GetChild(2).GetComponent<Text>().color = new Color32(themes.themes[themeSelection].textColor.r, themes.themes[themeSelection].textColor.g, themes.themes[themeSelection].textColor.b, 100);
    }

    public void HammerRequestPanelStatus(bool open)
    {
        if (open)
        {
            hammerRequestPanelOpen = true;
        }
        else
        {
            hammerRequestPanelOpen = false;
        }
    }

    public void HammerRequest()
    {
        hammerRequestPanelOpen = false;

        if (ads.hammerAd.IsLoaded())
        {
            ads.hammerAd.Show();
        }
        ads.HammerRequest();
    }


    public void LoadHammer(int number, bool fill, bool show)
    {
        if (fill)
        {
            PlayerPrefs.SetInt("Hammer", 5);
        }
        else
        {
            PlayerPrefs.SetInt("Hammer", PlayerPrefs.GetInt("Hammer") - number);

            if (PlayerPrefs.GetInt("Hammer") < 0)
            {
                PlayerPrefs.SetInt("Hammer", 0);
            }
        }

        hammerButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("Hammer").ToString();

        if (hammerButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text == "0")
        {
            hammerButton.SetActive(false);
        }
        else
        {
            if (show)
            {
                hammerButton.SetActive(true);
            }
            else
            {
                hammerButton.SetActive(false);
            }
        }
    }


    public void Hammer(bool close)
    {
        if (close)
        {
            hammerPanel.SetActive(false);

            for (int b = 0; b < buttons.Length; b++)
            {
                buttons[b].SetActive(true);
            }

            subSystem.SetActive(true);
            title.SetActive(true);
        }
        else
        {
            LoadHammer(1, false, true);
            for (int b = 0; b < buttons.Length; b++)
            {
                buttons[b].SetActive(false);
            }

            subSystem.SetActive(false);
            title.SetActive(false);
            hammerPanel.SetActive(true);
        }
    }

    public void ChangeRequestPanelStatus(bool open)
    {
        if (open)
        {
            changeRequestPanelOpen = true;
        }
        else
        {
            changeRequestPanelOpen = false;
        }
    }

    public void ChangeRequest()
    {
        changeRequestPanelOpen = false;

        if (ads.changeAd.IsLoaded())
        {
            ads.changeAd.Show();
        }
        ads.ChangeRequest();
    }


    public void LoadChange(int number, bool fill, bool show)
    {
        if (fill)
        {
            PlayerPrefs.SetInt("Change", 5);
        }
        else
        {
            PlayerPrefs.SetInt("Change", PlayerPrefs.GetInt("Change") - number);

            if (PlayerPrefs.GetInt("Change") < 0)
            {
                PlayerPrefs.SetInt("Change", 0);
            }
        }

        changeButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("Change").ToString();

        if (changeButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text == "0")
        {
            changeButton.SetActive(false);
        }
        else
        {
            if (show)
            {
                changeButton.SetActive(true);
            }
            else
            {
                changeButton.SetActive(false);
            }
        }
    }


    public void NextSquareChange(int value)
    {
        Change(true);
        changeValue = value;
        valueSquareMove = true;
        //NextSquareSet(value, true, false);
    }


    public void Change(bool close)
    {
        if (close)
        {
            changePanel.SetActive(false);
            subSystem.SetActive(true);
            title.SetActive(true);
        }
        else
        {
            LoadChange(1, false, true);

            subSystem.SetActive(false);
            title.SetActive(false);
            changePanel.SetActive(true);
        }
    }


    public void CallAds()
    {
        if (adCounter > 59.0f)
        {
            if (PlayerPrefs.GetInt("Visit") != 1 && !infoPanelOpened)
            {
                infoPanelOpen = true;
            }
            else
            {
                if (ads.interstitialad.IsLoaded())
                {
                    ads.interstitialad.Show();
                }
                ads.InterstitialsRequest();
            }

            adCounter = 0;
        }
    }


    public void Info(bool goToLink)
    {
        if (goToLink)
        {
            Application.OpenURL("market://details?id=com.reysadijital.mergeblock");
            PlayerPrefs.SetInt("Visit", 1);
        }
        else
        {
            if (infoPanelOpen)
            {
                infoPanelOpen = false;
            }
            else
            {
                infoPanelOpen = true;
            }

            infoPanelOpened = true;
        }
    }


    public void Final()
    {
        LoadHammer(0, true, false);
        LoadChange(0, true, false);
        Save();
        pause.SetActive(true);
        //nextSquare.GetComponent<Button>().enabled = true;
        //nextSquare.transform.GetChild(0).gameObject.SetActive(false);
        //nextSquare.transform.GetChild(1).gameObject.SetActive(true);
    }


    public void Exit()
    {
        Application.Quit();
    }
}
