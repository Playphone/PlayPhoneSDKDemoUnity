//
//  MNWSInfoRequestAnyUser.java
//  MultiNet client
//
//  Copyright 2012 PlayPhone. All rights reserved.
//

package com.playphone.multinet.providers;

import com.playphone.multinet.core.ws.MNWSRequestContent;
import com.playphone.multinet.core.ws.MNWSResponse;
import com.playphone.multinet.core.ws.MNWSRequestError;
import com.playphone.multinet.core.ws.data.MNWSAnyUserItem;

public class MNWSInfoRequestAnyUser extends MNWSInfoRequest
 {
  public MNWSInfoRequestAnyUser (long userId, IEventHandler eventHandler)
   {
    this.userId       = userId;
    this.eventHandler = eventHandler;
   }

  /* package */ void addContent (MNWSRequestContent content)
   {
    blockName = content.addAnyUser(userId);
   }

  void handleRequestCompleted (MNWSResponse       response)
   {
    MNWSAnyUserItem data = (MNWSAnyUserItem)response.getDataForBlock(blockName);

    RequestResult result = new RequestResult(data);

    eventHandler.onCompleted(result);
   }

  void handleRequestError (MNWSRequestError   error)
   {
    RequestResult result = new RequestResult(null);

    result.setError(error.getMessage());

    eventHandler.onCompleted(result);
   }

  public interface IEventHandler
   {
    public void onCompleted (RequestResult result);
   }

  public static class EventHandlerAbstract implements IEventHandler
   {
    public void onCompleted (RequestResult result)
     {
     }
   }

  public static class RequestResult extends MNWSInfoRequest.RequestResult
   {
    public MNWSAnyUserItem getDataEntry ()
     {
      return data;
     }

    private RequestResult (MNWSAnyUserItem data)
     {
      super();

      this.data = data;
     }

    private MNWSAnyUserItem data;
   }

  private final long          userId;
  private final IEventHandler eventHandler;
  private String              blockName;
 }

