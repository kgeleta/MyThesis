﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnityFeedback.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("UnityFeedback.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;config&gt;
        ///  &lt;!-- Specify all scenes in correct order --&gt;
        ///  &lt;scenes&gt;
        ///    &lt;scene path=&quot;Assets/Scenes/Scene1.unity&quot;/&gt;
        ///    &lt;scene path=&quot;Assets/Scenes/Scene2.unity&quot;/&gt;
        ///  &lt;/scenes&gt;
        ///
        ///  &lt;connectionString value=&quot;Server=YourServer;Database=YourDatabase;User Id=admin;Password=password;&quot;/&gt;
        ///
        ///  &lt;!-- if PowerShell is added to path variable you can leave this option as is --&gt;
        ///  &lt;powershellPath path=&quot;powershell.exe&quot;/&gt;
        ///
        ///  &lt;!-- uncomment option with your database provider --&gt; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DefaultConfiguration {
            get {
                return ResourceManager.GetString("DefaultConfiguration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;xs:schema attributeFormDefault=&quot;unqualified&quot; elementFormDefault=&quot;qualified&quot; xmlns:xs=&quot;http://www.w3.org/2001/XMLSchema&quot;&gt;
        ///&lt;xs:simpleType name=&quot;provider&quot;&gt;
        ///&lt;xs:restriction base=&quot;xs:string&quot;&gt;
        ///  &lt;xs:enumeration value=&quot;SqlServer&quot; /&gt;
        ///  &lt;xs:enumeration value=&quot;MySql&quot; /&gt;
        ///  &lt;xs:enumeration value=&quot;SQLite&quot; /&gt;
        ///  &lt;xs:enumeration value=&quot;PostgreSQL&quot; /&gt;
        ///&lt;/xs:restriction&gt;
        ///&lt;/xs:simpleType&gt;
        ///
        ///  &lt;xs:element name=&quot;config&quot;&gt;
        ///    &lt;xs:complexType&gt;
        ///      &lt;xs:sequence&gt;
        ///        &lt;xs:element name=&quot;scenes&quot;&gt;
        ///          &lt;xs:compl [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Schema {
            get {
                return ResourceManager.GetString("Schema", resourceCulture);
            }
        }
    }
}
