using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.设计模式.适配器
{
    public interface IMediaPlayer
    {
        void Play(string mediaType, string mediaName);
    }
}
