using UnityEngine;
using UnityEngine.UI;

public class loadingbar : MonoBehaviour
{
    Slider bar;
    void Awake()
    {
        bar = GetComponent<Slider>();
    }
    void FixedUpdate()
    {
        bar.value += 7 * Time.deltaTime; // lol
    }
}
