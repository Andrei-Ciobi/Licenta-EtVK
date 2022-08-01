using System;
using UnityEngine;

namespace EtVK.AI_Module.Core
{
    public class PatrolManager : MonoBehaviour
    {
        [Header("The path for AI to follow")]
        [SerializeField] private Transform pathHolder;

        [Header("Path options")] 
        [SerializeField] private bool usePath;
        [SerializeField] private bool loopedPath;

        public Vector3 CurrentWaypoint => waypoints[waypointIndex];
        public bool HasPath => waypoints.Length > 1 && usePath;
        
        private Vector3[] waypoints;
        private Transform agentTransform;
        private int waypointIndex;
        private int sign;
        private bool hasPath;

        private void Awake()
        {
            InitializeReferences();
        }

        private void Start()
        {
            OnStart();
        }

        public void GoToNextWaypoint(EnemyManager manager, float speed)
        {
            var agent = manager.GetNavMeshAgent();
            agent.isStopped = false;
            agent.speed = speed;
            agent.destination = waypoints[waypointIndex];
        }

        public bool WaypointReached(EnemyManager manager)
        {

            if (Vector3.Distance(agentTransform.position, waypoints[waypointIndex]) > 0.5f )
                return false;

            // If we have a loop path we start again from the first waypoint
            if (loopedPath)
            {
                waypointIndex = (waypointIndex + 1) % waypoints.Length;
            }
            else
            {
                if (waypointIndex == waypoints.Length - 1)
                    sign = -1;

                else if (waypointIndex == 0)
                    sign = +1;

                waypointIndex += sign;
            }

            manager.GetNavMeshAgent().destination = agentTransform.position;

            return true;
        }
        private void OnStart()
        {
            waypoints = new Vector3[pathHolder.childCount];

            for (var i = 0; i < waypoints.Length; ++i)
            {
                waypoints[i] = pathHolder.GetChild(i).position;
                waypoints[i] = new Vector3(waypoints[i].x, agentTransform.position.y, waypoints[i].z);
            }

            waypointIndex = 1;
            if (agentTransform.position != waypoints[0])
                waypointIndex = 0;
        }

        private void InitializeReferences()
        {
            agentTransform = transform.root;
        }
        
        private void OnDrawGizmos()
        {
            var startPosition = pathHolder.GetChild(0).position;
            var previousPosition = startPosition;

            foreach (Transform waypoint in pathHolder)
            {
                Gizmos.DrawSphere(waypoint.position, .3f);
                Gizmos.DrawLine(previousPosition, waypoint.position);
                previousPosition = waypoint.position;
            }

            if (loopedPath)
                Gizmos.DrawLine(previousPosition, startPosition);
        }
        
    }
}