using f14.AspNetCore.FileManager.Data.Params;
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
        /// Adds defaults file manager handlers.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddF14FileManager(this IServiceCollection services)
        {
            services.AddTransient<IOperationHandler<CopyParam>, CopyHandler>();
            services.AddTransient<IOperationHandler<CreateFolderParam>, CreateFolderHandler>();
            services.AddTransient<IOperationHandler<DeleteParam>, DeleteHandler>();
            services.AddTransient<IOperationHandler<FolderStructParam>, FolderStructHandler>();
            services.AddTransient<IOperationHandler<MoveParam>, MoveHandler>();
            services.AddTransient<IOperationHandler<RenameParam>, RenameHandler>();
            return services;
        }
    }
}
