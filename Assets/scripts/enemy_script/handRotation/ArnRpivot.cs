using UnityEngine;

public class EnemyArmRotationWithTag : MonoBehaviour
{
    public float detectionRadius = 5f; // The distance at which the enemy starts to point towards the player
    private Transform player; // To store the player's transform
    [SerializeField] private float angle;


    void Start()
    {
        // Seek the player by the "Player" tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("No player found with the 'Player' tag! Make sure your player is tagged correctly.");
        }
    }

    void Update()
    {
        // Ensure the player has been found before attempting to point at them
        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            // Calculate the direction from the enemy's arm to the player
            Vector3 direction = player.position - transform.position;
            Vector3 currentEulerAngles = transform.rotation.eulerAngles; // Decompose to Euler
            


            direction.Normalize();


            // Calculate the angle to rotate towards in radians and convert to degrees
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set the arm's rotation towards the player
            transform.rotation = Quaternion.Euler(0, 0, angle);


            if (angle == 90 || angle == -90) {



                if (currentEulerAngles.y == 0)
                {

                    currentEulerAngles.y = 180;
                }
                else if (currentEulerAngles.y == 180)
                {
                    
                    currentEulerAngles.y = 0;
                }

               transform.rotation = Quaternion.Euler(currentEulerAngles); // Reapply
                
            
            }



        }
    }
}
