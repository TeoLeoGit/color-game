using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public void CallAttack()
    {
        GameController.LaunchBossProjectile();
    }
}
