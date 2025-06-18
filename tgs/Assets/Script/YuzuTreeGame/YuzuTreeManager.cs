using System.Collections.Generic;
using UnityEngine;

public class YuzuTreeManager : MonoBehaviour
{
    public static YuzuTreeManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public GameObject branchLeftPrefab;
    public GameObject branchRightPrefab;

    private int countBranch = 0;
    private List<GameObject> activeBranches = new List<GameObject>();
    private const int branchMaxCount = 4;
    private const float branchVerticalSpacing = 3f;
    private float lastBranchY;

    public GameObject mainYuzuTreePrefab;
    private List<GameObject> activeYuzuTree = new List<GameObject>();
    private const float TreeVerticalSpacing = 10f;
    private float lastTreeY;

    void Start()
    {
        lastBranchY = 0f;
        for (int i = 0; i < branchMaxCount; i++)
        {
            SpawnBranchAtHeight(lastBranchY);
            SpawnYuzuTreeAtHeight(lastTreeY);

            lastBranchY += branchVerticalSpacing;
            lastTreeY += TreeVerticalSpacing;
        }
    }

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            return;
        }

        if (lastBranchY - player.transform.position.y < branchVerticalSpacing * (branchMaxCount - 1))
        {
            SpawnBranchAtHeight(lastBranchY);
            SpawnYuzuTreeAtHeight(lastBranchY);
            lastBranchY += branchVerticalSpacing;

            if (activeBranches.Count > branchMaxCount)
            {
                Destroy(activeBranches[0]);
                activeBranches.RemoveAt(0);
            }
        }
    }

    private void SpawnBranchAtHeight(float y)
    {
        GameObject prefab;
        if (countBranch <2)
        {
            prefab = branchRightPrefab;
        }
        else
        {
            prefab = (Random.value > 0.5f) ? branchLeftPrefab : branchRightPrefab;
        }

        Vector3 pos;
        if(prefab == branchLeftPrefab)
        {
            pos = new Vector3(-3.55f, y, 0);
        }
        else
        {
            pos = new Vector3(4.39f, y, 0);
        }
        GameObject newBranch = Instantiate(prefab, pos, Quaternion.identity);
        activeBranches.Add(newBranch);

        countBranch++;
    }

    private void SpawnYuzuTreeAtHeight(float y)
    {
        Vector3 pos = new Vector3(0, y, 0);
        GameObject newTrunk = Instantiate(mainYuzuTreePrefab, pos, Quaternion.identity);
        activeYuzuTree.Add(newTrunk);
    }
}
