using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

//needles made of blood erupt from your face that fan out like a shotgun, costs health instead of mana
public class Acupuncture : MonoBehaviour, Spell
{
    SpellCaster sc;
    public Coroutine activeCoroutine { get; private set; }
    public GameObject needles;
    public int baseDamage = 1;

    int healthCost = 3;
    float baseCastTime = .2f;
    void Awake()
    {
        activeCoroutine = null;
        sc = GetComponentInParent<SpellCaster>();
    }
    void Spell.Cancel()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            sc.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
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

        if (healthCost >= sc.health) { activeCoroutine = null; yield break; }
        sc.health -= healthCost;

        //TODO: play animation
        //if (enemy in hitbox collider)
        {
            //TODO: damage all enemy touching
            if (sc.health < sc.maxHealth)
            {
                sc.health += 1;
            }
        }
        float startTime = Time.time;
        sc.rb.constraints |= RigidbodyConstraints2D.FreezePosition;
        while (Time.time < startTime + baseCastTime * sc.durationMultiplier)
        {
            yield return null;
        }
        sc.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        //TODO: set animation back
    }
}