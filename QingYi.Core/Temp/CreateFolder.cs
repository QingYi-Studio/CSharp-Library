﻿namespace QingYi.Core.Temp
{
    public class CreateFolder
    {
        /// <summary>
        /// Create a new subfolder within the Temp folder<br></br>
        /// 在Temp文件夹内创建新的子文件夹
        /// </summary>
        /// <param name="newFolderName">
        ///     The name of the new folder you want to create<br></br>
        ///     想要创建的新文件夹名称
        /// </param>
        public static void Create(string newFolderName)
        {
            Folder.Create.CreateFolder(newFolderName, GetTempFolder.Get());
        }
    }
}
