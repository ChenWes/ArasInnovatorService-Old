using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class FabricPartHeaderType
    {
           
      private string versionField;
      private string statusField;
      private string cateogryField;
      private string wovenFabricationField;
      private string knitFabricationField;
      private CustomerBaseType[] customerInfoField;
      private string codeField;
      /// <remarks/>
      public string Version
      {
         get
         {
            return this.versionField;
         }
         set
         {
            this.versionField = value;
         }
      }
      /// <remarks/>
      public string Status
      {
         get
         {
            return this.statusField;
         }
         set
         {
            this.statusField = value;
         }
      }
      /// <remarks/>
      public string Cateogry
      {
         get
         {
            return this.cateogryField;
         }
         set
         {
            this.cateogryField = value;
         }
      }
      /// <remarks/>
      public string WovenFabrication
      {
         get
         {
            return this.wovenFabricationField;
         }
         set
         {
            this.wovenFabricationField = value;
         }
      }
      /// <remarks/>
      public string KnitFabrication
      {
         get
         {
            return this.knitFabricationField;
         }
         set
         {
            this.knitFabricationField = value;
         }
      }
      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("CustomerInfo")]
      public CustomerBaseType[] CustomerInfo
      {
         get
         {
            return this.customerInfoField;
         }
         set
         {
            this.customerInfoField = value;
         }
      }
      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string Code
      {
         get
         {
            return this.codeField;
         }
         set
         {
            this.codeField = value;
         }
      }
    }
}