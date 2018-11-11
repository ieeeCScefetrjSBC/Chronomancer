using UnityEngine;

public class Playerwalk : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Vector2 direction;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
    }

    public void Move() //move o personagem
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void GetInput() //define teclas de movimento
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
}
