using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Library;
using Toolbox.Library.IO;
using Toolbox.Library.Security.Cryptography;

//
using System.Windows.Forms;

namespace FirstPlugin
{
    public class ASBParse
    {
        public class Field
        {
            public uint Hash { get; set; }
            public uint Offset { get; set; }

            public object Value;
        }

        public class DataEntry
        {
            public Dictionary<string, object> Fields;
        }

        static Dictionary<uint, string> hashes = new Dictionary<uint, string>();
        public static Dictionary<uint, string> Hashes
        {
            get
            {
                if (hashes.Count == 0)
                    CalculateHashes();
                return hashes;
            }
        }

        public List<DataEntry> Entries = new List<DataEntry>();

        public void Read(FileReader reader)
        {
            //int fileLength = (int)reader.Length;
            //byte[] allBytes = reader.ReadBytes(fileLength);
            //reader.Position = 0;
            //StringBuilder strBuilder = new StringBuilder();
            //using (var textWriter = new System.IO.StringWriter(strBuilder))
            //{
            //    for (int i = 0; i < fileLength; i++)
            //    {
            //        byte bt = allBytes[i];
            //        textWriter.WriteLine(bt.ToString());
            //    }
            //}
            //string binaryText = strBuilder.ToString();
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "as |*.as;";
            //sfd.FileName = System.IO.Path.GetFileNameWithoutExtension("111");
            //sfd.DefaultExt = ".as";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //    System.IO.File.WriteAllText(sfd.FileName, binaryText);
            //
            byte signature1 = reader.ReadByte(); // 'A'
            if (signature1 != 'A')
            {
                throw new Exception("File format invalid");
            }
            byte signature2 = reader.ReadByte(); // 'S'
            if (signature2 != 'S')
            {
                throw new Exception("File format invalid");
            }
            byte signature3 = reader.ReadByte(); // 'B'
            if (signature3 != 'B')
            {
                throw new Exception("File format invalid");
            }
            byte blank = reader.ReadByte();      // ' '
            if (blank != ' ')
            {
                throw new Exception("File format invalid");
            }
            byte sevenOrFive = reader.ReadByte();      // 7 or 5
            if (sevenOrFive != 7 && sevenOrFive != 5)
            {
                throw new Exception("File format invalid");
            }
            byte four = reader.ReadByte();       // 4
            if (four != 4)
            {
                throw new Exception("File format invalid");
            }
            byte zero1 = reader.ReadByte();      // 0
            if (zero1 != 0)
            {
                throw new Exception("File format invalid");
            }
            byte zero2 = reader.ReadByte();      // 0
            if (zero2 != 0)
            {
                throw new Exception("File format invalid");
            }
            byte zero3 = reader.ReadByte();      // 0
            if (zero3 != 0)
            {
                throw new Exception("File format invalid");
            }
            byte zero4 = reader.ReadByte();      // 0
            if (zero4 != 0)
            {
                throw new Exception("File format invalid");
            }
            byte zero5 = reader.ReadByte();      // 0
            if (zero5 != 0)
            {
                throw new Exception("File format invalid");
            }
            byte zero6 = reader.ReadByte();      // 0
            if (zero6 != 0)
            {
                throw new Exception("File format invalid");
            }
            uint keyFrameCount1 = reader.ReadUInt32();  // key frame count
            uint keyFrameCount2 = reader.ReadUInt32();
            uint keyFrameCount3 = reader.ReadUInt32();
            uint keyFrameCount4 = reader.ReadUInt32();
            uint keyFrameCount5 = reader.ReadUInt32();
            uint filePointer1 = reader.ReadUInt32();
            uint filePointer2 = reader.ReadUInt32();
            uint filePointer3 = reader.ReadUInt32();
            uint filePointer4 = reader.ReadUInt32();
            uint secondPartBeginFrom = reader.ReadUInt32();
            uint filePointer6 = reader.ReadUInt32();
            uint filePointer7 = reader.ReadUInt32();
            uint keyFrameCount6 = (filePointer7 & 0xFF00) >> 8;
            uint filePointer8 = reader.ReadUInt32();
            uint filePointer9 = reader.ReadUInt32();
            uint filePointer10 = reader.ReadUInt32();
            uint filePointer11 = reader.ReadUInt32();
            uint filePointer12 = reader.ReadUInt32();
            uint filePointer13 = reader.ReadUInt32();
            uint filePointer14 = reader.ReadUInt32();
            uint filePointer15 = reader.ReadUInt32();
            uint filePointer16 = reader.ReadUInt32();
            uint filePointer17 = reader.ReadUInt32();
            
            for (int i = 0; i < keyFrameCount1; i++)
            {
                uint beginFrameIndex = reader.ReadUInt32(); // begin frame index
                ushort endFrameIndex = reader.ReadUInt16(); // end frame index
                uint allZero_1 = reader.ReadUInt32();       // all zero
                uint allZero_2 = reader.ReadUInt32();       // all zero
                ushort typeFlag = reader.ReadUInt16();      // what type flag?
                uint allZero_3 = reader.ReadUInt32();       // all zero
                uint allZero_4 = reader.ReadUInt32();       // all zero
                OpenTK.Quaternion quat = reader.ReadQuaternion();
                int a = 0;
            }
            uint whatCount = reader.ReadUInt32();           // what count?
            for (int i = 0; i < keyFrameCount2; i++)
            {
                ushort beginFrameIndex = reader.ReadUInt16(); // begin frame index
                ushort endFrameIndex = reader.ReadUInt16(); // end frame index
                uint allZero_1 = reader.ReadUInt32();       // all zero
                uint typeFlag = reader.ReadUInt32();        // what type flag?
                uint allZero_3 = reader.ReadUInt32();       // all zero
                uint allZero_4 = reader.ReadUInt32();       // all zero
                OpenTK.Quaternion quat = reader.ReadQuaternion();
                int a = 0;
            }
            reader.Position = secondPartBeginFrom;
            uint whatCount2 = reader.ReadUInt32();          // what count?
            for (int i = 0; i < keyFrameCount5; i++)
            {
                uint beginFrameIndex = reader.ReadUInt32();
                for (int j = 0; j < 14; j++)
                {
                    uint allZero_5 = reader.ReadUInt32();
                }
            }
            for (int i = 0; i < keyFrameCount6; i++)
            {
                uint beginFrameIndex = reader.ReadUInt32();
                for (int j = 0; j < 14; j++)
                {
                    uint allZero_5 = reader.ReadUInt32();
                }
            }
            //
            //uint numEntries = reader.ReadUInt32();
            //uint entrySize = reader.ReadUInt32();
            //ushort numFields = reader.ReadUInt16();
            //byte flag1 = reader.ReadByte();
            //byte flag2 = reader.ReadByte();
            //if (flag1 == 1)
            //{
            //    uint magic = reader.ReadUInt32();
            //    uint unk = reader.ReadUInt32(); //Always 100000
            //    reader.ReadUInt32();//0
            //    reader.ReadUInt32();//0
            //}

            //Field[] fields = new Field[numFields];
            //for (int i = 0; i < numFields; i++) {
            //    fields[i] = new Field()
            //    {
            //        Hash = reader.ReadUInt32(),
            //        Offset = reader.ReadUInt32(),
            //    };
            //}
            //for (int i = 0; i < numEntries; i++)
            //{
            //    DataEntry entry = new DataEntry();
            //    Entries.Add(entry);
            //    entry.Fields = new Dictionary<string, object>();

            //    long pos = reader.Position;
            //    for (int f = 0; f < fields.Length; f++)
            //    {
            //        DataType type = DataType.String;
            //        uint size = entrySize - fields[f].Offset;
            //        if (f < fields.Length - 1) {
            //            size = fields[f + 1].Offset - fields[f].Offset;
            //        }
            //        if (size == 1)
            //            type = DataType.Byte;
            //        if (size == 2)
            //            type = DataType.Int16;
            //        if (size == 4)
            //            type = DataType.Int32;

            //        reader.SeekBegin(pos + fields[f].Offset);
            //        object value = 0;
            //        string name = fields[f].Hash.ToString("x");
            //        switch (type)
            //        {
            //            case DataType.Byte:
            //                value = reader.ReadByte();
            //                break;
            //            case DataType.Float:
            //                value = reader.ReadSingle();
            //                break;
            //            case DataType.Int16:
            //                value = reader.ReadInt16();
            //                break;
            //            case DataType.Int32:
            //                value = reader.ReadInt32();
            //                if (IsFloatValue((int)value))
            //                {
            //                    reader.Seek(-4);
            //                    value = reader.ReadSingle();
            //                }
            //                break;
            //            case DataType.String:
            //                value = reader.ReadZeroTerminatedString(Encoding.UTF8);
            //                break;
            //        }

            //        if (Hashes.ContainsKey(fields[f].Hash))
            //            name = Hashes[fields[f].Hash];

            //        entry.Fields.Add(name, value);
            //    }

            //    reader.SeekBegin(pos + entrySize);
            //}
        }

        private bool IsFloatValue(int value) {
            return value.ToString().Length > 6;
        }

        public enum DataType
        {
            Byte,
            Int16,
            Int32,
            Int64,
            Float,
            String,
        }

        public void Write(FileWriter writer)
        {
            writer.Write(Entries.FirstOrDefault().Fields.Count);
        }

        private static void CalculateHashes()
        {
            string dir = Path.Combine(Runtime.ExecutableDir, "Hashes");
            if (!Directory.Exists(dir))
                return;

            foreach (var file in Directory.GetFiles(dir))
            {
                if (Utils.GetExtension(file) != ".txt")
                    continue;

                foreach (string hashStr in File.ReadAllLines(file))
                {
                    CheckHash(hashStr);
                }
            }
        }

        private static void CheckHash(string hashStr)
        {
            uint hash = Crc32.Compute(hashStr);
            if (!hashes.ContainsKey(hash))
                hashes.Add(hash, hashStr);
        }
    }
}
