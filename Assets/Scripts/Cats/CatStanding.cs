using UnityEditor.Animations;

class CatStanding : CatState {
    public CatStanding(AnimatorController animator, string animation) {
        this.SetVariables(-0.05f, 0.03f, 0.04f);
        this.SetAnimator(animator);
        this.Animation = animation;
    }
}