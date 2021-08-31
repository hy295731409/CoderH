using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.设计模式.适配器
{
    /// <summary>
    /// 更高级的媒体播放器接口。
    /// </summary>
    interface IAdvancedMediaPlayer
    {
        public void playVlc(string fileName);
        public void playMp4(string fileName);
    }
}
