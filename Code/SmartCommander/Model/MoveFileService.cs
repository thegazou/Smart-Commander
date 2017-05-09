using SmartCommander.ViewModel;
using System;
using System.IO;
using System.Linq;

namespace SmartCommander.Model
{
    public class MoveFileService
    {
        public static void PastFile(String copyPath, ExplorerWindowViewModel _viewModel)
        {
            String fileName = copyPath.Split('\\').Last();
            String destFileName;
            if (_viewModel.DirViewVM.CurrentItem != null)
            {
                if (_viewModel.DirViewVM.CurrentItem.DirType == (int)ObjectType.Directory)
                    destFileName = _viewModel.MainWindowVM.getPath(_viewModel.Id) + "\\" + fileName;
                else
                {
                    String dirName = _viewModel.MainWindowVM.getPath(_viewModel.Id).Substring(0, _viewModel.MainWindowVM.getPath(_viewModel.Id).LastIndexOf(fileName));
                    destFileName = dirName + fileName;
                }

                if (File.Exists(destFileName))
                {
                    //check si existe déjà
                    if (destFileName.Contains("-Copie("))
                        destFileName = destFileName.Substring(0, destFileName.LastIndexOf("-Copie(")) + '.' + copyPath.Split('.').Last();
                    String temp = destFileName;
                    String extension = copyPath.Split('.').Last();
                    int increment = 1;
                    while (File.Exists(temp))
                    {
                        temp = destFileName.Split('.').First();
                        temp += "-Copie(" + increment + ")." + extension;
                        increment++;
                    }
                    destFileName = temp;
                }
                File.Copy(copyPath, destFileName);
            }

        }
    }
}
