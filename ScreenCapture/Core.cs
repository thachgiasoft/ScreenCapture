using AForge.Video;
using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VideoCapture
{
    class Core
    {
        enum bitRate
        {
            _50kbit = 5000,
            _100kbit = 10000,
            _500kbit = 50000,
            _1000kbit = 1000000,
            _2000kbit = 2000000,
            _400kbit = 30000000
        }

        AForge.Video.ScreenCaptureStream streamVideo;
        VideoFileWriter writer;
        public void DoJob()
        {
            writer = new VideoFileWriter();
            string fullName = DateTime.Now.ToString("yyyy-MM-dd");
            writer.Open(
                       fullName + ".mp4",
                       Screen.PrimaryScreen.WorkingArea.Width,
                       Screen.PrimaryScreen.WorkingArea.Height,
                       (int)10,
                       VideoCodec.MPEG4, (int)(bitRate._400kbit));


            streamVideo = new ScreenCaptureStream(Screen.PrimaryScreen.WorkingArea);



            DateTime frameControl = DateTime.Now;
            streamVideo.NewFrame += new NewFrameEventHandler((sender,eventArgs) =>
            {
                

                    writer.WriteVideoFrame(eventArgs.Frame);
                    frameControl = DateTime.Now;
                
            
            });
            streamVideo.Start();
        }
        public void EndJob()
        {
            streamVideo.Stop();
            writer.Close();
        }
    }
}
