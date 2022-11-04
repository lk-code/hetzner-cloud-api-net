using lkcode.hetznercloudapi.Core;
using lkcode.hetznercloudapi.Exceptions;
using lkcode.hetznercloudapi.Objects.Action.Universal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class Action
    {
        #region # static properties #

        private static int _currentPage { get; set; }
        public static int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }

        private static int _maxPages { get; set; }
        public static int MaxPages
        {
            get
            {
                return _maxPages;
            }
            set
            {
                _maxPages = value;
            }
        }

        #endregion

        #region # public properties #

        /// <summary>
        /// ID of the action.
        /// </summary>
        public long Id { get; set; } = 0;

        /// <summary>
        /// Command executed in the action.
        /// </summary>
        public string Command { get; set; } = string.Empty;

        /// <summary>
        /// Status of the action.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Progress of action in percent.
        /// </summary>
        public int Progress { get; set; } = 0;

        /// <summary>
        /// Progress of action in percent.
        /// </summary>
        public DateTime Started { get; set; }

        /// <summary>
        /// Progress of action in percent.
        /// </summary>
        public DateTime Finished { get; set; }

        /// <summary>
        /// Resources the action relates to.
        /// </summary>
        public List<ActionResource> Resources { get; set; }

        /// <summary>
        /// Error message for the action if error occured, otherwise null.
        /// </summary>
        public Error Error { get; set; }

        #endregion

        #region # static methods #

        /// <summary>
        /// Returns all ssh-keys in a list.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Action>> GetAsync(int page = 1)
        {
            if ((_maxPages > 0 && (page <= 0 || page > _maxPages)))
            {
                throw new InvalidPageException("invalid page number (" + page + "). valid values between 1 and " + _maxPages + "");
            }

            List<Action> actionList = new List<Action>();

            string url = string.Format("/actions");
            if (page > 1)
            {
                url += "?page=" + page.ToString();
            }

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Action.Get.Response response = JsonConvert.DeserializeObject<Objects.Action.Get.Response>(responseContent);

            // load meta
            CurrentPage = response.meta.pagination.page;
            float pagesDValue = ((float)response.meta.pagination.total_entries / (float)response.meta.pagination.per_page);
            MaxPages = (int)Math.Ceiling(pagesDValue);

            foreach (Objects.Action.Universal.Action responseAction in response.actions)
            {
                Action action = GetActionFromResponseData(responseAction);

                actionList.Add(action);
            }

            return actionList;
        }

        /// <summary>
        /// Return a ssh-key by the given id.
        /// </summary>
        /// <returns></returns>
        public static async Task<Action> GetAsync(long id)
        {
            Action action = new Action();

            string url = string.Format("/actions/{0}", id);

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Action.GetOne.Response response = JsonConvert.DeserializeObject<Objects.Action.GetOne.Response>(responseContent);

            action = GetActionFromResponseData(response.action);

            return action;
        }

        #endregion

        #region # public methods #

        public Action()
        {

        }

        #endregion

        #region # private methods for processing #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static Action GetActionFromResponseData(Objects.Action.Universal.Action responseData)
        {
            Action action = new Action();

            action.Id = responseData.id;
            action.Command = responseData.command;
            action.Started = responseData.started;
            action.Finished = responseData.finished;
            action.Status = responseData.status;
            action.Progress = responseData.progress;
            action.Resources = new List<ActionResource>();

            foreach(Resource resource in responseData.resources)
            {
                action.Resources.Add(new ActionResource()
                {
                    Id = resource.id,
                    Type = resource.type
                });
            }

            if(responseData.error != null)
            {
                action.Error = new Error()
                {
                    Code = responseData.error.code,
                    Message = responseData.error.message
                };
            }

            return action;
        }

        #endregion
    }
}