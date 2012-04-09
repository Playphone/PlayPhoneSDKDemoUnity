public abstract class PPSView
{
  protected string _viewName;
  public string viewName {get {return _viewName;} }
  
  public PPSView(){}
  public abstract void Draw();
  public abstract void OnClose();
}

