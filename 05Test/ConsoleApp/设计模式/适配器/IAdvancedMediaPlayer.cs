using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.设计模式.适配器
{
    interface IAdvancedMediaPlayer
    {
        public void playVlc(string fileName);
        public void playMp4(string fileName);
    }
}
