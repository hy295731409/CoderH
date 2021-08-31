using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.设计模式.适配器
{
    /// <summary>
    /// vlc播放器
    /// </summary>
    class VlcPlayer : IAdvancedMediaPlayer
    {
        public void playMp4(string fileName)
        {
            throw new NotImplementedException();
        }

        public void playVlc(string fileName)
        {
            Console.WriteLine($"播放vcl,名称={fileName}");
        }
    }
}
