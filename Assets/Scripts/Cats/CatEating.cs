using UnityEngine;

class CatEating : CatState {
    public CatEating(RuntimeAnimatorController animator, string animation) {
        this.SetVariables(0.02f, -0.30f, 0.03f);
        this.SetAnimator(animator);
        this.Animation = animation;
    }
}