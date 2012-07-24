using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;

namespace WP.Basics.Controls
{
    public class TextBlockInlineConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            var inlines = new List<Inline>();
            if (value != null)
            {
                // parse text
                var textLines =
                    value.ToString().Split(
                        new string[] {"<br/>"}
                        , StringSplitOptions.RemoveEmptyEntries);

                // add inlines and linebreaks
                foreach (string line in textLines)
                {
                    inlines.Add(new Run() {Text = line});
                    if (textLines.ToList().IndexOf(line) < textLines.Length - 1)
                    {
                        inlines.Add(new LineBreak());
                    }
                }
            }
            return inlines;
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
