using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Adding weapon");
            FindObjectOfType<Player>().GetComponent<ItemController>().EquipItem(typeof(Crystal));
        }
    }
}
