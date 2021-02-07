using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileControl
{
    /// <summary>
    /// 文件功能类，实现所有功能
    /// </summary>
    class FileFunction
    {
        #region 属性
        private String filePath;
        private String dirName;
        private String fileName;
        private String fileExtName;
        private String fileRealName;
        private FileInfo file;
        #endregion
        #region 辅助函数
        public FileFunction(FileInfo file)
        {
            this.file = file;
            this.filePath = file.FullName;
            if (file.Exists)
            {
                this.fileName = file.Name;
                this.fileExtName = getExt(file.Name);
                this.fileRealName = getReal(fileName);
                this.dirName = file.Directory.FullName;
            }
            else
            {
                System.Console.WriteLine("文件不存在.......");
                System.Environment.Exit(0);
            }
        }

        private String getExt(String fileName)
        {
            int index = fileName.LastIndexOf(".");
            if (index != -1)
            {
                return fileName.Substring(index+1);
            }

            return "";
        }
        private String getReal(String fileName)
        {
            int index = fileName.LastIndexOf(".");
            if (index != -1)
            {
                return fileName.Substring(0,index);
            }

            return "";
        }
        public String getRandomName()
        {
            DateTime time = DateTime.Now;
            Random rnd = new Random();
            int rndDb = (int)(rnd.NextDouble() * 1000);
            return time.ToString("yyyyMMddHHmmss")+rndDb;
        }
        public String FileExtName
        {
            get { return this.fileExtName; }
        }
        public String FilePath
        {
            get { return this.filePath; }
        }
        public String FileName
        {
            get { return this.fileName; }
        }
        public String FileRealName
        {
            get { return this.fileRealName; }
        }
        public String DirName
        {
            get { return this.dirName; }
        }
        #endregion
        #region 功能函数
        /// <summary>
        /// 修改文件的扩展名
        /// </summary>
        /// <param name="extName"></param>
        public void ModifyExt(String extName)
        {
            if (!String.IsNullOrEmpty(extName))
            {
                FileInfo temp = new FileInfo(dirName + "/" + fileRealName + "." + extName);
                if (temp.Exists)
                {
                    System.Console.WriteLine("异常：文件名"+temp.FullName+"已经存在,"+this.filePath+"未修改");
                    return;
                }
                System.Console.WriteLine("修改文件[" + this.filePath + "]的文件扩展名[" + extName + "].......");
                file.MoveTo(dirName + "/" + fileRealName + "." + extName);
                this.filePath = file.FullName;
                this.fileName = file.Name;
                this.fileExtName = getExt(fileName);
            }
        }
        /// <summary>
        /// 修改文件的真实名部分
        /// </summary>
        /// <param name="realName"></param>
        public void ModifyRealName(String realName)
        {
            if (!String.IsNullOrEmpty(realName))
            {
                FileInfo temp = new FileInfo(dirName + "/" + realName + "." + fileExtName);
                if (temp.Exists)
                {
                    System.Console.WriteLine("异常：文件名" + temp.FullName + "已经存在," + this.filePath + "未修改");
                    return;
                }
                System.Console.WriteLine("修改文件[" + this.filePath + "]的文件名[" + realName + "].......");
                file.MoveTo(dirName + "/" + realName + "." + fileExtName);
                this.filePath = file.FullName;
                this.fileName = file.Name;
                this.fileRealName = getReal(fileName);
            }
        }
        /// <summary>
        /// 改变全文件名
        /// </summary>
        /// <param name="name"></param>
        public void ModifyName(String name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                FileInfo temp = new FileInfo(dirName + "/" + name);
                if (temp.Exists)
                {
                    System.Console.WriteLine("异常：文件名" + temp.FullName + "已经存在," + this.filePath + "未修改");
                    return;
                }
                System.Console.WriteLine("修改文件[" + this.filePath + "]的全文件名[" + name + "].......");
                file.MoveTo(dirName + "/" + name);
                this.filePath = file.FullName;
                this.fileName = file.Name;
                this.fileExtName = getExt(fileName);
                this.fileRealName = getReal(fileName);
            }
        }
        /// <summary>
        /// 移动文件到指定路径，分隔符"/"
        /// </summary>
        /// <param name="path"></param>
        public void MoveTo(String path)
        {
            System.Console.WriteLine("修改文件[" + this.filePath + "]的文件路径[" + path + "].......");
            if (!path.EndsWith("/"))
                path += "/";

            FileInfo newFile = new FileInfo(path + fileName);
            if (!newFile.Exists)
            {
                file.MoveTo(path + fileName);
            }
            else
            {
                String name = "";
                do
                {
                    name = getRandomName() + "." + fileExtName;
                    newFile = new FileInfo(path + name);
                    if (!newFile.Exists)
                        break;
                } while (true);
                file.MoveTo(path + name);
            }

            this.filePath = file.FullName;
            this.dirName = file.DirectoryName;
        }
        /// <summary>
        /// 复制文件到指定目录
        /// </summary>
        /// <param name="path"></param>
        public void CopyTo(String path)
        {
            System.Console.WriteLine("复制文件[" + this.filePath + "]的文件路径[" + path + "].......");
            if (!path.EndsWith("/") && !path.EndsWith("\\"))
                path += "/";

            FileInfo newFile = new FileInfo(path + fileName);
            if (!newFile.Exists)
            {
                file.CopyTo(path + fileName);
            }
            else
            {
                String name = "";
                do
                {
                    name = getRandomName() + "." + fileExtName;
                    newFile = new FileInfo(path + name);
                    if (!newFile.Exists)
                        break;
                } while (true);
                file.CopyTo(path + name);
            }
            
        }
        #endregion
        #region 扩展内容
        
        #endregion
    }
}
