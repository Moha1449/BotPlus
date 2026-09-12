using System.Collections;
using System.Collections.Generic;

namespace DataModelLayer.DataModels
{
    public class clsChatTemplate
    {
        public string ID { get; set; }

        public IEnumerable<clsQuestionAndAnswer> Chats { get; set; }
    }
}
