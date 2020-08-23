using UnityEditor.Animations;

class CatEating : CatState {
    public CatEating(AnimatorController animator, string animation) {
        // TODO: Make eating vary depending on food
        this.SetVariables(0.02f, -0.30f, 0.03f);
        this.SetAnimator(animator);
        this.Animation = animation;
    }
}