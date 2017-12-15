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
        /// Adds defaults file manager handlers as the <see cref="IOperationHandler{T}"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileManagerHandlers(this IServiceCollection services)
        {
            services.AddTransient<IOperationHandler<CopyParam>, CopyHandler>();
            services.AddTransient<IOperationHandler<CreateFolderParam>, CreateFolderHandler>();
            services.AddTransient<IOperationHandler<DeleteParam>, DeleteHandler>();
            services.AddTransient<IOperationHandler<FolderStructParam>, FolderStructHandler>();
            services.AddTransient<IOperationHandler<MoveParam>, MoveHandler>();
            services.AddTransient<IOperationHandler<RenameParam>, RenameHandler>();
            return services;
        }
        /// <summary>
        /// Adds defaults file manager handlers as the <see cref="IJOperationHandler{T}"/> to use build-in json parser functions.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileManagerJsonFileManager(this IServiceCollection services)
        {
            services.AddTransient<IJOperationHandler<CopyParam>, CopyHandler>();
            services.AddTransient<IJOperationHandler<CreateFolderParam>, CreateFolderHandler>();
            services.AddTransient<IJOperationHandler<DeleteParam>, DeleteHandler>();
            services.AddTransient<IJOperationHandler<FolderStructParam>, FolderStructHandler>();
            services.AddTransient<IJOperationHandler<MoveParam>, MoveHandler>();
            services.AddTransient<IJOperationHandler<RenameParam>, RenameHandler>();
            return services;
        }
    }
}
