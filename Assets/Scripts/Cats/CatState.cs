using UnityEditor.Animations;
using UnityEngine;

public abstract class CatState {
    protected float happiness_change;
    protected float hunger_change;
    protected float fatigue_change;

    protected string animation;
    public string Animation {
        get { return this.animation; }
        set { this.animation = value; }
    }
    protected AnimatorController animator;

    protected virtual void SetVariables(
        float happiness, float hunger, float fatigue) {

        this.happiness_change = happiness;
        this.hunger_change = hunger;
        this.fatigue_change = fatigue;
    }

    public virtual void Cycle(Cat cat) {
        cat.Happiness += this.happiness_change;
        cat.Hunger += this.hunger_change;
        cat.Fatigue += this.fatigue_change;
    }

    protected virtual void SetAnimator(AnimatorController animator) {
        this.animator = animator;
    }
}