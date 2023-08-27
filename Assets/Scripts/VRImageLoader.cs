using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRImageLoader : MonoBehaviour
{
    public TextAsset imageListTextAsset; 
    public GameObject vrImagePrefab, LeftButtonPrefab, RightButtonPrefab, UpButtonPrefab, DownButtonPrefab;
    public static GameObject vrImage, rightButton, leftButton, upButton, downButton;
    public static Shader shader;
    public static Dictionary<string, List<string>> adjList;
    public static List<string> startImgNames;
    GameObject canvasObj;

    private static VRImageLoader instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        shader = Shader.Find("Unlit/Texture");
        canvasObj = GameObject.FindWithTag("canvas");
        adjList = new Dictionary<string, List<string>>();
        startImgNames = new List<string>();

        string[] lines = imageListTextAsset.text.Split('\n');
        string first_line = lines[0].Trim();
        int cnt = -2;
        int num_spots = int.Parse(first_line);
        int sec_flag = 0;
        foreach (string line in lines)
        {
            if(cnt == -2)
            {
                cnt++;
                continue;
            }
            if (!string.IsNullOrEmpty(line))
            {
                int flag = 0;
                for(int i=0; i<line.Length; i++)
                {
                    if(line[i] == ',')
                    {
                        flag = 1;
                        break;
                    }
                }
                if(flag == 0)
                {
                    cnt++;
                    sec_flag = 1;
                }
                else
                {
                    string[] values = line.Split(',');
                    string imageName = values[0].Trim();
                    List<string> value = new List<string>();
                    for (int i = 1; i < 6; i++)
                    {
                        value.Add(values[i].Trim());
                    }
                    value.Add(cnt.ToString());

                    if(sec_flag == 1)
                    {
                        startImgNames.Add(imageName);
                        sec_flag = 0;
                    }
                    adjList.Add(imageName, value);
                }
            }
        }
        vrImage = Instantiate(vrImagePrefab);
        rightButton = Instantiate(RightButtonPrefab);
        //rightButton.transform.SetParent(canvasObj.GetComponent<Transform>(), false);
        rightButton.SetActive(false);
        downButton = Instantiate(DownButtonPrefab);
        //downButton.transform.SetParent(canvasObj.GetComponent<Transform>(), false);
        downButton.SetActive(false);
        leftButton = Instantiate(LeftButtonPrefab);
        //leftButton.transform.SetParent(canvasObj.GetComponent<Transform>(), false);
        leftButton.SetActive(false);
        upButton = Instantiate(UpButtonPrefab);
        //upButton.transform.SetParent(canvasObj.GetComponent<Transform>(), false);
        upButton.SetActive(false);

        //Debug.Log(startImgNames[0]);
        ImageLoader(startImgNames[3]);
    }

    public static void ImageLoader(string imgName)
    {
        string imageUrl = Application.dataPath + "/Images/" + imgName + ".JPG";
        Material material = new Material(shader);

        // Load the image from the specified URL
        Coroutine coroutine = instance.StartCoroutine(LoadImage(imageUrl, texture =>
        {
            // Set the albedo texture property of the material
            material.SetTexture("_MainTex", texture);

            // Assign the material to a game object or renderer
            vrImage.GetComponent<Renderer>().material = material;
            vrImage.name = imgName;
        }));

        vrImage.transform.rotation = Quaternion.Euler(new Vector3(0, float.Parse(adjList[imgName][4]), 0));

        if (adjList[imgName][0] != "-1")
        {
            rightButton.SetActive(true);
        }
        else
        {
            rightButton.SetActive(false);
        }
        if (adjList[imgName][1] != "-1")
        {
            downButton.SetActive(true);
        }
        else
        {
            downButton.SetActive(false);
        }
        if (adjList[imgName][2] != "-1")
        {
            leftButton.SetActive(true);
        }
        else
        {
            leftButton.SetActive(false);
        }
        if (adjList[imgName][3] != "-1")
        {
            upButton.SetActive(true);
        }
        else
        {
            upButton.SetActive(false);
        }
    }
    public static IEnumerator LoadImage(string url, System.Action<Texture2D> callback)
    {
        // Create a new WWW object to load the image from the URL
        WWW www = new WWW(url);

        // Wait for the download to complete
        yield return www;

        // Create a new texture using the downloaded data
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(www.bytes);

        // Invoke the callback with the loaded texture
        callback(texture);
    }
}
