using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.设计模式.适配器
{
    class Mp4Player : IAdvancedMediaPlayer
    {
        public void playMp4(string fileName)
        {
            Console.WriteLine($"播放mp4,名称={fileName}");
        }

        public void playVlc(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
