using lkcode.hetznercloudapi.Enums;
using lkcode.hetznercloudapi.Exceptions;
using lkcode.hetznercloudapi.Helper;
using lkcode.hetznercloudapi.Instances.ServerActions;
using lkcode.hetznercloudapi.Models.Api.ServerActions;

namespace lkcode.hetznercloudapi.Mapping;

internal static class ServerActionMappings
{
    internal static ActionResult ToActionResult(this ShutdownResponse response)
    {
        if (response.Action == null)
        {
            throw new InvalidArgumentException("the action property can't be null (invalid api response)");
        }

        ActionResponse action = response.Action;

        IEnumerable<ActionResourceResult> resources = action.Resources
            .Ensure("the progress property can't be null (invalid api response)")
            .Select(x => x.ToActionResourceResult())
        .ToList();

        ServerActionsResult actionStatus = ServerActionsResult.Unknown;
        Enum.TryParse(action.Status.Ensure("the status property can't be null (invalid api response)"), out actionStatus);

        ActionResult result = new ActionResult(action.Command.Ensure("the command property can't be null (invalid api response)"),
            action.Id.Ensure("the id property can't be null (invalid api response)"),
            action.Progress.Ensure("the progress property can't be null (invalid api response)"),
            resources,
            action.Started.Ensure("the started property can't be null (invalid api response)"),
            actionStatus
            );

        if (!string.IsNullOrEmpty(action.Finished))
        {
            result.Finished = DateTime.Parse(action.Finished);
        }

        return result;
    }

    internal static ActionResourceResult ToActionResourceResult(this ResourceResponse response)
    {
        return new ActionResourceResult(response.Id.Ensure("the id property can't be null (invalid api response)"),
            response.Type.Ensure("the type property can't be null (invalid api response)"));
    }
}
