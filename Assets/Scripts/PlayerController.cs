using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Dictionary<string, List<string>> adjList;
    string imgName, rightImgName, downImgName, leftImgName, upImgName;

    public void ShiftRight()
    {
        imgName = VRImageLoader.vrImage.name;
        rightImgName = VRImageLoader.adjList[imgName][0];

        if (rightImgName != "-1")
        {
            VRImageLoader.ImageLoader(rightImgName);
        }
    }

    public void ShiftDown()
    {
        imgName = VRImageLoader.vrImage.name;
        downImgName = VRImageLoader.adjList[imgName][1];

        if (downImgName != "-1")
        {
            VRImageLoader.ImageLoader(downImgName);
        }
    }

    public void ShiftLeft()
    {
        imgName = VRImageLoader.vrImage.name;
        leftImgName = VRImageLoader.adjList[imgName][2];

        if (leftImgName != "-1")
        {
            VRImageLoader.ImageLoader(leftImgName);
        }
    }

    public void ShiftUp()
    {
        imgName = VRImageLoader.vrImage.name;
        upImgName = VRImageLoader.adjList[imgName][3];

        if (upImgName != "-1")
        {
            VRImageLoader.ImageLoader(upImgName);
        }
    }
}
