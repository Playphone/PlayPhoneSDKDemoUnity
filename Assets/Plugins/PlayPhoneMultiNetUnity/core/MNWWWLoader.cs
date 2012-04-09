using UnityEngine;
using System.Collections;
using System.Threading;

namespace PlayPhone.MultiNet.Core
{
  public class MNWWWLoader : MonoBehaviour 
  {
    public delegate void WWWLoadedEventHandler(WWW www);

    public IEnumerator Start() 
    {
      yield return www;
      MNTools.DLog("loaded url= " + www.url);

      DestroyLoader();

      LoadedData(www);
    }
    
    public static void LoadData(string URL, WWWLoadedEventHandler WhenLoaded)
    {
      // check if we can start loading new task
      if (!busy)
      {
        busy = true;
        LoadedData = WhenLoaded;
        www = new WWW(URL);
        MNUnityCommunicator.registerComponent(typeof(MNWWWLoader));
      }
      else
      {
        MNTools.DLog("Already inited MNWWWLoader InitLoader(string URL)");
        throw new System.InvalidOperationException("Already inited. Init again after DestroyLoader()");
      }
    }
    
    public static bool IsBusy()
    {
      return busy;
    }

    private static void DestroyLoader()
    {
      MNTools.DLog("testing -- on destroy");
      MNUnityCommunicator.removeComponent(typeof(MNWWWLoader));

      busy = false;
    }

    protected static WWW www = null;
    protected static bool busy = false;   
    private static WWWLoadedEventHandler LoadedData = null;
  }
}