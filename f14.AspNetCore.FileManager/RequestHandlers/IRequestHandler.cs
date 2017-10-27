using f14.AspNetCore.FileManager.RequestModels;
using f14.AspNetCore.FileManager.ResponseModels;

namespace f14.AspNetCore.FileManager.RequestHandlers
{
    /// <summary>
    /// Represents the base request data container.
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// This method must do main part of work by handling request data.
        /// </summary>
        /// <param name="vars">Specified args.</param>
        /// <returns>Some process resutl.</returns>
        ResponseData Handle(RequestData requestData, ServiceVars vars);
    }
}
