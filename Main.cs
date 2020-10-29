using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Transform[] views;
    public float transitionSpeed;
    public int currentPlace = -1;
    public int currentThing = -1;
    Transform currentView;
    // Start is called before the first frame update
    void Start()
    {
        //Camera control
        currentView = views[0];

        //addName();
        SaveData.current.profile = (PlayerProfile) SerializationManager.Load(Application.persistentDataPath + "/saves/" + "playerProgress" + ".save");
        //SaveData.current.profile.setStars(0, 0, 5);
    }
    void Update()
    {
        //UnityEngine.Debug.Log("--->"+SaveData.current.profile.getLifePoints(0));
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnityEngine.Debug.Log("YAS");
            setView(1);
        }
        //UnityEngine.Debug.Log(Application.persistentDataPath);
    }
    void LateUpdate()
    {

        //UnityEngine.Debug.Log("[LateUpdate]switch to view" + currentView);
        //Lerp position
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        Vector3 currentAngle = new Vector3(
        Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));
        transform.eulerAngles = currentAngle;

    }
    public void addName()
    {
        SaveData.current.profile = new PlayerProfile();
        SaveData.current.profile.playerName = "Darren";
        SerializationManager.Save("playerProgress", SaveData.current.profile);
    }
    public void setView(int view)
    {
        UnityEngine.Debug.Log("switch to view"+view);
        currentView = views[view];
        UnityEngine.Debug.Log(currentView);
    }
    public void showPicture()
    {
        byte[] data = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/saves/" + "photo.JPG");
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(data);
        GetComponent<Renderer>().material.mainTexture = tex;
    }
    public void displayLevel()
    {
        this.GetComponent<Text>().text = SaveData.current.profile.getTotalLevel()+"";
    }
    public void setThing(int x)
    {
        currentThing = x;
    }
}