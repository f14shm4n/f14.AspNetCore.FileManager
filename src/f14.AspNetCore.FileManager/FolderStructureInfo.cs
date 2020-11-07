using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Provides information about files and folders that contains inside the particular folder. Folders and files of the sub-folders are not included.
    /// </summary>
    public class FolderStructureInfo
    {
        /// <summary>
        /// Folders count.
        /// </summary>
        public int FolderCount => Folders.Count;

        /// <summary>
        /// Files count.
        /// </summary>        
        public int FileCount => Files.Count;

        /// <summary>
        /// Folders collection.
        /// </summary>
        public List<FileSystemObjectInfo> Folders { get; } = new List<FileSystemObjectInfo>();

        /// <summary>
        /// Files collection.
        /// </summary>
        public List<FileSystemObjectInfo> Files { get; } = new List<FileSystemObjectInfo>();

        /// <summary>
        /// Sets the files list.
        /// </summary>
        /// <param name="fileSystemInfos">Sources.</param>
        public void SetFiles(IEnumerable<FileSystemInfo> fileSystemInfos) => Fill(Files, fileSystemInfos);

        /// <summary>
        /// Sets the folders list.
        /// </summary>
        /// <param name="fileSystemInfos">Sources.</param>
        public void SetFolders(IEnumerable<FileSystemInfo> fileSystemInfos) => Fill(Folders, fileSystemInfos);

        /// <summary>
        /// Projects each <see cref="FileSystemInfo"/> into the <see cref="FileSystemObjectInfo"/>.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        private void Fill(IList<FileSystemObjectInfo> to, IEnumerable<FileSystemInfo> from)
        {
            if (from?.Any() == true)
            {
                foreach (var o in from)
                {
                    to.Add(new FileSystemObjectInfo(o));
                }
            }
        }
    }
}
