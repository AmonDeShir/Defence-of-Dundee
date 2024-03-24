using UnityEngine;

public class CharacterBody {
    public Transform Head;
    public Transform Body;
    public Transform LeftLeg;
    public Transform RightLeg;
    public Transform LeftArm;
    public Transform RightArm;

    public CharacterBody(Transform parent) {
        this.Head = parent.Find("Head");
        this.Body = parent.Find("Body");
        this.LeftLeg = parent.Find("LeftLeg");
        this.RightLeg = parent.Find("RightLeg");
        this.LeftArm = parent.Find("LeftArm");
        this.RightArm = parent.Find("RightArm");
    }
}