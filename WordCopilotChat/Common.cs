using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCopilotChat
{
    class Common
    {
        private static Microsoft.Office.Tools.CustomTaskPane _myCustomTaskPane;
        // 创建显示用户窗体的方法，并对外暴露 关键字 public
        public void ShowCustomTask()
        {
            if (_myCustomTaskPane == null)
            {
                UserControl1 mainControl = new UserControl1();
                _myCustomTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(mainControl, "WordCopilotChat");
                _myCustomTaskPane.Visible = true;
                _myCustomTaskPane.Width = 520;
                _myCustomTaskPane.DockPosition = MsoCTPDockPosition.msoCTPDockPositionRight;

                // 监听VisibleChanged事件，当用户点击×关闭时重置变量
                _myCustomTaskPane.VisibleChanged += _myCustomTaskPane_VisibleChanged;
            }
            else
            {
                // 如果已存在，直接显示
                _myCustomTaskPane.Visible = true;
            }
        }

        // 处理CustomTaskPane可见性变化事件
        private void _myCustomTaskPane_VisibleChanged(object sender, EventArgs e)
        {
            if (_myCustomTaskPane != null && !_myCustomTaskPane.Visible)
            {
                // 当用户点击×关闭时，清理资源
                _myCustomTaskPane.VisibleChanged -= _myCustomTaskPane_VisibleChanged;
                Globals.ThisAddIn.CustomTaskPanes.Remove(_myCustomTaskPane);
                _myCustomTaskPane = null;
            }
        }
        // 创建隐藏用户窗体的方法，并对外暴露 关键字 public
        public void HideCustomTask()
        {
            if (_myCustomTaskPane != null)
            {
                // 取消事件监听，避免重复处理
                _myCustomTaskPane.VisibleChanged -= _myCustomTaskPane_VisibleChanged;
                Globals.ThisAddIn.CustomTaskPanes.Remove(_myCustomTaskPane);
                _myCustomTaskPane = null;
            }
        }
    }
}
