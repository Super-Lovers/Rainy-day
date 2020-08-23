using UnityEditor.Animations;
using UnityEngine;

class CatWalking : CatState {
    public CatWalking(AnimatorController animator, string animation) {
        this.SetVariables(0.05f, 0.04f, 0.08f);
        this.SetAnimator(animator);
        this.Animation = animation;
    }
}