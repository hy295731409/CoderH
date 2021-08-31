using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.设计模式.适配器
{
    /// <summary>
    /// 媒体播放器接口。
    /// </summary>
    public interface IMediaPlayer
    {
        void Play(string mediaType, string mediaName);
    }
}
