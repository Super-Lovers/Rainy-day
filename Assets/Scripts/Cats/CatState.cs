using UnityEngine;

public abstract class CatState {
    protected float happiness_change;
    protected float hunger_change;
    protected float fatigue_change;

    protected Animation animation;

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

    protected virtual void SetAnimation(Animation animation) {
        this.animation = animation;
    }
}