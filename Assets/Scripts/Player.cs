using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	[SerializeField] private float moveSpeed = 7f;
	[SerializeField] private GameInput gameInput;

	private bool isWalking;
	private void Update()
	{
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();

		Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

		float moveDistance = moveSpeed * Time.deltaTime;
		float playerRadius = .7f;
		float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // can move only on the x axis
                moveDir = moveDirX;
            }
            else
            {
                // Cannot move only on the x axis

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // can move only on the z axis
                    moveDir = moveDirZ;
                }
                else
                {
                    // Cannot move in any direction
                    moveDir = Vector3.zero;
                }
            }
        }

        if (canMove && moveDir != Vector3.zero)
        {
            transform.position += moveDir * moveDistance;

            isWalking = true;
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
        else
        {
            isWalking = false;
        }

    }

    public bool IsWalking() {
		return isWalking;
	}

}
