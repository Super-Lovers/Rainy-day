using UnityEngine;

class CatPlaying : CatState {
    public CatPlaying(RuntimeAnimatorController animator, string animation) {
        this.SetVariables(0.8f, 0.04f, 0.06f);
        this.SetAnimator(animator);
        this.Animation = animation;
    }
}