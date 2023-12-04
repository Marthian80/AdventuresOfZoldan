    using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirection = 2f;

    private enum State
    {
        Roaming
    };       


    private State state;
    private EnemyPathfinding enemyPathfinding;    
    

    private void Awake()
    {
        state = State.Roaming;
        enemyPathfinding = GetComponent<EnemyPathfinding>();        
    }

    private void Start()
    {
        StartCoroutine(RoamingState());
    }
        
    private IEnumerator RoamingState()
    {
        while (state == State.Roaming) 
        {
            enemyPathfinding.MoveTo(GetRoamingPosition());
            yield return new WaitForSeconds(roamChangeDirection);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    
}
