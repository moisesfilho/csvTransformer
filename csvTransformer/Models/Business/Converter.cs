using System;
using System.Data;
using System.IO;
using csvTransformer.Models.Enum;
using Excel;

namespace csvTransformer.Models.Business
{
    public class Converter
    {
        private FileType _fileTypeFrom;
        private FileType _fileTypeTo;
        private Stream _entryStream;

        public Converter From(FileType fileType)
        {
            _fileTypeFrom = fileType;

            return this;
        }

        public Converter To(FileType fileType)
        {
            _fileTypeTo = fileType;

            return this;
        }

        public Converter File(Stream stream)
        {
            _entryStream = stream;

            return this;
        }

        public Stream Result()
        {
            switch (_fileTypeFrom)
            {
                case FileType.csv:
                    switch (_fileTypeTo)
                    {
                        case FileType.csv:
                            return _entryStream;
                        case FileType.xls:
                            throw new NotImplementedException();
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case FileType.xls:
                    switch (_fileTypeTo)
                    {
                        case FileType.csv:
                            return XlsToCsv();
                        case FileType.xls:
                            return _entryStream;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Stream XlsToCsv()
        {
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(_entryStream);
            excelReader.IsFirstRowAsColumnNames = false;
            var dataSet = excelReader.AsDataSet();

            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                writer.Write(string.Join(",", row.ItemArray) + Environment.NewLine);
            }

            writer.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
    }
}