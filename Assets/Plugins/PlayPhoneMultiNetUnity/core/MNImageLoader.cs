using UnityEngine;
using System.Collections;

namespace PlayPhone.MultiNet.Core
{
  public class MNImageLoader : MonoBehaviour 
  {
    public delegate void ImageLoadedEventHandler(Texture2D Image);

    public static MNImageLoader GetInstance ()
    {
     if (instance == null)
     {
      instance = new MNImageLoader();
     }

     return instance;
    }

    public void Load(string URL, ImageLoadedEventHandler imageLoadedEventHandler)
    {
      queue.Enqueue(new TaskInfo(URL,imageLoadedEventHandler));
      LoadTask();
    }

    private void LoadTask()
    {
     if (queue.Count > 0)
      {
        if (!MNWWWLoader.IsBusy())
        {
          // load next element in queue
          MNWWWLoader.LoadData(((TaskInfo)queue.Peek()).URL, OnWWWLoaded);
        }
      }
    }
  
    private void OnWWWLoaded (WWW www) 
    {
      // when www loaded, return image
      ((TaskInfo)queue.Peek()).ImageLoadedEventHandler(ConvertWWWtoTexture(www));
      // and begin loading next element in queue
      queue.Dequeue();
      LoadTask();
    }

    private static MNImageLoader instance = null;

    private MNImageLoader()
    {
    }

    private Texture2D ConvertWWWtoTexture(WWW www)
    {
     Texture2D tex = null;

     if (www.error == null)
     {
      try
      {
       tex = www.texture;
      }
      catch
      {
      }
     }
     else
     {
        MNTools.DLog("www error" + www.error);
     }

      return tex;
     }
    
    protected class TaskInfo
    {
      public string URL;
      public MNImageLoader.ImageLoadedEventHandler ImageLoadedEventHandler = null;
      public TaskInfo(string URL, MNImageLoader.ImageLoadedEventHandler ImageLoadedEventHandler)
      {
        this.URL = URL;
        this.ImageLoadedEventHandler = ImageLoadedEventHandler;
      }
    }

    protected Queue queue = new Queue();     
  }
}
