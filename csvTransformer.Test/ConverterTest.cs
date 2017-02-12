using System.Data;
using System.IO;
using System.Reflection;
using csvTransformer.Models.Business;
using csvTransformer.Models.Enum;
using Excel;
using NUnit.Framework;

namespace csvTransformer.Test
{
    [TestFixture]
    public class ConverterTest
    {
        [Test]
        public void CarregarArquivoXlsxValidoSemErros()
        {
            Stream stream = new MemoryStream(Properties.Resources.teste);

            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

             Assert.That(excelReader.IsValid);
        }

        [Test]
        public void DadosDoXlsxDevemEstarCorretos()
        {
            Stream stream = new MemoryStream(Properties.Resources.teste);

            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            excelReader.IsFirstRowAsColumnNames = false;
            var dataSet = excelReader.AsDataSet();

            Assert.That(dataSet.Tables.Count, Is.EqualTo(1));
            Assert.That(dataSet.Tables[0].Columns.Count, Is.EqualTo(5));
            Assert.That(dataSet.Tables[0].Rows.Count, Is.EqualTo(21));

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (row[0].ToString() == string.Empty) { continue; }
                var linhaSeparadaPorVirgula = string.Join(",", row.ItemArray);
                Assert.That(linhaSeparadaPorVirgula, Is.EqualTo("valor1,valor2,valor3,valor4,valor5"));
            }
        }

        [Test]
        public void ConverterXlsEmCsvComSucesso()
        {
            if (File.Exists(@"teste.csv"))
                File.Delete(@"teste.csv");

            Stream stream = new MemoryStream(Properties.Resources.teste);

            var converter = new Converter();

            var result = converter.File(stream).From(FileType.xls).To(FileType.csv).Result();

            using (var fileStream = File.Create(@"teste.csv"))
            {
                var bytes = new byte[result.Length];
                result.Read(bytes, 0, bytes.Length);
                fileStream.Write(bytes, 0, bytes.Length);
            }

            Assert.That(File.Exists(@"teste.csv"));

            var fileInfo = new FileInfo(@"teste.csv");

            Assert.That(fileInfo.Length, Is.GreaterThan(0));
        }
    }
}
