using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : AllPlayers
{

    private Player playerScript;


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerScript = otherPlayer.gameObject.GetComponent<Player>();
    }


    // Start is called before the first frame update
    void Start()
    {
        movementRadius.transform.position = transform.position;
        spell2Particle = Instantiate(spell2Particle, transform.position, spell2Particle.transform.rotation);
        spell1Particle = Instantiate(spell1Particle, transform.position, spell1Particle.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {


        if(spell1Particle.isPlaying || spell2Particle.isPlaying)
        {
            hasCastedSPell = true;
        }

        if(hasCastedSPell && !spell2Particle.isPlaying && !spell1Particle.isPlaying)
        {
            hasCastedSPell = false;
            EnablePlayer();
        }



        if(navMeshAgent.velocity.magnitude > 1)
        {
            hasStartedMoving = true;
        }

        if(hasStartedMoving && navMeshAgent.velocity.magnitude <1 )
        {
            EnablePlayer();
            hasStartedMoving = false;
            movementRadius.transform.position = transform.position;
        }

        if(isActiveTurn)
        {
            if(CheckPlayerInRadius())
            {
                turnText.SetText("Enemy Casted Spell");
                playerScript.TakeDamage(25, "Player");
                CastSpell2();
                //EnablePlayer();
            }else
            {
                turnText.SetText("Enemy Moving");
                //Debug.Log("Enemy Moved");
                MoveTowardsPlayer();
            }
        }
        UpdateRadiusLocation();

    }

    

    void MoveTowardsPlayer()
    {
        Vector3 moveTo = (otherPlayer.transform.position);
        moveTo.y = 1;
        MovePlayer(moveTo);
        
    }

    void EnablePlayer()
    {
        turnText.SetText("Player Turn");
        playerScript.EnableDisableTurn();
    }

}
