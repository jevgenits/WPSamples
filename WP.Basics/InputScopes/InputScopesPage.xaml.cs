using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;

namespace WP.Basics.InputScopes
{
    public partial class InputScopesPage : PhoneApplicationPage
    {
        public InputScopesPage()
        {
            InitializeComponent();

            Loaded += (s, e) =>
                          {
                              spInputScopes.Children.Clear();

                              int index = 0;
                              foreach (int enumValue in EnumUtils.GetValues(typeof (InputScopeNameValue)))
                              {
                                  if (enumValue >= 0)
                                  {

                                      var tb = new TextBox()
                                                       {Text = Enum.GetName(typeof (InputScopeNameValue), enumValue)};

                                      var inputScope = new InputScope();
                                      inputScope.Names.Add(new InputScopeName()
                                                               {
                                                                   NameValue =
                                                                       (InputScopeNameValue)
                                                                       Enum.Parse(typeof (InputScopeNameValue),
                                                                                  enumValue.ToString(CultureInfo.InvariantCulture)
                                                                                  , true)
                                                               });
                                      tb.InputScope = inputScope;

                                      spInputScopes.Children.Add(tb);

                                      index++;
                                  }
                              }
                          };
        }



    }

    public static class EnumUtils
    {
        public static int[] GetValues(Type typeOfEnum)
        {
            var values = new int[0];

            if (typeOfEnum.BaseType == typeof(Enum))
            {
                // public static members of enumeration
                var fields = typeOfEnum.GetFields(BindingFlags.Public | BindingFlags.Static);

                // set a size of a result array to number of members
                values = new int[fields.Length];

                for (var index = 0; index < fields.Length; index++)
                {
                    values[index] = (int)fields[index].GetValue(null);
                }
            }

            return values;
        }
    }
}