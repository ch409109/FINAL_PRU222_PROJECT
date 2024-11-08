using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBulletScript : MonoBehaviour
{
    [Header("Attack 1")]
    [SerializeField] float attackPointRadius;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask player2Mask;
    [SerializeField] float attack1dame;

    //void Update()
    //{
    //    Collider2D[] Player2 = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackPointRadius, player2Mask);
    //    foreach (Collider2D player in Player2)
    //    {
    //        player.GetComponent<Player2Script>().TakeDamage(attack1dame);
    //    }
    //}
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackPointRadius);
    }
}
