using System.IO;
using csvTransformer.Models.Enum;

namespace csvTransformer.Models.Business
{
    public interface IConverter
    {
        Converter From(FileType fileType);
        Converter To(FileType fileType);
        Converter File(Stream stream);
        Stream Result();
    }
}