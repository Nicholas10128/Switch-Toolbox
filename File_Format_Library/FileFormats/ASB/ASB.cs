using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox;
using System.Windows.Forms;
using Toolbox.Library;
using Toolbox.Library.IO;
using Toolbox.Library.Forms;

namespace FirstPlugin
{
    public class ASB : IEditor<TextEditor>, IFileFormat, IConvertableTextFormat
    {
        public FileType FileType { get; set; } = FileType.Parameter;

        public bool CanSave { get; set; }
        public string[] Description { get; set; } = new string[] { "Binary Animation Sequence" };
        public string[] Extension { get; set; } = new string[] { "*.asb" };
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public IFileInfo IFileInfo { get; set; }

        public bool Identify(System.IO.Stream stream)
        {
            using (var reader = new Toolbox.Library.IO.FileReader(stream, true))
            {
                return reader.CheckSignature(3, "ASB", 0) || Utils.GetExtension(FileName) == ".asb";
            }
        }

        public Type[] Types
        {
            get
            {
                List<Type> types = new List<Type>();
                return types.ToArray();
            }
        }

        public TextEditor OpenForm()
        {
            var textEditor = new TextEditor();
            textEditor.ClearContextMenus(new string[] { "Search" });
            textEditor.AddContextMenu("Export as CSV", ExportCSV);
            return textEditor;
        }

        public void FillEditor(UserControl control)
        {
            ((TextEditor)control).FileFormat = this;
            ((TextEditor)control).FillEditor(ConvertToString());
            ((TextEditor)control).IsYAML = true;

        }

        public ASBParse ASBFile;

        #region Text Converter Interface
        public TextFileType TextFileType => TextFileType.Yaml;
        public bool CanConvertBack => false;

        public string ConvertToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            using (var textWriter = new System.IO.StringWriter(strBuilder))
            {
                for (int i = 0; i < ASBFile.Entries.Count; i++)
                {
                    textWriter.WriteLine($"Entry_{i}:");
                    foreach (var field in ASBFile.Entries[i].Fields) {
                        textWriter.WriteLine($"  {field.Key}: {field.Value}");
                    }
                }
            }
            return strBuilder.ToString();
        }

        private void ExportCSV(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV |*.csv;";
            sfd.FileName = System.IO.Path.GetFileNameWithoutExtension(FileName);
            sfd.DefaultExt = ".csv";
            if (sfd.ShowDialog() == DialogResult.OK)
                System.IO.File.WriteAllText(sfd.FileName, ConvertToCSV());
        }

        public string ConvertToCSV()
        {
            StringBuilder strBuilder = new StringBuilder();
            using (var textWriter = new System.IO.StringWriter(strBuilder))
            {
                var fields = ASBFile.Entries.FirstOrDefault().Fields;
                textWriter.WriteLine($"{string.Join(",", fields.Keys)}");

                for (int i = 0; i < ASBFile.Entries.Count; i++)
                    textWriter.WriteLine($"{string.Join(",", ASBFile.Entries[i].Fields.Values)}");
            }
            return strBuilder.ToString();
        }

        public void ConvertFromString(string text)
        {
        }

        #endregion

        public void Load(System.IO.Stream stream) {
            ASBFile = new ASBParse();
            ASBFile.Read(new FileReader(stream));
        }

        public void Unload()
        {
        }

        public void Save(System.IO.Stream stream) {
            ASBFile.Write(new FileWriter(stream));
        }
    }
}
