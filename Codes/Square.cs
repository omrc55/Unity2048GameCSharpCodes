using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    public Themes themes;

    public GameObject manager;
    public GameObject pointText;
    public GameObject pluses;
    public GameObject startPoint;

    public GameObject objectAvailable;
    public GameObject objectTop;
    public GameObject objectRight;
    public GameObject objectLeft;
    public GameObject plusValuePref;
    
    public int value;
    public int selected;
    public int rowCount;

    public int themeSelection;

    public bool takeFromTop;
    public bool takeFromRight;
    public bool takeFromLeft;
    public bool takeFromAvailable;

    bool topMotion;
    bool rightMotion;
    bool leftMotion;
    bool availableMotion;

    bool growth;

    bool showPlus;
    string plusValueText;
    bool plus;

    bool permission;

    float say;

    public bool showAd;


    void Start()
    {
        objectAvailable = startPoint;
        SquareSetting();
        permission = true;
    }


    void Update()
    {
        objectTop = null;
        objectRight = null;
        objectLeft = null;

        for (int a = 0; a < manager.GetComponent<Manager>().grid.Length; a++)
        {
            if (char.GetNumericValue(manager.GetComponent<Manager>().grid[a].name[0]) == char.GetNumericValue(objectAvailable.name[0]) - 1 && manager.GetComponent<Manager>().grid[a].name[1] == objectAvailable.name[1])
            {
                objectTop = manager.GetComponent<Manager>().grid[a];
            }

            if (manager.GetComponent<Manager>().grid[a].name[0] == objectAvailable.name[0] && char.GetNumericValue(manager.GetComponent<Manager>().grid[a].name[1]) == char.GetNumericValue(objectAvailable.name[1]) - 1)
            {
                objectLeft = manager.GetComponent<Manager>().grid[a];
            }

            if (manager.GetComponent<Manager>().grid[a].name[0] == objectAvailable.name[0] && char.GetNumericValue(manager.GetComponent<Manager>().grid[a].name[1]) == char.GetNumericValue(objectAvailable.name[1]) + 1)
            {
                objectRight = manager.GetComponent<Manager>().grid[a];
            }
        }

        if (objectTop != null && objectTop.GetComponent<Grid>().square == null)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, objectTop.transform.position, Time.deltaTime * 10);
            //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, objectTop.transform.position, Time.deltaTime);

            if((gameObject.transform.position - objectTop.transform.position).magnitude < 0.1f)
            {
                gameObject.transform.position = objectTop.transform.position;
                objectAvailable.GetComponent<Grid>().square = null;
                objectAvailable = objectTop;
                objectAvailable.GetComponent<Grid>().square = gameObject;
            }
        }
        else
        {
            Neighbors();
        }

        if (topMotion && (!manager.GetComponent<Manager>().operation || manager.GetComponent<Manager>().operationNumber == rowCount))
        {
            manager.GetComponent<Manager>().operation = true;
            manager.GetComponent<Manager>().operationNumber = rowCount;
            say = say + Time.deltaTime;
            objectTop.GetComponent<Grid>().square.GetComponent<Square>().enabled = false;
            objectTop.GetComponent<Grid>().square.GetComponent<ObjectDelete>().enabled = true;

            objectTop.GetComponent<Grid>().square.transform.position = Vector3.MoveTowards(objectTop.GetComponent<Grid>().square.transform.position, gameObject.transform.position, Time.deltaTime * 15);
            //objectTop.GetComponent<Grid>().square.transform.position = Vector3.MoveTowards(objectTop.GetComponent<Grid>().square.transform.position, gameObject.transform.position, Time.deltaTime);

            if (transform.localScale.x > 1.9f)
            {
                growth = false;
            }

            if (!growth && transform.localScale.x < 1.8f)
            {
                say = 0;
                showPlus = true;
                plus = true;

                manager.GetComponent<Manager>().squareQuantity[objectTop.GetComponent<Grid>().square.GetComponent<Square>().selected]--;

                Destroy(objectTop.GetComponent<Grid>().square);
                topMotion = false;
                permission = true;
                manager.GetComponent<Manager>().operation = false;
                manager.GetComponent<Manager>().operationNumber = 0;
            }
        }

        if (rightMotion && (!manager.GetComponent<Manager>().operation || manager.GetComponent<Manager>().operationNumber == rowCount))
        {
            manager.GetComponent<Manager>().operation = true;
            manager.GetComponent<Manager>().operationNumber = rowCount;
            say = say + Time.deltaTime;
            objectRight.GetComponent<Grid>().square.GetComponent<Square>().enabled = false;
            objectRight.GetComponent<Grid>().square.GetComponent<ObjectDelete>().enabled = true;
            objectRight.GetComponent<Grid>().square.transform.position = Vector3.MoveTowards(objectRight.GetComponent<Grid>().square.transform.position, gameObject.transform.position, Time.deltaTime * 15);
            //objectRight.GetComponent<Grid>().square.transform.position = Vector3.MoveTowards(objectRight.GetComponent<Grid>().square.transform.position, gameObject.transform.position, Time.deltaTime);

            if (transform.localScale.x > 1.9f)
            {
                growth = false;
            }

            if (!growth && transform.localScale.x < 1.8f)
            {
                say = 0;
                showPlus = true;
                plus = false;

                manager.GetComponent<Manager>().squareQuantity[objectRight.GetComponent<Grid>().square.GetComponent<Square>().selected]--;

                Destroy(objectRight.GetComponent<Grid>().square);
                rightMotion = false;
                permission = true;
                manager.GetComponent<Manager>().operation = false;
                manager.GetComponent<Manager>().operationNumber = 0;
            }
        }

        if (leftMotion && (!manager.GetComponent<Manager>().operation || manager.GetComponent<Manager>().operationNumber == rowCount))
        {
            manager.GetComponent<Manager>().operation = true;
            manager.GetComponent<Manager>().operationNumber = rowCount;
            say = say + Time.deltaTime;
            objectLeft.GetComponent<Grid>().square.GetComponent<Square>().enabled = false;
            objectLeft.GetComponent<Grid>().square.GetComponent<ObjectDelete>().enabled = true;
            objectLeft.GetComponent<Grid>().square.transform.position = Vector3.MoveTowards(objectLeft.GetComponent<Grid>().square.transform.position, gameObject.transform.position, Time.deltaTime * 15);
            //objectLeft.GetComponent<Grid>().square.transform.position = Vector3.MoveTowards(objectLeft.GetComponent<Grid>().square.transform.position, gameObject.transform.position, Time.deltaTime);

            if (transform.localScale.x > 1.9f)
            {
                growth = false;
            }

            if (!growth && transform.localScale.x < 1.8f)
            {
                say = 0;
                showPlus = true;
                plus = false;

                manager.GetComponent<Manager>().squareQuantity[objectLeft.GetComponent<Grid>().square.GetComponent<Square>().selected]--;

                Destroy(objectLeft.GetComponent<Grid>().square);
                leftMotion = false;
                permission = true;
                manager.GetComponent<Manager>().operation = false;
                manager.GetComponent<Manager>().operationNumber = 0;
            }
        }

        if (availableMotion && (!manager.GetComponent<Manager>().operation || manager.GetComponent<Manager>().operationNumber == rowCount))
        {
            takeFromAvailable = false;
            manager.GetComponent<Manager>().operation = true;
            manager.GetComponent<Manager>().operationNumber = rowCount;
            say = say + Time.deltaTime;
            objectAvailable.GetComponent<Grid>().square.GetComponent<Square>().enabled = false;
            objectAvailable.GetComponent<Grid>().square.GetComponent<ObjectDelete>().enabled = true;
            //objectAvailable.GetComponent<Grid>().square.transform.position = Vector3.MoveTowards(objectAvailable.GetComponent<Grid>().square.transform.position, gameObject.transform.position, Time.deltaTime * 15);
            //objectLeft.GetComponent<Grid>().square.transform.position = Vector3.MoveTowards(objectLeft.GetComponent<Grid>().square.transform.position, gameObject.transform.position, Time.deltaTime);

            if (transform.localScale.x > 1.9f)
            {
                growth = false;
            }

            if (!growth && transform.localScale.x < 1.1f)
            {
                say = 0;
                showPlus = true;
                plus = false;

                manager.GetComponent<Manager>().squareQuantity[objectAvailable.GetComponent<Grid>().square.GetComponent<Square>().selected]--;

                Destroy(objectAvailable.GetComponent<Grid>().square);
                objectAvailable.GetComponent<Grid>().square = gameObject;
                availableMotion = false;
                permission = true;
                manager.GetComponent<Manager>().operation = false;
                manager.GetComponent<Manager>().operationNumber = 0;
            }
        }

        if (growth)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), Time.deltaTime * 15);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 15);
        }

        if (showPlus)
        {
            GameObject plusValue = Instantiate(plusValuePref, gameObject.transform.position, Quaternion.identity);
            plusValue.transform.GetChild(0).GetComponent<Text>().text = plusValueText;
            plusValue.transform.SetParent(pluses.transform);
            plusValue.transform.localScale = new Vector3(1, 1, 1);
            plusValue.GetComponent<PlusValueCode>().manager = manager;
            plusValue.GetComponent<PlusValueCode>().pointText = pointText;
            plusValue.GetComponent<PlusValueCode>().plus = plus;
            plusValue.GetComponent<PlusValueCode>().showAd = showAd;
            showPlus = false;
            showAd = false;
        }
    }


    void Neighbors()
    {
        takeFromTop = false;
        takeFromRight = false;
        takeFromLeft = false;

        if (objectTop != null && objectTop.GetComponent<Grid>().square != null)
        {
            if (objectTop.GetComponent<Grid>().square.GetComponent<Square>().selected == selected)
            {
                takeFromTop = true;
            }
        }
        
        if(objectRight != null && objectRight.GetComponent<Grid>().square != null)
        {
            if (objectRight.GetComponent<Grid>().square.GetComponent<Square>().selected == selected && rowCount > objectRight.GetComponent<Grid>().square.GetComponent<Square>().rowCount && (objectRight.GetComponent<Grid>().square.transform.position - objectRight.transform.position).magnitude < 0.1f)
            {
                takeFromRight = true;
            }
        }

        if (objectLeft != null && objectLeft.GetComponent<Grid>().square != null)
        {
            if (objectLeft.GetComponent<Grid>().square.GetComponent<Square>().selected == selected && rowCount > objectLeft.GetComponent<Grid>().square.GetComponent<Square>().rowCount && (objectLeft.GetComponent<Grid>().square.transform.position - objectLeft.transform.position).magnitude < 0.1f)
            {
                takeFromLeft = true;
            }
        }

        if (permission)
        {
            manager.GetComponent<Manager>().squareQuantity[selected]--;

            if (takeFromTop && takeFromRight && takeFromLeft)
            {
                manager.GetComponent<Manager>().point = manager.GetComponent<Manager>().point + objectTop.GetComponent<Grid>().square.GetComponent<Square>().value + objectRight.GetComponent<Grid>().square.GetComponent<Square>().value + objectLeft.GetComponent<Grid>().square.GetComponent<Square>().value;

                topMotion = true;
                rightMotion = true;
                leftMotion = true;
                growth = true;
                permission = false;

                selected = selected + 3;
                plusValueText = "+ " + (objectTop.GetComponent<Grid>().square.GetComponent<Square>().value + objectRight.GetComponent<Grid>().square.GetComponent<Square>().value + objectLeft.GetComponent<Grid>().square.GetComponent<Square>().value);
                SquareSetting();
            }
            else if (takeFromTop && takeFromRight)
            {
                manager.GetComponent<Manager>().point = manager.GetComponent<Manager>().point + objectTop.GetComponent<Grid>().square.GetComponent<Square>().value + objectRight.GetComponent<Grid>().square.GetComponent<Square>().value;

                topMotion = true;
                rightMotion = true;
                growth = true;
                permission = false;

                selected = selected + 2;
                plusValueText = "+ " + (objectTop.GetComponent<Grid>().square.GetComponent<Square>().value + objectRight.GetComponent<Grid>().square.GetComponent<Square>().value);
                SquareSetting();
            }
            else if (takeFromTop && takeFromLeft)
            {
                manager.GetComponent<Manager>().point = manager.GetComponent<Manager>().point + objectTop.GetComponent<Grid>().square.GetComponent<Square>().value + objectLeft.GetComponent<Grid>().square.GetComponent<Square>().value;

                topMotion = true;
                leftMotion = true;
                growth = true;
                permission = false;

                selected = selected + 2;
                plusValueText = $"+ {(objectTop.GetComponent<Grid>().square.GetComponent<Square>().value + objectLeft.GetComponent<Grid>().square.GetComponent<Square>().value)}";
                SquareSetting();
            }
            else if (takeFromRight && takeFromLeft)
            {
                manager.GetComponent<Manager>().point = manager.GetComponent<Manager>().point + objectRight.GetComponent<Grid>().square.GetComponent<Square>().value + objectLeft.GetComponent<Grid>().square.GetComponent<Square>().value;

                rightMotion = true;
                leftMotion = true;
                growth = true;
                permission = false;

                selected = selected + 2;
                plusValueText = "+ " + (objectRight.GetComponent<Grid>().square.GetComponent<Square>().value + objectLeft.GetComponent<Grid>().square.GetComponent<Square>().value);
                SquareSetting();
            }
            else if (takeFromTop)
            {
                manager.GetComponent<Manager>().point = manager.GetComponent<Manager>().point + objectTop.GetComponent<Grid>().square.GetComponent<Square>().value;

                topMotion = true;
                growth = true;
                permission = false;

                selected++;
                plusValueText = "+ " + (objectTop.GetComponent<Grid>().square.GetComponent<Square>().value);
                SquareSetting();
            }
            else if (takeFromRight)
            {
                manager.GetComponent<Manager>().point = manager.GetComponent<Manager>().point + objectRight.GetComponent<Grid>().square.GetComponent<Square>().value;

                rightMotion = true;
                growth = true;
                permission = false;

                selected++;
                plusValueText = "+ " + (objectRight.GetComponent<Grid>().square.GetComponent<Square>().value);
                SquareSetting();
            }
            else if (takeFromLeft)
            {
                manager.GetComponent<Manager>().point = manager.GetComponent<Manager>().point + objectLeft.GetComponent<Grid>().square.GetComponent<Square>().value;

                leftMotion = true;
                growth = true;
                permission = false;

                selected++;
                plusValueText = "+ " + (objectLeft.GetComponent<Grid>().square.GetComponent<Square>().value);
                SquareSetting();
            }
            else if (takeFromAvailable)
            {
                manager.GetComponent<Manager>().point = manager.GetComponent<Manager>().point + objectAvailable.GetComponent<Grid>().square.GetComponent<Square>().value;

                availableMotion = true;
                growth = true;
                permission = false;

                selected++;
                plusValueText = "+ " + (objectAvailable.GetComponent<Grid>().square.GetComponent<Square>().value);
                SquareSetting();
            }

            manager.GetComponent<Manager>().squareQuantity[selected]++;

            if (manager.GetComponent<Manager>().seviye == 4)
            {
                if (selected > 10)
                {
                    showAd = true;
                }
            }
            else if (manager.GetComponent<Manager>().seviye == 3)
            {
                if (selected > 9)
                {
                    showAd = true;
                }
            }
            else
            {
                if (selected > 8)
                {
                    showAd = true;
                }
            }
        }
    }


    void SquareSetting()
    {
        gameObject.GetComponent<Image>().color = themes.themes[themeSelection].squares[selected].renk;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = themes.themes[themeSelection].squares[selected].count;
        value = themes.themes[themeSelection].squares[selected].value;
    }
}
