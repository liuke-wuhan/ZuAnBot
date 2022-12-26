using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuAnBot_Wpf.Helper
{
    /// <summary>
    /// 可以记录传输进度的文件流
    /// </summary>
    public class ProgressFileStream : FileStream
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isRead">true为上传流，false为下载流</param>
        public ProgressFileStream(string path, bool isRead) : base(path, isRead ? FileMode.Open : FileMode.Create, isRead ? FileAccess.Read : FileAccess.Write)
        {
        }

        public long CurrentSize { get; private set; }


        /// <summary>
        /// 流的传输监控器
        /// </summary>
        public event EventHandler<FileEventArgs> Progress;

        public override void Write(byte[] array, int offset, int count)
        {
            base.Write(array, offset, count);

            CurrentSize += count;
            if (Progress != null)
                Progress(this, new FileEventArgs(CurrentSize, Length));
        }

        public override int Read(byte[] array, int offset, int count)
        {
            int res = base.Read(array, offset, count);

            CurrentSize += count;
            if (Progress != null)
                Progress(this, new FileEventArgs(CurrentSize, Length));//上传进度

            return res;
        }
    }

    public class FileEventArgs : EventArgs
    {
        public FileEventArgs(long currentSize, long totalSize)
        {
            CurrentSize = currentSize;
            TotalSize = totalSize;
        }

        public long CurrentSize { get; }
        public long TotalSize { get; }
    }
}
