
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class DragonController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Button attackButton;

    public Animator myAnimation;
    private FixedJoystick fixedJoystick;
    private Rigidbody rigidBody;
    // private bool AttackWasDone;

    private void OnEnable()
    {
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        myAnimation = GetComponent<Animator>();
        attackButton = GameObject.Find("AttackButton").GetComponent<Button>();
        attackButton.onClick.AddListener(TryAttack);
    }

    private void OnDisable()
    {
        // Отписываемся от события нажатия кнопки
        attackButton.onClick.RemoveListener(TryAttack);
    }
    
    private void FixedUpdate()
    {
        float xVal = fixedJoystick.Horizontal;
        float yVal = fixedJoystick.Vertical;

        Vector3 movement = new Vector3(xVal, 0, yVal);

        rigidBody.velocity = movement * speed;

        if (xVal != 0 && yVal != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(xVal, yVal)*Mathf.Rad2Deg, transform.eulerAngles.z);
        }

        // TryAttack();
        // if (AttackWasDone)
        // {
        //     AttackWasDone = false;
        //     myAnimation.SetBool("Attack", false);
        // }

    }

    private void TryAttack()
    {
        // Проверяем, активен ли объект
        if (gameObject.activeInHierarchy)
        {
            // Запускаем анимацию атаки
            myAnimation.SetBool("Attack", true);
            StartCoroutine(ResetAttack());
            Debug.Log("Attack button pressed");
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.1f); // Ждем немного, чтобы анимация атаки началась
        myAnimation.SetBool("Attack", false); // Возвращаем параметр Attack в false
    }
}

  
