using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
namespace AddingFeaturesInFGDBV2
{
    public class AddFeaturesBro : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public AddFeaturesBro()
        {
        }

        protected override void OnClick()
        {
              
            IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactory();
            IFeatureWorkspace featureWorkspace = workspaceFactory.OpenFromFile(@"C:\Users\gurminderb\Documents\Palmo\TemplateData\TemplateData.gdb",0) as IFeatureWorkspace;

            IWorkspaceEdit workspaceEdit = featureWorkspace as IWorkspaceEdit;
            workspaceEdit.StartEditing(false);

            IFeatureClass featureClass = featureWorkspace.OpenFeatureClass("cities_1");
            IFeatureLayer featureLayer = new FeatureLayer();
            featureLayer.FeatureClass = featureClass;
            featureLayer.Name = "GurminderTheGreat";
      
            ArcMap.Document.ActiveView.FocusMap.AddLayer(featureLayer);
            IFeature featureAdd = featureClass.CreateFeature();
            IPoint point = new Point();
            point.X = 12.5;
            point.Y = 32.5;

            featureAdd.Shape = point;
            featureAdd.Store();
            workspaceEdit.StopEditing(true);


            ArcMap.Application.CurrentTool = null;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
