using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public interface Spell
{
    public void Cancel();
    public void Cast();
}