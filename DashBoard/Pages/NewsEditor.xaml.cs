using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using DashBoard.Models.Regular;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Drawing.Text;
using System;
using DashBoard.ApiDecoder;
using GemBox.Document;
using HelthTourismServer.ApiDecoder;
using Color = System.Windows.Media.Color;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for TicketPage.xaml
    /// </summary>
    public partial class NewsEditor : Page
    {
        public MainWindow MainWindow { get; set; }
        public TblUserPass LoggedInUser { get; set; }
        public NewsViewer ViewerControl { get; set; }

        public NewsEditor()
        {
            InitializeComponent(); GemBox.Document.ComponentInfo.SetLicense("DGLB-XWHQ-86FS-3QGO");

        }

        /// <summary>
        /// NewsEditor For addition
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="loggedInUser"></param>
        public NewsEditor(MainWindow mainWindow, TblUserPass loggedInUser)
        {

            IsEditting = false;

            this.MainWindow = mainWindow;
            this.LoggedInUser = loggedInUser;
            this.ViewerControl = new NewsViewer();
            InitializeComponent(); GemBox.Document.ComponentInfo.SetLicense("DGLB-XWHQ-86FS-3QGO");

            SwitchToEdittingMode();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                + "\\" + (ViewerControl.DataModel.News.Title ?? "خبر جدید") + "News.Html";
            File.WriteAllText(path, ViewerControl.DataModel.News.MainDataRtf);


            #region s
            _fileName = path;
            string fileName = path;
            TextRange range;
            FileStream fStream;
            if (File.Exists(fileName))
            {
                range = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);
                fStream = new FileStream(fileName, FileMode.OpenOrCreate);
                range.Load(fStream, DataFormats.Text);
                fStream.Close();
            }

            #endregion
        }

        /// <summary>
        /// NewsEditor for Editting
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="loggedInUser"></param>
        /// <param name="viewer"></param>
        public NewsEditor(MainWindow mainWindow, TblUserPass loggedInUser, NewsViewer viewer)
        {
            IsEditting = true;

            this.MainWindow = mainWindow;
            this.LoggedInUser = loggedInUser;
            this.ViewerControl = viewer;
            InitializeComponent(); GemBox.Document.ComponentInfo.SetLicense("DGLB-XWHQ-86FS-3QGO");
            WrpOverview.Visibility = Visibility.Visible;
            WrpYesNo.Visibility = Visibility.Collapsed;


            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + ViewerControl.DataModel.News.Title + "News.Html";
            File.WriteAllText(path, ViewerControl.DataModel.News.MainDataRtf);
            TxtTitle.Text = ViewerControl.DataModel.News.Title ?? "خبر جدید";

           
            // Load input HTML file.
            DocumentModel document = DocumentModel.Load(path);

            // When reading any HTML content a single Section element is created,
            // which can be used to specify various Word document's page options.
            // The same can also be achieved with HTML document itself,
            // by using CSS properties on "@page" directive or "<body>" element.
            GemBox.Document.Section section = document.Sections[0];
            GemBox.Document.PageSetup pageSetup = section.PageSetup;
            GemBox.Document.PageMargins pageMargins = pageSetup.PageMargins;
            pageMargins.Top = pageMargins.Bottom = pageMargins.Left = pageMargins.Right = 0;

            // Save output DOCX file.
            document.Save(path += ".Docx");

            using (var stream = new MemoryStream())
            {
                // Convert input file to RTF stream.
                DocumentModel.Load(path).Save(stream, SaveOptions.RtfDefault);

                stream.Position = 0;

                // Load RTF stream into RichTextBox.
                var textRange = new TextRange(this.RichTextBox.Document.ContentStart, this.RichTextBox.Document.ContentEnd);
                textRange.Load(stream, DataFormats.Rtf);
            }

            //#region set richtextbox text
            //_fileName = path;
            //string fileName = path;
            //TextRange range;
            //FileStream fStream;
            //if (File.Exists(fileName))
            //{
            //    range = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);
            //    fStream = new FileStream(fileName, FileMode.OpenOrCreate);
            //    range.Load(fStream, DataFormats.Text);
            //    fStream.Close();
            //}

            //#endregion

        }

        private void BtnCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (new  BlurOutMessageBox().Show("اخطار", "فایل از بین خواهد رفت آیا مطمعن هستید؟", "بستن فایل", "بازگشت"))
            {
                case MessageBoxResult.None: return;
                case MessageBoxResult.Cancel: return;
                case MessageBoxResult.No: return;
                default: break;
            }

            MainWindow.FrameNewsEditor.Visibility = Visibility.Hidden;
            MainWindow.FrameNews.Visibility = Visibility.Visible;
        }

        public bool IsEditting { get; set; }

        private async void BtnAccept_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Save(null, null);

            RichTextBox.SelectAll();
            using (var stream = new MemoryStream())
            {
                // Save RichTextBox content to RTF stream.
                var textRange = new TextRange(this.RichTextBox.Document.ContentStart, this.RichTextBox.Document.ContentEnd);
                textRange.Save(stream, DataFormats.Rtf);
                if (string.IsNullOrEmpty(_fileName))
                {
                    SaveAs(null, null);
                    return;
                }
                ViewerControl.DataModel.News.MainDataRtf = File.ReadAllText(_fileName);
                ViewerControl.DataModel.News.MainData = textRange.Text;
                ViewerControl.DataModel.News.Title = TxtTitle.Text ?? "خبر جدید";
            }

            if (IsEditting)
            {

            }
            else if (!IsEditting)
            {
                MainWindow.TheNewsPage.StckMain.Children.Insert(0, ViewerControl);
            }

            BtnAccept.IsEnabled = false;
            MainWindow.FrameNewsEditor.Visibility = Visibility.Hidden;
            MainWindow.FrameNews.Visibility = Visibility.Visible;
        }


        private string _fileName;

        #region Commands
        private void Open(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                AddExtension = true,
                Filter =
                    "All Documents (*.docx;*.docm;*.doc;*.dotx;*.dotm;*.dot;*.htm;*.html;*.rtf;*.xml;*.txt)|*.docx;*.docm;*.dotx;*.dotm;*.doc;*.dot;*.htm;*.html;*.rtf;*.xml;*.txt|" +
                    "Word Documents (*.docx)|*.docx|" +
                    "Word Macro-Enabled Documents (*.docm)|*.docm|" +
                    "Word 97-2003 Documents (*.doc)|*.doc|" +
                    "Word Templates (*.dotx)|*.dotx|" +
                    "Word Macro-Enabled Templates (*.dotm)|*.dotm|" +
                    "Word 97-2003 Templates (*.dot)|*.dot|" +
                    "Web Pages (*.htm;*.html)|*.htm;*.html|" +
                    "Rich Text Format (*.rtf)|*.rtf|" +
                    "Flat OPC (*.xml)|*.xml|" +
                    "Plain Text (*.txt)|*.txt"

                //Filter = "Web Pages (*.htm;*.html)|*.htm;*.html"

            };

            if (dialog.ShowDialog() == true)
                using (var stream = new MemoryStream())
                {
                    // Convert input file to RTF stream.
                    GemBox.Document.DocumentModel.Load(dialog.FileName).Save(stream, GemBox.Document.SaveOptions.RtfDefault);

                    stream.Position = 0;

                    // Load RTF stream into RichTextBox.
                    var textRange = new TextRange(this.RichTextBox.Document.ContentStart, this.RichTextBox.Document.ContentEnd);
                    textRange.Load(stream, DataFormats.Rtf);

                    // Save file location
                    _fileName = dialog.FileName;
                }
        }

        private void Save(object sender, ExecutedRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                SaveAs(null, null);
                return;
            }

            if (RichTextBox.Document != null)
                using (var stream = new MemoryStream())
                {
                    // Save RichTextBox content to RTF stream.
                    var textRange = new TextRange(this.RichTextBox.Document.ContentStart, this.RichTextBox.Document.ContentEnd);
                    textRange.Save(stream, DataFormats.Rtf);

                    stream.Position = 0;


                    // Convert RTF stream to output format.
                    GemBox.Document.DocumentModel.Load(stream, GemBox.Document.LoadOptions.RtfDefault).Save(_fileName);
                    Process.Start(_fileName);
                }
        }

        private void SaveAs(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                AddExtension = true,
                Filter = "Web Pages (*.htm;*.html)|*.htm;*.html"

                //"Word Document (*.docx)|*.docx|" +
                //"Word Macro-Enabled Document (*.docm)|*.docm|" +
                //"Word Template (*.dotx)|*.dotx|" +
                //"Word Macro-Enabled Template (*.dotm)|*.dotm|" +
                //"PDF (*.pdf)|*.pdf|" +
                //"XPS Document (*.xps)|*.xps|" +
                //"Web Page (*.htm;*.html)|*.htm;*.html|" +
                //"Single File Web Page (*.mht;*.mhtml)|*.mht;*.mhtml|" +
                //"Rich Text Format (*.rtf)|*.rtf|" +
                //"Flat OPC (*.xml)|*.xml|" +
                //"Plain Text (*.txt)|*.txt|" +
                //"Image (*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff;*.wdp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff;*.wdp"
            };

            if (dialog.ShowDialog(MainWindow) == true)
                using (var stream = new MemoryStream())
                {
                    // Save RichTextBox content to RTF stream.
                    var textRange = new TextRange(this.RichTextBox.Document.ContentStart, this.RichTextBox.Document.ContentEnd);
                    textRange.Save(stream, DataFormats.Rtf);

                    stream.Position = 0;

                    // Convert RTF stream to output format.
                    GemBox.Document.DocumentModel.Load(stream, GemBox.Document.LoadOptions.RtfDefault).Save(dialog.FileName);
                    Process.Start(dialog.FileName);

                    _fileName = dialog.FileName;
                }
        }

        private void Cut(object sender, ExecutedRoutedEventArgs e)
        {
            this.Copy(sender, e);

            // Clear selection.
            this.RichTextBox.Selection.Text = string.Empty;
        }

        private void Copy(object sender, ExecutedRoutedEventArgs e)
        {
            using (var stream = new MemoryStream())
            {
                // Save RichTextBox selection to RTF stream.
                this.RichTextBox.Selection.Save(stream, DataFormats.Rtf);

                stream.Position = 0;

                // Save RTF stream to clipboard.
                GemBox.Document.DocumentModel.Load(stream, GemBox.Document.LoadOptions.RtfDefault).Content.SaveToClipboard();
            }
        }

        private void Paste(object sender, ExecutedRoutedEventArgs e)
        {
            using (var stream = new MemoryStream())
            {
                // Save RichTextBox content to RTF stream.
                var textRange = new TextRange(this.RichTextBox.Document.ContentStart, this.RichTextBox.Document.ContentEnd);
                textRange.Save(stream, DataFormats.Rtf);

                stream.Position = 0;

                // Load document from RTF stream and prepend or append clipboard content to it.
                var document = GemBox.Document.DocumentModel.Load(stream, GemBox.Document.LoadOptions.RtfDefault);
                var position = (string)e.Parameter == "prepend" ? document.Content.Start : document.Content.End;
                position.LoadFromClipboard();

                stream.Position = 0;

                // Save document to RTF stream.
                document.Save(stream, GemBox.Document.SaveOptions.RtfDefault);

                stream.Position = 0;

                // Load RTF stream into RichTextBox.
                textRange.Load(stream, DataFormats.Rtf);
            }
        }

        private void CanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.RichTextBox != null)
            {
                var document = this.RichTextBox.Document;
                var startPosition = document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward);
                var endPosition = document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward);
                e.CanExecute = startPosition != null && endPosition != null && startPosition.CompareTo(endPosition) < 0;
            }
            else
                e.CanExecute = false;
        }

        private void CanCut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.RichTextBox != null && !this.RichTextBox.Selection.IsEmpty;
        }

        private void CanCopy(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.RichTextBox != null && !this.RichTextBox.Selection.IsEmpty;
        }

        private void CanPaste(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.RichTextBox != null && this.RichTextBox.IsKeyboardFocused;
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        List<string> _undoList = new List<string>();
        int _undoListIndex = 0;
        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd).Text.Length == 0) return;
            _undoList.Add(new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd).Text);
            _undoListIndex++;

            TxtFontSize.Text = RichTextBox.Selection.GetPropertyValue(TextElement.FontSizeProperty).ToString();
        }

        private void Undo(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                //richTextBox.Document.Blocks.Clear();
                //richTextBox.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(UndoList[UndoListIndex -= 2])));
                //if (UndoListIndex != UndoList.Count) UndoList.RemoveRange(UndoListIndex, UndoList.Count);

                RichTextBox.Undo();
            }
            catch { }
        }

        private void CanUndo(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _undoList.Count >= 1;
        }

        private void Redo(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                //richTextBox.Document.Blocks.Clear();
                //richTextBox.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(UndoList[UndoListIndex += 1])));
                RichTextBox.Redo();
            }
            catch { }
        }

        private void CanRedo(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _undoListIndex + 1 < _undoList.Count;
        }

        private void RichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z) { e.Handled = true; return; }
        }

        private void EdcomboFonts_Loaded(object sender, RoutedEventArgs e)
        {
            var installedFontCollection = new InstalledFontCollection();

            // Get the array of FontFamily objects.
            var fontFamilies = installedFontCollection.Families;
            foreach (var fontFamily in fontFamilies)
            {
                var mfont = new FontFamily(fontFamily.Name);
                EdcomboFonts.Items.Add(new TextBlock() { Width = EdcomboFonts.Width - 10, Text = fontFamily.Name, Tag = mfont, FontFamily = new FontFamily(fontFamily.Name) });
            }

            this.MainWindow.HidePreloader();
        }

        private void CanToggleBold(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = !RichTextBox.Selection.IsEmpty;
            }
            catch { }
        }

        private void ToggleBold(object sender, ExecutedRoutedEventArgs e)
        {
            if (RichTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty) is FontWeight && ((FontWeight)RichTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty)) == FontWeights.Normal)
                RichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            else
                RichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
        }

        private void ToggleItalic(object sender, ExecutedRoutedEventArgs e)
        {
            // Toggle Italic
            if (RichTextBox.Selection.GetPropertyValue(TextElement.FontStyleProperty) is FontStyle && ((FontStyle)RichTextBox.Selection.GetPropertyValue(TextElement.FontStyleProperty)) == FontStyles.Normal)
                RichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            else
                RichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontWeights.Normal);
        }

        private void ToggleUnderline(object sender, ExecutedRoutedEventArgs e)
        {
            if ((RichTextBox.Selection.GetPropertyValue(TextBlock.TextDecorationsProperty) == TextDecorations.Underline))
                RichTextBox.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, null);
            else
                RichTextBox.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, TextDecorations.Underline);
        }

        private void ToggleStrikeout(object sender, ExecutedRoutedEventArgs e)
        {
            if ((RichTextBox.Selection.GetPropertyValue(TextBlock.TextDecorationsProperty) == TextDecorations.Strikethrough))
                RichTextBox.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, null);
            else
                RichTextBox.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, TextDecorations.Strikethrough);
        }

        private void FontLargen(object sender, ExecutedRoutedEventArgs e)
        {
            //FontSizeConverter converter = new FontSizeConverter();
            //double current = (double)converter.ConvertTo(richTextBox.Selection.GetPropertyValue(TextElement.FontSizeProperty), typeof(double));
            //richTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, current + 1);
            //txtFontSize.Text = richTextBox.Selection.GetPropertyValue(TextElement.FontSizeProperty).ToString();
            EditingCommands.IncreaseFontSize.Execute(1, RichTextBox);
        }

        private void FontShrink(object sender, ExecutedRoutedEventArgs e)
        {
            //FontSizeConverter converter = new FontSizeConverter();
            //double current = (double)converter.ConvertTo(richTextBox.Selection.GetPropertyValue(TextElement.FontSizeProperty), typeof(double));
            //richTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, current - 1);
            //txtFontSize.Text = richTextBox.Selection.GetPropertyValue(TextElement.FontSizeProperty).ToString();
            EditingCommands.DecreaseFontSize.Execute(1, RichTextBox);
        }


        public void AlignRight(object sender, ExecutedRoutedEventArgs e)
        {
            //BlockCollection blocks = richTextBox.Document.Blocks;
            //foreach (System.Windows.Documents.Block block in blocks)
            //{
            //    block.TextAlignment = TextAlignment.Right;
            //}
            EditingCommands.AlignRight.Execute(null, RichTextBox);
        }
        public void AlignLeft(object sender, ExecutedRoutedEventArgs e)
        {
            //    BlockCollection blocks = richTextBox.Document.Blocks;
            //    foreach (System.Windows.Documents.Block block in blocks)
            //    {
            //        block.TextAlignment = TextAlignment.Left;
            //    }
            EditingCommands.AlignLeft.Execute(null, RichTextBox);
        }

        public void AlignCenter(object sender, ExecutedRoutedEventArgs e)
        {
            //BlockCollection blocks = richTextBox.Document.Blocks;
            //foreach (System.Windows.Documents.Block block in blocks)
            //{
            //    block.TextAlignment = TextAlignment.Center;
            //}
            EditingCommands.AlignCenter.Execute(null, RichTextBox);

        }
        public void AlignJustify(object sender, ExecutedRoutedEventArgs e)
        {
            //BlockCollection blocks = richTextBox.Document.Blocks;
            //foreach (System.Windows.Documents.Block block in blocks)
            //{
            //    block.TextAlignment = TextAlignment.Justify;
            //}
            EditingCommands.AlignJustify.Execute(null, RichTextBox);
        }

        public void IndentRight(object sender, ExecutedRoutedEventArgs e)
        {
            EditingCommands.IncreaseIndentation.Execute(1, RichTextBox);
        }

        public void IndentLeft(object sender, ExecutedRoutedEventArgs e)
        {
            EditingCommands.DecreaseIndentation.Execute(1, RichTextBox);
        }

        private void TxtFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RichTextBox.Selection.IsEmpty) return;
            try
            {
                RichTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, double.Parse(TxtFontSize.Text));
            }
            catch { }
        }

        public void ToggleBullet(object sender, ExecutedRoutedEventArgs e)
        {
            EditingCommands.ToggleBullets.Execute(null, RichTextBox);
        }

        public void ToggleNumber(object sender, ExecutedRoutedEventArgs e)
        {
            EditingCommands.ToggleNumbering.Execute(null, RichTextBox);
        }


        public void FontColor()
        {
            RichTextBox.Selection.ApplyPropertyValue(TextBlock.ForegroundProperty, new SolidColorBrush(ColorPickerMain.Color));
        }

        public void FontColor(Color color)
        {
            if (RichTextBox == null) return;
            if (RichTextBox.Selection == null) return;
            RichTextBox.Selection.ApplyPropertyValue(TextBlock.ForegroundProperty, new SolidColorBrush(color));
        }

        private void EdcomboFonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RichTextBox == null) return;
            try
            {
                if (this.RichTextBox.Selection.IsEmpty)
                    this.RichTextBox.FontFamily = (EdcomboFonts.SelectedItem as TextBlock).FontFamily;
                else
                    this.RichTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, (EdcomboFonts.SelectedItem as TextBlock).FontFamily);
            }
            catch
            {

            }
        }

        private void RichTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorPickerMain.Color = ((SolidColorBrush)RichTextBox.Selection.GetPropertyValue(TextBlock.ForegroundProperty)).Color;
            }
            catch { }
        }

        private void ColorPickerMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (ColorPickerMain == null) return;
            if (ColorPickerMain.Color == null) return;
            FontColor(ColorPickerMain.Color);
        }

        private void ColorPickerMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ColorPickerMain == null) return;
            if (ColorPickerMain.Color == null) return;
            Border swatch = new Border()
            {
                Width = 20,
                Height = 20,
                Margin = new Thickness(4),
                Background = new SolidColorBrush(ColorPickerMain.Color),
                CornerRadius = new CornerRadius(8),
            };
            swatch.MouseDown += Swatch_MouseDown;
            ScrollSwatches.Children.Insert(0, swatch);
        }

        private void Swatch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border swatch = sender as Border;
            ColorPickerMain.Color = ((SolidColorBrush)swatch.Background).Color;
            FontColor(((SolidColorBrush)swatch.Background).Color);
        }




        const string Password = "110ff8d5d2ec47b98b1d53fc3d2bb4e1b517864b502e2f8d1f4cf4b10c017cee";
        const string Username = "yanal";

        private async Task<TblNews> AddNews(TblNews currentNews)
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            NewsCore newsCore = new NewsCore(token);
            await newsCore.AddNews(currentNews);

            return new TblNews();
        }

        private void BtnBack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnEdit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SwitchToEdittingMode();
        }

        private void SwitchToEdittingMode()
        {
            WrpYesNo.Visibility = Visibility.Visible;
            WrpOverview.Visibility = Visibility.Collapsed;
            RichTextBox.IsReadOnly = false;
            StckMainEditor.IsEnabled = true;
        }
    }
}
