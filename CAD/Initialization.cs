
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Interop;
using System.Windows.Forms;
using System.Reflection;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using System.IO;
using System.Collections.Specialized;
using Autodesk.AutoCAD.Customization;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DNA;
using com.ccepc.entities;
using Autodesk.AutoCAD.Windows;

[assembly: ExtensionApplication(typeof(CAD.AppInitialization))]
namespace CAD
{

    public class AppInitialization : IExtensionApplication
    {
        public static User loginUser;
        private static ToolsForm toolsform;
        private static TabbedDialogExtension optiontab;
        public static Pane userInfoPanel;
        public static Pane fileInfoPanel;

        void DocumentManager_DocumentActivated(object sender, DocumentCollectionEventArgs e)
        {
            G.InvalidDocument();
        }

        #region IExtensionApplication 成员

        void AcadApp_DisplayingOptionDialog(object sender, TabbedDialogEventArgs e)
        {

            //创建一个自定义控件的实例，这里使用的是显示图片的自定义控件
            CADOptionControl options = new CADOptionControl();
            //为确定按钮添加动作，也可以为取消、应用、帮助按钮添加动作
            TabbedDialogAction onOkPress = new TabbedDialogAction(OnOptionOK);
            //将显示图片的控件作为标签页添加到选项对话框，并处理确定按钮所引发的动作
            optiontab = new TabbedDialogExtension(options, onOkPress);
            e.AddTab("协同设计设置", optiontab);
        }

        void IExtensionApplication.Initialize()
        {
            //Assembly.LoadFrom("OPM.NET.dll");
            //Masterhe.OPM.OPMManager.Add(typeof(Line), new LineAtt());
            //toolsform = new ToolsForm();
            //AcadApp.ShowModelessDialog(toolsform);
            //添加插件菜单
            Commands.AddMenu();
            //打印插件版权信息
            Tools.WriteMessageWithReturn("欢迎使用协同设计平台！");
            //注册右键菜单
            BlockContextMenu.AttachMenu();
            //注册双击事件
            CADDoubleClick.DoubleClickStart("CIADMS");

            AcadApp.DocumentManager.DocumentActivated += new DocumentCollectionEventHandler(DocumentManager_DocumentActivated);
            
            //设置应用标题图标
            TitleIcon.SetTitle("协同设计平台");
            TitleIcon.SetIcon();
            //注册提示信息
            Commands.InfoTips();
            //注册插件选项设置面板
            AcadApp.DisplayingOptionDialog += new TabbedDialogEventHandler(AcadApp_DisplayingOptionDialog);

            userInfoPanel = new Pane();
            userInfoPanel.Visible = true;
            userInfoPanel.Enabled = true;
            userInfoPanel.Style = PaneStyles.Normal;

            fileInfoPanel = new Pane();
            fileInfoPanel.Visible = true;
            fileInfoPanel.Enabled = true;
            fileInfoPanel.Style = PaneStyles.Normal;

            AcadApp.StatusBar.Panes.Add(userInfoPanel);
            AcadApp.StatusBar.Panes.Add(fileInfoPanel);
        }

        void IExtensionApplication.Terminate()
        {
            //AcadApp.DocumentManager.DocumentActivated -= new DocumentCollectionEventHandler(DocumentManager_DocumentActivated);
            BlockContextMenu.DetachMenu();
        }

        private void OnOptionOK()
        {
            //当确定按钮被按下时，显示一个警告对话框
            CADOptionControl oc = optiontab.Control as CADOptionControl;
            if (oc != null)
            {
                oc.SaveOptions();
            }
        }

        #endregion
    }

}

