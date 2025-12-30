//roots grow in the direction the player is facing that damages enemies and heals if hit
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
public class Leach : MonoBehaviour, Spell
{
    SpellCaster sc;
    public Coroutine activeCoroutine { get; private set; }
    public GameObject hitbox;
    public int baseDamage = 1;

    int manaCost = 3;
    float baseCastTime = .5f;
    int heal = 1;
    void Awake()
    {
        sc = GetComponentInParent<SpellCaster>();
    }
    void Spell.Cancel()
    {
        if (activeCoroutine != null)
        {
            hitbox = null;
            //TODO: disable hitbox and animations
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }
    }
    bool Spell.Cast()
    {
        activeCoroutine = StartCoroutine(Casting());
        return true;
    }

    IEnumerator Casting()
    {

        if (manaCost > sc.mana) { activeCoroutine = null; yield break; }
        sc.mana -= manaCost;
        float startTime = Time.time;

        //TODO: play animation
        //if (enemy in hitbox collider)
        {
            //TODO: damage all enemy touching
            if (sc.health < sc.maxHealth)
            {
                sc.health += 1;
            }
        }
        yield return new WaitForSeconds(baseCastTime * sc.durationMultiplier);
        //TODO: set animation back
        activeCoroutine = null;
    }
}
