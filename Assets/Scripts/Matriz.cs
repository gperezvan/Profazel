using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matriz : MonoBehaviour
{
    public int x, y;
    public int[,] matrizJuego;
    public int[,] matrizBloques;

    public GameObject bloque;

    // Start is called before the first frame update
    void Start()
    {
        matrizJuego = new int[x, y];
        matrizBloques = new int[x, y];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Start();
            TrazarRuta();
            DeterminarBloques();
            CrearBloques();
        }
    }

   void TrazarRuta()
    {
        int puntoInicio = Random.Range(0, x);
        int puntoFin = Random.Range(0, x);

        int[] puntoFlotante = { puntoInicio, y - 1 };

        while (!(puntoFlotante[0] == puntoFin && puntoFlotante[1] == 0))
        {
            int movimiento;
            bool repetir = false;
            do
            {
                repetir = false;
                movimiento = Random.Range(1, 4);
                if (movimiento == 1)
                {
                    puntoFlotante[0]++;
                    if (puntoFlotante[0] >= x)
                    {
                        puntoFlotante[0]--;
                        repetir = true;
                    }
                }
                else if (movimiento == 2)
                {
                    puntoFlotante[1]--;
                    if (puntoFlotante[1] < 0)
                    {
                        puntoFlotante[1]++;
                        repetir = true;
                    }
                }
                else if (movimiento == 3)
                {
                    puntoFlotante[0]--;
                    if (puntoFlotante[0] < 0)
                    {
                        puntoFlotante[0]++;
                        repetir = true;
                    }
                }
            } while (repetir);
            matrizJuego[puntoFlotante[0], puntoFlotante[1]] = 3;
        }
        matrizJuego[puntoInicio, y - 1] = 1;
        matrizJuego[puntoFin, 0] = 2;
    }

    void DeterminarBloques()
    {
        for(int i =0; i < x; i++)
        {
            for(int j=0;j<y;j++)
            {
                int resultado = 0;
                //utilizamos enteros como código binária para saber las posibles puertas que tiene la casilla
                resultado += 1 * SaberSiHayBloque(i, j + 1);
                resultado += 2 * SaberSiHayBloque(i+1, j);
                resultado += 4 * SaberSiHayBloque(i, j - 1);
                resultado += 8 * SaberSiHayBloque(i-1, j);
                matrizBloques[i, j] = resultado;
            }
        }
    }
    void CrearBloques()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject bl = Instantiate(bloque, new Vector3(i * 10, j * 10, 0), Quaternion.identity) as GameObject;
                bl.GetComponent<Bloques>().Inicializar(matrizBloques[i,j]);
            }
        }
    }

    public int SaberSiHayBloque(int _x, int _y)
    {
        if(_x<0 || _y < 0 || _x >= x || _y >= y)
        {
            return 0;
        }

        if (matrizJuego[_x,_y]== 0)
        {
            return 0;
        }

        return 1; //es área jugable
    }


    private void OnDrawGizmos()
    {
        if (matrizJuego == null)
        {
            return;
        }
        for (int i=0;i<x;i++)
        {
            for (int j = 0; j < y; j++) 
            {
                if (matrizJuego[i,j]== 0)
                {
                    Gizmos.color = Color.black;
                } 
                else if(matrizJuego[i, j] == 1){
                    Gizmos.color = Color.green;
                }
                else if (matrizJuego[i, j] == 2)
                {
                    Gizmos.color = Color.red;
                }
                else if (matrizJuego[i, j] == 3)
                {
                    Gizmos.color = Color.yellow;
                }
                //Gizmos.DrawCube(new Vector3(i * 10, j * 10, 0), Vector3.one * 10);
            }
        }
    }
}
