using UnityEditor.Animations;

class CatSleeping : CatState {
    public CatSleeping(AnimatorController animator, string animation) {
        this.SetVariables(0.02f, 0.22f, -0.05f);
        this.SetAnimator(animator);
        this.Animation = animation;
    }
}