using System.IO.Compression;
using E4A.PostGuard.Models;

namespace E4A.PostGuard.Zip;

internal static class ZipHelper
{
    public static byte[] CreateZip(IReadOnlyList<PgFile> files)
    {
        using var ms = new MemoryStream();
        using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, leaveOpen: true))
        {
            foreach (var file in files)
            {
                var entry = archive.CreateEntry(file.Name, CompressionLevel.Optimal);
                using var entryStream = entry.Open();
                file.Content.CopyTo(entryStream);
            }
        }

        return ms.ToArray();
    }
}
