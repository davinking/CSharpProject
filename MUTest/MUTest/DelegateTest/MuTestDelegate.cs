﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUTest.DelegateTest
{
    /// <summary>
    /// 测试 Delegate的函数回调
    /// </summary>
    public class MuTestDelegate
    {
        /// <summary>
        /// 完成时的回调事件
        /// </summary>
        public event EventHandler<MuTestDelegate> Completed;

    }
}
