using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.ccepc.entities;

namespace Model.com.ccepc.utils
{
    public interface CADService
    {
        string getUsers();
        string login(string userName, string password);
        string getDesigneFolder(string designerConfigId);
        string uploadFile(string designerConfigId, string fileNewName,string fileName, string userId);
        string getFileInfoByDesignerConfigAndFileName(string designerConfig, string fileName);
        string getDesignerConfigsByUser(string userId);
        string getFileInfo(string fileId);
    }
}
