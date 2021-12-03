using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AllPlayers : MonoBehaviour
{
    [SerializeField] protected GameObject movementRadius;
    [SerializeField] protected bool isActiveTurn;
    [SerializeField] protected GameObject otherPlayer;
    [SerializeField] protected ParticleSystem spell1Particle;
    [SerializeField] protected ParticleSystem spell2Particle;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] protected TextMeshProUGUI turnText;
    [SerializeField] private GameObject gameOverScreen;
    protected NavMeshAgent navMeshAgent;
    
    protected MovementRadius movementRadiusScript;
    protected bool hasStartedMoving = false;
    protected bool hasCastedSPell = false;
    [SerializeField] protected int hp = 100;

    protected virtual void MovePlayer(Vector3 worldPosition)
    {
        Vector3 offset = worldPosition - movementRadius.transform.position;
        navMeshAgent.destination = movementRadius.transform.position + Vector3.ClampMagnitude(offset, movementRadius.GetComponent<MovementRadius>().radius);
        EnableDisableTurn();
    }

    protected virtual void CastSpell1()
    {
        spell1Particle.transform.position = transform.position;
        spell1Particle.Play();
        EnableDisableTurn();
    }

    protected virtual void CastSpell2()
    {
        spell2Particle.transform.position = transform.position;
        spell2Particle.Play();
        //Debug.Log("Spell 2 casted");
        EnableDisableTurn();
    }

    public void EnableDisableTurn()
    {
        isActiveTurn = !isActiveTurn;
    }

    protected void UpdateRadiusLocation()
    {
        movementRadius.transform.position = Vector3.Lerp(movementRadius.transform.position, transform.position, 0.05f);
    }

    protected bool CheckPlayerInRadius()
    {
        if(Vector3.Distance(otherPlayer.transform.position, transform.position) < movementRadius.GetComponent<MovementRadius>().radius)
        {   
            return(true);
        }else
        {
            return false;
        }
    }

    public void TakeDamage(int damage, string name)
    {
        hp-=damage;
        healthText.SetText(name + " Health" + " = " + hp);

        if(hp<1)
        {
            UIManager.Instance.GameOver();
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
        Destroy(movementRadius);
        Destroy(spell1Particle);
        Destroy(spell2Particle);
    }
    

    

}
