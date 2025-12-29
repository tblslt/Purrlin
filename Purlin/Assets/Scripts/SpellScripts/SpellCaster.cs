using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    Spell[] spellOptions;
    public Rigidbody2D rb;

    public int maxMana = 10;
    public int mana = 10;
    public int manaRegen = 2;

    public int maxHealth = 9;
    public int health = 9;
    //stat modifiers
    public float durationMultiplier = 1f;
    public float damageMultiplier = 1f;


    void Awake()
    {
        spellOptions = GetComponents<Spell>();
    }
    
    void CastSpell(int i)
    {
        spellOptions[i].Cast();
    }
    void CancelSpell(int i)
    {
        spellOptions[i].Cancel();
    }
    void CancelAllSpell()
    {
        foreach (Spell spell in spellOptions)
        {
            spell.Cancel();
        }
    }
    
}