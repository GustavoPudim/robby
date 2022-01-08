using UnityEngine;

[RequireComponent(typeof(Register))]
public class Manager : MonoBehaviour
{
    
    public static Manager instance;

    public Player player;
    public static Register register;

    [SerializeField] private ItemEntity BaseItemDrop;

    private static Controls _controls;
    public static Controls controls
    {
        get
        {
            if(_controls == null)
            {
                _controls = new Controls();
            }
            return _controls;
        }
        set
        {
            _controls = value;
        }
    }

    void Awake()
    {
        instance = this;
        register = GetComponent<Register>();
        controls = new Controls();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        // TESTING
        for (int x = -16; x < 17; x++)
        {
            for (int y = -16; y < 17; y+=2)
            {
                if(Util.RandomDouble() < 0.2f)
                {
                    SpawnBlock(x, y, "tree");
                }
            }
        }
    }

    public ItemEntity getItemDrop(string id)
    {
        ItemEntity itemDrop = BaseItemDrop;
        itemDrop.id = id;

        return itemDrop;
    }

    public void SpawnBlock(float x, float y, string blockId)
    {
        Instantiate(register.GetBlockById(blockId).prefab, new Vector3(x * 1.6f, y * 1.6f, 0), Quaternion.identity);
    }
}
