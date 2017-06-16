using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Selection
{
    public class ClickToMove : MonoBehaviour
    {
        private NavMeshAgent navAgent;
        private Animator anim;
        [SerializeField]
        private LineRenderer trace;
        public Vector3 targetPos;
        public LayerMask groundLayer; // capa del suelo;
        private SelectableUnit selectable;

        private void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            selectable = GetComponent<SelectableUnit>();
        }

        private void Update()
        {
            anim.SetFloat("velocity", navAgent.velocity.sqrMagnitude);

            trace.SetPosition(0, transform.position);

            if (Input.GetButtonDown("Fire2") && selectable.IsSelected())
            {
                MoveTowardsClick();
            }

            if (selectable.IsSelected())
            {
                if (navAgent.remainingDistance <= navAgent.stoppingDistance)
                {
                    trace.enabled = false;
                }
                else
                    trace.enabled = true;

            }
            else
                trace.enabled = false;
        }

        private void MoveTowardsClick()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int radius = SelectionController.selectedUnits.Count / 2;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                targetPos = hit.point + new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
                navAgent.SetDestination(targetPos);
                trace.SetPosition(1, targetPos);

            }
        }
    }
}
