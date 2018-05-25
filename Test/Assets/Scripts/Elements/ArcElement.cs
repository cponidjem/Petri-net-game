using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PRECONDITION = 0, POSTCONDITION = 1

public class ArcElement : GameElement
{

    public PlaceElement place;
    public TransitionElement transition;
    public enum ConditionType { PRECONDITION, POSTCONDITION };
    public ConditionType type;
    public int coeff;
    public GameObject coefficientText;

    void Start()
    {
        // Place the arc between place and transition
        float angle = Vector3.SignedAngle(place.getPosition() - transition.getPosition(), Vector3.right, Vector3.forward);
        float angleRad = Mathf.Deg2Rad * angle;
        float placeElementRay = 1F;
        float transitionElementWidth = 0.1F;

        Vector3 placePosition = place.getPosition() + new Vector3(-Mathf.Cos(angleRad),Mathf.Sin(angleRad))* placeElementRay;
        Vector3 transitionPosition = transition.getPosition() +
            new Vector3(transitionElementWidth * Mathf.Sign(place.getPosition().x - transition.getPosition().x), -Mathf.Sin(angleRad));

        // add small change in the angle caused by transitionElementWidth
        angle = Vector3.SignedAngle(placePosition - transitionPosition, new Vector3(1, 0, 0), new Vector3(0, 0, 1));
        // Rotate the arc according to type
        if (type == ConditionType.PRECONDITION)
        {
            angle += 180;
        }

        // position, rotation & scale
        transform.position = placePosition + (transitionPosition - placePosition) / 2;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        float distance = Vector3.Distance(placePosition, transitionPosition);
        transform.localScale = new Vector3(distance/2, transform.localScale.y);


        // COEFFICIENT TEXT
        coefficientText = Instantiate(coefficientText, game.GetComponentInChildren<Canvas>().transform.GetChild(0).transform);
        coefficientText.GetComponent<UnityEngine.UI.Text>().text = coeff.ToString();

        // Move text to its right place + arbitrary numbers added to tune position
        float transformSign = Mathf.Sign(place.getPosition().x - transition.getPosition().x);
        if (type == ConditionType.POSTCONDITION)
        {
            coefficientText.transform.position = place.getPosition() +
                new Vector3(-Mathf.Cos(angleRad + transformSign * 0.2F) * 1.5F, Mathf.Sin(angleRad + transformSign * 0.2F) * 1.5F)*placeElementRay;
        }
        else
        {
            coefficientText.transform.position = transitionPosition +
            new Vector3(3f*transitionElementWidth * transformSign, 0.3F);
        }
    }
    // Update the text to correspond to marking.
    // Not necessary I think
    void Update()
    {
        coefficientText.GetComponent<UnityEngine.UI.Text>().text = coeff.ToString();
    }
}
