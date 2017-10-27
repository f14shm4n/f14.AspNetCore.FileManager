using f14.AspNetCore.FileManager.RequestHandlers;
using f14.AspNetCore.FileManager.RequestModels;
using f14.AspNetCore.FileManager.ResponseModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Request data manager.
    /// </summary>
    public class RequestHandlerManager
    {
        private class RequestHandlerInfo
        {
            public Type Handler { get; set; }
            public Type RequestData { get; set; }

            public RequestHandlerInfo(Type handler, Type requestData)
            {
                ExHelper.NotNull(() => handler);
                ExHelper.NotNull(() => requestData);

                Handler = handler;
                RequestData = requestData;
            }

            public IRequestHandler MakeHandler() => Activator.CreateInstance(Handler) as IRequestHandler;
            public T MakeRequestData<T>(JObject jobj) where T : RequestData, new() => jobj.ToObject<T>();
            public RequestData MakeRequestData(JObject jobj) => jobj.ToObject(RequestData) as RequestData;
        }

        public class RequestHandlerKeys
        {
            public const string Struct = "struct";
            public const string CreateFolder = "create_folder";
            public const string Rename = "rename";
            public const string Delete = "delete";
            public const string Move = "move";
            public const string Copy = "copy";
        }

        private Dictionary<string, RequestHandlerInfo> _registeredRequestData;

        /// <summary>
        /// Creates new instance of request data manager.
        /// </summary>
        public RequestHandlerManager()
        {
            _registeredRequestData = new Dictionary<string, RequestHandlerInfo>();

            RegisterHandler<StructHandler, StructRequestData>(RequestHandlerKeys.Struct);
            RegisterHandler<DeleteHandler, DeleteRequestData>(RequestHandlerKeys.Delete);
            RegisterHandler<RenameHandler, RenameRequestData>(RequestHandlerKeys.Rename);
            RegisterHandlerForMultipleTypes<MoveHandler, MoveRequestData>(RequestHandlerKeys.Move, RequestHandlerKeys.Copy);
            RegisterHandler<CreateFolderHandler, RequestData>(RequestHandlerKeys.CreateFolder);
        }

        #region Public API

        /// <summary>
        /// Registers the request handler and data types for the specified request type. If collection already contains handler and data then they will be overridden.
        /// </summary>
        /// <typeparam name="THandler">Request handler type.</typeparam>
        /// <typeparam name="TRequestData">Request handler data type.</typeparam>
        /// <param name="requestType">Request handler type key.</param>
        public void RegisterHandler<THandler, TRequestData>(string requestType)
            where THandler : IRequestHandler, new()
            where TRequestData : RequestData, new()
        {
            ExHelper.NotNullString(() => requestType);
            _registeredRequestData[requestType] = new RequestHandlerInfo(typeof(THandler), typeof(TRequestData));
        }
        /// <summary>
        /// Registers the request handler and data types for the specified request types. If collection already contains handler and data then they will be overridden.
        /// </summary>
        /// <typeparam name="THandler">Request handler type.</typeparam>
        /// <typeparam name="TRequestData">Request handler data type.</typeparam>
        /// <param name="requestTypes">Request handler type keys.</param>
        public void RegisterHandlerForMultipleTypes<THandler, TRequestData>(params string[] requestTypes)
            where THandler : IRequestHandler, new()
            where TRequestData : RequestData, new()
        {
            ExHelper.NotNull(() => requestTypes);
            ExHelper.Require(requestTypes.Length > 0, "Request type collection is empty.");

            var info = new RequestHandlerInfo(typeof(THandler), typeof(TRequestData));
            foreach (var k in requestTypes)
            {
                _registeredRequestData[k] = info;
            }
        }
        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <typeparam name="R">Response data type.</typeparam>
        /// <param name="json">Request json data.</param>
        /// <param name="vars">Service extra vars.</param>
        /// <returns>Some response data object.</returns>
        public R HandleRequest<R>(string json, ServiceVars vars) where R : ResponseData, new()
        {
            var jobj = ParseRequestDataObject(json);

            var rhi = GetAndCheckRequestHandlerInfo(jobj);

            var rData = rhi.MakeRequestData(jobj);
            var rHandler = rhi.MakeHandler();

            return rHandler.Handle(rData, vars) as R;
        }
        /// <summary>
        /// Parse the json data to the registered <see cref="RequestData"/> type.
        /// </summary>
        /// <typeparam name="T">Specified <see cref="RequestData"/> type.</typeparam>
        /// <param name="json">Json data.</param>
        /// <returns><see cref="RequestData"/> object.</returns>
        public T ParseRequestData<T>(string json) where T : RequestData, new()
        {
            var jobj = ParseRequestDataObject(json);
            var rhi = GetAndCheckRequestHandlerInfo(jobj);
            return rhi.MakeRequestData<T>(jobj);
        }
        /// <summary>
        /// Parse the json data and return the specified <see cref="IRequestHandler"/> object.
        /// </summary>
        /// <param name="json">Json data.</param>
        /// <returns><see cref="IRequestHandler"/> object.</returns>
        public IRequestHandler GetRequestHandler(string json)
        {
            var jobj = ParseRequestDataObject(json);
            var rhi = GetAndCheckRequestHandlerInfo(jobj);

            return rhi.MakeHandler();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Parse the json data to the JObject and validate it.
        /// </summary>
        /// <param name="json">Json data.</param>
        /// <returns>JObject.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="json"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">If json object does not contains "type" property.</exception>
        private JObject ParseRequestDataObject(string json)
        {
            ExHelper.NotNullString(() => json);
            var parsedJson = JObject.Parse(json);
            return parsedJson;
        }
        /// <summary>
        /// Search <see cref="RequestHandlerInfo"/> for the given type key and validate input and output data.
        /// </summary>
        /// <param name="json">Json data.</param>
        /// <returns><see cref="RequestHandlerInfo"/> object.</returns>
        /// <exception cref="InvalidOperationException">If <see cref="RequestHandlerInfo"/> is not registered for the "type".</exception>
        private RequestHandlerInfo GetAndCheckRequestHandlerInfo(JObject jobj)
        {
            string type = (jobj["type"] ?? jobj["Type"]).Value<string>();

            var rhi = FindRequestHandlerInfo(type);

            ExHelper.Require(rhi != null, $"Unknown request data type: {type}");

            return rhi;
        }
        /// <summary>
        /// Search <see cref="RequestHandlerInfo"/> for the given type key.
        /// </summary>
        /// <param name="key">Type key.</param>
        /// <returns><see cref="RequestHandlerInfo"/> object.</returns>
        private RequestHandlerInfo FindRequestHandlerInfo(string key)
        {
            RequestHandlerInfo info;
            _registeredRequestData.TryGetValue(key, out info);
            return info;
        }

        #endregion
    }
}
