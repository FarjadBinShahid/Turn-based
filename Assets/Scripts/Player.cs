using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : AllPlayers
{
    
    private Enemy enemyScript;
    private bool canMove, canCastSpell1, canCastSpell2;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyScript = otherPlayer.GetComponent<Enemy>();
        movementRadius.transform.position = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        spell2Particle = Instantiate(spell2Particle, transform.position, spell2Particle.transform.rotation);
        spell1Particle = Instantiate(spell1Particle, transform.position, spell1Particle.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        if(navMeshAgent.velocity.magnitude > 1)
        {
            hasStartedMoving = true;
        }

        if(spell1Particle.isPlaying || spell2Particle.isPlaying)
        {
            Debug.Log("played");
            hasCastedSPell = true;
        }

        if(hasCastedSPell && !spell2Particle.isPlaying && !spell1Particle.isPlaying)
        {
            Debug.Log("Stopped");
            hasCastedSPell = false;
            EnableEnemy();
        }

        if(hasStartedMoving && navMeshAgent.velocity.magnitude <1 )
        {
            EnableEnemy();
            hasStartedMoving = false;
            movementRadius.transform.position = transform.position;
        }

        if(isActiveTurn)
        {

            if(Input.GetKeyDown(KeyCode.A))
            {
                canMove = true;
            }

            if(canMove)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    turnText.SetText("Player Moving");
                    MovePlayer(GetWorldPosition());
                    canMove = false;
                }
                
            }
            else if(CheckPlayerInRadius())
            {
                if(Input.GetKeyDown(KeyCode.S))
                {
                    turnText.SetText("Player casted spell");
                    enemyScript.TakeDamage(50, "Enemy");
                    CastSpell1();
                    //EnableEnemy();
                }
                else if(Input.GetKeyDown(KeyCode.D))
                {
                    turnText.SetText("Player casted spell");
                    CastSpell2();
                    enemyScript.TakeDamage(50, "Enemy");
                    //hasCastedSPell = true;
                    //EnableEnemy();
                }
            }else 
            {
                Debug.Log("Not in Range");
            }

        }
        UpdateRadiusLocation();
    }

    Vector3 GetWorldPosition()
    {
        Vector3 worldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if(Physics.Raycast(ray, out hitData, 1000))
        {
            worldPosition = hitData.point;
            worldPosition.y = 1;
        }
        return worldPosition;
    }

    void EnableEnemy()
    {
        turnText.SetText("Enemy Turn");
        enemyScript.EnableDisableTurn();
    }
    

}
