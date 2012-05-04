using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;

namespace PlayPhone.MultiNet.Core
 {
  public class MNUtils
   {
    public static Dictionary<string,string> HttpGetRequestParseParams (string queryParams)
     {
      Dictionary<string,string> result = new Dictionary<string,string>();

      queryParams = queryParams.Trim();

      if (queryParams.Length > 0)
       {
        string[] parts = queryParams.Replace('+',' ').Split('&');

        foreach (string param in parts)
         {
          int index = param.IndexOf('=');

          if (index >= 0)
           {
            string name  = param.Substring(0,index);
            string value = param.Substring(index + 1);

            result.Add(WWW.UnEscapeURL(name,REQUEST_ENCODING_DEFAULT),WWW.UnEscapeURL(value,REQUEST_ENCODING_DEFAULT));
           }
          else
           {
            result.Add(WWW.UnEscapeURL(param,REQUEST_ENCODING_DEFAULT),"");
           }
         }
       }

      return result;
    }

    public static string HttpGetRequestBuildParamsString (Dictionary<string,string> queryParams)
    {
      MNStringJoiner paramString = new MNStringJoiner("&");

      foreach (var param in queryParams)
       {
        paramString.Join(WWW.EscapeURL(param.Key,REQUEST_ENCODING_DEFAULT) + "=" + WWW.EscapeURL(param.Value,REQUEST_ENCODING_DEFAULT));
       }

      return paramString.ToString();
    }

    private static System.Text.Encoding REQUEST_ENCODING_DEFAULT = System.Text.Encoding.UTF8;
  }

  public class MNStringJoiner
   {
    public MNStringJoiner (string joinString)
     {
      this.joinString = joinString;
      empty           = true;
      str             = new StringBuilder();
     }

    public void Join (string str)
     {
      if (!empty)
       {
        this.str.Append(joinString);
       }
      else
       {
        empty = false;
       }

      this.str.Append(str);
     }

    public override string ToString ()
     {
      return this.str.ToString();
     }

    private string        joinString;
    private bool          empty;
    private StringBuilder str;
   }
 }

