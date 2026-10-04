using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public TextMeshProUGUI countText;
    private int count;
    public GameObject winTextObject;
    [SerializeField] private float velocidade = 10f;
    [SerializeField] private float aceleracao = 50f;
    [SerializeField] private float desaceleracao = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }
    void Update()
    {
        Move();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 6)
            winTextObject.SetActive(true);
    }

    void Move()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float movimentoVertical = Input.GetAxis("Vertical");
        Vector3 movimento = new Vector3(movimentoHorizontal, 0.0f, movimentoVertical).normalized;

        Vector3 velideal = movimento * velocidade;

        Vector3 velatual = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        float aceleracaoAtual = movimento.magnitude > 0 ? aceleracao : desaceleracao;

        Vector3 novaVelocidade = Vector3.MoveTowards(velatual, velideal, aceleracaoAtual * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(novaVelocidade.x, rb.linearVelocity.y, novaVelocidade.z);
    }
}
