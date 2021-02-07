using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileControl
{
    public enum NameStrategy
    {
        IncreaseAtHead,
        IncreaseAtMiddle,
        IncreaseAtTail,
        CutStrAtCharForIncHead,
        CutStrAtCharForIncTail
    }

    public enum CaseName
    {
        ToLower,
        ToUpper
    }

    public enum CmdType
    {
        ForFile,
        ForDir,
        ForAll
    }

    public class CmdInfo
    {
        private String  cmdName;
        private CmdType cmdType;
        private String  description;

        public CmdInfo(String cmdName,CmdType cmdType,String description)
        {
            this.cmdName = cmdName;
            this.cmdType = cmdType;
            this.description = description;
        }

        public String CmdName
        {
            get { return cmdName; }
        }

        public String Description
        {
            get { return this.description; }
        }

        public CmdType CmdTypeInfo
        {
            get { return this.cmdType; }
        }
    }
}
