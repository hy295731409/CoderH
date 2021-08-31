using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.设计模式.适配器
{
    /// <summary>
    /// 实现了 MediaPlayer 接口的适配器类。
    /// </summary>
    public class MediaAdapter : IMediaPlayer
    {
        IAdvancedMediaPlayer advancedMediaPlayer;
        public MediaAdapter(string mediaType)
        {
            if (mediaType == "vlc")
                advancedMediaPlayer = new VlcPlayer();
            else if (mediaType == "mp4")
                advancedMediaPlayer = new Mp4Player();
        }
        public void Play(string mediaType, string mediaName)
        {
            if (mediaType == "vlc")
                advancedMediaPlayer.playVlc(mediaName);
            else if (mediaType == "mp4")
                advancedMediaPlayer.playMp4(mediaName);
        }
    }
}
