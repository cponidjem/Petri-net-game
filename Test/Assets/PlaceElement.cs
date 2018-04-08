using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceElement : GameElement {

    public int id;
    public int marking;

    public GameObject numberOfTokensText;

    // Add text representing the marking.
    void Start()
    {
        numberOfTokensText = Instantiate(numberOfTokensText, transform.position,transform.rotation, game.GetComponentInChildren<Canvas>().transform);
        numberOfTokensText.GetComponent<UnityEngine.UI.Text>().text = marking.ToString();
    }

    // Update the text to correspond to marking.
    void Update()
    {
        numberOfTokensText.GetComponent<UnityEngine.UI.Text>().text = marking.ToString();
        numberOfTokensText.transform.position = transform.position;
    }

}
