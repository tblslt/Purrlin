using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

//prevents incomming damage and staggers enemies that hit. if a successful block, refund mana
public class Counter : MonoBehaviour, Spell
{
    SpellCaster sc;
    bool cancelled = false;
    public Coroutine activeCoroutine { get; private set; }
    public int manaCost = 6;
    public float CastDuration = 1;
    void Awake()
    {
        sc = GetComponentInParent<SpellCaster>();
    }

    void Spell.Cancel()
    {
        if (activeCoroutine != null)
        {
            //TODO: stop animation
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

        if (manaCost > sc.mana) { activeCoroutine = null; yield break; }
        sc.mana -= manaCost;
        float startTime = Time.time;

        sc.rb.constraints |= RigidbodyConstraints2D.FreezePosition;
        //TODO: play counter animation
        yield return new WaitForSeconds(CastDuration * sc.durationMultiplier);

        sc.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        activeCoroutine = null;
        //TODO: set animation back
    }
}