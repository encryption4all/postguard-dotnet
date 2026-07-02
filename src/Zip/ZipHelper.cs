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
                // Strip any directory components so traversal sequences (e.g. "../../")
                // cannot be embedded as ZIP entry names in produced archives.
                var entryName = Path.GetFileName(file.Name);
                if (string.IsNullOrEmpty(entryName))
                {
                    throw new ArgumentException(
                        $"File name '{file.Name}' does not resolve to a valid entry name.",
                        nameof(files));
                }

                var entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
                using var entryStream = entry.Open();
                file.Content.CopyTo(entryStream);
            }
        }

        return ms.ToArray();
    }
}
