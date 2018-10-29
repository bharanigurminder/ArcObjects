using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;

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

            //Looping through all the feature dataset and feature classes 
            IWorkspace workspaceGetDatasetNames = featureWorkspace as IWorkspace;
            IEnumDatasetName listDatasetName =  workspaceGetDatasetNames.DatasetNames[esriDatasetType.esriDTFeatureDataset];
            IDatasetName featureDataset = listDatasetName.Next();
            while (featureDataset != null)
            {
                MessageBox.Show("Chichi ki jai ho! == " + featureDataset.Name);

                IFeatureDataset locFeatureDataset = featureWorkspace.OpenFeatureDataset(featureDataset.Name);
                IEnumDataset listFeatureClassDataset = locFeatureDataset.Subsets;
                IDataset locFeatureClass = listFeatureClassDataset.Next();
                while (locFeatureClass != null)
                {
                    MessageBox.Show("Chichi ki jai ho! == " + locFeatureClass.Name);
                    locFeatureClass = listFeatureClassDataset.Next();
                }
                
                featureDataset = listDatasetName.Next();
                
            }
            IWorkspaceEdit workspaceEdit = featureWorkspace as IWorkspaceEdit;
            workspaceEdit.StartEditing(false);

            //Adding the feature class to the Arcmap
            IFeatureClass featureClass = featureWorkspace.OpenFeatureClass("cities_1");
            IFeatureLayer featureLayer = new FeatureLayer();
            featureLayer.FeatureClass = featureClass;
            featureLayer.Name = "GurminderTheGreat";
            ArcMap.Document.ActiveView.FocusMap.AddLayer(featureLayer);

            //Adding feature to the feature class
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
