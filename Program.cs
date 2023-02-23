 using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Text;

namespace bptouipath
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = @"C:\Users\Admin\Downloads\BPA.bpprocess";
            //string filepath = @"C:\Users\Admin\Documents\UiPath\.tutorial\Unicorn Quick Tutorial\sam.xaml";

            if (File.Exists(filepath))
            {
                // Read all the content in one string
                // and display the string
                string str = File.ReadAllText(filepath);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(str);
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                //var jobject = JsonConvert.DeserializeObject<JObject>(jsonText);

                //Console.WriteLine(jobject.ContainsKey("process"));


                var obj = JsonConvert.DeserializeObject<dynamic>(jsonText);
                var usableList = obj["process"]["stage"];

                JObject finalobject = new JObject();
                JArray libraries= new JArray("System.Activities",
                                          "System.Activities.Statements",
                                          "System.Activities.Expressions",
                                          "System.Activities.Validation",
                                          "System.Activities.XamlIntegration",
                                          "Microsoft.VisualBasic",
                                          "Microsoft.VisualBasic.Activities",
                                          "System",
                                          "System.Collections",
                                          "System.Collections.Generic",
                                          "System.Data",
                                          "System.Diagnostics",
                                          "System.Drawing",
                                          "System.IO",
                                          "System.Linq",
                                          "System.Net.Mail",
                                          "System.Xml",
                                          "System.Xml.Linq",
                                          "UiPath.Core",
                                          "UiPath.Core.Activities",
                                          "System.Windows.Markup",
                                          "System.Activities.DynamicUpdate",
                                          "UiPath.Platform.ObjectLibrary",
                                          "UiPath.UIAutomationNext.Enums",
                                          "System.Reflection",
                                          "System.Runtime.InteropServices",
                                          "System.Collections.ObjectModel");
                JArray Assemblies = new JArray(
                                         "System.Activities",
                                          "Microsoft.VisualBasic",
                                          "mscorlib",
                                          "System.Data",
                                          "System",
                                          "System.Drawing",
                                          "System.Core",
                                          "System.Xml",
                                          "System.Xml.Linq",
                                          "PresentationFramework",
                                          "WindowsBase",
                                          "PresentationCore",
                                          "System.Xaml",
                                          "UiPath.System.Activities",
                                          "UiPath.UiAutomation.Activities",
                                          "UiPath.Platform",
                                          "UiPath.System.Activities.Design",
                                          "UiPath.UIAutomationNext");
                
                JArray Variablesarray = new JArray();
                JArray Assignarray = new JArray();
                finalobject.Add("Activity", new JObject(
                    new JProperty("@mc:Ignorable", "sap sap2010"),
                    new JProperty("@x:Class", "Main"),
                    new JProperty("@mva:VisualBasic.Settings", "{x:Null}"),
                    new JProperty("@sap:VirtualizedContainerService.HintSize", "868,691"),
                    new JProperty("@sap2010:WorkflowViewState.IdRef", "ActivityBuilder_1"),
                    new JProperty("@xmlns", "http://schemas.microsoft.com/netfx/2009/xaml/activities"),
                    new JProperty("@xmlns:mc", "http://schemas.openxmlformats.org/markup-compatibility/2006"),
                    new JProperty("@xmlns:mva", "clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities"),
                    new JProperty("@xmlns:sap", "http://schemas.microsoft.com/netfx/2009/xaml/activities/presentation"),
                    new JProperty("@xmlns:sap2010", "http://schemas.microsoft.com/netfx/2010/xaml/activities/presentation"),
                    new JProperty("@xmlns:scg", "clr-namespace:System.Collections.Generic;assembly=mscorlib"),
                    new JProperty("@xmlns:ui", "http://schemas.uipath.com/workflow/activities"),
                    new JProperty("@xmlns:x", "http://schemas.microsoft.com/winfx/2006/xaml"),
                    new JProperty("TextExpression.NamespacesForImplementation",new JObject(
                                   new JProperty("scg:List",new JObject(
                                       new JProperty("@x:TypeArguments", "x:String"),
                                       new JProperty("@Capacity", "52"),
                                       new JProperty("x:String", libraries
                                       ))))),
                    new JProperty("TextExpression.ReferencesForImplementation",new JObject(
                                   new JProperty("scg:List",new JObject(
                                       new JProperty("@x:TypeArguments", "AssemblyReference"),
                                       new JProperty("@Capacity", "18"),
                                       new JProperty("AssemblyReference", Assemblies
                                       ))))),
                    new JProperty("Sequence", new JObject(
                                   new JProperty("@sap:VirtualizedContainerService.HintSize", "139,626"),
                                   new JProperty("@sap2010:WorkflowViewState.IdRef", "Sequence_2"),
                                   new JProperty("Sequence.Variables",new JObject(
                                       new JProperty("Variable", Variablesarray)
                                       )),
                                   new JProperty("sap:WorkflowViewStateService.ViewState", new JObject(
                                       new JProperty("scg:Dictionary", new JObject(
                                           new JProperty("@x:TypeArguments", "x:String, x:Object"),
                                           new JProperty("x:Boolean", new JObject(
                                               new JProperty("@x:Key", "IsExpanded"),
                                               new JProperty("#text", "True")
                                               ))
                                           ))
                                       )),
                                   new JProperty("Assign",Assignarray)
                    ))));

                foreach (JObject item in usableList)
                {
                    string activitytype = item["@type"].ToString();
                    
                    if (activitytype == "Data")
                    {
                        JObject variables = new JObject();
                        JObject assignvariables = new JObject();
                        JProperty hs = new JProperty("@sap:VirtualizedContainerService.HintSize", "297,60");
                        JProperty id = new JProperty("@sap2010:WorkflowViewState.IdRef", "Assign" + item["@name"]);
                        assignvariables.Add(hs);
                        assignvariables.Add(id);

                        string datatype = item["datatype"].ToString();
                        if(datatype == "number")
                        {
                            JProperty dt = new JProperty("@x:TypeArguments", "x:Int32");
                            variables.Add(dt);
                            variables.Add("@Name", item["@name"]);
                            variables.Add("@Default", item["initialvalue"]);

                            JProperty to = new JProperty("Assign.To", new JObject(
                                new JProperty("OutArgument", new JObject(
                                    new JProperty("@x:TypeArguments", "x:Int32"),
                                    new JProperty("#text","["+ item["@name"]+"]")
                                    ))));
                            JProperty value = new JProperty("Assign.Value",new JObject(
                                new JProperty("InArgument", new JObject(
                                    new JProperty("@x:TypeArguments", "x:Int32"),
                                    new JProperty("#text", item["initialvalue"])
                                    ))));
                            assignvariables.Add(to);
                            assignvariables.Add(value);
                        }

                        
                        Variablesarray.Add(variables);
                        Assignarray.Add(assignvariables);
                    }
                    if (activitytype == "Decision")
                    {
                        string name = item["@name"].ToString();
                        string expression = item["decision"]["@expression"].ToString();
                        string ontrue = item["ontrue"].ToString();
                        string onfalse = item["onfalse"].ToString();
                        JObject ontrueobject = new JObject();
                        JObject onfalseobject = new JObject();
                        foreach(JObject item2 in usableList)
                        {
                            if(item2["@stageid"].ToString()==ontrue)
                            {
                                ontrueobject = item2;
                            }
                            if (item2["@stageid"].ToString() == onfalse)
                            {
                                onfalseobject = item2;
                            }
                        }
                        string ontrueobjecttype = ontrueobject["@type"].ToString();
                        string onfalseobjecttype = onfalseobject["@type"].ToString();
                        string ontrueexpression = null;
                        string onfalseexpression = null;
                        if(ontrueobjecttype == "Alert")
                        {
                            ontrueexpression = ontrueobject["alert"]["@expression"].ToString();
                            onfalseexpression = onfalseobject["alert"]["@expression"].ToString();
                        }
                        JObject ifobject = new JObject();
                        JProperty ifproperty = new JProperty("If", new JObject(
                                       new JProperty("@Condition", "["+expression.Replace("[", "").Replace("]", "")+ "]"),
                                       new JProperty("@sap:VirtualizedContainerService.HintSize", "797,334"),
                                       new JProperty("@sap2010:WorkflowViewState.IdRef", "If_1"),
                                       new JProperty("If.Then", new JObject(
                                           new JProperty("Sequence", new JObject(
                                               new JProperty("@sap:VirtualizedContainerService.HintSize", "476,176"),
                                               new JProperty("@sap2010:WorkflowViewState.IdRef", "Sequence_3"),
                                               new JProperty("sap:WorkflowViewStateService.ViewState", new JObject(
                                                   new JProperty("scg:Dictionary", new JObject(
                                                       new JProperty("@x:TypeArguments", "x:String, x:Object"),
                                                       new JProperty("x:Boolean", new JObject(
                                                           new JProperty("@x:Key", "IsExpanded"),
                                                           new JProperty("#text", "True")
                                                           ))
                                                       ))
                                                   )),
                                               new JProperty("ui:MessageBox",new JObject(
                                                   new JProperty("@Caption", "{x:Null}"),
                                                   new JProperty("@ChosenButton", "{x:Null}"),
                                                   new JProperty("@AutoCloseAfter", "00:00:00"),
                                                   new JProperty("@DisplayName", "Message Box"),
                                                   new JProperty("@sap:VirtualizedContainerService.HintSize", "334,84"),
                                                   new JProperty("@sap2010:WorkflowViewState.IdRef", "MessageBox_1"),
                                                   new JProperty("@Text", "[\" Hi Hello\"]")
                                                   ))
                                               ))
                                           )),
                                       new JProperty("If.Else",new JObject(
                                           new JProperty("Sequence",new JObject(
                                               new JProperty("@sap:VirtualizedContainerService.HintSize", "576,176"),
                                               new JProperty("@sap2010:WorkflowViewState.IdRef", "Sequence_4"),
                                               new JProperty("sap:WorkflowViewStateService.ViewState", new JObject(
                                                   new JProperty("scg:Dictionary", new JObject(
                                                       new JProperty("@x:TypeArguments", "x:String, x:Object"),
                                                       new JProperty("x:Boolean", new JObject(
                                                           new JProperty("@x:Key", "IsExpanded"),
                                                           new JProperty("#text", "True")
                                                           ))
                                                       ))
                                                   )),
                                                new JProperty("ui:MessageBox", new JObject(
                                                   new JProperty("@Caption", "{x:Null}"),
                                                   new JProperty("@ChosenButton", "{x:Null}"),
                                                   new JProperty("@AutoCloseAfter", "00:00:00"),
                                                   new JProperty("@DisplayName", "Message Box"),
                                                   new JProperty("@sap:VirtualizedContainerService.HintSize", "334,84"),
                                                   new JProperty("@sap2010:WorkflowViewState.IdRef", "MessageBox_2"),
                                                   new JProperty("@Text", "[\"Good Morning\"]")
                                                   ))
                                               ))
                                           ))
                                       )
                            );

                        ifobject.Add(ifproperty);
                        //finalobject["Activity"]["Sequence"].Add(ifproperty);
                        JObject temp = finalobject["Activity"]["Sequence"] as JObject;
                        temp.Add(ifproperty);
                        finalobject["Activity"]["Sequence"] = temp;

                    }
                    //finalobject = converter(item, finalobject);
                    //converter(item);

                }


                //foreach (JProperty x in jobject.SelectToken("process"))
                //{
                //    var qwe = jobject.Last.Last.Last;




                //    JToken type = x.Value.SelectToken("stage");
                //    string typeStr = type.ToString().ToLower();
                //    Console.WriteLine(typeStr);


                //}
                //string finalstring = JsonConvert.SerializeObject(finalobject);
                //XmlDocument xmldoc = new XmlDocument();
                //xmldoc.LoadXml(finalstring);
                //File.WriteAllText(@"C:\Users\Admin\Documents\videogames3.xaml", xmldoc);
                // Convert JObject to XML
                //string jsonAsString = finalobject.ToString();
                //XElement xml = XElement.Parse(JObject.Parse(jsonAsString).ToString());

                //// Save the XML to a file
                //string filePath = @"C:\Users\Admin\Documents\videogames3.xaml";
                //using (StreamWriter writer = new StreamWriter(filePath))
                //{
                //    xml.Save(writer);
                //}
                string jsonString = finalobject.ToString();
                // Parse the JSON string into an XElement

                // Convert JSON to XML without root element
                XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(jsonString, "");

                // Get the first child node (i.e., the root node) and remove it

                xmlDoc.Save(@"C:\Users\Admin\Documents\uipathsample.xaml");

            }

        }
    }
}
//public static JObject converter(JObject initialobject, JObject finalObject)
//{
//    Console.WriteLine(initialobject["@type"]);
//    string activitytype = initialobject["@type"].ToString();
//    if (activitytype == "Data")
//    {
//        Console.WriteLine("hi");
//    }
//    return finalObject;
//}