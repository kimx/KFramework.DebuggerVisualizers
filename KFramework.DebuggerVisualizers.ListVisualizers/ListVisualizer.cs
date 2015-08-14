using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Collections;
using KFramework.DebuggerVisualizers.ListVisualizers;

//reference:https://vsdatawatchers.codeplex.com/
//after build copy your dll to C:\Users\user\Documents\Visual Studio 2015\Visualizers
[assembly: System.Diagnostics.DebuggerVisualizer(
    typeof(ListVisualizer),
    typeof(ListVisualizerObjectSource),
    Target = typeof(List<>),
    Description = "KFramework List Viewer")]
namespace KFramework.DebuggerVisualizers.ListVisualizers
{
    public class ListVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService
            ,IVisualizerObjectProvider objectProvider)
        {
            var list = objectProvider.GetObject();
            VisualizerForm vf = new VisualizerForm(list);
            vf.ShowDialog();
        }

        /// <summary>
        /// for 測試用
        /// </summary>
        /// <param name="objectToVisualize"></param>
        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost host =
                new VisualizerDevelopmentHost(
                    objectToVisualize, typeof(ListVisualizer),
                    typeof(ListVisualizerObjectSource));
            host.ShowVisualizer();
        }
    }
}
