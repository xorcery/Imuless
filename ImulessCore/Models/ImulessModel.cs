using System.Collections.Generic;
using Newtonsoft.Json;
using Umbraco.Core.Models;

namespace Imuless.Models
{
    public class ImulessModel
    {
        public string Theme { get; set; }
        public IEnumerable<ImulessVarModel> Vars { get; set; }

        public static ImulessModel Deserialize(IContent content, string alias)
        {
            return JsonConvert.DeserializeObject<ImulessModel>(content.GetValue(alias).ToString());
        }
    }
}
