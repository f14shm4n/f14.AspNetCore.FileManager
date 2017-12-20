using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using f14.IO;
using Newtonsoft.Json;
using System;
using System.IO;

namespace FormatGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating format...");

            #region Params

            var p_copy = new CopyParam
            {
                CurrentFolderPath = "/",
                SourceDirectory = "/",
                DestinationDirectory = "/",
                Overwrite = false,
                Targets = new System.Collections.Generic.List<f14.AspNetCore.FileManager.Data.BaseActionTarget>
                {
                    new f14.AspNetCore.FileManager.Data.BaseActionTarget
                    {
                        IsFile = true,
                        Name = "sample.txt"
                    }
                }
            };

            var p_move = new MoveParam
            {
                CurrentFolderPath = "/",
                SourceDirectory = "/",
                DestinationDirectory = "/",
                Overwrite = false,
                Targets = new System.Collections.Generic.List<f14.AspNetCore.FileManager.Data.BaseActionTarget>
                {
                    new f14.AspNetCore.FileManager.Data.BaseActionTarget
                    {
                        IsFile = true,
                        Name = "sample.txt"
                    }
                }
            };

            var p_createFolder = new CreateFolderParam
            {
                CurrentFolderPath = "/",
                Name = "new folder name"
            };

            var p_delete = new DeleteParam
            {
                CurrentFolderPath = "/",
                Targets = new System.Collections.Generic.List<string>
                {
                    "itemNameToDelete"
                }
            };

            var p_folderStruct = new FolderStructParam
            {
                CurrentFolderPath = "/",
                FileExtensions = new string[] { ".txt" }
            };

            var p_rename = new RenameParam
            {
                CurrentFolderPath = "/",
                Targets = new System.Collections.Generic.List<f14.AspNetCore.FileManager.Data.RenameActionTarget>
                 {
                     new f14.AspNetCore.FileManager.Data.RenameActionTarget
                     {
                         IsFile = true,
                         Name = "new name",
                         OldName = "old name"
                     }
                 }
            };

            #endregion

            #region Results

            var r_copy = new CopyResult
            {
                Errors = new System.Collections.Generic.List<string>
                {
                    "Error message."
                }
            };

            var r_move = new MoveResult
            {
                Errors = new System.Collections.Generic.List<string>
                {
                    "Error message."
                }
            };

            var r_createFolder = new CreateFolderResult
            {
                Errors = new System.Collections.Generic.List<string>
                {
                    "Error message."
                }
            };

            var r_delete = new DeleteResult
            {
                Affected = 0,
                Errors = new System.Collections.Generic.List<string>
                {
                    "Error message."
                }
            };

            var r_folderStruct = new FolderStructResult
            {
                Errors = new System.Collections.Generic.List<string>
                {
                    "Error message."
                },
                Files = new System.Collections.Generic.List<f14.AspNetCore.FileManager.Data.FileInfoProxy>
                {
                    new f14.AspNetCore.FileManager.Data.FileInfoProxy
                    {
                        Name = "file.name",
                        Properties = new System.Collections.Generic.Dictionary<string, string>
                        {
                            { "Property", "Value" }
                        }
                    }
                },
                Folders = new System.Collections.Generic.List<f14.AspNetCore.FileManager.Data.FileInfoProxy>
                {
                    new f14.AspNetCore.FileManager.Data.FileInfoProxy
                    {
                        Name = "folder name",
                        Properties = new System.Collections.Generic.Dictionary<string, string>
                        {
                            { "Property", "Value" }
                        }
                    }
                }
            };

            var r_rename = new RenameResult
            {
                Affected = 0,
                Errors = new System.Collections.Generic.List<string>
                {
                    "Error message."
                },
                RenamedObjects = new System.Collections.Generic.List<f14.AspNetCore.FileManager.Data.RenameActionTarget>()
                {
                    new f14.AspNetCore.FileManager.Data.RenameActionTarget
                    {
                        IsFile = true,
                        Name = "new name",
                        OldName = "old name"
                    }
                }
            };

            #endregion

            SaveFormat("p_copy.json", p_copy);
            SaveFormat("p_move.json", p_move);
            SaveFormat("p_createFolder.json", p_createFolder);
            SaveFormat("p_delete.json", p_delete);
            SaveFormat("p_folderStruct.json", p_folderStruct);
            SaveFormat("p_rename.json", p_rename);

            SaveFormat("r_copy.json", r_copy);
            SaveFormat("r_move.json", r_move);
            SaveFormat("r_createFolder.json", r_createFolder);
            SaveFormat("r_delete.json", r_delete);
            SaveFormat("r_folderStruct.json", r_folderStruct);
            SaveFormat("r_rename.json", r_rename);
        }

        static void SaveFormat(string fileName, object o)
        {
            const string folder = "format";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            FileIO.WriteData(Path.Combine(folder, fileName), JsonConvert.SerializeObject(o, Formatting.Indented));
        }
    }
}
