using UnityEngine;

public class Item : MonoBehaviour 
{
    protected int currentRank;
    //Some data for UI as well, like icon 

    public virtual void RankUp()
    {
        currentRank++;
    }
}
