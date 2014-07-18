﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Mvvm;
using Xamarin.Forms.Labs.Services.SoundService;

namespace XF.Labs.Sample
{
    /// <summary>
    /// Sound Service ViewModel to support SoundPage sample page
    /// </summary>
    public class SoundServiceViewModel : ViewModel
    {
        ISoundService _soundService;

        public SoundServiceViewModel()
        {
            _soundService = DependencyService.Get<ISoundService>();
            if (_soundService == null)
                throw new ArgumentNullException("musicservice", new Exception("Didn't find any music service implementation for current platform"));

        }

        private Command _playCommand;
        public Command PlayCommand
        {
            get
            {
                return _playCommand ?? new Command(async () =>
                {
                    await SetandPlayMp3();
                });
            }
        }

        private double _duration = 0.1;
        public double Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                SetProperty(ref _duration, value);
                NotifyPropertyChanged("DurationText");
            }
        }

        public string DurationText
        {
            get
            {
                return TimeSpan.FromSeconds(_duration).ToString(@"mm\:ss");
            }
        }

        private async Task SetandPlayMp3()
        {
            var mediafile = await _soundService.PlayAsync("BusyEarnin.mp3");
            Duration = mediafile.Duration.TotalSeconds;
        }
    }
}