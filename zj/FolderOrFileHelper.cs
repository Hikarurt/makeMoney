using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace zj
{
    public class FolderOrFileHelper
    {
        //Display subdirectorty information（在同一类下面引发的方法必须为静态）
        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns>0不存在1存在</returns>
        public int DisplayFolder(string folderPath)
        {
            DirectoryInfo theFolder = new DirectoryInfo(folderPath);
            string folderName = folderPath.Substring(22);
            //Console.WriteLine(folderName);
            if (!theFolder.Exists)
                return 0;
            else
                return 1;
        }

        //Move folder
        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// <param name="sourceFolderName"></param>
        /// <param name="destFolderName"></param>
        public void MoveDirectory(string sourceFolderName, string destFolderName)
        {
            if (Directory.Exists(sourceFolderName))
            {
                Directory.Move(sourceFolderName, destFolderName);
            }

        }
        //Copy file
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        public void CopyFile(string sourceFileName, string destFileName)
        {
            if (File.Exists(sourceFileName))
            {
                File.Copy(sourceFileName, destFileName);
            }

        }
        //Delete folder
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="folderPath"></param>
        public void DeleteDirectory(string folderPath)
        {
            DirectoryInfo folderInfo = new DirectoryInfo(folderPath);
            if (folderInfo.Exists)
            {
                folderInfo.Delete();//该目录下必须为空，若有文件需要先删除文件
            }
        }

        //Delete file
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        public void DeleteFile(string folderPath, string fileName)
        {
            string fileFullName = Path.Combine(folderPath, fileName);
            FileInfo fileInfo = new FileInfo(fileFullName);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }
    }
}
