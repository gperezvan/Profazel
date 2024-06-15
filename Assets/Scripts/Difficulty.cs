using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbExistir : MonoBehaviour
{
	public int indice;

    void Start()
    {
        if (Matriz.singleton != null && Matriz.singleton.elementos != null)
        {
            if (indice >= 0 && indice < Matriz.singleton.elementos.Length)
            {
                gameObject.SetActive(!(UnityEngine.Random.Range(0f, 1f) > Matriz.singleton.elementos[indice]));
            }
        }
    }
}
