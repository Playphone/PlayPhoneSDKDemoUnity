
using PlayPhone.MultiNet;
using PlayPhone.MultiNet.Providers;
using PlayPhone.MultiNet.Core;

public abstract class PPSDemoViewAbstract : PPSView
{
  public new string viewName{ 
                              get{ return _viewName;}
                              set{_viewName = value;}
                            }
  public PPSDemoViewAbstract()
  {
    PPSDemoInfoStorage.GetInstance().UserLoggedIn += UserLoggedIn;
    PPSDemoInfoStorage.GetInstance().UserLoggedOut += UserLoggedOut;
  }


  public override void Draw()
  {
    // empty implementation
  }

  public virtual void UserLoggedIn(){}
  public virtual void UserLoggedOut(){}
  
  public override void OnClose()
  {
    // empty implementation
  }
}

