using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Enemy
{
    protected override void PlayerImpact(Player player)
    {
        base.PlayerImpact(player);
        Vector3 offset =  player.transform.position - transform.position;
        offset = -offset.normalized;

        Rigidbody _rb = player.GetComponent<Rigidbody>();

        //pull motor from the player
        BallMotor _motor = player.GetComponent<BallMotor>();
        if (_motor != null)
        {
            _rb.AddForce(offset * -500f);
        }
    }
}
