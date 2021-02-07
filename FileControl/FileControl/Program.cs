using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace FileControl
{
    class Program
    {
        static void Main(string[] args)
        {
            #region   固定部分，保留......
            if (args.Length < 1)
            {
                System.Console.WriteLine("格式不正确，参照/?");
                System.Environment.Exit(0);
            }
            if (args[0].ToLower().Equals("/ext"))
            {
                if (args.Length != 3)
                {
                    System.Console.WriteLine("正确格式：程序 /ext 扩展名 排除属性");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定时要修改指定目录下的文件的扩展名吗？ (y/n)");
                if(System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 3)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String extName = args[1];
                        FileAttributes attr=0;
                        if (!args[2].Equals("0"))
                        {
                            attr = getAttr(args[2]);
                        }
                        dirFunc.batchModifyFileExtName(extName,attr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/extA".ToLower()))
            {
                if (args.Length != 4)
                {
                    System.Console.WriteLine("正确格式：程序名 /extA 扩展名 排除文件属性 排除目录属性");
                    System.Environment.Exit(0);
                    
                }
                System.Console.WriteLine("确定时要迭代修改指定目录下的文件的扩展名吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 4)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String extName = args[1];
                        FileAttributes fileAttr = 0;
                        if (!args[2].Equals("0"))
                        {
                            fileAttr = getAttr(args[2]);
                        }
                        FileAttributes dirAttr = 0;
                        if (!args[3].Equals("0"))
                        {
                            dirAttr = getAttr(args[3]);
                        }
                        dirFunc.batchTreeModifyFileExtName(extName, fileAttr, dirAttr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/real".ToLower()))
            {
                if (args.Length != 6)
                {
                    System.Console.WriteLine("正确格式：程序名 /real 真实名或需替换的字串 连接字串 自增开始数 命名策略 排除属性");
                    System.Console.WriteLine("/0    结尾自增");
                    System.Console.WriteLine("/1    中部自增");
                    System.Console.WriteLine("/2    开始自增");
                    System.Console.WriteLine("/3    开始替换自增");
                    System.Console.WriteLine("/4    结尾替换自增");
                    System.Environment.Exit(0);
                    
                }
                System.Console.WriteLine("确定时要修改指定目录下的文件的真实名吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 6)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String realOrReplace = args[1];
                        String contact = args[2];
                        int startNumber = int.Parse(args[3]);
                        NameStrategy strategy = getStrategy(args[4]);
                        FileAttributes attr = getAttr(args[5]);

                        dirFunc.batchModifyFileRealName(realOrReplace,contact,startNumber,strategy,attr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/realA".ToLower()))
            {
                if (args.Length != 7)
                {
                    System.Console.WriteLine("正确格式：程序名 /realA 真实名或需替换的字串 连接字串 自增开始数 命名策略 排除文件属性 排除目录属性");
                    System.Console.WriteLine("/0    结尾自增");
                    System.Console.WriteLine("/1    中部自增");
                    System.Console.WriteLine("/2    开始自增");
                    System.Console.WriteLine("/3    开始替换自增");
                    System.Console.WriteLine("/4    结尾替换自增");
                    System.Environment.Exit(0);

                }
                System.Console.WriteLine("确定时要迭代修改指定目录下的文件的真实名吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 7)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String realOrReplace = args[1];
                        String contact = args[2];
                        int startNumber = int.Parse(args[3]);
                        NameStrategy strategy = getStrategy(args[4]);
                        FileAttributes fileAttr = getAttr(args[5]);
                        FileAttributes dirAttr = getAttr(args[6]);

                        dirFunc.batchTreeModifyFileRealName(realOrReplace, contact, startNumber, strategy, fileAttr,dirAttr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/name".ToLower()))
            {
                if (args.Length != 6)
                {
                    System.Console.WriteLine("正确格式：程序名 /name 全名或需替换的字串 连接字串 自增开始数 命名策略 排除属性");
                    System.Console.WriteLine("/0    结尾自增");
                    System.Console.WriteLine("/1    中部自增");
                    System.Console.WriteLine("/2    开始自增");
                    System.Console.WriteLine("/3    开始替换自增");
                    System.Console.WriteLine("/4    结尾替换自增");
                    System.Environment.Exit(0);
                    
                }
                System.Console.WriteLine("确定时要修改指定目录下的文件的全名吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 6)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String nameOrReplace = args[1];
                        String contact = args[2];
                        int startNumber = int.Parse(args[3]);
                        NameStrategy strategy = getStrategy(args[4]);
                        FileAttributes attr = getAttr(args[5]);

                        dirFunc.batchModifyFileName(nameOrReplace,contact,startNumber,strategy,attr);
                    }
               }
            }
            else if (args[0].ToLower().Equals("/nameA".ToLower()))
            {
                if (args.Length != 7)
                {
                    System.Console.WriteLine("正确格式：程序名 /nameA 全名或需替换的字串 连接字串 自增开始数 命名策略 排除文件属性 排除目录属性");
                    System.Console.WriteLine("/0    结尾自增");
                    System.Console.WriteLine("/1    中部自增");
                    System.Console.WriteLine("/2    开始自增");
                    System.Console.WriteLine("/3    开始替换自增");
                    System.Console.WriteLine("/4    结尾替换自增");
                    System.Environment.Exit(0);
                    
                }
                System.Console.WriteLine("确定时要迭代修改指定目录下的文件的全名吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 7)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String nameOrReplace = args[1];
                        String contact = args[2];
                        int startNumber = int.Parse(args[3]);
                        NameStrategy strategy = getStrategy(args[4]);
                        FileAttributes fileAttr = getAttr(args[5]);
                        FileAttributes dirAttr = getAttr(args[6]);

                        dirFunc.batchTreeModifyFileName(nameOrReplace,contact,startNumber,strategy,fileAttr,dirAttr);
                    }
               }
            }
            else if (args[0].ToLower().Equals("/move"))
            {
                if (args.Length != 3)
                {
                    System.Console.WriteLine("正确格式：程序名 /move 目的目录名  排除属性");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定时要移动当前目录下的文件吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 3)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String dirName = args[1];
                        FileAttributes attr = getAttr(args[2]);
                        dirFunc.batchMoveTo(dirName, attr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/moveA".ToLower()))
            {
                if (args.Length != 4)
                {
                    System.Console.WriteLine("正确格式：程序名 /moveA 目的目录名 排除文件属性 排除目录属性");
                    System.Environment.Exit(0);
                    
                }
                System.Console.WriteLine("确定时要迭代的移动文件吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 4)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String dirName = args[1];
                        FileAttributes fileAttr = getAttr(args[2]);
                        FileAttributes dirAttr = getAttr(args[3]);
                        dirFunc.batchTreeMoveTo(dirName, fileAttr,dirAttr);
                    }
                }
 
            }
            else if (args[0].ToLower().Equals("/copyDir".ToLower()))
            {
                if (args.Length != 2)
                {
                    System.Console.WriteLine("正确格式：程序名 /copy 指定要复制的文件夹");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定时赋值指定的目录结构到当前的目录吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 2)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String tempFolder = args[1];
                        dirFunc.copyFolder(tempFolder);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/copyFromT".ToLower()))
            {
                if (args.Length != 5)
                {
                    System.Console.WriteLine("正确格式：程序名 /copyFromT 指定要复制的文件夹 指定类型 排除文件属性 排除目录属性");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定时迭代的根据类型复制指定的目录到当前的目录吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 5)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String tempFolder = args[1];
                        String type = args[2];
                        FileAttributes fileAttr = getAttr(args[3]);
                        FileAttributes dirAttr = getAttr(args[4]);
                        dirFunc.batchTreeCopyFromByType(tempFolder,type,fileAttr,dirAttr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/copyFromA".ToLower()))
            {
                if (args.Length != 4)
                {
                    System.Console.WriteLine("正确格式：程序名 /copyFromA 指定要复制的文件夹 指定文件属性 排除目录属性");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定时迭代的根据属性复制指定的目录到当前的目录吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 4)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String tempFolder = args[1];
                        FileAttributes fileAttr = getAttr(args[2]);
                        FileAttributes dirAttr = getAttr(args[3]);
                        dirFunc.batchTreeCopyFromByAttribute(tempFolder,fileAttr,dirAttr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/extractT".ToLower()))
            {
                if (args.Length != 5)
                {
                    System.Console.WriteLine("正确格式：程序名 /extractT 指定要提取的文件夹 指定类型 排除文件属性 排除目录属性");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定时迭代的根据类型提取文件吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 5)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String tempFolder = args[1];
                        String type = args[2];
                        FileAttributes fileAttr = getAttr(args[3]);
                        FileAttributes dirAttr = getAttr(args[4]);
                        dirFunc.extractFileByType(tempFolder, type, fileAttr, dirAttr);
                    }
                }

            }
            else if (args[0].ToLower().Equals("/extractA".ToLower()))
            {
                if (args.Length != 4)
                {
                    System.Console.WriteLine("正确格式：程序名 /extractA 指定要提取的文件夹 指定文件属性 排除目录属性");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定时迭代的根据属性提取文件吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 4)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String tempFolder = args[1];
                        FileAttributes fileAttr = getAttr(args[2]);
                        FileAttributes dirAttr = getAttr(args[3]);
                        dirFunc.extractFileByAttribute(tempFolder, fileAttr, dirAttr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/createF".ToLower()))
            {
                if (args.Length != 9)
                {
                    System.Console.WriteLine("正确格式：程序名 /createF 文件真实名 连接名 开始数字 扩展名（带点号）追加字符串 创建数目 命名策略 指定文件属性");
                    System.Console.WriteLine("/0    结尾自增");
                    System.Console.WriteLine("/1    中部自增");
                    System.Console.WriteLine("/2    开始自增");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定要创建文件吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 9)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String realName = args[1];
                        String contact = args[2];
                        int startNumber = int.Parse(args[3]);
                        String extName = args[4];
                        String append = args[5];
                        int number = int.Parse(args[6]);
                        NameStrategy strategy = getStrategy(args[7]);
                        FileAttributes attr = getAttr(args[8]);
                        dirFunc.batchCreateFiles(realName,contact,startNumber,extName,append,number,strategy,attr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/createD".ToLower()))
            {
                if (args.Length != 7)
                {
                    System.Console.WriteLine("正确格式：程序名 /createD 目录真实名 连接名 开始数字 创建数目 命名策略 指定目录属性");
                    System.Console.WriteLine("/0    结尾自增");
                    System.Console.WriteLine("/1    中部自增");
                    System.Console.WriteLine("/2    开始自增");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定要创建目录吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 7)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String realName = args[1];
                        String contact = args[2];
                        int startNumber = int.Parse(args[3]);
                        int number = int.Parse(args[4]);
                        NameStrategy strategy = getStrategy(args[5]);
                        FileAttributes attr = getAttr(args[6]);
                        dirFunc.batchCreateDirectories(realName,contact,startNumber,number,strategy,attr);
                    }
                }
            }
            else if(args[0].ToLower().Equals("/copy".ToLower()))
            {
                if (args.Length != 3)
                {
                    System.Console.WriteLine("正确格式：程序名 /copy 目的目录名  排除属性");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定时要复制当前目录下的文件吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 3)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String dirName = args[1];
                        FileAttributes attr = getAttr(args[2]);
                        dirFunc.batchCopyTo(dirName, attr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/copyA".ToLower()))
            {
                if (args.Length != 4)
                {
                    System.Console.WriteLine("正确格式：程序名 /copyA 目的目录名 排除文件属性 排除目录属性");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定时要迭代的复制文件吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 4)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String dirName = args[1];
                        FileAttributes fileAttr = getAttr(args[2]);
                        FileAttributes dirAttr = getAttr(args[3]);
                        dirFunc.batchTreeCopyTo(dirName, fileAttr, dirAttr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/addF2D".ToLower()))
            {
                if (args.Length != 3)
                {
                    System.Console.WriteLine("正确格式：程序名 /addF2D 排除文件属性 排除目录属性");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定时要的添加当前目录文件到子目录吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 3)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        FileAttributes fileAttr = getAttr(args[1]);
                        FileAttributes dirAttr = getAttr(args[2]);
                        dirFunc.batchAddFileForDirectory(fileAttr,dirAttr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/addF2DA".ToLower()))
            {
                if (args.Length != 3)
                {
                    System.Console.WriteLine("正确格式：程序名 /addF2DA 排除文件属性 排除目录属性");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定时要的迭代的添加当前目录文件到子目录吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 3)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        FileAttributes fileAttr = getAttr(args[1]);
                        FileAttributes dirAttr = getAttr(args[2]);
                        dirFunc.batchTreeAddFileForDirectory(fileAttr, dirAttr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/Dname".ToLower()))
            {
                if (args.Length != 6)
                {
                    System.Console.WriteLine("正确格式：程序名 /Dname 目录名或需替换的字串 连接字串 自增开始数 命名策略 排除属性");
                    System.Console.WriteLine("/0    结尾自增");
                    System.Console.WriteLine("/1    中部自增");
                    System.Console.WriteLine("/2    开始自增");
                    System.Console.WriteLine("/3    开始替换自增");
                    System.Console.WriteLine("/4    结尾替换自增");
                    System.Environment.Exit(0);

                }
                System.Console.WriteLine("确定时要修改指定目录下的目录的名字吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 6)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String nameOrReplace = args[1];
                        String contact = args[2];
                        int startNumber = int.Parse(args[3]);
                        NameStrategy strategy = getStrategy(args[4]);
                        FileAttributes attr = getAttr(args[5]);

                        dirFunc.batchModifyDirectoryName(nameOrReplace,contact,startNumber,strategy,attr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/DnameA".ToLower()))
            {
                if (args.Length != 6)
                {
                    System.Console.WriteLine("正确格式：程序名 /DnameA 目录名或需替换的字串 连接字串 自增开始数 命名策略 排除属性");
                    System.Console.WriteLine("/0    结尾自增");
                    System.Console.WriteLine("/1    中部自增");
                    System.Console.WriteLine("/2    开始自增");
                    System.Console.WriteLine("/3    开始替换自增");
                    System.Console.WriteLine("/4    结尾替换自增");
                    System.Environment.Exit(0);

                }
                System.Console.WriteLine("确定时要迭代得修改指定目录下的目录的名字吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 6)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        String nameOrReplace = args[1];
                        String contact = args[2];
                        int startNumber = int.Parse(args[3]);
                        NameStrategy strategy = getStrategy(args[4]);
                        FileAttributes attr = getAttr(args[5]);

                        dirFunc.batchTreeModifyDirectoryName(nameOrReplace, contact, startNumber, strategy, attr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/caseExt".ToLower()))
            {
                if (args.Length != 3)
                {
                    System.Console.WriteLine("正确格式：程序 /caseExt 大小写指令 排除属性");
                    System.Console.WriteLine("/l 转换为小写");
                    System.Console.WriteLine("/u 转换为大写");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定要转换指定目录下的文件的扩展名为指定大小写吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 3)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        CaseName caseName = getCaseName(args[1]);
                        FileAttributes attr = 0;
                        if (!args[2].Equals("0"))
                        {
                            attr = getAttr(args[2]);
                        }
                        dirFunc.batchCaseExt(caseName,attr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/caseExtA".ToLower()))
            {
                if (args.Length != 4)
                {
                    System.Console.WriteLine("正确格式：程序 /caseExtA 大小写指令 排除文件属性　排除目录属性");
                    System.Console.WriteLine("/l 转换为小写");
                    System.Console.WriteLine("/u 转换为大写");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定要迭代得转换指定目录下的文件的扩展名为指定大小写吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 4)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        CaseName caseName = getCaseName(args[1]);
                        FileAttributes fileAttr = 0;
                        if (!args[2].Equals("0"))
                        {
                            fileAttr = getAttr(args[2]);
                        }
                        FileAttributes dirAttr = 0;
                        if (!args[3].Equals("0"))
                        {
                            dirAttr = getAttr(args[3]);
                        }
                        dirFunc.batchTreeCaseExt(caseName, fileAttr,dirAttr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/caseReal".ToLower()))
            {
                if (args.Length != 3)
                {
                    System.Console.WriteLine("正确格式：程序 /caseReal 大小写指令 排除属性");
                    System.Console.WriteLine("/l 转换为小写");
                    System.Console.WriteLine("/u 转换为大写");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定要转换指定目录下的文件的真实名为指定大小写吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 3)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        CaseName caseName = getCaseName(args[1]);
                        FileAttributes attr = 0;
                        if (!args[2].Equals("0"))
                        {
                            attr = getAttr(args[2]);
                        }
                        dirFunc.batchCaseReal(caseName, attr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/caseRealA".ToLower()))
            {
                if (args.Length != 4)
                {
                    System.Console.WriteLine("正确格式：程序 /caseRealA 大小写指令 排除文件属性　排除目录属性");
                    System.Console.WriteLine("/l 转换为小写");
                    System.Console.WriteLine("/u 转换为大写");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定要迭代得转换指定目录下的文件的真实名为指定大小写吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 4)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        CaseName caseName = getCaseName(args[1]);
                        FileAttributes fileAttr = 0;
                        if (!args[2].Equals("0"))
                        {
                            fileAttr = getAttr(args[2]);
                        }
                        FileAttributes dirAttr = 0;
                        if (!args[3].Equals("0"))
                        {
                            dirAttr = getAttr(args[3]);
                        }
                        dirFunc.batchTreeCaseReal(caseName, fileAttr, dirAttr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/caseName".ToLower()))
            {
                if (args.Length != 3)
                {
                    System.Console.WriteLine("正确格式：程序 /caseName 大小写指令 排除属性");
                    System.Console.WriteLine("/l 转换为小写");
                    System.Console.WriteLine("/u 转换为大写");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定要转换指定目录下的文件的全名为指定大小写吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 3)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        CaseName caseName = getCaseName(args[1]);
                        FileAttributes attr = 0;
                        if (!args[2].Equals("0"))
                        {
                            attr = getAttr(args[2]);
                        }
                        dirFunc.batchCaseName(caseName, attr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/caseNameA".ToLower()))
            {
                if (args.Length != 4)
                {
                    System.Console.WriteLine("正确格式：程序 /caseNameA 大小写指令 排除文件属性　排除目录属性");
                    System.Console.WriteLine("/l 转换为小写");
                    System.Console.WriteLine("/u 转换为大写");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定要迭代得转换指定目录下的文件的全名为指定大小写吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 4)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        CaseName caseName = getCaseName(args[1]);
                        FileAttributes fileAttr = 0;
                        if (!args[2].Equals("0"))
                        {
                            fileAttr = getAttr(args[2]);
                        }
                        FileAttributes dirAttr = 0;
                        if (!args[3].Equals("0"))
                        {
                            dirAttr = getAttr(args[3]);
                        }
                        dirFunc.batchTreeCaseName(caseName, fileAttr, dirAttr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/caseDName".ToLower()))
            {
                if (args.Length != 3)
                {
                    System.Console.WriteLine("正确格式：程序 /caseDName 大小写指令 排除目录属性");
                    System.Console.WriteLine("/l 转换为小写");
                    System.Console.WriteLine("/u 转换为大写");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定要转换指定目录下子目录名为指定大小写吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 3)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        CaseName caseName = getCaseName(args[1]);
                        FileAttributes attr = 0;
                        if (!args[2].Equals("0"))
                        {
                            attr = getAttr(args[2]);
                        }
                        dirFunc.batchCaseDirectoryName(caseName, attr);
                    }
                }
            }
            else if (args[0].ToLower().Equals("/caseDNameA".ToLower()))
            {
                if (args.Length != 3)
                {
                    System.Console.WriteLine("正确格式：程序 /caseDName 大小写指令 排除目录属性");
                    System.Console.WriteLine("/l 转换为小写");
                    System.Console.WriteLine("/u 转换为大写");
                    System.Environment.Exit(0);
                }
                System.Console.WriteLine("确定要迭代得转换指定目录下的子目录名为指定大小写吗？ (y/n)");
                if (System.Console.ReadLine().ToLower().Equals("y"))
                {
                    if (args.Length == 3)
                    {
                        DirectoryFunction dirFunc = new DirectoryFunction(new DirectoryInfo("."));
                        CaseName caseName = getCaseName(args[1]);
                        FileAttributes attr = 0;
                        if (!args[2].Equals("0"))
                        {
                            attr = getAttr(args[2]);
                        }
                        dirFunc.batchTreeCaseDirectoryName(caseName, attr);
                    }
                }
            }
            #endregion
            else if (args[0].Equals("/?"))
            {
                showCmd();
            }
            else
            {
                System.Console.WriteLine("未知指令，指令输入不正确，请重新输入，注意大小写.......");
                System.Environment.Exit(0);
            }

        }
        #region   辅助静态函数
        /// <summary>
        /// 解析属性
        /// </summary>
        /// <param name="paraAttr"></param>
        /// <returns></returns>
        private static FileAttributes getAttr(String paraAttr)
        {
            char[] attrArray = paraAttr.ToLower().ToCharArray();
            FileAttributes attr=0;
            for (int i = 1; i < attrArray.Length; i++)
            {
                if (attrArray[i] == 'a')
                    attr = attr | FileAttributes.Archive;
                else if (attrArray[i] == 's')
                    attr = attr | FileAttributes.System;
                else if (attrArray[i] == 'r')
                    attr = attr | FileAttributes.ReadOnly;
                else if (attrArray[i] == 'h')
                    attr = attr | FileAttributes.Hidden;
            }

            return attr;
        }
        /// <summary>
        /// 解析命名策略
        /// </summary>
        /// <param name="paraStrategy"></param>
        /// <returns></returns>
        private static NameStrategy getStrategy(String paraStrategy)
        {
            char[] strategyChar = paraStrategy.ToLower().ToCharArray();
            NameStrategy strategy = 0;
            if (strategyChar[1] == '0')
                strategy = NameStrategy.IncreaseAtTail;
            else if (strategyChar[1] == '1')
                strategy = NameStrategy.IncreaseAtMiddle;
            else if (strategyChar[1] == '2')
                strategy = NameStrategy.IncreaseAtHead;
            else if (strategyChar[1] == '3')
                strategy = NameStrategy.CutStrAtCharForIncHead;
            else if (strategyChar[1] == '4')
                strategy = NameStrategy.CutStrAtCharForIncTail;

            return strategy;
        }
        /// <summary>
        /// 解析大小写指令
        /// </summary>
        /// <param name="paraCaseName"></param>
        /// <returns></returns>
        private static CaseName getCaseName(String paraCaseName)
        {
            if (paraCaseName.ToLower().Equals("/l"))
                return CaseName.ToLower;
            else
                return CaseName.ToUpper;
        }
        /// <summary>
        /// 显示帮助信息
        /// </summary>
        private static void showCmd()
        {
            ArrayList CmdInfoArray = new ArrayList();
            #region 添加项目部分
            CmdInfo cmd1 = new CmdInfo("/ext",CmdType.ForFile,"修改文件扩展名");
            CmdInfo cmd2 = new CmdInfo("/extA",CmdType.ForFile,"迭代得修改文件扩展名");
            CmdInfo cmd3 = new CmdInfo("/real",CmdType.ForFile,"修改文件真实名");
            CmdInfo cmd4 = new CmdInfo("/realA",CmdType.ForFile,"迭代得修改文件真实名");
            CmdInfo cmd5 = new CmdInfo("/name",CmdType.ForFile,"修改文件全名");
            CmdInfo cmd6 = new CmdInfo("/nameA",CmdType.ForFile,"迭代得修改文件全名");
            CmdInfo cmd7 = new CmdInfo("/Dname",CmdType.ForDir,"修改目录名");
            CmdInfo cmd8 = new CmdInfo("/DnameA",CmdType.ForDir,"迭代得修改目录名");
            CmdInfo cmd9 = new CmdInfo("/move",CmdType.ForFile,"移动文件");
            CmdInfo cmd10 = new CmdInfo("/moveA",CmdType.ForFile,"迭代得移动文件");
            CmdInfo cmd11 = new CmdInfo("/copy",CmdType.ForFile,"复制文件");
            CmdInfo cmd12 = new CmdInfo("/copyA",CmdType.ForFile,"迭代得复制文件");
            CmdInfo cmd13 = new CmdInfo("/copyDir",CmdType.ForDir,"复制目录结构");
            CmdInfo cmd14 = new CmdInfo("/copyFromT",CmdType.ForFile,"据类型复制文件");
            CmdInfo cmd15 = new CmdInfo("/copyFromA",CmdType.ForFile,"据属性复制文件");
            CmdInfo cmd16 = new CmdInfo("/extractT",CmdType.ForFile,"提取指定类型的文件");
            CmdInfo cmd17 = new CmdInfo("/extractA",CmdType.ForFile,"提取指定属性的文件");
            CmdInfo cmd18 = new CmdInfo("/createF",CmdType.ForFile,"创建指定数目的文件");
            CmdInfo cmd19 = new CmdInfo("/createD",CmdType.ForDir,"提取指定数目的目录");
            CmdInfo cmd20 = new CmdInfo("/addF2D",CmdType.ForAll,"为目录添加文件");
            CmdInfo cmd21 = new CmdInfo("/addF2DA",CmdType.ForAll,"迭代得为目录添加文件");
            CmdInfo cmd22 = new CmdInfo("/?",CmdType.ForAll,"显示帮助信息");
            CmdInfo cmd23 = new CmdInfo("/caseExt",CmdType.ForFile,"改变文件扩展名大小写");
            CmdInfo cmd24 = new CmdInfo("/caseExtA", CmdType.ForFile, "迭代得改变文件扩展名大小写");
            CmdInfo cmd25 = new CmdInfo("/caseReal", CmdType.ForFile, "改变文件真实名大小写");
            CmdInfo cmd26 = new CmdInfo("/caseRealA", CmdType.ForFile, "迭代得改变文件真实名大小写");
            CmdInfo cmd27 = new CmdInfo("/caseName", CmdType.ForFile, "改变文件全名大小写");
            CmdInfo cmd28 = new CmdInfo("/caseNameA", CmdType.ForFile, "迭代得改变文件全名大小写");
            CmdInfo cmd29 = new CmdInfo("/caseDName", CmdType.ForDir, "改变目录名大小写");
            CmdInfo cmd30 = new CmdInfo("/caseDNameA", CmdType.ForDir, "迭代得改变目录名大小写");
            CmdInfoArray.Add(cmd1); CmdInfoArray.Add(cmd2); CmdInfoArray.Add(cmd3); CmdInfoArray.Add(cmd4);
            CmdInfoArray.Add(cmd5); CmdInfoArray.Add(cmd6); CmdInfoArray.Add(cmd7); CmdInfoArray.Add(cmd8);
            CmdInfoArray.Add(cmd9); CmdInfoArray.Add(cmd10); CmdInfoArray.Add(cmd11); CmdInfoArray.Add(cmd12);
            CmdInfoArray.Add(cmd13); CmdInfoArray.Add(cmd14); CmdInfoArray.Add(cmd15); CmdInfoArray.Add(cmd16);
            CmdInfoArray.Add(cmd17); CmdInfoArray.Add(cmd18); CmdInfoArray.Add(cmd19); CmdInfoArray.Add(cmd20);
            CmdInfoArray.Add(cmd21); CmdInfoArray.Add(cmd23); CmdInfoArray.Add(cmd24); CmdInfoArray.Add(cmd25);
            CmdInfoArray.Add(cmd26); CmdInfoArray.Add(cmd27); CmdInfoArray.Add(cmd28); CmdInfoArray.Add(cmd29);
            CmdInfoArray.Add(cmd30); CmdInfoArray.Add(cmd22);
            #endregion
            #region 格式输出，一般不变
            System.Console.WriteLine("★★★: {0,-12}          {1,2}           {2,-15} ","指令名","类别","描述");
            System.Console.WriteLine("注：D--目录指令 F--文件指令 A--混杂");
            for (int i = 0; i < CmdInfoArray.Count; i++)
            {
                CmdInfo temp=(CmdInfo)(CmdInfoArray[i]);
                String caseInfo="";
                switch (temp.CmdTypeInfo)
                {
                    case CmdType.ForFile:
                        caseInfo = "F";
                        break;
                    case CmdType.ForDir:
                        caseInfo = "D";
                        break;
                    case CmdType.ForAll:
                        caseInfo = "A";
                        break;
                }
                System.Console.WriteLine("☆: {0,-12}          {1,2}           {2,-15} ",temp.CmdName,caseInfo,temp.Description);
                if (System.Console.CursorTop != 0 && System.Console.CursorTop%19==0)
                {
                    System.Console.Write("Enter Continue,Pause ........");
                    System.Console.ReadLine();
                    System.Console.WriteLine("★★★: {0,-12}          {1,2}           {2,-15} ", "指令名", "类别", "描述");
                    System.Console.WriteLine("注：D--目录指令 F--文件指令 A--混杂");
                }
            }
            System.Console.WriteLine("☆更详细指令信息，请输入具体指令并Enter查看.........");
            #endregion
        }
        #endregion
    }
}