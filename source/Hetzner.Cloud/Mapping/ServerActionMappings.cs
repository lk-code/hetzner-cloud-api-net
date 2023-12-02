// using lkcode.hetznercloudapi.Enums;
// using lkcode.hetznercloudapi.Exceptions;
// using lkcode.hetznercloudapi.Helper;
// using lkcode.hetznercloudapi.Instances.ServerActions;
// using lkcode.hetznercloudapi.Models.Api.ServerActions;
//
// namespace lkcode.hetznercloudapi.Mapping;
//
// internal static class ServerActionMappings
// {
//     internal static Instances.ServerActions.ServerAction ToActionResult(this ActionResponse response)
//     {
//         IEnumerable<ActionResourceResult> resources = response.Resources
//             .Ensure("the progress property can't be null (invalid api response)")
//             .Select(x => x.ToActionResourceResult())
//         .ToList();
//
//         ServerActionsResult actionStatus = ServerActionsResult.Unknown;
//         Enum.TryParse(response.Status.Ensure("the status property can't be null (invalid api response)"), out actionStatus);
//
//         Instances.ServerActions.ServerAction result = new Instances.ServerActions.ServerAction(response.Command.Ensure("the command property can't be null (invalid api response)"),
//             response.Id.Ensure("the id property can't be null (invalid api response)"),
//             response.Progress.Ensure("the progress property can't be null (invalid api response)"),
//             resources,
//             response.Started.Ensure("the started property can't be null (invalid api response)"),
//             actionStatus
//             );
//
//         if (!string.IsNullOrEmpty(response.Finished))
//         {
//             result.Finished = DateTime.Parse(response.Finished);
//         }
//
//         return result;
//     }
//
//     internal static ActionResourceResult ToActionResourceResult(this ResourceResponse response)
//     {
//         return new ActionResourceResult(response.Id.Ensure("the id property can't be null (invalid api response)"),
//             response.Type.Ensure("the type property can't be null (invalid api response)"));
//     }
// }
