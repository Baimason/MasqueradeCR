using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : CharacterMove
{
    //crear enemigo - mover - daño al tocarlo - ruta - condicion de muerte -estados de animacion - persiga (bool)

    //Inicializar variables
    //Movimiento
    public int rutina;
    public float cronometro;
    public int direccion;
    public float caminar;
    public float correr;
    public GameObject target;
    public bool atacando;

    //persecuci
    public float rango_vision;
    public float rango_ataque;
    public GameObject rango;//testear jugador
    public GameObject Hit;//da mensaje

    private Animator animacion;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        animacion = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        target = GameObject.Find("Player");
    }

    public void Comportamiento(){
        HorizontalMove = 0;
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_vision && !atacando)// Si la posion x del enemigo - la pos del jugador es mayor al rango de vision y no esta atacando
        {
            //cigo de rutina
            animacion.SetBool("correr", false);//cancela la animacion correr
            cronometro += 1 * Time.deltaTime;//cronometro sumando + 1 * time.deltatime PREGUNTAR POR DELTA TIME
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);// Si cronometro es mayor o igual a 0, la rutina ser・entre 0 y 1
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    animacion.SetBool("caminar", false);//cancelar la animacion caminar
                    break;

                case 1:
                    direccion = Random.Range(0, 2);//direccion entre 0 y 1
                    rutina++;
                    break;

                case 2:
                    switch (direccion)
                    {
                        case 0:
                            HorizontalMove = 1 * caminar * SpeedMult;//derecha
                            //animacion.transform.localScale = new Vector3(1,1,1); //(1, 0, 0)
                            //transform.rotation = Quaternion.Euler(0, 0, 0);//rotacion en Y sea 0
                           // transform.Translate(Vector3.right * caminar * Time.deltaTime);//caminar hacia delante 

                            break;

                        case 1:
                            HorizontalMove = -1 * caminar * SpeedMult;//izquierda
                            //animacion.transform.localScale = new Vector3(-1,1,1); //(-1, 0, 0)
                           // transform.rotation = Quaternion.Euler(0, 180, 0);//rotacion en Y sea 0
                           // transform.Translate(Vector3.right * caminar * Time.deltaTime);//caminar hacia delante 
                            break;
                    }
                    animacion.SetBool("caminar", true);//activar la animacion correr
                    break;
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !atacando)// Si la posion x del enemigo - la pos del jugador es mayor al rango de vision y no esta atacando
            {
                if (transform.position.x < target.transform.position.x)
                {
                    animacion.SetBool("caminar", false);
                    animacion.SetBool("correr", true);
                    HorizontalMove = 1 * correr * SpeedMult;//derecha
                    //transform.Translate(Vector3.right * correr * Time.deltaTime);//caminar hacia delante 
                    //transform.rotation = Quaternion.Euler(0, 0, 0);//rotacion en Y sea 0
                    animacion.SetBool("atacando", false);
                }
                else
                {
                    animacion.SetBool("caminar", false);
                    animacion.SetBool("correr", true);
                    HorizontalMove = -1 * correr * SpeedMult;//izquierda
                    // transform.Translate(Vector3.right * correr * Time.deltaTime);//caminar hacia delante 
                    // transform.rotation = Quaternion.Euler(0, 180, 0);//rotacion en Y sea 0
                    animacion.SetBool("ataque", false);
                }
            }
            else
            {
                if (!atacando)
                {
                    if (transform.position.x < target.transform.position.x)
                    {
                        //transform.rotation = Quaternion.Euler(0, 0, 0);//rotacion en Y sea 0
                    }
                    else
                    {
                        //transform.rotation = Quaternion.Euler(0, 180, 0);//rotacion en Y sea 0
                        animacion.SetBool("caminar", false); 
                        animacion.SetBool("correr", false);
                    }
                }
            }
        }
    }

    public void Ataque_Animacion() {
        animacion.SetBool("ataque", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue() {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
    }
    public override void OnDeath()
    {
        rango.SetActive(false);
        Hit.SetActive(false);
        animacion.speed = 0;

        base.OnDeath();
    }
}
//End class
