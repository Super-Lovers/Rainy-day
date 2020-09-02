using UnityEngine;

class CatWalking : CatState {
    public CatWalking(RuntimeAnimatorController animator, string animation) {
        this.SetVariables(0.05f, 0.04f, 0.08f);
        this.SetAnimator(animator);
        this.Animation = animation;
    }
}