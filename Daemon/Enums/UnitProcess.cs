using System;
using System.Collections.Generic;
using System.Text;

namespace Daemon.Enums {
    public class UnitProcess {
        public enum State:Int32{
            停止=1,
            正在启动=2,
            运行中=3,
            正在停止=4
        }
    }
}
