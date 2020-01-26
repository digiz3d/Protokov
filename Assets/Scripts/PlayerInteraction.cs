using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Range(1f, 5f)]
    public float ArmsLength = 2.5f;

    public Camera cam;
    public Image img;
    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t = cam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(t.position, t.forward * ArmsLength, Color.green, 0f);

        RaycastHit hit;

        if (Physics.Raycast(t.position, t.forward, out hit, ArmsLength))
        {
            GameObject target = hit.collider.gameObject;

            Interactible interactibleObject = target.GetComponent<Interactible>();

            if (interactibleObject != null)
            {
                interactibleObject.TriggeredBy(gameObject);

                Debug.Log("c'est pas nul ");
                img.gameObject.SetActive(true);
                return;
            }

        }

        img.gameObject.SetActive(false);
    }
}
