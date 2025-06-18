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

    private int leftOrRightBranch;
    private int countBranch;

    private GameObject[] branches;
    private const int branchMaxCount = 4;

    [SerializeField]

    void Start()
    {
        branches = new GameObject[branchMaxCount];
    }

    void Update()
    {
        
    }


}
