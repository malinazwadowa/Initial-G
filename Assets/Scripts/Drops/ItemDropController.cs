using UnityEngine;

public class ItemDropController : SingletonMonoBehaviour<ItemDropController>
{
    public GameObject apple;
    public GameObject exp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropApple(Vector3 dropSpot)
    {
        ObjectPooler.Instance.SpawnObject(apple, dropSpot);
    }
}
