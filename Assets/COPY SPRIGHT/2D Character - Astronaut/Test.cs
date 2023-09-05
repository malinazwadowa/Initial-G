using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform startMarker; // The starting point
    public Transform endMarker;
    public Transform endMarker2;// The destination point
    public float arcHeight = 2.0f; // Height of the arc
    public float moveSpeed = 5.0f;    // Speed of the object while moving to the destination
    public float waitTime = 10.0f;     // Time to wait at the destination

    private float journeyLength;
    private float startTime;
    private float timer;

    private bool dupa = false;
    private bool play = false;

    //public GameObject prefab;
    public GameObject audioSource;
    public AudioSource audioDestination;

    private SpriteRenderer spriteRenderer;
    private enum State { MovingToDestination, Waiting, DrivingAway }
    private State currentState = State.MovingToDestination;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        startTime = Time.time;
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
        audioDestination = audioSource.GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        

        switch (currentState)
        {
            case State.MovingToDestination:
                if (!play)
                {
                    AudioManager.Instance.PlaySound(AudioManagerClips.Importante, audioDestination);
                    play = true;
                }
                
                MoveToDestination();
                break;

            case State.Waiting:
                timer += Time.deltaTime;
                //Debug.Log(timer);
                if (timer > waitTime/3)
                {
                    if (!dupa)
                    {
                        Vector3 offeset = new Vector2(1.25f, 0);
                        dupa = true;
                        EnemyManager.Instance.SpawnOnCall(PoolableObject.Test, transform.position + offeset);
                    }
                }
                
                
                if (timer >= waitTime)
                {
                    currentState = State.DrivingAway;
                }
                
                break;

            case State.DrivingAway:
                //Instantiate(prefab, Vector3.zero, transform.rotation);
                DriveAway();
                break;
        }
    }

    private void MoveToDestination()
    {
        float distanceCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        // Calculate the new position using Lerp for linear movement
        Vector2 newPos = Vector2.Lerp(startMarker.position, endMarker.position, fractionOfJourney);

        // Calculate the vertical position for the arc
        newPos.x += arcHeight * Mathf.Sin(Mathf.Clamp01(fractionOfJourney) * Mathf.PI);

        // Update the object's position
        transform.position = newPos;

        // Check if the object has reached the destination
        if (fractionOfJourney >= 1.0f)
        {
            currentState = State.Waiting;
            timer = 0.0f; // Reset the timer
        }
    }

    private void DriveAway()
    {
        
        spriteRenderer.flipX = true;
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (!dupa)
        {
            /*
             * Vector3 offeset = new Vector2(1.25f, 0);
            dupa = true;
            EnemyManager.Instance.SpawnOnCall(PoolableObject.Test, transform.position + offeset);
             * 
             * 
             */
        }
        if (transform.position.x > 50)
        {
            Destroy(gameObject);
        }
    }
    private void MoveToDestination2()
    {
        spriteRenderer.flipX = true;
        // Reset the start time and calculate a new journey length
        if (currentState == State.DrivingAway)
        {
            Debug.Log("dupaS");
            startTime = Time.time;
            journeyLength = Vector2.Distance(endMarker.position, endMarker2.position);
            Debug.Log("Journey Length: " + journeyLength);
        }

        float distanceCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        // Calculate the new position using Lerp for linear movement
        Vector2 newPos = Vector2.Lerp(endMarker.position, endMarker2.position, fractionOfJourney);

        // Calculate the vertical position for the arc
        newPos.y += arcHeight * Mathf.Sin(Mathf.Clamp01(fractionOfJourney) * Mathf.PI);

        // Update the object's position
        transform.position = newPos;

        // Check if the object has reached the destination
        if (fractionOfJourney >= 1.0f)
        {
            currentState = State.Waiting;
            timer = 0.0f; // Reset the timer
        }
    }
}