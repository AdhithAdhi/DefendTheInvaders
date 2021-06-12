using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public List<CusomCamera> Cameras = new List<CusomCamera>();
    public int activeCameraIndex = -1;
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(1440, 2560, true);
        activeCameraIndex = SaveAndLoad.LoadCamaraViewBy() - 1;
        Switch_Camera();
    }

    public void Switch_Camera()
    {
        activeCameraIndex++;
        if (activeCameraIndex >= Cameras.Count)
        {
            activeCameraIndex = 0;
        }
        text.text = (activeCameraIndex + 1).ToString();
        for (int i = 0; i < Cameras.Count; i++)
        {
            Cameras[i].camera.SetActive(false);
            Camera.main.orthographic = false;
        }
        Cameras[activeCameraIndex].camera.SetActive(true);
        if (Cameras[activeCameraIndex].viewType == CameraViewType.Ortho)
        {
            Camera.main.orthographic = true;

        }
        SaveAndLoad.SaveCamaraViewBy(activeCameraIndex);
        //Debug.LogError(activeCameraIndex+"called");
    }
}

[System.Serializable]
public class CusomCamera
{
    public GameObject camera;
    public CameraViewType viewType;
}
public enum CameraViewType
{
    Perspec,
    Ortho,
}
