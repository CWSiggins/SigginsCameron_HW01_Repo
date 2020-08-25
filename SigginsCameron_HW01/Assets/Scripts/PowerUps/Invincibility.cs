using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    protected override void PowerUp(Player player)
    {
        player.GetComponent<Renderer>().material.color = Color.green;
        player._invincible = true;
    }

    protected override void PowerDown(Player player)
    {
        player.GetComponent<Renderer>().material.color = Color.blue;
        player._invincible = false;
    }
}
