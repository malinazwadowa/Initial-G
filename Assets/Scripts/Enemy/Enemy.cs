using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //Needed for sure
    [SerializeField] public EnemyData enemyData;

    protected Player player;
    private HealthController healthController;
    //-----------------
    
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        Debug.Log("Initalizingenemi");
        player = PlayerManager.Instance.GetPlayer();
        healthController = GetComponent<HealthController>();
        healthController.Init(enemyData.maxHealth);
    }

    public void GetDamaged(float amount)
    {
        healthController.SubstractCurrentHealth(amount);
    }
}
