using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileControl
{
    /// <summary>
    /// 文件夹功能类
    /// </summary>
    class DirectoryFunction
    {
        #region 属性
        private String dirName;
        private DirectoryInfo dir;
        private DirectoryInfo[] childDirs;
        private FileInfo[] childFiles;
        #endregion
        #region 辅助函数
        public DirectoryFunction(DirectoryInfo dir)
        {
            this.dir = dir;
            this.dirName = dir.FullName;
            if (!dir.Exists)
            {
                System.Console.WriteLine("目录不存在......");
                System.Environment.Exit(0);
            }
            else
            {
                childFiles = dir.GetFiles();
                childDirs = dir.GetDirectories();
            }
        }
        public DirectoryInfo[] ChildDirs
        {
            get{return this.childDirs;}
        }
        public FileInfo[] ChildFiles
        {
            get { return this.childFiles; }
        }
        public String DirName
        {
            get { return this.dirName; }
        }
        public String getRandomName()
        {
            DateTime time = DateTime.Now;
            Random rnd = new Random();
            int rndDb = (int)(rnd.NextDouble() * 1000);
            return time.ToString("yyyyMMddHHmmss") + rndDb;
        }
        #endregion
        #region 功能函数
        /// <summary>
        /// 批量修改当前目录下文件的文件扩展名,排除其中的属性
        /// </summary>
        /// <param name="extName"></param>
        public void batchModifyFileExtName(String extName,FileAttributes attr)
        {
            if (extName == null)
                return;
            for (int i = 0; i < childFiles.Length; i++)
            {
                if ((childFiles[i].Attributes & attr) == 0)
                {
                    FileFunction file = new FileFunction(childFiles[i]);
                    file.ModifyExt(extName);
                }
            }
        }
        /// <summary>
        /// 迭代的修改文件扩展名,排除指定(文件以及目录)属性
        /// </summary>
        /// <param name="extName"></param>
        public void batchTreeModifyFileExtName(String extName,FileAttributes fileAttr,FileAttributes dirAttr)
        {
            if (extName == null)
                return;
            batchModifyFileExtName(extName,fileAttr);
            for (int i = 0; i < childDirs.Length; i++)
            {
                if ((childDirs[i].Attributes & dirAttr) == 0)
                {
                    DirectoryFunction tempDirFunc = new DirectoryFunction(childDirs[i]);
                    tempDirFunc.batchTreeModifyFileExtName(extName,fileAttr,dirAttr);
                }
            }
        }
        /// <summary>
        /// 批量修改当前目录下文件的文件名，排除指定的属性
        /// </summary>
        /// <param name="realName"></param>
        /// <param name="attr"></param>
        public void batchModifyFileRealName(String realNameOrReplace, String contactStr, int startNumber, NameStrategy strategy, FileAttributes attr)
        {
            if (realNameOrReplace == null)
                return;
            for (int i = 0; i < childFiles.Length; i++)
            {
                if ((childFiles[i].Attributes & attr) == 0)
                {
                    FileFunction file = new FileFunction(childFiles[i]);
                    if (strategy == NameStrategy.IncreaseAtHead)
                        file.ModifyRealName(startNumber + contactStr + realNameOrReplace);
                    else if (strategy == NameStrategy.IncreaseAtMiddle)
                        file.ModifyRealName(realNameOrReplace + startNumber + contactStr);
                    else if (strategy == NameStrategy.IncreaseAtTail)
                        file.ModifyRealName(realNameOrReplace + contactStr + startNumber);
                    else if (strategy == NameStrategy.CutStrAtCharForIncHead)
                    {
                        int index = file.FileRealName.IndexOf(realNameOrReplace);
                        if (index == -1)
                        {
                            file.ModifyRealName(startNumber + file.FileRealName);
                        }
                        else
                        {
                            file.ModifyRealName(startNumber+file.FileRealName.Substring(index));
                        }
                    }
                    else if (strategy == NameStrategy.CutStrAtCharForIncTail)
                    {
                        int index = file.FileRealName.IndexOf(realNameOrReplace);
                        if (index == -1)
                        {
                            file.ModifyRealName(file.FileRealName+startNumber);
                        }
                        else
                        {
                            index += realNameOrReplace.Length;
                            file.ModifyRealName(file.FileRealName.Substring(0,index)+startNumber);
                        }
                    }
                    startNumber++;
                }
            }
        }
        /// <summary>
        /// 迭代的批量修改当前目录下文件的文件名，排除指定的属性
        /// </summary>
        /// <param name="realNameOrReplace"></param>
        /// <param name="contactStr"></param>
        /// <param name="startNumber"></param>
        /// <param name="strategy"></param>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeModifyFileRealName(String realNameOrReplace, String contactStr, int startNumber, NameStrategy strategy, FileAttributes fileAttr, FileAttributes dirAttr)
        {
            if (realNameOrReplace == null)
                return;
            batchModifyFileRealName(realNameOrReplace,contactStr,startNumber,strategy,fileAttr);
            for (int i = 0; i < childDirs.Length; i++)
            {
                if ((childDirs[i].Attributes & dirAttr) == 0)
                {
                    DirectoryFunction tempDirFunc = new DirectoryFunction(childDirs[i]);
                    tempDirFunc.batchTreeModifyFileRealName(realNameOrReplace,contactStr,startNumber,strategy,fileAttr,dirAttr);
                }
            }
        }
        /// <summary>
        /// 批量修改文件的全名，排除指定属性
        /// </summary>
        /// <param name="nameOrReplace"></param>
        /// <param name="contactStr"></param>
        /// <param name="startNumber"></param>
        /// <param name="strategy"></param>
        /// <param name="attr"></param>
        public void batchModifyFileName(String nameOrReplace, String contactStr, int startNumber, NameStrategy strategy, FileAttributes attr)
        {
            if (nameOrReplace == null)
                return;
            for (int i = 0; i < childFiles.Length; i++)
            {
                if ((childFiles[i].Attributes & attr) == 0)
                {
                    FileFunction file = new FileFunction(childFiles[i]);
                    if (strategy == NameStrategy.IncreaseAtHead)
                        file.ModifyName(startNumber + contactStr + nameOrReplace);
                    else if (strategy == NameStrategy.IncreaseAtMiddle)
                        file.ModifyName(nameOrReplace + startNumber + contactStr);
                    else if (strategy == NameStrategy.IncreaseAtTail)
                        file.ModifyName(nameOrReplace + contactStr + startNumber);
                    else if (strategy == NameStrategy.CutStrAtCharForIncHead)
                    {
                        int index = file.FileName.IndexOf(nameOrReplace);
                        if (index == -1)
                        {
                            file.ModifyName(startNumber + file.FileName);
                        }
                        else
                        {
                            file.ModifyName(startNumber + file.FileName.Substring(index));
                        }
                    }
                    else if (strategy == NameStrategy.CutStrAtCharForIncTail)
                    {
                        int index = file.FileName.IndexOf(nameOrReplace);
                        if (index == -1)
                        {
                            file.ModifyName(file.FileName+startNumber);
                        }
                        else
                        {
                            index += nameOrReplace.Length;
                            file.ModifyName(file.FileName.Substring(0, index)+startNumber);
                        }
                    }
                    startNumber++;
                }
            }
        }
        /// <summary>
        /// 迭代的批量修改文件全名，排除指定的属性
        /// </summary>
        /// <param name="nameOrReplace"></param>
        /// <param name="contactStr"></param>
        /// <param name="startNumber"></param>
        /// <param name="strategy"></param>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeModifyFileName(String nameOrReplace, String contactStr, int startNumber, NameStrategy strategy, FileAttributes fileAttr, FileAttributes dirAttr)
        {
            if (nameOrReplace == null)
                return;
            batchModifyFileName(nameOrReplace, contactStr, startNumber, strategy, fileAttr);
            for (int i = 0; i < childDirs.Length; i++)
            {
                if ((childDirs[i].Attributes & dirAttr) == 0)
                {
                    DirectoryFunction tempDirFunc = new DirectoryFunction(childDirs[i]);
                    tempDirFunc.batchTreeModifyFileName(nameOrReplace, contactStr, startNumber, strategy, fileAttr, dirAttr);
                }
            }
        }
        /// <summary>
        /// 批量移动文件，排除属性
        /// </summary>
        /// <param name="newFolder"></param>
        /// <param name="attr"></param>
        public void batchMoveTo(String newFolder,FileAttributes attr)
        {
            DirectoryInfo newDir = new DirectoryInfo(newFolder);
            if (newDir.Exists)
            {
                if (!newDir.FullName.ToLower().Equals(dir.FullName.ToLower()))
                {
                    for (int i = 0; i < childFiles.Length; i++)
                    {
                        if ((childFiles[i].Attributes & attr) == 0)
                        {
                            FileFunction file = new FileFunction(childFiles[i]);
                            file.MoveTo(newDir.FullName);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 迭代的批量移动文件，除去指定的属性
        /// </summary>
        /// <param name="newFolder"></param>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeMoveTo(String newFolder, FileAttributes fileAttr, FileAttributes dirAttr)
        {
            DirectoryInfo newDir = new DirectoryInfo(newFolder);
            if (newDir.Exists)
            {
                if (!newDir.FullName.ToLower().Equals(dir.FullName.ToLower()))
                {
                    batchMoveTo(newFolder,fileAttr);
                    for (int i = 0; i < childDirs.Length; i++)
                    {
                        if ((childDirs[i].Attributes & dirAttr) == 0)
                        {
                            DirectoryFunction tempFunc = new DirectoryFunction(childDirs[i]);
                            tempFunc.batchTreeMoveTo(newFolder,fileAttr,dirAttr);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="templateFolder"></param>
        public void copyFolder(String templateFolder)
        {
            DirectoryInfo templateDir = new DirectoryInfo(templateFolder);
            if (templateDir.Exists)
            {
                if (!templateDir.FullName.ToLower().Equals(dir.FullName.ToLower()))
                {
                    DirectoryInfo[] dirs = templateDir.GetDirectories();
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        String name = dirs[i].Name;
                        DirectoryInfo temp = new DirectoryInfo(dir.FullName+"/"+name);
                        if (temp.Exists)
                        {
                            System.Console.WriteLine("文件夹["+temp.FullName+"]已存在,未能创建新文件夹");
                            continue;
                        }
                        System.Console.WriteLine("复制目录:" + dirs[i].FullName + "......");
                        DirectoryInfo newFolder=dir.CreateSubdirectory(name);
                        DirectoryFunction dirFun = new DirectoryFunction(newFolder);
                        dirFun.copyFolder(dirs[i].FullName);
                    }
                }
            }
        }
        /// <summary>
        /// 提取指定属性的文件，排除的目录属性
        /// </summary>
        public void extractFileByAttribute(String newFolder,FileAttributes attr,FileAttributes dirAttr)
        {
            DirectoryInfo newDir = new DirectoryInfo(newFolder);

            if (newDir.Exists && !newDir.FullName.ToLower().Equals(dir.FullName.ToLower()))
            {
                FileInfo[] files = newDir.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    if ((files[i].Attributes & attr) != 0)
                    {
                        FileFunction fileFunc = new FileFunction(files[i]);
                        fileFunc.MoveTo(dir.FullName);
                    }
                }

                DirectoryInfo[] dirs = newDir.GetDirectories();
                for (int i = 0; i < dirs.Length; i++)
                {
                    if ((dirs[i].Attributes & dirAttr) == 0)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(dir);
                        dirFunc.extractFileByAttribute(dirs[i].FullName,attr,dirAttr);
                    }
                }
            }
        }
        /// <summary>
        /// 提取指定类型的文件,排除指定属性
        /// </summary>
        /// <param name="newFolder"></param>
        /// <param name="type"></param>
        public void extractFileByType(String newFolder, String type,FileAttributes fileAttr,FileAttributes dirAttr)
        {
            DirectoryInfo newDir = new DirectoryInfo(newFolder);
            
            if (newDir.Exists && !newDir.FullName.ToLower().Equals(dir.FullName.ToLower()))
            {
                FileInfo[] files = newDir.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    if ((files[i].Attributes & fileAttr) == 0)
                    {
                        FileFunction fileFunc = new FileFunction(files[i]);
                        if (fileFunc.FileExtName.ToLower().Equals(type.ToLower()))
                        {
                            fileFunc.MoveTo(dir.FullName);
                        }
                    }
                }

                DirectoryInfo[] dirs = newDir.GetDirectories();
                for (int i = 0; i < dirs.Length; i++)
                {
                    if ((dirs[i].Attributes & dirAttr) == 0)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(dir);
                        dirFunc.extractFileByType(dirs[i].FullName, type, fileAttr, dirAttr);
                    }
                }
            }
        }
        /// <summary>
        /// 迭代的复制指定类型的文件
        /// </summary>
        /// <param name="newFolder"></param>
        /// <param name="type"></param>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeCopyFromByType(String newFolder,String type,FileAttributes fileAttr,FileAttributes dirAttr)
        {
            DirectoryInfo newDir = new DirectoryInfo(newFolder);

            if (newDir.Exists && !newDir.FullName.ToLower().Equals(dir.FullName.ToLower()))
            {
                FileInfo[] files = newDir.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    if ((files[i].Attributes & fileAttr) == 0)
                    {
                        
                        FileFunction fileFunc = new FileFunction(files[i]);
                        if (fileFunc.FileExtName.ToLower().Equals(type.ToLower()))
                        {
                            fileFunc.CopyTo(dir.FullName);
                        }
                    }
                }
                DirectoryInfo[] dirs = newDir.GetDirectories();
                for (int i = 0; i < dirs.Length; i++)
                {
                    if ((dirs[i].Attributes & dirAttr) == 0)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(dir);
                        dirFunc.batchTreeCopyFromByType(dirs[i].FullName,type,fileAttr,dirAttr);
                    }
                }
            }
        }
        /// <summary>
        /// 迭代的复制指定属性的文件
        /// </summary>
        /// <param name="newFolder"></param>
        /// <param name="type"></param>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeCopyFromByAttribute(String newFolder,FileAttributes fileAttr, FileAttributes dirAttr)
        {
            DirectoryInfo newDir = new DirectoryInfo(newFolder);

            if (newDir.Exists && !newDir.FullName.ToLower().Equals(dir.FullName.ToLower()))
            {
                FileInfo[] files = newDir.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    if ((files[i].Attributes & fileAttr) != 0)
                    {
                        FileFunction fileFunc = new FileFunction(files[i]);
                        fileFunc.CopyTo(dir.FullName);
                    }
                }
                DirectoryInfo[] dirs = newDir.GetDirectories();
                for (int i = 0; i < dirs.Length; i++)
                {
                    if ((dirs[i].Attributes & dirAttr) == 0)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(dir);
                        dirFunc.batchTreeCopyFromByAttribute(dirs[i].FullName,fileAttr, dirAttr);
                    }
                }
            }
        }
        /// <summary>
        /// 批量创建制定文件名的文件，指定属性、文件数量,扩展名绪指定点号
        /// </summary>
        /// <param name="realName"></param>
        /// <param name="contact"></param>
        /// <param name="startNumber"></param>
        /// <param name="strategy"></param>
        /// <param name="attr"></param>
        public void batchCreateFiles(String realName,String contact,int startNumber,String extName,String append,int number,NameStrategy strategy,FileAttributes attr)
        {
            if (strategy == NameStrategy.IncreaseAtTail)
            {
                for (int i = 0; i < number; i++)
                {
                    String fileName = realName + contact + (startNumber+i) + extName;
                    FileInfo file = new FileInfo(dir.FullName+"/"+fileName);
                    if (file.Exists)
                    {
                        System.Console.WriteLine("异常：文件[" + file.FullName + "]已经存在,未创建新文件");
                        continue;
                    }
                    FileStream stream=file.Create();
                    System.Console.WriteLine("正在创建["+fileName+"]文件");
                    stream.Close();
                    if (!append.Equals(""))
                    {
                        StreamWriter write=file.AppendText();
                        write.Write(append);
                        write.Dispose();
                        write.Close();
                    }
                    file.Attributes = attr;
                }
            }
            else if (strategy == NameStrategy.IncreaseAtMiddle)
            {
                for (int i = 0; i < number; i++)
                {
                    String fileName = realName + (startNumber+i)+contact + extName;
                    FileInfo file = new FileInfo(dir.FullName + "/" + fileName);
                    if (file.Exists)
                    {
                        System.Console.WriteLine("异常：文件[" + file.FullName + "]已经存在,未创建新文件");
                        continue;
                    }
                    FileStream stream = file.Create();
                    System.Console.WriteLine("正在创建[" + fileName + "]文件");
                    stream.Close();
                    if (!append.Equals(""))
                    {
                        StreamWriter write = file.AppendText();
                        write.Write(append);
                        write.Dispose();
                        write.Close();
                    }
                    file.Attributes = attr;
                }
            }
            else if (strategy == NameStrategy.IncreaseAtHead)
            {
                for (int i = 0; i < number; i++)
                {
                    String fileName = (startNumber + i)+contact+realName + extName;
                    FileInfo file = new FileInfo(dir.FullName + "/" + fileName);
                    if (file.Exists)
                    {
                        System.Console.WriteLine("异常：文件[" + file.FullName + "]已经存在,未创建新文件");
                        continue;
                    }
                    FileStream stream = file.Create();
                    System.Console.WriteLine("正在创建[" + fileName + "]文件");
                    stream.Close();
                    if (!append.Equals(""))
                    {
                        StreamWriter write = file.AppendText();
                        write.Write(append);
                        write.Dispose();
                        write.Close();
                    }
                    file.Attributes = attr;
                }
            }
            else
            {
                System.Console.WriteLine("非法策略....");
            }
        }
        /// <summary>
        /// 批量创建指定目录名的目录
        /// </summary>
        /// <param name="realName"></param>
        /// <param name="contact"></param>
        /// <param name="startNumber"></param>
        /// <param name="strategy"></param>
        /// <param name="attr"></param>
        public void batchCreateDirectories(String realName, String contact, int startNumber,int number, NameStrategy strategy, FileAttributes attr)
        {
            if (strategy == NameStrategy.IncreaseAtTail)
            {
                for (int i = 0; i < number; i++)
                {
                    String dirName = realName + contact + (startNumber + i);
                    DirectoryInfo dire = new DirectoryInfo(dir.FullName + "/" + dirName);
                    if (dire.Exists)
                    {
                        System.Console.WriteLine("异常：文件夹["+dire.FullName+"]已经存在,未创建新文件夹");
                        continue;
                    }
                    dire.Create();
                    System.Console.WriteLine("正在创建[" + dirName + "]文件夹");
                    dire.Attributes = attr;
                }
            }
            else if (strategy == NameStrategy.IncreaseAtMiddle)
            {
                for (int i = 0; i < number; i++)
                {
                    String dirName = realName + (startNumber + i) + contact;
                    DirectoryInfo dire = new DirectoryInfo(dir.FullName + "/" + dirName);
                    if (dire.Exists)
                    {
                        System.Console.WriteLine("异常：文件夹[" + dire.FullName + "]已经存在,未创建新文件夹");
                        continue;
                    }
                    dire.Create();
                    System.Console.WriteLine("正在创建[" + dirName + "]文件夹");
                    dire.Attributes = attr;
                }
            }
            else if (strategy == NameStrategy.IncreaseAtHead)
            {
                for (int i = 0; i < number; i++)
                {
                    String dirName = (startNumber + i) + contact + realName;
                    DirectoryInfo dire = new DirectoryInfo(dir.FullName + "/" + dirName);
                    if (dire.Exists)
                    {
                        System.Console.WriteLine("异常：文件夹[" + dire.FullName + "]已经存在,未创建新文件夹");
                        continue;
                    }
                    dire.Create();
                    System.Console.WriteLine("正在创建[" + dirName + "]文件夹");
                    dire.Attributes = attr;
                }
            }
            else
            {
                System.Console.WriteLine("非法策略....");
            }
        }
        #endregion
        #region 扩展部分
        /// <summary>
        /// 将当前目录下的文件复制到指定目录中
        /// </summary>
        /// <param name="newFolder"></param>
        /// <param name="attr"></param>
        public void batchCopyTo(String newFolder, FileAttributes attr)
        {
            DirectoryInfo newDir = new DirectoryInfo(newFolder);
            if (newDir.Exists)
            {
                if (!newDir.FullName.ToLower().Equals(dir.FullName.ToLower()))
                {
                    for (int i = 0; i < childFiles.Length; i++)
                    {
                        if ((childFiles[i].Attributes & attr) == 0)
                        {
                            FileFunction file = new FileFunction(childFiles[i]);
                            file.CopyTo(newDir.FullName);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 迭代得将当前目录下的文件复制到指定目录中
        /// </summary>
        /// <param name="newFolder"></param>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeCopyTo(String newFolder, FileAttributes fileAttr, FileAttributes dirAttr)
        {
            DirectoryInfo newDir = new DirectoryInfo(newFolder);
            if (newDir.Exists)
            {
                if (!newDir.FullName.ToLower().Equals(dir.FullName.ToLower()))
                {
                    batchCopyTo(newFolder, fileAttr);
                    for (int i = 0; i < childDirs.Length; i++)
                    {
                        if ((childDirs[i].Attributes & dirAttr) == 0)
                        {
                            DirectoryFunction tempFunc = new DirectoryFunction(childDirs[i]);
                            tempFunc.batchTreeCopyTo(newFolder, fileAttr, dirAttr);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 把当前目录下的文件复制到每一个子目录中，排除文件，目录属性
        /// </summary>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchAddFileForDirectory(FileAttributes fileAttr, FileAttributes dirAttr)
        {
            for (int i = 0; i < childDirs.Length; i++)
            {
                if ((childDirs[i].Attributes & dirAttr) == 0)
                {
                    batchCopyTo(childDirs[i].FullName,fileAttr);
                }
            }
        }
        /// <summary>
        /// 迭代得把当前目录下的文件复制到每一个子目录中,排除文件，目录属性
        /// </summary>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeAddFileForDirectory(FileAttributes fileAttr, FileAttributes dirAttr)
        {
            batchTreeAddFileForDirectory(dir,fileAttr,dirAttr);
        }
        /// <summary>
        /// 迭代得把源目录下的文件复制到每一个子目录中,排除文件，目录属性
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        private void batchTreeAddFileForDirectory(DirectoryInfo sourceDir, FileAttributes fileAttr, FileAttributes dirAttr)
        {
            for (int i = 0; i < childDirs.Length; i++)
            {
                if ((childDirs[i].Attributes & dirAttr) == 0)
                {
                    FileInfo[] subFiles = sourceDir.GetFiles();
                    for (int j = 0; j < subFiles.Length; j++)
                    {
                        if ((subFiles[j].Attributes & fileAttr) == 0)
                        {
                            FileFunction subFile = new FileFunction(subFiles[j]);
                            subFile.CopyTo(childDirs[i].FullName);
                        }
                    }

                    DirectoryFunction dirFunc = new DirectoryFunction(childDirs[i]);
                    dirFunc.batchTreeAddFileForDirectory(sourceDir,fileAttr,dirAttr);
                }
            }
        }
        /// <summary>
        /// 辅助函数，修改目录的名称
        /// </summary>
        /// <param name="name"></param>
        private void ModifyDirectoryName(DirectoryInfo subDir,String name)
        {
            if (subDir.Exists)
            {
                if (!String.IsNullOrEmpty(name))
                {
                    DirectoryInfo temp = new DirectoryInfo(Path.Combine(new String[] { subDir.Parent.FullName, name }));
                    if (temp.Exists)
                    {
                        System.Console.WriteLine("异常：文件夹["+temp.FullName+"]已存在,"+subDir.FullName+"未修改");
                        return;
                    }
                    System.Console.WriteLine("正在修改文件夹[" + subDir.FullName + "]的名称为[" + name + "]......");
                    subDir.MoveTo(Path.Combine(new String[] { subDir.Parent.FullName, name }));
                }
            }
        }
        /// <summary>
        /// 实现修改当前目录下所有文件夹的名称，排除文件夹属性
        /// </summary>
        /// <param name="realNameOrReplace"></param>
        /// <param name="contactStr"></param>
        /// <param name="startNumber"></param>
        /// <param name="strategy"></param>
        /// <param name="dirAttr"></param>
        public void batchModifyDirectoryName(String nameOrReplace, String contactStr, int startNumber, NameStrategy strategy, FileAttributes dirAttr)
        {
            for (int i = 0; i < childDirs.Length; i++)
            {
                if ((childDirs[i].Attributes & dirAttr) == 0)
                {
                    if (strategy == NameStrategy.IncreaseAtTail)
                    {
                        ModifyDirectoryName(childDirs[i],nameOrReplace + contactStr + startNumber);
                    }
                    else if (strategy == NameStrategy.IncreaseAtMiddle)
                    {
                        ModifyDirectoryName(childDirs[i],nameOrReplace + startNumber + contactStr);
                    }
                    else if (strategy == NameStrategy.IncreaseAtHead)
                    {
                        ModifyDirectoryName(childDirs[i],startNumber + contactStr + nameOrReplace);
                    }
                    else if (strategy == NameStrategy.CutStrAtCharForIncHead)
                    {
                        int index=childDirs[i].Name.IndexOf(nameOrReplace);
                        if (index == -1)
                        {
                            ModifyDirectoryName(childDirs[i], startNumber + childDirs[i].Name);
                        }
                        else
                        {
                            ModifyDirectoryName(childDirs[i],startNumber+childDirs[i].Name.Substring(index));
                        }
                    }
                    else if (strategy == NameStrategy.CutStrAtCharForIncTail)
                    {
                        int index = childDirs[i].Name.IndexOf(nameOrReplace);
                        if (index == -1)
                        {
                            ModifyDirectoryName(childDirs[i], childDirs[i].Name + startNumber);
                        }
                        else
                        {
                            index += nameOrReplace.Length;
                            ModifyDirectoryName(childDirs[i], childDirs[i].Name.Substring(0, index) + startNumber);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("非法策略......");
                    }
                    startNumber++;
                }
            }
        }
        /// <summary>
        /// 实现迭代得修改当前目录下所有文件夹的名称，排除文件夹属性
        /// </summary>
        /// <param name="realNameOrReplace"></param>
        /// <param name="contactStr"></param>
        /// <param name="startNumber"></param>
        /// <param name="strategy"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeModifyDirectoryName(String nameOrReplace, String contactStr, int startNumber, NameStrategy strategy, FileAttributes dirAttr)
        {
            batchModifyDirectoryName(nameOrReplace,contactStr,startNumber,strategy,dirAttr);
            for (int i = 0; i < childDirs.Length; i++)
            {
                if ((childDirs[i].Attributes & dirAttr) == 0)
                {
                    DirectoryFunction dirFunc = new DirectoryFunction(childDirs[i]);
                    dirFunc.batchTreeModifyDirectoryName(nameOrReplace, contactStr, startNumber, strategy, dirAttr); 
                }
            }
        }
        /// <summary>
        /// 修改当前目录下文件的扩展名为大小写,排除文件属性
        /// </summary>
        /// <param name="fileAttr"></param>
        public void batchCaseExt(CaseName caseName,FileAttributes fileAttr) 
        {
            for (int i = 0; i < childFiles.Length; i++)
            {
                if ((childFiles[i].Attributes & fileAttr) == 0)
                {
                    FileFunction fileFunc = new FileFunction(childFiles[i]);
                    String extName = ((caseName == CaseName.ToLower)?fileFunc.FileExtName.ToLower():fileFunc.FileExtName.ToUpper());
                    System.Console.WriteLine("正在修改文件["+fileFunc.FilePath+"]的扩展名为["+extName+"]......");
                    childFiles[i].MoveTo(dirName+"/"+fileFunc.FileRealName+"."+extName);
                }
            }
        }
        /// <summary>
        /// 迭代得修改当前目录下文件的扩展名为大小写,排除文件，目录属性
        /// </summary>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeCaseExt(CaseName caseName,FileAttributes fileAttr, FileAttributes dirAttr) 
        {
            batchCaseExt(caseName,fileAttr);
            for (int i = 0; i < childDirs.Length; i++)
            {
                if ((childDirs[i].Attributes & dirAttr) == 0)
                {
                    DirectoryFunction dire = new DirectoryFunction(childDirs[i]);
                    dire.batchTreeCaseExt(caseName,fileAttr,dirAttr);
                }
            }
        }
        /// <summary>
        /// 修改当前目录下的文件的真实名为大小写，排除文件属性
        /// </summary>
        /// <param name="caseName"></param>
        /// <param name="fileAttr"></param>
        public void batchCaseReal(CaseName caseName, FileAttributes fileAttr)
        {
            for (int i = 0; i < childFiles.Length; i++)
            {
                if ((childFiles[i].Attributes & fileAttr) == 0)
                {
                    FileFunction fileFunc = new FileFunction(childFiles[i]);
                    String realName = ((caseName == CaseName.ToLower) ? fileFunc.FileRealName.ToLower() : fileFunc.FileRealName.ToUpper());
                    System.Console.WriteLine("正在修改文件[" + fileFunc.FilePath + "]的真实名为[" + realName + "]......");
                    childFiles[i].MoveTo(dirName + "/" + realName+"."+fileFunc.FileExtName);
                }
            }
        }
        /// <summary>
        /// 迭代的修改当前目录下的真实名为大小写，排除文件，目录属性
        /// </summary>
        /// <param name="caseName"></param>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeCaseReal(CaseName caseName, FileAttributes fileAttr,FileAttributes dirAttr)
        {
            batchCaseReal(caseName, fileAttr);
            for (int i = 0; i < childDirs.Length; i++)
            {
                if ((childDirs[i].Attributes & dirAttr) == 0)
                {
                    DirectoryFunction dire = new DirectoryFunction(childDirs[i]);
                    dire.batchTreeCaseReal(caseName, fileAttr, dirAttr);
                }
            }
        }
        /// <summary>
        /// 修改当前目录下的文件的全名为大小写，排除文件属性
        /// </summary>
        /// <param name="caseName"></param>
        /// <param name="fileAttr"></param>
        public void batchCaseName(CaseName caseName, FileAttributes fileAttr)
        {
            for (int i = 0; i < childFiles.Length; i++)
            {
                if ((childFiles[i].Attributes & fileAttr) == 0)
                {
                    FileFunction fileFunc = new FileFunction(childFiles[i]);
                    String name = ((caseName == CaseName.ToLower) ? fileFunc.FileName.ToLower() : fileFunc.FileName.ToUpper());
                    System.Console.WriteLine("正在修改文件[" + fileFunc.FilePath + "]的全名为[" + name + "]......");
                    childFiles[i].MoveTo(dirName + "/" + name);
                }
            }
        }
        /// <summary>
        /// 迭代的修改当前目录下的全名为大小写，排除文件，目录属性
        /// </summary>
        /// <param name="caseName"></param>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeCaseName(CaseName caseName, FileAttributes fileAttr, FileAttributes dirAttr)
        {
            batchCaseName(caseName, fileAttr);
            for (int i = 0; i < childDirs.Length; i++)
            {
                if ((childDirs[i].Attributes & dirAttr) == 0)
                {
                    DirectoryFunction dire = new DirectoryFunction(childDirs[i]);
                    dire.batchTreeCaseName(caseName, fileAttr, dirAttr);
                }
            }
        }
        /// <summary>
        /// 修改当前目录下的子目录名为大小写，排除目录属性
        /// </summary>
        /// <param name="caseName"></param>
        /// <param name="fileAttr"></param>
        public void batchCaseDirectoryName(CaseName caseName, FileAttributes dirAttr)
        {
            for (int i = 0; i < childDirs.Length; i++)
            {
                if ((childDirs[i].Attributes & dirAttr) == 0)
                {
                    String directoryName=((caseName==CaseName.ToLower)?childDirs[i].Name.ToLower():childDirs[i].Name.ToUpper());
                    System.Console.WriteLine("正在修改目录["+childDirs[i].FullName+"]的名称为["+directoryName+"]......");
                    childDirs[i].MoveTo(dirName + "/" + directoryName + getRandomName());
                    childDirs[i].MoveTo(dirName + "/" + directoryName);
                }
            }
        }
        /// <summary>
        /// 迭代的修改当前目录下的子目录名为大小写，排除目录属性
        /// </summary>
        /// <param name="caseName"></param>
        /// <param name="fileAttr"></param>
        /// <param name="dirAttr"></param>
        public void batchTreeCaseDirectoryName(CaseName caseName,FileAttributes dirAttr)
        {
            batchCaseDirectoryName(caseName,dirAttr);
            for (int i = 0; i < childDirs.Length; i++)
            {
                DirectoryFunction dire = new DirectoryFunction(childDirs[i]);
                dire.batchTreeCaseDirectoryName(caseName,dirAttr);
            }
        }
        #endregion
    }
}
