using UnityEngine;

public class CharacterBody : MonoBehaviour {
    public Transform Head;
    public Transform Body;
    public Transform LeftKnee;
    public Transform RightKnee;
    public Transform LeftLeg;
    public Transform RightLeg;
    public Transform LeftArm;
    public Transform RightArm;
    public Transform LeftHand;
    public Transform RightHand;
    public Transform Gun;

    public Transform GroundCheck;


    private bool isFlipped;

    public bool IsFlipped { get { return isFlipped; } }
    public bool IsMovingBackward = false;


    public void Flip(bool left) {
        isFlipped = left;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, left ? 180 : 0, transform.rotation.eulerAngles.z);
    }

    public float GetFrontSide() {
        return IsFlipped ? -1f : 1f;
    }
}