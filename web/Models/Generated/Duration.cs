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
    public partial class Duration
    {
        public const string Codename = "duration";
        public const string DaysOfWeekCodename = "days_of_week";
        public const string TimeToCodename = "time_to";
        public const string TimeFromCodename = "time_from";

        public IEnumerable<MultipleChoiceOption> DaysOfWeek { get; set; }
        public DateTime? TimeTo { get; set; }
        public DateTime? TimeFrom { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}