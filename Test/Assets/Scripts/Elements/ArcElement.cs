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
        float r = 1F;
        float transitionWidth = 0.1F;

        Vector3 sourcePosition = place.getPosition() + new Vector3(-Mathf.Cos(angleRad) * r, Mathf.Sin(angleRad) * r);
        Vector3 destinationPosition = transition.getPosition() +
            new Vector3(transitionWidth * Mathf.Sign(place.getPosition().x - transition.getPosition().x), 0);

        transform.position = new Vector3(
            sourcePosition.x + (destinationPosition.x - sourcePosition.x) / 2,
            sourcePosition.y + (destinationPosition.y - sourcePosition.y) / 2,
            sourcePosition.z + (destinationPosition.z - sourcePosition.z) / 2);

        // Rotate the arc to right angle & according to type
        float z_angle_deg =
            (180 / Mathf.PI) *
            Mathf.Atan(
            ((sourcePosition.y - destinationPosition.y) / 2) /
            ((sourcePosition.x - destinationPosition.x) / 2));

        if ((sourcePosition.x - destinationPosition.x) > 0)
        {
            z_angle_deg = z_angle_deg + 180;
        }
        if (type == ConditionType.POSTCONDITION)
        {
            z_angle_deg = z_angle_deg + 180;
        }
        Vector3 rotationChange = new Vector3(0, 0, z_angle_deg);
        transform.Rotate(rotationChange);
        // Scale the arc according to the distance
        float distance = Vector3.Distance(sourcePosition, destinationPosition);
        float scaleFactor = distance / 2;
        transform.localScale = new Vector3(transform.localScale.x * scaleFactor, transform.localScale.y);

        // Initialise coefficient text
        coefficientText = Instantiate(coefficientText, game.GetComponentInChildren<Canvas>().transform.GetChild(0).transform);
        
        coefficientText.GetComponent<UnityEngine.UI.Text>().text = coeff.ToString();

        // Move text to its right place + arbitrary numbers added to tune position
        float transformSign = Mathf.Sign(place.getPosition().x - transition.getPosition().x);
        if (type == ConditionType.POSTCONDITION)
        {
            coefficientText.transform.position = place.getPosition() +
                new Vector3(-Mathf.Cos(angleRad + transformSign * 0.2F) * 1.5F * r, Mathf.Sin(angleRad + transformSign * 0.2F) * 1.5F * r);
        }
        else
        {
            coefficientText.transform.position = transition.getPosition() +
            new Vector3(5.5F * transitionWidth * transformSign, 0.3F);
        }
    }
    // Update the text to correspond to marking.
    // Not necessary I think
    void Update()
    {
        coefficientText.GetComponent<UnityEngine.UI.Text>().text = coeff.ToString();
    }
}
