using System;

namespace PlayPhone.MultiNet.Providers
{
  public class MNWSLoader {
    internal const long LOADER_ID_INVALID = -1;
    internal long LoaderId = LOADER_ID_INVALID;

    internal MNWSLoader(long loaderId) {
      this.LoaderId = loaderId;
    }

    public void Cancel() {
      MNDirect.GetWSProvider().CancelRequest(LoaderId);
    }
  }
}

