using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.ccepc.utils;
using com.ccepc.entities;

namespace com.ccepc.utils
{
    public class CADServiceImpl
    {
        private static CADService service = HessianHelper.getServiceInstance();

        public static List<User> getUsers()
        {
            string result = service.getUsers();
            List<User> users = JsonHelper.JsonDeserialize<List<User>>(result);
            return users;
        }

        public static User login(string userName, string password)
        {
            string result = service.login(userName, password);
            User user = JsonHelper.JsonDeserialize<User>(result);
            return user;
        }

        public static FolderInfo getDesigneFolder(string designerConfigId)
        {
            string result = service.getDesigneFolder(designerConfigId);
            FolderInfo folderInfo = JsonHelper.JsonDeserialize<FolderInfo>(result);
            return folderInfo;
        }

        public static string uploadFile(string designerConfigId, string fileNewName, string fileName, string userId)
        {
            return service.uploadFile(designerConfigId, fileNewName, fileName, userId);
        }

        public static FileInfo getFileInfoByDesignerConfigAndFileName(string designerConfig, string fileName)
        {
            string result = service.getFileInfoByDesignerConfigAndFileName(designerConfig, fileName);
            FileInfo fileInfo = JsonHelper.JsonDeserialize<FileInfo>(result);
            return fileInfo;
        }

        public static List<DesignerConfig> getDesignerConfigsByUser(string userId)
        {
            string result = service.getDesignerConfigsByUser(userId);
            List<DesignerConfig> designerConfigs = JsonHelper.JsonDeserialize<List<DesignerConfig>>(result);
            return designerConfigs;
        }

        public static FileInfo getFileInfo(string fileId)
        {
            string result = service.getFileInfo(fileId);
            FileInfo fileInfo = JsonHelper.JsonDeserialize<FileInfo>(result);
            return fileInfo;
        }
    }
}
