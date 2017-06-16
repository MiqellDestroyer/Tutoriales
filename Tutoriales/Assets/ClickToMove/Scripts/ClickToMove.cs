using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace ClickToMove
{
    public class ClickToMove : MonoBehaviour
    {
        private NavMeshAgent navAgent;
        private Animator anim;

        public Vector3 targetPos;
        public LayerMask groundLayer; // capa del suelo;

        private void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            anim.SetFloat("velocity", navAgent.velocity.sqrMagnitude);

            if (Input.GetButtonDown("Fire1"))
            {
                MoveTowardsClick();
            }
        }

        private void MoveTowardsClick()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                if (targetPos != hit.point)
                {
                    targetPos = hit.point;
                }

                navAgent.SetDestination(targetPos);
            }
        }
    }
}
