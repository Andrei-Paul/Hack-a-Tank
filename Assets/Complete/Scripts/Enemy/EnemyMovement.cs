using UnityEngine;
using System.Collections;
using System.Diagnostics;

namespace Complete
{ 
    public class EnemyMovement : MonoBehaviour
    {
        bool closeEnough;
        int enemyDistance = 10;
        public GameObject enemy;
        TankShooting shooting;
        Transform player;               // Reference to the player's position.
                                        //PlayerHealth playerHealth;      // Reference to the player's health.
                                        /* EnemyHealth enemyHealth; */       // Reference to this enemy's health.
        NavMeshAgent nav;               // Reference to the nav mesh agent.


        void Start()
        {
            enemy = this.gameObject;
            // Set up the references.
            player = GameObject.FindGameObjectWithTag("Player").transform;
            //playerHealth = player.GetComponent<PlayerHealth>();
            //enemyHealth = GetComponent<EnemyHealth>();
            nav = GetComponent<NavMeshAgent>();
        }



        void Update()
        {
            // If the enemy and the player have health left...
            //if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            //{
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination(player.position);



            closeEnough = false;
            if (Vector3.Distance(player.position, transform.position) <= enemyDistance)
            {
                
                
                //yield return new WaitForSeconds(.1f);
                StartCoroutine(WaitAndPrint(1.0f));
                //for (var index = 0; index < 10000; index++)
                //{
                //    index++;
                //}

            }
     
            


            //}
            // Otherwise...
            //else
            //{
            //    // ... disable the nav mesh agent.
            //    nav.enabled = false;
            //}
        }

        IEnumerator WaitAndPrint(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            //(enemy.GetComponent<TankShooting>()).Fire();
            print("WaitAndPrint " + Time.time);
        }
    }

}