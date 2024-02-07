using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;
    Vector3 Different;

    [SerializeField]
    float PositionDifferent;

    private void Start()
    {
        Different = Player.transform.position;
    }
    void Update()
    {
        if (Player.transform.position.y < this.transform.position.y + PositionDifferent)
        {
            this.transform.position = new Vector3(12f, Player.transform.position.y + 3f, 3.15f);
        }
    }

}
