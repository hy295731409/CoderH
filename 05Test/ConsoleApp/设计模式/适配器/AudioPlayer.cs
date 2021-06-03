using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.设计模式.适配器
{
    /// <summary>
    /// 多功能播放器
    /// </summary>
    public class AudioPlayer : IMediaPlayer
    {
        public void Play(string mediaType, string mediaName)
        {
            //一开始只能播放mp3
            if(mediaType == "mp3")
            Console.WriteLine($"播放mp3,名称={mediaName}");
            
            //为了让播放器可以播放mp4，vcl
        }
    }
}
