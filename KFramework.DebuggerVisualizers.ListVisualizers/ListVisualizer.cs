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
        protected override void Show(
    IDialogVisualizerService windowService,
    IVisualizerObjectProvider objectProvider)
        {
            VisualizerForm vf = new VisualizerForm(
               objectProvider.GetObject());
            vf.ShowDialog();
            
        }

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
