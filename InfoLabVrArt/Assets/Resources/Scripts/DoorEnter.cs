using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEnter : MonoBehaviour
{
    [SerializeField] private string _scene;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            StartCoroutine(MoveToScene());
        }
    }

    private IEnumerator MoveToScene()
    {
        _animator.SetBool("Opened", true);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(_scene, LoadSceneMode.Single);
    }
}
