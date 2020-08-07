using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoverandPathfinding : MonoBehaviour
{
    private Rigidbody2D target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    public float pathupdateinterval = .5f;

    private Seeker seeker;
    private Rigidbody2D rigidbody2d;

    public float maxRayCastRange = 5;
    public LayerMask obsticlesMask;

    //todo make em not shoot when they are rewinding
    public bool isAgressive = true;

    private void Awake()
    {
        // sets the target to a player
        target = FindObjectOfType<PlayerMover>().GetComponent<Rigidbody2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
    }
    private void Start()
    {
        InvokeRepeating("UpdatePath", 0f, pathupdateinterval);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone() && !reachedEndOfPath)
            seeker.StartPath(rigidbody2d.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (p.error) return;
        path = p;
        currentWaypoint = 0;
    }

    private void FixedUpdate()
    {
        //Checks whether the whole path has been walked and then shoots
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            if (isAgressive)
                BroadcastMessage("Fire");
            return;
        }
        else reachedEndOfPath = false;

        // Raycasting to the player
        if (Vector2.Distance(target.position, rigidbody2d.position) < maxRayCastRange)
        {
            //Debug.DrawRay(rigidbody2d.position, (target.position - rigidbody2d.position), Color.blue);
            var hit = Physics2D.Raycast(rigidbody2d.position, (target.position - rigidbody2d.position), maxRayCastRange, obsticlesMask);
            if (hit && hit.collider.transform.CompareTag("Player"))
            {
                reachedEndOfPath = true;
                if (isAgressive)
                    BroadcastMessage("Fire");
                return;
            }
        }
        // Moving the enemy
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2d.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;
        rigidbody2d.AddForce(force);
        float distance = Vector2.Distance(rigidbody2d.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance) currentWaypoint++;
    }
}
