using System.Collections;
using System.Collections.Generic;

public class PPSStackView : PPSView
{
  public PPSStackView()
  {
    viewStack = new Stack<PPSView>();
  }

  public int Count()
  {
    return viewStack.Count;
  }
  
  public void Push(PPSView ppsView)
  {
    viewStack.Push(ppsView);
  }
  
  public PPSView Peek()
  {
    return viewStack.Peek();
  }
  
  public void Pop()
  {
    viewStack.Peek().OnClose();
    viewStack.Pop();
  }
  
  public string getViewName()
  {
    return viewStack.Peek().viewName;
  }
  
  public override void Draw() 
  {
    viewStack.Peek().Draw();
  }
  
  public override void OnClose()
  {
    // empty implementation
  }

  protected Stack<PPSView> viewStack;
}