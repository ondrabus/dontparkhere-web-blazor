// This code was generated by a cloud-generators-net tool 
// (see https://github.com/Kentico/cloud-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System;
using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace KenticoCloudModels
{
    public partial class PublicHoliday
    {
        public const string Codename = "public_holiday";
        public const string DateCodename = "date";

        public DateTime Date { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}