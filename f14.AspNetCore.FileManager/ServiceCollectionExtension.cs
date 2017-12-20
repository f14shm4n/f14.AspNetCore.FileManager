using f14.AspNetCore.FileManager.Abstractions;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using f14.AspNetCore.FileManager.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds defaults file manager handlers as the <see cref="IOperationHandler{T}"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
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
