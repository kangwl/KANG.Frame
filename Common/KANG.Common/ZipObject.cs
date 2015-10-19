#region

using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

#endregion

namespace KANG.Common {
    public class ZipObject {
        #region 压缩解压object

        public static byte[] CompressionObject(object DataOriginal) {
            if (DataOriginal == null) return null;
            var bFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            bFormatter.Serialize(mStream, DataOriginal);
            var bytes = mStream.ToArray();
            var oStream = new MemoryStream();
            var zipStream = new DeflateStream(oStream, CompressionMode.Compress);
            zipStream.Write(bytes, 0, bytes.Length);
            zipStream.Flush();
            zipStream.Close();
            return oStream.ToArray();
        }

        public static object DecompressionObject(byte[] bytes) {
            if (bytes == null) return null;
            var mStream = new MemoryStream(bytes);
            mStream.Seek(0, SeekOrigin.Begin);
            var unZipStream = new DeflateStream(mStream, CompressionMode.Decompress, true);
            object dsResult = null;
            var bFormatter = new BinaryFormatter();
            dsResult = bFormatter.Deserialize(unZipStream);
            return dsResult;
        }

        #endregion
    }
}