using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoxKey
{
    class Const
    {
        public const string ScriptDicPath = @"Script";
        public const string ScriptFileExtension = "scp";
        public const int IntegerArraySize = 10;
        /// <summary>
        /// 記錄腳本時,滑鼠位移允許的誤差斜率量,超出誤差會被當做不同的位移
        /// </summary>
        public const double Deviation = 0.017455064928217585;
        // 0.017455064928217585 = 1度斜率
        
    }
}
