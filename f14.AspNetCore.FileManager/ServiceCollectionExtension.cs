using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// The extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds defaults file manager handlers as the <see cref="IOperationHandler{TParam, TResult}"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The services.</returns>
        public static IServiceCollection AddFileManagerHandlers(this IServiceCollection services)
        {
            services.AddTransient<ICopyHandler, CopyHandler>();
            services.AddTransient<ICreateFolderHandler, CreateFolderHandler>();
            services.AddTransient<IDeleteHandler, DeleteHandler>();
            services.AddTransient<IFolderStructHandler, FolderStructHandler>();
            services.AddTransient<IMoveHandler, MoveHandler>();
            services.AddTransient<IRenameHandler, RenameHandler>();
            return services;
        }
    }
}
