using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace WP.Basics.Controls
{
    public partial class DialerControl : UserControl
    {
        DispatcherTimer dt = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(200) };


        private int lastPosition = -1;
        private int CurrentPosition
        {
            get
            {
                int position = tbRealValue.Text.IndexOf("|", System.StringComparison.Ordinal);

                if (position <= -1 && lastPosition > -1)
                {
                    return lastPosition;
                }

                return position;
            }
        }

        public DialerControl()
        {
            InitializeComponent();
            dt.Tick += (s, e) =>
            {
                int cPos = tbRealValue.Text.IndexOf("|", System.StringComparison.Ordinal);
                if (cPos <= -1)
                {
                    RemoveDigitAtPosition(lastPosition);
                    AddDigitAtPosition(lastPosition, "|");
                }
                else
                {
                    lastPosition = cPos;
                    RemoveDigitAtPosition(cPos);
                    AddDigitAtPosition(lastPosition, " ");
                }
            };
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null && btn.Tag != null)
            {
                ProcessDigit(btn.Tag.ToString());
            }
        }

        private void ProcessDigit(string digitInStringFormat)
        {
            bool isTimerRunning = dt.IsEnabled;

            if (isTimerRunning)
            {
                dt.Stop();
            }

            if (digitInStringFormat == "-")
            {
                if (CurrentPosition == 0)
                {
                    dt.Stop();
                    RemoveDigitAtPosition(CurrentPosition);
                }
                else
                {
                    RemoveDigitAtPosition(CurrentPosition - 1);
                }
            }
            else
            {
                AddDigitAtPosition(CurrentPosition, digitInStringFormat);
            }

            if (isTimerRunning)
            {
                dt.Start();
            }
        }

        private void RemoveDigitAtPosition(int position)
        {
            bool isTimerRunning = dt.IsEnabled;

            if (isTimerRunning)
            {
                dt.Stop();
            }

            if (tbRealValue.Text.Length > 0)
            {
                if (position <= -1)
                {
                    tbRealValue.Text = tbRealValue.Text.Substring(0, tbRealValue.Text.Length - 1);
                }
                else if (tbRealValue.Text.Length > position)
                {
                    if (isTimerRunning && lastPosition != -1 && tbRealValue.Text[position] != '|' && tbRealValue.Text[position] != ' ')
                    {
                        lastPosition--;
                    }

                    tbRealValue.Text = tbRealValue.Text.Remove(position, 1);
                }
            }

            if (isTimerRunning)
            {
                dt.Start();
            }
        }

        private void AddDigitAtPosition(int position, string digitInStringFormat)
        {
            if (digitInStringFormat != "|" && digitInStringFormat != " ")
            {
                if (lastPosition != -1)
                {
                    lastPosition++;
                }
            }

            if (position == -1)
            {
                tbRealValue.Text += digitInStringFormat;
            }
            else if (position <= tbRealValue.Text.Length)
            {
                tbRealValue.Text = tbRealValue.Text.Insert(position, digitInStringFormat);
            }
        }

        private void Tap(object sender, ManipulationCompletedEventArgs e)
        {
            ProcessTap(e.ManipulationOrigin.X);
        }

        private void ProcessTap(double d)
        {
            if (tbRealValue.Text.StartsWith("+"))
            {
                d -= 21;
            }
            int position = (int)(d / 14) - 1;

            if (position < -1)
            {
                position = -1;
            }
            else
            {
                // change position
                if (CurrentPosition > -1)
                {
                    dt.Stop();
                    RemoveDigitAtPosition(CurrentPosition);
                }
                AddDigitAtPosition(position, "|");
                dt.Start();
            }
        }
    }
}
