#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Autodesk.Revit.DB.Mechanical;

#endregion

namespace Session01Challenge
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        private XYZ upDirection;

        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var app = uiapp.Application;
            var doc = uidoc.Document;


            var collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(TextNoteType));


            var newPoint = new XYZ(0, 0, 0);

            var t = new Transaction(doc, "test");
            t.Start();
            for (var i = 0; i <= 100; i++)
            {
                XYZ offset = new XYZ(0, i++ / 32, 0);
                newPoint.Add(offset);
                var xxx = Fizzbuzz(i);
                var myTextNote = TextNote.Create(doc, doc.ActiveView.Id, offset, xxx,
                    collector.FirstElementId());

            }

            t.Commit();
            return Result.Succeeded;
        }

        public XYZ UpDirection { get => upDirection; set => upDirection = value; }


        private static string Fizzbuzz(int i)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                return "FIZZBUZZ";
            }

            if (i % 3 == 0)
            {
                return "FIZZ";
            }

            if (i % 5 == 0)
            {
                return "BUZZ";
            }

            return i.ToString();
        }

    }
}
