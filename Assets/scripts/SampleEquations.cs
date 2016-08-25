using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SampleEquations : MonoBehaviour
{

    public GameObject drop_down;

    public GameObject X_In;
    public GameObject Y_In;
    public GameObject Z_In;

    public GameObject R_min;
    public GameObject R_max;
    public GameObject S_min;
    public GameObject S_max;
    public GameObject Resolution;

    public void changeEquation()
    {
        switch (drop_down.GetComponent<Dropdown>().value)
        {
            case 0:
                // Torus
                X_In.GetComponent<InputField>().text = "(0.75 + 0.25 * cos(r)) * cos(s)";
                Y_In.GetComponent<InputField>().text = "(0.75 + 0.25 * cos(r)) * sin(s)";
                Z_In.GetComponent<InputField>().text = "0.25 * sin(r)";

                R_min.GetComponent<InputField>().text = "0";
                R_max.GetComponent<InputField>().text = "6.28318";
                S_min.GetComponent<InputField>().text = "0";
                S_max.GetComponent<InputField>().text = "6.28318";

                Resolution.GetComponent<Slider>().value = 20;
                break;
            case 1:
                // Mobius Strip
                X_In.GetComponent<InputField>().text = "(1 + (s / 2) * cos(r / 2)) * cos(r)";
                Y_In.GetComponent<InputField>().text = "(1 + (s / 2) * cos(r / 2)) * sin(r)";
                Z_In.GetComponent<InputField>().text = "(s / 2) * sin(r / 2)";

                R_min.GetComponent<InputField>().text = "0";
                R_max.GetComponent<InputField>().text = "6.28318";
                S_min.GetComponent<InputField>().text = "-1";
                S_max.GetComponent<InputField>().text = "1";

                Resolution.GetComponent<Slider>().value = 20;
                break;
            case 2:
                // Klein Bottle
                X_In.GetComponent<InputField>().text = "(2 + cos(r / 2) * sin(s) - sin(r / 2) * sin(2 * s)) * cos(r)";
                Y_In.GetComponent<InputField>().text = "(2 + cos(r / 2) * sin(s) - sin(r / 2) * sin(2 * s)) * sin(r)";
                Z_In.GetComponent<InputField>().text = "sin(r / 2) * sin(s) + cos(r / 2) * sin(2 * s)";

                R_min.GetComponent<InputField>().text = "0";
                R_max.GetComponent<InputField>().text = "6.28318";
                S_min.GetComponent<InputField>().text = "0";
                S_max.GetComponent<InputField>().text = "6.28318";

                Resolution.GetComponent<Slider>().value = 20;
                break;
            default:
                break;
        }

    }
}
