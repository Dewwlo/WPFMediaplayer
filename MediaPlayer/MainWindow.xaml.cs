using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IsPlaying(false);
        }

        private void IsPlaying(bool flag)
        {
            PlayMedia.IsEnabled = flag;
            StopMedia.IsEnabled = flag;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "Videos",
                DefaultExt = ".WMV",
                Filter = "All Videos Files |*.dat; *.wmv; *.3g2; *.3gp; *.3gp2; *.3gpp; *.amv; *.asf;  *.avi; *.bin; *.cue; *.divx; *.dv; *.flv; *.gxf; *.iso; *.m1v; *.m2v; *.m2t; *.m2ts; *.m4v; " +
                  " *.mkv; *.mov; *.mp2; *.mp2v; *.mp4; *.mp4v; *.mpa; *.mpe; *.mpeg; *.mpeg1; *.mpeg2; *.mpeg4; *.mpg; *.mpv2; *.mts; *.nsv; *.nuv; *.ogg; *.ogm; *.ogv; *.ogx; *.ps; *.rec; *.rm; *.rmvb; *.tod; *.ts; *.tts; *.vob; *.vro; *.webm"
            };

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                Media.Source = new Uri(dialog.FileName);
                Media.ScrubbingEnabled = true;
                Media.Pause();
                Media.Position = TimeSpan.FromTicks(1);
                PlayMedia.IsEnabled = true;
            }
        }

        private void PlayMedia_Click(object sender, RoutedEventArgs e)
        {
            Play();
        }

        private void StopMedia_Click(object sender, RoutedEventArgs e)
        {
            Media.Stop();
            PlayMedia.ToolTip = "Play";
            PlayPause.Source = new BitmapImage(new Uri(@"images/play-icon.png", UriKind.RelativeOrAbsolute));
            IsPlaying(false);
            PlayMedia.IsEnabled = true;
            Media.Close();
        }

        private void Media_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(Media.Source != null)
                Play();
        }

        private void Play()
        {
            IsPlaying(true);
            if (PlayMedia.ToolTip.ToString() == "Play")
            {
                Media.Play();
                PlayMedia.ToolTip = "Pause";
                PlayPause.Source = new BitmapImage(new Uri(@"images/pause-icon.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                Media.Pause();
                PlayMedia.ToolTip = "Play";
                PlayPause.Source = new BitmapImage(new Uri(@"images/play-icon.png", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
