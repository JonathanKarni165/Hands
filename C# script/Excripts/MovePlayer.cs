using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovePlayer : MonoBehaviour
{
    Transform[] positions;
    GameSceneSetup gss;

    void Start()
    {
        gss = FindObjectOfType<GameSceneSetup>();
        positions = gss.GetPositions();

        gameObject.transform.position = positions
            [(int)gameObject.GetPhotonView().Owner.CustomProperties["ActorNumber"]].position;
        gameObject.transform.rotation = positions
            [(int)gameObject.GetPhotonView().Owner.CustomProperties["ActorNumber"]].rotation;
    }

    
}
