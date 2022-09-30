using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private bool expandable;

    private List<GameObject> availableList;
    private List<GameObject> usedList;

    private void Awake()
    {
        availableList = new List<GameObject>();
        usedList = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GenerateNewObject();
        }
    }

    public GameObject GetObject()
    {
        int totalAvailable = availableList.Count;
        if (totalAvailable == 0 && !expandable) return null;
        else if (totalAvailable == 0) GenerateNewObject();

        GameObject g = availableList[totalAvailable - 1];
        availableList.RemoveAt(totalAvailable - 1);
        usedList.Add(g);
        return g;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        usedList.Remove(obj);
        availableList.Add(obj);
    }

    private void GenerateNewObject()
    {
        GameObject g = Instantiate(prefab);
        g.transform.parent = transform;
        g.SetActive(false);
        availableList.Add(g);
    }
}
