using System.Windows.Input;

namespace DashBoard.Commands
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand ToggleStrikeout = new RoutedUICommand
            (
                "ToggleStrikeout",
                "ToggleStrikeout",
                typeof(CustomCommands),
                new InputGestureCollection()
            );


        public static readonly RoutedUICommand FontLargen = new RoutedUICommand
       (
           "FontLargen",
           "FontLargen",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
                    new KeyGesture(Key.OemPlus, ModifierKeys.Control)
           }
       );


        public static readonly RoutedUICommand FontShrink = new RoutedUICommand
       (
           "FontShrink",
           "FontShrink",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
                    new KeyGesture(Key.OemMinus, ModifierKeys.Control)
           }
       );


        public static readonly RoutedUICommand AlignRight = new RoutedUICommand
       (
           "AlignRight",
           "AlignRight",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
           }
       );


        public static readonly RoutedUICommand AlignLeft = new RoutedUICommand
       (
           "AlignLeft",
           "AlignLeft",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
           }
       );



        public static readonly RoutedUICommand AlignCenter = new RoutedUICommand
       (
           "AlignCenter",
           "AlignCenter",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
           }
       );

        public static readonly RoutedUICommand AlignJustify = new RoutedUICommand
       (
           "AlignJustify",
           "AlignJustify",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
           }
       );



        public static readonly RoutedUICommand IndentRight = new RoutedUICommand
       (
           "IndentRight",
           "IndentRight",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
           }
       );

        public static readonly RoutedUICommand IndentLeft = new RoutedUICommand
       (
           "IndentLeft",
           "IndentLeft",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
           }
       );


        public static readonly RoutedUICommand ToggleBullet = new RoutedUICommand
       (
           "ToggleBullet",
           "ToggleBullet",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
           }
       );


        public static readonly RoutedUICommand ToggleNumber = new RoutedUICommand
       (
           "ToggleNumber",
           "ToggleNumber",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
           }
       );


        public static readonly RoutedUICommand FontColor = new RoutedUICommand
       (
           "FontColor",
           "FontColor",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
           }
       );
    }
}
