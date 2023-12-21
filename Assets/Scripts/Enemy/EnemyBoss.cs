using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : Enemy
{
    private NavMeshAgent agent;
    private EnemyMovementController movementController;
    private Animator animator;
    private float timer;

    //private TextMeshPro text;
    private TextMeshProUGUI textUGUI;
    private string tekst1 = "Sluchaj no synek...";
    private string tekst2 = "Jest taka gra...";

    public GameObject chat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        timer += Time.deltaTime;
        if(timer > 5)
        {
            //Debug.Log("dupa");
            ChasePlayer();
        }
        //Debug.Log(dupa1);
    }
    public override void Initialize()
    {
        base.Initialize();

        agent = GetComponent<NavMeshAgent>();
        //Needed due to how NavMeshPlus works.
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        movementController = GetComponent<EnemyMovementController>();

        textUGUI = FindAnyObjectByType<TextMeshProUGUI>();
        //Debug.Log(textUGUI);
        textUGUI.SetText(tekst1);

        StartCoroutine(DisableText());
    }
    public void ChasePlayer()
    {

        textUGUI.SetText(tekst2);
        
        animator = GetComponentInChildren<Animator>();

        animator.SetBool("isWalking", true);

        
        //movementController.Init(agent, player);
    }
    IEnumerator DisableText()
    {
        yield return new WaitForSeconds(9);
        chat.SetActive(false);
    }
}

