using UnityEngine;

class CatSleeping : CatState {
    public CatSleeping(RuntimeAnimatorController animator, string animation) {
        this.SetVariables(0.02f, 0.22f, -0.09f);
        this.SetAnimator(animator);
        this.Animation = animation;
    }
}