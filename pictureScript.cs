using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class pictureScript : MonoBehaviour
{
    public int location;
    public int thing;


    public RawImage r1;
    public RawImage r2;
    public RawImage r3;
    public RawImage r4;

    string locationThing;




    private void Start()
    {
        showImage(location+""+thing);
    }
    private void Update()
    {
        //Debug.Log(Application.persistentDataPath + "/saves/" + "photo.png");
    }
    public void showPicture()
    {
        //yte[] data = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/saves/" + "photo.png");
        //Texture2D tex = new Texture2D(2, 2);
        //tex.LoadImage(data);
        //GetComponent<Renderer>().material.mainTexture = tex;
        /*
        WWW www = new WWW(Application.persistentDataPath + "/saves/" + "photo.JPG");
        this.GetComponent<AspectRatioFitter>().aspectRatio = (float) www.texture.width / www.texture.height;
        this.GetComponent<RawImage>().texture = www.texture;
        */
        GameObject g = GameObject.Find("Manager");
        int thing = g.GetComponent<Main>().currentThing;

        if (thing == 0)
        {
            this.GetComponent<RawImage>().texture = r1.texture;
        }
        else if (thing == 1)
        {
            this.GetComponent<RawImage>().texture = r2.texture;
        }
        else if (thing == 2)
        {
            this.GetComponent<RawImage>().texture = r3.texture;
        }
        else if (thing == 3)
        {
            this.GetComponent<RawImage>().texture = r4.texture;
        }

    }
    public void showImage(string locationThing)
    {
        /*
        int i = 1;
        string destinationFile = Application.persistentDataPath + "/saves/" + locationThing[0] + "/" + locationThing.Substring(1) + "/" + "photo1.JPG";
        while (File.Exists(destinationFile))
        {
            i++;
            destinationFile = Application.persistentDataPath + "/saves/" + locationThing[0] + "/" + locationThing.Substring(1) + "/" + "photo" + i + ".JPG";
        }
        WWW www = new WWW(Application.persistentDataPath + "/saves/" + locationThing[0] + "/" + locationThing.Substring(1) + "/" + "photo" + (i-1) + ".JPG");
        UnityEngine.Debug.Log(Application.persistentDataPath + "/saves/" + locationThing[0] + "/" + locationThing.Substring(1) + "/" + "photo" + (i-1) + ".JPG");
        this.GetComponent<AspectRatioFitter>().aspectRatio = (float)www.texture.width / www.texture.height;*/
        this.locationThing = locationThing;
        if (locationThing.Substring(1).Equals("0"))
        {
            this.GetComponent<RawImage>().texture = r1.texture;
        }else if (locationThing.Substring(1).Equals("1"))
        {
            this.GetComponent<RawImage>().texture = r2.texture;
        }
        else if(locationThing.Substring(1).Equals("2"))
        {
            this.GetComponent<RawImage>().texture = r3.texture;
        }
        else if(locationThing.Substring(1).Equals("3"))
        {
            this.GetComponent<RawImage>().texture = r4.texture;
        }
    }
    public void savePicture()
    {
        /*
        GameObject g = GameObject.Find("Manager");
        int location = g.GetComponent<Main>().currentPlace;
        int thing = g.GetComponent<Main>().currentThing;

        string sourceFile = Application.persistentDataPath + "/saves/" + "photo.JPG";
        //UnityEngine.Debug.Log(File.Exists(sourceFile));

        int i = 1;
        string destinationFile = Application.persistentDataPath + "/saves/" + location + "/" + thing + "/" + "photo1.JPG";
        while (File.Exists(destinationFile))
        {
            i++;
            destinationFile = Application.persistentDataPath + "/saves/" + location + "/" + thing + "/" + "photo" + i + ".JPG";
        }

        System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/saves/" + location + "/" + thing + "/");

        File.Move(sourceFile, destinationFile);
        */
    }
}
