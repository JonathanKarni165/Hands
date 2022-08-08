using UnityEngine;

public class DestroyPlayers : MonoBehaviour
{
    OnlineGameplay og;
    public PhotonUtil pu;                 
    private bool enterence1, enterence2;

    void Start()
    {
        enterence1 = true;
        enterence2 = false;
        og = FindObjectOfType<OnlineGameplay>();
    }

    void Update()
    {
        if(pu.start && enterence1)
        {
            enterence2 = true;
            enterence1 = false;
        }

        if(enterence2)
        {
            for(int i =0; i< 2; i++)
            {
                if (pu.GetMyOp().getFingersNum(i) <= 0 && !og.choseToSwitch)
                    pu.GetMyRpc().DestroyHand(i);
            }
        }
    }
}
