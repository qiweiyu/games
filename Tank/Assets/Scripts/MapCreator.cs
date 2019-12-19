using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public GameObject[] item;

    private List<Vector3> itemPositionList = new List<Vector3>();

    private void Awake()
    {
        InitMap();
    }

    private void InitMap()
    {
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(-1, -7, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(0, -7, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -7, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);

        for (int i = -11; i <= 11; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i <= 8; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }

        for (int i = 0; i < 150; i++)
        {
            CreateItem(item[Random.Range(1, 5)], CreateRandomPosition(), Quaternion.identity);
        }

        GameObject go = Instantiate(item[5], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;

        CreateItem(item[5], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[5], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[5], new Vector3(10, 8, 0), Quaternion.identity);

        InvokeRepeating("CreateEnemy", 4, 5);
    }

    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }

    private Vector3 CreateRandomPosition()
    {
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!itemPositionList.Contains(createPosition))
            {
                return createPosition;
            }
        }
    }

    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 pos;
        switch (num)
        {
            case 0:
                pos = new Vector3(-10, 8, 0);
                break;
            case 1:
                pos = new Vector3(0, 8, 0);
                break;
            default:
                pos = new Vector3(10, 8, 0);
                break;
        }
        CreateItem(item[5], pos, Quaternion.identity);
    }
}
