using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using SevenZip;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using SharpCompress.Common.Zip;

namespace Audio.Crack.NetEase.Auto
{
    public class Compress
    {
        public class Zip
        {
            public class Sync
            {
                public static void CrackAndPack(string inputZipPath, string outputZipPath)
                {
                    using ZipArchive inputArchive = System.IO.Compression.ZipFile.OpenRead(inputZipPath);
                    using ZipArchive outputArchive = System.IO.Compression.ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    foreach (ZipArchiveEntry entry in inputArchive.Entries)
                    {
                        if (entry.FullName.EndsWith(".ncm", System.StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            entry.ExtractToFile(tempFilePath, true);
                            Crack.CrackAudio(tempFilePath);
                            AddToArchive(outputArchive, tempFilePath, entry.Name);
                            File.Delete(tempFilePath);
                        }
                    }
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }

            public class Async
            {
                public static async Task CrackAndPackAsync(string inputZipPath, string outputZipPath)
                {
                    using ZipArchive inputArchive = System.IO.Compression.ZipFile.OpenRead(inputZipPath);
                    using ZipArchive outputArchive = System.IO.Compression.ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    List<Task> crackingTasks = [];

                    foreach (ZipArchiveEntry entry in inputArchive.Entries)
                    {
                        if (entry.FullName.EndsWith(".ncm", System.StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            entry.ExtractToFile(tempFilePath, true);
                            Task crackingTask = CrackAudioAsync(tempFilePath);
                            crackingTasks.Add(crackingTask);
                            AddToArchive(outputArchive, tempFilePath, entry.Name);
                        }
                    }

                    await Task.WhenAll(crackingTasks);
                }

                private static async Task CrackAudioAsync(string fileName)
                {
                    await Task.Run(() =>
                    {
                        Crack.CrackAudio(fileName);
                    });
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }
        }

        public class Rar
        {
            public class Sync
            {
                public static void CrackAndPack(string inputRarPath, string outputZipPath)
                {
                    using var archive = RarArchive.Open(inputRarPath);
                    using ZipArchive outputArchive = System.IO.Compression.ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    foreach (var entry in archive.Entries)
                    {
                        if (entry.Key.EndsWith(".ncm", StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            entry.WriteToFile(tempFilePath);
                            Crack.CrackAudio(tempFilePath);
                            AddToArchive(outputArchive, tempFilePath, entry.Key);
                            File.Delete(tempFilePath);
                        }
                    }
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }

            public class Async
            {
                public static async Task CrackAndPackAsync(string inputRarPath, string outputZipPath)
                {
                    using var archive = RarArchive.Open(inputRarPath);
                    using ZipArchive outputArchive = System.IO.Compression.ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    List<Task> crackingTasks = [];

                    foreach (var entry in archive.Entries)
                    {
                        if (entry.Key.EndsWith(".ncm", StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            entry.WriteToFile(tempFilePath);
                            Task crackingTask = CrackAudioAsync(tempFilePath);
                            crackingTasks.Add(crackingTask);
                            AddToArchive(outputArchive, tempFilePath, entry.Key);
                        }
                    }

                    await Task.WhenAll(crackingTasks);
                }

                private static async Task CrackAudioAsync(string fileName)
                {
                    await Task.Run(() =>
                    {
                        Crack.CrackAudio(fileName);
                    });
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }
        }

        public class SevenZip_
        {
            public class Sync
            {
                public static void CrackAndPack(string input7zPath, string outputZipPath)
                {
                    using SevenZipExtractor extractor = new(input7zPath);
                    using ZipArchive outputArchive = System.IO.Compression.ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    foreach (var entry in extractor.ArchiveFileData)
                    {
                        if (entry.FileName.EndsWith(".ncm", StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            using (FileStream fs = new(tempFilePath, FileMode.Create))
                            {
                                extractor.ExtractFile(entry.Index, fs);
                            }
                            Crack.CrackAudio(tempFilePath);
                            AddToArchive(outputArchive, tempFilePath, entry.FileName);
                            File.Delete(tempFilePath);
                        }
                    }
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }

            public class Async
            {
                public static async Task CrackAndPackAsync(string input7zPath, string outputZipPath)
                {
                    using SevenZipExtractor extractor = new(input7zPath);
                    using ZipArchive outputArchive = System.IO.Compression.ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    List<Task> crackingTasks = [];

                    foreach (var entry in extractor.ArchiveFileData)
                    {
                        if (entry.FileName.EndsWith(".ncm", StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            using (FileStream fs = new(tempFilePath, FileMode.Create))
                            {
                                extractor.ExtractFile(entry.Index, fs);
                            }
                            Task crackingTask = CrackAudioAsync(tempFilePath);
                            crackingTasks.Add(crackingTask);
                            AddToArchive(outputArchive, tempFilePath, entry.FileName);
                        }
                    }

                    await Task.WhenAll(crackingTasks);
                }

                private static async Task CrackAudioAsync(string fileName)
                {
                    await Task.Run(() =>
                    {
                        Crack.CrackAudio(fileName);
                    });
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }
        }

        public class GZip
        {
            public static void SyncCrack(string gzFilePath, string exportFileName)
            {
                var tempDir = ExtractGz(gzFilePath);
                var ncmFiles = Directory.GetFiles(tempDir, "*.ncm");
                foreach (var ncmFile in ncmFiles)
                {
                    Crack.CrackAudio(ncmFile);
                }
                CompressFiles(ncmFiles, exportFileName);
                Directory.Delete(tempDir, true);
            }

            public static async Task AsyncCrack(string gzFilePath, string exportFileName)
            {
                var tempDir = ExtractGz(gzFilePath);
                var ncmFiles = Directory.GetFiles(tempDir, "*.ncm");
                var tasks = new List<Task>();
                foreach (var ncmFile in ncmFiles)
                {
                    tasks.Add(Task.Run(() => Crack.CrackAudio(ncmFile)));
                }
                await Task.WhenAll(tasks);
                CompressFiles(ncmFiles, exportFileName);
                Directory.Delete(tempDir, true);
            }

            private static string ExtractGz(string gzFilePath)
            {
                var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempDir);
                using (var fileStream = File.OpenRead(gzFilePath))
                using (var gzipStream = new GZipStream(fileStream, System.IO.Compression.CompressionMode.Decompress))
                {
                    using var archive = new ZipArchive(gzipStream, ZipArchiveMode.Read);
                    archive.ExtractToDirectory(tempDir);
                }
                return tempDir;
            }

            private static void CompressFiles(IEnumerable<string> fileNames, string exportFileName)
            {
                using var zipStream = new FileStream(exportFileName, FileMode.Create);
                using var archive = new ZipArchive(zipStream, ZipArchiveMode.Create);
                foreach (var fileName in fileNames)
                {
                    var entry = archive.CreateEntry(Path.GetFileName(fileName));
                    using var entryStream = entry.Open();
                    using var fileStream = new FileStream(fileName, FileMode.Open);
                    fileStream.CopyTo(entryStream);
                }
            }
        }

        public class Tar()
        {
            public static void SyncCrackAndCompress(string tarFilePath, string exportFileName)
            {
                var tempDir = ExtractTar(tarFilePath);
                var ncmFiles = Directory.GetFiles(tempDir, "*.ncm");

                foreach (var ncmFile in ncmFiles)
                {
                    Crack.CrackAudio(ncmFile);
                }

                CompressFiles(ncmFiles, exportFileName);
                Directory.Delete(tempDir, true);
            }

            public static async Task AsyncCrackAndCompress(string tarFilePath, string exportFileName)
            {
                var tempDir = ExtractTar(tarFilePath);
                var ncmFiles = Directory.GetFiles(tempDir, "*.ncm");

                await Task.WhenAll(Array.ConvertAll(ncmFiles, async ncmFile =>
                {
                    await Task.Run(() => Crack.CrackAudio(ncmFile));
                }));

                CompressFiles(ncmFiles, exportFileName);
                Directory.Delete(tempDir, true);
            }

            private static string ExtractTar(string tarFilePath)
            {
                var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempDir);
                System.IO.Compression.ZipFile.ExtractToDirectory(tarFilePath, tempDir);
                return tempDir;
            }

            private static void CompressFiles(IEnumerable<string> fileNames, string exportFileName)
            {
                using var zipStream = new FileStream(exportFileName, FileMode.Create);
                using var archive = new ZipArchive(zipStream, ZipArchiveMode.Create);
                foreach (var fileName in fileNames)
                {
                    var entry = archive.CreateEntry(Path.GetFileName(fileName));
                    using var entryStream = entry.Open();
                    using var fileStream = new FileStream(fileName, FileMode.Open);
                    fileStream.CopyTo(entryStream);
                }
            }
        }

        public class TarGz
        {
            public static void SyncCrackAndZip(string tarGzFilePath, string exportFileName)
            {
                // 提取 .tar.gz 文件到临时目录
                string tempDirectory = ExtractTarGz(tarGzFilePath);

                // 获取所有 .ncm 文件路径
                string[] ncmFiles = Directory.GetFiles(tempDirectory, "*.ncm", SearchOption.AllDirectories);

                // 遍历所有 .ncm 文件并逐个破解
                foreach (string ncmFile in ncmFiles)
                {
                    Crack.CrackAudio(ncmFile);
                }

                // 打包为 .zip 文件
                CreateZip(exportFileName, ncmFiles);

                // 清理临时目录
                Directory.Delete(tempDirectory, true);
            }

            public static async Task AsyncCrackAndZip(string tarGzFilePath, string exportFileName)
            {
                // 提取 .tar.gz 文件到临时目录
                string tempDirectory = ExtractTarGz(tarGzFilePath);

                // 获取所有 .ncm 文件路径
                string[] ncmFiles = Directory.GetFiles(tempDirectory, "*.ncm", SearchOption.AllDirectories);

                // 异步破解并等待所有任务完成
                await Task.WhenAll(Array.ConvertAll(ncmFiles, async ncmFile =>
                {
                    await Task.Run(() => Crack.CrackAudio(ncmFile));
                }));

                // 打包为 .zip 文件
                CreateZip(exportFileName, ncmFiles);

                // 清理临时目录
                Directory.Delete(tempDirectory, true);
            }

            private static void CreateZip(string zipFileName, IEnumerable<string> files)
            {
                // 创建新的 .zip 文件
                using ZipArchive archive = System.IO.Compression.ZipFile.Open(zipFileName, ZipArchiveMode.Create);
                // 将所有文件添加到 .zip 文件中
                foreach (string filePath in files)
                {
                    archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                }
            }

            private static string ExtractTarGz(string tarGzFilePath)
            {
                // 创建临时目录
                string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(tempDirectory);

                // 解压 .tar.gz 文件到临时目录
                using (FileStream fileStream = File.OpenRead(tarGzFilePath))
                {
                    using GZipStream gzipStream = new(fileStream, System.IO.Compression.CompressionMode.Decompress);
                    using var tarArchive = SharpCompress.Archives.Tar.TarArchive.Open(gzipStream);
                    foreach (var entry in tarArchive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            entry.WriteToDirectory(tempDirectory, new SharpCompress.Common.ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }
                    }
                }

                return tempDirectory;
            }
        }

        public class BZip
        {
            public static void SyncCrackAndZip(string bzFilePath, string exportFileName)
            {
                string tempDirectory = ExtractBz(bzFilePath);
                string[] ncmFiles = Directory.GetFiles(tempDirectory, "*.ncm", SearchOption.AllDirectories);

                foreach (string ncmFile in ncmFiles)
                {
                    Crack.CrackAudio(ncmFile);
                }

                CreateZip(exportFileName, ncmFiles);
                Directory.Delete(tempDirectory, true);
            }

            public static async Task AsyncCrackAndZip(string bzFilePath, string exportFileName)
            {
                string tempDirectory = ExtractBz(bzFilePath);
                string[] ncmFiles = Directory.GetFiles(tempDirectory, "*.ncm", SearchOption.AllDirectories);

                await Task.WhenAll(ncmFiles.Select(async ncmFile =>
                {
                    await Task.Run(() => Crack.CrackAudio(ncmFile));
                }));

                CreateZip(exportFileName, ncmFiles);
                Directory.Delete(tempDirectory, true);
            }

            private static void CreateZip(string zipFileName, IEnumerable<string> files)
            {
                using ZipArchive archive = System.IO.Compression.ZipFile.Open(zipFileName, ZipArchiveMode.Create);
                foreach (string filePath in files)
                {
                    archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                }
            }

            private static string ExtractBz(string bzFilePath)
            {
                string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(tempDirectory);

                using (var stream = File.OpenRead(bzFilePath))
                {
                    using var archive = ArchiveFactory.Open(stream);
                    foreach (var entry in archive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            entry.WriteToDirectory(tempDirectory, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                        }
                    }
                }

                return tempDirectory;
            }
        }

        public class BZip2
        {
            public static void SyncCrackAndZip(string bz2FilePath, string exportFileName)
            {
                string tempDirectory = ExtractBz2(bz2FilePath);
                string[] ncmFiles = Directory.GetFiles(tempDirectory, "*.ncm", SearchOption.AllDirectories);

                foreach (string ncmFile in ncmFiles)
                {
                    Crack.CrackAudio(ncmFile);
                }

                CreateZip(exportFileName, ncmFiles);
                Directory.Delete(tempDirectory, true);
            }

            public static async Task AsyncCrackAndZip(string bz2FilePath, string exportFileName)
            {
                string tempDirectory = ExtractBz2(bz2FilePath);
                string[] ncmFiles = Directory.GetFiles(tempDirectory, "*.ncm", SearchOption.AllDirectories);

                await Task.WhenAll(ncmFiles.Select(async ncmFile =>
                {
                    await Task.Run(() => Crack.CrackAudio(ncmFile));
                }));

                CreateZip(exportFileName, ncmFiles);
                Directory.Delete(tempDirectory, true);
            }

            private static void CreateZip(string zipFileName, IEnumerable<string> files)
            {
                using ZipArchive archive = System.IO.Compression.ZipFile.Open(zipFileName, ZipArchiveMode.Create);
                foreach (string filePath in files)
                {
                    archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                }
            }

            private static string ExtractBz2(string bz2FilePath)
            {
                string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(tempDirectory);

                using (var stream = File.OpenRead(bz2FilePath))
                {
                    using var archive = ArchiveFactory.Open(stream);
                    foreach (var entry in archive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            entry.WriteToDirectory(tempDirectory, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                        }
                    }
                }

                return tempDirectory;
            }
        }

        public class TarBZip2
        {
            public static void SyncCrackAndZip(string tarBz2FilePath, string exportFileName)
            {
                string tempDirectory = ExtractTarBz2(tarBz2FilePath);
                string[] ncmFiles = Directory.GetFiles(tempDirectory, "*.ncm", SearchOption.AllDirectories);

                foreach (string ncmFile in ncmFiles)
                {
                    Crack.CrackAudio(ncmFile);
                }

                CreateZip(exportFileName, ncmFiles);
                Directory.Delete(tempDirectory, true);
            }

            public static async Task AsyncCrackAndZip(string tarBz2FilePath, string exportFileName)
            {
                string tempDirectory = ExtractTarBz2(tarBz2FilePath);
                string[] ncmFiles = Directory.GetFiles(tempDirectory, "*.ncm", SearchOption.AllDirectories);

                await Task.WhenAll(ncmFiles.Select(async ncmFile =>
                {
                    await Task.Run(() => Crack.CrackAudio(ncmFile));
                }));

                CreateZip(exportFileName, ncmFiles);
                Directory.Delete(tempDirectory, true);
            }

            private static void CreateZip(string zipFileName, IEnumerable<string> files)
            {
                using ZipArchive archive = System.IO.Compression.ZipFile.Open(zipFileName, ZipArchiveMode.Create);
                foreach (string filePath in files)
                {
                    archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                }
            }

            private static string ExtractTarBz2(string tarBz2FilePath)
            {
                string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(tempDirectory);

                using (Stream stream = File.OpenRead(tarBz2FilePath))
                using (var archive = ArchiveFactory.Open(stream))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            entry.WriteToDirectory(tempDirectory, new ExtractionOptions { ExtractFullPath = true, Overwrite = true });
                        }
                    }
                }

                return tempDirectory;
            }
        }

        public class Z
        {
            public static void SyncCrackAndZip(string zFilePath, string exportFileName)
            {
                string tempDirectory = ExtractZip(zFilePath);
                string[] ncmFiles = Directory.GetFiles(tempDirectory, "*.ncm", SearchOption.AllDirectories);

                foreach (string ncmFile in ncmFiles)
                {
                    Crack.CrackAudio(ncmFile);
                }

                CreateZip(exportFileName, ncmFiles);
                Directory.Delete(tempDirectory, true);
            }

            public static async Task AsyncCrackAndZip(string zFilePath, string exportFileName)
            {
                string tempDirectory = ExtractZip(zFilePath);
                string[] ncmFiles = Directory.GetFiles(tempDirectory, "*.ncm", SearchOption.AllDirectories);

                await Task.WhenAll(ncmFiles.Select(async ncmFile =>
                {
                    await Task.Run(() => Crack.CrackAudio(ncmFile));
                }));

                CreateZip(exportFileName, ncmFiles);
                Directory.Delete(tempDirectory, true);
            }

            private static void CreateZip(string zipFileName, IEnumerable<string> files)
            {
                using ZipOutputStream zipStream = new(File.Create(zipFileName));
                byte[] buffer = new byte[4096];

                foreach (string filePath in files)
                {
                    ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new(Path.GetFileName(filePath));
                    zipStream.PutNextEntry(entry);

                    using (FileStream fileStream = File.OpenRead(filePath))
                    {
                        int bytesRead;
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            zipStream.Write(buffer, 0, bytesRead);
                        }
                    }

                    zipStream.CloseEntry();
                }
            }

            private static string ExtractZip(string zipFilePath)
            {
                string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(tempDirectory);

                using (ICSharpCode.SharpZipLib.Zip.ZipFile zipFile = new(zipFilePath))
                {
                    foreach (ICSharpCode.SharpZipLib.Zip.ZipEntry entry in zipFile)
                    {
                        if (!entry.IsFile)
                        {
                            continue;
                        }

                        string entryFileName = Path.GetFileName(entry.Name);
                        if (entryFileName.Length == 0)
                        {
                            continue;
                        }

                        string fullZipToPath = Path.Combine(tempDirectory, entryFileName);
                        using FileStream streamWriter = File.Create(fullZipToPath);
                        byte[] buffer = new byte[4096];
                        using Stream inputStream = zipFile.GetInputStream(entry);
                        int bytesRead;
                        while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            streamWriter.Write(buffer, 0, bytesRead);
                        }
                    }
                }


                return tempDirectory;
            }
        }
    }
}
