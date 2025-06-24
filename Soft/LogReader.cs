using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Soft
{

	public class LogReader
	{

        private static LogReader _singleton;

        private static LogReader Instance
        {
            get
            {
                _singleton ??= new LogReader();
                return _singleton;
            }
        }

        private LogReader()
		{
		}

        private string[] GetFileLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

		public static IEnumerable<Func<Task<string[]>>> LoadFolder(string folderPath)
        {
            return Instance.LoadFolderAsync(folderPath);
        }

        private IEnumerable<Func<Task<string[]>>> LoadFolderAsync(string folderPath)
        {
            List<Func<Task<string[]>>> allAsyncTasks = new();
            var fileList = Directory.GetFiles(folderPath);
            foreach (var file in fileList)
            {
                allAsyncTasks.Add(async () => { return GetFileLines(file); });
            }
            return allAsyncTasks;
        }


        // multi thread
        //private ... LoadFolderMultiThread(string folderPath)
        //{
        //    var fileList = Directory.GetFiles(folderPath);
        //    Parallel.ForEach(fileList, file =>
        //    {

        //    });
        //}



        // pourrait etre parrallele mais en fait est bloquante et lineaire
        //string[] lines = await Task.Run(() =>
        //{
        //    return GetFileLines(file);
        //});


    }

}
