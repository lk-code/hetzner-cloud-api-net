using lkcode.hetznercloudapi.Instances.ServerActions;

namespace lkcode.hetznercloudapi.Interfaces;

public interface IServerActionsService
{
    /// <summary>
    /// Shuts down a Server gracefully by sending an ACPI shutdown request. The Server operating system must support ACPI and react to the request, otherwise the Server will not shut down.
    /// </summary>
    /// <param name="id">ID of the Server</param>
    /// <returns></returns>
    Task<ActionResult> Shutdown(long id);
}
