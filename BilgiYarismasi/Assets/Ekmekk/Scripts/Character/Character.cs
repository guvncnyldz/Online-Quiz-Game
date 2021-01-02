using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterCosmetics))]
[RequireComponent(typeof(CharacterAnimation))]
public class Character : MonoBehaviour
{
    [HideInInspector] public CharacterAnimation charAnim;
    [HideInInspector] public CharacterCosmetics cosmetic;

    private void Awake()
    {
        charAnim = GetComponent<CharacterAnimation>();
        cosmetic = GetComponent<CharacterCosmetics>();
    }
}