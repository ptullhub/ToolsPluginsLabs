using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImageManager : MonoBehaviour
{
    private string[] webImages = new string[]
    {
        "https://upload.wikimedia.org/wikipedia/commons/thumb/1/15/Cat_August_2010-4.jpg/2560px-Cat_August_2010-4.jpg",
        "https://upload.wikimedia.org/wikipedia/en/thumb/c/c2/Peter_Griffin.png/220px-Peter_Griffin.png",
        "https://cdn.prod.website-files.com/5d7e8885cad5174a2fcb98d7/64933f98a477f02e36a282d1_5eddd950e5cf1ec1fa5c2d83_virtual-influencer-john-pork.jpeg"
    };

    private Dictionary<string, Texture2D> imageCache = new Dictionary<string, Texture2D>();
    private Dictionary<string, bool> ongoingDownloads = new Dictionary<string, bool>();

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject billboard = GameObject.CreatePrimitive(PrimitiveType.Quad);
            billboard.transform.position = new Vector3(i * 2, 0, 0);

            GetWebImage((texture) =>
            {
                Renderer renderer = billboard.GetComponent<Renderer>();
                renderer.material.mainTexture = texture;
            });
        }
    }
    private IEnumerator DownloadImage(string url, Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(request);

            if (!imageCache.ContainsKey(url))
            {
                imageCache[url] = downloadedTexture;
            }

            callback(downloadedTexture);
        }
        else
        {
            Debug.LogError($"Failed to download image from {url}: {request.error}");
        }

        ongoingDownloads[url] = false;
    }
    public void GetWebImage(Action<Texture2D> callback)
    {
        string selectedUrl = webImages[UnityEngine.Random.Range(0, webImages.Length)].Trim();

        if (imageCache.TryGetValue(selectedUrl, out Texture2D cachedTexture))
        {
            Debug.Log($"Using cached image for URL: {selectedUrl}");
            callback(cachedTexture); 
            return;
        }

        if (ongoingDownloads.ContainsKey(selectedUrl) && ongoingDownloads[selectedUrl])
        {
            Debug.Log($"Download already in progress for URL: {selectedUrl}");
            StartCoroutine(WaitForDownload(selectedUrl, callback));
            return;
        }

        Debug.Log($"No cached image found for URL: {selectedUrl}. Downloading...");
        ongoingDownloads[selectedUrl] = true;

        StartCoroutine(DownloadImage(selectedUrl, (texture) =>
        {
            if (texture != null)
            {
                Debug.Log($"Image downloaded and cached for URL: {selectedUrl}");
                callback(texture);
            }
            else
            {
                Debug.LogError($"Failed to download image from URL: {selectedUrl}");
            }
        }));
    }
    private IEnumerator WaitForDownload(string url, Action<Texture2D> callback)
    {
        while (ongoingDownloads[url])
        {
            yield return null;
        }

        if (imageCache.TryGetValue(url, out Texture2D cachedTexture))
        {
            callback(cachedTexture);
        }
    }
}
