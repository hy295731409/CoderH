using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.设计模式.适配器
{
    /// <summary>
    /// 实现了 MediaPlayer 接口的实体类的多功能播放器
    /// 一开始只要求能播放mp3
    /// </summary>
    public class AudioPlayer : IMediaPlayer
    {
        public void Play(string mediaType, string mediaName)
        {
            if(mediaType == "mp3")
                Console.WriteLine($"播放mp3,名称={mediaName}");
        }
    }

    /// <summary>
    /// 新需求让播放器可以播放mp4，vcl
    /// </summary>
    public class AudioPlayer2 : IMediaPlayer
    {
        MediaAdapter mediaAdapter;
        public void Play(string mediaType, string mediaName)
        {
            if (mediaType == "mp3")
            {
                Console.WriteLine($"播放mp3,名称={mediaName}");
            }
            else if(mediaType == "vlc" || mediaType == "mp4")
            {
                mediaAdapter = new MediaAdapter(mediaType);
                mediaAdapter.Play(mediaType, mediaName);
            }
            else
            {
                Console.WriteLine($"{mediaType} format not supported");
            }
        }
    }
}
