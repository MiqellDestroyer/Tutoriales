using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour {

    private Animator anim;

    public Transform leftHandTarget;

    public Transform leftElbowTarget;

    public Transform rightHandTarget;

    public Transform rightElbowTarget;

    public float leftHandIKWeight;

    public float leftHandRotWeight;

    public float leftElbowIKWeight;

    public float rightHandIKWeight;

    public float rightHandRotWeight;

    public float rightElbowIKWeight;

    public bool isIKActive;

    public float distance;

    public float verticalOffset;

    private RaycastHit leftHit;

    private RaycastHit rightHit;

    public LayerMask layer;


	void Start () {
        anim = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
        CheckNearWall();
	}

    private void CheckNearWall()
    {
        if (isIKActive)
        {
            if (Physics.Raycast(transform.position + Vector3.up * verticalOffset, transform.right, out rightHit, distance, layer.value))
            {
                rightHandTarget.position = rightHit.point - transform.right * 0.1f ;
                rightHandIKWeight = Mathf.Lerp(rightHandIKWeight, 1, Time.fixedDeltaTime * 5);
                rightHandRotWeight = Mathf.Lerp(rightHandRotWeight, 1, Time.fixedDeltaTime * 5);                                                                         
                rightHandTarget.rotation = Quaternion.LookRotation(transform.up + transform.forward, rightHit.normal);
                rightElbowIKWeight = Mathf.Lerp(rightElbowIKWeight, 1, Time.fixedDeltaTime * 5);                                                 
                rightElbowTarget.position = transform.position + transform.right * 0.5f + transform.up + transform.forward * -0.5f;
            }
            else
            {
                rightHandIKWeight = Mathf.Lerp(rightHandIKWeight, 0, Time.fixedDeltaTime * 5);
                rightHandRotWeight = Mathf.Lerp(rightHandRotWeight, 0, Time.fixedDeltaTime * 5); 
                rightElbowIKWeight = Mathf.Lerp(rightElbowIKWeight, 0, Time.fixedDeltaTime * 5); 
            }

            if (Physics.Raycast(transform.position + Vector3.up * verticalOffset, -transform.right, out leftHit, distance, layer.value))
            {
                leftHandTarget.position = leftHit.point + transform.right * 0.1f;
                leftHandIKWeight = Mathf.Lerp(leftHandIKWeight, 1, Time.fixedDeltaTime * 5); 
                leftHandRotWeight = Mathf.Lerp(leftHandRotWeight, 1, Time.fixedDeltaTime * 5); 
                leftHandTarget.rotation = Quaternion.LookRotation(transform.up + transform.forward, leftHit.normal);
                leftElbowIKWeight = Mathf.Lerp(leftElbowIKWeight, 1, Time.fixedDeltaTime * 5);
                leftElbowTarget.position = transform.position + transform.right * -0.5f + transform.up + transform.forward * -0.5f;
            }
            else
            {
                leftHandIKWeight = Mathf.Lerp(leftHandIKWeight, 0, Time.fixedDeltaTime * 5);
                leftHandRotWeight = Mathf.Lerp(leftHandRotWeight, 0, Time.fixedDeltaTime * 5);
                leftElbowIKWeight = Mathf.Lerp(leftElbowIKWeight, 0, Time.fixedDeltaTime * 5);
            }

        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (isIKActive)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandIKWeight);
            anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, leftElbowIKWeight);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandRotWeight);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
            anim.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowTarget.position);

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandIKWeight);
            anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, rightElbowIKWeight);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandRotWeight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
            anim.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowTarget.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position + Vector3.up * verticalOffset, transform.position + Vector3.up * verticalOffset + transform.right * distance);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position + Vector3.up * verticalOffset, transform.position + Vector3.up * verticalOffset - transform.right * distance);

    }
}
