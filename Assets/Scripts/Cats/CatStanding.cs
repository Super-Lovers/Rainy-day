using UnityEngine;

class CatStanding : CatState {
    public CatStanding(RuntimeAnimatorController animator, string animation) {
        this.SetVariables(-0.05f, 0.03f, 0.04f);
        this.SetAnimator(animator);
        this.Animation = animation;
    }
}