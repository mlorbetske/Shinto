using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Shinto
{
    /// <summary>
    /// 
    /// </summary>
    public interface IThreadFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="threadName"></param>
        /// <param name="isBackground"></param>
        /// <param name="threadStart"></param>
        /// <returns></returns>
        Thread CreateAndStart(string threadName, bool isBackground, Action threadStart);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="milliseconds"></param>
        void Sleep(Thread t, int milliseconds);
    }
}
