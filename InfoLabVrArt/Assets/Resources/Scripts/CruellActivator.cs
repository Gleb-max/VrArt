using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CruellActivator : MonoBehaviour
{
    private IEnumerable<Fire> _fires;

    [SerializeField]
    private Material redMaterial;

    [SerializeField]
    private Material whiteMaterial;

    [SerializeField] 
    private AudioSource _fireSounds;

    [SerializeField]
    private float fadeSpeed = 0.001f;

    private bool isDefaultState = true;
    private bool fadeIn = false;
    private bool fadeOut = false;

    void Start()
    {
        _fires = gameObject.GetComponentsInChildren<Fire>();
        gameObject.GetComponent<Renderer>().materials = new Material[] { redMaterial };
        isDefaultState = true;
    }

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            _fireSounds.Play();
            foreach (Fire fire in _fires.AsParallel())
            {
                fire.Play();
            }
            /*fadeIn = true;
            fadeOut = false;
            StartCoroutine(FadeInObject(true));*/
            StartCoroutine(changeMaterial(false));
        }
    }

    void OnTriggerExit(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            foreach (Fire fire in _fires.AsParallel())
            {
                fire.Stop();
            }
            _fireSounds.Stop();
            StartCoroutine(changeMaterial(true));
            /*fadeIn = true;
            fadeOut = false;
            StartCoroutine(FadeInObject(false));*/
        }
    }

    private IEnumerator changeMaterial(bool isActive)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (isDefaultState) yield return new WaitForSeconds(1);
        if (isDefaultState) meshRenderer.material = whiteMaterial;
        else meshRenderer.material = redMaterial;
        isDefaultState = isActive;
    }

    private IEnumerator FadeInObject(bool isActive)
    {
        bool isSuccess = true;
        fadeOut = false;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Color objectColor = meshRenderer.material.color;
        while (objectColor.a < 1)
        {
            if (!fadeIn)
            {
                yield break;
                isSuccess = false;
            }
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            meshRenderer.material.color = objectColor;
            yield return null;
        }
        if (isSuccess)
        {
            StartCoroutine(changeMaterial(isActive));
            fadeOut = true;
            StartCoroutine(FadeOutObject());
            fadeIn = false;
        }
    }

    private IEnumerator FadeOutObject()
    {
        bool isSuccess = true;
        fadeIn = false;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Color objectColor = meshRenderer.material.color;
        while (objectColor.a > 0)
        {
            if (!fadeOut)
            {
                yield break;
                isSuccess = false;
            }
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            meshRenderer.material.color = objectColor;
            yield return null;
        }
        if (isSuccess) 
        {
            fadeOut = false;
        }
    }
}
