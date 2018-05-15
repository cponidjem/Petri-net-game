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
        Vector3 sourcePosition = place.getPosition();
        Vector3 destinationPosition = transition.getPosition();
		float correctionCoef = 0.45f;

		float posX = 0.0f;
		if (type == ConditionType.PRECONDITION) {
			posX = sourcePosition.x + correctionCoef + (destinationPosition.x - sourcePosition.x) / 2;
		} else {
			posX = sourcePosition.x - correctionCoef + (destinationPosition.x - sourcePosition.x) / 2;
		}
        transform.position = new Vector3(
			posX,
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
        // Initialise coefficient text
        coefficientText = Instantiate(coefficientText, transform.position, transform.rotation, game.GetComponentInChildren<Canvas>().transform);
        coefficientText.GetComponent<UnityEngine.UI.Text>().text = coeff.ToString();
    }
    // Update the text to correspond to marking.
    void Update()
    {
        coefficientText.GetComponent<UnityEngine.UI.Text>().text = coeff.ToString();
        coefficientText.transform.position = transform.position;
    }
}
